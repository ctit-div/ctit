using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {  //set Processing Mode of Report as Local   

        if(!IsPostBack)
        {
            if (Session["UserCode"] == null)
            {
                Response.Redirect("Signin.aspx");
            }
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            string ConStr = ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            ReportDataSource datasource = new ReportDataSource();

            if (Session["Print"].ToString() == "Currency")
            {
                string strQuery = "SELECT * FROM tCurrency";
                da = new SqlDataAdapter(strQuery, con);
                da.Fill(dt);
                MyDataset ds = new MyDataset();
                ds.Tables["tCurrency"].Merge(dt);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurrency.rdlc");
                datasource = new ReportDataSource("MyDataset", ds.Tables[1]);
            }
            else if (Session["Print"].ToString() == "Agent")
            {
                string strQuery = "SELECT * FROM AgentTable";
                da = new SqlDataAdapter(strQuery, con);
                da.Fill(dt);
                MyDataset ds = new MyDataset();
                ds.Tables["AgentTable"].Merge(dt);
                //***************
                 dt = ds.Tables[2];
                DataColumn dc = new DataColumn("LogoImage", typeof(System.Byte[]));
                DataColumn dc1 = new DataColumn("ImageType", typeof(System.String));
                //dt.Columns.Add(dc);
                dt.Columns.Add(dc1);


                foreach (DataRow row in dt.Rows)
                {
                    string FolderPath = Server.MapPath("AgentLogo");
                    string filepath = string.Empty;
                    string ext = string.Empty;
                    if (Directory.Exists(FolderPath))
                    {
                        if (File.Exists(FolderPath + "\\" + row["LogoImage"].ToString() + ".jpg"))
                        {
                            filepath = FolderPath + "\\" + row["LogoImage"].ToString() + ".jpg";
                            ext = "jpg";




                        }
                        else if (File.Exists(FolderPath + "\\" + row["LogoImage"].ToString() + ".png"))
                        {
                            filepath = FolderPath + "\\" + row["LogoImage"].ToString() + ".png";
                            ext = "png";


                        }
                        else if (File.Exists(FolderPath + "\\" + row["LogoImage"].ToString() + ".gif"))
                        {
                            filepath = FolderPath + "\\" + row["LogoImage"].ToString() + ".gif";
                            ext = "gif";


                        }
                        else
                        {
                            filepath = FolderPath + "\\" + "no-image-available.png";
                            ext = "png";


                        }


                    }
                    row["LogoImage"] = Getimage(filepath);
                    row["ImageType"] = "image/" + ext;




                }
                //***************
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAgent.rdlc");
                datasource = new ReportDataSource("DataSet1", ds.Tables[2]);


            }
            else
            {
                return;
            }
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
    }


    private byte[] Getimage(string filepath)
    {
        // string FolderPath = Server.MapPath("Images");
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);

        byte[] image = br.ReadBytes((int)fs.Length);

        br.Close();

        fs.Close();

        return image;

    }
}