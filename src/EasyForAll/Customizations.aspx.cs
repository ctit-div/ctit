using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Customizations : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    static public Int64 ChartId
    {
        get; set;
    }

    static public string COAChartName
    {
        get; set;
    }
    static public int CompanyID
    {
        get; set;
    }
    static public Int64 ParentChartID
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
    static public string Level1
    {
        get; set;
    }
    static public string Level2
    {
        get; set;
    }
    static public string Level3
    {
        get; set;
    }
    static public string Level4
    {
        get; set;
    }
    static public string Level5
    {
        get; set;
    }
    static public string Level6
    {
        get; set;
    }
    static public string Level7
    {
        get; set;
    }
    static public string Level8
    {
        get; set;
    }
    static public string Level9
    {
        get; set;
    }
    static public string k
    {
        get; set;
    }
    static public string COAChartCode
    {
        get; set;
    }
    static public string ChartCat
    {
        get; set;
    }
    static public string LastLevel
    {
        get; set;
    }
    static public Int64 COAChartId
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

            Session["LastAccount"] = "";
            Session["CustomizationId"] = 0;
            ViewState["EditId"] = "0";

            ChartId = 0;
            ParentChartID = 0;
            GetCustomization();
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
        }
    }
    void GetCustomization()
    {

        Class1 class1 = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Customization_SelById";
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.Parameters.Add("CompanyId", SqlDbType.Int).Value =int.Parse(Session["CompanyID"].ToString());
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();


            if (accountantCls.Reader.Read())
            {
                //TxtBankGroup.Text = accountantCls.Reader["BankGroup"].ToString();
                //TxtCashGroup.Text = accountantCls.Reader["CashGroup"].ToString();
                //TxtChequeGroup.Text = accountantCls.Reader["ChequeGroup"].ToString();
                //TxtProfitLossLedger.Text = accountantCls.Reader["ProfitLossLedger"].ToString();
                //TxtCustomerGroup.Text = accountantCls.Reader["CustomerGroup"].ToString();
                //TxtEmployeeGroup.Text = accountantCls.Reader["EmployeeGroup"].ToString();
                //TxtSupplierGroup.Text = accountantCls.Reader["SupplierGroup"].ToString();
                //RadioButtonListPostingType.SelectedValue = accountantCls.Reader["PostingType"].ToString();
            }
            accountantCls.Reader.Close();
            accountantCls.commitTrans();




        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;

        }
    }
    private void PopulateTreeView()
    {
        TreeView1.Nodes.Clear();

        Session["LastAccount"] = "";
        COAChartName = "";

        ChartId = 0;
        ParentChartID = 0;
        Level = 1;
        AccountType = 0;
        COAChartCode = "";
        COAChartId = 0;
        loadTreeMenu(TreeView1, LoadDataTable());
    }
    public DataTable LoadDataTable()
    {
        string DataBase = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;

        string query = "Select distinct * from tChartOfAccounts order by COAChartCode";
        DataTable dataTable = new DataTable();
        SqlDataAdapter dAdapter = new SqlDataAdapter(query, DataBase);
        dAdapter.Fill(dataTable);
        return dataTable;
    }
    public TreeView loadTreeMenu(TreeView tvMenu, DataTable dtMenu)
    {
        if (dtMenu.Rows.Count > 0)
        {
            foreach (DataRow menu in dtMenu.Select("ChartLevel='1'"))
            {
                TreeNode ParentNode = new TreeNode();
                ParentNode.Text = menu["COAChartName"].ToString() + ':' + menu["COAChartCode"].ToString();
                tvMenu.Nodes.Add(ParentNode);
                loadTreeSubMenu(ref ParentNode, menu["COAChartCode"].ToString(), dtMenu);
            }
        }
        return tvMenu;
    }
    private void loadTreeSubMenu(ref TreeNode ParentNode, string ParentId, DataTable dtMenu)
    {
        DataRow[] childs = dtMenu.Select("ParentChartID='" + ParentId + "'");
        foreach (DataRow dRow in childs)
        {
            TreeNode child = new TreeNode();
            child.Text = dRow["COAChartName"].ToString() + ':' + dRow["COAChartCode"].ToString();
            ParentNode.ChildNodes.Add(child);
            //Recursion Call
            loadTreeSubMenu(ref child, dRow["COAChartCode"].ToString(), dtMenu);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        LabelErrorMessage.Text = "";
        LblNodeText.Enabled = true;
        LblNodeValue.Enabled = true;
        LblNodeText.Text = "";
        LblNodeValue.Text = "";
        COAChartCode = "";
        ParentChartID = 0;
        string s = this.TreeView1.SelectedNode.Value;

        string[] separators = { ":" };

        string[] acc = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //LblNodeText.Text = acc[0].ToString();
        TxtAccountNameGroupEn.Text = "";
        TxtAccountNameGroupAr.Text = "";
        //COAChartCode = acc[1].ToString();
        LabelAccountNumberGroup.Text ="";
        LabelErrorMessage.Text = "";
        LblNodeValue.Text = acc[1].ToString();
        GetDetails();
        if (AccountType == 1)
        {

            LblNodeText.Text = acc[0].ToString();
            TxtAccountNameGroupAr.Text = acc[0].ToString();
            COAChartCode = acc[1].ToString();
            LabelAccountNumberGroup.Text = acc[1].ToString();
            LabelErrorMessage.Text = "";
            LblNodeValue.Text = acc[1].ToString();
            //if (Session["Account"].ToString() == "Bank")
            //{
            //    TxtBankGroup.Text = LblNodeValue.Text;
            //}
            //else if(Session["Account"].ToString() == "Cash")
            //{
            //    TxtCashGroup.Text = LblNodeValue.Text;
            //}
            //else if (Session["Account"].ToString() == "Cheque")
            //{
            //    TxtChequeGroup.Text = LblNodeValue.Text;
            //}
            //else if (Session["Account"].ToString() == "Customer")
            //{
            //    TxtCustomerGroup.Text = LblNodeValue.Text;
            //}
            //else if (Session["Account"].ToString() == "Employee")
            //{
            //    TxtEmployeeGroup.Text = LblNodeValue.Text;
            //}
            //else if (Session["Account"].ToString() == "Supplier")
            //{
            //    TxtSupplierGroup.Text = LblNodeValue.Text;
            //}
            //else if (Session["Account"].ToString() == "Profit")
            //{
            //    TxtProfitLossLedger.Text = LblNodeValue.Text;
            //}

            TreeView1.Visible = false;
            //LabelErrorMessage.Text = "This is last account!!!";
        }
        else
        {
            LblNodeText.Text = "This is not the last account!!!";
            LblNodeValue.Text = "";
            //LblNodeText.Text = "";
        }
       
       
    }
    void GetDetails()
    {

        Class1 class1 = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select * from tChartOfAccounts WHERE COAChartCode ='" + LblNodeValue.Text + "' ";

            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();


            if (accountantCls.Reader.Read())
            {
                //Level = Convert.ToInt32(accountantCls.Reader["ChartLevel"].ToString());
                //ChartId = Convert.ToInt32(accountantCls.Reader["ChartId"].ToString());
                AccountType = Convert.ToInt32(accountantCls.Reader["AccountType"].ToString());

            }
            accountantCls.Reader.Close();
            accountantCls.commitTrans();




        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;

        }
    }
    protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {


    }
    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        foreach (TreeNode node in TreeView1.Nodes)
        {
            //node.Checked = true;

        }
    }

    

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
        accountantCls.Cmd.CommandType = CommandType.StoredProcedure;

        try
        {

            accountantCls.Cmd.CommandText = "USP_Customizations_Save";
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.Parameters.Add("@CustomizationId", SqlDbType.Int).Value = Session["CustomizationId"].ToString();
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyID"].ToString();
            accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = Session["BranchId"].ToString();
            accountantCls.Cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = Session["DepartmentId"].ToString();
            accountantCls.Cmd.Parameters.Add("@DivisionId", SqlDbType.Int).Value = Session["DivisionId"].ToString();
            accountantCls.Cmd.Parameters.Add("@CustomizationNameAr", SqlDbType.NVarChar).Value = TxtAccountNameGroupAr.Text;
            accountantCls.Cmd.Parameters.Add("@CustomizationNameEn", SqlDbType.NVarChar).Value = TxtAccountNameGroupEn.Text;
            accountantCls.Cmd.Parameters.Add("@COAChartCode", SqlDbType.NVarChar).Value = LabelAccountNumberGroup.Text.Trim();
            accountantCls.Cmd.Parameters.Add("@OrderNo", SqlDbType.Int).Value = 0;
            //accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyID"].ToString();
            //accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyID"].ToString();
            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();



            class1.ErrorNo = 0;
            accountantCls.beginTrans();
            accountantCls.Cmd.ExecuteNonQuery();
            //class1.RecordId = Convert.ToInt32(
            if (class1.RecordId == 0)
            {
                class1.ErrorNo = -1;
                //class1.ErrorDesc = BOL.Resources.Messages.BranchAlreadyExists;
                //class1.ErrorFunction = " Finance.DAL.Customization.Save";
            }
            accountantCls.commitTrans();
            LabelErrorMessage.Text = "Succeeded";
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            LabelErrorMessage.Text = ex.Message.ToString();
            class1.ErrorNo = -1;
            //class1.ErrorFunction = " Finance.DAL.Customization.Save";
        }
        finally
        {
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customizations.aspx");
    }

    //protected void ImageButtonProfitLossLedger_Click(object sender, ImageClickEventArgs e)
    //{
    //    TreeView1.Visible = true;
    //    PopulateTreeView();
    //    TreeView1.CollapseAll();
    //    Session["Account"] = "ProfitLossLedger";

    //}

    protected void ImageButtonAccountGroup_Click(object sender, ImageClickEventArgs e)
    {
        TreeView1.Visible = true;
        PopulateTreeView();
        TreeView1.CollapseAll();
        //Session["Account"] = "Bank";
    }

    protected void GvCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtAccountNameGroupEn.Text = GvCompany.SelectedRow.Cells[2].Text;
        TxtAccountNameGroupAr.Text = GvCompany.SelectedRow.Cells[3].Text;
        LabelAccountNumberGroup.Text = GvCompany.SelectedRow.Cells[4].Text;
        Label lbCus = (Label)GvCompany.SelectedRow.FindControl("Label1");
        Session["CustomizationId"] = lbCus.Text.Trim();
    }
}