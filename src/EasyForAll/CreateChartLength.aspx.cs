

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



public partial class CreateChartLength : System.Web.UI.Page
{

     AccountantCls accountantCls =new AccountantCls();
     Class1 class1 = new Class1();
     //Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption RijndaelEncryption = new Midwest.Security.RijndaelEncryptionAlg.RijndaelEncryption();
        


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("Signin.aspx");
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
   
    #region Account()
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

        ArrayList ArrNoOfChartLevels = new ArrayList();
        ArrayList ArrLengthOfEachLevels = new ArrayList();

    private void CreateTextBox(string idl, string id, int f)
    {
       
            Label Lbl = new Label();

            Lbl.ID = idl;

            TextBox txt = new TextBox();
            txt.ID = id;
           
            pnlTextBoxes.Controls.Add(Lbl);
           
            pnlTextBoxes.Controls.Add(txt);
            

            txt.Width = 40;
            txt.CssClass = "form-control2";
            //txt.TextChanged += txtBox_TextChanged;
        
            //txt.AutoPostBack = true;
            //Literal lt = new Literal();

            Lbl.Text = "Level No :   " + f + "    ";
            //pnlTextBoxes.Controls.Add(lt);
            //lt.Text = "<br />";


            if (Session["again"] != null)
            {
                if (Session["again"].ToString() == "1")
                {
                    if (f == 1)
                    {
                        ArrNoOfChartLevels.Add(f);
                        ArrLengthOfEachLevels.Add("1");
                        txt.Text = "1";
                        TextBoxLength.Text += "1";
                        txt.Enabled = false;
                    }
                    else if (f == 2)
                    {
                        ArrNoOfChartLevels.Add(f);
                        ArrLengthOfEachLevels.Add("01");
                        txt.Text = "2";
                        TextBoxLength.Text += "01";
                        txt.Enabled = false;
                    }
                    else if (f == 3)
                    {
                        ArrNoOfChartLevels.Add(f);
                        ArrLengthOfEachLevels.Add("001");
                        txt.Text = "3";
                        TextBoxLength.Text += "001";
                        txt.Enabled = false;
                    }
                    else
                    {
                        ArrNoOfChartLevels.Add(f);
                        ArrLengthOfEachLevels.Add("00001");
                        txt.Text = "5";
                        TextBoxLength.Text += "00001";
                    }
                    pnlTextBoxes.Controls.Add(txt);

                }

            }
            else
            {

                if (f == 1)
                {
                    ArrNoOfChartLevels.Add(f);
                    ArrLengthOfEachLevels.Add("1");
                    txt.Text = "1";
                    TextBoxLength.Text += "1";
                    txt.Enabled = false;
                }
                else if (f == 2)
                {
                    ArrNoOfChartLevels.Add(f);
                    ArrLengthOfEachLevels.Add("01");
                    txt.Text = "2";
                    TextBoxLength.Text += "01";
                    txt.Enabled = false;
                }
                else if (f == 3)
                {
                    ArrNoOfChartLevels.Add(f);
                    ArrLengthOfEachLevels.Add("001");
                    txt.Text = "3";
                    TextBoxLength.Text += "001";
                    txt.Enabled = false;
                }
                else
                {
                    ArrNoOfChartLevels.Add(f);
                    ArrLengthOfEachLevels.Add("00001");
                    txt.Text = "5";
                    TextBoxLength.Text += "00001";
                }
                pnlTextBoxes.Controls.Add(txt);
            }
           
    }
    //private void txtBox_TextChanged(object sender, EventArgs e)
    //{
    //    //TextBoxLength.Text.Substring(0, TextBoxLength.Text.Length - 2);
    //    //TextBox tx=(TextBox)sender;
    //    //TextBoxLength.Text += tx.Text;
    //}
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
#endregion


   
 

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
        Response.Redirect("CreateChartLength.aspx");
    }
    ArrayList ArrTextNames = new ArrayList();
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        foreach(var control in Page.Controls)
   {
      if(control is TextBox)
      {

        if(((TextBox)control).ID.IndexOf("txtDynamic") != -1)
        {
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlCommand cmd = new SqlCommand("INSERT INTO TI_Homes(CustomerID, Year, Make, Size, Owed, Offer, Wholesale) VALUES('1000', @name, @year, '80ft', '100,000', '80,000', 'Wholesale?')"))
        //    {

        //        cmd.Connection = con;
        //        cmd.Parameters.AddWithValue("@name", ((TextBox)control).Text);
        //        cmd.Parameters.AddWithValue("@year", (pnl.FindControl("txtDynamic2") as TextBox).Text);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //  }
        }
      }
     }
   
        
        DataTable tbl = new DataTable();
        tbl =(DataTable) Session["Tbl"];
        int k = 0;
        TextBox TxtDynamic = new TextBox();
        foreach (DataRow row in tbl.Rows)
        {

            //for (int k = 0; k < 4; k++)
            //{
            TextBox txt = (TextBox)Page.FindControl(row[k + 1].ToString());

            //}

            //foreach (Control contrl in Master.Controls)
            //{
            //    if (Master.FindControl("ContentPlaceHolder1") != null)
            //    {

            //    }

            //}
            //if (Master.FindControl("ContentPlaceHolder1") != null)
            //{
                //if (Master.FindControl("ContentPlaceHolder1").FindControl("UpdatePanel1").FindControl("pnlTextBoxes").FindControl(row[k + 1]) != null)
                //{
                //    TextBox tb = (TextBox)Master.FindControl("pnlTextBoxes").FindControl("TxtDynamic" + (k + 1));

                //    ArrTextNames.Add(tb.Text);
                //}
            //}
            //if (Master.FindControl("UpdatePanel1").FindControl("pnlTextBoxes").FindControl("TxtDynamic" + (k + 1)) != null)
            //{
            //    TextBox tb = (TextBox)Master.FindControl("pnlTextBoxes").FindControl("TxtDynamic" + (k + 1));
            //    ArrTextNames.Add(tb.Text);
            //}
         
           
        }


        //foreach (Control ctrl in pnlTextBoxes.Controls)
        //{
        //    if (ctrl is Panel)
        //    {
        //        // Get each ID and Text from TextBox
        //        var textBoxes = ctrl.Controls.OfType<TextBox>()
        //                                     .Select(t => Tuple.Create(t.ID, t.Text))
        //                                     .ToList();

        //        foreach (var item in textBoxes)
        //        {
        //            ArrLengthOfEachLevels.Add(item.ToString());
        //        }
        //    }
        //}


        //int i = 0;
        //foreach (Control contrl in this.pnlTextBoxes.Controls)
        //{
        //    if (contrl.ID == ("txt" + i.ToString()))
        //    {
        //        ArrLengthOfEachLevels.Add(contrl.ToString());
        //        //contrl.Text = "requiredtexttobeset";
        //    }
        //    i = i + 1;
        //}
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
        }
    }
   
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //Session["again"] = "1";
        DataTable tbl = new DataTable();
        TextBoxLength.Text = "";
        //Session.Remove("again");
        int g = 0;
        g = int.Parse(_TextBoxCount.Text);
        for (int i = 1; i <= g; i++)
        {

            int index = pnlTextBoxes.Controls.OfType<TextBox>().ToList().Count + 1;
            this.CreateTextBox("LblDynamic" + index, "TxtDynamic" + index, index);

        }
        SqlDataSource1.DataBind();
        GvCompany.DataBind();
        BtnUpdate.Visible = true;
        BtnSave.Visible = false;
        int h = 0;
        h = int.Parse(_TextBoxCount.Text);
        DataColumn tc = new DataColumn("TextName");
        tbl.Columns.Add(tc);
        for (int p = 0; p < h; p++)
        {

            DataRow ro = tbl.NewRow();
            //ro["TextName"] = Session["SID"].ToString();
            ////TableCell tc = new TableCell();

            TextBox TxtDynamic = new TextBox();
            TxtDynamic.ID = "TxtDynamic" +( p +1);
            //_checkbox.Text = "Checkbox" + i;
            ro["TextName"] = TxtDynamic.ID;
            tbl.Rows.Add(ro);
            
        }
        Session["Tbl"] = tbl;
    }
}