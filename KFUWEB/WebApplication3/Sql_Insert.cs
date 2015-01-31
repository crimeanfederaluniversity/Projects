using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Sql_Insert
    {
        private SqlConnection connect = null;

        public void OpenConnection(string connectionString)
        {
            connect = new SqlConnection(connectionString);
            connect.Open();
        }

        public void CloseConnection()
        {
            connect.Close();
        }

        public void InsertAuto(int active, string login, string password, int fk_first_stage, int fk_second_stage, int fk_third_stage)
        {
            // Оператор SQL
            string sql = string.Format("Insert Into Users" +
                   "(active, login, password, fk_first_stage, fk_second_stage, fk_third_stage) Values('{0}','{1}','{2}','{3}' ,'{4}','{5}','{6})", active, login, password, fk_first_stage, fk_second_stage, fk_third_stage);
            using (SqlCommand cmd = new SqlCommand(sql,this.connect))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}