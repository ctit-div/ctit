using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EditableDropDownListTest;

public partial class AutoComplete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)

        {

            //List<Item> lst = new List<Item>();

            //lst.Add(new Item() { ID = "0", Val = "Select" });

            //lst.Add(new Item() { ID = "1", Val = "This is a test for long text... This is a test for long text" });

            //lst.Add(new Item() { ID = "2", Val = "now lest see how this works out.... This is a test for long textThis is a test for long text" });

            //lst.Add(new Item() { ID = "3", Val = "joseph" });

            //lst.Add(new Item() { ID = "4", Val = "kiran" });

            //ddlTest.DataSource = lst;

            //ddlTest.DataTextField = "Val";

            //ddlTest.DataValueField = "ID";

            ddlTest.DataBind();
            ddlTest.Items.Insert(0, new ListItem("Select", "1b"));

        }

    }
}
class Item

{

    string _val;

    string _id;



    public string ID { get { return _id; } set { _id = value; } }

    public string Val { get { return _val; } set { _val = value; } }





}