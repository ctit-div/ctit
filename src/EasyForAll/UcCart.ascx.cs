using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UcCart : System.Web.UI.UserControl
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
        if (Session["cart"] != null)
        {
            GridView1.DataSource = Session["cart"];
            GridView1.DataBind();

        }
        else
        {
            return;
        }

        int qty = 0;
        decimal amt = 0;
        decimal price = 0;

        //int price = 0, amt = 0, qty = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label Code = (Label)row.FindControl("lblProductCode");
            Session["ProCode"] = Code.Text;
            Label t1 = (Label)row.FindControl("lblPrice");

            price = decimal.Parse(t1.Text);


            TextBox t2 = (TextBox)row.FindControl("txtQty0");
            qty = int.Parse(t2.Text);
            Session["Qty"] = qty;
            amt = (price * qty) + amt;


        }
        Session.Remove("ProCode");
        Session.Remove("Qty");
        txtTotal.Text = amt.ToString();


    }
    void Bind()
    {
        if (Session["cart"] != null)
        {
            GridView1.DataSource = Session["cart"];
            GridView1.DataBind();

        }
        else
        {
            return;
        }
        int qty = 0;
        decimal amt = 0;
        decimal price = 0;
        //int price = 0, amt = 0, qty = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label Code = (Label)row.FindControl("lblProductCode");
            Session["ProCode"] = Code.Text;
            Label t1 = (Label)row.FindControl("lblPrice");
            price = decimal.Parse(t1.Text);
            TextBox t2 = (TextBox)row.FindControl("txtQty0");
            qty = int.Parse(t2.Text);
            Session["Qty"] = qty;
            amt = (price * qty) + amt;

        }
        Session.Remove("ProCode");
        Session.Remove("Qty");
        txtTotal.Text = amt.ToString();


    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        int qty = 0;
        decimal amt = 0;
        decimal price = 0;
        //int price = 0, amt = 0, qty = 0;
        Int32 OrderCode = 0;      string invNo = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label Code = (Label)row.FindControl("lblProductCode");
            Session["ProCode"] = Code.Text;
            Label t1 = (Label)row.FindControl("lblPrice");

            price = decimal.Parse(t1.Text);


            TextBox t2 = (TextBox)row.FindControl("txtQty0");
            qty = int.Parse(t2.Text);
            Session["Qty"] = qty;
            amt = (price * qty) + amt;
            System.Data.SqlClient.SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();

            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
  
            if (Session["OrderCode"] == null)
            {
                //QuotationId




        

                invNo = new Class_ImageResize().GetInvoiceNo(Session["CompanyId"].ToString(), "QuotationId", "tQuotations");
                MyCmd.CommandText = "insert into tQuotations (CustomerId,CreatedBy,InvoiceNo,InvoiceType,InvoiceTotal) values('" + Session["UserCode"].ToString() + "','" + Session["UserCode"].ToString() + "','" + invNo + "','0' , '" + txtTotal.Text + "')";
                MyCmd.ExecuteNonQuery();


                string sSQL2 = "SELECT SCOPE_IDENTITY()";
                MyCmd.CommandText = sSQL2;


                MyCmd.CommandType = CommandType.Text;
                MyCmd.Connection = MyCon;
                OrderCode = Convert.ToInt32(MyCmd.ExecuteScalar());
                Session["OrderCode1"] = invNo;
            }


            MyCmd.CommandText = "insert into tQuotationDetails (QuotationId,ItemId,Quantity,Price,InvoiceNo) values('" + OrderCode.ToString() + "', '" + Session["ProCode"].ToString() + "','" + Session["Qty"].ToString() + "','" + price.ToString() + "','" + invNo + "')";
            int i = MyCmd.ExecuteNonQuery();
            if (i > 0)
            {

                Session["OrderCode"] = "hh";
            }
            MyCon.Close();


            //Msg.Text = "Order is done";
        }
        Session.Remove("Cart");
        OrderCode = 0;
        Session.Remove("OrderCode");
        Session["Amount"] = txtTotal.Text;

        Response.Redirect("Checkout.aspx");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        tab = (DataTable)Session["cart"];

        tab.Clear();

        foreach (GridViewRow rows in GridView1.Rows)
        {
            CheckBox ck = (CheckBox)rows.FindControl("chk");
            if (ck.Checked == true)
            {
            }
            else
            {
                //DataColumn c1 = new DataColumn("ItemId");
                //DataColumn c2 = new DataColumn("Quantity");
                //DataColumn c3 = new DataColumn("Price");
                //DataColumn c4 = new DataColumn("ItemName");
                //DataColumn c5 = new DataColumn("ImageURL");
                Label s1 = (Label)rows.FindControl("lblProductCode");
                Label s2 = (Label)rows.FindControl("lblProductName");
                Label s3 = (Label)rows.FindControl("lblPrice");
                TextBox t = (TextBox)rows.FindControl("txtQty0");
                //Image ii = (Image)rows.FindControl("ImageURL");

                DataRow row = tab.NewRow();

                row["ItemId"] = s1.Text;
                row["ItemName"] = s2.Text;
                row["Price"] = s3.Text;
                row["Quantity"] = t.Text;
                //row["ImageURL"] = ii.ImageUrl;
                tab.Rows.Add(row);
            }
        }

        Session["cart"] = tab;
        Bind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        tab = (DataTable)Session["cart"];

        tab.Clear();

        foreach (GridViewRow rows in GridView1.Rows)
        {
            Label s1 = (Label)rows.FindControl("lblProductCode");
            Label s2 = (Label)rows.FindControl("lblProductName");
            Label s3 = (Label)rows.FindControl("lblPrice");
            TextBox t = (TextBox)rows.FindControl("txtQty0");
            //Image ii = (Image)rows.FindControl("ImageURL");

            DataRow row = tab.NewRow();

            row["ItemId"] = s1.Text;
            row["ItemName"] = s2.Text;
            row["Price"] = s3.Text;
            row["Quantity"] = t.Text;
            //row["ImageURL"] = ii.ImageUrl;
            tab.Rows.Add(row);

        }

        Session["cart"] = tab;
        Bind();
    }
}