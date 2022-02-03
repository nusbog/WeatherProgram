using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPIConsole
{
    class Program
    {
        static void Get(string city)
        {
            string response;

            using (WebClient client = new WebClient())
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=c6a2fb09b303bc24611ed5f3b3ff67d4";
                response   = client.DownloadString(url);
            }

            WeatherData.Root data = JsonSerializer.Deserialize<WeatherData.Root>(response);
        }

        static void Main()
        {
            string city = Console.ReadLine();
        }
    }
}
