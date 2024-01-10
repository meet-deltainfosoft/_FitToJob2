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

/// <summary>
/// Summary description for InterViewFormBLL
/// </summary>
public class InterViewFormBLL
{
    private InterViewFromDTO _InterViewFromDTO;
    private InterViewFormDAL _InterViewFormDAL;
    private GeneralDAL _GeneralDAL;

    public InterViewFormBLL()
    {
        _InterViewFormDAL = new InterViewFormDAL();
        _InterViewFromDTO = new InterViewFromDTO();
        _GeneralDAL = new GeneralDAL();
        _InterViewFromDTO.IsNew = true;
    }
    public InterViewFormBLL(string InterViewFormId)
        : this()
    {
        _InterViewFromDTO.IsNew = false;
        Load(InterViewFormId);
    }

    ~InterViewFormBLL()
    {
        _InterViewFormDAL = null;
        _GeneralDAL = null;
    }

    public string InterViewFormId
    {
        get
        {
            return _InterViewFromDTO.InterviewFormId;
        }
        set
        {
            _InterViewFromDTO.InterviewFormId = value;
        }
    }

    public string FullName
    {
        get
        {
            return _InterViewFromDTO.FullName;
        }
        set
        {
            _InterViewFromDTO.FullName = value;
        }
    }

    public string PresentAddress
    {
        get
        {
            return _InterViewFromDTO.PresentAddress;
        }
        set
        {
            _InterViewFromDTO.PresentAddress = value;
        }
    }

    public string PresentPost
    {
        get
        {
            return _InterViewFromDTO.PresentPost;
        }
        set
        {
            _InterViewFromDTO.PresentPost = value;
        }
    }

    public string PresentVillage
    {
        get
        {
            return _InterViewFromDTO.PresentVillage;
        }
        set
        {
            _InterViewFromDTO.PresentVillage = value;
        }
    }

    public string PresentDistrict
    {
        get
        {
            return _InterViewFromDTO.PresentDistrict;
        }
        set
        {
            _InterViewFromDTO.PresentDistrict = value;
        }
    }



