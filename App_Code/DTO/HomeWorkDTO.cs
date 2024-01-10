﻿using System;
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

/// <summary>
/// Summary description for HomeWorkDTO
/// </summary>
public class HomeWorkDTO
{
    public string HomeWorkId;
    public string SubId;
    public string SubIdTo;
    public string Subject;
    public string SubjectTo;

    public string SubLnId;
    public string HomeWork;
    public string A1;
    public string A2;
    public string A3;
    public string A4;
    public string Ans;
    public byte[] HomeWorkImg;
    public byte[] A1Img;
    public byte[] A2Img;
    public byte[] A3Img;
    public byte[] A4Img;
    public bool IsNew;
    public bool HomeWorkImgSelected;
    public bool A1ImgSelected;
    public bool A2ImgSelected;
    public bool A3ImgSelected;
    public bool A4ImgSelected;

    public string ImageNameQus;
    public string ImageNameA1;
    public string ImageNameA2;
    public string ImageNameA3;
    public string ImageNameA4;

    public string ChapterId;
    public string TestIdTo;
    public string StandardTextListId;
    public int? SrNo;

    public bool? IsHomeWorkChanged;
    public bool? IsA1Changed;
    public bool? IsA2Changed;
    public bool? IsA3Changed;
    public bool? IsA4Changed;

    public string HomeWorkType;
    public string HomeWorkDataType;
    
    public int? NoOfFile;
    public string SampleAns1;
    public string SampleAns2;
    public string SampleAns3;
    public string SampleAns4;

    public bool? IsSampleAns1Changed;
    public bool? IsSampleAns2Changed;
    public bool? IsSampleAns3Changed;
    public bool? IsSampleAns4Changed;

    public string AnsSelection;
    public DateTime? Dt;

    public int? ChapterVideoId;
}
