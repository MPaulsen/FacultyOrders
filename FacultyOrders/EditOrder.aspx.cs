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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrderID"] == null)
                Response.Redirect("default.aspx");
            if (!IsPostBack)
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
                                chkUrgent.Checked = (result[3].ToString() == "1") ? true : false;
                                chkComp.Checked = (result[4].ToString() == "1") ? true : false;
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

            lblStatus.Text = "Order updated successfully";
        }
    }
}