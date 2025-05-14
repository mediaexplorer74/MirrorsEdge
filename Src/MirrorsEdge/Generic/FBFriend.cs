
// Type: generic.FBFriend
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace generic
{
  public class FBFriend
  {
    private long m_uid;
    private string m_name;
    private string m_picURL;

    public FBFriend(long uid)
    {
      this.m_uid = uid;
      this.m_name = "";
      this.m_picURL = "";
    }

    public FBFriend(long uid, string name, string picURL)
    {
      this.m_uid = uid;
      this.m_name = name;
      this.m_picURL = picURL;
    }

    public void SetName(string name) => this.m_name = name;

    public void SetPicURL(string picURL) => this.m_picURL = picURL;

    public long GetUID() => this.m_uid;

    public string GetName() => this.m_name;

    public string GetPicURL() => this.m_picURL;
  }
}
