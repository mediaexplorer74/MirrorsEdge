
// Type: generic.FBConnection
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using System.Collections.Generic;

#nullable disable
namespace generic
{
  public class FBConnection : Displayable
  {
    public long m_uid;
    public string m_sessionKey;
    public string m_name;
    private Display m_display;
    private List<FBFriend> m_friendList;
    private FacebookLoginResult m_loginResult;

    public FBConnection(Display display)
    {
      this.m_display = display;
      this.m_friendList = new List<FBFriend>();
    }

    public override void Destructor()
    {
      this.m_friendList.Clear();
      this.m_display = (Display) null;
      this.m_friendList = (List<FBFriend>) null;
    }

    public void activate()
    {
    }

    public void deactivate()
    {
    }

    public void login()
    {
    }

    public void logout()
    {
    }

    public FacebookLoginResult GetLoginResult() => this.m_loginResult;

    public void loginSucceeded()
    {
    }

    public void loginCanceled()
    {
    }

    public bool isLoggedIn() => false;

    public void postMessage(string message)
    {
    }

    public override void showNotify()
    {
    }

    public override void hideNotify()
    {
    }

    public void AddFriend(FBFriend fbfriend)
    {
    }

    public FBFriend GetFriend(int idx) => (FBFriend) null;

    public int GetFriendCount() => this.m_friendList.Count;

    public void GetFriendIDs(List<long> idList)
    {
    }

    public void ClearFriends()
    {
    }
  }
}
