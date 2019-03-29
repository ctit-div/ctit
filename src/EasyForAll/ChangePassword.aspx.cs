
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



public partial class ChangePassword : System.Web.UI.Page
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
            if (Session["dir"].ToString() == "ltr")
            {
                LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
                LabelCompanyName.Text = "الاسم";
            }
            else
            {
                LabelCompanyName.Text = "الاسم ";
                /*LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);*/// GetLocalResourceObject("CompanyName").ToString();

            }
            SqlDataSource1.DataBind();
            BtnUpdate.Visible = true;
            SqlDataSource2.DataBind();
            _CustCityInfo.DataBind();
            _CustCityInfo.Items.Insert(0, new ListItem("الرجاء اختيار", "0"));
       
            Bind();
        }
    }



    void Bind()
    {
        ViewState["UserCode"] = Session["UserCode"];
        Session["CustomerId"] = ViewState["UserCode"];
        class1.RecordId = int.Parse(ViewState["UserCode"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "UserId='" + ViewState["UserCode"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            _CustomerNameText.Text = MyDv[0].Row["UserName"].ToString();
            _CustCityInfo.SelectedValue = MyDv[0].Row["CityId"].ToString();
            _CUSIDType.SelectedValue = MyDv[0].Row["IDType"].ToString();
            CUSIdNumber.Text = MyDv[0].Row["IdNumber"].ToString();

            _CUSAddress.Text = MyDv[0].Row["Address"].ToString();
            _EmailText.Text = MyDv[0].Row["Email"].ToString();
            _EmailConfirmText.Text = MyDv[0].Row["Email"].ToString();
            _MobileText.Text = MyDv[0].Row["ContactNo"].ToString();


            _CustCityInfo.SelectedValue = MyDv[0].Row["CityId"].ToString();
            _CUSIDType.SelectedValue = MyDv[0].Row["IDType"].ToString();
            string st = "";
            st = MyDv[0].Row["Password"].ToString().Trim();
            st = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Decrypt(st);
            //_CUSAccountingMonthText.SelectedValue = MyDv[0].Row["CUSAccountingMonth"].ToString();
            _PasswordText.Text = st;
            ///* _PasswordText.Text*/ = MyDv[0].Row["Password"].ToString();
            _PasswordConfirmText.Text = _PasswordText.Text;

            Session["Status"] = MyDv[0].Row["IsActive"].ToString().Trim();
            if (Session["Status"].ToString().ToLower() == "1")
            {
                LblStatus.Text = "Active";
                LblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblStatus.Text = "Not Active";
                LblStatus.ForeColor = System.Drawing.Color.Red;

            }

            BtnUpdate.Visible = true;
            //accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
        }
    }



    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Customer_Edit";
            accountantCls.Cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = int.Parse(ViewState["UserCode"].ToString());

            accountantCls.Cmd.Parameters.Add("CustomerName", SqlDbType.NVarChar).Value = _CustomerNameText.Text;
            accountantCls.Cmd.Parameters.Add("CUSTypeOfBusiness", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = 966;
            accountantCls.Cmd.Parameters.Add("CityId", SqlDbType.Int).Value = _CustCityInfo.SelectedValue;
            accountantCls.Cmd.Parameters.Add("CUSIDType", SqlDbType.Int).Value = _CUSIDType.SelectedValue;
            accountantCls.Cmd.Parameters.Add("CUSIdNumber", SqlDbType.NVarChar).Value = CUSIdNumber.Text;
            accountantCls.Cmd.Parameters.Add("CUSEmail", SqlDbType.NVarChar).Value = _EmailText.Text;
            accountantCls.Cmd.Parameters.Add("CUSContactNo", SqlDbType.NVarChar).Value = _MobileText.Text;
            accountantCls.Cmd.Parameters.Add("CUSFaxNo", SqlDbType.NVarChar).Value = "-";
            accountantCls.Cmd.Parameters.Add("CUSAddress", SqlDbType.NVarChar).Value = _CUSAddress.Text;
            accountantCls.Cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
            accountantCls.Cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            accountantCls.Cmd.Parameters.Add("LedgerId", SqlDbType.Int).Value = 0;

            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
            accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            class1.ErrorNo = 0;

            accountantCls.beginTrans();
            int t = 0;
            t = accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.Cmd.Parameters.Clear();


            if (ViewState["UserCode"].ToString() == "0")
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;
                SqlDataSource1.DataBind();

            }
            else if (t == -1)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc = Resources.ResourceMain.UserAlreadyExists;

            }
            accountantCls.commitTrans();




            BtnUpdate.Visible = false;

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

        }
    }


}