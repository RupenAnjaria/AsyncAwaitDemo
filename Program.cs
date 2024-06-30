// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using CityInfo;

await ExampleMain();

async Task ExampleMain()
{
    HttpClient httpClient = new HttpClient();
    ApiClient apiClient = new ApiClient(httpClient);

    string cityName = "SampleCity";
    string dateString = "2024-07-01";
    int preferredTemperature = 200;
    int rainPercentage = 70;
    string cityInfo = await apiClient.GetCityInfoAsync(cityName, dateString, preferredTemperature, rainPercentage);
    Console.WriteLine(cityInfo);
}
