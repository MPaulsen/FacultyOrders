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
    public partial class Accounting : System.Web.UI.Page
    {
        int numRecords;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null)
                Response.Redirect("/login.aspx", true);
            else if (!(Session["Role"].ToString().Equals("Accountant")))
                Response.Redirect("/default.aspx", true);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {


            if (ViewState["sortDirection"] == null)
                ViewState.Add("sortDirection", "DESC");

            if (ViewState["sortExpression"] == null)
            {
                ViewState.Add("sortExpression", "Urgent");
            }
            
            loadGrid();
            sortGrid();
            
            disableApprove();
        }

        private void disableApprove()
        {
            int i = 0;
            int end = numRecords;
            for (i = 0; i < end; i++)
            {
                if (!(grdOrders.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                    grdOrders.Rows[i].Cells[19].Enabled = false;
            }
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            dbControls.Delete(gvr.Cells[0].Text);
            dbControls.nonQuery(" DELETE FROM Orders  WHERE OrderID = '" + gvr.Cells[0].Text + "'");
        }

        private void loadGrid()
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                cmd.CommandText = "Select * FROM Orders "
                    + (rdoDateView.SelectedIndex == 1 ? "WHERE Orders.ApprovalDate IS  NULL" : "")
                    + (rdoDateView.SelectedIndex == 2 ? "WHERE DATEDIFF(d, Orders.OrderRequestDate, '" + FromCalendar.SelectedDate.ToString() + "') < 1 "
                    + " AND DATEDIFF(d, Orders.OrderRequestDate, '" + ToCalendar.SelectedDate.ToString() + "') > -1" : "")
                    + (rdoDateView.SelectedIndex == 3 ? "WHERE Orders.purchaseDate IS NULL" : "")
                    + (rdoDateView.SelectedIndex == 4 ? "WHERE Orders.purchaseDate IS NOT NULL AND Orders.receiveDate IS NULL" : "");
                        
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                numRecords = dt.Rows.Count;
                grdOrders.DataSource = dt;
                grdOrders.DataBind();
                con.Close();
            }
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            Session["OrderID"] = gvr.Cells[0].Text;

            Response.Redirect("EditOrder.aspx");

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            dbControls.nonQuery("UPDATE Orders SET ApprovalDate = GETDATE() WHERE OrderID = '" + gvr.Cells[0].Text + "'");
            dbControls.Approve(gvr.Cells[0].Text);

        }

        protected void IndexChanged(Object sender, EventArgs e)
        {
            if (rdoDateView.SelectedIndex == 2)
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
        
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            loadGrid();
            sortGrid();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + "OrdersExport.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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

    }
}