
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

public partial class ItemsGroup : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    Class2 class2 = new Class2();
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
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["dir"].ToString() == "ltr")
            {
                LabelCategoryTitle.Text = String.Format(global::Resources.ResourceMain.CategoryTitle);
                LabelCategoryItem.Text = String.Format(global::Resources.ResourceMain.CategoryItem);
                BtnSearch.Text = String.Format(global::Resources.ResourceMain.BtnSearch);
                LblAlias.Text = String.Format(global::Resources.ResourceMain.LblAlias);
                LblGroup.Text = String.Format(global::Resources.ResourceMain.LblGroup);
                LabelUpload.Text = String.Format(global::Resources.ResourceMain.LabelUpload);
                

            }
            else
            {
                LabelCategoryTitle.Text = String.Format(global::Resources.ResourceMain_Ar.CategoryTitle);
                LabelCategoryItem.Text = String.Format(global::Resources.ResourceMain_Ar.CategoryItem);
                BtnSearch.Text = String.Format(global::Resources.ResourceMain_Ar.BtnSearch);
                LblAlias.Text = String.Format(global::Resources.ResourceMain_Ar.LblAlias);
                LblGroup.Text = String.Format(global::Resources.ResourceMain_Ar.LblGroup);
                LabelUpload.Text = String.Format(global::Resources.ResourceMain_Ar.LabelUpload);
                
            }


            ViewState["EditId"] = "0";
            
            
            ItemGroupId = 0;
            ParentGroupId = 0;
            PopulateTreeView();
            TreeView1.CollapseAll();
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
        }
    }

    private void PopulateTreeView()
    {


        Session["LastAccount"] = "";
        ItemGroupName = "";
        //CreatedBy = Convert.ToInt32(Session["UserCode"].ToString());
        //ModifiedBy = Convert.ToInt32(Session["UserCode"].ToString());
        //Session["AdminUserName"] = "1";
        ItemGroupId = 0;
        ParentGroupId = 0;
        Level = 0;
        AccountType = 0;
        ItemGroupCode = "";
        COAGrouptId = 0;

        DataTable treeViewData = GetTreeViewData();
        AddTopTreeViewNodes(treeViewData);
    }
    private DataTable GetTreeViewData()
    {
        string selectCommand = "SELECT * FROM tItemGroups";

        SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        DataTable dtblDiscuss = new DataTable();
        dad.Fill(dtblDiscuss);
        return dtblDiscuss;
    }
    private void AddTopTreeViewNodes(DataTable treeViewData)
    {
        TreeView1.Nodes.Clear();
       

        DataView view = new DataView(treeViewData);
        view.RowFilter = "ParentGroupId = 0";
        foreach (DataRowView row in view)
        {
           
                TreeNode newNode = new TreeNode(row["ItemGroupName"].ToString() + " - " + row["ItemGroupCode"].ToString(), row["ItemGroupId"].ToString());
                TreeView1.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
        }

    }
    private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
    {
      
        DataView view = new DataView(treeViewData);
        view.RowFilter = "ParentGroupId=" + parentTreeViewNode.Value;
        foreach (DataRowView row in view)
        {
            TreeNode newNode = new TreeNode(row["ItemGroupName"].ToString() + " - " + row["ItemGroupCode"].ToString(), row["ItemGroupId"].ToString());
            parentTreeViewNode.ChildNodes.Add(newNode);
            AddChildTreeViewNodes(treeViewData, newNode);

        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Clear();
        pnlTextBoxes.Visible = true;
        BtnSave.Visible = false;
        BtnAddGroup.Visible = true;
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
       
        string url = "";
        Class_ImageResize ClsImage = new Class_ImageResize();
        LabelErrorMessage.Text = "";
        if (UploadCategory.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {

            HttpFileCollection hfc = Request.Files;

            if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
            {
                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    url = ClsImage.imageresize(hpf);

                }
                LabelErrorMessage.Text += "<br>  Total <b>" + hfc.Count + "</b> file(s) ";
            }
            else
            {
                LabelErrorMessage.Text = "Max. 10 files allowed.";
            }
        }
        else
        {
            url = H1.NavigateUrl;
        }


        Class1 queryResult = new Class1();
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_ItemGroup_Update";
            accountantCls.Cmd.Parameters.Add("@ItemGroupId", SqlDbType.Int).Value = ItemGroupId;
          
            accountantCls.Cmd.Parameters.Add("@ItemGroupName", SqlDbType.NVarChar).Value = _ItemGroupNameText.Text;
           
           
            accountantCls.Cmd.Parameters.Add("@CategoryImage", SqlDbType.NVarChar).Value = url;
           
            queryResult.ErrorNo = 0;
            accountantCls.beginTrans();
            queryResult.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            if (queryResult.RecordId == 0)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc = Resources.ResourceMain.ChartAlreadyExists;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            else if (queryResult.RecordId == -1)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc = Resources.ResourceMain.EndOfLevel;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = "select ItemGroupCode From tItemGroups where ItemGroupId = " + queryResult.RecordId;
            queryResult.OtherData = accountantCls.Cmd.ExecuteScalar();
            accountantCls.commitTrans();
            LabelErrorMessage.Text = "Saved Successfully";
            TreeView1.ExpandAll();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;
            queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
        }
        PopulateTreeView();
        LblNodeValue.Text = "";


    }
  
    protected void GvCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        pnlTextBoxes.Visible = false;
        BtnDelete.Visible = false;
        BtnSave.Visible = true;
        Clear();
    }
    void Clear()
    {
        LabelErrorMessage.Text = "";
        
        BtnUpdate.Visible = false;
    
        BtnAddGroup.Visible = false;
       
        pnlTextBoxes.Visible = false;
        BtnDelete.Visible = false;
        BtnSave.Visible = true;
        _ItemGroupCodeText.Text = "";
        _ItemGroupNameText.Text = "";
        LabelErrorMessage.Text = "";
    }
    protected void BtnAddGroup_Click(object sender, EventArgs e)
    {
       
        string url="";
        Class_ImageResize ClsImage = new Class_ImageResize();
        LabelErrorMessage.Text = "";
        if (UploadCategory.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {

            HttpFileCollection hfc = Request.Files;

            if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
            {
                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    url = ClsImage.imageresize(hpf);

                }
                LabelErrorMessage.Text += "<br>  Total <b>" + hfc.Count + "</b> file(s) ";
            }
            else
            {
                LabelErrorMessage.Text = "Max. 10 files allowed.";
            }
        }
        else
        {
            url = "UploadedImages/no-image-available.png";
        }

        if (LblNodeValue.Text != "")
        {
            GetLevelParentChartID();
        }
        else
        {
            Level = 1;
            ParentGroupId = 0;
        }
        Class1 queryResult = new Class1();
        try
        {
            ItemGroupId = 0;
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_ItemGroup_Save";
            accountantCls.Cmd.Parameters.Add("@ItemGroupId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value =int.Parse(Session["CompanyId"].ToString());
            accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = Session["BranchId"];
            accountantCls.Cmd.Parameters.Add("@Level", SqlDbType.Int).Value =Level;
            accountantCls.Cmd.Parameters.Add("@ItemGroupName", SqlDbType.NVarChar).Value = _ItemGroupNameText.Text;
            accountantCls.Cmd.Parameters.Add("@ParentGroupId", SqlDbType.NVarChar).Value = ParentGroupId;
            accountantCls.Cmd.Parameters.Add("@ItemGroupCode", SqlDbType.NVarChar).Value = _ItemGroupCodeText.Text;
            accountantCls.Cmd.Parameters.Add("@CategoryImage", SqlDbType.NVarChar).Value = url;
            if (ItemGroupId == 0)
                accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            else
                accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = ModifiedBy;
            queryResult.ErrorNo = 0;
            accountantCls.beginTrans();
            queryResult.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteScalar());
            if (queryResult.RecordId == 0)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc = Resources.ResourceMain.ChartAlreadyExists;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            else if (queryResult.RecordId == -1)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc = Resources.ResourceMain.EndOfLevel;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = "select ItemGroupCode From tItemGroups where ItemGroupId = " + queryResult.RecordId;
            queryResult.OtherData = accountantCls.Cmd.ExecuteScalar();
            accountantCls.commitTrans();
            LabelErrorMessage.Text = "Saved Successfully";
            TreeView1.ExpandAll();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;
            queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
        }
        PopulateTreeView();
        LblNodeValue.Text = "";
    }
    
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        LabelErrorMessage.Text = "";
        pnlTextBoxes.Visible = true;
        LblNodeValue.Text = TreeView1.SelectedNode.Value;
        ItemGroupId = Convert.ToInt32(TreeView1.SelectedNode.Value);
        BtnUpdate.Visible = true;
        _ItemGroupNameText.Enabled = true;
        BtnAddGroup.Visible = false;
        BtnDelete.Visible = true;

        
        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select *  from tItemGroups WHERE ItemGroupId =" + ItemGroupId + "";
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {
                _ItemGroupCodeText.Text = accountantCls.Reader["ItemGroupCode"].ToString() ;

               _ItemGroupNameText.Text = accountantCls.Reader["ItemGroupName"].ToString() ;
                H1.NavigateUrl= accountantCls.Reader["CategoryImage"].ToString();
                ParentGroupId = 0;

                accountantCls.Reader.Close();
                accountantCls.commitTrans();
            }
            else
            {
                accountantCls.Reader.Close();
                accountantCls.commitTrans();
                //GetLevelChartID();
            }
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }

    }

    
    void GetLevelParentChartID()
    {
       
        _ItemGroupNameText.Enabled = true;
        BtnAddGroup.Visible = true;
        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select Level,ParentGroupId  from tItemGroups WHERE ItemGroupId =" + LblNodeValue.Text + "";
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {
               

                    Level = Convert.ToInt32(accountantCls.Reader["Level"].ToString()) + 1;
                    ParentGroupId = Convert.ToInt32(LblNodeValue.Text.Trim());

                accountantCls.Reader.Close();
                accountantCls.commitTrans();
            }
            else
            {
                accountantCls.Reader.Close();
                accountantCls.commitTrans();
                //GetLevelChartID();
            }
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }
    
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        PopulateTreeView();
        TreeView1.CollapseAll();
        Session.Remove("Active");
        FindNodesByString();
        Session.Remove("Active");
    }
    private void FindNodesByString()
    {
        foreach (TreeNode currentNode in TreeView1.Nodes)
        {
            Session["Active"] = "1";
            FindNodeByString(currentNode);


        }

       
    }


    private void FindNodeByString(TreeNode parentNode)
    {
        FindMatch(parentNode);
        foreach (TreeNode currentNode in parentNode.ChildNodes)
        {
            FindMatch(currentNode);
            FindNodeByString(currentNode);
        }
    }


    private void FindMatch(TreeNode currentNode)
    {
        if (currentNode.Text.ToUpper().Contains(TxtSearch.Text.ToUpper()))
        {


            TreeView1.ExpandAll();
            currentNode.Expand();

            currentNode.ShowCheckBox = true;
            currentNode.Checked = true;
            currentNode.Select();
        }
    }

    bool Block = false;
    protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (Block == true)  // stop recursive re-entrancy
            return;
        Block = true;


        TreeView1.CollapseAll();


        // expand current node and all parent nodes
        TreeNode Node = e.Node;
        while (Node != null)
        {
            Node.Expand();
            Node = Node.Parent;
        }


        Block = false;
    }



    protected void BtnDelete_Click(object sender, EventArgs e)
    {



        
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.Connection.Open();
           
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_ItemGroup_Del";
            accountantCls.Cmd.Parameters.Add("ItemGroupId", SqlDbType.Int).Value = ItemGroupId;
            accountantCls.Cmd.ExecuteNonQuery();
            
            LabelErrorMessage.Text = "Deleted Successfully";
            TreeView1.ExpandAll();
        }
        catch (Exception ex)
        {

            LabelErrorMessage.Text = ex.ToString();
        }
        PopulateTreeView();
        LblNodeValue.Text = "";
    }
}