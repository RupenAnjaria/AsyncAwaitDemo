using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityInfo
{
    public class ApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<weatherResultWithAlert> GetWeatherAsync(string cityName, string date, int preferredTemperature, int rainPercentage)
        {
            // Fictional API URL
            //string apiUrl = $"https://api.example.com/weather?city={cityName}&date={date}";

            //// Send the GET request
            //HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            //response.EnsureSuccessStatusCode();

            //// Read the response content as a string
            //string jsonResponse = await response.Content.ReadAsStringAsync();

            //// Deserialize the JSON response into a weatherResult object
            //weatherResult result = JsonSerializer.Deserialize<weatherResult>(jsonResponse);
            weatherResult result = new weatherResult { Temperature = "150°F", Condition="Head", Wind="10", Rain = 90 };

            // Create a weatherResultWithAlert object and copy the weatherResult properties
            weatherResultWithAlert resultWithAlert = new weatherResultWithAlert
            {
                Temperature = result.Temperature,
                Condition = result.Condition,
                Rain = result.Rain,
                Wind = result.Wind
            };

            // Calculate the alert based on the temperature
            if (double.TryParse(result.Temperature.Replace("°F", ""), out double temperature) && temperature > preferredTemperature)
            {
                resultWithAlert.Alert = $"Temperature is above {preferredTemperature}°F.";
            }

            if (result.Rain > rainPercentage)
            {
                resultWithAlert.Alert += $"\nRain percentage is above {rainPercentage}%.";
            }

            return resultWithAlert;
        }

        public async Task<string> GetCityInfoAsync(string cityName, string dateString, int preferredTemperature, int rainPercentage)
        {
            // Start the three API calls in parallel
            Task<string> historyTask = GetCityHistoryAsync(cityName);
            Task<weatherResultWithAlert> weatherTask = GetWeatherAsync(cityName, dateString, preferredTemperature, rainPercentage);
            Task<string> placesTask = GetBestPlacesToVisitAsync(cityName, dateString);

            // Wait for all the tasks to complete
            await Task.WhenAll(historyTask, weatherTask, placesTask);

            // Combine the results
            string historyDetails = historyTask.Result;
            weatherResultWithAlert weatherDetails = weatherTask.Result;
            string placesDetails = placesTask.Result;

            // Assuming weatherResultWithAlert has a ToString() method that formats it as a string,
            // or you need to manually format it.
            string formattedWeatherDetails = weatherDetails.ToString(); // or manually format the weatherResult object

            // Format the result
            string result = $"City: {cityName}\nDate: {dateString}\n\nHistory:\n{historyDetails}\n\nWeather:\n{formattedWeatherDetails}\n\nBest Places to Visit:\n{placesDetails}";

            return result;
        }

        private async Task<string> GetCityHistoryAsync(string cityName)
        {
            // Simulate an asynchronous API call to fetch city history
            await Task.Delay(1000); // Simulate network delay

            // Example API response
            return "Founded in 1234, the city has a rich history of trade and culture.";
        }

        private async Task<string> GetBestPlacesToVisitAsync(string cityName, string dateString)
        {
            // Simulate an asynchronous API call to fetch best places to visit
            await Task.Delay(1000); // Simulate network delay

            // Example API response
            return "1. Central Park\n2. City Museum\n3. River Walk";
        }
    }

}
