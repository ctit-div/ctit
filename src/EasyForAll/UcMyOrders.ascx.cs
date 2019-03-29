
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UcMyOrders : System.Web.UI.UserControl
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (Session["dir"].ToString() == "ltr")
        {

            GridView1.Columns[1].HeaderText = String.Format(global::Resources.ResourceMain.InvoiceNo);
            GridView1.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain.InvoiceDate);
            GridView1.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain.InvoiceTotal);
            GridView1.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain.Enter);
            GridView1.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain.TransactionNo);
            GridView1.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain.Status);
        }
        else
        {

            GridView1.Columns[1].HeaderText = String.Format(global::Resources.ResourceMain_Ar.InvoiceNo);
            GridView1.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain_Ar.InvoiceDate);
            GridView1.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain_Ar.InvoiceTotal);
            GridView1.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Enter);
            GridView1.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain_Ar.TransactionNo);
            GridView1.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Status);


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

       

        double InvoiceTotal = 0;
     

        //int price = 0, amt = 0, InvoiceTotal = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label Code = (Label)row.FindControl("lblInvoiceNo");
            Session["InvoiceNo"] = Code.Text;
          


            TextBox t2 = (TextBox)row.FindControl("txtInvoiceTotal");
            InvoiceTotal = double.Parse(t2.Text);
            Session["InvoiceTotal"] = InvoiceTotal;
       


        }
        Session.Remove("InvoiceNo");
        Session.Remove("InvoiceTotal");


        

    }



    Class1 class1 = new Class1();
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        LabelErrorMessage.Text = "";
        foreach (GridViewRow rows in GridView1.Rows)
        {
            TextBox TxtTrans = (TextBox)rows.FindControl("TxtTransNo");
            TextBox TxtReqAmount = (TextBox)rows.FindControl("txtInvoiceTotal");
            TextBox TxtAmount = (TextBox)rows.FindControl("TxtAmount");
            Session["Amount"] = TxtAmount.Text;
            double Amount = 0;
            double Req = 0;
            if(TxtAmount.Text.Trim()=="")
            {
                TxtAmount.Text = "0";
            }
            if(TxtReqAmount.Text.Trim()=="")
            {
                TxtAmount.Text = "0";
            }
            Amount = double.Parse(TxtAmount.Text.Trim());
            Req = double.Parse(TxtReqAmount.Text.Trim());
            CheckBox ck = (CheckBox)rows.FindControl("ck");
            if (ck.Checked == true)
            {
                if (Amount !=Req && Req!=0)
                {
                    LabelErrorMessage.Text = "المبلغ المطلوب لا يساوي المدفوع ";
                    return;
                }

                if (TxtTrans.Text.Trim() == "")
                {
                    LabelErrorMessage.Text += " ادخل رقم عملية التحويل";
                    return;
                }

                //TextBox TxtTrans = (TextBox)rows.FindControl("TxtTransNo");
                //TextBox TxtAmount = (TextBox)rows.FindControl("TxtAmount");

                Session["TransactionNo"] = TxtTrans.Text;
                Session["Amount"] = TxtAmount.Text;

                Label lblInvoiceNo = (Label)rows.FindControl("lblInvoiceNo");
                Session["InvoiceNo"] = lblInvoiceNo.Text.Trim();
                //CheckBox ck = (CheckBox)rows.FindControl("ck");

                if (ck.Checked == true)
                {
                    Session["Note"] = "تم تحويل مبلغ : " + TxtAmount.Text + "   بعملية رقم:    " + TxtTrans.Text;
                    //SqlDataSource1.Update();

                    try
                    {
                        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
                        accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
                        accountantCls.Cmd.CommandText = "USP_Quotation_Edit_Invoice_Insert";

                        accountantCls.Cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar).Value = Session["InvoiceNo"].ToString();
                        accountantCls.Cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = Session["Note"];
                        //accountantCls.Cmd.Parameters.Add("@InvoiceStatusId", SqlDbType.Int).Value = "6";
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
        }
        Response.Redirect("DisplayMyOrderd.aspx");

   
       
    }
}