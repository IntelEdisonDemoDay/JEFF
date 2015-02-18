
// prepares the plot
function SetPlot(plotName, plotLegend, title, series) {
    var plot = $.plot(plotName,
                    [
                        {
                            label: title,
                            data: series,
                            lines: { show: true },
                            points: { show: false }
                        }
                    ],
                    {
                        colors: ['#0094FF'],
                        shadowSize: 0,
                        xaxis: {
                            mode: "time",
                            timeformat: "%H %M %S",
                            timezone: "browser",
                            tickLength: 0
                        },
                        yaxes: [
                            { min: 0 }
                        ],
                        grid: {
                            backgroundColor: { colors: ["#fff", "#fff"] },
                            borderWidth: 1,
                            borderColor: '#dedede',
                            hoverable: true
                        },
                        legend: {
                            container: $(plotLegend),
                            labelBoxBorderColor: null,
                            noColumns: 2
                        }
                    });
    return plot;
}


// View Model
function DashboardVM() {
    var self = this;

    self.temp1Plot = null;
    self.hum1Plot = null;
    self.light1Plot = null;

    // load a sensor's data
    self.loadSensorData = function (sensorId, onLoaded) {
        api.get("~/api/Dashboard/SensorsData?sensorId=" + sensorId).done(function (vm) {
            var model = ko.mapping.fromJS(vm);

            var series = [];
            $.each(model.Series()[0].Items(), function (index, value) {
                var seriesItem = [];
                seriesItem.push(new Date((value.Ticks() - 621355968000000000) / 10000));
                seriesItem.push(value.Value());
                series.push(seriesItem);
            });

            onLoaded(model.Series()[0].Title(), series);
        });
    };

    // sets up a dynamic plot
    self.setDynamicPlot = function (sensorId, plotName, legendName, plot) {
        setInterval(
            function () {
                self.loadSensorData(sensorId,
                    function (title, series) {
                        if (plot == null) {
                            plot = SetPlot(plotName, legendName, title, series);
                        }
                        else {
                            plot.setData([series]);
                            plot.setupGrid();
                            plot.draw();
                        }
                    })
            }, 1000);
    };

    // start method (creates dynamic plots)
    self.start = function () {
        self.setDynamicPlot("TempMoist 1 - Temperature", "#temp1-stats-chart", "#temp1-stats-chart-legend", self.temp1Plot);
        self.setDynamicPlot("TempMoist 1 - Humidity", "#hum1-stats-chart", "#hum1-stats-chart-legend", self.hum1Plot);
        self.setDynamicPlot("Light 1", "#light1-stats-chart", "#light1-stats-chart-legend", self.light1Plot);
    }

    self.start();
}

/* INIT */
var vm = new DashboardVM();

$(document).ready(function () {

    timezoneJS.timezone.zoneFileBasePath = window.BASE_API_ADDRESS + "/Scripts/timezone/tz";
    timezoneJS.timezone.defaultZoneFile = [];
    timezoneJS.timezone.init({ async: false });

    // setup tooltips for plots
    // TODO: changed to func with params
    $("<div id='tooltip'></div>").css({
        position: "absolute",
        display: "none",
        border: "1px solid #fdd",
        padding: "2px",
        "background-color": "#fee",
        opacity: 0.80
    }).appendTo("body");

    $('#temp1-stats-chart').css({ 'width': '100%', 'height': '300px' });
    $("#temp1-stats-chart").bind("plothover", function (event, pos, item) {
        if (item) {
            var x = item.datapoint[0].toFixed(2),
                y = item.datapoint[1].toFixed(2);

            $("#tooltip").html(item.series.label + " = " + y)
                .css({ top: item.pageY + 5, left: item.pageX + 5 })
                .fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });

    $('#hum1-stats-chart').css({ 'width': '100%', 'height': '300px' });
    $("#hum1-stats-chart").bind("plothover", function (event, pos, item) {
        if (item) {
            var x = item.datapoint[0].toFixed(2),
                y = item.datapoint[1].toFixed(2);

            $("#tooltip").html(item.series.label + " = " + y)
                .css({ top: item.pageY + 5, left: item.pageX + 5 })
                .fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });

    $('#light1-stats-chart').css({ 'width': '100%', 'height': '300px' });
    $("#light1-stats-chart").bind("plothover", function (event, pos, item) {
        if (item) {
            var x = item.datapoint[0].toFixed(2),
                y = item.datapoint[1].toFixed(2);

            $("#tooltip").html(item.series.label + " = " + y)
                .css({ top: item.pageY + 5, left: item.pageX + 5 })
                .fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });

    ko.applyBindings(vm);

});

