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

public partial class JournalVoucher : System.Web.UI.Page
{

    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();


    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                //LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
            }
            else
            {
                //LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);// GetLocalResourceObject("CompanyName").ToString();

            }



            //SqlDataSource1.DataBind();
            //GvCompany.DataBind();
            //Session["FinYearID"] = "8";
            //Session["CompanyId"] = "8";
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
            SqlDataSourceBranch.DataBind();
            _ddBranchName.DataBind();
            _ddBranchName.Items.Insert(0, new ListItem("Please Select", "0"));
            BindCurrency();
           
            BindPaidTo();
            //SqlDataSourceCurrency.DataBind();
            //_ddCurrency.DataBind();
            //_ddPaidFrom.Items.Insert(0, new ListItem("Please Select", "0"));
            //_ddPayedTo.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    void BindCurrency()
    {

        //string selectCommand;CurrencyId, CurrencyName + '(' + CurrencySymbol + ')'  as CurrencyName,
        //selectCommand = "SELECT * FROM tCurrency where  CompanyID='" + Session["CompanyId"].ToString() + "' and ParentChartID='" + Session["CashGroup"].ToString() + "' ";
        DataView dv;
        dv = (DataView)SqlDataSourceCurrency.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0)
        {
            _ddCurrency.DataSource = dv;
            _ddCurrency.DataTextField = "CurrencyName";
            _ddCurrency.DataValueField = "CurrencyId";
            _ddCurrency.DataBind();

        }
        _ddCurrency.Items.Insert(0, new ListItem("Please Select", "0"));
        //SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        //DataTable dt = new DataTable();
        //dad.Fill(dt);



    }
  

    void BindPaidTo()
    {
        //int s = 0;
        //int ss = 0;


        //s =new Class2().CashGroup;
        //ss = new Class2().BankGroup;
        string selectCommand;

        selectCommand = "SELECT ChartId, COAChartCode, COAChartCode + ':' + COAChartName AS COAChartName, CompanyID, ParentChartID FROM tChartOfAccounts where  CompanyID='" + Session["CompanyId"].ToString() + "' and AccountType='1'";



        SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        DataTable dt = new DataTable();
        dad.Fill(dt);
        _ddPayedTo.DataSource = dt;
        _ddPayedTo.DataTextField = "COAChartName";
        _ddPayedTo.DataValueField = "COAChartCode";
        _ddPayedTo.DataBind();
        _ddPayedTo.Items.Insert(0, new ListItem("Please Select", "0"));

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if(LblCredit.Text!=LblDebt.Text)
        {
            LblStatus.Text = "Debit is not equal to credit";
            return;
        }
        if (_VoucherNoText.Text == null) _VoucherNoText.Text = "";


        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            //accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.beginTrans();
            if (ViewState["EditId"].ToString() != "0")
            {
                accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                accountantCls.Cmd.Parameters.Clear();
                accountantCls.Cmd.CommandText = "USP_Voucher_Log";
                accountantCls.Cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value = ViewState["EditId"];
                accountantCls.Cmd.ExecuteNonQuery();
            }
            //Voucher Master
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandText = "USP_VoucherMaster_Ins";

            accountantCls.Cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value = ViewState["EditId"];
            accountantCls.Cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "J";
            accountantCls.Cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            accountantCls.Cmd.Parameters.Add("@FinYearID", SqlDbType.Int).Value = Session["FinYearID"].ToString();
            accountantCls.Cmd.Parameters.Add("@ManualVoucherNo", SqlDbType.NVarChar).Value = _VoucherNoText.Text;
            accountantCls.Cmd.Parameters.Add("@VoucherDate", SqlDbType.VarChar).Value = _VoucherDateText.Text;
            accountantCls.Cmd.Parameters.Add("@PayedReceivedBy", SqlDbType.NVarChar).Value = 0;
            accountantCls.Cmd.Parameters.Add("@PaymentType", SqlDbType.Int).Value = _ddReceiptType.SelectedValue;
            accountantCls.Cmd.Parameters.Add("@BankType", SqlDbType.Int).Value = 0;
           
                accountantCls.Cmd.Parameters.Add("@BankTransferDate", SqlDbType.VarChar).Value = DBNull.Value;
            
            accountantCls.Cmd.Parameters.Add("@ChequeNo", SqlDbType.NVarChar).Value = "-";
            accountantCls.Cmd.Parameters.Add("@ChequePayeeName", SqlDbType.NVarChar).Value ="-";
           
                accountantCls.Cmd.Parameters.Add("@ChequeDate", SqlDbType.VarChar).Value = DBNull.Value;
           
            accountantCls.Cmd.Parameters.Add("@PlaceOfIssue", SqlDbType.NVarChar).Value = "-";
            accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = "-";
            accountantCls.Cmd.Parameters.Add("@IsSingle", SqlDbType.Bit).Value = Convert.ToBoolean(0);
            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();


            //accountantCls.Cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value = ViewState["EditId"];
            //accountantCls.Cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "J";
            //accountantCls.Cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            //accountantCls.Cmd.Parameters.Add("@FinYearID", SqlDbType.Int).Value = Session["FinYearID"].ToString();
            //accountantCls.Cmd.Parameters.Add("@ManualVoucherNo", SqlDbType.NVarChar).Value = _VoucherNoText.Text;
            //accountantCls.Cmd.Parameters.Add("@VoucherDate", SqlDbType.VarChar).Value = _VoucherDateText.Text;
            //accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            int vouhcerId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            if (ViewState["EditId"].ToString() != "0") vouhcerId = int.Parse(ViewState["EditId"].ToString());

            if (vouhcerId > 0)// To ledger
            {
                //int price = 0, amt = 0, qty = 0;
                foreach (GridViewRow row in GvCompany.Rows)
                {

                 accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                accountantCls.Cmd.Parameters.Clear();
                accountantCls.Cmd.CommandText = "USP_VoucherDetail_Ins";
                accountantCls.Cmd.Parameters.Add("@VoucherID", SqlDbType.Int).Value = vouhcerId;
                accountantCls.Cmd.Parameters.Add("@TransactionType", SqlDbType.VarChar).Value = "J";//to
                    accountantCls.Cmd.Parameters.Add("@LedgerId", SqlDbType.Int).Value = row.Cells[8].Text.Trim();
                accountantCls.Cmd.Parameters.Add("@FinYearId", SqlDbType.Int).Value = Session["FinYearID"].ToString();
                accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = _ddBranchName.SelectedValue;
                accountantCls.Cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = _ddDepartment.SelectedValue;
                accountantCls.Cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = _ddUnitName.SelectedValue;
                    accountantCls.Cmd.Parameters.Add("@Debit", SqlDbType.Decimal).Value = row.Cells[3].Text.Trim();
                accountantCls.Cmd.Parameters.Add("@Credit", SqlDbType.Decimal).Value = row.Cells[4].Text.Trim();
                    accountantCls.Cmd.Parameters.Add("@ForeignAmount", SqlDbType.Decimal).Value = row.Cells[5].Text.Trim();
                    accountantCls.Cmd.Parameters.Add("@ExchangeRate", SqlDbType.Decimal).Value = row.Cells[6].Text.Trim();
                    accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = row.Cells[7].Text.Trim();
                    accountantCls.Cmd.Parameters.Add("@CostCenterApplicable", SqlDbType.Int).Value = 0;
                accountantCls.Cmd.Parameters.Add("@VoucherDate", SqlDbType.VarChar).Value = _VoucherDateText.Text;
                    accountantCls.Cmd.Parameters.Add("@ManualVoucherNo", SqlDbType.NVarChar).Value = _VoucherNoText.Text;
                    int vouhcerDetailId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
                class1.ErrorNo = 0;
                class1.RecordId = vouhcerId;
                }
               
                accountantCls.commitTrans();
            }
            else
            {
                accountantCls.rollBackTrans();
                class1.ErrorDesc = "Error while saving to voucher master";
                class1.ErrorNo = -1;
                class1.ErrorFunction = " Finance.DAL.Voucher.Save";
            }
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;
            class1.ErrorFunction = " Finance.DAL.Voucher.Save";
        }
    }



    protected void GvCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        BindPaidTo();

        ViewState["EditId"] = GvCompany.SelectedRow.Cells[2].Text.Trim();
        Session["VoucherID"] = ViewState["EditId"];
        class1.RecordId = int.Parse(ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "VoucherID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            _VoucherDateText.Text = MyDv[0].Row["VOMVoucherDate"].ToString();
            _TxtDescription.Text = MyDv[0].Row["VOMDescription"].ToString();
            _VoucherNoText.Text = MyDv[0].Row["VOMManualVoucherNo"].ToString().Trim();
            _ddReceiptType_SelectedIndexChanged(sender, e);
            BtnSave.Visible = false;
            BtnChangeStatus.Visible = true;
            BtnUpdate.Visible = true;
        }

        DataView MyDvDetails = (DataView)SqlDataSourceDetails.Select(DataSourceSelectArguments.Empty);
        MyDvDetails.RowFilter = "VoucherID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDvDetails.Count > 0)
        {
            _TxtAmount.Text = MyDvDetails[0].Row["Debit"].ToString();
            _TxtCurrency.Text = MyDvDetails[0].Row["ForeignAmount"].ToString();
            _TxtValue.Text = MyDvDetails[0].Row["ExchangeRate"].ToString();
        }

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalVoucher.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
       
    }
    protected void BtnChangeStatus_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Calendar1.Visible == true)
        {
            Calendar1.Visible = false;
        }
        else
        {
            Calendar1.Visible = true;

        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        _VoucherDateText.Text = Calendar1.SelectedDate.ToShortDateString();
        Calendar1.Visible = false;
    }
    protected void _ddReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
              
        BindPaidTo();
    }
    protected void _ddBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["BranchId"] = _ddBranchName.SelectedValue;
        SqlDataSourceDepartment.DataBind();
        _ddDepartment.DataBind();
        _ddDepartment.Items.Insert(0, new ListItem("Please Select", "0"));
    }
    protected void _ddDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["DepartmentId"] = _ddBranchName.SelectedValue;
        SqlDataSourceDivision.DataBind();
        _ddUnitName.DataBind();
        _ddUnitName.Items.Insert(0, new ListItem("Please Select", "0"));
    }
    protected void _ddCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        _TxtAmount.Text = "0";
        _TxtValue.Text = "0";
        _TxtCurrency.Text = "0";
        if (_ddCurrency.SelectedIndex == 0)
        {
            _TxtCurrency.Enabled = false;
            _TxtValue.Enabled = false;
            _TxtAmount.Focus();
        }
        else
        {
            _TxtCurrency.Enabled = true;
            _TxtValue.Enabled = true;
            _TxtCurrency.Focus();
        }
    }
    protected void _TxtCurrency_TextChanged(object sender, EventArgs e)
    {
        decimal CurrencyAmount = 0;
        decimal ValuePrice = 0;
        decimal Result = 0;
        CurrencyAmount = decimal.Parse(_TxtCurrency.Text);
        ValuePrice = decimal.Parse(_TxtValue.Text);
        Result = CurrencyAmount * ValuePrice;
        _TxtAmount.Text = Result.ToString();
        _TxtValue.Focus();

    }
    protected void _TxtValue_TextChanged(object sender, EventArgs e)
    {
        decimal CurrencyAmount = 0;
        decimal ValuePrice = 0;
        decimal Result = 0;
        CurrencyAmount = decimal.Parse(_TxtCurrency.Text);
        ValuePrice = decimal.Parse(_TxtValue.Text);
        Result = CurrencyAmount * ValuePrice;
        _TxtAmount.Text = Result.ToString();
        _TxtAmount.Focus();
    }
    DataTable tab = new DataTable();
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        DataColumn c1 = new DataColumn("ManualVoucherNo");
        DataColumn c2 = new DataColumn("VoucherDate");
        DataColumn c3 = new DataColumn("Debit");
        DataColumn c4 = new DataColumn("Credit");
        DataColumn c5 = new DataColumn("ForeignAmount");

        DataColumn c6 = new DataColumn("ExchangeRate");
        DataColumn c7 = new DataColumn("Description");
        DataColumn c8 = new DataColumn("Ledger");
        //DataColumn c9 = new DataColumn("Credit");
        //DataColumn c10 = new DataColumn("ForeignAmount");

        tab.Columns.Add(c1);
        tab.Columns.Add(c2);
        tab.Columns.Add(c3);
        tab.Columns.Add(c4);
        tab.Columns.Add(c5);
        tab.Columns.Add(c6);
        tab.Columns.Add(c7);
        tab.Columns.Add(c8);

        if (Session["cart"] == null)
        {


            string d = "0";
            string c = "0";
            if (_ddReceiptType.SelectedValue == "0")
            {
                return;
            }
            else if (_ddReceiptType.SelectedValue == "1")
            {
                d = _TxtAmount.Text;
            }
            else if (_ddReceiptType.SelectedValue == "2")
            {
              c=  _TxtAmount.Text;


            }
            DataRow row = tab.NewRow();
            row["ManualVoucherNo"] =_VoucherNoText.Text;
            row["VoucherDate"] = _VoucherDateText.Text;
            row["Debit"] = d;
            row["Credit"] = c;
            row["ForeignAmount"] = _TxtCurrency.Text;
            row["ExchangeRate"] = _TxtValue.Text;
            row["Description"] = _TxtDescription.Text;
            row["Ledger"] = _ddPayedTo.SelectedValue;
            tab.Rows.Add(row);

        }
        else
        {
            tab = (DataTable)Session["cart"];


            string d = "0";
            string c = "0";
            if (_ddReceiptType.SelectedValue == "0")
            {
                return;
            }
            else if (_ddReceiptType.SelectedValue == "1")
            {
                d = _TxtAmount.Text;
            }
            else if (_ddReceiptType.SelectedValue == "2")
            {
                c = _TxtAmount.Text;


            }
            DataRow row = tab.NewRow();
            row["ManualVoucherNo"] = _VoucherNoText.Text;
            row["VoucherDate"] = _VoucherDateText.Text;
            row["Debit"] = d;
            row["Credit"] = c;
            row["ForeignAmount"] = _TxtCurrency.Text;
            row["ExchangeRate"] = _TxtValue.Text;
            row["Description"] = _TxtDescription.Text;
            row["Ledger"] = _ddPayedTo.SelectedValue;
            tab.Rows.Add(row);
        }
        Session["cart"] = tab;
        GvCompany.DataSource = tab;
        GvCompany.DataBind();
        Bind();
    }

    void Bind()
    {
        decimal Credit = 0;
        decimal amt = 0;
        decimal Debit = 0;

        //int price = 0, amt = 0, qty = 0;
        foreach (GridViewRow row in GvCompany.Rows)
        {

            Credit += decimal.Parse(row.Cells[4].Text.Trim());
            LblCredit.Text = Credit.ToString();

            Debit += decimal.Parse(row.Cells[3].Text.Trim());
            LblDebt.Text = Debit.ToString();
        }
        if (Credit != Debit)
        {
            LblStatus.Text = "Debit is not equal to credit";
            LblStatus.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            LblStatus.Text = "Debit is  equal to credit";
            LblStatus.ForeColor = System.Drawing.Color.Green;
        }
      
    }
}