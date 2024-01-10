using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class MasterValuesBLL
{
    private string _group;

    private MasterValuesDAL _masterValuesDAL;
    private GeneralDAL _generalDAL;

    public MasterValuesBLL()
    {
        _masterValuesDAL = new MasterValuesDAL();
        _generalDAL = new GeneralDAL();
    }

    ~MasterValuesBLL()
    {
        _masterValuesDAL = null;
        _generalDAL = null;
    }

    public string Group
    {
        set
        {
            _group = value;
        }
    }

    public DataTable Groups()
    {
        return _masterValuesDAL.Groups();
    }

    public DataTable MasterValues()
    {
        return _masterValuesDAL.MasterValues(_group);
    }
    public DataTable ExportToExcel()
    {
        try
        {
            return _masterValuesDAL.ExportToExcel(_group);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
}
