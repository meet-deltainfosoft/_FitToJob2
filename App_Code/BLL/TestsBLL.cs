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

public class TestsBLL
{
    public string _Name;
    public bool _AllRecord;

    private TestsDAL _testsDAL;

    public TestsBLL()
    {
        _testsDAL = new TestsDAL();
    }
    ~TestsBLL()
    {
        _testsDAL = null;
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
        return _testsDAL.Tests(_Name, _AllRecord);
    }
}
