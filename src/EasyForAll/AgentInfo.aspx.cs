

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Configuration;

public partial class AgentInfo : System.Web.UI.Page
{
    AccountantCls accountantCls = new AccountantCls();
    string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        else
        {
           
        }
        if (!IsPostBack)
        {
            BindUser();
        }
    }

    void BindUser()
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
            MyCmd.CommandText = "select * from AgentTable";
          SqlDataAdapter MyAdapter =new SqlDataAdapter(MyCmd);
          DataSet MyDateSet = new DataSet();
          MyAdapter.Fill(MyDateSet, "Users");
          DataList1.DataSource = MyDateSet.Tables["Users"];
          DataList1.DataBind();


            MyCon.Close();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        Random rnum = new Random();
        int randomNumber = rnum.Next(100000000, 999999999);
        int randomNumber1 = rnum.Next(100000000, 999999999);

        if (FileUpload1.HasFile)
        {
            string myFilePath = Server.MapPath("AgentLogo/" + FileUpload1.FileName);
            string ext = Path.GetExtension(myFilePath);
            FileUpload1.SaveAs(Server.MapPath("AgentLogo/" + randomNumber.ToString() + randomNumber1.ToString() + ext));
            Session["url"] = "AgentLogo/" + randomNumber.ToString() + randomNumber1.ToString() + ext;
        }

        else
        {
            Session["url"] = "AgentLogo/no-image-available.png";

        }
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
          
                MyCmd.CommandText = "insert into AgentTable(UserName,Email,Mobile,Address,LogoImage) values(N'" + TxtUserName.Text + "',N'" + TxtEmail.Text + "',N'" + TxtMobile.Text + "',N'" + TxtAddress.Text + "',N'" + Session["url"].ToString() + "')";
            
          
            MyCmd.ExecuteNonQuery();


           


            LblMessage.Text = "Successfully registerd";
            MyCon.Close();
            BindUser();
        }
    catch (Exception ex)
    {
        LblMessage.Text = ex.Message.ToString();
    }
    }
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Random rnum = new Random();
        int randomNumber = rnum.Next(100000000, 999999999);
        int randomNumber1 = rnum.Next(100000000, 999999999);

        if (FileUpload1.HasFile)
        {
            string myFilePath = Server.MapPath("AgentLogo/" + FileUpload1.FileName);
            string ext = Path.GetExtension(myFilePath);
            FileUpload1.SaveAs(Server.MapPath("AgentLogo/" + randomNumber.ToString() + randomNumber1.ToString() + ext));
            Session["url"] = "AgentLogo/" + randomNumber.ToString() + randomNumber1.ToString() + ext;
        }
        else
        {
            if (H1.NavigateUrl == null)
            {
                Session["url"] = "AgentLogo/no-image-available.png";
            }
            else
            {
                Session["url"] = H1.NavigateUrl;
            }
        }
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;
          
                MyCmd.CommandText = "update  AgentTable set UserName=N'" + TxtUserName.Text + "',LogoImage=N'" + Session["url"].ToString() + "',Status=N'" + RadioButtonList2.SelectedValue + "',Email=N'" + TxtEmail.Text + "',Mobile=N'" + TxtMobile.Text + "',Address=N'" + TxtAddress.Text + "' where  AgentCode = N'" + Session["AgentCode"].ToString() + "'";
         
            
            MyCmd.ExecuteNonQuery();


            LblMessage.Text = "Successfully Edited";
            MyCon.Close();
            BindUser();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection MyCon = new SqlConnection(conString);
            MyCon.Open();
            SqlCommand MyCmd = new SqlCommand();
            MyCmd.Connection = MyCon;

            MyCmd.CommandText = "delete from  AgentTable  where  AgentCode = N'" + Session["AgentCode"].ToString() + "'";
            MyCmd.ExecuteNonQuery();
            LblMessage.Text = "Successfully Edited";
            MyCon.Close();
            BindUser();
        }
        catch (Exception ex)
        {
            LblMessage.Text = ex.Message.ToString();
        }
    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        Label AgentCodeLabel = (Label)DataList1.SelectedItem.FindControl("AgentCodeLabel");
        Session["AgentCode"] = AgentCodeLabel.Text;


        Label UserNameLabel = (Label)DataList1.SelectedItem.FindControl("UserNameLabel");
        TxtUserName.Text = UserNameLabel.Text;

        Label AddressLabel = (Label)DataList1.SelectedItem.FindControl("AddressLabel");
        TxtAddress.Text = AddressLabel.Text;
        Label EmailLabel = (Label)DataList1.SelectedItem.FindControl("EmailLabel");
        TxtEmail.Text = EmailLabel.Text;

          Label MobileLabel = (Label)DataList1.SelectedItem.FindControl("MobileLabel");

             
          TxtMobile.Text = MobileLabel.Text;
          
         
         

         
         Label StatusLabel = (Label)DataList1.SelectedItem.FindControl("StatusLabel1");
         RadioButtonList2.SelectedValue = StatusLabel.Text;
        Image Im = (Image)DataList1.SelectedItem.FindControl("picture");
        H1.NavigateUrl = Im.ImageUrl;


        BtnDelete.Visible = true;
         BtnEdit.Visible = true;
         BtnSave.Visible = false;
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentInfo.aspx");
    }


    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        Session["Print"] = "Agent";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Print.aspx');", true);
    }
}