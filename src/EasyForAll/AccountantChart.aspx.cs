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

public partial class AccountantChart : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 class1 = new Class1();
    static public Int64 ChartId { get; set; }

    static public string COAChartName { get; set; }
    static public int CompanyID { get; set; }
   static public Int64 ParentChartID { get; set; }
   static public int CreatedBy { get; set; }
   static public System.DateTime CreatedDate { get; set; }
   static public Nullable<int> ModifiedBy { get; set; }
   static public Nullable<System.DateTime> ModifiedDate { get; set; }
   static public int Level { get; set; }
   static public int AccountType { get; set; }
   static public string Level1 { get; set; }
   static public string Level2 { get; set; }
   static public string Level3 { get; set; }
   static public string Level4 { get; set; }
   static public string Level5 { get; set; }
   static public string Level6 { get; set; }
   static public string Level7 { get; set; }
   static public string Level8 { get; set; }
   static public string Level9 { get; set; }
   static public string k { get; set; }
   static public string COAChartCode { get; set; }
    static public string ChartCat { get; set; }
    static public string LastLevel { get; set; }
    static public Int64 COAChartId { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {
            
               Session["LastAccount"] = "";
            
            ViewState["EditId"] = "0";
          
            ChartId = 0;
            ParentChartID = 0;
            //PopulateTreeView();
            //TreeView1.CollapseAll();
            TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
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
        TreeView1.CollapseAll();
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
        LabelErrorMessage.Text = "";
           LblNodeText.Enabled=true;
        LblNodeValue.Enabled = true;
        LblNodeText.Text = "";
        LblNodeValue.Text = "";
       COAChartCode = "";
        ParentChartID = 0;
        Session["HasChild"] = "N";
        _Typelist.Enabled = true;
        string s = this.TreeView1.SelectedNode.Value;

        string[] separators = { ":" };

        string[] acc = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        LblNodeText.Text = acc[0].ToString();

        COAChartCode= acc[1].ToString();
       
        LabelErrorMessage.Text = "";
        pnlTextBoxes.Visible = true;
        LblNodeValue.Text =acc[1].ToString();
        LblAccount.Text = acc[0].ToString();
        TxtSearch.Text = LblNodeValue.Text;
        ParentChartID = Convert.ToInt64(acc[1].ToString());
        GetDetails();
        if (AccountType == 1)
        {
            BtnAddGroup.Visible = false;
            pnlTextBoxes.Visible = false;
            LabelErrorMessage.Text = "This is last account!!!";
        }
        else
        {
            BtnAddGroup.Visible = true;
            pnlTextBoxes.Visible = true;
        }
    }
    void GetDetails()
    {

        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select * from tChartOfAccounts WHERE COAChartCode ='" + LblNodeValue.Text + "' ";

            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();


            if (accountantCls.Reader.Read())
            {
                Level = Convert.ToInt32(accountantCls.Reader["ChartLevel"].ToString());
                ChartId= Convert.ToInt32(accountantCls.Reader["ChartId"].ToString());
                AccountType = Convert.ToInt32(accountantCls.Reader["AccountType"].ToString());

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
            accountantCls.Cmd.CommandText = " select max(ChartId) as ChartId,COAChartCode  from tChartOfAccounts WHERE ParentChartID ='" + LblNodeValue.Text + "' Group by ChartId,COAChartCode   order by ChartId desc ";
            
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();


            if (accountantCls.Reader.Read())
            {
                Session["HasChild"] = "Y";
                COAChartCode = accountantCls.Reader["COAChartCode"].ToString();
                LblNodeValue.Text = COAChartCode;
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
            accountantCls.Cmd.CommandText = " select AccountType  from tChartOfAccounts WHERE COAChartCode ='" + LblNodeText.Text + "' ";
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {
                    AccountType = Convert.ToInt32(accountantCls.Reader["AccountType"].ToString());
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
        Response.Redirect("AccountantChart.aspx");
       

    }
    protected void BtnAddGroup_Click(object sender, EventArgs e)
    {
        if (_Typelist.SelectedValue == "0")
        {
            LastLevel = "N";
        }
        else
        {
            LastLevel = "W";
        }
        Class1 queryResult = new Class1();
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_ChartOfAccount_Save";
            accountantCls.Cmd.Parameters.Add("@ChartId", SqlDbType.Int).Value = 0;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"];
            accountantCls.Cmd.Parameters.Add("@COAChartName", SqlDbType.NVarChar).Value =_LedgerNameText.Text;
            accountantCls.Cmd.Parameters.Add("@ParentChartID", SqlDbType.NVarChar).Value = ParentChartID.ToString();
            accountantCls.Cmd.Parameters.Add("@ChartLevel", SqlDbType.NVarChar).Value = Level;
            accountantCls.Cmd.Parameters.Add("@COAChartCode", SqlDbType.NVarChar).Value = COAChartCode.ToString();
            accountantCls.Cmd.Parameters.Add("@ChartCat", SqlDbType.NVarChar).Value = "T1";
            accountantCls.Cmd.Parameters.Add("@LastLevel", SqlDbType.NVarChar).Value = LastLevel;
            accountantCls.Cmd.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value = _Typelist.SelectedValue;
            if (ChartId == 0)
                accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            else
                accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            queryResult.ErrorNo = 0;
            accountantCls.beginTrans();
            queryResult.RecordId = Convert.ToInt32(accountantCls.Cmd.ExecuteNonQuery());
            accountantCls.commitTrans();
            if (queryResult.RecordId == 0)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc =Resources.ResourceMain.ChartAlreadyExists;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            else if (queryResult.RecordId == -1)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc =Resources.ResourceMain.EndOfLevel;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            //accountantCls.Cmd.CommandType = CommandType.Text;
            //accountantCls.Cmd.CommandText = "select COAChartCode From tChartOfAccounts where ChartId = " + queryResult.RecordId;
            //queryResult.OtherData = accountantCls.Cmd.ExecuteScalar();
           
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
        TxtNewAccountNo.Text = "";
        TxtSearch.Text = "";
        _LedgerAliasText.Text = "";
        _LedgerNameText.Text = "";

        LblNodeText.Text = "";
        LblNodeValue.Text = "";
        COAChartCode = "";
        ParentChartID = -1;
        _Typelist.SelectedIndex = 0;
        _Typelist.Enabled = true;
        pnlTextBoxes.Visible = false;
        PopulateTreeView();
    }
    protected void _Typelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtNewAccountNo.Text="";
        _Typelist.Enabled = false;
        LabelErrorMessage.Text = "";
        LblAlias.Text = "";
        _LedgerAliasText.Text = "";
     


        if (_Typelist.SelectedValue == "0")
            {

            CheckForChild();
            Level1 = LblNodeValue.Text.ToString().Substring(0, 1);
            Level2 = LblNodeValue.Text.ToString().Substring(1, 2);
            Level3 = LblNodeValue.Text.ToString().Substring(3, 2);
            Level4 = LblNodeValue.Text.ToString().Substring(5, 2);
            Level5 = LblNodeValue.Text.ToString().Substring(7);
            LblAlias.Visible = false;
            _LedgerAliasText.Visible = false;
            RequiredFieldValidatorAlais.Visible = false;
            LblGroup.Text = "Group Name : ";

            GetGroupDetails();
            }
        
        else if (_Typelist.SelectedValue == "1")

        {
            LblGroup.Text = "Ledger Name:";
            GetLevelChartIDLevel5();
        }
        else

        {
            LabelErrorMessage.Text = "Please Select Type";
        }
    }
    void GetGroupDetails()
    {
        if (Level == 5)
        {
            

        }
        else if (Level == 4)
        {
          
            LabelErrorMessage.Text = "Can not create more groups, must select account";
            return;
        }
        else if (Level == 3)
        {
            string P = "";
            char pad = '0';
            Int16 ii = Convert.ToInt16(Level4);
            ii += 1;
            P = ii.ToString();
            P = P.PadLeft(Level4.Length, pad);

            COAChartCode = Level1 + Level2 + Level3 + P + Level5;
            Level = 4;
        }
        else if (Level == 2)
        {
            string P = "";
            char pad = '0';
            Int16 ii = Convert.ToInt16(Level3);
            ii += 1;
            P = ii.ToString();
            P = P.PadLeft(Level3.Length, pad);

            COAChartCode = Level1 + Level2 + P + Level4 + Level5;
            Level = 3;
        }
        else if (Level == 1)
        {
            string P = "";
            char pad = '0';
            Int16 ii = Convert.ToInt16(Level2);
            ii += 1;
            P = ii.ToString();
            P = P.PadLeft(Level2.Length, pad);

            COAChartCode = Level1 + P + Level3 + Level4 + Level5;
            Level = 2;
        }
        TxtNewAccountNo.Text = COAChartCode.ToString();
        ChartCat = "T" + Level1.ToString();
    }
    void GetLevelChartIDLevel5()
    {
        CheckForChild();
        Level1 = LblNodeValue.Text.ToString().Substring(0, 1);
        Level2 = LblNodeValue.Text.ToString().Substring(1, 2);
        Level3 = LblNodeValue.Text.ToString().Substring(3, 2);
        Level4 = LblNodeValue.Text.ToString().Substring(5, 2);
        Level5 = LblNodeValue.Text.ToString().Substring(7);
        //ParentChartID = Convert.ToInt64(COAChartCode);
        string P = "";
        char pad = '0';
        if (Convert.ToInt16(Level5) > 0)
        {
            Int16 ii = Convert.ToInt16(Level5);
            ii += 1;
            P = ii.ToString();
            P = P.PadLeft(Level5.Length, pad);
            LastLevel = "W";

            COAChartCode = Level1 + Level2 + Level3 + Level4 + P;
            Level = 5;

        }
       
        else
        {

            Level1 = LblNodeValue.Text.ToString().Substring(0, 1);
            Level2 = LblNodeValue.Text.ToString().Substring(1, 2);
            Level3 = LblNodeValue.Text.ToString().Substring(3, 2);
            Level4 = LblNodeValue.Text.ToString().Substring(5, 2);
            Level5 = LblNodeValue.Text.ToString().Substring(7);
            COAChartCode = Level1.ToString() + Level2.ToString() + Level3.ToString() + Level4.ToString() + "0001".ToString();
            Level = 5;
        }
        TxtNewAccountNo.Text = COAChartCode;

    }
    void GetAccountNo()
    {
        Class1 queryResult = new Class1();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = " select count(*) as count  from tChartOfAccounts WHERE ParentChartID ='0'";
            accountantCls.beginTrans();
            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {


                Level = 1;
                     string ss = accountantCls.Reader[0].ToString();

                Int16 ii = Convert.ToInt16(ss.ToString());
                ii += 1;
                ChartCat ="T"+ ii.ToString();
                COAChartCode = ii.ToString() + "000000000";
                TxtNewAccountNo.Enabled = true;
                TxtNewAccountNo.Text = COAChartCode;
                    ParentChartID = 0;
                    AccountType = 0;

                }
                else
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        loadTreeMenu(TreeView1, LoadDataTable());
        Session["Active"] = "1";
        FindNodesByString();
        TreeNode searchNode = TreeView1.FindNode(TxtSearch.Text);
        if (searchNode != null)
            searchNode.Expand();
        LabelErrorMessage.Text = "Finished searching";
        
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountantChart.aspx");
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

    //private void FindMatch(TreeNode currentNode)
    //{
    //    string[] tokens = currentNode.Text.Split(':');

    //    if (tokens[1].Contains(TxtSearch.Text.ToUpper()))
    //    {
    //        if (Session["Active"].ToString() == "1")
    //        {
    //            //TreeView1.ExpandAll();
    //            currentNode.Expand();

    //            currentNode.ShowCheckBox = true;
    //            currentNode.Checked = true;
    //            currentNode.Select();
    //        }


    //    }
    //    else if (tokens[0].Contains(TxtSearch.Text.ToUpper()))
    //    {
    //        if (Session["Active"].ToString() == "1")
    //        {
    //            //TreeView1.ExpandAll();
    //            currentNode.Expand();

    //            currentNode.ShowCheckBox = true;
    //            currentNode.Checked = true;
    //            currentNode.Select();
    //        }


    //    }
    //    else
    //    {
    //        // currentNode.Collapse();
    //      /*  currentNode.ShowCheckBox = false*/;
    //    }
    //}
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
    protected void BtnSddMain_Click(object sender, EventArgs e)
    {
        GetAccountNo();
        _Typelist.SelectedValue = "0";
        pnlTextBoxes.Visible = true;
    }
}