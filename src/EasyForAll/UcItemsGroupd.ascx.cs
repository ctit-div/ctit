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

public partial class UcItemsGroupd : System.Web.UI.UserControl
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

            //Session["CompanyId"] = "0";
            //Session["BranchId"] = "0";
            //ViewState["EditId"] = "0";
            
           
            //Session["AdminUserName"] = "1";
            ItemGroupId = 0;
            ParentGroupId = 0;
        

        }
    }


   

}
