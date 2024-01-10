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
public class QueBLL
{
    private QueDTO _queDTO;
    private QueDAL _queDAL;
    private GeneralDAL _generalDAL;

    private bool _isNew;

    public QueBLL()
    {
        _queDTO = new QueDTO();
        _queDAL = new QueDAL();
        _generalDAL = new GeneralDAL();

        _isNew = true;
    }
    public QueBLL(string QueId)
        : this()
    {
        _isNew = false;
        Load(QueId);
    }
    ~QueBLL()
    {
        _queDAL = null;
        _queDTO = null;
        _generalDAL = null;
    }
    public string QueId
    {
        get
        {
            return _queDTO.QueId;
        }
        set
        {
            _queDTO.QueId = value;
        }
    }
    public string SubId
    {
        get
        {
            return _queDTO.SubId;
        }
        set
        {
            _queDTO.SubId = value;
        }
    }

    public string SubIdTo
    {
        get
        {
            return _queDTO.SubIdTo;
        }
        set
        {
            _queDTO.SubIdTo = value;
        }
    }
    public string Subject
    {
        get
        {
            return _queDTO.Subject;
        }
        set
        {
            _queDTO.Subject = value;
        }
    }


    public string SubLnId
    {
        get
        {
            return _queDTO.SubLnId;
        }
        set
        {
            _queDTO.SubLnId = value;
        }
    }
    public string Que
    {
        get
        {
            return _queDTO.Que;
        }
        set
        {
            _queDTO.Que = value;
        }
    }
    public string A1
    {
        get
        {
            return _queDTO.A1;
        }
        set
        {
            _queDTO.A1 = value;
        }
    }
    public string A2
    {
        get
        {
            return _queDTO.A2;
        }
        set
        {
            _queDTO.A2 = value;
        }
    }

    public string ImageNameQus
    {
        get
        {
            return _queDTO.ImageNameQus;
        }
        set
        {
            _queDTO.ImageNameQus = value;
        }
    }

    public string ImageNameA1
    {
        get
        {
            return _queDTO.ImageNameA1;
        }
        set
        {
            _queDTO.ImageNameA1 = value;
        }
    }
    public string ImageNameA2
    {
        get
        {
            return _queDTO.ImageNameA2;
        }
        set
        {
            _queDTO.ImageNameA2 = value;
        }
    }
    public string ImageNameA3
    {
        get
        {
            return _queDTO.ImageNameA3;
        }
        set
        {
            _queDTO.ImageNameA3 = value;
        }
    }
    public string ImageNameA4
    {
        get
        {
            return _queDTO.ImageNameA4;
        }
        set
        {
            _queDTO.ImageNameA4 = value;
        }
    }
    public string A3
    {
        get
        {
            return _queDTO.A3;
        }
        set
        {
            _queDTO.A3 = value;
        }
    }
    public string A4
    {
        get
        {
            return _queDTO.A4;
        }
        set
        {
            _queDTO.A4 = value;
        }
    }
    public string Ans
    {
        get
        {
            return _queDTO.Ans;
        }
        set
        {
            _queDTO.Ans = value;
        }
    }
    public byte[] QueImg
    {
        get
        {
            return _queDTO.QueImg;
        }
        set
        {
            _queDTO.QueImg = value;
        }
    }
    public bool QueImgSelected
    {
        get
        {
            return _queDTO.QueImgSelected;
        }
        set
        {
            _queDTO.QueImgSelected = value;
        }
    }
    public byte[] A1Img
    {
        get
        {
            return _queDTO.A1Img;
        }
        set
        {
            _queDTO.A1Img = value;
        }
    }
    public bool A1ImgSelected
    {
        get
        {
            return _queDTO.A1ImgSelected;
        }
        set
        {
            _queDTO.A1ImgSelected = value;
        }
    }
    public byte[] A2Img
    {
        get
        {
            return _queDTO.A2Img;
        }
        set
        {
            _queDTO.A2Img = value;
        }
    }
    public bool A2ImgSelected
    {
        get
        {
            return _queDTO.A2ImgSelected;
        }
        set
        {
            _queDTO.A2ImgSelected = value;
        }
    }
    public byte[] A3Img
    {
        get
        {
            return _queDTO.A3Img;
        }
        set
        {
            _queDTO.A3Img = value;
        }
    }
    public bool A3ImgSelected
    {
        get
        {
            return _queDTO.A3ImgSelected;
        }
        set
        {
            _queDTO.A3ImgSelected = value;
        }
    }
    public byte[] A4Img
    {
        get
        {
            return _queDTO.A4Img;
        }
        set
        {
            _queDTO.A4Img = value;
        }
    }
    public bool A4ImgSelected
    {
        get
        {
            return _queDTO.A4ImgSelected;
        }
        set
        {
            _queDTO.A4ImgSelected = value;
        }
    }
    public bool IsNew
    {
        get
        {
            return _queDTO.IsNew;
        }
    }

