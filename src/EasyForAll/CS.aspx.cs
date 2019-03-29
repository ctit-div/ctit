using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
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

    protected void AddTextBox(object sender, EventArgs e)
    {
        
        int g = 0;
        g = int.Parse(TextBoxCount.Text);
        for (int i = 1; i <= g ; i++)
        {
            int index = pnlTextBoxes.Controls.OfType<TextBox>().ToList().Count + 1;
            this.CreateTextBox("LblDynamic" + index, "TxtDynamic" + index, index);
            //("txtDynamic" + index.ToString()) = "2";
        }
        
    }

    private void CreateTextBox(string idl,string id,int f)
    {
        Label Lbl = new Label();
        Lbl.ID = idl;

        TextBox txt = new TextBox();
        txt.ID = id;
        pnlTextBoxes.Controls.Add(Lbl);
        pnlTextBoxes.Controls.Add(txt);
        Lbl.Text = "Level No:" + f + "    "; txt.Text = "2";
        txt.Width = 50;

        Literal lt = new Literal();
        lt.Text = "<br />";
        pnlTextBoxes.Controls.Add(lt);
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

            TextBoxLength.Text += s ;
        }
       
    }

}
