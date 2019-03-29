
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

public partial class Divisions : System.Web.UI.Page
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
            _CMPlist.DataBind();
            _CMPlist.Items.Insert(0, new ListItem("Please Select", "0"));
            //Session["CompanyName"] = "Ararawi";
            //Session["CompanyId"] = "8";
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
            //_CMPCompanyNameText.Text = Session["CompanyName"].ToString();
            //_CMPCompanyNameText.Enabled = false;
        }
    }
    protected void _CMPlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CompanyId"] = _CMPlist.SelectedValue;
        SqlDataSourceBranch.DataBind();
        _BranchList.DataBind();
        _BranchList.Items.Insert(0, new ListItem("Please Select", "0"));

    }

     protected void BtnSave_Click(object sender, EventArgs e)
    {

        
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Division_Save";
            accountantCls.Cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value =_DepartmentList.SelectedValue;
            //accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = _CMPlist.SelectedValue;;
            accountantCls.Cmd.Parameters.Add("@DIVDivisionName", SqlDbType.NVarChar).Value = _DivisionNameText.Text;
            accountantCls.Cmd.Parameters.Add("@DIVDescription", SqlDbType.NVarChar).Value = _DIVDescriptionText.Text;
           
                accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
           
            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            //int t = accountantCls.Cmd.ExecuteNonQuery();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());

           
            if (class1.RecordId == 0)
            {
                class1.ErrorNo = -1;
                class1.ErrorDesc =Resources.ResourceMain.BranchAlreadyExists;
                
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
        Label lb=(Label)GvCompany.SelectedRow.FindControl("Label1");
        ViewState["EditId"] = lb.Text.Trim();
        Session["Department"] = ViewState["EditId"];
        class1.RecordId =int.Parse( ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "DivisionID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            _DIVDescriptionText.Text = MyDv[0].Row["DIVDescription"].ToString();
            _DivisionNameText.Text = MyDv[0].Row["DIVDivisionName"].ToString();
            _CMPlist.SelectedValue = MyDv[0].Row["CompanyId"].ToString();
            _CMPlist_SelectedIndexChanged(sender, e);
            _BranchList.SelectedValue = MyDv[0].Row["BranchID"].ToString();
       _BranchList_SelectedIndexChanged( sender,  e);
           _DepartmentList.SelectedValue = MyDv[0].Row["DepartmentID"].ToString();
     

            Session["Status"] = MyDv[0].Row["IsActive"].ToString().Trim();
            if (Session["Status"].ToString() == "True")
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
        Response.Redirect("Divisions.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Division_Edit";
            accountantCls.Cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = ViewState["EditId"];
            accountantCls.Cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = _DepartmentList.SelectedValue;
          
            accountantCls.Cmd.Parameters.Add("@DIVDivisionName", SqlDbType.NVarChar).Value = _DivisionNameText.Text;
            accountantCls.Cmd.Parameters.Add("@DIVDescription", SqlDbType.NVarChar).Value = _DIVDescriptionText.Text;

            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
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
   
    protected void BtnChangeStatus_Click(object sender, EventArgs e)
    { 
        try
            {
                accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
                accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                accountantCls.Cmd.CommandText = "USP_Division_ChangeStatus";
                accountantCls.Cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString());
                if (Session["Status"].ToString() == "True")
                {
                    accountantCls.Cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = false;
                    LblStatus.Text = "Not Active";
                    LblStatus.ForeColor = System.Drawing.Color.Red;
                    Session["Status"] = false;
                }
                else
                {
                    accountantCls.Cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = true;
                    LblStatus.Text = "Active";
                    LblStatus.ForeColor = System.Drawing.Color.Green;
                    Session["Status"] = true;
                }
                
             
              
                class1.ErrorNo = 0;

                accountantCls.beginTrans();
                int t = accountantCls.Cmd.ExecuteNonQuery();
               

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
    //protected void _CMPlist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Session["CompanyName"] = _CMPlist.SelectedValue;

    //}
    protected void _BranchList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["BranchId"] = _BranchList.SelectedValue;
        SqlDataSourceDepartment.DataBind();
        _DepartmentList.DataBind();
        _DepartmentList.Items.Insert(0, new ListItem("Please Select", "0"));

    }

    protected void _DepartmentList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
