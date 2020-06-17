using PlaneBuilder.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public class AirplaneClient : IAirplaneClient
    {
        private readonly HttpClient _client;

        public AirplaneClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<Airplanes> Airplanes()
        {
            try
            {
                var endpoint = $"airplanes?access_key=8fef6104a20a6eeaf00499c011705ef9";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Airplanes>(json);

            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}