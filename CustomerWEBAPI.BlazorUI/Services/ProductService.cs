using CustomerWEBAPI.BlazorUI.Models;
using System.Net.Http.Json;

namespace CustomerWEBAPI.BlazorUI.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
               _httpClient = httpClient;
        }
        public async Task<List<Product>> GetAllAsync()
        {
           return await _httpClient.GetFromJsonAsync<List<Product>>("api/Product/all");
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/Product/{id}");
        
        }
        public async Task<bool> AddAsync(Product product)
        {
            var success = await _httpClient.PostAsJsonAsync("api/Product",product);
            return success.IsSuccessStatusCode;
        
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            var success = await _httpClient.PutAsJsonAsync($"api/Product/{product.prod_id}",product);
            return success.IsSuccessStatusCode;
        
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var success = await _httpClient.DeleteAsync($"api/Product/{id}");
            return success.IsSuccessStatusCode;
        
        }
    }
}
