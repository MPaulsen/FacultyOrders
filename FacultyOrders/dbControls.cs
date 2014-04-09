using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacultyOrders
{
    public static class dbControls
    {
        public static void nonQuery(String qS)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Parameters.Clear();
                    try
                    {
                        command.CommandText = qS;
                        command.Connection = connection;
                        command.Connection.Open();

                        command.ExecuteNonQuery();

                        command.Connection.Close();

                    }
                    catch
                    {
                        command.Connection.Close();
                    }
                }
            }
        }
        public static String dbQuery(String qS)
        {
            object result = "";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Parameters.Clear();
                    try
                    {
                        command.CommandText = qS;
                        command.Connection = connection;
                        command.Connection.Open();

                        result = command.ExecuteScalar();

                        command.Connection.Close();

                    }
                    catch (Exception excep)
                    {
                        String strExcep = excep.ToString();
                        command.Connection.Close();
                    }
                }
            }
            if (result == null)
                return "";
            else
                return result.ToString();
        }
    }
}