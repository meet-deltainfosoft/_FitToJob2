using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for ChapterLnsBLL
/// </summary>
public class ChapterLnsBLL : CollectionBase
{
    ArrayList _deletedChapterLns;

    public ChapterLnsBLL()
	{
        _deletedChapterLns = new ArrayList();
    }

    public ArrayList DeletedChapterLns
    {
        get
        {
            return _deletedChapterLns;
        }
    }
    public void Add(ChapterLnBLL ChapterLnBLL)
    {
        List.Add(ChapterLnBLL);
    }

    public void Remove(int index)
    {
        ChapterLnBLL ChapterLnBLL = (ChapterLnBLL)this.List[index];

        if (ChapterLnBLL.IsNew == false)
        {
            _deletedChapterLns.Add(ChapterLnBLL.GetState());
        }
        List.RemoveAt(index);
        ReArrangeChapterLnSrNo();
    }

    private void ReArrangeChapterLnSrNo()
    {
        for (int i = 0; i < this.List.Count; i++)
        {
            ChapterLnBLL ChapterLnBLL = (ChapterLnBLL)this.List[i];
            ChapterLnBLL.LnNo = i + 1;
            ChapterLnBLL.IsDirty = true;
        }
    }

    public ChapterLnBLL this[int index]
    {
        get
        {
            return ((ChapterLnBLL)List[index]);
        }
        set
        {
            List[index] = value;
        }
    }
}