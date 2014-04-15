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
    public partial class Purchase : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        int numRows;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null)
                Response.Redirect("/login.aspx", true);
            else if (!(Session["Role"].ToString().Equals("PurchaserComp") || Session["Role"].ToString().Equals("PurchaserOther")))
                Response.Redirect("/default.aspx", true);
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            

            if (ViewState["sortDirection"] == null)
                ViewState.Add("sortDirection", "ASC");
            if (ViewState["sortExpression"] == null)
            {
                ViewState.Add("sortExpression", "Urgent");
            }

            loadGrid();
            sortGrid();
            checkPlacedOrder();

        }

        private void checkPlacedOrder()
        {
            int i = 0;
            for (i = 0; i < numRows; i++)
            {
                if(grdOrders.Rows[i].Cells[12].Text != "&nbsp;")
                {
                    Button btn = grdOrders.Rows[i].Cells[17].FindControl("btnPlaceOrder") as Button;
                    btn.Text = "Cancel Order";                
                }
                else
                    grdOrders.Rows[i].Cells[18].Enabled = false;
                if (grdOrders.Rows[i].Cells[14].Text != "&nbsp;")
                {
                    Button btna = grdOrders.Rows[i].Cells[17].FindControl("btnPlaceOrder") as Button;
                    btna.Text = "Order Recieved";
                    grdOrders.Rows[i].Cells[17].Enabled = false;
                    grdOrders.Rows[i].Cells[18].Enabled = false;
                }
                    
            }
        }

        private void loadGrid()
        {
            int viewIsComputer = 0;
            if (Session["Role"] == null)
                Response.Redirect("/Login.aspx", true);
            else
            {
                String role = Session["Role"].ToString();
                if (role.Equals("PurchaserComp"))
                    viewIsComputer = 1;
                else if (!(role.Equals("PurchaserOther")))
                {
                    if (role.Equals("Accountant"))
                        Response.Redirect("Accounting.aspx");
                    else
                        Response.Redirect("Register.aspx");
                }
            }


            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                try
                {
                    cmd.CommandText = "Select * FROM Orders WHERE Orders.ApprovalDate IS NOT NULL AND ComputerPurchase =  " + viewIsComputer.ToString() + "AND (UserID IS NULL OR UserID = " + Session["UserId"] + ") "
                        + (rdoDateView.SelectedIndex == 2 ? "AND Orders.purchaseDate IS NULL" : "")
                        + (rdoDateView.SelectedIndex == 3 ? "AND Orders.purchaseDate IS NOT NULL AND Orders.receiveDate IS NULL" : "")
                        + (rdoDateView.SelectedIndex == 1 ? "AND DATEDIFF(d, Orders.OrderRequestDate, '" + FromCalendar.SelectedDate.ToString() + "') < 1 "
                        + " AND DATEDIFF(d, Orders.OrderRequestDate, '" + ToCalendar.SelectedDate.ToString() + "') > -1" : "");
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);
                    grdOrders.DataSource = dt;
                    numRows = dt.Rows.Count;
                    grdOrders.DataBind();
                    con.Close();
                }
                catch
                {
                    ;
                }



            }
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            Session["OrderID"] = gvr.Cells[0].Text;

            Response.Redirect("EditOrder.aspx");

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            dbControls.Delete(gvr.Cells[0].Text);
            nonQuery(" DELETE FROM Orders  WHERE OrderID = '" + gvr.Cells[0].Text + "'");

        }

        protected void sortGrid()
        {
            DataTable dtSortTable = grdOrders.DataSource as DataTable;
            if (dtSortTable != null)
            {
                DataView dvSortedView = new DataView(dtSortTable);
                dvSortedView.Sort = ViewState["sortExpression"] + " " + ViewState["sortDirection"];
                grdOrders.DataSource = dvSortedView;
                grdOrders.DataBind();
            }
        }
        protected void grdOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression.ToString() == ViewState["sortExpression"].ToString())
                ViewState["sortDirection"] = getSortDirectionString();
            else
            {
                ViewState["sortDirection"] = "ASC";
                ViewState["sortExpression"] = e.SortExpression;
            }
        }
        private string getSortDirectionString()
        {
            if (ViewState["sortDirection"] == null)
            {
                ViewState["sortDirection"] = "ASC";
            }
            else
            {
                if (ViewState["sortDirection"].ToString() == "ASC")
                {
                    ViewState["sortDirection"] = "DESC";
                    return ViewState["sortDirection"].ToString();
                }
                if (ViewState["sortDirection"].ToString() == "DESC")
                {
                    ViewState["sortDirection"] = "ASC";
                    return ViewState["sortDirection"].ToString();
                }
            }
            return ViewState["sortDirection"].ToString();
        }

        protected void gv_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                string script = "alert(\"Edit!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }

            else if (e.CommandName == "cmdApprove")
            {
                string script = "alert(\"Approve!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
        }
        protected void IndexChanged(Object sender, EventArgs e)
        {
            if (rdoDateView.SelectedIndex == 1)
            {
                tblDate.Visible = true;
                ToCalendar.SelectedDate = DateTime.Today;
                ToCalendar.VisibleDate = DateTime.Today;
                FromCalendar.SelectedDate = DateTime.Today.AddDays(-14);
                FromCalendar.VisibleDate = DateTime.Today.AddDays(-14);
                CalenderChange();
            }
            else
                tblDate.Visible = false;
        }

        protected void FromCal_Click(object sender, EventArgs e)
        {
            if (FromCalendar.Visible == false)
            {
                FromCalendar.Visible = true;
                ToCalendar.Visible = true;
                FromCal.Text = "Close Calender";
            }
            else
            {
                FromCalendar.Visible = false;
                ToCalendar.Visible = false;
                FromCal.Text = "Open Calender";
            }
        }



        protected void CalenderChange(object sender, EventArgs e)
        {
            txtFrom.Text = FromCalendar.SelectedDate.ToShortDateString().ToString();
            txtTo.Text = ToCalendar.SelectedDate.ToShortDateString().ToString();
        }

        protected void CalenderChange()
        {
            txtFrom.Text = FromCalendar.SelectedDate.ToShortDateString().ToString();
            txtTo.Text = ToCalendar.SelectedDate.ToShortDateString().ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime dtf = new DateTime(), dtt = new DateTime();
            if (DateTime.TryParse(txtFrom.Text, out dtf) && DateTime.TryParse(txtTo.Text, out dtt))
            {
                FromCalendar.SelectedDate = dtf;
                FromCalendar.VisibleDate = dtf;
                if (dtf > dtt)
                    dtt = dtf;
                ToCalendar.SelectedDate = dtt;
                ToCalendar.VisibleDate = dtt;
            }
            CalenderChange();
        }
        protected void btnRecieveOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            dbControls.nonQuery("UPDATE Orders SET ReceiveDate = GETDATE() WHERE OrderID = " + gvr.Cells[0].Text + "");
            //Get the button that raised the event
            string eMail = dbControls.dbQuery("Select RequestorEmail from Orders where OrderID = " + gvr.Cells[0].Text);
            EECSMail mail = new EECSMail(eMail, "Your order has been canceled!!", "Unfortunately, your order (order#" + gvr.Cells[0]
                + ") has been canceled.\nAnother email may shortly come explaining any changes, along with a new order being placed.");
            mail.sendMail();
        }
            
        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if (gvr.Cells[12].Text.Equals("&nbsp;"))
            {
                dbControls.nonQuery("UPDATE Orders SET PurchaseDate = GETDATE(), UserID = " + Session["UserID"].ToString() + " WHERE OrderID = " + gvr.Cells[0].Text + "");
                //Get the button that raised the event
                dbControls.Place(gvr.Cells[0].Text);
                Session["OrderID"] = gvr.Cells[0].Text;


                Response.Redirect("EditOrder.aspx");
            }
            else
            {
                dbControls.nonQuery("UPDATE Orders SET PurchaseDate = NULL, UserID = NULL WHERE OrderID = '" + gvr.Cells[0].Text + "'");
                dbControls.Cancel(gvr.Cells[0].Text);
            }
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
    }
}