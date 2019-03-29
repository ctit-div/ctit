

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Resources;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Collections;
using System.Drawing;

public partial class UcSearchAccount : System.Web.UI.UserControl
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


        }
    }



    public DataTable LoadDataTable()
    {
        string DataBase = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;

        string query = "Select * from tChartOfAccounts where  ChartId>134 and (COAChartCode like  N'%" + TxtSearch.Text + "%' or   COAChartName like N'%" + TxtSearch.Text + "%' or  ParentChartID like N'%" + TxtSearch.Text + "%') order by COAChartCode";
        DataTable dataTable = new DataTable();
        SqlDataAdapter dAdapter = new SqlDataAdapter(query, DataBase);
        dAdapter.Fill(dataTable);
        return dataTable;
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        GridView1.DataSource = LoadDataTable();
        GridView1.DataBind();
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["AccNo"] = GridView1.SelectedRow.Cells[1].Text.Trim();
        Session["AccName"] = GridView1.SelectedRow.Cells[2].Text.Trim();
    }
}