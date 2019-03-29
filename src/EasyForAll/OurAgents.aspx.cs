
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class OurAgents : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUser();
        }
    }
    void BindUser()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            MyCmd.CommandText = "select * from AgentTable where status='0'";
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCmd);
            DataSet MyDateSet = new DataSet();
            MyAdapter.Fill(MyDateSet, "Users");
            DataList1.DataSource = MyDateSet.Tables["Users"];
            DataList1.DataBind();


            MyCon.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}