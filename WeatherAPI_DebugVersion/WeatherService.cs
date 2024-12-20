using System.IO;
using System.IO.Ports;
using System.Text;

namespace WeatherAPI_DebugVersion
{
    public class Weater
    {
        public Weater(float t, float h)
        {
            Temperature = t;
            Humidity = h;
        }
        public float Temperature { get; set; }
        public float Humidity{ get; set; }
    }
    public class WeatherService : IWeather
    {
        private SerialPort _sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        private bool IsActive = true;
        public bool IsOpening { get; private set; } = false;
        public float Temperature { get; private set; } = 0;
        public float Humidity { get; private set; } = 0;

        public Weater GetWeather()
        {
            Console.WriteLine("Data was read");
            return new Weater(Temperature,Humidity);
        }
        public void Close()
        {
            IsActive = false;
            Thread.Sleep(100);
            _sp.Close();
        }
        public async void StartReading()
        {
            await Task.Run(() =>
            {
                _sp.Open();

                while (_sp.BytesToRead <= 0)
                {
                    Thread.Sleep(1000);
                }
                Thread.Sleep(3000);

                _sp.ReadExisting();
                Console.WriteLine("Weather Service Started Reading");
                for (; IsActive;)
                {
                    string data = _sp.ReadExisting();
                    string[] tempAndHum = data.Split('/');
                    try
                    {
                        tempAndHum[0] = tempAndHum[0].Replace(".", ",");
                        tempAndHum[1] = tempAndHum[1].Replace(".", ",");
                        Temperature = float.Parse(tempAndHum[0]);
                        Humidity = float.Parse(tempAndHum[1]);
                    }
                    catch
                    {
                        Console.WriteLine("ERROR. DATA IS INVALID");
                        Temperature = 0;
                        Humidity = 0;
                    }

                    IsOpening = true;
                    Thread.Sleep(1500);
                }
            });
        }
    }
}
