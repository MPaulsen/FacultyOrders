using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacultyOrders
{
    public partial class FacultyOrders : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Role"] != null)
            {
                hlLogin.Visible = false;
                lblLogInMess.Text = "Welcome " + Session["User"] + ". | ";
                lblLogInMess.Visible = true;
                lbLogout.Visible = true;
            }
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("default.aspx");
        }
        

    }
}