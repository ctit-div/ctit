using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        if (!IsPostBack)
        {
            Label2.Text = Session["Amount"].ToString();
            Label4.Text = Session["OrderCode1"].ToString();

            Session.Remove("Amount");
            Session.Remove("OrderCode1");
        }
    }
}