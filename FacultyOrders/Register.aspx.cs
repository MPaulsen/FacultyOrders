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
            else if (!(Session["Role"].ToString().Equals("Admin")))
                Response.Redirect("/default.aspx", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Encryption crypto = new Encryption();
            String role = "";
            switch (rdoRole.SelectedIndex)
            {
                case 0:
                    role = "PurchaserComp";
                    break;
                case 1:
                    role = "PurchaserOther";
                    break;
                case 2:
                    role = "Accountant";
                    break;
                case 3:
                    role = "Admin";
                    break;
            }
            nonQuery(@"INSERT INTO Users 
                        VALUES('" + txtUser.Text.ToString() + "', '" + crypto.Encrypt(txtPass.Text.ToString()) + "', '" + txtFirst.Text.ToString() + @"',
                                '" + txtLast.Text.ToString() + "', '" + txtEmail.Text.ToString() + "', '" + role + "')");
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
            rdoRole.SelectedIndex = 0;
            txtUser.Text = "";
        }
    }
}