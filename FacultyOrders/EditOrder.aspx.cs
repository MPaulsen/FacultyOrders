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
    
    public partial class EditOrder : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["OrderID"] == null)
                Response.Redirect("default.aspx");
                loadForm();
        }

        protected void loadForm()
        {
            txtOrderID.Text = Session["OrderID"].ToString();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Parameters.Clear();
                    try
                    {
                        command.CommandText = @"SELECT Requestor, RequestorEmail, AccountNumber, URGENT, ComputerPurchase, Vendor, ItemDesc, 
                                                    PostOrderNotes, PO_Number, Amount
                                                FROM Orders WHERE OrderID = '" + txtOrderID.Text.ToString() + "'";
                        command.Connection = connection;
                        command.Connection.Open();

                        using (SqlDataReader result = command.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                txtName.Text = result[0].ToString();
                                txtEmail.Text = result[1].ToString();
                                txtAccountNumber.Text = result[2].ToString();
                                chkUrgent.Checked = (result[3].ToString() == "True");
                                
                                chkComp.Checked = (result[4].ToString() == "True");
                                Session["wasChecked"] = (result[4].ToString() == "True");
                                
                                txtVendor.Text = result[5].ToString();
                                txtItemDesc.Text = result[6].ToString();
                                txtPONotes.Text = result[7].ToString();
                                txtPONumber.Text = result[8].ToString();
                                txtAmount.Text = result[9].ToString();
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
            dbControls.nonQuery(@"
            UPDATE Orders
            SET Requestor = '" + txtName.Text.ToString() + @"', 
                RequestorEmail = '" + txtEmail.Text.ToString() + @"',
                AccountNumber = '" + txtAccountNumber.Text.ToString() + @"',
                URGENT = '" + ((chkUrgent.Checked == true) ? "1": "0") + @"', 
                ComputerPurchase = '" + ((chkComp.Checked == true) ? "1" : "0") + @"', 
                Vendor = '" + txtVendor.Text.ToString() + @"', 
                ItemDesc = '" + txtItemDesc.Text.ToString() + @"', 
                PostOrderNotes = '" + txtPONotes.Text.ToString() + @"', 
                PO_Number = '" + txtPONumber.Text.ToString() + @"',
                Amount = '" + txtAmount.Text.ToString() + @"' 
            WHERE OrderID = '" + txtOrderID.Text.ToString() + "'");

            EECSMail mail = new EECSMail(txtEmail.Text, "Your order has been changed!", "Your order has been changed!\nHere is the new order #"+ txtOrderID.Text + ".\n"
                + "Order ID: " + txtOrderID.Text + 
                              "\nName: " + txtName.Text.ToString() +
                              "\nVendor: " + txtVendor.Text.ToString() +
                              "\nAmount: $" + txtAmount.Text.ToString() +
                              "\nAccountNum: " + txtAccountNumber.Text +
                              "\nItem Description: " + txtItemDesc.Text.ToString() +
                              "\nOrder notes: " + txtPONotes.Text 
                + "\nYou will recieve another mail when your order has been updated.");
            mail.sendMail();

            if (Session["wasChecked"].ToString() != chkComp.Checked.ToString() && dbControls.dbQuery("Select ApprovalDate from Orders Where orderid = " + txtOrderID.Text)!=null)
            {
                dbControls.sendMailToRole((chkComp.Checked ? "PurchaserComp" : "PurchaserOther"), "An order has been transferred", "Order: " + txtOrderID.Text + " has been transferred under your control.");
            }
                
        }
    }
}