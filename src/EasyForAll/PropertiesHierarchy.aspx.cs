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

public partial class PropertiesHierarchy : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    
    static public string CMPCompanyName
    {
        get; set;
    }
    static public int CompanyID
    {
        get; set;
    }
    static public Int64 CompanyIDParent
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
          

            ViewState["EditId"] = "0";
            BindTree();


        }
    }

    void CreateNode(TreeNode node)
    {
        DataSet ds = RunQuery("Select CompanyId, CMPCompanyName, CompanyIDParent  from tCompanys where CompanyIDParent =" + node.Value);
        if (ds.Tables[0].Rows.Count == 0)
        {
            return;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode tnode = new TreeNode(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString());
            //tnode.SelectAction = TreeNodeSelectAction.Expand;
            node.ChildNodes.Add(tnode);
            CreateNode(tnode);
        }

    }
    void BindTree()
    {
        TreeView1.Nodes.Clear();
        DataSet ds = RunQuery("Select CompanyId,CMPCompanyName ,CompanyIDParent   from tCompanys where CompanyIDParent=0  order by CompanyID");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode root = new TreeNode(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString());
            //root.SelectAction = TreeNodeSelectAction.Expand;
            CreateNode(root);
            TreeView1.Nodes.Add(root);
        }
    }
    DataSet RunQuery(String Query)
    {
        DataSet ds = new DataSet();
        String connStr = conString;//write your connection string here;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlCommand objCommand = new SqlCommand(Query, conn);
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            da.Fill(ds);
            da.Dispose();
        }
        return ds;
    }
    
    
    void GetDetails()
    {

        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select * from tCompanys WHERE CompanyID ='" + CompanyID + "' ";

            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();


            if (accountantCls.Reader.Read())
            {
                
                LblNodeText.Text = accountantCls.Reader["CMPCompanyName"].ToString();
                pnlTextBoxes.Visible = true;
                LblNodeValue.Text = accountantCls.Reader["CompanyID"].ToString();
                CompanyIDParent = Convert.ToInt64(accountantCls.Reader["CompanyID"].ToString());

            }
            accountantCls.Reader.Close();
            accountantCls.commitTrans();




        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }
    void CheckForChild()
    {

        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select max(CompanyID) as CompanyID  from tCompanys WHERE CompanyIDParent ='" + LblNodeValue.Text + "' Group by ChartId,CompanyID   order by ChartId desc ";

            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();


            if (accountantCls.Reader.Read())
            {
                Session["HasChild"] = "Y";
                CompanyID =int.Parse( accountantCls.Reader["CompanyID"].ToString());
                LblNodeValue.Text = CompanyID.ToString();
                accountantCls.Reader.Close();
                accountantCls.commitTrans();
            }
            else
            {
                Session["HasChild"] = "N";
                accountantCls.Reader.Close();
                accountantCls.commitTrans();
            }

        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }
    void CheckForLastNode()
    {

        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select AccountType  from tCompany WHERE CompanyID ='" + LblNodeText.Text + "' ";
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {
            }

            accountantCls.Reader.Close();
            accountantCls.commitTrans();

        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PropertiesHierarchy.aspx");


    }
    protected void BtnAddGroup_Click(object sender, EventArgs e)
    {
       
        Class1 queryResult = new Class1();
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_CompanyProperty_Save";
            accountantCls.Cmd.Parameters.Clear();
            accountantCls.Cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CMPCompanyName", SqlDbType.NVarChar).Value = _LedgerNameText.Text;
            accountantCls.Cmd.Parameters.Add("@CompanyIDParent", SqlDbType.NVarChar).Value = CompanyIDParent.ToString();
            accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
           
            queryResult.ErrorNo = 0;
            accountantCls.beginTrans();
            queryResult.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteNonQuery());
            accountantCls.commitTrans();
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
            LabelErrorMessage.Text = "Saved Successfully";
            Clear();

        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;
            queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
        }

    }
    void Clear()
    {
        LabelErrorMessage.Text = "";
        LblNodeText.Enabled = true;
        LblNodeValue.Enabled = true;
        //TxtNewAccountNo.Text = "";
        TxtSearch.Text = "";
        _LedgerAliasText.Text = "";
        _LedgerNameText.Text = "";

        LblNodeText.Text = "";
        LblNodeValue.Text = "";
        CompanyID = 0;
        CompanyIDParent = -1;
        pnlTextBoxes.Visible = false;
        //Response.Redirect("PropertiesHierarchy.aspx");
        //PopulateTreeView();
        BindTree();
    }
  
    
    void GetAccountNo()
    {
        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select Max(CompanyID) as CompanyID  from tCompanys WHERE CompanyIDParent ='0'";
            accountantCls.beginTrans();
            object o =accountantCls.Cmd.ExecuteScalar();
            if (o !=DBNull.Value)
            {

                CompanyID =int.Parse( o.ToString());
                

            }
            else
            {
                CompanyID = 0;
            }

            accountantCls.Reader.Close();
            accountantCls.commitTrans();


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
        //loadTreeMenu(TreeView1, LoadDataTable());
        Session["Active"] = "1";
        FindNodesByString();
        TreeNode searchNode = TreeView1.FindNode(TxtSearch.Text);
        if (searchNode != null)
            searchNode.Expand();
        LabelErrorMessage.Text = "Finished searching";

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("PropertiesHierarchy.aspx");
    }
    private void FindNodesByString()
    {
        foreach (TreeNode currentNode in TreeView1.Nodes)
        {
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
        string[] tokens = currentNode.Text.Split(':');
        if (tokens.Length > 1)
        {
            if (tokens[1].Contains(TxtSearch.Text.ToUpper()))
            {

                TreeView1.ExpandAll();
                //currentNode.Expand();

                currentNode.ShowCheckBox = true;
                currentNode.Checked = true;
                currentNode.Select();
                currentNode.Expand();


            }
            else if (tokens[0].Contains(TxtSearch.Text.ToUpper()))
            {

                //TreeView1.ExpandAll();
                currentNode.Expand();

                currentNode.ShowCheckBox = true;
                currentNode.Checked = true;
                currentNode.Select();

                currentNode.Expand();

            }
            else
            {

            }
        }
        else if (tokens.Length == 1)
        {
            if (tokens[0].Contains(TxtSearch.Text.ToUpper()))
            {

                //TreeView1.ExpandAll();
                currentNode.Expand();

                currentNode.ShowCheckBox = true;
                currentNode.Checked = true;
                currentNode.Select();



            }

            else
            {

            }
        }

    }

    
    
    protected void BtnSddMain_Click(object sender, EventArgs e)
    {
        CompanyIDParent = 0;
        //GetAccountNo();
        pnlTextBoxes.Visible = true;
    }

  

    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        foreach (TreeNode node in TreeView1.Nodes)
        {
            node.Checked = true;

        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        LabelErrorMessage.Text = "";
        LblNodeText.Enabled = true;
        LblNodeValue.Enabled = true;
        LblNodeText.Text = "";
        LblNodeValue.Text = "";
        CompanyID = 0;
        CompanyIDParent = 0;
        string s = this.TreeView1.SelectedNode.Value;

        //string[] separators = { ":" };

        //string[] acc = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //LblNodeText.Text = acc[0].ToString();

        CompanyID =int.Parse( s.ToString());

        LabelErrorMessage.Text = "";
        pnlTextBoxes.Visible = true;
        //LblNodeValue.Text = acc[1].ToString();
        TxtSearch.Text = LblNodeValue.Text;
        //CompanyIDParent = Convert.ToInt64(acc[1].ToString());
        GetDetails();
    }

    protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {

    }
}