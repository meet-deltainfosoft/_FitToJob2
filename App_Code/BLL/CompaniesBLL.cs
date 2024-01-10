using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class CompaniesBLL
{
    private string _name;
  

    private CompaniesDAL _companiesDAL;
    private GeneralDAL _generalDAL;

	public CompaniesBLL()
	{
        _companiesDAL = new CompaniesDAL();
        _generalDAL = new GeneralDAL();
    }

    ~CompaniesBLL()
    {
        _companiesDAL = null;
        _generalDAL = null;
    }

    public string Name
    {
        set
        {
            _name = value;
        }
    }

  
    public DataTable Companies()
    {
        return _companiesDAL.Companies(_name);
    }

    public DataTable ExportToExcel()
    {
        try
        {
            return _companiesDAL.ExportToExcel(_name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
