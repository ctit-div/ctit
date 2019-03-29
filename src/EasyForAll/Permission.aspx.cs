

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Permission : System.Web.UI.Page
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
            loadTreeMenu(TreeView1, LoadDataTable());
            GV_Bind();
        }
    }

    public DataTable LoadDataTable()
    {
        string DataBase = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;

        string query = "Select * from tMenus where MenuStatusId=1  order by MenuId";
        DataTable dataTable = new DataTable();
        SqlDataAdapter dAdapter = new SqlDataAdapter(query, DataBase);
        dAdapter.Fill(dataTable);
        return dataTable;
    }

    public TreeView loadTreeMenu(TreeView tvMenu, DataTable dtMenu)
    {
        if (dtMenu.Rows.Count > 0)
        {
            foreach (DataRow menu in dtMenu.Select("ParentId='0'"))
            {
                TreeNode ParentNode = new TreeNode(menu["MenuNameAr"].ToString() + "  :  " + menu["MenuNameEn"].ToString(), menu["MenuId"].ToString());
                //ParentNode.Text = menu["MenuNameAr"].ToString() + "  :  " + menu["MenuNameEn"].ToString();
                tvMenu.Nodes.Add(ParentNode);
                loadTreeSubMenu(ref ParentNode, menu["MenuId"].ToString(), dtMenu);
            }
        }
        return tvMenu;
    }

    private void loadTreeSubMenu(ref TreeNode ParentNode, string ParentId, DataTable dtMenu)
    {
        DataRow[] childs = dtMenu.Select("ParentId='" + ParentId + "'");
        foreach (DataRow dRow in childs)
        {
            TreeNode child = new TreeNode(dRow["MenuNameAr"].ToString() + "  :  " + dRow["MenuNameEn"].ToString(), dRow["MenuId"].ToString());
            //child.Text = dRow["MenuNameAr"].ToString() + "  :  " + dRow["MenuNameEn"].ToString();
            ParentNode.ChildNodes.Add(child);
            //Recursion Call
            loadTreeSubMenu(ref child, dRow["MenuId"].ToString(), dtMenu);
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string s = this.TreeView1.SelectedNode.Value;

        string[] separators = { ":" };

        //string[] acc = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //TxtSearch.Text = acc[1].ToString().Trim();
        //VerifyChecked(acc[0].ToString());

    }


    private void CheckAllParentNodes(TreeNode treeNode, bool nodeChecked)
    {
        TreeNode parentNode = treeNode.Parent;
        while (parentNode != null)
        {
            // check if parent has still checked child nodes
            //if (parent.Nodes.Any(n => n.Checked)) return;

            //parentNode.Checked = nodeChecked;
            //parentNode = parentNode.Parent;
        }
    }

    private void VerifyChecked(string nodes)
    {
       
        TreeNode tn = new TreeNode();
        tn.Value =nodes;
        while (tn != null)
        {
            tn.Checked = tn.Text.Cast<TreeNode>()
               .Any(child => child.Checked);
            tn = tn.Parent;
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
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





    protected void GvCompany_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GV_Bind();
    }
    void GV_Bind()
    {
        DataView dv;
        dv = (DataView)SqlDataSourceUsersType.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0)
        {
            //dv.RowFilter = "UserRoleId='" + RadioButtonList1.SelectedValue + "'";
            GvCompany.DataSource = dv;
            GvCompany.DataBind();
        }
    }

    protected void GvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCompany.PageIndex = e.NewPageIndex;
        GV_Bind();
    }

    protected void ButtonLink_Click(object sender, EventArgs e)
    {
        //ArrayList al = new ArrayList();
        foreach ( GridViewRow R in GvCompany.Rows)
        {
            Session["UserRoleId "] = R.Cells[1].Text.Trim();
    
            RadioButton ch = (RadioButton)R.FindControl("C1");

            if (ch.Checked)
            {        SqlDataSource1.Delete();
                SqlDataSourceUsersType.Insert();
                foreach (TreeNode node in TreeView1.CheckedNodes)
                {

                    Session["MenuIdd"] = node.Value.Trim();
               
                    SqlDataSource1.Insert();

                }
              
            }
            LabelErrorMessage.Text = "تم حفظ البيانات";




        }
    }

    protected void GvCompany_SelectedIndexChanged(object sender, EventArgs e)
    {//SqlDataSource1
        foreach (TreeNode node in TreeView1.Nodes)
        {
           
                node.Checked = false;

            
        }
        Session["UserRoleId"] = GvCompany.SelectedRow.Cells[1].Text.Trim();
        SqlDataSource1.DataBind();
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        dv.RowFilter = "UserRoleId ='" + Session["UserRoleId"].ToString() + "' ";
        if(dv.Count>0)
        {


            foreach (TreeNode parent in TreeView1.Nodes)
            {
                foreach (TreeNode child in parent.ChildNodes)
                {
                    for (int j = 0; j < dv[0].DataView.Count; j++)
                    {
                        if (child.Value.Trim() == dv[0].DataView[j]["MenuId"].ToString().Trim())
                        {
                            child.Checked = true;
                            parent.Checked = true;
                            break;
                        }
                    }
                }
            }


        }
    }
}