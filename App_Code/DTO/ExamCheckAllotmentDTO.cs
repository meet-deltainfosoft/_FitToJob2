using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ExamCheckAllotmentDTO
{
    public string ExamCheckAllotmentId;
    public string StandardTextListId;
    public string SubId;
    public string TestId;
    public string ExamScheduleId;

    public DataTable dtExamCheckAllotmentLns;

    public bool IsNew;
}
