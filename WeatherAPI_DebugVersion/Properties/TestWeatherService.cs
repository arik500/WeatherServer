namespace WeatherAPI_DebugVersion.Properties
{
    public class TestWeatherService : IWeather
    {
        public TestWeatherService()
        {
            Temperature = 44;
            Humidity = 55;
        }
        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public Weater GetWeather()
        {
            return new Weater(Temperature,Humidity);
        }
    }
}
