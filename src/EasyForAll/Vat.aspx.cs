﻿using System;
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

public partial class Vat : System.Web.UI.Page
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
            //BindUnit();



        }
    }
    void BindUnit()
    {
        try
        {

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
            accountantCls.Cmd.CommandType = CommandType.Text;
            accountantCls.Cmd.CommandText = "select * from tvat";
            
            DataSet MyDateSet = new DataSet();
            accountantCls.beginTrans();
            accountantCls.Adapter.SelectCommand = accountantCls.Cmd;
            accountantCls.Adapter.Fill(MyDateSet, "vat");
            GridView1.DataSource = MyDateSet.Tables["vat"];
            GridView1.DataBind();

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
            accountantCls.Cmd.CommandText = "USP_Vat_Save";
            //accountantCls.Cmd.Parameters.Add("@UnitId", SqlDbType.Int).Value = Unit.UnitId;
            accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = int.Parse(Session["CompanyId"].ToString());
            //accountantCls.Cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = int.Parse(Session["BranchId"].ToString());
            accountantCls.Cmd.Parameters.Add("@VAT_Value", SqlDbType.NVarChar).Value = TxtVAT_Value.Text;
            accountantCls.Cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = TxtRemarks.Text;

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
        Response.Redirect("vat.aspx");
    }
    


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Label lb = (Label)GridView1.SelectedRow.FindControl("Label1");
        string lb = GridView1.SelectedRow.Cells[1].Text;
        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

        accountantCls.beginTrans();
        accountantCls.Cmd.CommandText = "select * from tvat where VAT_Id='" + lb + "'";
        accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
        if (accountantCls.Reader.Read())
        {
            TxtVAT_Value.Text = accountantCls.Reader["VAT_Value"].ToString();
            TxtRemarks.Text = accountantCls.Reader["Remarks"].ToString();
            Session["Id"] = lb;

            BtnSave.Visible = false;
            //BtnUpdate.Visible = true;
        }

        accountantCls.Reader.Close();
        accountantCls.commitTrans();
    }
}