    public string PresentPinCode
    {
        get
        {
            return _InterViewFromDTO.PresentPinCode;
        }
        set
        {
            _InterViewFromDTO.PresentPinCode = value;
        }
    }
    public string PresentMobileNo
    {
        get
        {
            return _InterViewFromDTO.PresentMobileNo;
        }
        set
        {
            _InterViewFromDTO.PresentMobileNo = value;
        }
    }
    public string PermanentAddress
    {
        get
        {
            return _InterViewFromDTO.PermanentAddress;
        }
        set
        {
            _InterViewFromDTO.PermanentAddress = value;
        }
    }
    public string PermanentPost
    {
        get
        {
            return _InterViewFromDTO.PermanentPost;
        }
        set
        {
            _InterViewFromDTO.PermanentPost = value;
        }
    }
    public string PermanentVillage
    {
        get
        {
            return _InterViewFromDTO.PermanentVillage;
        }
        set
        {
            _InterViewFromDTO.PermanentVillage = value;
        }
    }
    public string PermanentDistrict
    {
        get
        {
            return _InterViewFromDTO.PermanentDistrict;
        }
        set
        {
            _InterViewFromDTO.PermanentDistrict = value;
        }
    }
    public string PermanentPinCode
    {
        get
        {
            return _InterViewFromDTO.PermanentPinCode;
        }
        set
        {
            _InterViewFromDTO.PermanentPinCode = value;
        }
    }
    public string PermanentMobileNo
    {
        get
        {
            return _InterViewFromDTO.PermanentMobileNo;
        }
        set
        {
            _InterViewFromDTO.PermanentMobileNo = value;
        }
    }
    public DateTime? DOB
    {
        get
        {
            return _InterViewFromDTO.DOB;
        }
        set
        {
            _InterViewFromDTO.DOB = value;
        }
    }
    public string BloodGroup
    {
        get
        {
            return _InterViewFromDTO.BloodGroup;
        }
        set
        {
            _InterViewFromDTO.BloodGroup = value;
        }
    }
    public string AadharCardNo
    {
        get
        {
            return _InterViewFromDTO.AadharCardNo;
        }
        set
        {
            _InterViewFromDTO.AadharCardNo = value;
        }
    }
    public string PanCardNo
    {
        get
        {
            return _InterViewFromDTO.PanCardNo;
        }
        set
        {
            _InterViewFromDTO.PanCardNo = value;
        }
    }
    public string ElectionCardNo
    {
        get
        {
            return _InterViewFromDTO.ElectionCardNo;
        }
        set
        {
            _InterViewFromDTO.ElectionCardNo = value;
        }
    }
    public string Category
    {
        get
        {
            return _InterViewFromDTO.Category;
        }
        set
        {
            _InterViewFromDTO.Category = value;
        }
    }
    public string mailto
    {
        get
        {
            return _InterViewFromDTO.mailto;
        }
        set
        {
            _InterViewFromDTO.mailto = value;
        }
    }
    public string FatherName
    {
        get
        {
            return _InterViewFromDTO.FatherName;
        }
        set
        {
            _InterViewFromDTO.FatherName = value;
        }
    }
    public string FatherOccupation
    {
        get
        {
            return _InterViewFromDTO.FatherOccupation;
        }
        set
        {
            _InterViewFromDTO.FatherOccupation = value;
        }
    }
    public string FatherEducation
    {
        get
        {
            return _InterViewFromDTO.FatherEducation;
        }
        set
        {
            _InterViewFromDTO.FatherEducation = value;
        }
    }
    public string FatherMobileNo
    {
        get
        {
            return _InterViewFromDTO.FatherMobileNo;
        }
        set
        {
            _InterViewFromDTO.FatherMobileNo = value;
        }
    }
    public string MotherName
    {
        get
        {
            return _InterViewFromDTO.MotherName;
        }
        set
        {
            _InterViewFromDTO.MotherName = value;
        }
    }
    public string MotherOccupation
    {
        get
        {
            return _InterViewFromDTO.MotherOccupation;
        }
        set
        {
            _InterViewFromDTO.MotherOccupation = value;
        }
    }
    public string MotherEducation
    {
        get
        {
            return _InterViewFromDTO.MotherEducation;
        }
        set
        {
            _InterViewFromDTO.MotherEducation = value;
        }
    }
    public string MotherMobileNo
    {
        get
        {
            return _InterViewFromDTO.MotherMobileNo;
        }
        set
        {
            _InterViewFromDTO.MotherMobileNo = value;
        }
    }
    public string WifeName
    {
        get
        {
            return _InterViewFromDTO.WifeName;
        }
        set
        {
            _InterViewFromDTO.WifeName = value;
        }
    }
    public string WifeOccupation
    {
        get
        {
            return _InterViewFromDTO.WifeOccupation;
        }
        set
        {
            _InterViewFromDTO.WifeOccupation = value;
        }
    }
    public string WifeEducation
    {
        get
        {
            return _InterViewFromDTO.WifeEducation;
        }
        set
        {
            _InterViewFromDTO.WifeEducation = value;
        }
    }
    public string WifeMobileNo
    {
        get
        {
            return _InterViewFromDTO.WifeMobileNo;
        }
        set
        {
            _InterViewFromDTO.WifeMobileNo = value;
        }
    }
    public string BrotherName
    {
        get
        {
            return _InterViewFromDTO.BrotherName;
        }
        set
        {
            _InterViewFromDTO.BrotherName = value;
        }
    }
    public string BrotherOccupation
    {
        get
        {
            return _InterViewFromDTO.BrotherOccupation;
        }
        set
        {
            _InterViewFromDTO.BrotherOccupation = value;
        }
    }
    public string BrotherEducation
    {
        get
        {
            return _InterViewFromDTO.BrotherEducation;
        }
        set
        {
            _InterViewFromDTO.BrotherEducation = value;
        }
    }
    public string BrotherMobileNo
    {
        get
        {
            return _InterViewFromDTO.BrotherMobileNo;
        }
        set
        {
            _InterViewFromDTO.BrotherMobileNo = value;
        }
    }
    public string NomineeName
    {
        get
        {
            return _InterViewFromDTO.NomineeName;
        }
        set
        {
            _InterViewFromDTO.NomineeName = value;
        }
    }
    public DateTime? NomineeDOB
    {
        get
        {
            return _InterViewFromDTO.NomineeDOB;
        }
        set
        {
            _InterViewFromDTO.NomineeDOB = value;
        }
    }
    public string NomineeRelation
    {
        get
        {
            return _InterViewFromDTO.NomineeRelation;
        }
        set
        {
            _InterViewFromDTO.NomineeRelation = value;
        }
    }
    public Int16? NomineeAge
    {
        get
        {
            return _InterViewFromDTO.NomineeAge;
        }
        set
        {
            _InterViewFromDTO.NomineeAge = value;
        }
    }
    public string Standanrd10Subject
    {
        get
        {
            return _InterViewFromDTO.Standanrd10Subject;
        }
        set
        {
            _InterViewFromDTO.Standanrd10Subject = value;
        }
    }
    public Int16? Standanrd10PassingYear
    {
        get
        {
            return _InterViewFromDTO.Standanrd10PassingYear;
        }
        set
        {
            _InterViewFromDTO.Standanrd10PassingYear = value;
        }
    }
    public decimal? Standanrd10Percentage
    {
        get
        {
            return _InterViewFromDTO.Standanrd10Percentage;
        }
        set
        {
            _InterViewFromDTO.Standanrd10Percentage = value;
        }
    }
    public string Standanrd12Subject
    {
        get
        {
            return _InterViewFromDTO.Standanrd12Subject;
        }
        set
        {
            _InterViewFromDTO.Standanrd12Subject = value;
        }
    }
    public Int16? Standanrd12PassingYear
    {
        get
        {
            return _InterViewFromDTO.Standanrd12PassingYear;
        }
        set
        {
            _InterViewFromDTO.Standanrd12PassingYear = value;
        }
    }
    public decimal? Standanrd12Percentage
    {
        get
        {
            return _InterViewFromDTO.Standanrd12Percentage;
        }
        set
        {
            _InterViewFromDTO.Standanrd12Percentage = value;
        }
    }
    public string GraduateSubject
    {
        get
        {
            return _InterViewFromDTO.GraduateSubject;
        }
        set
        {
            _InterViewFromDTO.GraduateSubject = value;
        }
    }
    public Int16? GraduatePassingYear
    {
        get
        {
            return _InterViewFromDTO.GraduatePassingYear;
        }
        set
        {
            _InterViewFromDTO.GraduatePassingYear = value;
        }
    }
    public decimal? GraduatePercentage
    {
        get
        {
            return _InterViewFromDTO.GraduatePercentage;
        }
        set
        {
            _InterViewFromDTO.GraduatePercentage = value;
        }
    }
    public string PostGraduateSubject
    {
        get
        {
            return _InterViewFromDTO.PostGraduateSubject;
        }
        set
        {
            _InterViewFromDTO.PostGraduateSubject = value;
        }
    }
    public Int16? PostGraduatePassingYear
    {
        get
        {
            return _InterViewFromDTO.PostGraduatePassingYear;
        }
        set
        {
            _InterViewFromDTO.PostGraduatePassingYear = value;
        }
    }
    public decimal? PostGraduatePercentage
    {
        get
        {
            return _InterViewFromDTO.PostGraduatePercentage;
        }
        set
        {
            _InterViewFromDTO.PostGraduatePercentage = value;
        }
    }
    public string OtherSubject
    {
        get
        {
            return _InterViewFromDTO.OtherSubject;
        }
        set
        {
            _InterViewFromDTO.OtherSubject = value;
        }
    }
    public Int16? OtherPassingYear
    {
        get
        {
            return _InterViewFromDTO.OtherPassingYear;
        }
        set
        {
            _InterViewFromDTO.OtherPassingYear = value;
        }
    }
    public decimal? OtherPercentage
    {
        get
        {
            return _InterViewFromDTO.OtherPercentage;
        }
        set
        {
            _InterViewFromDTO.OtherPercentage = value;
        }
    }
    public string CertificateCourseName
    {
        get
        {
            return _InterViewFromDTO.CertificateCourseName;
        }
        set
        {
            _InterViewFromDTO.CertificateCourseName = value;
        }
    }
    public Int16? CertificateCourseYear
    {
        get
        {
            return _InterViewFromDTO.CertificateCourseYear;
        }
        set
        {
            _InterViewFromDTO.CertificateCourseYear = value;
        }
    }
    public string TrainingName
    {
        get
        {
            return _InterViewFromDTO.TrainingName;
        }
        set
        {
            _InterViewFromDTO.TrainingName = value;
        }
    }
    public Int16? TrainingYear
    {
        get
        {
            return _InterViewFromDTO.TrainingYear;
        }
        set
        {
            _InterViewFromDTO.TrainingYear = value;
        }
    }
    public string MedalName
    {
        get
        {
            return _InterViewFromDTO.MedalName;
        }
        set
        {
            _InterViewFromDTO.MedalName = value;
        }
    }
    public Int16? MedalYear
    {
        get
        {
            return _InterViewFromDTO.MedalYear;
        }
        set
        {
            _InterViewFromDTO.MedalYear = value;
        }
    }
    public string FirstCompanyName
    {
        get
        {
            return _InterViewFromDTO.FirstCompanyName;
        }
        set
        {
            _InterViewFromDTO.FirstCompanyName = value;
        }
    }
    public string FirstCompanyDesignation
    {
        get
        {
            return _InterViewFromDTO.FirstCompanyDesignation;
        }
        set
        {
            _InterViewFromDTO.FirstCompanyDesignation = value;
        }
    }
    public decimal? FirstCompanyExp
    {
        get
        {
            return _InterViewFromDTO.FirstCompanyExp;
        }
        set
        {
            _InterViewFromDTO.FirstCompanyExp = value;
        }
    }
    public decimal? FirstCompanySalary
    {
        get
        {
            return _InterViewFromDTO.FirstCompanySalary;
        }
        set
        {
            _InterViewFromDTO.FirstCompanySalary = value;
        }
    }
    public string SecondCompanyName
    {
        get
        {
            return _InterViewFromDTO.SecondCompanyName;
        }
        set
        {
            _InterViewFromDTO.SecondCompanyName = value;
        }
    }
    public string SecondCompanyDesignation
    {
        get
        {
            return _InterViewFromDTO.SecondCompanyDesignation;
        }
        set
        {
            _InterViewFromDTO.SecondCompanyDesignation = value;
        }
    }
    public decimal? SecondCompanyExp
    {
        get
        {
            return _InterViewFromDTO.SecondCompanyExp;
        }
        set
        {
            _InterViewFromDTO.SecondCompanyExp = value;
        }
    }
    public decimal? SecondCompanySalary
    {
        get
        {
            return _InterViewFromDTO.SecondCompanySalary;
        }
        set
        {
            _InterViewFromDTO.SecondCompanySalary = value;
        }
    }
    public string ThirdCompanyName
    {
        get
        {
            return _InterViewFromDTO.ThirdCompanyName;
        }
        set
        {
            _InterViewFromDTO.ThirdCompanyName = value;
        }
    }
    public string ThirdCompanyDesignation
    {
        get
        {
            return _InterViewFromDTO.ThirdCompanyDesignation;
        }
        set
        {
            _InterViewFromDTO.ThirdCompanyDesignation = value;
        }
    }
    public decimal? ThirdCompanyExp
    {
        get
        {
            return _InterViewFromDTO.ThirdCompanyExp;
        }
        set
        {
            _InterViewFromDTO.ThirdCompanyExp = value;
        }
    }
    public decimal? ThirdCompanySalary
    {
        get
        {
            return _InterViewFromDTO.ThirdCompanySalary;
        }
        set
        {
            _InterViewFromDTO.ThirdCompanySalary = value;
        }
    }
    public string OtherExpNoExpDetails
    {
        get
        {
            return _InterViewFromDTO.OtherExpNoExpDetails;
        }
        set
        {
            _InterViewFromDTO.OtherExpNoExpDetails = value;
        }
    }

