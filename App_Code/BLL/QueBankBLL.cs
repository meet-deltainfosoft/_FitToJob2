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
/// Summary description for QueBLL
/// </summary>
public class QueBankBLL
{
    private QueBankDTO _queBankDTO;
    private QueBankDAL _queBankDAL;
    private GeneralDAL _generalDAL;

    private bool _isNew;

    public QueBankBLL()
    {
        _queBankDTO = new QueBankDTO();
        _queBankDAL = new QueBankDAL();
        _generalDAL = new GeneralDAL();
        _queBankDTO.TestId = _queBankDAL.GetNewID();
        _isNew = true;
    }
    public QueBankBLL(string QueId)
        : this()
    {
        _isNew = false;
        Load(QueId);
    }
    ~QueBankBLL()
    {
        _queBankDAL = null;
        _queBankDTO = null;
        _generalDAL = null;
    }
    public string QueBankId
    {
        get
        {
            return _queBankDTO.QueBankId;
        }
        set
        {
            _queBankDTO.QueBankId = value;
        }
    }
    public string SubId
    {
        get
        {
            return _queBankDTO.SubId;
        }
        set
        {
            _queBankDTO.SubId = value;
        }
    }

    public string SubIdTo
    {
        get
        {
            return _queBankDTO.SubIdTo;
        }
        set
        {
            _queBankDTO.SubIdTo = value;
        }
    }
    public string Subject
    {
        get
        {
            return _queBankDTO.Subject;
        }
        set
        {
            _queBankDTO.Subject = value;
        }
    }


    public decimal? PeriodNo
    {
        get
        {
            return _queBankDTO.PeriodNo;
        }
        set
        {
            _queBankDTO.PeriodNo = value;
        }
    }
    public string SubLnId
    {
        get
        {
            return _queBankDTO.SubLnId;
        }
        set
        {
            _queBankDTO.SubLnId = value;
        }
    }
    public string Que
    {
        get
        {
            return _queBankDTO.Que;
        }
        set
        {
            _queBankDTO.Que = value;
        }
    }
    public string A1
    {
        get
        {
            return _queBankDTO.A1;
        }
        set
        {
            _queBankDTO.A1 = value;
        }
    }
    public string A2
    {
        get
        {
            return _queBankDTO.A2;
        }
        set
        {
            _queBankDTO.A2 = value;
        }
    }

    public string ImageNameQus
    {
        get
        {
            return _queBankDTO.ImageNameQus;
        }
        set
        {
            _queBankDTO.ImageNameQus = value;
        }
    }

    public string ImageNameA1
    {
        get
        {
            return _queBankDTO.ImageNameA1;
        }
        set
        {
            _queBankDTO.ImageNameA1 = value;
        }
    }
    public string ImageNameA2
    {
        get
        {
            return _queBankDTO.ImageNameA2;
        }
        set
        {
            _queBankDTO.ImageNameA2 = value;
        }
    }
    public string ImageNameA3
    {
        get
        {
            return _queBankDTO.ImageNameA3;
        }
        set
        {
            _queBankDTO.ImageNameA3 = value;
        }
    }
    public string ImageNameA4
    {
        get
        {
            return _queBankDTO.ImageNameA4;
        }
        set
        {
            _queBankDTO.ImageNameA4 = value;
        }
    }
    public string A3
    {
        get
        {
            return _queBankDTO.A3;
        }
        set
        {
            _queBankDTO.A3 = value;
        }
    }
    public string A4
    {
        get
        {
            return _queBankDTO.A4;
        }
        set
        {
            _queBankDTO.A4 = value;
        }
    }
    public string Ans
    {
        get
        {
            return _queBankDTO.Ans;
        }
        set
        {
            _queBankDTO.Ans = value;
        }
    }
    public byte[] QueImg
    {
        get
        {
            return _queBankDTO.QueImg;
        }
        set
        {
            _queBankDTO.QueImg = value;
        }
    }
    public bool QueImgSelected
    {
        get
        {
            return _queBankDTO.QueImgSelected;
        }
        set
        {
            _queBankDTO.QueImgSelected = value;
        }
    }
    public byte[] A1Img
    {
        get
        {
            return _queBankDTO.A1Img;
        }
        set
        {
            _queBankDTO.A1Img = value;
        }
    }
    public bool A1ImgSelected
    {
        get
        {
            return _queBankDTO.A1ImgSelected;
        }
        set
        {
            _queBankDTO.A1ImgSelected = value;
        }
    }
    public byte[] A2Img
    {
        get
        {
            return _queBankDTO.A2Img;
        }
        set
        {
            _queBankDTO.A2Img = value;
        }
    }
    public bool A2ImgSelected
    {
        get
        {
            return _queBankDTO.A2ImgSelected;
        }
        set
        {
            _queBankDTO.A2ImgSelected = value;
        }
    }
    public byte[] A3Img
    {
        get
        {
            return _queBankDTO.A3Img;
        }
        set
        {
            _queBankDTO.A3Img = value;
        }
    }
    public bool A3ImgSelected
    {
        get
        {
            return _queBankDTO.A3ImgSelected;
        }
        set
        {
            _queBankDTO.A3ImgSelected = value;
        }
    }
    public byte[] A4Img
    {
        get
        {
            return _queBankDTO.A4Img;
        }
        set
        {
            _queBankDTO.A4Img = value;
        }
    }
    public bool A4ImgSelected
    {
        get
        {
            return _queBankDTO.A4ImgSelected;
        }
        set
        {
            _queBankDTO.A4ImgSelected = value;
        }
    }
    public bool IsNew
    {
        get
        {
            return _queBankDTO.IsNew;
        }
    }

