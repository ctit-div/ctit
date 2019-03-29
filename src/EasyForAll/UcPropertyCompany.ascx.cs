using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class UcPropertyCompany : System.Web.UI.UserControl
{
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LabelErrorMessage.Text = "";
            dv = (DataView)SqlDataSourceCompany.Select(DataSourceSelectArguments.Empty);
            Ddlist.DataSource = dv;
            if (dv.Count > 0)
            {
                Ddlist.DataValueField = "CompanyID";
                Ddlist.DataTextField = "CMPCompanyName";
                Ddlist.DataBind();
                Ddlist.Items.Insert(0, new ListItem("Select One", "-1"));
            }
            else
            {
                LabelErrorMessage.Text = "This is the last level";
            }
            
        }
    }

    protected void Ddlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CompanyIdName"] = Ddlist.SelectedValue;
        //Session["CompanyIdName"] = Ddlist.SelectedValue;
        //LabelErrorMessage.Text = "";
        //dv = (DataView)SqlDataSourceDivision.Select(DataSourceSelectArguments.Empty);
        //dv.RowFilter = "CompanyIDParent='" + Ddlist.SelectedValue + "'";
        //Ddlist.DataSource = dv;
        //if (dv.Count > 0)
        //{
        //    Ddlist.DataValueField = "CompanyID";
        //    Ddlist.DataTextField = "CMPCompanyName";
        //    Ddlist.DataBind();
        //    Ddlist.Items.Insert(0, new ListItem("Select One", "-1"));
        //}
        //else
        //{
        //    LabelErrorMessage.Text = "This is the last level";
        //}
    }

    protected void ImageButtonDown_Click(object sender, ImageClickEventArgs e)
    {
        //Session["CompanyIdName"] = Ddlist.SelectedValue;
        LabelErrorMessage.Text = "";
        dv = (DataView)SqlDataSourceDivision.Select(DataSourceSelectArguments.Empty);

        if (Ddlist.SelectedValue == "-1")
        {
            dv.RowFilter = "CompanyIDParent='0'";
        }
        else
        {
            dv.RowFilter = "CompanyIDParent='" + Ddlist.SelectedValue + "'";
        }
       
       
        Ddlist.DataSource = dv;
        if (dv.Count > 0)
        { Session["Pre"] = dv[0].Row[2].ToString();
            Ddlist.DataValueField = "CompanyID";
            Ddlist.DataTextField = "CMPCompanyName";
            Ddlist.DataBind();
            Ddlist.Items.Insert(0, new ListItem("Select One", "-1"));
        }
        else
        {
            LabelErrorMessage.Text = "This is the last level";
        }
        Session["CompanyIdName"] = Ddlist.SelectedValue;
    }

    protected void ImageButtonUp_Click(object sender, ImageClickEventArgs e)
    {
      
        LabelErrorMessage.Text = "";
        dv = (DataView)SqlDataSourceDivision.Select(DataSourceSelectArguments.Empty);
        if (Ddlist.SelectedValue == "-1")
        {
            dv.RowFilter = "CompanyIDParent='0'";
        }
        else
        {
            dv.RowFilter = "CompanyID='" + Session["Pre"].ToString() + "'";
        }

      
       
        Ddlist.DataSource = dv;
        if (dv.Count > 0)
        {
            Session["Pre"] = dv[0].Row[2].ToString();
            Ddlist.DataValueField = "CompanyID";
            Ddlist.DataTextField = "CMPCompanyName";
            Ddlist.DataBind();
            Ddlist.Items.Insert(0, new ListItem("Select One", "-1"));
        }
        else
        {
            LabelErrorMessage.Text = "This is the last level";
        }
        Session["CompanyIdName"] = Ddlist.SelectedValue;
    }
}