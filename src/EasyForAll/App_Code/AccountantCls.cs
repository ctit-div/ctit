using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;

/// <summary>
/// Summary description for AccountantCls
/// </summary>
public class AccountantCls : System.Web.UI.Page
{
    public AccountantCls()
    {
    }
		//
		// TODO: Add constructor logic here
		//

        //public string GetConnStr()
        //{
        //    return ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
        //}
        private SqlConnection _Conn = new SqlConnection();

        private SqlTransaction _Trans;

        private SqlCommand _Cmd = new SqlCommand();

        private SqlDataReader _Reader;

        private SqlDataAdapter _Adapter = new SqlDataAdapter();

        public string GetConnStr()
        {
            return ConfigurationManager.ConnectionStrings["FinanceConnStr"].ConnectionString;
        }

        public void beginTrans()
        {
            _Conn.ConnectionString = ConnStr;
            _Conn.Open();
            _Trans = _Conn.BeginTransaction();
            _Cmd.Transaction = _Trans;
            _Cmd.Connection = _Conn;
        }

        public void commitTrans()
        {
        
        _Trans.Commit();
            _Cmd.Dispose();
            _Conn.Close();
        }

        public void rollBackTrans()
        {
            _Trans.Rollback();
            _Cmd.Dispose();
            _Conn.Close();
        }

        public SqlConnection Conn
        {
            get
            {
                return new SqlConnection();
            }
        }

        public string ConnStr
        {
            get
            {
                return this.GetConnStr();
            }
        }

        public SqlCommand Cmd
        {
            get
            {
                _Cmd.Connection = _Conn;
                _Cmd.Transaction = _Trans;
                return _Cmd;
            }
        }

        public SqlTransaction Trans
        {
            get
            {
                return _Trans;
            }
        }

        public SqlDataReader Reader
        {
            get
            {
                return _Reader;
            }
            set
            {
                _Reader = value;
            }
        }

        public SqlDataAdapter Adapter
        {
            get
            {
                return _Adapter;
            }
            set
            {
                _Adapter = value;
            }
        }

