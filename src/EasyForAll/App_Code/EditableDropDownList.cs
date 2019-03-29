using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;

#region About
/* ------------------------------------------
 * Developer:	Eric Faust
 *				www.mosaiccrest.com
 * Date:		November 25, 2011
 * 
 * Description:	Editable Drop Down List
 *		This Control inherets from the .NET
 *		DropDownList and uses jQuery to create
 *		a drop down list that works like
 *		a text box with the available
 *		autocomplete and drop down list.
 *		
 * Required Files:
 *		jquery-ui.css
 *		img/ui-bg_flat_0_aaaaaa_40x100.png
 *		img/ui-icons_222222_256x240.png
 *		img/ui-icons_454545_256x240.png
 *		img/ui-icons_888888_256x240.png
 *		(Javascript files below should be used in this order below)
 *		jquery-1.6.4.min.js (* or greater)
 *		jquery.ui.core.js
 *		jquery.ui.widget.js
 *		jquery.ui.button.js
 *		jquery.ui.position.js
 *		jquery.ui.autocomplete.js
 *		jquery.ui.combobox.js  (This file is not a part of the jQuery library)
 *		
 * ** You are welcome to modify this source code
 *    to fit your requirements.
 * ------------------------------------------ */
#endregion About

[assembly: TagPrefix("EditableControls", "editable")]
namespace EditableControls
{
	[ToolboxData(@"<{0}:EditableDropDownList runat=""server"" ></{0}:EditableDropDownList>"),
	ToolboxBitmapAttribute(typeof(EditableDropDownList), "EditableDropDownList.bmp")]
	public class EditableDropDownList : System.Web.UI.WebControls.DropDownList, IPostBackDataHandler
	{
        // ###################
        #region Private Variables

        /// <summary>
        /// Store which events to raise
        /// </summary>
        private List<string> _raiseEvents = new List<string>();

        /// <summary>
        /// Check if we need to set focus
        /// </summary>
        private bool _setFocus;

        #endregion Private Variables

        // ###################
		#region Public Properties

		[Description("Displayed Text")]
		[Category("Behavior")]
		[Browsable(true)]
		[Themeable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				object o = ViewState["Text"];
				if (o == null)
					return string.Empty;
				return (string)o;
			}
			set { ViewState["Text"] = value; }
		}

