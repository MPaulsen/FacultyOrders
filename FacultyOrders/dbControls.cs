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


        public static SqlDataReader getReader(String qS)
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
                        SqlDataReader res = command.ExecuteReader();
                        return res;

                    }
                    catch (Exception excep)
                    {
                        String strExcep = excep.ToString();
                        command.Connection.Close();
                        return null;
                    }
                }
            }

        }

        public static void  sendMailToRole(String role, String subject, String message)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Parameters.Clear();
                    command.CommandText = ("SELECT email FROM USERS WHERE Role = '" + role + "'");
                    command.Connection = connection;
                    command.Connection.Open();
                    string orderID = dbControls.dbQuery("SELECT TOP 1 OrderID FROM Orders ORDER BY OrderRequestDate DESC");

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                         EECSMail  mail= new EECSMail(dr[0].ToString(), subject ,message);
                         mail.sendMail();
                    }
                }
            }
        }

        public static void Approve(string id)
        {
            string isComp = dbQuery("Select ComputerPurchase from Orders where orderID = " + id);
            string eMail = dbQuery("Select RequestorEmail from Orders where OrderID = " + id);
            sendMailToRole((isComp == "True"?"PurchaserComp":"PurchaserOther"), "An order has been approved", "A new order: " + id + " has been approved.");
            EECSMail mail = new EECSMail(eMail, "Your order has been approved!", "Your order has been approved!\nFor reference, your order ID is" + id + ".\n"
                + "You will recieve a mail when your order has been purchased or edited.");
            mail.sendMail();
        }

        public static void Delete(string id)
        {
            string eMail = dbQuery("Select RequestorEmail from Orders where OrderID = " + id);
            EECSMail mail = new EECSMail(eMail, "Your order has been deleted!!", "Unfortunately, your order (order#" + id
                + ") has been deleted.");
            mail.sendMail();
        }

        public static void Place(string id)
        {
            string eMail = dbQuery("Select RequestorEmail from Orders where OrderID = " + id);
            EECSMail mail = new EECSMail(eMail, "Your order has been placed!", "Your order (order#" + id
                + ") has just been placed.\nAnother email should come shortly with any edits.");
            mail.sendMail();
        }

        public static void Cancel(string id)
        {
            string eMail = dbQuery("Select RequestorEmail from Orders where OrderID = " + id);
            EECSMail mail = new EECSMail(eMail, "Your order has been canceled!!", "Unfortunately, your order (order#" + id
                + ") has been canceled.\nAnother email may shortly come explaining any changes, along with a new order being placed.");
            mail.sendMail();
        }
    }
}