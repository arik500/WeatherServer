const temp = document.getElementById("temp");
const hum = document.getElementById("hum");

async function getWeather() {
    const response = await fetch("/weather/get", {
        method: "GET",
        headers: { "Accept": "application.json"}
    });
    if (response.ok === true) {
        const weather = await response.json();
        temp.innerText = weather.temperature + "°";
        hum.innerText = weather.humidity + "%";
    }
}

getWeather();