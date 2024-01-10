using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InterviewDTO
/// </summary>
public class InterviewDTO
{
    public string InterviewId;
    public string CareerId;
    public string Name;
    public string UserId;
    public string UserName;
    public string Status;
    public string Remarks;
    public DateTime? Dt;

    public string SalStructureId;
    public decimal? CTC;
    public decimal? ViewMonthlyGrossSalary;
    public decimal? ViewMonthlyBasic;
    public decimal? ViewMonthlyHRA;
    public decimal? Conveyance;
    public decimal? SpecialAllowances;
    public decimal? ViewMonthlyPFCmpnyShare13Point61Per;
    public decimal? ViewMonthlyESIEmpShare4Point75Per;
    public bool? IsDeductESI;
    public bool? IsDeductPF;

    public bool IsNew;
}