using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ParkingService.Models;

namespace ParkingService.Client
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient )
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/User");
        }

        public async Task<User> GetUserByIdAsync(string email)
        {
            try
            {
                
                var response = await _httpClient.GetAsync($"api/User/{email}");

           
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    // Handle other status codes as needed
                    throw new Exception("An error occurred while fetching the user.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<User> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User", user);
            Console.WriteLine(response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/user/{user.id}", user);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/user/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
