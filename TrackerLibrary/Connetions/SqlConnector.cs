using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using System.Data.SqlClient;
using Dapper;

namespace TrackerLibrary.Connections
{
    public class SqlConnector : IDataConnection
    {
       

        /// <summary>
        /// Saves a new prize to the SQL databse
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns>The prize information, including unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@Id", 1, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                return model;

            }


        }

        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAdress", model.EmailAdress);
                p.Add("@CellPhoneNumber", model.CellPhoneNumber);
                p.Add("@Id", 1, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                return model;

            }
        }

    }
}
