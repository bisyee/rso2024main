using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ParkingService.Models;
using System.Text.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Text.RegularExpressions;
using System;

namespace ParkingService.Client
{
    public class ParkingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

        public ParkingApiService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ParkingService");

            _retryPolicy = Policy.Handle<HttpRequestException>()
                                 .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            _circuitBreakerPolicy = Policy.Handle<HttpRequestException>()
                                          .CircuitBreakerAsync(3, TimeSpan.FromSeconds(10));
        }

        public async Task<List<ParkingSpot>> GetAllParkingSpotsAsync()
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _circuitBreakerPolicy.ExecuteAsync(() =>
                        _httpClient.GetFromJsonAsync<List<ParkingSpot>>("api/parkingSpot")
                    )
                );

                return response ?? new List<ParkingSpot>();
            }
            catch (BrokenCircuitException)
            {
                return new List<ParkingSpot>();
            }
            catch
            {
                return new List<ParkingSpot>();
            }
        }

        public async Task<ParkingSpot> GetParkingSpotByLocationAsync(string loc)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _circuitBreakerPolicy.ExecuteAsync(() =>
                        _httpClient.GetFromJsonAsync<ParkingSpot>($"api/parkingSpot/{loc}")
                    )
                );

                return response;
            }
            catch (BrokenCircuitException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ParkingSpot> CreateParkingSpotAsync(ParkingSpot spot)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _circuitBreakerPolicy.ExecuteAsync(() =>
                        _httpClient.PostAsJsonAsync("api/parkingSpot", spot)
                    )
                );

                if (response.IsSuccessStatusCode)
                {
                    var createdSpot = await response.Content.ReadFromJsonAsync<ParkingSpot>();
                    return createdSpot;
                }
                return null;
            }
            catch (BrokenCircuitException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ParkingSpot> UpdateParkingSpotAsync(int id, ParkingSpot spot)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _circuitBreakerPolicy.ExecuteAsync(() =>
                        _httpClient.PutAsJsonAsync($"api/parkingSpot/{id}", spot)
                    )
                );

                if (response.IsSuccessStatusCode)
                {
                    var updatedSpot = await response.Content.ReadFromJsonAsync<ParkingSpot>();
                    return updatedSpot;
                }
                return null;
            }
            catch (BrokenCircuitException)
            {
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
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _circuitBreakerPolicy.ExecuteAsync(() =>
                        _httpClient.GetAsync("api/ParkingSpot/parking")
                    )
                );

                var geoJsonData = await response.Content.ReadAsStringAsync();
                string trimmedJson = geoJsonData.Trim('"');
                string unescapedJson = Regex.Unescape(trimmedJson);
                string fixedJson = unescapedJson.Replace("\\u0022", "\"");
                fixedJson = fixedJson.Trim('"');
                var geoJsonObject = JsonSerializer.Deserialize<GeoJson>(fixedJson);
                return geoJsonObject;
            }
            catch (BrokenCircuitException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteParkingSpotAsync(int id)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() =>
                    _circuitBreakerPolicy.ExecuteAsync(() =>
                        _httpClient.DeleteAsync($"api/parkingSpot/{id}")
                    )
                );

                return response.IsSuccessStatusCode;
            }
            catch (BrokenCircuitException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
