using WeatherAPI_DebugVersion.Properties;

namespace WeatherAPI_DebugVersion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //WeatherService weatherService = new WeatherService();
            //weatherService.StartReading();
            builder.Services.AddSingleton<IWeather,TestWeatherService>();
            var app = builder.Build();

            app.UseStaticFiles();

            app.UseMiddleware<WeatherMiddleware>();

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync("wwwroot/html/weather.html");
            });



            app.Run();
        }
    }
}
