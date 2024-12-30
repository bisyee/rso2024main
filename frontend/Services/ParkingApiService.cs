using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ParkingService.Models;
using System.Text.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ParkingService.Client
{
    public class ParkingApiService
    {
        private readonly HttpClient _httpClient;

        public ParkingApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all parking spots
        public async Task<List<ParkingSpot>> GetAllParkingSpotsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ParkingSpot>>("api/parkingSpot");
                return response ?? new List<ParkingSpot>();
            }
            catch
            {
                return new List<ParkingSpot>();
            }
        }

        // Get parking spot by Id
        public async Task<ParkingSpot> GetParkingSpotByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ParkingSpot>($"api/parkingSpot/{id}");
                return response;
            }
            catch
            {
                return null;
            }
        }

        // Create a new parking spot
        public async Task<ParkingSpot> CreateParkingSpotAsync(ParkingSpot spot)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/parkingSpot", spot);
                if (response.IsSuccessStatusCode)
                {
                    var createdSpot = await response.Content.ReadFromJsonAsync<ParkingSpot>();
                    return createdSpot;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        // Update an existing parking spot
        public async Task<ParkingSpot> UpdateParkingSpotAsync(int id, ParkingSpot spot)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/parkingSpot/{id}", spot);
                if (response.IsSuccessStatusCode)
                {
                    var updatedSpot = await response.Content.ReadFromJsonAsync<ParkingSpot>();
                    return updatedSpot;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
         public async Task<GeoJson> GetParkingDataAsGeoJsonAsync()
        {
            try
            {
                // Call the API endpoint
                var response = await _httpClient.GetAsync("api/ParkingSpot/parking");
                var geoJsonData = await response.Content.ReadAsStringAsync();
                string trimmedJson = geoJsonData.Trim('"');

                // Step 2: Unescape the content
                string unescapedJson = Regex.Unescape(trimmedJson);
                string fixedJson = unescapedJson.Replace("\\u0022", "\"");
                fixedJson =  fixedJson.Trim('"');
                Console.WriteLine(fixedJson);
                var geoJsonObject = JsonSerializer.Deserialize<GeoJson>(fixedJson);
                return geoJsonObject;
            }
            catch
            {
                return null;
            }
        }

        // Delete a parking spot
        public async Task<bool> DeleteParkingSpotAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/parkingSpot/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