		[Description("Sort the DropDownList items")]
		[Category("Behavior")]
		[Browsable(true)]
		[Themeable(true)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Sorted
		{
			get
			{
				object o = ViewState["Sorted"];
				if (o == null)
					return false;
				return (bool)o;
			}
			set { ViewState["Sorted"] = value; }
		}

		[Description("Selected Index")]
		[Bindable(true)]
		[Category("Data")]
		[DefaultValue(-1)]
		public override int SelectedIndex
		{
			get
			{
				return GetSelectedIndex();
			}
			set
			{
				SetSelectedIndex(value);
			}
		}

		[Description("Selected Value")]
		[Bindable(true)]
		[Category("Data")]
		public override string SelectedValue
		{
			get
			{
				return GetSelectedValue();
			}
			set
			{
				SetSelectedValue(value);
			}
		}

		[Description("Get the Selected Item")]
		[Bindable(true)]
		[Category("Data")]
		public override ListItem SelectedItem
		{
			get
			{
				return GetSelectedItem();
			}
		}

        [Description("Autoselect the first item of the DropDownList items")]
        [Category("Behavior")]
        [Browsable(true)]
        [Themeable(true)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AutoselectFirstItem
        {
            get
            {
                object o = ViewState["AutoselectFirstItem"];
				if (o == null)
					return false;
				return (bool)o;
			}
            set { ViewState["AutoselectFirstItem"] = value; }
        }
		#endregion Public Properties

		// ###################
		#region Public Events

		[Description("List Item Selected")]
		[Category("Action")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public event EventHandler OnClick;

		#endregion Public Events

		// ###################
		#region Control Init

		/// <summary>
		/// Initialize this control.  We need to call RegisterRequiresPostBack
		/// </summary>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page.RegisterRequiresPostBack(this);
		}

		#endregion Control Init

		// ###################
		#region Render

		/// <summary>
		/// Override the Render Event
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer)
		{
			// Check if our Drop Down List is Visible
            if (Visible)
            {
                // Prepare our Style
                string style = (Width.Value > 0) ? string.Format(@" style=""width: {0}; ", Width) : string.Empty;
                if (Style.Count > 0)
                {
                    if (string.IsNullOrEmpty(style)) style = @" style=""";
                    foreach (string key in Style.Keys)
                    {
                        style += string.Format("{0}: {1}; ", key, Style[key]);
                    }
                }
                if (!string.IsNullOrEmpty(style)) style = style.TrimEnd() + @"""";

                // Check if we have any Drop Down Selections
                if (Items.Count > 0 && Enabled)
                {
                    // Prepare our DropDownList
                    string css = string.Format(@"class=""ui-widget ui-widget-content ui-corner-left{0}""", (string.IsNullOrWhiteSpace(CssClass)) ? string.Empty : string.Format(@" {0}", CssClass));

                    List<string> dropDownItems = new List<string>();
                    foreach (ListItem item in Items)
                    {
                        dropDownItems.Add(item.Text);
                    }
                    // Create a distinct list of drop down selections from our values
                    string userSelections = CreateJavascriptArray(dropDownItems);

                    string tabIndex = (TabIndex > 0) ? string.Format(@" tabindex=""{0}"" ", TabIndex) : string.Empty;

                    // Check if we have Custom Attributes for this control
                    string customAttributes = GetCustomAttributes();
                    string baseID = ClientID;
                    string markup = string.Format(@"<input type=""text"" value=""{0}"" id=""{1}"" name=""{1}"" {2}{3}{4}{5}/>", Text, baseID, css, style, tabIndex, customAttributes);
                    writer.Write(markup);

                    string listID = string.Format("{0}_list", ClientID);
                    string script = string.Format(@"$(""#{0}"").combobox( {{source: {1}, name: ""{2}"", autopostback: {3}, autoselectFirstItem: {4}}} );", baseID, userSelections, listID, (AutoPostBack || OnClick != null).ToString().ToLower(), AutoselectFirstItem.ToString().ToLower());
                    
                    // Check if we need to set focus on our combobox
                    if (_setFocus)
                    {
                        script += string.Format("\n$(\"#{0}\").focus();", listID);
                    }

                    if (ScriptManager.GetCurrent(this.Page) != null)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "init" + ClientID, script, true);
                    else
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "init" + ClientID, script, true);
                }
                else
                {
                    // Standard Text Box
                    string css = string.Format(@"class=""ui-widget ui-widget-content ui-corner-left ui-corner-right{0}""", (string.IsNullOrWhiteSpace(CssClass)) ? string.Empty : string.Format(@" {0}", CssClass));

                    // Check if this item is Disabled
                    string disabled = (Enabled == true) ? string.Empty : @" disabled=""true""";

                    string markup = string.Format(@"<input type=""text"" value=""{0}"" id=""{1}"" name=""{1}"" {2}{3}{4}/>", Text, ClientID, css, style, disabled);
                    writer.Write(markup);
                }
            }
		}

		#endregion Render

		// ###################
		#region Postback Events
		
		/// <summary>
		/// Handle the Postback Event.
		/// <remarks>Also fires the events SelectedIndexChanged.</remarks>
		/// </summary>
		/// <param name="postDataKey"></param>
		/// <param name="postCollection"></param>
		/// <returns>True if a match is found</returns>
		public new bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			int currentIndex = SelectedIndex;
			string currentText = Text;
			string postedText = GetPostedText(postDataKey, postCollection);
			if (postedText != null && !currentText.Equals(postedText, StringComparison.Ordinal))
			{
				Text = postedText;
                if (SelectedIndex != currentIndex)
				{
                    _raiseEvents.Add("OnSelectedIndexChanged");
				}
                // Check if we need to raise a OnClick event
				if (OnClick != null)
				{
					string controlName = Page.Request.Params.Get("__EVENTTARGET");
					if (controlName == base.ClientID)
					{
                        _raiseEvents.Add("OnClick");
					}
				}
				return true;
			}
			return false;
		}
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			return;
		}

		#endregion Postback Events

        // ###################
        #region Control Events

        /// <summary>
        /// The OnPreRender event.  Use this event to raise any events that need to be fired before our controls are rendered.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            // Raise any events that need to be raised
            foreach (string eventName in _raiseEvents)
            {
                switch (eventName)
                {
                    case "OnSelectedIndexChanged":
                        OnSelectedIndexChanged(new EventArgs());
                        break;
                    case "OnClick":
                        if (OnClick != null)
                        {
                            OnClick(this, new EventArgs());
                        }
                        break;
                }
            }

            base.OnPreRender(e);
        }

        #endregion Control Events

        // ###################
        #region Overrides

        /// <summary>
        /// Set focus on this control
        /// </summary>
        public override void Focus()
        {
            _setFocus = true;
        }

        #endregion Overrides

        // ###################
		#region Private Functions

		/// <summary>
		/// Create a Javascript array from a list of values.
		/// <remarks>Empty Strings will be ignored.  To create a blank entry then use &nbsp;</remarks>
		/// </summary>
		/// <returns>A javascript array</returns>
		private string CreateJavascriptArray(List<string> values)
		{
			if (values == null || values.Count == 0)
				return string.Empty;
			// Check if we should sort our list
			if (Sorted)
			{
				values.Sort();
			}
			// Build our array
			string data = "[";
			string comma = string.Empty;
			Dictionary<string, bool> previousValues = new Dictionary<string, bool>();
			foreach (string s in values)
			{
				if (!string.IsNullOrEmpty(s) && !previousValues.ContainsKey(s))
				{
					data += comma + @"""" + s.Replace(@"""", @"\""") + @"""";
					if (string.IsNullOrEmpty(comma))
						comma = ",";
					previousValues.Add(s, true);
				}
			}
			data += "]";
			// Done
			return data;
		}

		/// <summary>
		/// Get the Currently Selected Index
		/// </summary>
		/// <returns>The current Selection or -1</returns>
		private int GetSelectedIndex()
		{
			string text = Text;
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < Items.Count; i++ )
				{
					if (text == Items[i].Text)
					{
						// We found our match
						return i;
					}
				}
			}
			// No match found
			return -1;
		}

		/// <summary>
		/// Get the Currently Selected Value
		/// </summary>
		/// <returns>The current Selection or empty String</returns>
		private string GetSelectedValue()
		{
			string text = Text;
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < Items.Count; i++)
				{
					if (text == Items[i].Text)
					{
						// We found our match
						return Items[i].Value;
					}
				}
			}
			// No match found
			return string.Empty;
		}

		/// <summary>
		/// Set the Selected Index by setting the appropriate Text
		/// </summary>
		/// <param name="value">Index</param>
		private void SetSelectedIndex(int value)
		{
			if (value >= 0 && value < Items.Count)
			{
				// Set our Text
				Text = Items[value].Text;
			}
			else
			{
				// Empty our Text
				Text = string.Empty;
			}
		}

		/// <summary>
		/// Set our Selected Value by Setting the Text of the matching Value
		/// </summary>
		/// <param name="value">Value</param>
		private void SetSelectedValue(string value)
		{
			foreach (ListItem item in Items)
			{
				if (item.Value == value)
				{
					// We found our match
					Text = item.Text;
					return;
				}
			}
			// No match found
			Text = string.Empty;
		}

		/// <summary>
		/// Get our Selected Item
		/// </summary>
		/// <returns></returns>
		private ListItem GetSelectedItem()
		{
			int index = GetSelectedIndex();
			return (index >= 0) ? Items[index] : null;
		}

		/// <summary>
		/// Get our Posted Text.  The postDataKey will need to be modified if this control is used in a master page.
		/// </summary>
		private static string GetPostedText(string postDataKey, NameValueCollection postCollection)
		{
			// Check our current post data key
            string value = postCollection[postDataKey];
			while (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(postDataKey))
			{
				// Try again
				if (postDataKey.Contains("$"))
				{
					// Convert Dollar sign into underscores
					postDataKey = postDataKey.Replace("$", "_");
				}
				else
				{
					// Remove up to our next underscore
					int pos = postDataKey.IndexOf("_");
					if (pos > 0)
					{
						postDataKey = postDataKey.Substring(pos + 1);
					}
					else
					{
						// No match found
                        return value;
					}
				}
				// Try to get our value
                value = postCollection[postDataKey] ?? value;
			}
			// Done
			return value;
		}

		/// <summary>
		/// Get any custom attributes that have been added
		/// </summary>
		/// <returns>A space delimetted list of Attributes</returns>
		private string GetCustomAttributes()
		{
			string result = string.Empty;
			foreach (string attKey in this.Attributes.Keys)
			{
				result += string.Format(@" {0}=""{1}""", attKey, this.Attributes[attKey]);
			}
			return result;
		}

		#endregion Private Functions
	}
}
