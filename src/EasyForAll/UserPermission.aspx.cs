

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

public partial class UserPermission : System.Web.UI.Page
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
          
          
            ViewState["EditId"] = "0";

        }
    }





    protected void GvPundles_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GV_Bind();
    }
    void GV_Bind()
    {
        DataView dv;
        dv = (DataView)SqlDataSourceUsers.Select(DataSourceSelectArguments.Empty);
        if (dv.Count > 0)
        {
            dv.RowFilter = "UserTypeCode='" + RadioButtonList1.SelectedValue + "'";
            GridViewUsers.DataSource = dv;
            GridViewUsers.DataBind();
        }
    }

    protected void GvPundles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPundles.PageIndex = e.NewPageIndex;
        GV_Bind();
    }

    protected void ButtonLink_Click(object sender, EventArgs e)
    {
        //ArrayList al = new ArrayList();
        foreach (GridViewRow R in GvPundles.Rows)
        {
            Session["UserRoleId"] = R.Cells[0].Text.Trim();
            //SqlDataSourceUsers.Delete();
            RadioButton ch = (RadioButton)R.FindControl("C1");

            if (ch.Checked)
            {
                foreach (GridViewRow RR in GridViewUsers.Rows)
                {
                    Session["UserId"] = RR.Cells[0].Text.Trim();
                    RadioButton chh = (RadioButton)RR.FindControl("C1");
                    if (chh.Checked)
                    {
                        SqlDataSourceUsers.Update();
                    }
                }

            }
            LabelErrorMessage.Text = "تم حفظ البيانات";





        }
    }

    protected void ButtonUnsellectAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow RR in GridViewUsers.Rows)
        {
            RadioButton chh = (RadioButton)RR.FindControl("C1");
            chh.Checked = false;
        }
    }

    protected void ButtonSellectAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow RR in GridViewUsers.Rows)
        {
            RadioButton chh = (RadioButton)RR.FindControl("C1");
            chh.Checked = true;
        }
    }

    protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridViewUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["UserRoleId"] = GridViewUsers.SelectedRow.Cells[3].Text.Trim();
        foreach (GridViewRow RR in GridViewUsers.Rows)
        {
            RadioButton chhh = (RadioButton)RR.FindControl("C1");
            chhh.Checked = false;
        }
        RadioButton chh = (RadioButton)GridViewUsers.SelectedRow.FindControl("C1");
        chh.Checked = true;

        foreach (GridViewRow R in GvPundles.Rows)
        {
            RadioButton ch = (RadioButton)R.FindControl("C1");

            if (Session["UserRoleId"].ToString() == R.Cells[0].Text.Trim())
            {
                R.BackColor = System.Drawing.Color.Turquoise;
                ch.Checked = true;
            }
            else
            {
                R.BackColor = System.Drawing.Color.FromArgb(120, 217, 239, 253); ;
                ch.Checked = false;
            }
        }



    }
}