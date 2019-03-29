
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

public partial class ReceiptVoucher : System.Web.UI.Page
{

     AccountantCls accountantCls =new AccountantCls();
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
                LabelTitle.Text = "Receipt Voucher";
                LabelPayedTo.Text = String.Format(global::Resources.ResourceMain.PayTo);
                LabelVoucherNo.Text = String.Format(global::Resources.ResourceMain.VoucherNo);
                LabelVoucherDate.Text = String.Format(global::Resources.ResourceMain.VoucherDate);
                LabelReceiver.Text = String.Format(global::Resources.ResourceMain.ReceiverName);
                LabelPaymentType.Text = String.Format(global::Resources.ResourceMain.PaymentType);
                LabelPaidFrom.Text = String.Format(global::Resources.ResourceMain.PayFrom);
                LabelReceiptType.Text = String.Format(global::Resources.ResourceMain.ReceiptType);
                LabelTransferDate.Text = String.Format(global::Resources.ResourceMain.ChequeDate);
                LabelDescription.Text = String.Format(global::Resources.ResourceMain.Description);
                LabelChequeNumber.Text = String.Format(global::Resources.ResourceMain.ChequeNo);
                LabelChequePayeeName.Text = String.Format(global::Resources.ResourceMain.ChequePayeeName);
                LabelPlaceOfIssue.Text = String.Format(global::Resources.ResourceMain.PlaceOfIssue);
                LabelReceivedFrom.Text = String.Format(global::Resources.ResourceMain.ReceivedFrom);
                LabelPayedFrom.Text = String.Format(global::Resources.ResourceMain.PayFrom);
                LabelBranchName.Text = String.Format(global::Resources.ResourceMain.BranchName);
                LabelDepartmentName.Text = String.Format(global::Resources.ResourceMain.DepartmentName);
                LabelDevisionName.Text = String.Format(global::Resources.ResourceMain.DevisionName);
                LabelCurrency.Text = String.Format(global::Resources.ResourceMain.Currency);
                LabelForiegnCurrencyAmount.Text = String.Format(global::Resources.ResourceMain.ForiegnCurrencyAmount);
                LabelExchangeRate.Text = String.Format(global::Resources.ResourceMain.ExchangeRate);
                LabelAmountinLocalCurrency.Text = String.Format(global::Resources.ResourceMain.ForiegnCurrencyAmount);
                LabelDescriptionS.Text = String.Format(global::Resources.ResourceMain.Description);
                GvCompany.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain.VoucherID);
                GvCompany.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain.ManualVoucherNo);
                GvCompany.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain.VoucherNo);
                GvCompany.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain.VoucherDate);
                GvCompany.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain.VoucherType);


            }
            else
            {
                LabelTitle.Text = "سند قبض";
                LabelPayedTo.Text = String.Format(global::Resources.ResourceMain_Ar.PayTo);
                LabelVoucherNo.Text = String.Format(global::Resources.ResourceMain_Ar.VoucherNo);
                LabelVoucherDate.Text = String.Format(global::Resources.ResourceMain_Ar.VoucherDate);
                LabelReceiver.Text = String.Format(global::Resources.ResourceMain_Ar.ReceiverName);
                LabelPaymentType.Text = String.Format(global::Resources.ResourceMain_Ar.PaymentType);
                LabelPaidFrom.Text = String.Format(global::Resources.ResourceMain_Ar.PayFrom);
                LabelReceiptType.Text = String.Format(global::Resources.ResourceMain_Ar.ReceiptType);
                LabelTransferDate.Text = String.Format(global::Resources.ResourceMain_Ar.ChequeDate);
                LabelDescription.Text = String.Format(global::Resources.ResourceMain_Ar.Description);
                LabelChequeNumber.Text = String.Format(global::Resources.ResourceMain_Ar.ChequeNo);
                LabelChequePayeeName.Text = String.Format(global::Resources.ResourceMain_Ar.ChequePayeeName);
                LabelPlaceOfIssue.Text = String.Format(global::Resources.ResourceMain_Ar.PlaceOfIssue);
                LabelReceivedFrom.Text = String.Format(global::Resources.ResourceMain_Ar.ReceivedFrom);
                LabelPayedFrom.Text = String.Format(global::Resources.ResourceMain_Ar.PayFrom);
                LabelBranchName.Text = String.Format(global::Resources.ResourceMain_Ar.BranchName);
                LabelDepartmentName.Text = String.Format(global::Resources.ResourceMain_Ar.DepartmentName);
                LabelDevisionName.Text = String.Format(global::Resources.ResourceMain_Ar.DevisionName);
                LabelCurrency.Text = String.Format(global::Resources.ResourceMain_Ar.Currency);
                LabelForiegnCurrencyAmount.Text = String.Format(global::Resources.ResourceMain_Ar.ForiegnCurrencyAmount);
                LabelExchangeRate.Text = String.Format(global::Resources.ResourceMain_Ar.ExchangeRate);
                LabelAmountinLocalCurrency.Text = String.Format(global::Resources.ResourceMain_Ar.ForiegnCurrencyAmount);
                LabelDescriptionS.Text = String.Format(global::Resources.ResourceMain_Ar.Description);
                GvCompany.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain_Ar.VoucherID);
                GvCompany.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain_Ar.ManualVoucherNo);
                GvCompany.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain_Ar.VoucherNo);
                GvCompany.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain_Ar.VoucherDate);
                GvCompany.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain_Ar.VoucherType);


            }
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
            SqlDataSourceBranch.DataBind();
            _ddBranchName.DataBind();
            _ddBranchName.Items.Insert(0, new ListItem("Please Select", "0"));
            BindCurrency();
            BindPaidFrom();
            BindPaidTo();

        }
    }
    void BindCurrency()
    {
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

    }
    void BindPaidFrom()
    {

        string selectCommand;

        selectCommand = "SELECT * FROM View_Bank_Account where  CompanyID='" + Session["CompanyId"].ToString() + "' and AccountType='" + _ddReceiptType.SelectedValue + "'";// and ParentChartID='" + Session["CashGroup"].ToString() + "' ";


        SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        DataTable dt = new DataTable();
        dad.Fill(dt);
        _ddPaidFrom.DataSource = dt;
        _ddPaidFrom.DataTextField =  "COAChartName";
        _ddPaidFrom.DataValueField = "COAChartCode";
        _ddPaidFrom.DataBind();
        _ddPaidFrom.Items.Insert(0, new ListItem("Please Select", "0"));

    }

    void BindPaidTo()
    {
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
        if (_VoucherNoText.Text == null) _VoucherNoText.Text = "";
       

        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            //accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.beginTrans();
            if(ViewState["EditId"].ToString()!="0")
            { 
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandText = "USP_Voucher_Log";
            accountantCls.Cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value =  ViewState["EditId"];
            accountantCls.Cmd.ExecuteNonQuery();
            }
            //Voucher Master
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandText = "USP_VoucherMaster_Ins";
            accountantCls.Cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value =  ViewState["EditId"];
            accountantCls.Cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "R";
            accountantCls.Cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            accountantCls.Cmd.Parameters.Add("@FinYearID", SqlDbType.Int).Value = Session["FinYearID"].ToString();
            accountantCls.Cmd.Parameters.Add("@ManualVoucherNo", SqlDbType.NVarChar).Value = _VoucherNoText.Text;
            accountantCls.Cmd.Parameters.Add("@VoucherDate", SqlDbType.VarChar).Value = _VoucherDateText.Text;
            accountantCls.Cmd.Parameters.Add("@PayedReceivedBy", SqlDbType.NVarChar).Value = _ReceiverNameText.Text;
            accountantCls.Cmd.Parameters.Add("@PaymentType", SqlDbType.Int).Value =_ddReceiptType.SelectedValue;
            accountantCls.Cmd.Parameters.Add("@BankType", SqlDbType.Int).Value = _ddPaymentType.SelectedValue;
            if (_TransferDateText.Text == null)
                accountantCls.Cmd.Parameters.Add("@BankTransferDate", SqlDbType.VarChar).Value = DBNull.Value;
            else
                accountantCls.Cmd.Parameters.Add("@BankTransferDate", SqlDbType.VarChar).Value = _TransferDateText.Text;
            accountantCls.Cmd.Parameters.Add("@ChequeNo", SqlDbType.NVarChar).Value = _TxtChequeNo.Text;
            accountantCls.Cmd.Parameters.Add("@ChequePayeeName", SqlDbType.NVarChar).Value = _TxtPayeeName.Text;
            if (_TransferDateText.Text == null)
                accountantCls.Cmd.Parameters.Add("@ChequeDate", SqlDbType.VarChar).Value = DBNull.Value;
            else
                accountantCls.Cmd.Parameters.Add("@ChequeDate", SqlDbType.VarChar).Value = _TransferDateText.Text;
            accountantCls.Cmd.Parameters.Add("@PlaceOfIssue", SqlDbType.NVarChar).Value = _TxtPlaceOfIssue.Text;
            accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _DescriptionText.Text;
            accountantCls.Cmd.Parameters.Add("@IsSingle", SqlDbType.Bit).Value = Convert.ToBoolean(0);
            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            int vouhcerId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            if ( ViewState["EditId"].ToString() != "0") vouhcerId = int.Parse( ViewState["EditId"].ToString());

            if (vouhcerId > 0)// To ledger
            {
                
                    accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                    accountantCls.Cmd.Parameters.Clear();
                    accountantCls.Cmd.CommandText = "USP_VoucherDetail_Ins";
                    accountantCls.Cmd.Parameters.Add("@VoucherID", SqlDbType.Int).Value = vouhcerId;
                    accountantCls.Cmd.Parameters.Add("@TransactionType", SqlDbType.VarChar).Value = "T";//to
                    accountantCls.Cmd.Parameters.Add("@LedgerId", SqlDbType.Int).Value = _ddPayedTo.SelectedValue;
                    accountantCls.Cmd.Parameters.Add("@FinYearId", SqlDbType.Int).Value = Session["FinYearID"].ToString();
                    accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value =_ddBranchName.SelectedValue;
                    accountantCls.Cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = _ddDepartment.SelectedValue;
                    accountantCls.Cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value =_ddUnitName.SelectedValue;
                    accountantCls.Cmd.Parameters.Add("@Debit", SqlDbType.Decimal).Value =_TxtAmount.Text;
                    accountantCls.Cmd.Parameters.Add("@Credit", SqlDbType.Decimal).Value = 0;
                    accountantCls.Cmd.Parameters.Add("@ForeignAmount", SqlDbType.Decimal).Value = _TxtCurrency.Text;
                    accountantCls.Cmd.Parameters.Add("@ExchangeRate", SqlDbType.Decimal).Value = _TxtValue.Text;
                    accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _TxtDescription.Text;
                    accountantCls.Cmd.Parameters.Add("@CostCenterApplicable", SqlDbType.Int).Value = 0;
                    accountantCls.Cmd.Parameters.Add("@VoucherDate", SqlDbType.VarChar).Value = _VoucherDateText.Text;
                accountantCls.Cmd.Parameters.Add("@ManualVoucherNo", SqlDbType.NVarChar).Value = _VoucherNoText.Text;
                int vouhcerDetailId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
                class1.ErrorNo = 0;
                class1.RecordId = vouhcerId;
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
        BindPaidFrom();
        BindPaidTo();

        ViewState["EditId"] = GvCompany.SelectedRow.Cells[2].Text.Trim();
        Session["VoucherID"] = ViewState["EditId"];
        class1.RecordId =int.Parse( ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "VoucherID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            _VoucherDateText.Text = Convert.ToDateTime(MyDv[0].Row["VOMVoucherDate"]).ToString("dd/MM/yyyy");
            _ReceiverNameText.Text = MyDv[0].Row["VOMPayedReceivedBy"].ToString();
            _ddReceiptType.SelectedValue = MyDv[0].Row["VOMPayRepType"].ToString();
            _TxtDescription.Text = MyDv[0].Row["VOMDescription"].ToString();
            _ddPaymentType.SelectedValue = MyDv[0].Row["VOMBankType"].ToString();
  

             _TxtChequeNo.Text = MyDv[0].Row["VOMChequeNo"].ToString();
            _TxtPlaceOfIssue.Text = MyDv[0].Row["VOMPlaceOfIssue"].ToString();
            _TxtPayeeName.Text = MyDv[0].Row["VOMChequePayeeName"].ToString();
            if (MyDv[0].Row["VOMBankTransferDate"] != null && MyDv[0].Row["VOMBankTransferDate"].ToString() != "")
            {
                _TransferDateText.Text = Convert.ToDateTime(MyDv[0].Row["VOMBankTransferDate"]).ToString("dd/MM/yyyy");
            }
            _TxtPlaceOfIssue.Text = MyDv[0].Row["VOMPlaceOfIssue"].ToString();
            _VoucherNoText.Text = MyDv[0].Row["VOMManualVoucherNo"].ToString().Trim();
            _ddReceiptType_SelectedIndexChanged(sender, e);
            _ddBankType_SelectedIndexChanged(sender, e);
            BtnSave.Visible = false;
            BtnChangeStatus.Visible = true;
            BtnUpdate.Visible = true;
        }

   DataView MyDvDetails = (DataView)SqlDataSourceDetails.Select(DataSourceSelectArguments.Empty);
        MyDvDetails.RowFilter = "VoucherID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDvDetails.Count > 0)
        {
            _TxtAmount.Text = MyDvDetails[0].Row["Debit"].ToString();
            _DescriptionText.Text = MyDvDetails[0].Row["Description"].ToString();
            _TxtCurrency.Text = MyDvDetails[0].Row["ForeignAmount"].ToString();
            _TxtValue.Text = MyDvDetails[0].Row["ExchangeRate"].ToString();
        }

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiptVoucher.aspx");
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
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (Calendar2.Visible == true)
        {
            Calendar2.Visible = false;
        }
        else
        {
            Calendar2.Visible = true;

        }
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        _TransferDateText.Text = Calendar2.SelectedDate.ToShortDateString();
        Calendar2.Visible = false;
    }

    protected void _ddReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(_ddReceiptType.SelectedIndex==2)
        {
            Panel1.Visible = true;
            //_ddPayedTo.Enabled = true;
        }
        else
        { Panel1.Visible = false;
            //_ddPayedTo.Enabled = false;
        }
        BindPaidFrom();
        BindPaidTo();
    }

    protected void _ddBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_ddPaymentType.SelectedIndex == 2)
        {
            Panel2.Visible = true;
        }
        else
        {
            Panel2.Visible = false;
        }
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
        if (_ddCurrency.SelectedIndex==0)
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
}