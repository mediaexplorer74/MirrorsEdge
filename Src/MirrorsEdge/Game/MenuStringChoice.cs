
// Type: game.MenuStringChoice
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;
using text;

#nullable disable
namespace game
{
  public class MenuStringChoice
  {
    private short[] m_stringIdArray;
    private string[] m_upperCaseStringArray;
    private WrappedString[] m_wrappedStringArray;
    private int m_length;
    private int m_selectionIndex;
    private int m_parentTitle;
    private int m_thisTitle;
    private string m_parentTitleUpper;
    private string m_thisTitleUpper;

    public MenuStringChoice()
    {
      this.m_stringIdArray = new short[0];
      this.m_upperCaseStringArray = new string[0];
      this.m_wrappedStringArray = new WrappedString[0];
      this.m_length = 0;
      this.m_selectionIndex = 0;
      this.m_parentTitle = 2048;
      this.m_thisTitle = 2048;
      this.m_parentTitleUpper = (string) null;
      this.m_thisTitleUpper = (string) null;
    }

    public MenuStringChoice(MenuStringChoice other)
    {
      this.m_stringIdArray = new short[other.m_stringIdArray.Length];
      Array.Copy((Array) other.m_stringIdArray, (Array) this.m_stringIdArray, other.m_stringIdArray.Length);
      this.m_upperCaseStringArray = new string[other.m_upperCaseStringArray.Length];
      Array.Copy((Array) other.m_upperCaseStringArray, (Array) this.m_upperCaseStringArray, other.m_upperCaseStringArray.Length);
      this.m_wrappedStringArray = new WrappedString[this.m_wrappedStringArray.Length];
      Array.Copy((Array) other.m_wrappedStringArray, (Array) this.m_wrappedStringArray, other.m_wrappedStringArray.Length);
      this.m_length = other.m_length;
      this.m_selectionIndex = other.m_selectionIndex;
      this.m_parentTitle = other.m_parentTitle;
      this.m_thisTitle = other.m_thisTitle;
      this.m_parentTitleUpper = other.m_parentTitleUpper;
      this.m_thisTitleUpper = other.m_thisTitleUpper;
    }

    public virtual void Destructor()
    {
      for (int index = 0; index < this.m_wrappedStringArray.Length; ++index)
      {
        this.m_wrappedStringArray[index].Destructor();
        this.m_wrappedStringArray[index] = (WrappedString) null;
      }
      this.m_stringIdArray = (short[]) null;
      this.m_upperCaseStringArray = (string[]) null;
      this.m_wrappedStringArray = (WrappedString[]) null;
    }

    public bool isCreated() => this.m_stringIdArray != null && this.m_stringIdArray.Length > 0;

    public void create(int numItems) => this.create(numItems, 2048, 2048);

    public void create(int numItems, int titleId) => this.create(numItems, 2048, titleId);

    public void create(int numItems, int parentTitleId, int thisTitleId)
    {
      if (this.m_stringIdArray.Length != numItems)
        this.m_stringIdArray = new short[numItems];
      this.m_upperCaseStringArray = new string[0];
      this.m_wrappedStringArray = new WrappedString[0];
      this.m_parentTitleUpper = (string) null;
      this.m_thisTitleUpper = (string) null;
      this.m_length = 0;
      this.m_selectionIndex = 0;
      this.m_parentTitle = parentTitleId;
      this.m_thisTitle = thisTitleId;
    }

    public void append(int itemStringId)
    {
      this.m_stringIdArray[this.m_length++] = (short) itemStringId;
    }

    public int getLength() => this.m_length;

    public int getItem(int index) => (int) this.m_stringIdArray[index];

    public string getUpperCaseItem(int index)
    {
      if (this.m_upperCaseStringArray.Length == 0)
        this.m_upperCaseStringArray = new string[this.m_length];
      if (this.m_upperCaseStringArray[index] == null)
      {
        string str = AppEngine.getCanvas().getTextManager().getString((int) this.m_stringIdArray[index]);
        this.m_upperCaseStringArray[index] = str.ToUpper();
      }
      return this.m_upperCaseStringArray[index];
    }

    public int getSelectedIndex() => this.m_selectionIndex;

    public int getSelectedItem()
    {
      return this.m_stringIdArray.Length == 0 ? this.m_thisTitle : (int) this.m_stringIdArray[this.m_selectionIndex];
    }

    public void setSelectedIndex(int index) => this.m_selectionIndex = index;

    public void setSelectedItem(int itemStringId)
    {
      int length = this.m_stringIdArray.Length;
      for (int index = 0; index != length; ++index)
      {
        if (itemStringId == (int) this.m_stringIdArray[index])
        {
          this.m_selectionIndex = index;
          break;
        }
      }
    }

    public int getParentTitleId() => this.m_parentTitle;

    public int getThisTitleId() => this.m_thisTitle;

    public string getParentTitleAllCapsString()
    {
      if (this.m_parentTitleUpper == null)
        this.m_parentTitleUpper = AppEngine.getCanvas().getTextManager().getString(this.m_parentTitle).ToUpper();
      return this.m_parentTitleUpper;
    }

    public string getThisTitleAllCapsString()
    {
      if (this.m_thisTitleUpper == null)
        this.m_thisTitleUpper = AppEngine.getCanvas().getTextManager().getString(this.m_thisTitle).ToUpper();
      return this.m_thisTitleUpper;
    }

    public void previousItem() => this.m_selectionIndex = Math.Max(0, this.m_selectionIndex - 1);

    public void nextItem()
    {
      this.m_selectionIndex = Math.Min(this.m_selectionIndex + 1, this.m_length - 1);
    }

    public void wrapStrings(int fontId, int lineWidth, bool allCaps)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_wrappedStringArray = new WrappedString[this.m_length];
      for (int index = 0; index < this.m_length; ++index)
        this.m_wrappedStringArray[index] = new WrappedString();
      for (int index = 0; index != this.m_length; ++index)
      {
        string upper = textManager.getString((int) this.m_stringIdArray[index]);
        if (allCaps)
          upper = upper.ToUpper();
        this.m_wrappedStringArray[index].wrapString(upper, fontId, lineWidth, true);
      }
    }

    public bool isStringsWrapped() => this.m_wrappedStringArray.Length != 0;

    public WrappedString getMenuItemWrappedString(int index) => this.m_wrappedStringArray[index];
  }
}
