using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrailBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!this.IsPostBack)
        {
            string constr = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT top 20 COAChartCode,COAChartName,ChartLevel FROM tChartOfAccounts", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                       //GvCompany .DataSource = dt;
                       // GvCompany.DataBind();
                    }
                }
            }
        }
    }
}