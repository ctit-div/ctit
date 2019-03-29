
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



public partial class Currencies : System.Web.UI.Page
{

     AccountantCls accountantCls =new AccountantCls();
     Class1 class1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }

        if (!IsPostBack)
        {
            //BtnPrint.OnClientClick = "javascript:window.open('Print.aspx');";

            //Session["CompanyName"] = "Ararawi";
            //Session["CompanyId"] = "8";
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
            //_CMPCompanyNameText.Text = Session["CompanyName"].ToString();
            //_CMPCompanyNameText.Enabled = false;
        }
    }
   
    


    protected void BtnSave_Click(object sender, EventArgs e)
    {
      
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Currency_Save";
            accountantCls.Cmd.Parameters.Add("@CurrencyName", SqlDbType.NVarChar).Value = _CurrencyText.Text;
            accountantCls.Cmd.Parameters.Add("@CurrencySymbol", SqlDbType.NVarChar).Value =_CurrencySymbolText.Text;
            class1.ErrorNo = 0;
            accountantCls.beginTrans();
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
        Label lb = (Label)GvCompany.SelectedRow.FindControl("Label1");
        _CurrencyText.Text = GvCompany.SelectedRow.Cells[1].Text;
        _CurrencySymbolText.Text = GvCompany.SelectedRow.Cells[2].Text;

        ViewState["EditId"] = lb.Text.Trim();
       
        class1.RecordId =int.Parse( ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "CurrencyId='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            
            Session["Status"] = MyDv[0].Row["Status"].ToString().Trim();
            if (MyDv[0].Row["IsLocal"].ToString().Trim().ToLower()=="true")
            {
                CheckBox1.Checked =true;

            }
           else
            {
                CheckBox1.Checked = false;
            }
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
        Response.Redirect("Currencies.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Currency_Update";
            accountantCls.Cmd.Parameters.Add("@CurrencyId", SqlDbType.Int).Value = ViewState["EditId"];
            accountantCls.Cmd.Parameters.Add("@CurrencyName", SqlDbType.NVarChar).Value = _CurrencyText.Text;
            accountantCls.Cmd.Parameters.Add("@CurrencySymbol", SqlDbType.NVarChar).Value = _CurrencySymbolText.Text;
            accountantCls.Cmd.Parameters.Add("@IsLocal", SqlDbType.NVarChar).Value = CheckBox1.Checked;
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
    protected void BtnChangeStatusCancel_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Currency_Update_Cancel";
            accountantCls.Cmd.Parameters.Add("@CurrencyId", SqlDbType.Int).Value = ViewState["EditId"];
                accountantCls.Cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = false;
                LblStatus.Text = "Not Active";
                LblStatus.ForeColor = System.Drawing.Color.Red;
                Session["Status"] = false;
          



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
    protected void BtnChangeStatus_Click(object sender, EventArgs e)
    { 
        try
            {
                accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
                accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                accountantCls.Cmd.CommandText = "USP_Currency_Status";
                accountantCls.Cmd.Parameters.Add("@CurrencyId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString());
            //if (Session["IsLocal"].ToString() == "T")
            //{
            //    accountantCls.Cmd.Parameters.Add("@IsLocal", SqlDbType.Bit).Value = false;
            //    LblStatus.Text = "No";
            //    LblStatus.ForeColor = System.Drawing.Color.Red;
            //    Session["IsLocal"] = false;
            //}
            //else
            //{
            //    accountantCls.Cmd.Parameters.Add("@IsLocal", SqlDbType.Bit).Value = true;
            //    LblStatus.Text = "Yes";
            //    LblStatus.ForeColor = System.Drawing.Color.Green;
            //    Session["IsLocal"] = true;
            //}

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

    protected void GvCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Id"] = e.Keys[0].ToString();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.beginTrans();


            accountantCls.Cmd.CommandText = "delete from  tCurrency  where  CurrencyId='" + Session["Id"].ToString() + "'";
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

    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        Session["Print"] = "Currency";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Print.aspx');", true);
       
      
    }
}