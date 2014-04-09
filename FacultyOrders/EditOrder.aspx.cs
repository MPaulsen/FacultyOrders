using System;
using System.Collections.Generic;
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
            loadForm();
        }

        protected void loadForm()
        {
            txtOrderID.Text = Session["OrderID"].ToString();


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //UPDATE where order ID = txtOrderID
        }
    }
}