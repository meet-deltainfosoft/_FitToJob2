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

/// <summary>
/// Summary description for HomeWorkBLL
/// </summary>
public class HomeWorkBLL
{
    private HomeWorkDTO _homeWorkDTO;
    private HomeWorkDAL _homeWorkDAL;
    private GeneralDAL _generalDAL;

    private bool _isNew;

    public HomeWorkBLL()
    {
        _homeWorkDTO = new HomeWorkDTO();
        _homeWorkDAL = new HomeWorkDAL();
        _generalDAL = new GeneralDAL();

        _isNew = true;
    }
    public HomeWorkBLL(string HomeWorkId)
        : this()
    {
        _isNew = false;
        Load(HomeWorkId);
    }
    ~HomeWorkBLL()
    {
        _homeWorkDAL = null;
        _homeWorkDTO = null;
        _generalDAL = null;
    }
    public string HomeWorkId
    {
        get
        {
            return _homeWorkDTO.HomeWorkId;
        }
        set
        {
            _homeWorkDTO.HomeWorkId = value;
        }
    }
    public string SubId
    {
        get
        {
            return _homeWorkDTO.SubId;
        }
        set
        {
            _homeWorkDTO.SubId = value;
        }
    }

    public string SubIdTo
    {
        get
        {
            return _homeWorkDTO.SubIdTo;
        }
        set
        {
            _homeWorkDTO.SubIdTo = value;
        }
    }
    public string Subject
    {
        get
        {
            return _homeWorkDTO.Subject;
        }
        set
        {
            _homeWorkDTO.Subject = value;
        }
    }


    public string SubLnId
    {
        get
        {
            return _homeWorkDTO.SubLnId;
        }
        set
        {
            _homeWorkDTO.SubLnId = value;
        }
    }
    public string HomeWork
    {
        get
        {
            return _homeWorkDTO.HomeWork;
        }
        set
        {
            _homeWorkDTO.HomeWork = value;
        }
    }
    public string A1
    {
        get
        {
            return _homeWorkDTO.A1;
        }
        set
        {
            _homeWorkDTO.A1 = value;
        }
    }
    public string A2
    {
        get
        {
            return _homeWorkDTO.A2;
        }
        set
        {
            _homeWorkDTO.A2 = value;
        }
    }

    public string ImageNameQus
    {
        get
        {
            return _homeWorkDTO.ImageNameQus;
        }
        set
        {
            _homeWorkDTO.ImageNameQus = value;
        }
    }

    public string ImageNameA1
    {
        get
        {
            return _homeWorkDTO.ImageNameA1;
        }
        set
        {
            _homeWorkDTO.ImageNameA1 = value;
        }
    }
    public string ImageNameA2
    {
        get
        {
            return _homeWorkDTO.ImageNameA2;
        }
        set
        {
            _homeWorkDTO.ImageNameA2 = value;
        }
    }
    public string ImageNameA3
    {
        get
        {
            return _homeWorkDTO.ImageNameA3;
        }
        set
        {
            _homeWorkDTO.ImageNameA3 = value;
        }
    }
    public string ImageNameA4
    {
        get
        {
            return _homeWorkDTO.ImageNameA4;
        }
        set
        {
            _homeWorkDTO.ImageNameA4 = value;
        }
    }
    public string A3
    {
        get
        {
            return _homeWorkDTO.A3;
        }
        set
        {
            _homeWorkDTO.A3 = value;
        }
    }
    public string A4
    {
        get
        {
            return _homeWorkDTO.A4;
        }
        set
        {
            _homeWorkDTO.A4 = value;
        }
    }
    public string Ans
    {
        get
        {
            return _homeWorkDTO.Ans;
        }
        set
        {
            _homeWorkDTO.Ans = value;
        }
    }
    public byte[] HomeWorkImg
    {
        get
        {
            return _homeWorkDTO.HomeWorkImg;
        }
        set
        {
            _homeWorkDTO.HomeWorkImg = value;
        }
    }
    public bool HomeWorkImgSelected
    {
        get
        {
            return _homeWorkDTO.HomeWorkImgSelected;
        }
        set
        {
            _homeWorkDTO.HomeWorkImgSelected = value;
        }
    }
    public byte[] A1Img
    {
        get
        {
            return _homeWorkDTO.A1Img;
        }
        set
        {
            _homeWorkDTO.A1Img = value;
        }
    }
    public bool A1ImgSelected
    {
        get
        {
            return _homeWorkDTO.A1ImgSelected;
        }
        set
        {
            _homeWorkDTO.A1ImgSelected = value;
        }
    }
    public byte[] A2Img
    {
        get
        {
            return _homeWorkDTO.A2Img;
        }
        set
        {
            _homeWorkDTO.A2Img = value;
        }
    }
    public bool A2ImgSelected
    {
        get
        {
            return _homeWorkDTO.A2ImgSelected;
        }
        set
        {
            _homeWorkDTO.A2ImgSelected = value;
        }
    }
    public byte[] A3Img
    {
        get
        {
            return _homeWorkDTO.A3Img;
        }
        set
        {
            _homeWorkDTO.A3Img = value;
        }
    }
    public bool A3ImgSelected
    {
        get
        {
            return _homeWorkDTO.A3ImgSelected;
        }
        set
        {
            _homeWorkDTO.A3ImgSelected = value;
        }
    }
    public byte[] A4Img
    {
        get
        {
            return _homeWorkDTO.A4Img;
        }
        set
        {
            _homeWorkDTO.A4Img = value;
        }
    }
    public bool A4ImgSelected
    {
        get
        {
            return _homeWorkDTO.A4ImgSelected;
        }
        set
        {
            _homeWorkDTO.A4ImgSelected = value;
        }
    }
    public bool IsNew
    {
        get
        {
            return _homeWorkDTO.IsNew;
        }
    }

