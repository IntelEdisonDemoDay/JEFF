## About JEFF

For more information about JEFF, please visit http://www.jeff-it.co/.

On the weekend of 23rd to 25th January 2015, I participated to the "Seedhack IoT" (http://www.seedhack.com/), a hackaton on the INternet of Things organized by Seedcamp @ Google Campus London.

This code was developed during the hackaton to implement the brain of JEFF, our "digital butler" project.

JEFF would collect environmental data from several sensors connected to a Smartables board (http://smartables.io/), then a worker role in Azure would process the data, store it on a remote MongoDB instance (on MongoLab), then send back some commands (e.g. to open a window if the temperature is too high).

## Project structure

- JEFF.Models: entities for the MongoDB instance
- JEFF.DataAccess: DB context and repository for the MongoDB instance
- JEFF.Dto: Request and repsonse POCOs to communicate with REST APIs (Smartables)
- JEFF.GreenHouseController: JEFF's brain (aka implementation of the Azure Worker Role)
- JEFF.Cloud: Azure Cloud Service project used to deploy the GreenHouseController
- JEFF.Portal: ASP.MVC application used to visualise trends of sensor data stored in the MongodDB instance
- Pebble files: source files for your Pebble Watch to display one sensor's data directly from the Smartables REST API

## Requirements

All dependencies should be available as NuGet packages, however you will need to have Visual Studio 2013 + Azure SDK installed in order to open all the projects.

## References

http://www.jeff-it.co/
http://www.seedhack.com/
http://smartables.io/
https://cloudpebble.net/
http://docs.mongodb.org/ecosystem/drivers/csharp/
http://www.newtonsoft.com/json
http://www.asp.net/mvc
http://azure.microsoft.com/en-us/services/cloud-services/

## Tests

Regretfully there are no unit tests on this project, mainly due to the fact that we only had a couple of days to hack the whole thing together and present it to a jury.

## License

This code is available under the MIT license.