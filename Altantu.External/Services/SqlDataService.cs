using Altantu.Core.Error;
using Altantu.Core.Model;
using Altantu.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Altantu.Infrastructure.Service
{
    public class SqlDataService : IIntFunctionService
    {
        #region Constants

        private const string CONNECTION_STRING_FORMAT = "Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connect Timeout={2}";

        #endregion

        #region Methods

        public int GetInt(string server, string database, int timeOutSeconds, List<string> parameters)
        {
            int? result = null;

            try
            {
                string query = parameters[0];
                timeOutSeconds = 10; //timeOutSeconds > 0 ? timeOutSeconds : Configuration.DEFAULT_TIMEOUT_SECONDS;
                string connectionString = String.Format(CONNECTION_STRING_FORMAT, server, database, timeOutSeconds);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                throw new ManagedException("Error reading database.");
            }
        }

        #endregion
    }
}
