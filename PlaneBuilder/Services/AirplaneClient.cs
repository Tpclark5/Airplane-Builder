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

        public async Task<Airplanes> FindAPlane(AirplaneDBO model)
        {
            try
            {
                var endpoint = $"/airplanes?access_key=8fef6104a20a6eeaf00499c011705ef9&limit=5000&engines_type={model.Engine_Type}&engines_count={model.Engine_Count}&plane_age={model.Age}";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Airplanes>(json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Departure> FindAnAirport(Airplanes model)
        {
            var PlaneIATAProps = new List<AirplaneProperties>();
            try
            {

                PlaneIATAProps = model.data
                .Select(PlaneDBO => new AirplaneProperties { airline_iata_code = PlaneDBO.airline_iata_code, iata_code_short = PlaneDBO.iata_code_short })
                .ToList();

                var iataCodeStorage = PlaneIATAProps.FirstOrDefault();
                var endpoint = $"/flights?access_key=8fef6104a20a6eeaf00499c011705ef9&limit=5&iata={(iataCodeStorage.airline_iata_code + iataCodeStorage.iata_code_short)}&flight_status=scheduled";
                var response = await _client.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();
                var simpleResponse = JsonSerializer.Deserialize<APISimpleResponse>(json);
                var departureData = simpleResponse.Departures.FirstOrDefault();
                return departureData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}