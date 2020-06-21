using PlaneBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    
    public class AirportCodeClient : IAirportCodeClient
    {
        private readonly HttpClient _client;

        public AirportCodeClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<AirportCode> GetAirportCode()
        {
            try
            {
                var endpoint = $"airports?access_key=f415a6133de905c91e276fc3ef678ede&country_name=United States";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AirportCode>(json);

            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
