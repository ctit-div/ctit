using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class MasterPage : System.Web.UI.MasterPage
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    public SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserName"]!= null)
        {
            Label1.Text ="Welcome our customer: " + Session["UserName"].ToString();
        }
        else
        {
            //Session.Abandon();
            Label1.Text = "Welcome visitor";
        }
        //Session["dir"] = "ltr";
        if (!IsPostBack)
        {
           
            //if (Session["dir"].ToString() == "ltr")
            //{
            //    LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
            //}
            //else
            //{
            //    LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);// GetLocalResourceObject("CompanyName").ToString();

            //}



            
            
            Session["AdminUserName"] = "1";

            if (Session["UserCode"] != null)
            {
                getMenu();
            }

        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblMessage.Text = "";
        try
        {

            SqlConnection MyCon = new SqlConnection(accountantCls.GetConnStr());
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;


            MyCmd.CommandText = "select * from View_tUsers where Email='" + _EmailText.Text + "' and Password = '" + Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text) + "' and IsActive='1'";
            SqlDataReader Reader = MyCmd.ExecuteReader();

            if (Reader.Read())
            {
                Session["Type"] = Reader[27].ToString().Trim();
                Session["UserCode"] = Reader[0].ToString().Trim();
                Session["EditId"] = Reader[0].ToString().Trim();
                Session["CompanyId"] = Reader[3].ToString().Trim();
                Reader.Close();
                Session["CountryId"] = "966";
                
                Session["AdminUserName"] = "1";
                GetCustomization();
                Response.Redirect("default.aspx");

            }
            else
            {
                Reader.Close();
                LblMessage.Text = "Permission denied";

            }
            MyCon.Close();

        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    void GetCustomization()
    {
        Class1 queryResult = new Class1();
        try
        {

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
    public void Connect()
    {
        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString);
    }
    private void getMenu()
    {
        Connect();
        con.Open();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sql = "Select distinct MenuID,MenuNameAr,MenuNameEn,Action,ParentID from View_Menus where  UserRoleId='" + Session["UserRoleId1"].ToString() + "' and MenuStatusId ='1' order by MenuId";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(ds);
        dt = ds.Tables[0];
        DataRow[] drowpar = dt.Select("ParentID=" + 0);
        if (Session["dir"].ToString() == "ltr")
        {
            foreach (DataRow dr in drowpar)
            {
                Menu1.Items.Add(new MenuItem(dr["MenuNameEn"].ToString(), dr["MenuID"].ToString(),
                "", dr["Action"].ToString()));
            }

            foreach (DataRow dr in dt.Select("ParentID >" + 0))
            {
                MenuItem mnu = new MenuItem(dr["MenuNameEn"].ToString(), dr["MenuID"].ToString(),
                "", dr["Action"].ToString());
                Menu1.FindItem(dr["ParentID"].ToString()).ChildItems.Add(mnu);
                //MenuID,MenuNameAr,MenuNameEn,Action
            }
        }
        else
        {
            foreach (DataRow dr in drowpar)
            {
                Menu1.Items.Add(new MenuItem(dr["MenuNameAr"].ToString(), dr["MenuID"].ToString(),
                "", dr["Action"].ToString()));
            }

            foreach (DataRow dr in dt.Select("ParentID >" + 0))
            {
                MenuItem mnu = new MenuItem(dr["MenuNameAr"].ToString(), dr["MenuID"].ToString(),
                "", dr["Action"].ToString());
                Menu1.FindItem(dr["ParentID"].ToString()).ChildItems.Add(mnu);
            }
        }
       
        con.Close();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session["dir"] = "ltr";
        Response.Redirect("Default.aspx");
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Session["dir"] = "rtl";
        Response.Redirect("Default.aspx");
    }
}
