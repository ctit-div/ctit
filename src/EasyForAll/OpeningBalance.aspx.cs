using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpeningBalance : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!this.IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                //LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
            }
            else
            {
                //LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);// GetLocalResourceObject("CompanyName").ToString();

            }

            ////Session["FinYearID"] = "8";
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
        

        _LevelDDL.DataBind();
            _LevelDDL.Items.Insert(0,new ListItem("Select All","-1"));

            DataView dv;
            dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
           
            if (dv.Count > 0)
            {
                GvCompany.DataSource = dv;
                GvCompany.DataBind();
            }
            //    string constr = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(constr))
            //    {
            //        using (SqlCommand cmd = new SqlCommand("SELECT top 20 ChartId, COAChartCode,COAChartName,ChartLevel FROM tChartOfAccounts", con))
            //        {
            //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            //            {
            //                cmd.CommandType = CommandType.Text;
            //                DataTable dt = new DataTable();
            //                sda.Fill(dt);
            //                GvCompany.DataSource = dt;
            //                GvCompany.DataBind();
            //            }
            //        }
            //    }
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        DataView dv;
        dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        dv.RowFilter = "COAChartCode ='" + _SearchText.Text + "' or COAChartName like '%" + _SearchText.Text + "%'";
        if (dv.Count > 0)
        {
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }
        else
        {
            dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }
    }

    protected void _LevelDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dv;
        dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        dv.RowFilter = "ChartLevel <='" + _LevelDDL.SelectedValue + "'";
        if(dv.Count>0)
        {
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }
        else
        {
            dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }
    }

    protected void GvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCompany.PageIndex = e.NewPageIndex;
        DataView dv;
        dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        dv.RowFilter = "ChartLevel <='" + _LevelDDL.SelectedValue + "'";
        if (dv.Count > 0)
        {
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }
        else
        {
            dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.beginTrans();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandText = "USP_LedgerOpeningBalance_Del";
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"];
            accountantCls.Cmd.ExecuteNonQuery();
        
            
                //Save branch opening balances for the ledger 
                foreach (GridViewRow r in GvCompany.Rows)
                {

                    accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                    accountantCls.Cmd.Parameters.Clear();
                    accountantCls.Cmd.CommandText = "USP_BranchOpeningBalance_Save";
             
                accountantCls.Cmd.Parameters.Add("@FinYearID", SqlDbType.Int).Value = Session["FinYearID"].ToString();
                accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = Session["BranchId"].ToString();
                accountantCls.Cmd.Parameters.Add("@LedgerId", SqlDbType.Int).Value = r.Cells[0].Text.Trim();


                DropDownList OpeningBalanceTypeDDL = (DropDownList)r.FindControl("_OpeningBalanceTypeDDL");
                if (OpeningBalanceTypeDDL.SelectedIndex!=0)
                {
                    TextBox TxtOB = (TextBox)r.FindControl("TxtOB");

                accountantCls.Cmd.Parameters.Add("@OpeningBalance", SqlDbType.Decimal).Value = TxtOB.Text;
                    accountantCls.Cmd.Parameters.Add("@OpeningBalanceType", SqlDbType.NVarChar).Value = OpeningBalanceTypeDDL.SelectedValue;
                    accountantCls.Cmd.ExecuteNonQuery();

                }
                
                }
            

            class1.RecordId = 0;
            class1.ErrorNo = 0;
            accountantCls.commitTrans();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;
            class1.ErrorFunction = " Finance.DAL.OpeningBalance.Save";
        }
    


        finally
        {
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
    }
}