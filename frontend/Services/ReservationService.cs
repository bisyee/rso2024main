using ParkingService.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ParkingService.Client
{
    public class ReservationApiService
    {
        private readonly HttpClient _httpClient;

        public ReservationApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Reservation>>("api/reservation");
                return response ?? new List<Reservation>();
            }
            catch
            {
                return new List<Reservation>();
            }
        }
        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Reservation>($"api/reservation/{id}");
                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/reservation", reservation);
                if (response.IsSuccessStatusCode)
                {
                    var createdReservation = await response.Content.ReadFromJsonAsync<Reservation>();
                    return createdReservation;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Reservation> UpdateReservationAsync(int id, Reservation reservation)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/reservation/{id}", reservation);
                if (response.IsSuccessStatusCode)
                {
                    var updatedReservation = await response.Content.ReadFromJsonAsync<Reservation>();
                    return updatedReservation;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/reservation/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Reservation>>($"api/reservation/user/{userId}");
                return response ?? new List<Reservation>();
            }
            catch
            {
                return new List<Reservation>();
            }
        }
    }
}
