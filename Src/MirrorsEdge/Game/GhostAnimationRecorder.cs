
// Type: game.GhostAnimationRecorder
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using mirrorsedge_wp7;
using SevenZip;
using SevenZip.Compression.LZMA;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace game
{
  public class GhostAnimationRecorder
  {
    private List<GhostKeyframe> m_keyframeList;
    private int packedDataLength;

    public GhostAnimationRecorder()
    {
      this.m_keyframeList = new List<GhostKeyframe>();
      this.packedDataLength = 0;
    }

    public void Destructor() => this.m_keyframeList.Clear();

    public void reset()
    {
      this.m_keyframeList.Clear();
      this.packedDataLength = 0;
    }

    public void addKeyframe(GhostKeyframe keyframe)
    {
      this.m_keyframeList.Add(keyframe);
      this.packedDataLength = 0;
    }

    public int getPackedDataLength() => this.packedDataLength;

    public void write() => this.write((string) null);

    public void write(string filename)
    {
      if (AppEngine.getCanvas().storageFull())
        return;
      filename = filename != null ? filename + (MirrorsEdge.TrialMode ? "_trial" : "") : GhostAnimation.getCurrentLevelGhostFilename();
      OutputStream resourceAsStream1 = (OutputStream) OutputStream.getResourceAsStream(filename);
      if (resourceAsStream1 == null)
        return;
      DataOutputStream dataOutputStream = new DataOutputStream(resourceAsStream1);
      dataOutputStream.writeByte((byte) 3);
      int count = this.m_keyframeList.Count;
      dataOutputStream.writeInt(count);
      GhostKeyframe keyframe1 = this.m_keyframeList[0];
      dataOutputStream.writeInt((int) ((double) keyframe1.position.x * 65536.0));
      dataOutputStream.writeInt((int) ((double) keyframe1.position.y * 65536.0));
      dataOutputStream.writeInt((int) ((double) keyframe1.position.z * 65536.0));
      dataOutputStream.writeShort(keyframe1.visualCode);
      dataOutputStream.writeInt((int) ((double) keyframe1.blend3WayValue * 65536.0));
      for (int index = 1; index != count; ++index)
      {
        GhostKeyframe keyframe2 = this.m_keyframeList[index];
        dataOutputStream.writeShort((short) keyframe2.duration);
        dataOutputStream.writeInt((int) ((double) keyframe2.position.x * 65536.0));
        dataOutputStream.writeInt((int) ((double) keyframe2.position.y * 65536.0));
        dataOutputStream.writeInt((int) ((double) keyframe2.position.z * 65536.0));
        dataOutputStream.writeShort(keyframe2.visualCode);
        dataOutputStream.writeInt((int) ((double) keyframe2.blend3WayValue * 65536.0));
      }
      dataOutputStream.close();
      if (this.packedDataLength != 0)
        return;
      InputStream resourceAsStream2 = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream(filename);
      if (resourceAsStream2 == null)
        return;
      MemoryStream outStream = new MemoryStream();
      CoderPropID[] propIDs = new CoderPropID[8]
      {
        CoderPropID.DictionarySize,
        CoderPropID.PosStateBits,
        CoderPropID.LitContextBits,
        CoderPropID.LitPosBits,
        CoderPropID.Algorithm,
        CoderPropID.NumFastBytes,
        CoderPropID.MatchFinder,
        CoderPropID.EndMarker
      };
      int num1 = 2097152;
      int num2 = 2;
      int num3 = 3;
      int num4 = 0;
      int num5 = 2;
      int num6 = 128;
      bool flag = false;
      string str = "bt4";
      object[] properties = new object[8]
      {
        (object) num1,
        (object) num2,
        (object) num3,
        (object) num4,
        (object) num5,
        (object) num6,
        (object) str,
        (object) flag
      };
      Stream wp7Stream = resourceAsStream2.getWP7Stream();
      Encoder encoder = new Encoder();
      encoder.SetCoderProperties(propIDs, properties);
      encoder.WriteCoderProperties((Stream) outStream);
      long length = wp7Stream.Length;
      for (int index = 0; index < 8; ++index)
        outStream.WriteByte((byte) (length >> 8 * index));
      encoder.Code(wp7Stream, (Stream) outStream, -1L, -1L, (ICodeProgress) null);
      resourceAsStream2.close();
      byte[] array = outStream.ToArray();
      this.packedDataLength = array.Length;
      outStream.Close();
      OutputStream resourceAsStream3 = (OutputStream) OutputStream.getResourceAsStream(filename + "_7zip");
      if (resourceAsStream3 == null)
        return;
      resourceAsStream3.write(array, this.packedDataLength);
      resourceAsStream3.close();
    }
  }
}