    public string ChapterId
    {
        get
        {
            return _queBankDTO.ChapterId;
        }
        set
        {
            _queBankDTO.ChapterId = value;
        }
    }

    //public string ChapterIdMain
    //{
    //    get
    //    {
    //        return _queBankDTO.ChapterIdMain;
    //    }
    //    set
    //    {
    //        _queBankDTO.ChapterIdMain = value;
    //    }
    //}

    public string TestIdTo
    {
        get
        {
            return _queBankDTO.TestIdTo;
        }
        set
        {
            _queBankDTO.TestIdTo = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _queBankDTO.StandardTextListId;
        }
        set
        {
            _queBankDTO.StandardTextListId = value;
        }
    }

    public void Save()
    {
        try
        {
            if (_isNew == true)
            {
                _queBankDAL.Insert(_queBankDTO);
            }
            else
            {
                _queBankDAL.Update(_queBankDTO);
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
            return _queBankDTO.SrNo;
        }
        set
        {
            _queBankDTO.SrNo = value;
        }
    }

    public bool? IsQueChanged
    {
        get
        {
            return _queBankDTO.IsQueChanged;
        }
        set
        {
            _queBankDTO.IsQueChanged = value;
        }
    }

    public bool? IsA1Changed
    {
        get
        {
            return _queBankDTO.IsA1Changed;
        }
        set
        {
            _queBankDTO.IsA1Changed = value;
        }
    }

    public bool? IsA2Changed
    {
        get
        {
            return _queBankDTO.IsA2Changed;
        }
        set
        {
            _queBankDTO.IsA2Changed = value;
        }
    }

    public bool? IsA3Changed
    {
        get
        {
            return _queBankDTO.IsA3Changed;
        }
        set
        {
            _queBankDTO.IsA3Changed = value;
        }
    }

    public bool? IsA4Changed
    {
        get
        {
            return _queBankDTO.IsA4Changed;
        }
        set
        {
            _queBankDTO.IsA4Changed = value;
        }
    }
    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_queBankDTO.SubId == null)
        {
            sl.Add("Subject", " Designation cannot be blank");
        }

        if (_queBankDTO.ChapterId != null)
        {
            if (_queBankDTO.PeriodNo == null)
            {
                sl.Add("PeriodNo", "Please Select Chapter No.");
            }

            if (_queBankDTO.ChapterId == null)
            {
                sl.Add("ChapterId", "Chapter can Not be blank");
            }
        }

        if (_queBankDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank");
        }

        if (_queBankDTO.SrNo == null)
        {
            sl.Add("SrNo", "SrNo can Not be blank");
        }

        if (_queBankDTO.QueType == null)
        {
            sl.Add("QueType", "Question type cannot be blank");
        }

        //if ((_queBankDTO.Que != null) && (_queBankDTO.ImageNameQus != null))
        //{
        //sl.Add("Question", "Please Enter Question Either Image Question");
        //}

        if ((_queBankDTO.Que == null) && (_queBankDTO.ImageNameQus == null))
        {
            sl.Add("Question", "Question can Not be blank");
        }
        else
        {
            if ((_queBankDTO.ImageNameQus == null))
            {
                if (_queBankDTO.Que == null)
                {
                    sl.Add("Question", "Question can Not be blank");
                }
                else
                {
                    if (_queBankDTO.Que.Length >= 350)
                    {
                        sl.Add("Question", "Question Cannot be More Than 350 character.");
                    }
                }
            }
        }

