using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacultyOrders
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            if (txtAmount.Text.ToString() == "")
            {
                lblStatus.Text = "Please enter an amount.";
                return;
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Parameters.Clear();
                    try
                    {
                        command.CommandText = "INSERT INTO Orders Values(GETDATE(), NULL, NULL, NULL, @account, @urgent, @Comp, @Vendor, @Desc, NULL, @Name, @Email, @Amount, NULL, NULL, NULL, NULL, NULL)";
                        command.Parameters.Add(new SqlParameter("account", txtAccountNumber.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("urgent", (chkUrgent.Checked)?1:0));
                        command.Parameters.Add(new SqlParameter("comp", (chkComp.Checked)?1:0));
                        command.Parameters.Add(new SqlParameter("Vendor", txtVendor.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Desc",  txtItemDesc.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Name", txtName.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Email", txtEmail.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Amount", txtAmount.Text.ToString()));
                        command.Connection = connection;
                        command.Connection.Open();

                        command.ExecuteNonQuery();

                        command.Connection.Close();

                        lblStatus.Text = "Order submitted successfully.";
                        string orderID = dbControls.dbQuery("SELECT TOP 1 OrderID FROM Orders ORDER BY OrderRequestDate DESC");

                        EECSMail mailbox = new EECSMail("taeiantwist@gmail.com", "New Faculty Order",
                            @"A new faculty order has been placed.
                              Order ID: " + orderID + @"\n
                              Name: " + txtName.Text.ToString() + @"\n
                              Vendor: " + txtVendor.Text.ToString() + @"\n
                              Ammount: " + txtAmount.Text.ToString() + @"\n
                              Item Description: " + txtItemDesc.Text.ToString() + @"\n\n
                              Please log into the accountant view to access further order details.");
                        mailbox.sendMail();

                        EECSMail mailbox2 = new EECSMail("taeiantwist@gmail.com", "New Faculty Order",
                            @"Your faculty order has been placed.
                              Order ID: " + orderID + @"\n
                              Name: " + txtName.Text.ToString() + @"\n
                              Vendor: " + txtVendor.Text.ToString() + @"\n
                              Ammount: " + txtAmount.Text.ToString() + @"\n
                              Item Description: " + txtItemDesc.Text.ToString());
                        mailbox2.sendMail();

                        txtAccountNumber.Text = "";
                        txtAmount.Text = "";
                        txtEmail.Text = "";
                        txtItemDesc.Text = "";
                        txtName.Text = "";
                        txtVendor.Text = "";

                    }
                    catch (Exception excep)
                    {
                        String strExcep = excep.ToString();
                        command.Connection.Close();
                    }
                }
            }
        }
    }
}