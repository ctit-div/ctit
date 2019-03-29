using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UcSearchItem : System.Web.UI.UserControl
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnSearchItem_Click(object sender, EventArgs e)
    {
        try
        {

            Session["ItemIds"] = TxtSearchItem.Text;
            Response.Redirect("DisplaySearch.aspx");
        }
        catch (Exception ex)
        {
            //Response.Write( ex.Message.ToString());
        }
    }
 


}