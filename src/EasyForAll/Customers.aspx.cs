
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



public partial class Customers : System.Web.UI.Page
{
    static public string Level1
    {
        get; set;
    }
    static public string Level2
    {
        get; set;
    }
    static public string Level3
    {
        get; set;
    }
    static public string Level4
    {
        get; set;
    }
    static public string Level5
    {
        get; set;
    }
    static public string Level6
    {
        get; set;
    }
    static public string Level7
    {
        get; set;
    }
    static public string Level8
    {
        get; set;
    }
    static public string Level9
    {
        get; set;
    }
    static public int Level
    {
        get; set;
    }
    static public int AccountType
    {
        get; set;
    }
    static public string LastLevel
    {
        get; set;
    }
    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["dir"] == null)
            {
                Session["dir"] = "rtl";
            }
            if (Session["dir"].ToString() == "ltr")
            {
                LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
                LabelCompanyName.Text = "اسم العميل";
            }
            else
            {
                LabelCompanyName.Text = "اسم العميل";
                /*LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);*/// GetLocalResourceObject("CompanyName").ToString();

            }
           
            Session["EditId"]="0";

            //SqlDataSource1.DataBind();
            Session["CompanyId"] = "7";
            Session["CustomerId"] = "0";
            ViewState["EditId"] = "0";
            
        }
    }
    


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //Session["CompanyId"] = "24";
        try
        {
            Session["AccFrom"] = "C";
            accountantCls.GetLevelChartIDLevel5();
            
            Session["Cut"]= _CustomerNameText.Text;
            //CreateAccountForCustomer();
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Customer_Save";
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.Parameters.Add("CustomerId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("CustomerName", SqlDbType.NVarChar).Value = Session["Cut"].ToString();
            accountantCls.Cmd.Parameters.Add("CUSTypeOfBusiness", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = 966;
            accountantCls.Cmd.Parameters.Add("CityId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("CUSIDType", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("CUSIdNumber", SqlDbType.NVarChar).Value = 0;
            accountantCls.Cmd.Parameters.Add("CUSEmail", SqlDbType.NVarChar).Value = _EmailText.Text;
            accountantCls.Cmd.Parameters.Add("CUSContactNo", SqlDbType.NVarChar).Value = _MobileText.Text;
            accountantCls.Cmd.Parameters.Add("CUSFaxNo", SqlDbType.NVarChar).Value = "-";
            accountantCls.Cmd.Parameters.Add("CUSAddress", SqlDbType.NVarChar).Value = "-";
            accountantCls.Cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
            accountantCls.Cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            accountantCls.Cmd.Parameters.Add("COAChartCode", SqlDbType.NVarChar).Value = Session["COAChartCode"].ToString();
            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;



            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            //accountantCls.Cmd.ExecuteNonQuery();

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
            else
            {
                Session["EditId"] = class1.RecordId;
                LabelErrorMessage.Text = "Successfully registered, you can add more information about your self or login to system";
                BtnClear.Visible = true;
                BtnSave.Visible = false;
            }
            accountantCls.commitTrans();

            //Session.Abandon();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;

        }
        finally
        {
            //SqlDataSource1.DataBind();

        }
    }


    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("signin.aspx");
    }
}