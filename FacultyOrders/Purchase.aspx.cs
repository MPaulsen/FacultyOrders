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
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void loadGrid()
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                try
                {
                    cmd.CommandText = "Select * FROM Orders WHERE Orders.ApprovalDate IS  NULL " + (rdoDateView.SelectedIndex == 2 ? "AND Orders.purchaseDate IS NULL" : "") + (rdoDateView.SelectedIndex == 3 ? "AND Orders.purchaseDate IS NOT NULL" : "");
                    //if (rdoDateView.SelectedIndex == 3)
                    lblError.Text = cmd.CommandText;
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    da.Fill(dt);
                    grdOrders.DataSource = dt;
                    grdOrders.DataBind();
                    con.Close();
                }
                catch (Exception e)
                {
                    lblError.Text = e.ToString();
                }

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
        protected void IndexChanged(Object sender, EventArgs e){
            lblError.Text = "You selected" + rdoDateView.SelectedIndex.ToString();
            if (rdoDateView.SelectedIndex == 3)
                ;
        }

        protected void FromCal_Click(object sender, EventArgs e)
        {
            if (FromCalendar.Visible == false)
            {
                FromCalendar.Visible = true;
                FromCal.Text = "Close Calender";
            }
            else
            {
                FromCalendar.Visible = false;
                FromCal.Text = "Open Calender";
            }
        }
        
        protected void ToCal_Click(object sender, EventArgs e)
        {
            ;
        }
    }
}