using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for InterviewLnsBLL
/// </summary>
public class InterviewLnsBLL : CollectionBase
{
    ArrayList _deletedLns;

    public InterviewLnsBLL()
    {
        _deletedLns = new ArrayList();
    }
    ~InterviewLnsBLL()
    {
    }
    public ArrayList DeletedLns
    {
        get
        {
            return _deletedLns;
        }
    }
    public void Add(InterviewLnBLL InterviewLnBLL)
    {
        List.Add(InterviewLnBLL);
    }
    public void Remove(int index)
    {
        InterviewLnBLL InterviewLnBLL = (InterviewLnBLL)this.List[index];

        if (InterviewLnBLL.IsNew == false)
        {
            _deletedLns.Add(InterviewLnBLL.GetState());
        }
        List.RemoveAt(index);
        ReArrangeLnSrNo();
    }
    private void ReArrangeLnSrNo()
    {
        for (int i = 0; i < this.List.Count; i++)
        {
            InterviewLnBLL InterviewLnBLL = (InterviewLnBLL)this.List[i];
            InterviewLnBLL.LnNo = i + 1;
            InterviewLnBLL.IsDirty = true;
        }
    }
    public InterviewLnBLL this[int index]
    {
        get
        {
            return ((InterviewLnBLL)List[index]);
        }
        set
        {
            List[index] = value;
        }
    }
}