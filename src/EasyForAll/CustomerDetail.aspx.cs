
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



public partial class CustomerDetail : System.Web.UI.Page
{

    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["dir"].ToString().Trim() == "ltr")
            {
                LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString().Trim();
                LabelCompanyName.Text = "اسم العميل";
            }
            else
            {
                LabelCompanyName.Text = "اسم العميل";
                /*LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);*/// GetLocalResourceObject("CompanyName").ToString().Trim();

            }



            SqlDataSource1.DataBind();
            GvCompany.DataBind();
            SqlDataSource2.DataBind();
            _CustCityInfo.DataBind();
            _CustCityInfo.Items.Insert(0, new ListItem("الرجاء اختيار", "0"));
            //Session["CompanyId"] = "7";
            Session["CustomerId"] = "0";
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
            Bind();
        }
    }



    void Bind()
    {
        ViewState["EditId"] = Session["EditId"];
        Session["CustomerId"] = ViewState["EditId"];
        class1.RecordId = int.Parse(ViewState["EditId"].ToString().Trim());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "UserId='" + ViewState["EditId"].ToString().Trim() + "' ";
        if (MyDv.Count > 0)
        {
            _CustomerNameText.Text = MyDv[0].Row["UserName"].ToString().Trim();
            _CustCityInfo.SelectedValue = MyDv[0].Row["CityId"].ToString().Trim();
            _CUSIDType.SelectedValue = MyDv[0].Row["IDType"].ToString().Trim();
            CUSIdNumber.Text = MyDv[0].Row["IdNumber"].ToString().Trim();
            //_CUSShortNameText.Text = MyDv[0].Row["CUSShortName"].ToString().Trim();
            _CUSAddress.Text = MyDv[0].Row["Address"].ToString().Trim();
            _EmailText.Text = MyDv[0].Row["Email"].ToString().Trim();
            _MobileText.Text = MyDv[0].Row["ContactNo"].ToString().Trim();
            //_CUSAccountingMonthText.SelectedValue = MyDv[0].Row["CUSAccountingMonth"].ToString().Trim();
            //_PasswordText.Text = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Decrypt(MyDv[0].Row["Password"].ToString().Trim());
            string st = "";
            st = MyDv[0].Row["Password"].ToString().Trim().Trim();
            st = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Decrypt(st);
            //_CUSAccountingMonthText.SelectedValue = MyDv[0].Row["CUSAccountingMonth"].ToString().Trim();
            _PasswordText.Text = st;
            ///* _PasswordText.Text*/ = MyDv[0].Row["Password"].ToString().Trim();
            _PasswordConfirmText.Text = _PasswordText.Text;

            Session["Status"] = MyDv[0].Row["IsActive"].ToString().Trim();
            if (Session["Status"].ToString().ToLower() == "true")
            {
                LblStatus.Text = "Active";
                LblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblStatus.Text = "Not Active";
                LblStatus.ForeColor = System.Drawing.Color.Red;

            }
            BtnSave.Visible = false;
            BtnChangeStatus.Visible = true;
            BtnUpdate.Visible = true;
            //accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Customer_Save";
            accountantCls.Cmd.Parameters.Add("CustomerId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("UserName", SqlDbType.NVarChar).Value = _CustomerNameText.Text;
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
            accountantCls.Cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString().Trim();
            accountantCls.Cmd.Parameters.Add("LedgerId", SqlDbType.Int).Value = 0;

            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString().Trim();
            accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            int t = accountantCls.Cmd.ExecuteNonQuery();


            if (class1.RecordId == 0)
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;

            }
            else if (t == -1)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc = Resources.ResourceMain.UserAlreadyExists;

            }
            else
            {
                LabelErrorMessage.Text = "Successfully registered, you can add more information about your self or login to system";
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
        ViewState["EditId"] = GvCompany.SelectedRow.Cells[2].Text.Trim();
        Session["CustomerId"] = ViewState["EditId"];
        class1.RecordId = int.Parse(ViewState["EditId"].ToString().Trim());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "UserId='" + ViewState["EditId"].ToString().Trim() + "' ";
        if (MyDv.Count > 0)
        {
            _CustomerNameText.Text = MyDv[0].Row["UserName"].ToString().Trim();
            _CustCityInfo.SelectedValue = MyDv[0].Row["CityId"].ToString().Trim();
            _CUSIDType.SelectedValue = MyDv[0].Row["IDType"].ToString().Trim();
            CUSIdNumber.Text = MyDv[0].Row["IdNumber"].ToString().Trim();
            //_CUSShortNameText.Text = MyDv[0].Row["CUSShortName"].ToString().Trim();
            _CUSAddress.Text = MyDv[0].Row["Address"].ToString().Trim();
            _EmailText.Text = MyDv[0].Row["Email"].ToString().Trim();
            _MobileText.Text = MyDv[0].Row["ContactNo"].ToString().Trim();
            //_CUSAccountingMonthText.SelectedValue = MyDv[0].Row["CUSAccountingMonth"].ToString().Trim();
            //_PasswordText.Text = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Decrypt(MyDv[0].Row["Password"].ToString().Trim());
            string st = "";
            st = MyDv[0].Row["Password"].ToString().Trim();
            st = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Decrypt(st);
            //_CUSAccountingMonthText.SelectedValue = MyDv[0].Row["CUSAccountingMonth"].ToString();
            _PasswordText.Text = st;
            ///* _PasswordText.Text*/ = MyDv[0].Row["Password"].ToString();
            _PasswordConfirmText.Text = _PasswordText.Text;
            Session["Status"] = MyDv[0].Row["IsActive"].ToString().Trim();
            if (Session["Status"].ToString().ToLower() == "true")
            {
                LblStatus.Text = "Active";
                LblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblStatus.Text = "Not Active";
                LblStatus.ForeColor = System.Drawing.Color.Red;

            }
            BtnSave.Visible = false;
            BtnChangeStatus.Visible = true;
            BtnUpdate.Visible = true;
            //accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerDetail.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Customer_Edit";
            accountantCls.Cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString().Trim());

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
            accountantCls.Cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString().Trim();
            accountantCls.Cmd.Parameters.Add("LedgerId", SqlDbType.Int).Value = 0;

            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString().Trim();
            accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            class1.ErrorNo = 0;

            accountantCls.beginTrans();
            int t = 0;
            t = accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.Cmd.Parameters.Clear();
       

            if (ViewState["EditId"].ToString().Trim() == "0")
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;
                SqlDataSource1.DataBind();
                GvCompany.DataBind();

            }
            else if (t == -1)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc = Resources.ResourceMain.UserAlreadyExists;

            }
            else
            {
                LabelErrorMessage.Text = "Successfully Edited";
            }
            accountantCls.commitTrans();
            //BtnSave.Visible = true;



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
            GvCompany.DataBind();
        }
    }

    protected void BtnChangeStatus_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Company_ChangeStatus";
            accountantCls.Cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString().Trim());
            if (Session["Status"].ToString().Trim() == "0")
            {
                accountantCls.Cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "1";
                LblStatus.Text = "Not Active";
                LblStatus.ForeColor = System.Drawing.Color.Red;
                Session["Status"] = "1";
            }
            else
            {
                accountantCls.Cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "0";
                LblStatus.Text = "Active";
                LblStatus.ForeColor = System.Drawing.Color.Green;
                Session["Status"] = "0";
            }

            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString().Trim();
            accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            class1.ErrorNo = 0;

            accountantCls.beginTrans();
            int t = accountantCls.Cmd.ExecuteNonQuery();


            if (ViewState["EditId"].ToString().Trim() == "0")
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;
                SqlDataSource1.DataBind();
                GvCompany.DataBind();

            }
            else if (t == -1)
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

    }

    protected void GvCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Id"] = e.Keys[0].ToString().Trim();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.beginTrans();


            accountantCls.Cmd.CommandText = "delete from  tUsers  where  UserId='" + Session["Id"].ToString().Trim() + "'";
            accountantCls.Cmd.ExecuteNonQuery();
            LabelErrorMessage.Text = "Successfully deleted";
            accountantCls.Conn.Close();
            accountantCls.commitTrans();
            accountantCls.Cmd.Parameters.Clear();
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            LabelErrorMessage.Text = ex.Message.ToString().Trim();
        }
    }

    protected void GvCompany_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
        catch (Exception ex)
        {

            LabelErrorMessage.Text = ex.Message.ToString().Trim();
        }
    }
}