using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter a location (e.g. city or address):");
            string location = Console.ReadLine();

            string weatherApiKey = "YOUR_OPENWEATHERMAP_API_KEY";
            string geolocationApiKey = "YOUR_GOOGLE_MAPS_GEOLOCATION_API_KEY";

            try
            {
                string geolocationData = await GetGeolocationDataAsync(location, geolocationApiKey);
                string latitude = ParseGeolocationData(geolocationData, "lat");
                string longitude = ParseGeolocationData(geolocationData, "lng");

                if (latitude != null && longitude != null)
                {
                    string weatherData = await GetWeatherDataAsync(latitude, longitude, weatherApiKey);
                    string weatherDescription = ParseWeatherData(weatherData, "description");
                    string temperature = ParseWeatherData(weatherData, "temp");
                    string humidity = ParseWeatherData(weatherData, "humidity");
                    string windSpeed = ParseWeatherData(weatherData, "wind_speed");

                    Console.WriteLine($"Weather in {location}:");
                    Console.WriteLine($"Description: {weatherDescription}");
                    Console.WriteLine($"Temperature: {temperature}°C");
                    Console.WriteLine($"Humidity: {humidity}%");
                    Console.WriteLine($"Wind Speed: {windSpeed} m/s");
                }
                else
                {
                    Console.WriteLine($"Unable to retrieve geolocation data for {location}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task<string> GetGeolocationDataAsync(string location, string apiKey)
        {
            string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={location}&key={apiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        static async Task<string> GetWeatherDataAsync(string latitude, string longitude, string apiKey)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        static string ParseGeolocationData(string geolocationData, string field)
        {
            JObject data = JObject.Parse(geolocationData);
            return data["results"]?.FirstOrDefault()?["geometry"]?[field]?.ToString();
        }

        static string ParseWeatherData(string weatherData, string field)
        {
            JObject data = JObject.Parse(weatherData);
            return data["main"]?[field]?.ToString();
        }
    }
}