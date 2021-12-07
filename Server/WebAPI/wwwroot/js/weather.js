const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start();

connection.on("ReceiveNewWeatherData", function (weatherData) {
    console.log("Bob is here");
    var Date = weatherData.date;
    var Temperature = weatherData.temperature;
    var AirHumidity = weatherData.airHumidity;
    var AirPressure = weatherData.airPressure;
    var ObservedLocation = weatherData.observedLocation;
    var LocationName = ObservedLocation.locationName;
    var Latitude = ObservedLocation.latitude;
    var Longitude = ObservedLocation.longitude;

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