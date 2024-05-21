using ConsultaVoos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ConsultaVoos.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Voo>> GetVoosFromApi(string apiUrl)
        {
            var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();
            
            var content =  await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Voo>>(content);
        }
    }
}