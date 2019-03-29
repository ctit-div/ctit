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

public partial class ItemsSummary : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    static public Int64 ItemGroupId
    {
        get; set;
    }

    static public string ItemGroupName
    {
        get; set;
    }
    static public int CompanyID
    {
        get; set;
    }
    static public Int64 ParentGroupId
    {
        get; set;
    }
    static public int CreatedBy
    {
        get; set;
    }
    static public System.DateTime CreatedDate
    {
        get; set;
    }
    static public Nullable<int> ModifiedBy
    {
        get; set;
    }
    static public Nullable<System.DateTime> ModifiedDate
    {
        get; set;
    }
    static public int Level
    {
        get; set;
    }
    static public int AccountType
    {
        get; set;
    }

    static public string k
    {
        get; set;
    }
    static public string ItemGroupCode
    {
        get; set;
    }
    static public Int64 COAGrouptId
    {
        get; set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                LabelItemsInfo.Text = String.Format(global::Resources.ResourceMain.LabelItemsInfo);
                LabelProductCode.Text = String.Format(global::Resources.ResourceMain.LabelProductCode);
                LabelActive.Text = String.Format(global::Resources.ResourceMain.LabelActive);
                LabelAvaiableQuantity.Text = String.Format(global::Resources.ResourceMain.LabelAvaiableQuantity);
                LabelCostPrice.Text = String.Format(global::Resources.ResourceMain.LabelCostPrice);
                LabelDescription.Text = String.Format(global::Resources.ResourceMain.LabelDescription);
                LabelSalePrice.Text = String.Format(global::Resources.ResourceMain.LabelSalePrice);

                LabelDisplayforCustomers.Text = String.Format(global::Resources.ResourceMain.LabelDisplayforCustomers);
                LabelLeastQuantity.Text = String.Format(global::Resources.ResourceMain.LabelLeastQuantity);
                LabelMinSalePrice.Text = String.Format(global::Resources.ResourceMain.LabelMinSalePrice);
                LabelUnitName.Text = String.Format(global::Resources.ResourceMain.LabelUnitName);


                LabelOfferPrice.Text = String.Format(global::Resources.ResourceMain.LabelOfferPrice);
                LabelOrderQuantity.Text = String.Format(global::Resources.ResourceMain.LabelOrderQuantity);
                LabelUploadImage.Text = String.Format(global::Resources.ResourceMain.LabelUploadImage);
                LabelVAT.Text = String.Format(global::Resources.ResourceMain.LabelVAT);

                LabelWholeSalePrice.Text = String.Format(global::Resources.ResourceMain.LabelWholeSalePrice);
                BtnNext.Text = String.Format(global::Resources.ResourceMain.BtnNext);
                ButtonPrevious.Text = String.Format(global::Resources.ResourceMain.ButtonPrevious);
                //GvCompany.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain.CompanyId);
                //GvCompany.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain.CompanyName);
                //GvCompany.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain.CompanyShortName);
                //GvCompany.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain.Address);
                //GvCompany.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain.Email);
                //GvCompany.Columns[7].HeaderText = String.Format(global::Resources.ResourceMain.Phone);

            }
            else
            {
                LabelItemsInfo.Text = String.Format(global::Resources.ResourceMain_Ar.LabelItemsInfo);
                LabelProductCode.Text = String.Format(global::Resources.ResourceMain_Ar.LabelProductCode);
                LabelActive.Text = String.Format(global::Resources.ResourceMain_Ar.LabelActive);
                LabelAvaiableQuantity.Text = String.Format(global::Resources.ResourceMain_Ar.LabelAvaiableQuantity);
                LabelCostPrice.Text = String.Format(global::Resources.ResourceMain_Ar.LabelCostPrice);
                LabelDescription.Text = String.Format(global::Resources.ResourceMain_Ar.LabelDescription);
                LabelSalePrice.Text = String.Format(global::Resources.ResourceMain_Ar.LabelSalePrice);

                LabelDisplayforCustomers.Text = String.Format(global::Resources.ResourceMain_Ar.LabelDisplayforCustomers);
                LabelLeastQuantity.Text = String.Format(global::Resources.ResourceMain_Ar.LabelLeastQuantity);
                LabelMinSalePrice.Text = String.Format(global::Resources.ResourceMain_Ar.LabelMinSalePrice);
                LabelUnitName.Text = String.Format(global::Resources.ResourceMain_Ar.LabelUnitName);


                LabelOfferPrice.Text = String.Format(global::Resources.ResourceMain_Ar.LabelOfferPrice);
                LabelOrderQuantity.Text = String.Format(global::Resources.ResourceMain_Ar.LabelOrderQuantity);
                LabelUploadImage.Text = String.Format(global::Resources.ResourceMain_Ar.LabelUploadImage);
                LabelVAT.Text = String.Format(global::Resources.ResourceMain_Ar.LabelVAT);

                LabelWholeSalePrice.Text = String.Format(global::Resources.ResourceMain_Ar.LabelWholeSalePrice);

                BtnNext.Text = String.Format(global::Resources.ResourceMain_Ar.BtnNext);
                ButtonPrevious.Text = String.Format(global::Resources.ResourceMain_Ar.ButtonPrevious);

                //LabelCompanyName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyName);
                //LabelCMPShortName.Text = String.Format(global::Resources.ResourceMain_Ar.CompanyShortName);
                //LabelCMPAddress.Text = String.Format(global::Resources.ResourceMain_Ar.Address);
                //LabelEmail.Text = String.Format(global::Resources.ResourceMain_Ar.Email);
                //LabelMobile.Text = String.Format(global::Resources.ResourceMain_Ar.Phone);
                //LabelCMPAccountingMonth.Text = String.Format(global::Resources.ResourceMain_Ar.AccountingMonth);
                //LabelPassword.Text = String.Format(global::Resources.ResourceMain_Ar.Password);
                //GvCompany.Columns[2].HeaderText = String.Format(global::Resources.ResourceMain_Ar.CompanyId);
                //GvCompany.Columns[3].HeaderText = String.Format(global::Resources.ResourceMain_Ar.CompanyName);
                //GvCompany.Columns[4].HeaderText = String.Format(global::Resources.ResourceMain_Ar.CompanyShortName);
                //GvCompany.Columns[5].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Address);
                //GvCompany.Columns[6].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Email);
                //GvCompany.Columns[7].HeaderText = String.Format(global::Resources.ResourceMain_Ar.Phone);
            }
            ViewState["EditId"] = "0";
           

            BindUnit();
            DropDownListUnitName.Items.Insert(0, new ListItem("Select Unit", "0"));

            DropDownListVATvalue.DataBind();
            DropDownListVATvalue.Items.Insert(0, new ListItem("Select VAT", "-1"));
        }
    }


    void BindUnit()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            MyCmd.CommandText = "select * from tUnits";
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCmd);
            DataSet MyDateSet = new DataSet();
            MyAdapter.Fill(MyDateSet, "tUnitsTable");
            DropDownListUnitName.DataSource = MyDateSet.Tables["tUnitsTable"];
            DropDownListUnitName.DataTextField = "UnitName";
            DropDownListUnitName.DataValueField = "UnitId";
            DropDownListUnitName.DataBind();


            MyCon.Close();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
  
    void BindProduct()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            MyCmd.CommandText = "select * from View_Category_Item where ItemCode=N'" + TxtProductCode.Text + "'";
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCmd);
            DataSet MyDateSet = new DataSet();
            MyAdapter.Fill(MyDateSet, "Product");
            DataList1.DataSource = MyDateSet.Tables["Product"];
            DataList1.DataBind();


            MyCon.Close();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemsSummary.aspx");
    }
    
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
        int RecordId = 0;
        try
        {
            
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Item_Pricing_Select_Summ";
            accountantCls.Cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = Session["ProductId"].ToString();
            accountantCls.beginTrans();
            RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            
            
        }
        catch (Exception ex)
        {
           
        }

        string url = "";
        Class_ImageResize ClsImage = new Class_ImageResize();
        LblMessage.Text = "";
        if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {

            HttpFileCollection hfc = Request.Files;

            if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
            {
                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    url = ClsImage.imageresize(hpf);

                }
                LblMessage.Text += "<br>  Total <b>" + hfc.Count + "</b> file(s) ";
            }
            else
            {
                LblMessage.Text = "Max. 10 files allowed.";
            }
        }
        else
        {
            url = H1.NavigateUrl;
        }

        try
        {

            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Item_Edit_Summ";
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = Session["ProductId"].ToString();

            accountantCls.Cmd.Parameters.Add("@ItemCode", SqlDbType.NVarChar).Value = TxtProductCode.Text;
            accountantCls.Cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar).Value = TxtProductName.Text;
            accountantCls.Cmd.Parameters.Add("@ProductNameEng", SqlDbType.NVarChar).Value = TxtProductNameEng.Text;
            accountantCls.Cmd.Parameters.Add("@BasicUnit", SqlDbType.Int).Value = DropDownListUnitName.SelectedValue;
            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            accountantCls.Cmd.Parameters.Add("@IsActive", SqlDbType.NChar).Value = RadioButtonListActive.SelectedValue;
            accountantCls.Cmd.Parameters.Add("@IsVAT", SqlDbType.NChar).Value = DropDownListVATvalue.SelectedValue;

            accountantCls.Cmd.Parameters.Add("@ImageURL", SqlDbType.NVarChar).Value = url;
            accountantCls.Cmd.Parameters.Add("@IsDisplayed", SqlDbType.NVarChar).Value = RadioButtonListIsDisplayed.SelectedValue;
            accountantCls.Cmd.Parameters.Add("@Details", SqlDbType.NVarChar).Value = TxtDetails.Text;
            accountantCls.Cmd.Parameters.Add("@OrderQuantity", SqlDbType.Int).Value = TxtOrderQuantity.Text;
            accountantCls.Cmd.Parameters.Add("@LeastQuantity", SqlDbType.Int).Value = TxtLeastQuantity.Text;
            


            accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.Cmd.Parameters.Clear();
            if (RecordId > 0)
            {

                accountantCls.Cmd.CommandText = "USP_Item_Pricing_Edit_Summ";
                accountantCls.Cmd.Parameters.Add("ItemId", SqlDbType.Int).Value = Session["ProductId"].ToString();
                accountantCls.Cmd.Parameters.Add("FillQuantity", SqlDbType.Decimal).Value = TxtAvaiableQuantity.Text;
                accountantCls.Cmd.Parameters.Add("CostPrice", SqlDbType.Decimal).Value = TxtPrice.Text;
                accountantCls.Cmd.Parameters.Add("SalePrice", SqlDbType.Decimal).Value = TxtSalePrice.Text;
                accountantCls.Cmd.Parameters.Add("MinSalePrice", SqlDbType.Decimal).Value = TxtMinSalePrice.Text;
                accountantCls.Cmd.Parameters.Add("WholeSalePrice", SqlDbType.Decimal).Value = TxtWholeSalePrice.Text;
                accountantCls.Cmd.Parameters.Add("OutSidePrice", SqlDbType.Decimal).Value = TxtOutSidePrice.Text;
                accountantCls.Cmd.Parameters.Add("OfferPrice", SqlDbType.Decimal).Value = TxtOfferPrice.Text;
            }
            else
            {
                accountantCls.Cmd.Parameters.Clear();
                accountantCls.Cmd.CommandText = "USP_Item_Pricing";
                accountantCls.Cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = Session["ProductId"].ToString();
                accountantCls.Cmd.Parameters.Add("@FillQuantity", SqlDbType.Int).Value = TxtAvaiableQuantity.Text;
                accountantCls.Cmd.Parameters.Add("@CostPrice", SqlDbType.Decimal).Value = TxtPrice.Text;
                accountantCls.Cmd.Parameters.Add("@AveragePrice", SqlDbType.Decimal).Value = 0;
                accountantCls.Cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = TxtSalePrice.Text;
                accountantCls.Cmd.Parameters.Add("@MinSalePrice", SqlDbType.Decimal).Value = TxtMinSalePrice.Text;
                accountantCls.Cmd.Parameters.Add("@WholeSalePrice", SqlDbType.Decimal).Value = TxtWholeSalePrice.Text;
                accountantCls.Cmd.Parameters.Add("@OutSidePrice", SqlDbType.Int).Value = TxtOutSidePrice.Text;
                accountantCls.Cmd.Parameters.Add("@BarCode", SqlDbType.NVarChar).Value = 0;
                accountantCls.Cmd.Parameters.Add("@OfferPrice", SqlDbType.Decimal).Value = TxtOfferPrice.Text;
            }
            accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.commitTrans();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
        }
        BindProduct();
    }
  
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbId = (Label)DataList1.SelectedItem.FindControl("ItemIdLabel");
        Session["ProductId"] = lbId.Text;
        Label lb = (Label)DataList1.SelectedItem.FindControl("ProductCodeLabel");
        Session["ProductCode"] = lb.Text;
        TxtProductCode.Text = lb.Text;
        Label ProductName = (Label)DataList1.SelectedItem.FindControl("ProductNameLabel");
        TxtProductName.Text = ProductName.Text;
        Session["ProductNameLabel"] = TxtProductName.Text;

        Label Price = (Label)DataList1.SelectedItem.FindControl("LabelCostPrice");
        TxtPrice.Text = Price.Text;
        Label CategoryCodeLabel = (Label)DataList1.SelectedItem.FindControl("CategoryCodeLabel");
        Session["CategoryId"] = CategoryCodeLabel.Text.Trim();

        //DropDownList1.SelectedValue = CategoryCodeLabel.Text;
        Label ProductNameEngLabel = (Label)DataList1.SelectedItem.FindControl("ProductNameEngLabel");
        TxtProductNameEng.Text = ProductNameEngLabel.Text;

        Label AvaiableQuantity = (Label)DataList1.SelectedItem.FindControl("AvaiableQuantityLabel");
        TxtAvaiableQuantity.Text = AvaiableQuantity.Text;
        Label ItemIdLabel = (Label)DataList1.SelectedItem.FindControl("ItemIdLabel");
        Session["ProductId"] = ItemIdLabel.Text;

        TextBox Details = (TextBox)DataList1.SelectedItem.FindControl("Details");
        TxtDetails.Text = Details.Text;

        Label LeastQuantity = (Label)DataList1.SelectedItem.FindControl("LeastQuantity");
        TxtLeastQuantity.Text = LeastQuantity.Text;
        Label OrderQuantity = (Label)DataList1.SelectedItem.FindControl("OrderQuantity");
        TxtOrderQuantity.Text = OrderQuantity.Text;
        
        Label UnitNameLabel = (Label)DataList1.SelectedItem.FindControl("UnitIdLabel");
        DropDownListUnitName.SelectedValue = UnitNameLabel.Text;
        Label ActiveLabel0 = (Label)DataList1.SelectedItem.FindControl("ActiveLabel0");
        RadioButtonListActive.SelectedValue = ActiveLabel0.Text;
      
   

        Label LabelSalePrice = (Label)DataList1.SelectedItem.FindControl("LabelSalePrice");
        TxtSalePrice.Text = LabelSalePrice.Text;
        Label LabelMinSalePrice = (Label)DataList1.SelectedItem.FindControl("LabelMinSalePrice");
        TxtMinSalePrice.Text = LabelMinSalePrice.Text;
        Label LabelWholeSalePrice = (Label)DataList1.SelectedItem.FindControl("LabelWholeSalePrice");
        TxtWholeSalePrice.Text = LabelWholeSalePrice.Text;
        Label LabelOfferPrice = (Label)DataList1.SelectedItem.FindControl("LabelOfferPrice");
        TxtOfferPrice.Text = LabelOfferPrice.Text;
        Label OutSidePriceLabel = (Label)DataList1.SelectedItem.FindControl("OutSidePriceLabel");
        TxtOutSidePrice.Text = OutSidePriceLabel.Text;


        System.Web.UI.WebControls.Image Im = (System.Web.UI.WebControls.Image)DataList1.SelectedItem.FindControl("picture");
        H1.NavigateUrl = Im.ImageUrl;
        if (Session["Type"].ToString() != "1")
        {
            BtnEdit.Visible = true;
        }
        else
        {
            BtnEdit.Visible = true;
        }


        BindSelectItem();
    }
    void BindSelectItem()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;


            MyCmd.CommandText = "select * from View_Category_Item where ItemCode=N'" + TxtProductCode.Text + "'";
            SqlDataReader Reader = MyCmd.ExecuteReader();

            if (Reader.Read())
            {
               
                RadioButtonListIsVAT.SelectedValue = Reader["VAT_Id"].ToString();
                DropDownListVATvalue.SelectedValue = Reader["VAT_Id"].ToString();
                RadioButtonListIsDisplayed.SelectedValue = Reader["IsDisplayed"].ToString();
                //TxtAvaiableQuantity.Text = Reader["AvaiableQuantity"].ToString();
                TxtPrice.Text = Reader["CostPrice"].ToString();
                TxtSalePrice.Text = Reader["SalePrice"].ToString();
                TxtMinSalePrice.Text = Reader["MinSalePrice"].ToString();
                TxtWholeSalePrice.Text = Reader["WholeSalePrice"].ToString();
                TxtOutSidePrice.Text = Reader["OutSidePrice"].ToString();
                TxtOfferPrice.Text = Reader["OfferPrice"].ToString();
            }
            Reader.Close();


            MyCon.Close();

        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProduct();
    }





    



    protected void RadioButtonListIsVAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListIsVAT.SelectedIndex == 0)
        {
            DropDownListVATvalue.Visible = false;
        }
        else
        {
            DropDownListVATvalue.Visible = true;
        }
    }
  

    protected void BtnSearchItem_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            MyCmd.CommandText = "select * from View_Category_Item where ItemCode like N'%" + TxtSearchItem.Text + "%' or ItemName  like N'%" + TxtSearchItem.Text + "%'  or ProductNameEng  like N'%" + TxtSearchItem.Text + "%'  or ItemGroupName  like N'%" + TxtSearchItem.Text + "%'";
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

    protected void BtnNext_Click(object sender, EventArgs e)
    {
        TxtSearchItem.Text = "";
        DataList1.DataSource = null;
        DataList1.DataBind();
        int ItemId;
        ItemId = int.Parse(Session["ProductId"].ToString());
        ItemId = ItemId + 1;

        Session["ProductId"] = ItemId.ToString();
        BindSelectItemNext();
    }

    protected void ButtonPrevious_Click(object sender, EventArgs e)
    {
        TxtSearchItem.Text = "";
        DataList1.DataSource = null;
        DataList1.DataBind();
        int ItemId;
        ItemId = int.Parse(Session["ProductId"].ToString());
        ItemId = ItemId -1;
        Session["ProductId"] = ItemId.ToString();
        BindSelectItemNext();
    }

    void BindSelectItemNext()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;


            MyCmd.CommandText = "select * from View_Category_Item where ItemId = N'" + Session["ProductId"].ToString() + "'";
            SqlDataReader Reader = MyCmd.ExecuteReader();

            if (Reader.Read())
            {

                RadioButtonListIsVAT.SelectedValue = Reader["VAT_Id"].ToString();
                DropDownListVATvalue.SelectedValue = Reader["VAT_Id"].ToString();
                RadioButtonListIsDisplayed.SelectedValue = Reader["IsDisplayed"].ToString();
                //TxtAvaiableQuantity.Text = Reader["AvaiableQuantity"].ToString();
                TxtPrice.Text = Reader["CostPrice"].ToString();
                TxtSalePrice.Text = Reader["SalePrice"].ToString();
                TxtMinSalePrice.Text = Reader["MinSalePrice"].ToString();
                TxtWholeSalePrice.Text = Reader["WholeSalePrice"].ToString();
                TxtOutSidePrice.Text = Reader["OutSidePrice"].ToString();
                TxtOfferPrice.Text = Reader["OfferPrice"].ToString();


                TxtProductCode.Text = Reader["ItemCode"].ToString();
                TxtProductName.Text = Reader["ItemName"].ToString();
                TxtProductNameEng.Text = Reader["ProductNameEng"].ToString();
                TxtAvaiableQuantity.Text = Reader["FillQuantity"].ToString();
                TxtDetails.Text = Reader["Details"].ToString();
                TxtLeastQuantity.Text = Reader["LeastQuantity"].ToString();
                TxtOrderQuantity.Text = Reader["OrderQuantity"].ToString();
                



            }
            Reader.Close();


            MyCon.Close();

        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
}
