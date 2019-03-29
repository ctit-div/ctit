
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UcInvoiceConfirmation : System.Web.UI.UserControl
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }

        if (!IsPostBack)
        {
            BindContinue();
        }



    }

    DataTable tab = new DataTable();
    void BindContinue()
    {
        //if (Session["cart"] != null)
        //{
        DataView dv;
        dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

        GridView1.DataSource = dv;
        GridView1.DataBind();





    }

    Class1 class1 = new Class1();
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        LabelErrorMessage.Text = "";
        foreach (GridViewRow rows in GridView1.Rows)
        {
            
            CheckBox ck = (CheckBox)rows.FindControl("ck");
        

                Label lblInvoiceNo = (Label)rows.FindControl("lblInvoiceNo");
                Session["InvoiceNo"] = lblInvoiceNo.Text.Trim();
                if (ck.Checked == true)
                {

                    try
                    {
                        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
                        accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                        accountantCls.Cmd.CommandText = "USP_Quotation_Edit_Invoice_Edit";

                        accountantCls.Cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar).Value = Session["InvoiceNo"].ToString();
                        accountantCls.Cmd.Parameters.Add("@InvoiceStatusId", SqlDbType.Int).Value = "6";

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
                        LabelErrorMessage.Text += "تمت العملية بنجاح ";

                    }




                }
            }
        
        Response.Redirect("InvoiceConfirmation.aspx");



    }




    protected void btnArchive_Click(object sender, EventArgs e)
    {

        LabelErrorMessage.Text = "";
        foreach (GridViewRow rows in GridView1.Rows)
        {

            CheckBox ck = (CheckBox)rows.FindControl("ck");


            Label lblInvoiceNo = (Label)rows.FindControl("lblInvoiceNo");
            Session["InvoiceNo"] = lblInvoiceNo.Text.Trim();
            if (ck.Checked == true)
            {

                try
                {
                    accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
                    accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                    accountantCls.Cmd.CommandText = "USP_Quotation_Invoice_Archive";

                    accountantCls.Cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar).Value = Session["InvoiceNo"].ToString();
                    accountantCls.Cmd.Parameters.Add("@InvoiceStatusId", SqlDbType.Int).Value = "8";

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
                    LabelErrorMessage.Text += "تمت العملية بنجاح ";

                }




            }
        }

        Response.Redirect("InvoiceConfirmation.aspx");


    }
}