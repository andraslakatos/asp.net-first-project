using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Data;

namespace Desktop.Model
{
    public class FoodOrderApiService
    {
        private readonly HttpClient _httpClient;

        public FoodOrderApiService(string baseAddress)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
            
        }

        public async Task<IEnumerable<OrderDTO>> LoadOrdersAsync()
        {
            var response = await _httpClient.GetAsync("api/Orders/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<OrderDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<IEnumerable<CategoryDTO>> LoadCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("api/Categories/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CategoryDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<IEnumerable<CartItemDTO>> LoadCartItemsAsync(int orderId)
        {
            var response = await _httpClient.GetAsync($"api/Items/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CartItemDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task UpdateOrderAsync(OrderDTO orderDto)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Orders/{orderDto.Id}", orderDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task CreateItemAsync(ItemDTO itemDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Items/", itemDto);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<bool> LoginAsync(string name, string password)
        {
            LoginDTO user = new LoginDTO
            {
                UserName = name,
                Password = password
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _httpClient.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response);
        }

    }
}
