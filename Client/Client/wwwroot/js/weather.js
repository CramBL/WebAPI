const connection = new signalR.HubConnectionBuilder().withUrl("/weatherHub").build();

connection.on("ReceiveNewWeatherData", function (weatherData) {
    var Date = weatherData.Date;
    var Temperature = weatherData.Temperature;
    var AirHumidity = weatherData.AirHumidity;
    var AirPressure = weatherData.AirPressure;
    var ObservedLocation = weatherData.ObservedLocation;
    var LocationName = ObservedLocation.LocationName;
    var Latitude = ObservedLocation.Latitude;
    var Longitude = ObservedLocation.Longitude;

    document.getElementById("date").innerHTML = Date;
    document.getElementById("temperature").innerHTML = Temperature;
    document.getElementById("airHumidity").innerHTML = AirHumidity;
    document.getElementById("airPressure").innerHTML = AirPressure;
    document.getElementById("locationName").innerHTML = LocationName;
    document.getElementById("latitude").innerHTML = Latitude;
    document.getElementById("longitude").innerHTML = Longitude;

});




//connection.on("ReceiveMessage", function (user, message) {
//    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//    var encodedMsg = user + " says " + msg;
//    var li = document.createElement("li");
//    li.textContent = encodedMsg;
//    document.getElementById("messagesList").appendChild(li);
//});