        if (_queBankDTO.QueType != null)
        {
            if (_queBankDTO.QueType.ToString().ToUpper() == "MCQ".ToString().ToUpper()) //MCQ compulsary case
            {
                if ((_queBankDTO.A1 != null) && (_queBankDTO.ImageNameA1 != null))
                {
                    sl.Add("A1", "Please Enter A1 Either Image A1");
                }
                if ((_queBankDTO.A2 != null) && (_queBankDTO.ImageNameA2 != null))
                {
                    sl.Add("A2", "Please Enter A2 Either Image A2");
                }
                if ((_queBankDTO.A3 != null) && (_queBankDTO.ImageNameA3 != null))
                {
                    sl.Add("A3", "Please Enter A3 Either Image A3");
                }
                if ((_queBankDTO.A4 != null) && (_queBankDTO.ImageNameA4 != null))
                {
                    sl.Add("A4", "Please Enter A4 Either Image A4");
                }

                if ((_queBankDTO.A1 == null) && (_queBankDTO.ImageNameA1 == null))
                {
                    sl.Add("A1", "A1 can Not be blank");
                }
                else
                {
                    if ((_queBankDTO.A1 != null))
                    {
                        if (_queBankDTO.A1 == null)
                        {
                            sl.Add("A1", "A1 can Not be blank");
                        }
                        else
                        {
                            if (_queBankDTO.A1.Length >= 350)
                            {
                                sl.Add("A1", "A1 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if ((_queBankDTO.A2 == null) && (_queBankDTO.ImageNameA2 == null))
                {
                    sl.Add("A2", "A2 can Not be blank");
                }
                else
                {
                    if ((_queBankDTO.A2 != null))
                    {
                        if (_queBankDTO.A2 == null)
                        {
                            sl.Add("A2", "A2 can Not be blank");
                        }
                        else
                        {
                            if (_queBankDTO.A2.Length >= 350)
                            {
                                sl.Add("A2", "A2 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if ((_queBankDTO.A3 == null) && (_queBankDTO.ImageNameA3 == null))
                {
                    sl.Add("A3", "A3 can Not be blank");
                }
                else
                {
                    if ((_queBankDTO.A3 != null))
                    {
                        if (_queBankDTO.A3 == null)
                        {
                            sl.Add("A3", "A3 can Not be blank");
                        }
                        else
                        {
                            if (_queBankDTO.A3.Length >= 350)
                            {
                                sl.Add("A3", "A3 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if ((_queBankDTO.A4 == null) && (_queBankDTO.ImageNameA4 == null))
                {
                    sl.Add("A4", "A4 can Not be blank");
                }
                else
                {
                    if ((_queBankDTO.A4 != null))
                    {
                        if (_queBankDTO.A4 == null)
                        {
                            sl.Add("A4", "A4 can Not be blank");
                        }
                        else
                        {
                            if (_queBankDTO.A4.Length >= 350)
                            {
                                sl.Add("A4", "A4 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if (_queBankDTO.Ans == null)
                {
                    sl.Add("Answer", "Answer can Not be blank");
                }

                if (_queBankDTO.AnsSelection == null)
                {
                    sl.Add("AnsSelection", "Answer selection type can Not be blank");
                }
            }
            else if (_queBankDTO.QueType.ToString().ToUpper() == "NONMCQ".ToString().ToUpper()) //NONMCQ compulsary case
            {
                if (_queBankDTO.QueDataType == null)
                {
                    sl.Add("QueDataType", "Question datatype cannot be blank");
                }
            }
        }

        //if (_queBankDTO.RightMarks == null)
        //{
        //    sl.Add("RightMarks", "Right marks cannot be blank");
        //}

        //if (_queBankDTO.WrongMarks == null)
        //{
        //    sl.Add("WrongMarks", "Wrong marks cannot be blank");
        //}

        //if (_queBankDTO.NonMarks == null)
        //{
        //    sl.Add("NonMarks", "Skipped marks cannot be blank");
        //}

        return sl;
    }

    public void Load(string QueId)
    {
        _queBankDTO = _queBankDAL.Select(QueId);
    }

    public void Delete(string QueId)
    {
        string isReferenced = _queBankDAL.IsReferenced(QueId);
        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Que is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _queBankDAL.Delete(QueId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Que is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception(ex.Message);
        }
    }

    public void DeleteForce(string QueId)
    {
        try
        {
            _queBankDAL.Delete(QueId);
        }
        catch (Exception ex)
        {
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
            return _queBankDAL.LoadSubjects(StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadChapter()
    {
        try
        {
            return _queBankDAL.LoadChapter(_queBankDTO.SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadPeriodNo()
    {
        try
        {
            return _generalDAL.LoadPeriodNo(_queBankDTO.ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //public DataTable LoadTestTo()
    //{
    //    try
    //    {
    //        return _queBankDAL.LoadTest(_queBankDTO.SubIdTo);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    public int getSrNo(string ChapterId, string ChapterVideoId)
    {
        try
        {
            return _queBankDAL.getSrNo(_queBankDTO.SubId, ChapterId, ChapterVideoId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public string QueType
    {
        get
        {
            return _queBankDTO.QueType;
        }
        set
        {
            _queBankDTO.QueType = value;
        }
    }
    public string QueDataType
    {
        get
        {
            return _queBankDTO.QueDataType;
        }
        set
        {
            _queBankDTO.QueDataType = value;
        }
    }
    public decimal? RightMarks
    {
        get
        {
            return _queBankDTO.RightMarks;
        }
        set
        {
            _queBankDTO.RightMarks = value;
        }
    }
    public decimal? WrongMarks
    {
        get
        {
            return _queBankDTO.WrongMarks;
        }
        set
        {
            _queBankDTO.WrongMarks = value;
        }
    }
    public decimal? NonMarks
    {
        get
        {
            return _queBankDTO.NonMarks;
        }
        set
        {
            _queBankDTO.NonMarks = value;
        }
    }

    public void DeleteQuestion(ArrayList al)
    {
        try
        {
            _queBankDAL.DeleteQuestion(al);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void PasteQuestionFrom(DataTable Dt)
    {
        try
        {
            _queBankDAL.PasteQuestionFrom(Dt, _queBankDTO);
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
            return _queBankDTO.NoOfFile;
        }
        set
        {
            _queBankDTO.NoOfFile = value;
        }
    }
    public string SampleAns1
    {
        get
        {
            return _queBankDTO.SampleAns1;
        }
        set
        {
            _queBankDTO.SampleAns1 = value;
        }
    }
    public string SampleAns2
    {
        get
        {
            return _queBankDTO.SampleAns2;
        }
        set
        {
            _queBankDTO.SampleAns2 = value;
        }
    }
    public string SampleAns3
    {
        get
        {
            return _queBankDTO.SampleAns3;
        }
        set
        {
            _queBankDTO.SampleAns3 = value;
        }
    }
    public string SampleAns4
    {
        get
        {
            return _queBankDTO.SampleAns4;
        }
        set
        {
            _queBankDTO.SampleAns4 = value;
        }
    }

    public bool? IsSampleAns1Changed
    {
        get
        {
            return _queBankDTO.IsSampleAns1Changed;
        }
        set
        {
            _queBankDTO.IsSampleAns1Changed = value;
        }
    }

    public bool? IsSampleAns2Changed
    {
        get
        {
            return _queBankDTO.IsSampleAns2Changed;
        }
        set
        {
            _queBankDTO.IsSampleAns2Changed = value;
        }
    }

    public bool? IsSampleAns3Changed
    {
        get
        {
            return _queBankDTO.IsSampleAns3Changed;
        }
        set
        {
            _queBankDTO.IsSampleAns3Changed = value;
        }
    }

    public bool? IsSampleAns4Changed
    {
        get
        {
            return _queBankDTO.IsSampleAns4Changed;
        }
        set
        {
            _queBankDTO.IsSampleAns4Changed = value;
        }
    }

    public string AnsSelection
    {
        get
        {
            return _queBankDTO.AnsSelection;
        }
        set
        {
            _queBankDTO.AnsSelection = value;
        }
    }

    public string HashTag
    {
        get
        {
            return _queBankDTO.HashTag;
        }
        set
        {
            _queBankDTO.HashTag = value;
        }
    }

    public string LevelofQue
    {
        get
        {
            return _queBankDTO.LevelofQue;
        }
        set
        {
            _queBankDTO.LevelofQue = value;
        }
    }
    public string TestId
    {
        get
        {
            return _queBankDTO.TestId;
        }
        set
        {
            _queBankDTO.TestId = value;
        }
    }
    public DataTable TextList(string Group)
    {
        try
        {
            return _generalDAL.TextList(Group);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetChapterId(string ChapterName, string PeriodNo, string ChapterId)
    {
        try
        {
            return _generalDAL.GetChapterId(ChapterName, PeriodNo, ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int? ChapterVideoId
    {
        get
        {
            return _queBankDTO.ChapterVideoId;
        }
        set
        {
            _queBankDTO.ChapterVideoId = value;
        }
    }

    public DataTable LoadChapterVideo()
    {
        try
        {
            return _queBankDAL.LoadChapterVideo(_queBankDTO.ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}