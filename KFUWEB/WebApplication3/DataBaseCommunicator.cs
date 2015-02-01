using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication3
{
    public class DataBaseCommunicator
    {


        public static List<Users> GetUsersTable(string login, string password)
        {

            List<Users> returnValue = new List<Users>();
            using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionString")))
			
            using (SqlCommand dbCommand = dbConnection.CreateCommand())
            {
                string SqlCommandText = "SELECT TOP 1 [id_users],[active],[login],[password],[fk_first_stage],[fk_second_stage],[fk_third_stage],[fk_fourth_stage],[fk_fifth_stage] FROM [KPI].[dbo].[Users]";
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = SqlCommandText;
                returnValue = ExecuteSQL<Users>(dbConnection, dbCommand);
               // if (returnValue == null) throw new Exception("Can't get data from Users table.");
            }
            return returnValue;
        }

         public static List<First_stage> GetFirst_stageTable()
        {

            List<First_stage> returnValue = new List<First_stage>();
            using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionString")))
			
            using (SqlCommand dbCommand = dbConnection.CreateCommand())
            {
                string SqlCommandText = "SELECT ALL [id_first_stage],[active],[name] FROM [KPI].[dbo].[First_stage]";
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = SqlCommandText;
                returnValue = ExecuteSQL<First_stage>(dbConnection, dbCommand);
               // if (returnValue == null) throw new Exception("Can't get data from Users table.");
            }
            return returnValue;
        }



         public static List<Second_stage> GetSecond_stageTable(int id)
         {

             List<Second_stage> returnValue = new List<Second_stage>();
             using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionString")))

             using (SqlCommand dbCommand = dbConnection.CreateCommand())
             {
                 string SqlCommandText = "SELECT ALL [id_second_stage],[active],[name],[fk_first_stage] FROM [KPI].[dbo].[Second_stage] WHERE [fk_first_stage] = "+ id.ToString();
                 dbCommand.CommandType = CommandType.Text;
                 dbCommand.CommandText = SqlCommandText;
                 returnValue = ExecuteSQL<Second_stage>(dbConnection, dbCommand);
                 // if (returnValue == null) throw new Exception("Can't get data from Users table.");
             }
             return returnValue;
         }


         public static List<Third_stage> GetThird_stageTable(int id)
         {

             List<Third_stage> returnValue = new List<Third_stage>();
             using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionString")))

             using (SqlCommand dbCommand = dbConnection.CreateCommand())
             {
                 string SqlCommandText = "SELECT [id_third_stage],[active],[name] ,[fk_second_stage] FROM [dbo].[Third_stage] WHERE fk_second_stage = " + id.ToString();
                 dbCommand.CommandType = CommandType.Text;
                 dbCommand.CommandText = SqlCommandText;
                 returnValue = ExecuteSQL<Third_stage>(dbConnection, dbCommand);
                 // if (returnValue == null) throw new Exception("Can't get data from Users table.");
             }
             return returnValue;
         }
        
        
        
        
        protected static List<T> ExecuteSQL<T>(SqlConnection connection, SqlCommand cmd)
        {
            var result = new List<T>();
            cmd.Connection = connection;
            Type type = typeof(T);
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                if (constructor != null)
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object obj = constructor.Invoke(null);
                            int cnt = reader.FieldCount;
                            for (int i = 0; i < cnt; i++)
                            {
                                string name = reader.GetName(i);
                                object val = reader[i];
                                if (val is DBNull)
                                {
                                    val = null;
                                }
                                var property = type.GetProperty(name,
                                                                BindingFlags.IgnoreCase | BindingFlags.Instance |
                                                                BindingFlags.Public);
                                if (property != null)
                                {
                                    property.SetValue(obj, val, null);
                                }
                            }
                            result.Add((T)obj);
                        }
                        reader.Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                //LogHandler.LogWriter.WriteError(sqlEx);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }


    }
}