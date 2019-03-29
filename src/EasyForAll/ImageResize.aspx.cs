using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImageResize : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Class2 class2 = new Class2();
    Class_ImageResize cls = new Class_ImageResize();
    //protected void Upload_Files(object sender, EventArgs e)
    //{
    //    lblUploadStatus.Text = cls.imageresize(fileUpload);

    //}

    //Class2 class2 = new Class2();
    protected void Upload_Files(object sender, EventArgs e)
    {
        lblUploadStatus.Text = "";
        if (fileUpload.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {
            
            HttpFileCollection hfc = Request.Files;
          
            if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
            {
                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    lblUploadStatus.Text = cls.imageresize(hpf);
                   
                }
                lblUploadStatus.Text += "<br>  Total <b>" + hfc.Count + "</b> file(s) ";
            }
            else
            {
                lblUploadStatus.Text = "Max. 10 files allowed.";
            }
        }
        else
        {
            lblUploadStatus.Text = "No files selected.";
        }

    }


    
 }