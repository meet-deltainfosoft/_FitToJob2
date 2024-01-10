using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResumeUploadBLL
/// </summary>
public class ResumeUploadBLL
{

    private ResumeUploadDAL _ResumeUploadDAL;
    private GeneralDAL _generalDAL;
    private ResumeUploadDTO _ResumeUploadDTO;
    public ResumeUploadBLL()
    {
        _ResumeUploadDAL = new ResumeUploadDAL();
        _generalDAL = new GeneralDAL();
        _ResumeUploadDTO = new ResumeUploadDTO();
        _ResumeUploadDTO.IsNew = true;
        _ResumeUploadDTO.ResumeId = _generalDAL.GetNEWID();
    }
    ~ResumeUploadBLL()
    {
        _ResumeUploadDAL = null;
        _generalDAL = null;
        _ResumeUploadDTO = null;
    }
    public ResumeUploadBLL(string RegistrationId)
     : this()
    {
        _ResumeUploadDTO.IsNew = false;
        
    }


   

    public string ResumeId
    {
        set
        {
            _ResumeUploadDTO.ResumeId = value;
        }
        get
        {
            return _ResumeUploadDTO.ResumeId;
        }
    }
    public string ResumeName
    {
        set
        {
            _ResumeUploadDTO.ResumeName = value;
        }
        get
        {
            return _ResumeUploadDTO.ResumeName;
        }
    }

    public string UplaodResume
    {
        set
        {
            _ResumeUploadDTO.UplaodResume = value;
        }
        get
        {
            return _ResumeUploadDTO.UplaodResume;
        }
    }

    public bool IsNew
    {
        get
        {
            return _ResumeUploadDTO.IsNew;
        }
    }

    public void Save()
    {
        try
        {
            _ResumeUploadDAL.UpdateResume(_ResumeUploadDTO);
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
}