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

public partial class UcMakeOffer : System.Web.UI.UserControl
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
            ViewState["EditId"] = "0";
            
            //CreatedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //ModifiedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //Session["AdminUserName"] = "1";
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


    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        ItemGroupId = Convert.ToInt32(TreeView1.SelectedNode.Value);
   
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
        //if (currentNode.Text.ToUpper().Contains(TxtSearch.Text.ToUpper()))
        //{


            TreeView1.ExpandAll();
            currentNode.Expand();

            currentNode.ShowCheckBox = true;
            currentNode.Checked = true;
            currentNode.Select();
        //}
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


    protected void BtnClose_Click(object sender, EventArgs e)
    {
        //TxtSearch.Text = TreeView1.SelectedNode.Value + " - " + TreeView1.SelectedNode.Text;
        //ItemGroupId = Convert.ToInt32(TreeView1.SelectedNode.Value);

    }

    
    protected void BtnSearch_Click1(object sender, EventArgs e)
    {
        PopulateTreeView();
        TreeView1.CollapseAll();
        Session.Remove("Active");
        FindNodesByString();
        Session.Remove("Active");
    }
}
