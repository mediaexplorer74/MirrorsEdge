
// Type: game.PendingChallenge
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class PendingChallenge
  {
    public string m_challengeID;
    public string m_challengeComment;

    public PendingChallenge(string id, string comment)
    {
      this.m_challengeID = id;
      this.m_challengeComment = comment;
    }
  }
}
