


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;

public partial class Signin : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    public SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session.Abandon();
        }
        if (Session["dir"] == null)
        {
            //Session["dir"] = "ltr";
            //ButtonLanguage.Text = "تغيير اللغة الى العربية";
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


            MyCmd.CommandText = "select * from View_Menus where Email='" + _EmailText.Text + "' and Password = '" + Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text) + "' and IsActive='1' and FinYearIDStatus='1'";
            SqlDataReader Reader = MyCmd.ExecuteReader();

            if (Reader.Read())
            {



                Session["Type"] = Reader["UserTypeCode"].ToString().Trim();
                Session["UserName"] = Reader["UserName"].ToString().Trim();
                Session["UserCode"] = Reader["UserId"].ToString().Trim();
                Session["EditId"] = Reader[0].ToString().Trim();
                Session["CompanyId"] = Reader["CompanyId"].ToString().Trim();
                Session["FinYearID"] = Reader["FinYearID"].ToString().Trim();
                //Session["DepartmentId"] = Reader["DepartmentId"].ToString().Trim();
                //Session["DivisionId"] = Reader["DivisionId"].ToString().Trim();
                Session["UserRoleId1"] = Reader["UserRoleId"].ToString().Trim();
                Session["CountryId"] = Reader["CountryId"].ToString().Trim();

                Reader.Close();
              
                Session["UserLog"] = "1";
                Session["AdminUserName"] = "1";
    
                //GetCustomization();
                if (Session["cart"] != null)
                {
                    Response.Redirect("Checkout.aspx");
                }
                else
                {
                    Response.Redirect("default.aspx");

                }
                    

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
    
    public void Connect()
    {
        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString);
    }



}