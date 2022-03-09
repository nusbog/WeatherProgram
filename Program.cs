using System.Globalization;
using System.Text.Json;
using System.Net;
using System.IO;
using System;

namespace WeatherAPIConsole
{
    class Program
    {
        static string key = "";
        static bool keyIsSet = false;

        static void SetKey(string keyPath) {
            // Kolla om sökvägen till API-nyckeln finns
            try {
                key = File.ReadAllText(keyPath);
                keyIsSet = true;
            } catch {
                Console.WriteLine(keyPath + " finns inte eller är inte en API-nyckel, vänligen specificera sökvägen till din API-nyckel: ");
                SetKey(Console.ReadLine());
            }
        }

        static WeatherData.Root Get(string city)
        {
            string response;

            using (WebClient client = new WebClient())
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + key + "&units=metric";

                // Kolla om data för staden existerar
                try
                {
                    response = client.DownloadString(url);
                } catch {
                    Console.WriteLine('\"' + city + "\" är ingen stad");
                    return null;
                }
            }

            WeatherData.Root data = JsonSerializer.Deserialize<WeatherData.Root>(response);
            return data;
        }

        static void WriteData(string city, WeatherData.Root data) {
            string writeData =
                "--- " + city.ToUpper() + ", " + data.sys.country.ToUpper() + " ---\n" +
                "lon: " + data.coord.lon + " lat: " + data.coord.lat + "\n" +
                "temp: " + data.main.temp + " °C\n" +
                "feels like: " + data.main.feels_like + " °C\n" +
                "humidity: " + data.main.humidity + "\n" +
                "pressure: " + data.main.pressure + "\n" +
                "wind speed: " + data.wind.speed + "\n";
            
            Console.WriteLine(writeData);

            // Filhantering
            string date = DateTime.Today.ToString().Replace(" 00:00:00", ""); // Få endast datum, ej tid
            File.WriteAllText(city + '_' + date + ".txt", writeData); // Ex: Stockholm_2022-01-01
        }

        static void Main()
        {
            // Läs inte in nyckeln om den redan är inläst
            if(!keyIsSet)
                SetKey("key.txt");

            Console.WriteLine("Skriv stad: ");
            string city = Console.ReadLine();
            WeatherData.Root data = Get(city);

            if(data != null) {
                WriteData(city, data);
            }

            Main();
        }
    }
}
