using Microsoft.Extensions.Options;
using PlaneBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public class TSAClient : ITSAClient
    {
        private readonly HttpClient _client;
        private readonly string _tsaClient;

        public TSAClient(HttpClient httpClient, IOptions<APISecretConfig> tsaclient)
        {
            _client = httpClient;
            _tsaClient = tsaclient.Value.TSAKey;
        }

        public async Task<TSAWaitTime> GetAirport(string airport)
        {
            try
            {
                var endpoint = $"{airport}/JSON/";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TSAWaitTime>(json);

            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
