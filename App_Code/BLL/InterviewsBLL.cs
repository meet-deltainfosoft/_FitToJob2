using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for InterviewsBLL
/// </summary>
public class InterviewsBLL
{
    private DateTime? _FromDt;
    private DateTime? _ToDt;
    private string _Name;
    private string _UserId;
    private string _Status;

    private InterviewsDAL _InterviewsDAL;
    private GeneralDAL _generalDAL;

    public InterviewsBLL()
    {
        _InterviewsDAL = new InterviewsDAL();
        _generalDAL = new GeneralDAL();
    }
    ~InterviewsBLL()
    {
        _generalDAL = null;
        _InterviewsDAL = null;
    }
    public DateTime? FromDt
    {
        set
        {
            _FromDt = value;
        }
    }
    public DateTime? ToDt
    {
        set
        {
            _ToDt = value;
        }
    }
    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public string UserId
    {
        set
        {
            _UserId = value;
        }
    }
    public string Status
    {
        set
        {
            _Status = value;
        }
    }
    public DataTable InterviewDetail()
    {
        try
        {
            return _InterviewsDAL.InterviewDetail(_FromDt, _ToDt, _Name, _UserId, _Status);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}