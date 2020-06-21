using Dapper;
using Microsoft.Extensions.Options;
using PlaneBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public class AirplaneRepository : IAirplaneRepository
    {

        private string _connectionString;

        public AirplaneRepository(IOptions<DatabaseConfig> config)
        {
            _connectionString = config.Value.ConnectionString;
        }


     

        public async Task<bool> AddAirplane(AirplaneDBO dboAirplane)
        {
            var queryString = @$"INSERT INTO DreamPlanes (Name, Iata_Code, Description, Email_Address, Engine_Count, Age, Engine_Type, Plane_Status, Have_Ridden, Rating, Picture, Does_Exist) 
                                VALUES(@{nameof(AirplaneDBO.Name)}, @{nameof(AirplaneDBO.Iata_Code)}, @{nameof(AirplaneDBO.Description)}, @{nameof(AirplaneDBO.Email_Address)}, @{nameof(AirplaneDBO.Engine_Count)},@{nameof(AirplaneDBO.Age)}, @{nameof(AirplaneDBO.Engine_Type)},@{nameof(AirplaneDBO.Plane_Status)}, @{nameof(AirplaneDBO.Have_Ridden)},@{nameof(AirplaneDBO.Rating)},@{nameof(AirplaneDBO.Picture)},@{nameof(AirplaneDBO.Does_Exist)})";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var orderDetail = await connection.ExecuteAsync(queryString, dboAirplane);
                    return true;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }


        public async Task<bool> DeleteSelectedPlane(int planeID)
        {
            var query = @"DELETE FROM DreamPlanes WHERE PlaneID = @PlaneID;";


            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var orderDetail = await connection.ExecuteAsync(query, new { planeID });
                    return true;
                }
                catch
                {

                    return false;
                }
            }
        }


        public async Task<IEnumerable<AirplaneDBO>> DisplayAllPlanes()
        {
            const string queryString = "Select * from [dbo].DreamPlanes";

            using (var connection = new SqlConnection(_connectionString))
            {
                IEnumerable<AirplaneDBO> orderDetail = await connection.QueryAsync<AirplaneDBO>(queryString);

                return orderDetail;
            }
        }

        public async Task<AirplaneDBO> SelectOnePlane(int planeId)
        {
            var query = @"Select * From DreamPlanes
                         WHERE PlaneID = @PlaneID";

            using (var connection = new SqlConnection(_connectionString))
            {
                var orderDetail = (await connection.QueryAsync<AirplaneDBO>(query, new { planeId })).FirstOrDefault();

                return orderDetail;
            }
        }

        public async Task<bool> UpdateSelectedPlane(AirplaneDBO model)
        {
            var queryString = @$"UPDATE DreamPlanes
                                SET 
                                    Name = @Name,
	                                 Iata_Code= @Iata_Code, 
	                                 Engine_Count=	@Engine_Count,
                                     Engine_Type= @Engine_Type,
                                    Description = @Description,
                                    Age = @Age,
                                    Does_Exist = @Does_Exist,
                                    Have_Ridden = @Have_Ridden,
                                    Rating = @Rating,
                                    Email_Address = @Email_Address,
                                    Plane_Status = @Plane_Status,
                                    Picture = @Picture
                                WHERE PlaneID = @PlaneID";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var orderDetail = await connection.ExecuteAsync(queryString, model);
                    return true;
                }
                catch (Exception exception)
                {
                    var e = exception;
                    return false;
                }
            }
        }
    }
}
