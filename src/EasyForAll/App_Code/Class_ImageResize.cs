using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web;
/// <summary>
/// Summary description for Class_ImageResize
/// </summary>
public class Class_ImageResize : System.Web.UI.Page
{
    Class2 class2 = new Class2();
    public Class_ImageResize()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetInvoiceNo(string com, string ColName, string TabName )
    {
        AccountantCls accountantCls = new AccountantCls();
        string conString = WebConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
        Class1 class1 = new Class1();
        int i = 0;
        string P = "";
        try
        {
            

            accountantCls.Cmd.Connection.ConnectionString = accountantCls.GetConnStr();

            accountantCls.Cmd.Parameters.Clear();
            //accountantCls.Cmd.CommandText = " select Max(" + ColName + ") as InvoiceId from " + TabName ;
            accountantCls.Cmd.CommandType = CommandType.StoredProcedure;
            accountantCls.Cmd.CommandText = "USP_PathCompany_Select";
            accountantCls.Cmd.Parameters.Add("@PropertyId", SqlDbType.NVarChar).Value = com;
            accountantCls.beginTrans();



            accountantCls.Reader = accountantCls.Cmd.ExecuteReader();
            if (accountantCls.Reader.Read())
            {
                if(accountantCls.Reader["depth"].ToString()=="")
                {
                    P = "1";
                }
                else
                {
                    P = accountantCls.Reader["depth"].ToString();
                    //P = i.ToString();
                }
              
            }
            else
            {
                P = "1";

            }
          
            accountantCls.Reader.Close();
            accountantCls.commitTrans();




        }
        catch (Exception ex)
        {
            accountantCls.rollBackTrans();
          

        }

        char pad = '0';
        P = P.PadLeft(6, pad);
        string InvNo = com +  P;

        return InvNo;
    }
        public string imageresize(HttpPostedFile fileUpload)
    {
            string url1 = "";
           string url="", fname="";
        if (fileUpload.ContentLength>0)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {
            int iUploadedCnt = 0;

                    HttpPostedFile hpf = fileUpload;
                    if (hpf.ContentLength > 0)
                    {
                        string ran = "";
                        ran = RandomNumberGenerate();
                      
                        string myFilePath = Server.MapPath("UploadedImages/" + hpf.FileName);
                        string ext = Path.GetExtension(myFilePath);
                        if (ext.ToLower() != ".jpeg" && ext.ToLower() != ".jpg" && ext.ToLower() != ".png" && ext.ToLower() != ".gif")
                        {

                           
                            return "Type of file must be jpg, jpeg, png or gif";

                        }
                        url = "UploadedImages/" + ran + ext;
                        url1 = ran + ext;
                        fname = ran;
                        if (!File.Exists(Server.MapPath(Path.GetFileName(fname))))
                        {
                            DirectoryInfo objDir = new DirectoryInfo(Server.MapPath("UploadedImages\\"));
                            string sFileName = Path.GetFileName(fname);
                            string sFileExt = Path.GetExtension(url);
                            hpf.SaveAs(Server.MapPath(url));
                            iUploadedCnt += 1;
                           
                        }

                    }
                    Bind(url1);
            return url;
         
        }
        else
        {
            return "No files selected.";
        }


       
    }

    void Bind(string imageFile)
    {


        if (imageFile != "")
        {
           
            string path = Server.MapPath("~/UploadedImages/" + imageFile);
        
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
            int newHeight = 200;
            int newWidth = Convert.ToInt32(aspectRatio * newHeight);
            System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
            System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbBitmap.Save(Server.MapPath("~/UploadedImages/thumb/" + imageFile), System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbGraph.Dispose();
            thumbBitmap.Dispose();
            image.Dispose();
        }
    }


    public string RandomNumberGenerate()
    {

        Random rnum = new Random();
        int randomNumber = rnum.Next(100000000, 999999999);
        int randomNumber1 = rnum.Next(100000000, 999999999);
        return randomNumber.ToString() + randomNumber1.ToString();
    }
}