    public string TestId
    {
        get
        {
            return _queDTO.TestId;
        }
        set
        {
            _queDTO.TestId = value;
        }
    }

    public string TestIdTo
    {
        get
        {
            return _queDTO.TestIdTo;
        }
        set
        {
            _queDTO.TestIdTo = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _queDTO.StandardTextListId;
        }
        set
        {
            _queDTO.StandardTextListId = value;
        }
    }

    public void Save()
    {
        try
        {
            if (_isNew == true)
            {
                _queDAL.Insert(_queDTO);
            }
            else
            {
                _queDAL.Update(_queDTO);
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
            return _queDTO.SrNo;
        }
        set
        {
            _queDTO.SrNo = value;
        }
    }

    public bool? IsQueChanged
    {
        get
        {
            return _queDTO.IsQueChanged;
        }
        set
        {
            _queDTO.IsQueChanged = value;
        }
    }

    public bool? IsA1Changed
    {
        get
        {
            return _queDTO.IsA1Changed;
        }
        set
        {
            _queDTO.IsA1Changed = value;
        }
    }

    public bool? IsA2Changed
    {
        get
        {
            return _queDTO.IsA2Changed;
        }
        set
        {
            _queDTO.IsA2Changed = value;
        }
    }

    public bool? IsA3Changed
    {
        get
        {
            return _queDTO.IsA3Changed;
        }
        set
        {
            _queDTO.IsA3Changed = value;
        }
    }

    public bool? IsA4Changed
    {
        get
        {
            return _queDTO.IsA4Changed;
        }
        set
        {
            _queDTO.IsA4Changed = value;
        }
    }
    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_queDTO.SubId == null)
        {
            sl.Add("Subject", " Designation cannot be blank");
        }

        if (_queDTO.TestId == null)
        {
            sl.Add("TestId", "Test can Not be blank");
        }

        if (_queDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank");
        }

        if (_queDTO.SrNo == null)
        {
            sl.Add("SrNo", "SrNo can Not be blank");
        }

        if (_queDTO.QueType == null)
        {
            sl.Add("QueType", "Question type cannot be blank");
        }

        if (_queDTO.Que == null)
        {
            sl.Add("Question", "Question can Not be blank");
        }
        else if (_queDTO.Que != null)
        {
            if (_queDTO.QueId == null)
            {
                if (_queDTO.IsNew ? _queDAL.NameExists(_queDTO.Que) : _queDAL.NameExists(_queDTO.Que, _queDTO.QueId, _queDTO.TestId) == true)
                {
                    sl.Add("Question", "Already Add This Question");
                }
            }
        }
        //if ((_queDTO.Que != null) && (_queDTO.ImageNameQus != null))
        //{
        //sl.Add("Question", "Please Enter Question Either Image Question");
        //}

        if ((_queDTO.Que == null) && (_queDTO.ImageNameQus == null))
        {
            sl.Add("Question", "Question can Not be blank");
        }
        else
        {
            if ((_queDTO.ImageNameQus == null))
            {
                if (_queDTO.Que == null)
                {
                    sl.Add("Question", "Question can Not be blank");
                }
                else
                {
                    if (_queDTO.Que.Length >= 350)
                    {
                        sl.Add("Question", "Question Cannot be More Than 350 character.");
                    }
                }
            }
        }

