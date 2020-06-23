using Microsoft.Extensions.Options;
using PlaneBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static PlaneBuilder.Models.Airplanes;

namespace PlaneBuilder.Services
{
    public class AirplaneClient : IAirplaneClient
    {
        private readonly HttpClient _client;
        private readonly string _aviationStackKey;

        public AirplaneClient(HttpClient httpClient, IOptions<APISecretConfig> secretConfig)
        {
            _client = httpClient;
            _aviationStackKey = secretConfig.Value.AviationStackKey;
        }

        public async Task<Airplanes> Airplanes()
        {
            try
            {
                var endpoint = $"airplanes?access_key={_aviationStackKey}";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Airplanes>(json);

            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Airplanes> FindAPlane(AirplaneDBO model)
        {
            try
            {
                var endpoint = $"airplanes?access_key={_aviationStackKey}&limit=5&engines_type={model.Engine_Type}&engines_count={model.Engine_Count}&plane_age={model.Age}";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Airplanes>(json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<APISimpleResponse> FindAnAirport(List<AirplaneProperties> model)
        {
            var departureData = new APISimpleResponse();
            var DepartureList = new List<Departure>();
            var FlightsList = new List<Flight>();

            try
            {
                foreach (var plane in model)
                {
                    var endpoint = $"flights?access_key={_aviationStackKey}&limit=5&iata={(plane.airline_iata_code + plane.iata_code_short)}&flight_status=scheduled&country_name=United States";
                    var response = await _client.GetAsync(endpoint); // What do we do if there is no flight for the cirteria? Whatdo we do about iata call retruning whatever it wants?
                    var json = await response.Content.ReadAsStringAsync();
                    var simpleResponse = JsonSerializer.Deserialize<OverarchingAPIModel>(json);
                    var overarchingList = simpleResponse.data.ToList();
                    foreach (var data in overarchingList)
                    {
                        DepartureList.Add(data.departure);
                        FlightsList.Add(data.flight);
                    }
                    
                }
                departureData.Departures = DepartureList;
                departureData.Flights = FlightsList;
                return departureData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}