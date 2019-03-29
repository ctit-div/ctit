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

public partial class _Default : System.Web.UI.Page
{
    
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if(!IsPostBack)
        {
            if (Session["dir"] == null)
            {
                Session["dir"] = "rtl";
            }

            GetCustomization();
        }
    }
    void GetCustomization()
    {
        Class1 queryResult = new Class1();
        try
        {
            Session["CompanyId"] = "7";
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = "select *  from tCustomizations WHERE CompanyId = " + Session["CompanyId"].ToString();
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {
                //Class2 class2 = new Class2()
                //{
                Session["BankGroup"] = accountantCls.Reader["BankGroup"].ToString();
                Session["CashGroup"] = accountantCls.Reader["CashGroup"].ToString();
                Session["ChequeGroup"] = accountantCls.Reader["ChequeGroup"].ToString();
                Session["CustomerGroup"] = accountantCls.Reader["CustomerGroup"].ToString();
                Session["EmployeeGroup"] = accountantCls.Reader["EmployeeGroup"].ToString();
                Session["IsVoucherNoMandatory"] = accountantCls.Reader["IsVoucherNoMandatory"].ToString();
                Session["PostingType"] = accountantCls.Reader["PostingType"].ToString();
                Session["ProfitLossLedger"] = accountantCls.Reader["ProfitLossLedger"].ToString();
                Session["SupplierGroup"] = accountantCls.Reader["SupplierGroup"].ToString();
                //Session["BankGroup"] = Convert.ToInt32(accountantCls.Reader["BankGroup"].ToString());
                //Session["CashGroup"] = Convert.ToInt32(accountantCls.Reader["CashGroup"].ToString());
                //Session["ChequeGroup"] = Convert.ToInt32(accountantCls.Reader["ChequeGroup"].ToString());
                //Session["CustomerGroup"] = Convert.ToInt32(accountantCls.Reader["CustomerGroup"].ToString());
                //Session["EmployeeGroup "] = Convert.ToInt32(accountantCls.Reader["EmployeeGroup"].ToString());
                //Session["IsVoucherNoMandatory "] = Convert.ToBoolean(accountantCls.Reader["IsVoucherNoMandatory"].ToString());
                //Session["PostingType "] = Convert.ToInt32(accountantCls.Reader["PostingType"].ToString());
                //Session["ProfitLossLedger "] = accountantCls.Reader["ProfitLossLedger"].ToString();
                //Session["SupplierGroup "] = Convert.ToInt32(accountantCls.Reader["SupplierGroup"].ToString());

                //};




            }
            accountantCls.Reader.Close();
            //accountantCls.commitTrans();
        }
        catch (Exception ex)
        {
            //accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }

    
}