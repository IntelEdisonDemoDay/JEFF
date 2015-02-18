var xhrRequest = function (url, type, callback) {
    var xhr = new XMLHttpRequest();
    xhr.onload = function () {
        callback(this.responseText);
    };
    xhr.open(type, url);
    xhr.send();
};

function getTemperature() {
    // TODO: specify your API + BOARD ID + SENSOR
    var url = "https://cloud.smartables.io/api/read/.../..../SENSE/...";

    // Send request to Smartables
    xhrRequest(url, 'GET',
               function (responseText) {
                   // responseText contains a JSON object with weather info
                   var json = JSON.parse(responseText);
                   console.log("Response is " + responseText);

                   // Temperature in Kelvin requires adjustment
                   console.log("Temperature is " + json.temp);

                   // Assemble dictionary using our keys
                   var dictionary = {
                       "KEY_TEMPERATURE": json.temp.toString(),
                   };

                   // Send to Pebble
                   Pebble.sendAppMessage(dictionary,
                                         function (e) {
                                             console.log("Temperature info sent to Pebble successfully!");
                                         },
                                         function (e) {
                                             console.log("Error sending weather info to Pebble!");
                                         }
                                        );
               }
              );
}

// Listen for when the watchface is opened
Pebble.addEventListener('ready',
                        function (e) {
                            console.log("PebbleKit JS ready!");

                            // Get the initial weather
                            getTemperature();
                        }
                       );

// Listen for when an AppMessage is received
Pebble.addEventListener('appmessage',
                        function (e) {
                            console.log("AppMessage received!");
                            getTemperature();
                        }
                       );
