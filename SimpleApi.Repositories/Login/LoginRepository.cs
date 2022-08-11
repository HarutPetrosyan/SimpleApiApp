using SimpleApi.Models.Models;
using SimpleApi.Repositories.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Repositories.Login
{
    public  class LoginRepository : ILoginRepository
    {
        private readonly BaseReppository baseRepo;

        public LoginRepository(BaseReppository _BaseRepo)
        {
            baseRepo = _BaseRepo;
        }
        public LoginModel Get(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(baseRepo.conString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserForLoginPass", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "Login", Value = username, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Password", Value = password, SqlDbType = SqlDbType.NVarChar });
               SqlDataReader reader= command.ExecuteReader();
                LoginModel login=new LoginModel();
                if(reader.Read())
                {
                    login.Login = (string)reader["Login"];
                    login.Password = (string)reader["Password"];
                }
                return (login);
            }
        }
    }
}
