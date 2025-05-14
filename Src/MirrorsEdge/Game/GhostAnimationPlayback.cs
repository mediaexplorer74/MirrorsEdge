
// Type: game.GhostAnimationPlayback
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using GameManager;

#nullable disable
namespace game
{
  public class GhostAnimationPlayback
  {
    private GhostKeyframe[] m_keyframeArray;

    public static GhostAnimationPlayback createForCurrentLevel()
    {
      InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream(GhostAnimation.getCurrentLevelGhostFilename());
      if (resourceAsStream != null)
      {
        if (resourceAsStream.read() == 3)
          return new GhostAnimationPlayback(resourceAsStream);
        resourceAsStream.close();
      }
      return (GhostAnimationPlayback) null;
    }

    public static GhostAnimationPlayback createFromChallenge()
    {
      InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream("ChallengeGhost" + (MirrorsEdge.TrialMode ? "_trial" : ""));
      if (resourceAsStream != null)
      {
        if (resourceAsStream.read() == 3)
          return new GhostAnimationPlayback(resourceAsStream);
        resourceAsStream.close();
      }
      return (GhostAnimationPlayback) null;
    }

    private GhostAnimationPlayback(InputStream inputStream)
    {
      DataInputStream dataInputStream = new DataInputStream(inputStream);
      int length = dataInputStream.readInt();
      int index1 = length - 1;
      this.m_keyframeArray = new GhostKeyframe[length];
      for (int index2 = 0; index2 != index1; ++index2)
        this.m_keyframeArray[index2] = new GhostKeyframe()
        {
          position = {
            x = (float) dataInputStream.readInt() * 1.52587891E-05f,
            y = (float) dataInputStream.readInt() * 1.52587891E-05f,
            z = (float) dataInputStream.readInt() * 1.52587891E-05f
          },
          visualCode = dataInputStream.readShort(),
          blend3WayValue = (float) dataInputStream.readInt() * 1.52587891E-05f,
          duration = dataInputStream.readUnsignedShort()
        };
      this.m_keyframeArray[index1] = new GhostKeyframe()
      {
        position = {
          x = (float) dataInputStream.readInt() * 1.52587891E-05f,
          y = (float) dataInputStream.readInt() * 1.52587891E-05f,
          z = (float) dataInputStream.readInt() * 1.52587891E-05f
        },
        visualCode = dataInputStream.readShort(),
        blend3WayValue = (float) dataInputStream.readInt() * 1.52587891E-05f
      };
      inputStream.close();
    }

    public void Destructor() => this.m_keyframeArray = (GhostKeyframe[]) null;

    public int getKeyframeNum() => this.m_keyframeArray.Length;

    public GhostKeyframe getKeyframe(int index) => this.m_keyframeArray[index];
  }
}