    public string ChapterId
    {
        get
        {
            return _homeWorkDTO.ChapterId;
        }
        set
        {
            _homeWorkDTO.ChapterId = value;
        }
    }

    public string TestIdTo
    {
        get
        {
            return _homeWorkDTO.TestIdTo;
        }
        set
        {
            _homeWorkDTO.TestIdTo = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _homeWorkDTO.StandardTextListId;
        }
        set
        {
            _homeWorkDTO.StandardTextListId = value;
        }
    }

    public void Save()
    {
        try
        {
            if (_isNew == true)
            {
                _homeWorkDAL.Insert(_homeWorkDTO);
            }
            else
            {
                _homeWorkDAL.Update(_homeWorkDTO);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int? SrNo
    {
        get
        {
            return _homeWorkDTO.SrNo;
        }
        set
        {
            _homeWorkDTO.SrNo = value;
        }
    }

    public bool? IsHomeWorkChanged
    {
        get
        {
            return _homeWorkDTO.IsHomeWorkChanged;
        }
        set
        {
            _homeWorkDTO.IsHomeWorkChanged = value;
        }
    }

    public bool? IsA1Changed
    {
        get
        {
            return _homeWorkDTO.IsA1Changed;
        }
        set
        {
            _homeWorkDTO.IsA1Changed = value;
        }
    }

    public bool? IsA2Changed
    {
        get
        {
            return _homeWorkDTO.IsA2Changed;
        }
        set
        {
            _homeWorkDTO.IsA2Changed = value;
        }
    }

    public bool? IsA3Changed
    {
        get
        {
            return _homeWorkDTO.IsA3Changed;
        }
        set
        {
            _homeWorkDTO.IsA3Changed = value;
        }
    }

    public bool? IsA4Changed
    {
        get
        {
            return _homeWorkDTO.IsA4Changed;
        }
        set
        {
            _homeWorkDTO.IsA4Changed = value;
        }
    }
    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_homeWorkDTO.SubId == null)
        {
            sl.Add("Subject", " Designation cannot be blank");
        }

        if (_homeWorkDTO.ChapterId == null)
        {
            sl.Add("ChapterId", "Chapter can Not be blank");
        }

        if (_homeWorkDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank");
        }

        if (_homeWorkDTO.SrNo == null)
        {
            sl.Add("SrNo", "SrNo can Not be blank");
        }

        if (_homeWorkDTO.HomeWorkType == null)
        {
            sl.Add("HomeWorkType", "HomeWork type cannot be blank");
        }

        //if ((_homeWorkDTO.HomeWork != null) && (_homeWorkDTO.ImageNameQus != null))
        //{
        //sl.Add("HomeWork", "Please Enter HomeWork Either Image HomeWork");
        //}

        if ((_homeWorkDTO.HomeWork == null) && (_homeWorkDTO.ImageNameQus == null))
        {
            sl.Add("HomeWork", "HomeWork can Not be blank");
        }
        else
        {
            if ((_homeWorkDTO.ImageNameQus == null))
            {
                if (_homeWorkDTO.HomeWork == null)
                {
                    sl.Add("HomeWork", "HomeWork can Not be blank");
                }
                else
                {
                    if (_homeWorkDTO.HomeWork.Length >= 350)
                    {
                        sl.Add("HomeWork", "HomeWork Cannot be More Than 350 character.");
                    }
                }
            }
        }

        if (_homeWorkDTO.HomeWorkType != null)
        {
            if (_homeWorkDTO.HomeWorkType.ToString().ToUpper() == "MCQ".ToString().ToUpper()) //MCQ compulsary case
            {
                //if ((_homeWorkDTO.A1 != null) && (_homeWorkDTO.ImageNameA1 != null))
                //{
                //    sl.Add("A1", "Please Enter A1 Either Image A1");
                //}
                //if ((_homeWorkDTO.A2 != null) && (_homeWorkDTO.ImageNameA2 != null))
                //{
                //    sl.Add("A2", "Please Enter A2 Either Image A2");
                //}
                //if ((_homeWorkDTO.A3 != null) && (_homeWorkDTO.ImageNameA3 != null))
                //{
                //    sl.Add("A3", "Please Enter A3 Either Image A3");
                //}
                //if ((_homeWorkDTO.A4 != null) && (_homeWorkDTO.ImageNameA4 != null))
                //{
                //    sl.Add("A4", "Please Enter A4 Either Image A4");
                //}

                //if ((_homeWorkDTO.A1 == null) && (_homeWorkDTO.ImageNameA1 == null))
                //{
                //    sl.Add("A1", "A1 can Not be blank");
                //}
                //else
                //{
                //    if ((_homeWorkDTO.A1 != null))
                //    {
                //        if (_homeWorkDTO.A1 == null)
                //        {
                //            sl.Add("A1", "A1 can Not be blank");
                //        }
                //        else
                //        {
                //            if (_homeWorkDTO.A1.Length >= 350)
                //            {
                //                sl.Add("A1", "A1 Cannot be More Than 350 character.");
                //            }
                //        }
                //    }
                //}

                //if ((_homeWorkDTO.A2 == null) && (_homeWorkDTO.ImageNameA2 == null))
                //{
                //    sl.Add("A2", "A2 can Not be blank");
                //}
                //else
                //{
                //    if ((_homeWorkDTO.A2 != null))
                //    {
                //        if (_homeWorkDTO.A2 == null)
                //        {
                //            sl.Add("A2", "A2 can Not be blank");
                //        }
                //        else
                //        {
                //            if (_homeWorkDTO.A2.Length >= 350)
                //            {
                //                sl.Add("A2", "A2 Cannot be More Than 350 character.");
                //            }
                //        }
                //    }
                //}

                //if ((_homeWorkDTO.A3 == null) && (_homeWorkDTO.ImageNameA3 == null))
                //{
                //    sl.Add("A3", "A3 can Not be blank");
                //}
                //else
                //{
                //    if ((_homeWorkDTO.A3 != null))
                //    {
                //        if (_homeWorkDTO.A3 == null)
                //        {
                //            sl.Add("A3", "A3 can Not be blank");
                //        }
                //        else
                //        {
                //            if (_homeWorkDTO.A3.Length >= 350)
                //            {
                //                sl.Add("A3", "A3 Cannot be More Than 350 character.");
                //            }
                //        }
                //    }
                //}

                //if ((_homeWorkDTO.A4 == null) && (_homeWorkDTO.ImageNameA4 == null))
                //{
                //    sl.Add("A4", "A4 can Not be blank");
                //}
                //else
                //{
                //    if ((_homeWorkDTO.A4 != null))
                //    {
                //        if (_homeWorkDTO.A4 == null)
                //        {
                //            sl.Add("A4", "A4 can Not be blank");
                //        }
                //        else
                //        {
                //            if (_homeWorkDTO.A4.Length >= 350)
                //            {
                //                sl.Add("A4", "A4 Cannot be More Than 350 character.");
                //            }
                //        }
                //    }
                //}

                if (_homeWorkDTO.Ans == null)
                {
                    sl.Add("Answer", "Answer can Not be blank");
                }

                if (_homeWorkDTO.AnsSelection == null)
                {
                    sl.Add("AnsSelection", "Answer selection type can Not be blank");
                }
            }
            else if (_homeWorkDTO.HomeWorkType.ToString().ToUpper() == "NONMCQ".ToString().ToUpper()) //NONMCQ compulsary case
            {
                if (_homeWorkDTO.HomeWorkDataType == null)
                {
                    sl.Add("HomeWorkDataType", "HomeWork datatype cannot be blank");
                }
            }
        }

        if (_homeWorkDTO.Dt == null)
            sl.Add("Dt", "Date can not be blank.");

        return sl;
    }

    public void Load(string HomeWorkId)
    {
        _homeWorkDTO = _homeWorkDAL.Select(HomeWorkId);
    }

    public void Delete(string HomeWorkId)
    {
        string isReferenced = _homeWorkDAL.IsReferenced(HomeWorkId);
        try
        {
            if (isReferenced != "")
            {
                throw new Exception("HomeWork is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _homeWorkDAL.Delete(HomeWorkId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "HomeWork is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception(ex.Message);
        }
    }

    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadSubjects(string StandardTextListId)
    {
        try
        {
            return _homeWorkDAL.LoadSubjects(StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadChapters()
    {
        try
        {
            return _homeWorkDAL.LoadChapters(_homeWorkDTO.SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadChaptersTo()
    {
        try
        {
            return _homeWorkDAL.LoadChapters(_homeWorkDTO.SubIdTo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadChapterVideo()
    {
        try
        {
            return _homeWorkDAL.LoadChapterVideo(_homeWorkDTO.ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int getSrNo()
    {
        try
        {
            return _homeWorkDAL.getSrNo(_homeWorkDTO.SubId, _homeWorkDTO.ChapterId, _homeWorkDTO.ChapterVideoId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public string HomeWorkType
    {
        get
        {
            return _homeWorkDTO.HomeWorkType;
        }
        set
        {
            _homeWorkDTO.HomeWorkType = value;
        }
    }
    public string HomeWorkDataType
    {
        get
        {
            return _homeWorkDTO.HomeWorkDataType;
        }
        set
        {
            _homeWorkDTO.HomeWorkDataType = value;
        }
    }

    public void DeleteHomeWork(ArrayList al)
    {
        try
        {
            _homeWorkDAL.DeleteHomeWork(al);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void PasteHomeWorkFrom(ArrayList al)
    {
        try
        {
            _homeWorkDAL.PasteHomeWorkFrom(al, _homeWorkDTO);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int? NoOfFile
    {
        get
        {
            return _homeWorkDTO.NoOfFile;
        }
        set
        {
            _homeWorkDTO.NoOfFile = value;
        }
    }
    public string SampleAns1
    {
        get
        {
            return _homeWorkDTO.SampleAns1;
        }
        set
        {
            _homeWorkDTO.SampleAns1 = value;
        }
    }
    public string SampleAns2
    {
        get
        {
            return _homeWorkDTO.SampleAns2;
        }
        set
        {
            _homeWorkDTO.SampleAns2 = value;
        }
    }
    public string SampleAns3
    {
        get
        {
            return _homeWorkDTO.SampleAns3;
        }
        set
        {
            _homeWorkDTO.SampleAns3 = value;
        }
    }
    public string SampleAns4
    {
        get
        {
            return _homeWorkDTO.SampleAns4;
        }
        set
        {
            _homeWorkDTO.SampleAns4 = value;
        }
    }

    public bool? IsSampleAns1Changed
    {
        get
        {
            return _homeWorkDTO.IsSampleAns1Changed;
        }
        set
        {
            _homeWorkDTO.IsSampleAns1Changed = value;
        }
    }

    public bool? IsSampleAns2Changed
    {
        get
        {
            return _homeWorkDTO.IsSampleAns2Changed;
        }
        set
        {
            _homeWorkDTO.IsSampleAns2Changed = value;
        }
    }

    public bool? IsSampleAns3Changed
    {
        get
        {
            return _homeWorkDTO.IsSampleAns3Changed;
        }
        set
        {
            _homeWorkDTO.IsSampleAns3Changed = value;
        }
    }

    public bool? IsSampleAns4Changed
    {
        get
        {
            return _homeWorkDTO.IsSampleAns4Changed;
        }
        set
        {
            _homeWorkDTO.IsSampleAns4Changed = value;
        }
    }

    public string AnsSelection
    {
        get
        {
            return _homeWorkDTO.AnsSelection;
        }
        set
        {
            _homeWorkDTO.AnsSelection = value;
        }
    }


    public DateTime? Dt
    {
        get
        {
            return _homeWorkDTO.Dt;
        }
        set
        {
            _homeWorkDTO.Dt = value;
        }
    }


    public int? ChapterVideoId
    {
        get
        {
            return _homeWorkDTO.ChapterVideoId;
        }
        set
        {
            _homeWorkDTO.ChapterVideoId = value;
        }
    }
}
