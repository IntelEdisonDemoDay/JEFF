using JEFF.DataAccess;
using JEFF.Dto.Smartables.Response;
using JEFF.GreenHouseController.Sensors;
using JEFF.Models;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace JEFF.GreenHouseController
{
    /// <summary>
    /// WorkerRole
    /// </summary>
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        MongoContext _context;
        MongoSensorReadingsRepository _readingsRepository;

        GreenHouse house;
        SmartablesBoard board;

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public override void Run()
        {
            Trace.TraceInformation("GreenHouseController is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        /// <summary>
        /// Called when the role starts.
        /// </summary>
        /// <returns></returns>
        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // TODO: Set your keys!
            string apiKey = "";
            string apiSecret = "";
            string boardID = "";

            // Init board
            board = new SmartablesBoard("https://cloud.smartables.io/api/", apiKey, apiSecret, boardID);

            // Init greenhouse 
            house = new GreenHouse(board);
            house.Light1 = new LightSensor(board)
            {
                Name = "Light 1",
                ChannelName = "SENSE/6",
            };
            house.Water1 = new WaterSensor(board)
            {
                Name = "Water 1",
                ChannelName = "SENSE/5",
            };
            house.TempMoist1 = new TempMoistureSensor(board)
            {
                Name = "TempMoist 1",
                ChannelName = "SENSE/4",
            };
            house.Relay1 = new Relay(board)
            {
                Name = "Relay 1",
                ChannelName = "ACT/2",
            };
            house.Motor1 = new Motor(board)
            {
                Name = "Motor 1",
                ChannelName = "ACT/3"
            };
            house.Rgb1 = new Rgb(board)
            {
                Name = "Rgb 1",
                ChannelName = "ACT/1",
                ChannelId = 1
            };

            // init Mongo
            _context = new MongoContext();
            _readingsRepository = new MongoSensorReadingsRepository(_context);

            bool result = base.OnStart();

            Trace.TraceInformation("GreenHouseController has been started");

            return result;
        }

        /// <summary>
        /// Called when the role is stopped.
        /// </summary>
        public override void OnStop()
        {
            Trace.TraceInformation("GreenHouseController is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("GreenHouseController has stopped");
        }

        /// <summary>
        /// Runs the role asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task RunAsync(CancellationToken cancellationToken)
        {
            Trace.TraceInformation("Working");

            while (!cancellationToken.IsCancellationRequested)
            {
                SensorReading light1 = (await house.Light1.Read())[0];
                await Task.Delay(200);

                SensorReading water1 = (await house.Water1.Read())[0];
                await Task.Delay(200);

                SensorReading[] temp1 = await house.TempMoist1.Read();
                await Task.Delay(200);

                // init readings
                SafeDBUpdate(light1);
                SafeDBUpdate(temp1[0]);
                SafeDBUpdate(temp1[1]);

                OpenDoor(temp1[0]);
                OpenWindow(temp1[0]);
                AddLight(light1);

                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// Opens the greenhouse door.
        /// </summary>
        /// <param name="temperatureReading">The temperature reading.</param>
        private void OpenDoor(SensorReading temperatureReading)
        {
            if (temperatureReading.IsValid)
            {
                bool openDoor = temperatureReading.Value > 28;
                if (house.Relay1.Value != openDoor)
                {
                    house.Relay1.Value = openDoor;
                    SafeOutputWrite(async () =>
                    {
                        return await house.Relay1.Write();
                    });
                }
            }
        }

        /// <summary>
        /// Opens the greenhouse hatch.
        /// </summary>
        /// <param name="temperatureReading">The temperature reading.</param>
        private void OpenWindow(SensorReading temperatureReading)
        {
            if (temperatureReading.IsValid)
            {
                // 0 = 20 C
                // 180 = 50 C
                int angle = (int)(((temperatureReading.Value - 20) / 30) * 180);
                if (house.Motor1.Angle != angle)
                {
                    house.Motor1.Angle = angle;
                    SafeOutputWrite(async () =>
                    {
                        return await house.Motor1.Write();
                    });
                }
            }
        }

        /// <summary>
        /// Adds light to the greenhouse.
        /// </summary>
        /// <param name="lightReading">The light reading.</param>
        private void AddLight(SensorReading lightReading)
        {
            if (lightReading.IsValid)
            {
                // 0 = FFFFFF
                // 580 = 000000
                Color color = Color.FromArgb((int)(0xFFFFFF / (lightReading.Value / 580)));

                if (house.Rgb1.Color != color)
                {
                    house.Rgb1.Color = color;
                    SafeOutputWrite(async () =>
                    {
                        return await house.Rgb1.Write();
                    });
                }
            }

        }

        /// <summary>
        /// Safely writes to an output.
        /// </summary>
        /// <param name="writeAction">The write action.</param>
        /// <remarks>Retires up to 20 times, with 0.2s waiting time betwwen attempts.</remarks>
        private async void SafeOutputWrite(Func<Task<PortUpdateResponse>> writeAction)
        {
            int retryCount = 0;
            PortUpdateResponse result = await writeAction();
            await Task.Delay(200);

            while (!result.IsValid && retryCount < 20)
            {
                result = await writeAction();
                retryCount++;
                await Task.Delay(200);
            }
        }

        /// <summary>
        /// Safely updates the DB. Skips invalid values.
        /// </summary>
        /// <param name="reading">The reading.</param>
        private void SafeDBUpdate(SensorReading reading)
        {
            if (reading.IsValid)
                _readingsRepository.Update(reading);
        }
    }
}
