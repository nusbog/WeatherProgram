using System;
using System.IO;
using System.Net;

namespace WeatherAPIConsole
{
    class Program
    {
        static void Get()
        {
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=Stockholm&appid=c6a2fb09b303bc24611ed5f3b3ff67d4");
            }

            File.WriteAllText("../../../response.json", response);
        }

        static void Main(string[] args)
        {
            Get();
        }
    }
}
