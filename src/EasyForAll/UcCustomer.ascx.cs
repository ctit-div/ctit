


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

public partial class UcCustomer : System.Web.UI.UserControl
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

            //Session["CompanyId"] = "7";
            //Session["BranchId"] = "16";
            //ViewState["EditId"] = "0";
            
            //CreatedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //ModifiedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //Session["AdminUserName"] = "1";
            ItemGroupId = 0;
            FindNodesByString();



        }
    }


    protected void btn_Click(object sender, ImageClickEventArgs e)
    {

    }



    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbId = (Label)DataList1.SelectedItem.FindControl("ItemIdLabel");
        Session["ProductId"] = lbId.Text;
        Label lb = (Label)DataList1.SelectedItem.FindControl("ProductCodeLabel");
        Session["ProductCode"] = lb.Text;
        Label ProductName = (Label)DataList1.SelectedItem.FindControl("ProductNameLabel");
        Label Price = (Label)DataList1.SelectedItem.FindControl("PriceLabel");
        Label CategoryCodeLabel = (Label)DataList1.SelectedItem.FindControl("CategoryCodeLabel");
        Session["CategoryId"] = CategoryCodeLabel.Text.Trim();
        Label ProductNameEngLabel = (Label)DataList1.SelectedItem.FindControl("ProductNameEngLabel");
        Label AvaiableQuantity = (Label)DataList1.SelectedItem.FindControl("AvaiableQuantityLabel");
        TextBox Details = (TextBox)DataList1.SelectedItem.FindControl("Details");
        Label LeastQuantity = (Label)DataList1.SelectedItem.FindControl("LeastQuantity");
        Label OrderQuantity = (Label)DataList1.SelectedItem.FindControl("OrderQuantity");
        Label BarCodeLabel = (Label)DataList1.SelectedItem.FindControl("BarCodeLabel");
        Label UnitNameLabel = (Label)DataList1.SelectedItem.FindControl("UnitIdLabel");
        Label ActiveLabel0 = (Label)DataList1.SelectedItem.FindControl("ActiveLabel0");
        Label AgentCode = (Label)DataList1.SelectedItem.FindControl("AgentCodeLabel");
        System.Web.UI.WebControls.Image Im = (System.Web.UI.WebControls.Image)DataList1.SelectedItem.FindControl("picture");

    }



    private void FindNodesByString()
    {
        string ProductCode = "";

        //Assuming user comes back after several hours. several < 12.
        //Read the cookie from Request.
        HttpCookie myCookie = Request.Cookies["YourCookie"];
        if (myCookie == null)
        {
            return;
            //No cookie found or cookie expired.
            //Handle the situation here, Redirect the user or simply return;
        }

        //ok - cookie is found.
        //Gracefully check if the cookie has the key-value as expected.
        if (!string.IsNullOrEmpty(myCookie.Values["ItemCode"]))
        {
            ProductCode = myCookie.Values["ItemCode"].ToString();
            //Yes userId is found. Mission accomplished.
        }
        ProductCode = "(" + ProductCode + ")";
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            MyCmd.CommandText = "select top 10 * from View_Item_Display  where IsDisplayed='0' and ItemId in " + ProductCode + " order by ItemId desc";
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
