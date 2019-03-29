

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
using System.Collections;



public partial class CreateChart : System.Web.UI.Page
{

     AccountantCls accountantCls =new AccountantCls();
     Class1 class1 = new Class1();

 #region Others()
   protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
        }
        if (Session["dir"] == null)
        {
            Session["dir"] = "ltr";
            
        }
        
        if (!IsPostBack)
        {

            pnlLongDescription.Visible = true;
            pnlTextBoxes.Visible = true;
            //Session["CompanyId"] = "8";
            ViewState["EditId"] = "0";
            
            Session["AdminUserName"] = "1";
        }
    }
    protected void GetTextBoxValues(object sender, EventArgs e)
    {
        TextBoxLength.Text = "";
        foreach (TextBox textBox in pnlTextBoxes.Controls.OfType<TextBox>())
        {
            string s = "";
            if (textBox.Text == "1")
            {
                s = "1";
            }
            if (textBox.Text == "2")
            {
                s = "01";
            }
            if (textBox.Text == "3")
            {
                s = "001";
            }
            if (textBox.Text == "4")
            {
                s = "0001";
            }
            if (textBox.Text == "5")
            {
                s = "00001";
            }
            if (textBox.Text == "6")
            {
                s = "000001";
            }
            if (textBox.Text == "7")
            {
                s = "0000001";
            }

            TextBoxLength.Text += s;
        }
        Session.Remove("again");
    }
    protected void GvCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["EditId"] = GvCompany.SelectedRow.Cells[1].Text.Trim();
        Session["CompanyID"] = ViewState["EditId"];
        class1.RecordId =int.Parse( ViewState["EditId"].ToString());
        DataView MyDv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        MyDv.RowFilter = "CompanyID='" + ViewState["EditId"].ToString() + "' ";
        if (MyDv.Count > 0)
        {
            _CMPCompanyNameText.Text = MyDv[0].Row["CMPCompanyName"].ToString();
          
            TextBoxLength.Text = MyDv[0].Row["ChartLastLevelLength"].ToString();
            _TextBoxCount.Text = MyDv[0].Row["NoOfChartLevels"].ToString();
            Session["Status"] = MyDv[0].Row["IsActive"].ToString().Trim();
            if (Session["Status"].ToString() == "True")
            {
                LblStatus.Text = "Active";
                LblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblStatus.Text = "Not Active";
                LblStatus.ForeColor = System.Drawing.Color.Red;

            }
          
           
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateChart.aspx");
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
 #endregion

#region Account()
    //int r = 0;
 protected void Page_PreInit(object sender, EventArgs e)
    {
      
        List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("TxtDynamic")).ToList();
        int i = 1;
        foreach (string key in keys)
        {
           
            this.CreateTextBox("LblDynamic" + i, "TxtDynamic" + i, i);
            i++;
        }
    }

       protected void BtnSave_Click(object sender, EventArgs e)
    {
        TextBoxLength.Text = "";
        int g = 0;
        g = int.Parse(_TextBoxCount.Text);
        for (int i = 1; i <= g; i++)
        {
            this.CreateTextBox("LblDynamic" + i, "TxtDynamic" + i, i);
        }
        BtnUpdate.Visible = true;
        BtnSave.Visible = false;
      
    }

    private void CreateTextBox(string idl, string id, int f)
       {
           if (f == 1)
           {
               TextBoxLength.Text += "1";
               Session["Add"] = "1";
           }
           else if (f == 2)
           {
               TextBoxLength.Text += "01";
               Session["Add"] = "01";
           }
           else if (f == 3)
           {
               TextBoxLength.Text += "001";
               Session["Add"] = "001";
           }
           else
           {
               Session["Add"] = "00001";
               TextBoxLength.Text += "00001";
           }
         
        TextBox txt = new TextBox();
        txt.ID = id;
        txt.Text = Session["Add"].ToString();
        
      
        pnlTextBoxes.Controls.Add(txt);

        Literal lt = new Literal();
        //lt.Text = "<br />";
        pnlTextBoxes.Controls.Add(lt);

       
               
           
    }
    
       protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        string s1 = "0";
        string  s2 = "0";
        string  s3 = "0";
        string  s4 = "0";
        string  s5 = "0";
        string  s6 = "0";
        string  s7 = "0";
        string  s8 = "0";
        string  s9 = "0";
        

        int d = 0;
        TextBoxLength.Text = "";
        foreach (TextBox textBox in pnlTextBoxes.Controls.OfType<TextBox>())
        {
            d = d + 1;
            if (d == 1)
            {
                s1 = textBox.Text;
                TextBoxLength.Text += s1;
            }
            else if (d == 2)
            {
                s2 = textBox.Text;
                TextBoxLength.Text += s2;
            }
            else if (d == 3)
            {
                s3 = textBox.Text;
                TextBoxLength.Text += s3;
            }
            else if (d == 4)
            {
                s4 = textBox.Text;
                TextBoxLength.Text += s4;
            }
            else if (d == 5)
            {
                s5 = textBox.Text;
                TextBoxLength.Text += s5;
            }
            else if (d == 6)
            {
                s6 = textBox.Text;
                TextBoxLength.Text += s6;
            }
            else if (d == 7)
            {
                s7 = textBox.Text;
                TextBoxLength.Text += s7;
            }
            else if (d == 8)
            {
                s8 = textBox.Text;
                TextBoxLength.Text += s8;
            }
            else if (d == 9)
            {
                s9 = textBox.Text;
                TextBoxLength.Text += s9;
            }
        }

        try
           
        {
        accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();
        accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
        accountantCls.Cmd.CommandText = "USP_Company_CreateChartLength";
        accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString());


        accountantCls.Cmd.Parameters.Add("@NoOfChartLevels", SqlDbType.Int).Value = int.Parse(_TextBoxCount.Text);
        accountantCls.Cmd.Parameters.Add("@ChartLastLevelLength", SqlDbType.NVarChar, 50).Value = TextBoxLength.Text;


        accountantCls.Cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = Session["UserCode"].ToString();
        accountantCls.Cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

        class1.ErrorNo = 0;

        accountantCls.beginTrans();
        int t = accountantCls.Cmd.ExecuteNonQuery();
        accountantCls.Cmd.Parameters.Clear();
        accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
        accountantCls.Cmd.CommandText = "USP_ChartingLevel_Save";
        accountantCls.Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = int.Parse(ViewState["EditId"].ToString());


        accountantCls.Cmd.Parameters.Add("@Level1", SqlDbType.NVarChar,50).Value = s1.ToString();
        accountantCls.Cmd.Parameters.Add("@Level2",  SqlDbType.NVarChar,50).Value = s2.ToString();
        accountantCls.Cmd.Parameters.Add("@Level3", SqlDbType.NVarChar, 50).Value = s3.ToString();
        accountantCls.Cmd.Parameters.Add("@Level4", SqlDbType.NVarChar, 50).Value = s4.ToString();
        accountantCls.Cmd.Parameters.Add("@Level5", SqlDbType.NVarChar, 50).Value = s5.ToString();
        accountantCls.Cmd.Parameters.Add("@Level6", SqlDbType.NVarChar, 50).Value = s6.ToString();
        accountantCls.Cmd.Parameters.Add("@Level7", SqlDbType.NVarChar, 50).Value = s7.ToString();
        accountantCls.Cmd.Parameters.Add("@Level8", SqlDbType.NVarChar, 50).Value = s8.ToString();
        accountantCls.Cmd.Parameters.Add("@Level9", SqlDbType.NVarChar, 50).Value = s9.ToString();
       

         t = accountantCls.Cmd.ExecuteNonQuery();

        if (ViewState["EditId"].ToString() == "0")
        {
            class1.ErrorNo = -1;

            class1.ErrorDesc = Resources.ResourceMain.CompanyAlreadyExists;
            SqlDataSource1.DataBind();
            GvCompany.DataBind();

        }
        else if (t == -1)
        {
            class1.ErrorNo = -1;
            class1.ErrorDesc = Resources.ResourceMain.UserAlreadyExists;

        }
        accountantCls.commitTrans();
       }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
            class1.ErrorDesc = ex.Message;
            class1.ErrorNo = -1;
        }
        finally
        {
            SqlDataSource1.DataBind();
            GvCompany.DataBind();
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            BtnClear_Click(sender, e);
        }
    }
#endregion

}