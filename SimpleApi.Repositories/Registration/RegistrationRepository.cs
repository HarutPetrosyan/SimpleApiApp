using SimpleApi.Models.Models;
using SimpleApi.Repositories.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Repositories.Registration
{
    public  class RegistrationRepository:IRegistrationRepository
    {
        public readonly BaseReppository _reppository;
        public RegistrationRepository(BaseReppository reppository)
        {
            _reppository = reppository;
        }
        public void AddUser(RegistrationModel user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_reppository.conString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("AddUserProcedure", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "FirstName", Value = user.FirtName, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "SecondName", Value = user.SecondName, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Age", Value = user.Age, SqlDbType = SqlDbType.Int });
                command.Parameters.Add(new SqlParameter() { ParameterName = "EMail", Value = user.EMail, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "PhoneNumber", Value = user.PhoneNumber, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Login", Value = user.Login, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Password", Value = user.Password, SqlDbType = SqlDbType.NVarChar });
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void UpdateUser(RegistrationModel user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_reppository.conString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("UpdateUser", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "Id", Value = user.Id, SqlDbType = SqlDbType.Int });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Name", Value = user.FirtName, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Surname", Value = user.SecondName, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Age", Value = user.Age, SqlDbType = SqlDbType.Int });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Email", Value = user.EMail, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName="PhoneNumber",Value=user.PhoneNumber,SqlDbType=SqlDbType.NVarChar});
                command.Parameters.Add(new SqlParameter() { ParameterName = "UserName", Value = user.Login, SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter() { ParameterName = "Password", Value = user.Password, SqlDbType = SqlDbType.NVarChar });
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_reppository.conString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("DeletUserFromUser", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "Id", Value = id, SqlDbType = SqlDbType.Int });
                command.ExecuteNonQuery();
            }
        }
    }
}
