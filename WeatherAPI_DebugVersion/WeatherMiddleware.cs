namespace WeatherAPI_DebugVersion
{
    public class WeatherMiddleware
    {
        private readonly RequestDelegate _next;
        public WeatherMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context,IWeather weatherService)
        {
            if(context.Request.Path == "/weather/get" && context.Request.Method == "GET")
            {
                Weater currentWeather = weatherService.GetWeather();
                await Console.Out.WriteLineAsync($"Take data:\nt:{currentWeather.Temperature}\nh:{currentWeather.Humidity}");
                await context.Response.WriteAsJsonAsync(currentWeather);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
