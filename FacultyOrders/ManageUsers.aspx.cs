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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null)
                Response.Redirect("/login.aspx", true);
            else if (!(Session["Role"].ToString().Equals("Admin")))
                Response.Redirect("/default.aspx", true);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (ViewState["sortDirection"] == null)
                ViewState.Add("sortDirection", "ASC");
            if (ViewState["sortExpression"] == null)
            {
                ViewState.Add("sortExpression", "UserID");
            }

            loadGrid();
            sortGrid();
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

        protected void sortGrid()
        {
            DataTable dtSortTable = grdUsers.DataSource as DataTable;
            if (dtSortTable != null)
            {
                DataView dvSortedView = new DataView(dtSortTable);
                dvSortedView.Sort = ViewState["sortExpression"] + " " + ViewState["sortDirection"];
                grdUsers.DataSource = dvSortedView;
                grdUsers.DataBind();
            }
        }
        protected void grdUsers_Sorting(object sender, GridViewSortEventArgs e)
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

            Session["UserID"] = gvr.Cells[0].Text;

            Response.Redirect("EditUser.aspx");

        }

        protected void btnAdd_click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

    }
}