using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for HODInterviewLnsBLL
/// </summary>
public class HODInterviewLnsBLL : CollectionBase
{
	 ArrayList _deletedLns;

    public HODInterviewLnsBLL()
    {
        _deletedLns = new ArrayList();
    }
    ~HODInterviewLnsBLL()
    {
    }
    public ArrayList DeletedLns
    {
        get
        {
            return _deletedLns;
        }
    }
    public void Add(HODInterviewLnBLL HODInterviewLnBLL)
    {
        List.Add(HODInterviewLnBLL);
    }
    public void Remove(int index)
    {
        HODInterviewLnBLL HODInterviewLnBLL = (HODInterviewLnBLL)this.List[index];

        if (HODInterviewLnBLL.IsNew == false)
        {
            _deletedLns.Add(HODInterviewLnBLL.GetState());
        }
        List.RemoveAt(index);
        ReArrangeLnSrNo();
    }
    private void ReArrangeLnSrNo()
    {
        for (int i = 0; i < this.List.Count; i++)
        {
            HODInterviewLnBLL HODInterviewLnBLL = (HODInterviewLnBLL)this.List[i];
            HODInterviewLnBLL.LnNo = i + 1;
            HODInterviewLnBLL.IsDirty = true;
        }
    }
    public HODInterviewLnBLL this[int index]
    {
        get
        {
            return ((HODInterviewLnBLL)List[index]);
        }
        set
        {
            List[index] = value;
        }
    }
}