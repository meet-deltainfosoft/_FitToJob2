using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for PatternLnsBLL
/// </summary>
public class PatternLnsBLL : CollectionBase
{
    ArrayList _deletedPatternLns;

	public PatternLnsBLL()
	{
        _deletedPatternLns = new ArrayList();
	}

    public ArrayList deletedLgrLns
    {
        get
        {
            return _deletedPatternLns;
        }
    }

    public void Add(PatternLnBLL PatternLnBLL)
    {
        List.Add(PatternLnBLL);
    }

    public void Remove(int index)
    {
        PatternLnBLL PatternLnBLL = (PatternLnBLL)this.List[index];

        if (PatternLnBLL.IsNew == false)
        {
            PatternLnBLL.IsDeleted = true;
            _deletedPatternLns.Add(PatternLnBLL.GetState());

        }
        List.RemoveAt(index);
        ReArrangePatternLnSrNo();
    }

    public PatternLnBLL this[int index]
    {
        get
        {
            return ((PatternLnBLL)List[index]);
        }
        set
        {
            List[index] = value;
        }
    }

    private void ReArrangePatternLnSrNo()
    {
        for (int i = 0; i < this.List.Count; i++)
        {
            PatternLnBLL PatternLnBLL = (PatternLnBLL)this.List[i];

            PatternLnBLL.LnNo = i + 1;
            PatternLnBLL.IsDirty = true;
        }
    }
}