    public string adharcard
    {
        set
        {
            _InterViewFromDTO.adharcard = value;
        }
        get
        {
            return _InterViewFromDTO.adharcard;
        }
    }

    public string electioncard
    {
        set
        {
            _InterViewFromDTO.electioncard = value;
        }
        get
        {
            return _InterViewFromDTO.electioncard;
        }
    }

    public string rationcard1
    {
        set
        {
            _InterViewFromDTO.rationcard1 = value;
        }
        get
        {
            return _InterViewFromDTO.rationcard1;
        }
    }

    public string rationcard2
    {
        set
        {
            _InterViewFromDTO.rationcard2 = value;
        }
        get
        {
            return _InterViewFromDTO.rationcard2;
        }
    }

    public string pancard
    {
        set
        {
            _InterViewFromDTO.pancard = value;
        }
        get
        {
            return _InterViewFromDTO.pancard;
        }
    }

    public string photo
    {
        set
        {
            _InterViewFromDTO.photo = value;
        }
        get
        {
            return _InterViewFromDTO.photo;
        }
    }

    public string marksheet
    {
        set
        {
            _InterViewFromDTO.marksheet = value;
        }
        get
        {
            return _InterViewFromDTO.marksheet;
        }
    }

    public string certificate
    {
        set
        {
            _InterViewFromDTO.certificate = value;
        }
        get
        {
            return _InterViewFromDTO.certificate;
        }
    }

