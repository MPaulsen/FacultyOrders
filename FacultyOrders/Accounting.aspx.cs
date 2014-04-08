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
                cmd.CommandText = "Select * FROM Orders";
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
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
    }
}