using SimpleApi.Models.Models;
using SimpleApi.Repositories.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Repositories.Operation
{
    public class OperationRepository : IOperationRepository
    {
        private BaseReppository reppository;
        public OperationRepository(BaseReppository baseReppository)
        {
            reppository = baseReppository;
        }

        public RegistrationModel Get(string username, string SecondName)
        {
            using (SqlConnection connection = new SqlConnection(reppository.conString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserForLoginPass", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "Login", Value = username, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Password", Value = SecondName, SqlDbType = SqlDbType.NVarChar });
                SqlDataReader reader = command.ExecuteReader();
                RegistrationModel login = new RegistrationModel();
                if (reader.Read())
                {
                    login.Id = (int)reader["Id"];
                    login.FirtName = (string)reader["FirstName"];
                    login.SecondName = (string)reader["SecondName"];
                    login.Age = (int)reader["Age"];
                    login.EMail = (string)reader["EMail"];
                    login.PhoneNumber = (string)reader["PhoneNumber"];
                    login.Login = (string)reader["Login"];
                    login.Password = (string)reader["Password"];
                }
                return (login);
            }

        }
    }
}
