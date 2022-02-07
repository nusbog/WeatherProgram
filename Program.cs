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
            string key = "";

            using (WebClient client = new WebClient())
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + key;
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