    public string leavingcertificate1
    {
        set
        {
            _InterViewFromDTO.leavingcertificate1 = value;
        }
        get
        {
            return _InterViewFromDTO.leavingcertificate1;
        }
    }

    public string leavingcertificate2
    {
        set
        {
            _InterViewFromDTO.leavingcertificate2 = value;
        }
        get
        {
            return _InterViewFromDTO.leavingcertificate2;
        }
    }

    public string salaryslip
    {
        set
        {
            _InterViewFromDTO.salaryslip = value;
        }
        get
        {
            return _InterViewFromDTO.salaryslip;
        }
    }

    public string appointmentletter
    {
        set
        {
            _InterViewFromDTO.appointmentletter = value;
        }
        get
        {
            return _InterViewFromDTO.appointmentletter;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        //Name
        if (_InterViewFromDTO.FullName == null)
        {
            sl.Add("FullName", "FullName cannot be blank.");
        }
        if (_InterViewFromDTO.PresentAddress == null)
        {
            sl.Add("PresentAddress", "PresentAddress cannot be blank.");
        }
        if (_InterViewFromDTO.PresentMobileNo == null)
        {
            sl.Add("PresentMobileNo", "Present MobileNo cannot be blank.");
        }
        if (_InterViewFromDTO.DOB == null)
        {
            sl.Add("DOB", "DOB cannot be blank.");
        }
        if (_InterViewFromDTO.AadharCardNo == null)
        {
            sl.Add("AadharCardNo", "AadharCardNo cannot be blank.");
        }
        if (_InterViewFromDTO.PresentVillage == null)
        {
            sl.Add("PresentVillage", "Present Village cannot be blank.");
        }
        if (_InterViewFromDTO.PresentDistrict == null)
        {
            sl.Add("PresentDistrict", "PresentDistrict cannot be blank.");
        }

        ////Name
        return sl;
    }

    public void Save()
    {
        try
        {
            if (_InterViewFromDTO.IsNew == true)
                _InterViewFormDAL.Insert(_InterViewFromDTO);
            else
                _InterViewFormDAL.Update(_InterViewFromDTO);
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
    public void Load(string InterViewFormId)
    {
        ArrayList alRegistrationLns = new ArrayList();
        _InterViewFromDTO = _InterViewFormDAL.Select(InterViewFormId);
    }

    public void Delete(string InterViewFormId)
    {
        try
        {
            _InterViewFormDAL.Delete(InterViewFormId);
        }
        catch (Exception)
        {
            throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }

    public DataTable LoadStandard()
    {
        try
        {
            return _GeneralDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadDivision()
    {
        try
        {
            return _GeneralDAL.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void DeleteRegistration(ArrayList al)
    {
        try
        {
            _InterViewFormDAL.DeleteRegistration(al);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
