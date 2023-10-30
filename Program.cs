using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string apiUrl = "https://api.tfl.gov.uk/StopPoint/490000129R/Arrivals";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var data = JsonSerializer.Deserialize<List<ArrivalModel>>(apiResponse);

                    foreach (var arrival in data)
                    {
                        Console.WriteLine($"Destination: {arrival.destinationName}, Due: {arrival.timeToStation/60} mins");
                    }
                }
                else
                {
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

