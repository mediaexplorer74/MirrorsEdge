
// Type: game.Achievement
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using GameManager;
using text;
using System;

#nullable disable
namespace game
{
  public class Achievement
  {
    protected readonly int m_idx;
    protected int m_name;
    protected int m_description;
    protected bool m_complete;
    public int m_GamePoints;
    public int m_CompletedDescription;
    public Image iconLocked;
    public Image iconOpened;
    public string m_ServerKey;

    public Achievement(int idx, int name, int description)
    {
      this.m_idx = idx;
      this.m_name = name;
      this.m_description = description;
      this.m_complete = false;
    }

    public virtual void Destructor()
    {
      this.iconLocked.Destructor();
      this.iconLocked = (Image) null;
      this.iconOpened.Destructor();
      this.iconOpened = (Image) null;
    }

    public int getIdx() => this.m_idx;

    public int getNameId() => this.m_name;

    public bool isComplete() => this.m_complete;

    public void complete()
    {
      if (MirrorsEdge.TrialMode)
        return;
      this.m_complete = true;
    }

    public void dropCompletion() => this.m_complete = false;

    public virtual StringBuffer getNameStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, this.m_name);
      return stringBuffer;
    }

    public virtual StringBuffer getDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, this.m_description);
      return stringBuffer;
    }

    public virtual StringBuffer getCompletedDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, this.m_CompletedDescription);
      return stringBuffer;
    }

    public void eventHappended()
    {
            
    }
  }
}
