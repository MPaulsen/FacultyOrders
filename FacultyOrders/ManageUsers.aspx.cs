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
    public partial class ManageUsers : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        GridViewSortEventArgs ea = new GridViewSortEventArgs("Role", new SortDirection());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["Role"] == null)
                Response.Redirect("/login.aspx", true);
            else if (!(Session["Role"].ToString().Equals("Admin")))
                Response.Redirect("/default.aspx", true);
            if (ea == null)
            {
                ea = new GridViewSortEventArgs("Urgent", new SortDirection());
                ea.SortExpression = "Urgent";

            }

            if (ViewState["sortDirection"] == null)
                ViewState.Add("sortDirection", "ASC");
            if (ViewState["sortExpression"] == null)
            {
                ViewState.Add("sortExpression", "Urgent");
            }

            loadGrid();
            grdUsers_Sorting(ea);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            dbControls.nonQuery("DELETE FROM Users WHERE UserID = '" + gvr.Cells[0].Text + "'");

        }

        protected void loadGrid()
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString))
            {
                cmd.CommandText = "Select * FROM Users";

                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                grdUsers.DataSource = dt;
                grdUsers.DataBind();

                con.Close();
            }
        }

        protected void grdUsers_Sorting(GridViewSortEventArgs e)
        {
            DataTable dtSortTable = grdUsers.DataSource as DataTable;
            if (dtSortTable != null)
            {
                DataView dvSortedView = new DataView(dtSortTable);
                dvSortedView.Sort = e.SortExpression + " " + getSortDirectionString();
                ViewState["sortExpression"] = e.SortExpression;
                grdUsers.DataSource = dvSortedView;
                grdUsers.DataBind();
            }
        }
        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            ea = e;
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

            Session["UserID"] = gvr.Cells[0].Text;

            Response.Redirect("EditUser.aspx");

        }


    }
}