    void GetCustomization()
    {
        Class1 queryResult = new Class1();
        try
        {
            Session["CompanyId"] = "7";
            Cmd.Connection.ConnectionString = GetConnStr();

            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "select *  from tCustomizations WHERE CompanyId = " + Session["CompanyId"].ToString();
            beginTrans();
            Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                //Class2 class2 = new Class2()
                //{
                Session["BankGroup"] = Reader["BankGroup"].ToString();
                Session["CashGroup"] = Reader["CashGroup"].ToString();
                Session["ChequeGroup"] = Reader["ChequeGroup"].ToString();
                Session["CustomerGroup"] = Reader["CustomerGroup"].ToString();
                Session["EmployeeGroup"] = Reader["EmployeeGroup"].ToString();
                Session["IsVoucherNoMandatory"] = Reader["IsVoucherNoMandatory"].ToString();
                Session["PostingType"] = Reader["PostingType"].ToString();
                Session["ProfitLossLedger"] = Reader["ProfitLossLedger"].ToString();
                Session["SupplierGroup"] = Reader["SupplierGroup"].ToString();





            }
            Reader.Close();
            commitTrans();
        }
        catch (Exception ex)
        {
            //rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }
    void CheckForChild()
    {

        Class1 queryResult = new Class1();
        try
        {

            Cmd.Connection.ConnectionString = GetConnStr();

            Cmd.CommandType = CommandType.Text;
            Cmd.Parameters.Clear();
            if (Session["AccFrom"].ToString() == "C")
            {
                Cmd.CommandText = " select max(ChartId) as ChartId,COAChartCode  from tChartOfAccounts WHERE ParentChartID ='" + Session["CustomerGroup"].ToString() + "' Group by ChartId,COAChartCode   order by ChartId desc ";

                //Session["ParentId"] = Session["CustomerGroup"];
            }
            else if (Session["AccFrom"].ToString() == "E")
            {
                Cmd.CommandText = " select max(ChartId) as ChartId,COAChartCode  from tChartOfAccounts WHERE ParentChartID ='" + Session["EmployeeGroup"].ToString() + "' Group by ChartId,COAChartCode   order by ChartId desc ";

                //Session["ParentId"] = Session["EmployeeGroup"];
            }
            else if (Session["AccFrom"].ToString() == "S")
            {
                Cmd.CommandText = " select max(ChartId) as ChartId,COAChartCode  from tChartOfAccounts WHERE ParentChartID ='" + Session["SupplierGroup"].ToString() + "' Group by ChartId,COAChartCode   order by ChartId desc ";

                //Session["ParentId"] = Session["SupplierGroup"];
            }
            else if (Session["AccFrom"].ToString() == "B")
            {
                Cmd.CommandText = " select max(ChartId) as ChartId,COAChartCode  from tChartOfAccounts WHERE ParentChartID ='" + Session["BankGroup"].ToString() + "' Group by ChartId,COAChartCode   order by ChartId desc ";

                //Session["ParentId"] = Session["SupplierGroup"];
            }
            else if (Session["AccFrom"].ToString() == "CA")
            {
                Cmd.CommandText = " select max(ChartId) as ChartId,COAChartCode  from tChartOfAccounts WHERE ParentChartID ='" + Session["CashGroup"].ToString() + "' Group by ChartId,COAChartCode   order by ChartId desc ";

                //Session["ParentId"] = Session["SupplierGroup"];
            }
            else
            {
                return;
            }
           
            beginTrans();
            Reader = Cmd.ExecuteReader();


            if (Reader.Read())
            {
                Session["HasChild"] = "Y";


                //if (Session["AccFrom"].ToString() == "C")
                //{
                //    Session["COAChartCode"] = Reader["COAChartCode"].ToString();
                //    //Session["ParentId"] = Session["CustomerGroup"];
                //}
                //else if (Session["AccFrom"].ToString() == "E")
                //{
                //    Session["COAChartCode"] = Reader["COAChartCode"].ToString();
                //    //Session["ParentId"] = Session["EmployeeGroup"];
                //}
                //else if (Session["AccFrom"].ToString() == "S")
                //{
                    Session["COAChartCode"] = Reader["COAChartCode"].ToString();
                    //Session["ParentId"] = Session["SupplierGroup"];
                //}
                //else
                //{
                //    return;
                //}
               
                //LblNodeValue.Text = COAChartCode;
                Reader.Close();
                commitTrans();
            }
            else
            {

                Reader.Close();
                commitTrans();
                Session["HasChild"] = "N";
                //Session["COAChartCode"] = Session["CustomerGroup"];
                if (Session["AccFrom"].ToString() == "C")
                {
                    Session["COAChartCode"] = Session["CustomerGroup"];
                }
                else if (Session["AccFrom"].ToString() == "E")
                {
                    Session["COAChartCode"] = Session["EmployeeGroup"];
                }
                else if (Session["AccFrom"].ToString() == "S")
                {
                    Session["COAChartCode"] = Session["SupplierGroup"];
                }

                else if (Session["AccFrom"].ToString() == "B")
                {
                    Session["COAChartCode"] = Session["BankGroup"];
                }
                else if (Session["AccFrom"].ToString() == "CA")
                {
                    Session["COAChartCode"] = Session["CashGroup"];
                }
                else
                {
                    return;
                }
                //GetLevelChartIDLevel5();
            }

        }
        catch (Exception ex)
        {
            rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;

        }
    }
  public  void GetLevelChartIDLevel5()
    {
        GetCustomization();
        if (Session["AccFrom"].ToString() == "C")
        {
            Session["ParentId"] = Session["CustomerGroup"];
        }
        else if (Session["AccFrom"].ToString() == "E")
        {
            Session["ParentId"] = Session["EmployeeGroup"];
        }
        else if (Session["AccFrom"].ToString() == "S")
        {
            Session["ParentId"] = Session["SupplierGroup"];
        }
        else if (Session["AccFrom"].ToString() == "B")
        {

            Session["ParentId"] = Session["BankGroup"];
        }
        else if (Session["AccFrom"].ToString() == "CA")
        {

            Session["ParentId"] = Session["CashGroup"];
        }
        else
        {
            return;
        }

        CheckForChild();
        Session["Level1"] = Session["COAChartCode"].ToString().Substring(0, 1);
        Session["Level2"] = Session["COAChartCode"].ToString().Substring(1, 2);
        Session["Level3"] = Session["COAChartCode"].ToString().Substring(3, 2);
        Session["Level4"] = Session["COAChartCode"].ToString().Substring(5, 2);
        Session["Level5"] = Session["COAChartCode"].ToString().Substring(7);

        //ParentChartID = Convert.ToInt64(COAChartCode);
        string P = "";
        char pad = '0';
        if (Convert.ToInt16(Session["Level5"]) > 0)
        {
            Int16 ii = Convert.ToInt16(Session["Level5"]);
            ii += 1;
            P = ii.ToString();
            P = P.PadLeft(Session["Level5"].ToString().Length, pad);
            Session["LastLevel"] = "W";

            Session["COAChartCode"] = Session["Level1"].ToString() + Session["Level2"].ToString() + Session["Level3"].ToString() + Session["Level4"].ToString() + P;
            Session["Level"] = 5;

        }

        else
        {

            Session["Level1"] = Session["COAChartCode"].ToString().Substring(0, 1);
            Session["Level2"] = Session["COAChartCode"].ToString().Substring(1, 2);
            Session["Level3"] = Session["COAChartCode"].ToString().Substring(3, 2);
            Session["Level4"] = Session["COAChartCode"].ToString().Substring(5, 2);
            Session["Level5"] = Session["COAChartCode"].ToString().Substring(7);

            Session["COAChartCode"] = Session["Level1"].ToString() + Session["Level2"].ToString() + Session["Level3"].ToString() + Session["Level4"].ToString() + "0001".ToString();
            Session["Level"] = 5;
        }
        //TxtNewAccountNo.Text = COAChartCode;
        CreateAccountForCustomer();
    }
    void CreateAccountForCustomer()
    {



        Class1 queryResult = new Class1();
        try
        {
            Cmd.Connection.ConnectionString = GetConnStr();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "USP_ChartOfAccount_Save";
            Cmd.Parameters.Clear();
            Cmd.Parameters.Add("@ChartId", SqlDbType.Int).Value = 0;
            Cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Session["CompanyId"];
            Cmd.Parameters.Add("@COAChartName", SqlDbType.NVarChar).Value = Session["Cut"];
            Cmd.Parameters.Add("@ParentChartID", SqlDbType.NVarChar).Value = Session["ParentId"].ToString();
            Cmd.Parameters.Add("@ChartLevel", SqlDbType.NVarChar).Value = 5;
            Cmd.Parameters.Add("@COAChartCode", SqlDbType.NVarChar).Value = Session["COAChartCode"].ToString();
            Cmd.Parameters.Add("@ChartCat", SqlDbType.NVarChar).Value = "T1";
            Cmd.Parameters.Add("@LastLevel", SqlDbType.NVarChar).Value = "W";
            Cmd.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value = 1;
            //if (ChartId == 0)
            //    Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserCode"].ToString();
            //else
            Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = 0;
            queryResult.ErrorNo = 0;
            beginTrans();
            queryResult.RecordId = Convert.ToInt32(Cmd.ExecuteNonQuery());
            commitTrans();
            if (queryResult.RecordId == 0)
            {
                if (Session["dir"].ToString() == "ltr")
                {
                    Session["Success"] = String.Format(global::Resources.ResourceMain.Save);// GetLocalResourceObject("CompanyName").ToString();
                   
                }
                else
                {
                    Session["Success"] = String.Format(global::Resources.ResourceMain_Ar.Save);// GetLocalResourceObject("CompanyName").ToString();

                }

                //Session["Success"] = Resources.ResourceMain.Save;
                queryResult.ErrorNo = -1;

                queryResult.ErrorDesc = Resources.ResourceMain.ChartAlreadyExists;

                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            else if (queryResult.RecordId == -1)
            {
                queryResult.ErrorNo = -1;
                queryResult.ErrorDesc = Resources.ResourceMain.EndOfLevel;
                queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
            }
            //Cmd.CommandType = CommandType.Text;
            //Cmd.CommandText = "select COAChartCode From tChartOfAccounts where ChartId = " + queryResult.RecordId;
            //queryResult.OtherData = Cmd.ExecuteScalar();

          


        }
        catch (Exception ex)
        {
            rollBackTrans();
            queryResult.ErrorDesc = ex.Message;
            queryResult.ErrorNo = -1;
            queryResult.ErrorFunction = " Finance.DAL.ChartOfAccount.SaveGroup";
        }
    }




}

