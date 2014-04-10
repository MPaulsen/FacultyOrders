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
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("default.aspx");
            if (!IsPostBack)
                loadForm();
        }

        protected void loadForm()
        {
            txtUserID.Text = Session["UserID"].ToString();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Parameters.Clear();
                    try
                    {
                        command.CommandText = @"SELECT Username, FirstName, LastName, Email, Role
                                                FROM Users WHERE UserID = '" + txtUserID.Text.ToString() + "'";
                        command.Connection = connection;
                        command.Connection.Open();

                        using (SqlDataReader result = command.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                txtUserName.Text = result[0].ToString();
                                txtFirst.Text = result[1].ToString();
                                txtLast.Text = result[2].ToString();
                                txtEmail.Text = result[3].ToString();
                                ddlRole.SelectedItem.Value = ddlRole.Items.FindByText(result[4].ToString()).Value;
                            }
                            command.Connection.Close();
                        }
                    }
                    catch (Exception excep)
                    {
                        String strExcep = excep.ToString();
                        command.Connection.Close();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Encryption crypto = new Encryption();
            if (chkPass.Checked)
                dbControls.nonQuery(@"
                    UPDATE Users
                    SET Username = '" + txtUserName.Text.ToString() + @"', 
                        Password = '" + crypto.Encrypt(txtPass.Text.ToString()) + @"',
                        FirstName = '" + txtFirst.Text.ToString() + @"',
                        LastName = '" + txtLast.Text.ToString() + @"', 
                        Email = '" + txtEmail.Text.ToString() + @"', 
                        Role = '" + ddlRole.SelectedItem.Text.ToString() + @"'
                    WHERE UserID = '" + txtUserID.Text.ToString() + "'");
            else
                dbControls.nonQuery(@"
                    UPDATE Users
                    SET Username = '" + txtUserName.Text.ToString() + @"', 
                        FirstName = '" + txtFirst.Text.ToString() + @"',
                        LastName = '" + txtLast.Text.ToString() + @"', 
                        Email = '" + txtEmail.Text.ToString() + @"', 
                        Role = '" + ddlRole.SelectedItem.Text.ToString() + @"' 
                    WHERE UserID = '" + txtUserID.Text.ToString() + "'");

            lblStatus.Text = "User updated successfully!";
        }


    }
}