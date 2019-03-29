
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



public partial class Suppliers : System.Web.UI.Page
{

    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
                LabelCompanyName.Text = "اسم المورد";
            }
            else
            {
                LabelCompanyName.Text = "اسم المورد";
                /*LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);*/// GetLocalResourceObject("CompanyName").ToString();

            }

            Session["EditId"] = "0";

            SqlDataSource1.DataBind();
            //Session["CompanyId"] = "24";
            Session["CustomerId"] = "0";
            ViewState["EditId"] = "0";

            Session["AdminUserName"] = "1";
        }
    }




    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //Session["CompanyId"] = "24";
        try
        {
            Session["AccFrom"] = "S";
            accountantCls.GetLevelChartIDLevel5();

            Session["Cut"] = _CustomerNameText.Text;
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandText = "USP_Customer_Save_Supplier";
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



            //accountantCls.Cmd.Parameters.Add("CustomerId", SqlDbType.Int).Value = 0;
            //accountantCls.Cmd.Parameters.Add("CustomerName", SqlDbType.NVarChar).Value = _CustomerNameText.Text;
            //accountantCls.Cmd.Parameters.Add("CUSTypeOfBusiness", SqlDbType.Int).Value = 0;
            //accountantCls.Cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = 966;
            //accountantCls.Cmd.Parameters.Add("CityId", SqlDbType.Int).Value = 0;
            //accountantCls.Cmd.Parameters.Add("CUSIDType", SqlDbType.Int).Value = 0;
            //accountantCls.Cmd.Parameters.Add("CUSIdNumber", SqlDbType.NVarChar).Value = 0;
            //accountantCls.Cmd.Parameters.Add("CUSEmail", SqlDbType.NVarChar).Value = _EmailText.Text;
            //accountantCls.Cmd.Parameters.Add("CUSContactNo", SqlDbType.NVarChar).Value = _MobileText.Text;
            //accountantCls.Cmd.Parameters.Add("CUSFaxNo", SqlDbType.NVarChar).Value = "-";
            //accountantCls.Cmd.Parameters.Add("CUSAddress", SqlDbType.NVarChar).Value = "-";
            //accountantCls.Cmd.Parameters.Add("Password", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
            //accountantCls.Cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            //accountantCls.Cmd.Parameters.Add("LedgerId", SqlDbType.Int).Value = 0;

            //accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
            //accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());


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
            //_CustCityInfo.SelectedValue = MyDv[0].Row["CityId"].ToString().Trim();
            //_CUSIDType.SelectedValue = MyDv[0].Row["IDType"].ToString().Trim();
            //CUSIdNumber.Text = MyDv[0].Row["IdNumber"].ToString().Trim();
            //_CUSShortNameText.Text = MyDv[0].Row["CUSShortName"].ToString().Trim();
            //_CUSAddress.Text = MyDv[0].Row["Address"].ToString().Trim();
            _EmailText.Text = MyDv[0].Row["Email"].ToString().Trim();
            _EmailConfirmText.Text = MyDv[0].Row["Email"].ToString().Trim();
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
            //BtnChangeStatus.Visible = true;
            //BtnUpdate.Visible = true;
            //accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Suppliers.aspx");
    }
}