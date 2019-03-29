

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



public partial class UsersRole : System.Web.UI.Page
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
            
            ViewState["EditId"] = "0";
            
           
           
        }
    }




    protected void BtnSave_Click(object sender, EventArgs e)
    {

        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_UserRole_Save";
            accountantCls.Cmd.Parameters.Add("@UserRoleId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString();
            accountantCls.Cmd.Parameters.Add("@UserRoleName", SqlDbType.NVarChar).Value =_UserRoleNameText.Text;
            accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _DescriptionText.Text;
            if (ViewState["EditId"].ToString() == "0")
                accountantCls.Cmd.Parameters.Add("@UserId ", SqlDbType.Int).Value = Session["UserCode"].ToString();
           
            else
                accountantCls.Cmd.Parameters.Add("@UserId ", SqlDbType.Int).Value = Session["UserCode"].ToString();
           
            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());


            if (class1.RecordId == 0)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc = Resources.ResourceMain.BranchAlreadyExists;

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
        Label lb = (Label)GvCompany.SelectedRow.FindControl("Label1");
        _UserRoleNameText.Text = GvCompany.SelectedRow.Cells[1].Text;
       _DescriptionText.Text = GvCompany.SelectedRow.Cells[2].Text;

        ViewState["EditId"] = lb.Text.Trim();

        class1.RecordId = int.Parse(ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "UserRoleId='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {

           
            BtnSave.Visible = false;
           
            BtnUpdate.Visible = true;
            
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("UsersRole.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_UserRole_Save";
            accountantCls.Cmd.Parameters.Add("@UserRoleId", SqlDbType.Int).Value = ViewState["EditId"];
            accountantCls.Cmd.Parameters.Add("@UserRoleName", SqlDbType.NVarChar).Value = _UserRoleNameText.Text;
            accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value =_DescriptionText.Text;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"].ToString();
                accountantCls.Cmd.Parameters.Add("@UserId ", SqlDbType.Int).Value = Session["UserCode"].ToString();

            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            int t = accountantCls.Cmd.ExecuteNonQuery();
            BtnSave.Visible = true;



            BtnUpdate.Visible = false;
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

    protected void GvCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Id"] = e.Keys[0].ToString();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.beginTrans();


            accountantCls.Cmd.CommandText = "delete from  tUserRoles  where  UserRoleId='" + Session["Id"].ToString() + "'";
            accountantCls.Cmd.ExecuteNonQuery();
            LabelErrorMessage.Text = "Successfully deleted";
            accountantCls.Conn.Close();
            accountantCls.commitTrans();
            accountantCls.Cmd.Parameters.Clear();

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
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
        catch (Exception ex)
        {

            LabelErrorMessage.Text = ex.Message.ToString();
        }
    }


}