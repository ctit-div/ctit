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



public partial class Cities : System.Web.UI.Page
{

    AccountantCls accountantCls = new AccountantCls();
    Class1 class1 = new Class1();
    //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                //LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);// GetLocalResourceObject("CompanyName").ToString();
            }
            else
            {
                //LabelCompanyName.Text = "اسم العميل";
                /*LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);*/// GetLocalResourceObject("CompanyName").ToString();

            }


            Session["CountryId"] = "966";
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
            //Session["CompanyId"] = "7";
          ;
            ViewState["EditId"] = "0";
            //Session["UserLog"] = "1";
            //Session["AdminUserName"] = "1";
        }
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_City_Save";
            accountantCls.Cmd.Parameters.Add("CityId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("CityName_Ar", SqlDbType.NVarChar).Value = _CityName_ArText.Text;
            accountantCls.Cmd.Parameters.Add("CityName_En", SqlDbType.NVarChar).Value = _CityName_ArText.Text;
            accountantCls.Cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = 966;
           

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
        Session["CityId"] = ViewState["EditId"];
        class1.RecordId = int.Parse(ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "CityId='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
           _CityName_ArText.Text = MyDv[0].Row["CityName_Ar"].ToString();
            
           
            BtnSave.Visible = false;
            BtnUpdate.Visible = true;
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cities.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_City_Edit";
            accountantCls.Cmd.Parameters.Add("@CityId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString());

            
            accountantCls.Cmd.Parameters.Add("CityName_Ar", SqlDbType.NVarChar).Value = _CityName_ArText.Text;
            accountantCls.Cmd.Parameters.Add("CityName_En", SqlDbType.NVarChar).Value = _CityName_ArText.Text;
            accountantCls.Cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = 966;

            class1.ErrorNo = 0;

            accountantCls.beginTrans();
            int t = 0;
            t = accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.Cmd.Parameters.Clear();


            if (ViewState["EditId"].ToString() == "0")
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
            BtnSave.Visible = true;



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

    protected void GvCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Id"] = e.Keys[0].ToString();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.beginTrans();


            accountantCls.Cmd.CommandText = "delete from  tCity where  CityId='" + Session["Id"].ToString() + "'";
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
            LabelErrorMessage.Text = ex.Message.ToString();
        }
    }

    protected void GvCompany_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            //SqlDataSource1.DataBind();
            //GvCompany.DataBind();
            //BtnClear_Click( sender,  e);
        }
        catch (Exception ex)
        {

            LabelErrorMessage.Text = ex.Message.ToString();
        }
    }
}