using CustomerWEBAPI.BlazorUI.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace CustomerWEBAPI.BlazorUI.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient http)
        {
               _httpClient = http;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
           //return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customer/All");
            var response = await _httpClient.GetAsync("api/Customer/All");

          
            return await response.Content.ReadFromJsonAsync<List<Customer>>();
        }
        
        public async Task<Customer?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Customer>($"api/Customer/{id}");
        
        }
        public async Task<bool> AddAsync(Customer customer)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Customer",customer);
            return res.IsSuccessStatusCode;
        
        }
        public async Task<bool> UpdateAsync(Customer customer)
        {
           var res = await _httpClient.PutAsJsonAsync($"api/Customer/{customer.cust_id}",customer);
            return res.IsSuccessStatusCode;        
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var success = await _httpClient.DeleteAsync($"api/Customer/{id}");
            return success.IsSuccessStatusCode;

        }
    }
}
