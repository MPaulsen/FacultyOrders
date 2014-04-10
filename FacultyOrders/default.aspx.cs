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
            if (!Page.IsValid)
            {
                lblStatus.Text = "Failed Captcha";
                return;
            }

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
                        command.Parameters.Add(new SqlParameter("urgent", (chkUrgent.Checked) ? 1 : 0));
                        command.Parameters.Add(new SqlParameter("comp", (chkComp.Checked) ? 1 : 0));
                        command.Parameters.Add(new SqlParameter("Vendor", txtVendor.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Desc", txtItemDesc.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Name", txtName.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Email", txtEmail.Text.ToString()));
                        command.Parameters.Add(new SqlParameter("Amount", txtAmount.Text.ToString()));
                        command.Connection = connection;
                        command.Connection.Open();

                        command.ExecuteNonQuery();

                        command.Connection.Close();

                        string orderID = dbControls.dbQuery("SELECT TOP 1 OrderID FROM Orders ORDER BY OrderRequestDate DESC");


                        dbControls.sendMailToRole("Accountant", "New Request",
                          @"A new faculty order has been placed.
                              Order ID: " + orderID + @"
                              Name: " + txtName.Text.ToString() + @"
                              Vendor: " + txtVendor.Text.ToString() + @"
                              Amount: " + txtAmount.Text.ToString() + @"
                              Item Description: " + txtItemDesc.Text.ToString() + @"
                              Please log into the accountant view to access further order details.");


                        String message =  "Your order has been requested.\nOrder ID:" + orderID + "\n\tVendor: " + txtVendor.Text.ToString() + 
                            "\n\tAmount: " + txtAmount.Text.ToString() + "\n\tItem Description: " + txtItemDesc.Text.ToString() 
                            + "\nYou will recieve an email when it has been approved/denied, an order has been placed or canceled, or when it has arrived."
                            + "\nYou will also recieve an email if your order has been changed for any reason.";
            
                        EECSMail mailTo = new EECSMail(txtEmail.Text,  "Your order has been requested", message);
                        mailTo.sendMail();

                        txtAccountNumber.Text = "";
                        txtAmount.Text = "";
                        txtEmail.Text = "";
                        txtItemDesc.Text = "";
                        txtName.Text = "";
                        txtVendor.Text = "";
                        lblStatus.Text = "Order submitted successfully.";
                    }
                    catch { }
                    }
                
                    
                }
            }
        }
    }
