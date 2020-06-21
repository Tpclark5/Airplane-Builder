using Dapper;
using Microsoft.Extensions.Options;
using PlaneBuilder.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public class GetEmailAddress: IGetEmailAddress
    {
        private string _connectionString;

        public GetEmailAddress(IOptions<DatabaseConfig> config)
        {
            _connectionString = config.Value.ConnectionString;
        }
        public async Task<IEnumerable<LoginViewModel>> DisplayAllEmailAddresses()
        {
            const string queryString = "Select Email from [dbo].IdentityUser";

            using (var connection = new SqlConnection(_connectionString))
            {
                IEnumerable<LoginViewModel> orderDetail = await connection.QueryAsync<LoginViewModel>(queryString);

                return orderDetail;
            }
        }
    }
}
