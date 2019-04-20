

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class UnitsInfo : System.Web.UI.Page
{

    AccountantCls accountantCls = new AccountantCls();
   
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    static public int CreatedBy { get; set; }
    static public System.DateTime CreatedDate { get; set; }
    static public Nullable<int> ModifiedBy { get; set; }
    static public Nullable<System.DateTime> ModifiedDate { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (!IsPostBack)
        {

            //Session["CompanyId"] = "7";
            //Session["BranchId"] = "16";
            ViewState["EditId"] = "0";
            
            //CreatedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //ModifiedBy = Convert.ToInt32(Session["UserCode"].ToString());
            //Session["AdminUserName"] = "1";
            BindUnit();



        }
    }
    void BindUnit()
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Unit_Select";
            accountantCls.Cmd.Parameters.Add("@UnitId", SqlDbType.Int).Value = 0;
            DataSet MyDateSet = new DataSet();
            accountantCls.beginTrans();
            accountantCls.Adapter.SelectCommand = accountantCls.Cmd;
            accountantCls.Adapter.Fill(MyDateSet, "Units");
            GvDriver.DataSource = MyDateSet.Tables["Units"];
            GvDriver.DataBind();

            accountantCls.commitTrans();

        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Unit_Save";
            //accountantCls.Cmd.Parameters.Add("@UnitId", SqlDbType.Int).Value = Unit.UnitId;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value =int.Parse(Session["CompanyId"].ToString());
            //accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = int.Parse(Session["BranchId"].ToString());
            accountantCls.Cmd.Parameters.Add("@UnitName", SqlDbType.NVarChar).Value = TxtUnitName.Text;
            accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = TxtDescription.Text;
           
                accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
           
         
            accountantCls.beginTrans();
            accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.commitTrans();
            accountantCls.Cmd.Parameters.Clear();
            BindUnit();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
          LblMessage.Text = ex.Message;
            
        }
       
    }
    protected void BtnCleart_Click(object sender, EventArgs e)
    {
        Session.Remove("Id");
        Response.Redirect("UnitsInfo.aspx");
    }
    protected void GvDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lb = (Label)GvDriver.SelectedRow.FindControl("Label1");
        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

        accountantCls.beginTrans();
        accountantCls.Cmd.CommandText = "select * from tUnits where UnitId='" + lb.Text.Trim() + "'";
        accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
        if (accountantCls.Reader.Read())
        {
            TxtUnitName.Text = accountantCls.Reader["UnitName"].ToString();
            TxtDescription.Text = accountantCls.Reader["Description"].ToString();
            Session["Id"] = lb.Text.Trim();

            BtnSave.Visible = false;
            BtnUpdate.Visible = true;
        }

        accountantCls.Reader.Close();
        accountantCls.commitTrans();
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_Unit_Edit";
            accountantCls.Cmd.Parameters.Add("@UnitId", SqlDbType.Int).Value = Session["Id"].ToString();
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"];
            //accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = Session["BranchId"];
            accountantCls.Cmd.Parameters.Add("@UnitName", SqlDbType.NVarChar).Value = TxtUnitName.Text;
            accountantCls.Cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = TxtDescription.Text;
            accountantCls.Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            accountantCls.beginTrans();
            accountantCls.Cmd.ExecuteNonQuery();
            accountantCls.commitTrans();
            accountantCls.Cmd.Parameters.Clear();
            BindUnit();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            LblMessage.Text = ex.Message;

        }

       
    }

    protected void GvDriver_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Id"] = e.Keys[0].ToString();
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.beginTrans();


            accountantCls.Cmd.CommandText = "delete from  tUnits  where  UnitId='" + Session["Id"].ToString() + "'";
            accountantCls.Cmd.ExecuteNonQuery();
            LblMessage.Text = "Successfully deleted";
            accountantCls.Conn.Close();
            accountantCls.commitTrans();
            accountantCls.Cmd.Parameters.Clear();
            BindUnit();
        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            LblMessage.Text = ex.Message.ToString();
        }


    }

}