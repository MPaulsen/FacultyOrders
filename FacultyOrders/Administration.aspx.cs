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
    public partial class Administration : System.Web.UI.Page
    {
        
        int numRecords;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null)
                Response.Redirect("/login.aspx", true);
            else if (!(Session["Role"].ToString().Equals("Admin")))
                Response.Redirect("/default.aspx", true);
            loadGrid();
            disableApprove();
        }

        private void disableApprove()
        {
            int i = 0;
            int end = numRecords;
            for (i = 0; i < end; i++)
            {
                if (!(grdOrders.Rows[i].Cells[11].Text.Equals("&nbsp;")))
                    grdOrders.Rows[i].Cells[18].Enabled = false;
            }
            
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;



            dbControls.nonQuery(" DELETE FROM Orders  WHERE OrderID = '" + gvr.Cells[0].Text + "'");

            Response.Redirect(Request.RawUrl);
        }

        private void loadGrid()
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                cmd.CommandText = "Select * FROM Orders";
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

        protected void grdOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtSortTable = grdOrders.DataSource as DataTable;
            if (dtSortTable != null)
            {
                DataView dvSortedView = new DataView(dtSortTable);
                dvSortedView.Sort = e.SortExpression + " " + getSortDirectionString();
                ViewState["sortExpression"] = e.SortExpression;
                grdOrders.DataSource = dvSortedView;
                grdOrders.DataBind();
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

            Response.Redirect(Request.RawUrl);
        }
    }
}