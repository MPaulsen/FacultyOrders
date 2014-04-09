using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacultyOrders
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null)
                Response.Redirect("/login.aspx", true);
            else if (!(Session["Role"].ToString().Equals("Accountant")))
                Response.Redirect("/default.aspx", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Encryption crypto = new Encryption();
            nonQuery(@"INSERT INTO Users 
                        VALUES('" + txtUser.Text.ToString() + "', '" + crypto.Encrypt(txtPass.Text.ToString()) + "', '" + txtFirst.Text.ToString() + @"',
                                '" + txtLast.Text.ToString() + "', '" + txtEmail.Text.ToString() + "', '" + txtRole.Text.ToString() + "')");
            lblStatus.Text = "User successfully registered";
            clearForm();
        }

        protected void nonQuery(String qS)
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

        private void clearForm()
        {
            txtEmail.Text = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtPass.Text = "";
            txtRole.Text = "";
            txtUser.Text = "";
        }
    }
}