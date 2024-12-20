namespace WeatherAPI_DebugVersion
{
    public interface IWeather
    {
        float Temperature { get; }
        float Humidity { get; }
        Weater GetWeather();
    }
}