        if (_queDTO.QueType != null)
        {
            if (_queDTO.QueType.ToString().ToUpper() == "MCQ".ToString().ToUpper()) //MCQ compulsary case
            {
                if ((_queDTO.A1 != null) && (_queDTO.ImageNameA1 != null))
                {
                    sl.Add("A1", "Please Enter A1 Either Image A1");
                }
                if ((_queDTO.A2 != null) && (_queDTO.ImageNameA2 != null))
                {
                    sl.Add("A2", "Please Enter A2 Either Image A2");
                }
                if ((_queDTO.A3 != null) && (_queDTO.ImageNameA3 != null))
                {
                    sl.Add("A3", "Please Enter A3 Either Image A3");
                }
                if ((_queDTO.A4 != null) && (_queDTO.ImageNameA4 != null))
                {
                    sl.Add("A4", "Please Enter A4 Either Image A4");
                }

                if ((_queDTO.A1 == null) && (_queDTO.ImageNameA1 == null))
                {
                    sl.Add("A1", "A1 can Not be blank");
                }
                else
                {
                    if ((_queDTO.A1 != null))
                    {
                        if (_queDTO.A1 == null)
                        {
                            sl.Add("A1", "A1 can Not be blank");
                        }
                        else
                        {
                            if (_queDTO.A1.Length >= 350)
                            {
                                sl.Add("A1", "A1 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if ((_queDTO.A2 == null) && (_queDTO.ImageNameA2 == null))
                {
                    sl.Add("A2", "A2 can Not be blank");
                }
                else
                {
                    if ((_queDTO.A2 != null))
                    {
                        if (_queDTO.A2 == null)
                        {
                            sl.Add("A2", "A2 can Not be blank");
                        }
                        else
                        {
                            if (_queDTO.A2.Length >= 350)
                            {
                                sl.Add("A2", "A2 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if ((_queDTO.A3 == null) && (_queDTO.ImageNameA3 == null))
                {
                    sl.Add("A3", "A3 can Not be blank");
                }
                else
                {
                    if ((_queDTO.A3 != null))
                    {
                        if (_queDTO.A3 == null)
                        {
                            sl.Add("A3", "A3 can Not be blank");
                        }
                        else
                        {
                            if (_queDTO.A3.Length >= 350)
                            {
                                sl.Add("A3", "A3 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if ((_queDTO.A4 == null) && (_queDTO.ImageNameA4 == null))
                {
                    sl.Add("A4", "A4 can Not be blank");
                }
                else
                {
                    if ((_queDTO.A4 != null))
                    {
                        if (_queDTO.A4 == null)
                        {
                            sl.Add("A4", "A4 can Not be blank");
                        }
                        else
                        {
                            if (_queDTO.A4.Length >= 350)
                            {
                                sl.Add("A4", "A4 Cannot be More Than 350 character.");
                            }
                        }
                    }
                }

                if (_queDTO.Ans == null)
                {
                    sl.Add("Answer", "Answer can Not be blank");
                }

                if (_queDTO.AnsSelection == null)
                {
                    sl.Add("AnsSelection", "Answer selection type can Not be blank");
                }
            }
            else if (_queDTO.QueType.ToString().ToUpper() == "NONMCQ".ToString().ToUpper()) //NONMCQ compulsary case
            {
                if (_queDTO.QueDataType == null)
                {
                    sl.Add("QueDataType", "Question datatype cannot be blank");
                }
            }
            else if (_queDTO.QueType.ToString().ToUpper() == "FILE".ToString().ToUpper()) //FILE compulsary case
            {
                if (_queDTO.NoOfSubQues == null)
                {
                    sl.Add("NoOfSubQues", "Number of Sub Questions cannot be blank");
                }
            }
        }

        if (_queDTO.RightMarks == null)
        {
            sl.Add("RightMarks", "Right marks cannot be blank");
        }

        if (_queDTO.WrongMarks == null)
        {
            sl.Add("WrongMarks", "Wrong marks cannot be blank");
        }

        if (_queDTO.NonMarks == null)
        {
            sl.Add("NonMarks", "Skipped marks cannot be blank");
        }

        return sl;
    }

    public void Load(string QueId)
    {
        _queDTO = _queDAL.Select(QueId);
    }

    public void Delete(string QueId)
    {
        string isReferenced = _queDAL.IsReferenced(QueId);
        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Que is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _queDAL.Delete(QueId);
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

    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
            //return _generalDAL.TextList("StafCategory");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //public DataTable LoadSubjects()
    //{
    //    try
    //    {
    //        return _queDAL.LoadSubjects();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    public DataTable LoadSubjects(string StandardTextListId)
    {
        try
        {
            return _queDAL.LoadSubjects(StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadTest()
    {
        try
        {
            return _queDAL.LoadTest(_queDTO.SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadTestTo()
    {
        try
        {
            return _queDAL.LoadTest(_queDTO.SubIdTo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable TextList(string Text)
    {
        try
        {
            return _generalDAL.TextList(Text);
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
            return _queDAL.getSrNo(_queDTO.SubId, _queDTO.TestId);
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
            return _queDTO.QueType;
        }
        set
        {
            _queDTO.QueType = value;
        }
    }
    public string QueDataType
    {
        get
        {
            return _queDTO.QueDataType;
        }
        set
        {
            _queDTO.QueDataType = value;
        }
    }
    public decimal? RightMarks
    {
        get
        {
            return _queDTO.RightMarks;
        }
        set
        {
            _queDTO.RightMarks = value;
        }
    }
    public decimal? WrongMarks
    {
        get
        {
            return _queDTO.WrongMarks;
        }
        set
        {
            _queDTO.WrongMarks = value;
        }
    }
    public decimal? NonMarks
    {
        get
        {
            return _queDTO.NonMarks;
        }
        set
        {
            _queDTO.NonMarks = value;
        }
    }

    public void DeleteQuestion(ArrayList al)
    {
        try
        {
            _queDAL.DeleteQuestion(al);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void PasteQuestionFrom(ArrayList al)
    {
        try
        {
            _queDAL.PasteQuestionFrom(al, _queDTO);
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
            return _queDTO.NoOfFile;
        }
        set
        {
            _queDTO.NoOfFile = value;
        }
    }
    public string SampleAns1
    {
        get
        {
            return _queDTO.SampleAns1;
        }
        set
        {
            _queDTO.SampleAns1 = value;
        }
    }
    public string SampleAns2
    {
        get
        {
            return _queDTO.SampleAns2;
        }
        set
        {
            _queDTO.SampleAns2 = value;
        }
    }
    public string SampleAns3
    {
        get
        {
            return _queDTO.SampleAns3;
        }
        set
        {
            _queDTO.SampleAns3 = value;
        }
    }
    public string SampleAns4
    {
        get
        {
            return _queDTO.SampleAns4;
        }
        set
        {
            _queDTO.SampleAns4 = value;
        }
    }

    public bool? IsSampleAns1Changed
    {
        get
        {
            return _queDTO.IsSampleAns1Changed;
        }
        set
        {
            _queDTO.IsSampleAns1Changed = value;
        }
    }

    public bool? IsSampleAns2Changed
    {
        get
        {
            return _queDTO.IsSampleAns2Changed;
        }
        set
        {
            _queDTO.IsSampleAns2Changed = value;
        }
    }

    public bool? IsSampleAns3Changed
    {
        get
        {
            return _queDTO.IsSampleAns3Changed;
        }
        set
        {
            _queDTO.IsSampleAns3Changed = value;
        }
    }

    public bool? IsSampleAns4Changed
    {
        get
        {
            return _queDTO.IsSampleAns4Changed;
        }
        set
        {
            _queDTO.IsSampleAns4Changed = value;
        }
    }

    public string AnsSelection
    {
        get
        {
            return _queDTO.AnsSelection;
        }
        set
        {
            _queDTO.AnsSelection = value;
        }
    }

    public Int16? NoOfSubQues
    {
        get
        {
            return _queDTO.NoOfSubQues;
        }
        set
        {
            _queDTO.NoOfSubQues = value;
        }
    }
}
