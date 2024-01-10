using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

public class TestQueBanksBLL
{
    public string _Name;
    public bool _AllRecord;

    private TestQueBanksDAL _testQueBanksDAL;

    public TestQueBanksBLL()
    {
        _testQueBanksDAL = new TestQueBanksDAL();
    }
    ~TestQueBanksBLL()
    {
        _testQueBanksDAL = null;
    }

    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable Subs()
    {
        return _testQueBanksDAL.TestQueBanks(_Name, _AllRecord);
    }
}
