
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
using System.Web.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;

public partial class DisplayItemsByCategory   : System.Web.UI.Page
{


    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    static public Int64 ItemGroupId { get; set; }

    static public string ItemGroupName { get; set; }
    static public int CompanyID { get; set; }
    static public Int64 ParentGroupId { get; set; }
    static public int CreatedBy { get; set; }
    static public System.DateTime CreatedDate { get; set; }
    static public Nullable<int> ModifiedBy { get; set; }
    static public Nullable<System.DateTime> ModifiedDate { get; set; }
    static public int Level { get; set; }
    static public int AccountType { get; set; }

    static public string k { get; set; }
    static public string ItemGroupCode { get; set; }
    static public Int64 COAGrouptId { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserCode"] == null)
            {
                Response.Redirect("Signin.aspx");
            }

            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Session["Offer"] = "1";
              
            }
            else
            {
                Session.Remove("Offer");
               
            }
            ViewState["EditId"] = "0";
            
           
            ItemGroupId = 0;
            FindNodesByString();



        }
    }


    protected void btn_Click(object sender, ImageClickEventArgs e)
    {

    }
    DataTable tab = new DataTable();

    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbId = (Label)DataList1.SelectedItem.FindControl("ItemIdLabel");
        Session["ProductId"] = lbId.Text;
        Label lb = (Label)DataList1.SelectedItem.FindControl("ProductCodeLabel");
        Session["ProductCode"] = lb.Text;
        Label ProductName = (Label)DataList1.SelectedItem.FindControl("ProductNameLabel");
        Label Price = (Label)DataList1.SelectedItem.FindControl("PriceLabel");
        Label CategoryCodeLabel = (Label)DataList1.SelectedItem.FindControl("CategoryCodeLabel");
       
        System.Web.UI.WebControls.Image Im = (System.Web.UI.WebControls.Image)DataList1.SelectedItem.FindControl("picture");
        DataColumn c1 = new DataColumn("ItemId");
        DataColumn c2 = new DataColumn("Quantity");
        DataColumn c3 = new DataColumn("Price");
        DataColumn c4 = new DataColumn("ItemName");
        DataColumn c5 = new DataColumn("ImageURL");
        tab.Columns.Add(c1);
        tab.Columns.Add(c2);
        tab.Columns.Add(c3);
        tab.Columns.Add(c4);
        tab.Columns.Add(c5);
        if (Session["cart"] == null)
        {
            Label s1 = (Label)DataList1.SelectedItem.FindControl("ItemIdLabel");
            Label s2 = (Label)DataList1.SelectedItem.FindControl("ProductNameLabel");
            Label s3;
            if (Session["Offer"] != null)
            {
                if (Session["Offer"].ToString() == "1")
                {
                    s3 = (Label)DataList1.SelectedItem.FindControl("PriceLabel0");
                }
                else
                {
                    s3 = (Label)DataList1.SelectedItem.FindControl("PriceLabel");
                }
            }
            else
            {
                 s3 = (Label)DataList1.SelectedItem.FindControl("PriceLabel");
            }
            
            ImageButton b = (ImageButton)DataList1.SelectedItem.FindControl("btn");
            System.Web.UI.WebControls.Image ii = (System.Web.UI.WebControls.Image)DataList1.SelectedItem.FindControl("picture");
            b.Enabled = false;
            TextBox t = (TextBox)DataList1.SelectedItem.FindControl("txtQty");
            if (t.Text == "0" || t.Text == "")
            {
                return;
            }
            DataRow row = tab.NewRow();
            row["ItemId"] = s1.Text;
            row["ItemName"] = s2.Text;
            row["Price"] = s3.Text;
            row["Quantity"] = t.Text;
            row["ImageURL"] = ii.ImageUrl;
            t.BackColor = System.Drawing.Color.CadetBlue;
            tab.Rows.Add(row);

        }
        else
        {
            tab = (DataTable)Session["cart"];
            Label s1 = (Label)DataList1.SelectedItem.FindControl("ItemIdLabel");
            Label s2 = (Label)DataList1.SelectedItem.FindControl("ProductNameLabel");
            Label s3;
            if (Session["Offer"] != null)
            {
                if (Session["Offer"].ToString() == "1")
                {
                    s3 = (Label)DataList1.SelectedItem.FindControl("PriceLabel0");
                }
                else
                {
                    s3 = (Label)DataList1.SelectedItem.FindControl("PriceLabel");
                }
            }
            else
            {
                s3 = (Label)DataList1.SelectedItem.FindControl("PriceLabel");
            }
            System.Web.UI.WebControls.Image ii = (System.Web.UI.WebControls.Image)DataList1.SelectedItem.FindControl("picture");
            ImageButton b = (ImageButton)DataList1.SelectedItem.FindControl("btn");
            b.Enabled = false;
            TextBox t = (TextBox)DataList1.SelectedItem.FindControl("txtQty");
            if (t.Text == "0" || t.Text == "")
            {
                return;
            }
            DataRow row = tab.NewRow();
            row["ItemId"] = s1.Text;
            row["ItemName"] = s2.Text;
            row["Price"] = s3.Text;
            row["Quantity"] = t.Text;
            row["ImageURL"] = ii.ImageUrl;
            t.BackColor = System.Drawing.Color.CadetBlue;
            tab.Rows.Add(row);
        }

        Session["cart"] = tab;
        Session.Remove("Offer");
        Response.Redirect("DisplayItemsByCategory.aspx");
    }



    private void FindNodesByString()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Session["Offer"] = "1";
                // Query string value is there so now use it
                MyCmd.CommandText = "SELECT  * FROM View_Item_Display where   ItemId ='" + Request.QueryString["id"] + "' ";
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["CidAll"]))
            {
                MyCmd.CommandText = "SELECT  * FROM View_Item_Display where   ItemGroupId ='" + Request.QueryString["CidAll"] + "' ";
            }


            else
            {
                Session.Remove("Offer");
                MyCmd.CommandText = "SELECT  * FROM View_Item_Display where   ItemId ='" + Request.QueryString["Cid"] + "' ";
            }
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCmd);
            DataSet MyDateSet = new DataSet();
            MyAdapter.Fill(MyDateSet, "Category_Item");
            DataList1.DataSource = MyDateSet.Tables["Category_Item"];

            DataList1.DataBind();


            MyCon.Close();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }

    }





}
