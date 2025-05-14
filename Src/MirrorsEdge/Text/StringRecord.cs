
// Type: text.StringRecord
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace text
{
  public class StringRecord
  {
    private string m_str;
    private Image m_image;

    public StringRecord()
    {
      this.m_str = (string) null;
      this.m_image = (Image) null;
    }

    public StringRecord(ref StringRecord rhs)
    {
      this.m_str = new string(rhs.m_str.ToCharArray(), 0, rhs.m_str.Length);
      this.m_image = rhs.m_image;
    }

    public StringRecord CopyFrom(ref StringRecord rhs)
    {
      if (rhs != this)
      {
        this.m_str = new string(rhs.m_str.ToCharArray(), 0, rhs.m_str.Length);
        this.m_image = rhs.m_image;
      }
      return this;
    }

    public void set(string str, Image image)
    {
      this.m_str = str;
      this.m_image = image;
    }

    public bool equals(string str) => this.m_str != null && this.m_str == str;

    public Image getImage() => this.m_image;

    public bool isEmpty() => this.m_str == null;

    public void clear()
    {
      this.m_str = (string) null;
      this.m_image = (Image) null;
    }
  }
}
