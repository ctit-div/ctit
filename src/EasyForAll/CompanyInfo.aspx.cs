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



public partial class CompanyInfo : System.Web.UI.Page
{

     AccountantCls accountantCls =new AccountantCls();
     Class1 class1 = new Class1();
     //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();
        


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] != null || Session["UserCode"].ToString() != string.Empty)
        {
            // let him access the page
        }
        else
        {
            //clear the cache and  redirect back to login page.
            Response.Redirect("SignIn.aspx");
        }
       
        if (!IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                LabelCompanyName.Text = String.Format(global::Resources.ResourceMain.CompanyName);
                //LabelCMPShortName.Text = String.Format(global::Resources.ResourceMain.CompanyShortName);
                LabelCMPAddress.Text = String.Format(global::Resources.ResourceMain.Address);
                LabelEmail.Text = String.Format(global::Resources.ResourceMain.Email);
                LabelMobile.Text = String.Format(global::Resources.ResourceMain.CompanyName);
                //LabelCMPAccountingMonth.Text = String.Format(global::Resources.ResourceMain.CompanyName);
                LabelPassword.Text = String.Format(global::Resources.ResourceMain.Password);
                GvCompany.Columns[2].HeaderText= String.Format(global::Resources.ResourceMain.CompanyId);
                GvCompany.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain.CompanyName);
                GvCompany.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain.CompanyShortName);
                GvCompany.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain.Address);
                GvCompany.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain.Email);
                GvCompany.Columns[7].HeaderText = String.Format(global::Resources.ResourceMain.Phone);

            }
            else
            {
                LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);
                //LabelCMPShortName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyShortName);
                LabelCMPAddress.Text = String.Format(global::Resources.ResourceMain_Ar.Address);
                LabelEmail.Text = String.Format(global::Resources.ResourceMain_Ar.Email);
                LabelMobile.Text = String.Format(global::Resources.ResourceMain_Ar.Phone);
                //LabelCMPAccountingMonth.Text = String.Format(global::Resources.ResourceMain_Ar.AccountingMonth);
                LabelPassword.Text = String.Format(global::Resources.ResourceMain_Ar.Password);
                GvCompany.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain_Ar.CompanyId);
                GvCompany.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain_Ar.CompanyName);
                GvCompany.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain_Ar.CompanyShortName);
                GvCompany.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Address);
                GvCompany.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Email);
                GvCompany.Columns[7].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Phone);
            }



            //SqlDataSource1.DataBind();
            //GvCompany.DataBind();
            //Session["CompanyId"] = "0";
            ViewState["EditId"] = "0";
            
            //Session["AdminUserName"] = "1";
        }
    }
   
    


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Company_Save";
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CMPCompanyName", SqlDbType.NVarChar).Value = _CMPCompanyNameText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPShortName", SqlDbType.NVarChar).Value = _CMPCompanyNameText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPAddress", SqlDbType.NText).Value = _CMPAddressText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPEmail", SqlDbType.NVarChar).Value = _EmailText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPPhone", SqlDbType.VarChar).Value = _MobileText.Text;
            accountantCls.Cmd.Parameters.Add("@CUSFaxNo", SqlDbType.VarChar).Value = "";
            accountantCls.Cmd.Parameters.Add("@CCMPAccountingMonth", SqlDbType.Int).Value =1;

            accountantCls.Cmd.Parameters.Add("@AdminUserName", SqlDbType.NVarChar).Value = Session["AdminUserName"].ToString();
            accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);

            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
            accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            //accountantCls.Cmd.ExecuteNonQuery();
            class1.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            ////update applications status - finance
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_CompanyApplication_Save";
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = class1.RecordId;
            accountantCls.Cmd.Parameters.Add("@ApplicationId", SqlDbType.Int).Value = 1;
            accountantCls.Cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 1;





             int t =  accountantCls.Cmd.ExecuteNonQuery();


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
        Session["CompanyID"] = ViewState["EditId"];
        class1.RecordId =int.Parse( ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "CompanyID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            _CMPCompanyNameText.Text = MyDv[0].Row["CMPCompanyName"].ToString();
            //_CMPShortNameText.Text = MyDv[0].Row["CMPShortName"].ToString();
            _CMPAddressText.Text = MyDv[0].Row["CMPAddress"].ToString();
            _EmailText.Text = MyDv[0].Row["CMPEmail"].ToString();
            _MobileText.Text = MyDv[0].Row["CMPPhone"].ToString();
            //_CMPAccountingMonthText.SelectedValue = MyDv[0].Row["CMPAccountingMonth"].ToString();
            _PasswordText.Text = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Decrypt(MyDv[0].Row["DefaultPassword"].ToString());
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
        Response.Redirect("CompanyInfo.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Company_Edit";
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value =int.Parse(ViewState["EditId"].ToString());
            accountantCls.Cmd.Parameters.Add("@CMPCompanyName", SqlDbType.NVarChar).Value = _CMPCompanyNameText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPShortName", SqlDbType.NVarChar).Value = _CMPCompanyNameText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPAddress", SqlDbType.NText).Value = _CMPAddressText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPEmail", SqlDbType.NVarChar).Value = _EmailText.Text;
            accountantCls.Cmd.Parameters.Add("@CMPPhone", SqlDbType.VarChar).Value = _MobileText.Text;
         
            accountantCls.Cmd.Parameters.Add("@CCMPAccountingMonth", SqlDbType.Int).Value = 1;
          
            accountantCls.Cmd.Parameters.Add("@AdminUserName", SqlDbType.NVarChar).Value = Session["AdminUserName"].ToString();
            accountantCls.Cmd.Parameters.Add("@DefaultPassword", SqlDbType.NVarChar).Value = Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption.Encrypt(_PasswordText.Text);
           
                accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
                accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
           
            class1.ErrorNo = 0;

            accountantCls.beginTrans();
            int t = 0;
            t = accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_CompanyApplication_Save";
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ViewState["EditId"].ToString();
            accountantCls.Cmd.Parameters.Add("@ApplicationId", SqlDbType.Int).Value = 1;
            accountantCls.Cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 1;
           t =  accountantCls.Cmd.ExecuteNonQuery();


            if (ViewState["EditId"].ToString() == "0")
            {
                class1.ErrorNo = -1;

                class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;
                SqlDataSource1.DataBind();
                GvCompany.DataBind();

            }
            else if (t== -1)
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
   
    protected void BtnChangeStatus_Click(object sender, EventArgs e)
    { 
        try
            {
                accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
                accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                accountantCls.Cmd.CommandText = "USP_Company_ChangeStatus";
                accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString());
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
                
                accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
                accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
              
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

    }

    protected void GvCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Id"] = e.Keys[0].ToString();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.beginTrans();


            accountantCls.Cmd.CommandText = "delete from  tCompanys  where  CompanyID='" + Session["Id"].ToString() + "'";
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
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
        catch (Exception ex)
        {
            
            LabelErrorMessage.Text = ex.Message.ToString();
        }
    }
}