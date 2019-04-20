using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BankInfo : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            Session["AccNo"] = "";
            Session["AccName"] = "";
        }
    }

    protected void LinkButtonAccount_Click(object sender, EventArgs e)
    {
        if (Panel1.Visible == false)
        {
            Panel1.Visible = true;
            LinkButtonAccount.Text = "Close";
           
        }
        else
        {
            LinkButtonAccount.Text = "Get Account No";

            Panel1.Visible = false;
        }
        _LedgerIdText.Text = Session["AccNo"].ToString();
        Label7.Text = Session["AccName"].ToString();
    }

    protected void BtnChangeStatus_Click(object sender, EventArgs e)
    {

    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankInfo.aspx");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (RadioButtonListAccountType.SelectedValue == "0")
            {
                Session["AccFrom"] = "B";
            }
            else
            {
                Session["AccFrom"] = "CA";
            }
            accountantCls.GetLevelChartIDLevel5();
            _LedgerIdText.Text = Session["COAChartCode"].ToString();
            Session["Cut"] = _BNKBankNameArabicText.Text + " : " + _BNKBankNameEnglishText.Text;
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandText = "USP_Bank_Save";
            accountantCls.Cmd.Parameters.Add("@BankId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            accountantCls.Cmd.Parameters.Add("@BNKBankNameArabic", SqlDbType.NVarChar).Value =_BNKBankNameArabicText.Text;
            accountantCls.Cmd.Parameters.Add("@BNKBankNameEnglish", SqlDbType.NVarChar).Value =_BNKBankNameEnglishText.Text;
            accountantCls.Cmd.Parameters.Add("@LedgerId", SqlDbType.NVarChar).Value = Session["COAChartCode"].ToString();
            accountantCls.Cmd.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value =RadioButtonListAccountType.SelectedValue;
            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
           
            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            //accountantCls.Cmd.ExecuteNonQuery();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            ////update applications status - finance
            accountantCls.Cmd.Parameters.Clear();
            





            if (class1.RecordId == 0)
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;

            }
            else if (class1.RecordId == -1)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc = Resources.ResourceMain.UserAlreadyExists;

            }
            accountantCls.commitTrans();


        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;

        }
        finally
        {
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Bank_Edit";
            accountantCls.Cmd.Parameters.Add("@BankId", SqlDbType.Int).Value = ViewState["EditId"];
           
            accountantCls.Cmd.Parameters.Add("@BNKBankNameArabic", SqlDbType.NVarChar).Value = _BNKBankNameArabicText.Text;
            accountantCls.Cmd.Parameters.Add("@BNKBankNameEnglish", SqlDbType.NVarChar).Value = _BNKBankNameEnglishText.Text;
            accountantCls.Cmd.Parameters.Add("@LedgerId", SqlDbType.NVarChar).Value = _LedgerIdText.Text;
            accountantCls.Cmd.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value = RadioButtonListAccountType.SelectedValue;
           

            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            //accountantCls.Cmd.ExecuteNonQuery();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            ////update applications status - finance
            accountantCls.Cmd.Parameters.Clear();






            if (class1.RecordId == 0)
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;

            }
            else if (class1.RecordId == -1)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc = Resources.ResourceMain.UserAlreadyExists;

            }
            accountantCls.commitTrans();


        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;

        }
        finally
        {
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
    }

    protected void GvCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lb = (Label)GvCompany.SelectedRow.FindControl("Label1");
        ViewState["EditId"] = lb.Text.Trim();
        Session["BankID"] = ViewState["EditId"];
        class1.RecordId = int.Parse(ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "BankID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {

          _BNKBankNameArabicText.Text = MyDv[0].Row["BNKBankNameArabic"].ToString();
           _BNKBankNameEnglishText.Text = MyDv[0].Row["BNKBankNameEnglish"].ToString();
           _LedgerIdText.Text = MyDv[0].Row["LedgerId"].ToString();
          RadioButtonListAccountType.SelectedValue = MyDv[0].Row["AccountType"].ToString();
            BtnSave.Visible = false;
            BtnChangeStatus.Visible = true;
            BtnUpdate.Visible = true;
            //accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
        }
    }

    protected void RadioButtonListAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(RadioButtonListAccountType.SelectedIndex==0)
        {
            Label3.Text = "Bank Name Arabic";
            Label4.Text = "Bank Name English";
        }
        else
        {
            Label3.Text = "Cashier Name Arabic";
            Label4.Text = "Cashier Name English";
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

        _LedgerIdText.Text = Session["AccNo"].ToString();
        Label7.Text = Session["AccName"].ToString();
    }

    protected void ButtonSavechanges_Click(object sender, EventArgs e)
    {
        _LedgerIdText.Text = Session["AccNo"].ToString();
        Label7.Text = Session["AccName"].ToString();
    }
}