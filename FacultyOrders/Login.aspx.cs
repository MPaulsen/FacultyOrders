using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace FacultyOrders
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_Login_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
            
            connection.Open();
            
            string checkUser = "SELECT COUNT(*) FROM Users where Username ='" + txtUser.Text.ToString() + "'";
            SqlCommand com = new SqlCommand(checkUser, connection);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            connection.Close();

            //Used to check what the password is for the username that was slected. 
            if (temp == 1)
            {
                Encryption crypto = new Encryption();

                string password = dbQuery("SELECT Password FROM Users WHERE Username = '" + txtUser.Text.ToString() + "'");

                if (crypto.Decrypt(password) == txtPass.Text.ToString())
                {
                    string user = txtUser.Text.ToString();
                    Session["User"] = dbQuery("SELECT FirstName FROM Users WHERE Username = '" + user + "'");
                    Session["Role"] = dbQuery("SELECT Role FROM Users WHERE Username = '" + user + "'");
                    Response.Write("Password is correct");
                    String role = Session["Role"].ToString();
                    if(role.Equals("PurchaserComp") || role.Equals ("PurchaserOther"))
                        Response.Redirect("/Purchase.aspx"); 
                    else if(role.Equals("Accountant"))
                        Response.Redirect("Accounting.aspx"); 
                    else
                        Response.Redirect("Register.aspx"); 
                    
                    
                    /*
                     * See if role is Accountant/PurchaserComp/PurchaserOther and redirect to relevant view
                     */
                }
                else
                {
                    Response.Write("Password is not correct");
                }
            }
            else
            {
                Response.Write("Please provid a valid Username");
            }

        }
    }
}