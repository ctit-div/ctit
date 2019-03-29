using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class2
/// </summary>
public class Class2
{
    
    public int id;
    public int companyId;
     int bankGroup;
     int cashGroup;
    public int chequeGroup;
    public string profitLossLedger;
    public int employeeGroup;
    public int customerGroup;
    public int supplierGroup;
    public int postingType;
    public Boolean isVoucherNoMandatory;
    public int createdBy;
    public System.DateTime createdDate;
    public int modifiedBy;
    public System.DateTime modifiedDate;


    public int CashGroup
    {
        get
        {
            return cashGroup;
        }
        set
        {
            this.cashGroup = value;
        }
    }
    public int BankGroup
    {
        get
        {
            return this.bankGroup;
        }
        set
        {
            this.bankGroup = value;
        }
    }
    public string ProfitLossLedger
    {
        get
        {
            return this.profitLossLedger;
        }
        set
        {
            this.profitLossLedger = value;
        }
    }
    public string RandomNumberGenerate()
    {

        Random rnum = new Random();
    int randomNumber = rnum.Next(100000000, 999999999);
    int randomNumber1 = rnum.Next(100000000, 999999999);
        return randomNumber.ToString() + randomNumber1.ToString();
}
    public bool IsVoucherNoMandatory
    {
        get
        {
            return this.isVoucherNoMandatory;
        }
        set
        {
            this.isVoucherNoMandatory = value;
        }
    }
    public DateTime ModifiedDate
    {
        get
        {
            return modifiedDate;
        }
        set
        {
            this.modifiedDate = value;
        }
    }
    public int ModifiedBy
    {
        get
        {
            return this.modifiedBy;
        }
        set
        {
            this.modifiedBy = value;
        }
    }
    public int CreatedBy
    {
        get
        {
            return this.createdBy;
        }
        set
        {
            this.createdBy = value;
        }
    }
    public int PostingType
    {
        get
        {
            return this.postingType;
        }
        set
        {
            this.postingType = value;
        }
    }
    public int SupplierGroup
    {
        get
        {
            return this.supplierGroup;
        }
        set
        {
            this.supplierGroup = value;
        }
    }
    public int CustomerGroup
    {
        get
        {
            return this.customerGroup;
        }
        set
        {
            this.customerGroup = value;
        }
    }
    public int EmployeeGroup
    {
        get
        {
            return this.employeeGroup;
        }
        set
        {
            this.employeeGroup = value;
        }
    }
    public int ChequeGroup
    {
        get
        {
            return this.chequeGroup;
        }
        set
        {
            this.chequeGroup = value;
        }
    }
    public int CompanyId
    {
        get
        {
            return this.companyId;
        }
        set
        {
            this.companyId = value;
        }
    }
    public int Id
    {
        get
        {
            return this.id;
        }
        set
        {
            this.id = value;
        }
    }
}