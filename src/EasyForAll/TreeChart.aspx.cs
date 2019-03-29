
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TreeChart : System.Web.UI.Page
{

   
   
    static public int CreatedBy { get; set; }
    
    static public Nullable<int> ModifiedBy { get; set; }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            Session["LastAccount"] = "";
            //Session["CompanyId"] = "8";
            ViewState["EditId"] = "0";

            //CreatedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //ModifiedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //Session["AdminUserName"] = "1";
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
            //loadTreeMenu(TreeView1, LoadDataTable());
        }
    }

    public DataTable LoadDataTable()
    {
        string query = "";
           string DataBase = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
        
    DataTable dataTable = new DataTable();

        SqlDataAdapter dAdapter = new SqlDataAdapter();
        
        dataTable.Clear();


        string f = "";
        
        
        if (TxtSearch.Text != "")
        {
            f = TxtSearch.Text.Trim().Substring(0, 1);
            query = "Select * from tChartOfAccounts where COAChartCode like '" + f + "%' and CompanyId = '" + Session["CompanyId"].ToString() + "'  order by COAChartCode";
        }
        else if (TxtSearch.Text == "" && R1.SelectedIndex != -1)
        {
            f = R1.SelectedValue.Substring(0, 1);
            query = "Select * from tChartOfAccounts   where COAChartCode  like '" + f + "%' and CompanyId = '" + Session["CompanyId"].ToString() + "'  order by COAChartCode";
        }
        else if (TxtSearch.Text == "" && R1.SelectedIndex == -1)
        {
            query = "Select * from tChartOfAccounts  order by COAChartCode";
        }
        
         dataTable = new DataTable();
       
         dAdapter = new SqlDataAdapter(query, DataBase);
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
       string s = this.TreeView1.SelectedNode.Value;

        string[] separators = {":"};
        
        string[] acc = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        TxtSearch.Text = acc[1].ToString();

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        loadTreeMenu(TreeView1, LoadDataTable());
        FindNodesByString();
        LabelErrorMessage.Text = "Finished Searching";
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
        if(tokens.Length>1)
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
        else if (tokens.Length==1)
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





    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("TreeChart.aspx");
    }
}