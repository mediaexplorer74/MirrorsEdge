// Decompiled with JetBrains decompiler
// Type: game.GhostKeyframe
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game
{
  public class GhostKeyframe
  {
    public int duration;
    public MathVector position;
    public short visualCode;
    public float blend3WayValue;

    public GhostKeyframe()
    {
      this.duration = 0;
      this.position = new MathVector();
      this.visualCode = (short) -1;
      this.blend3WayValue = 0.0f;
    }

    public GhostKeyframe(GhostKeyframe other)
    {
      this.duration = other.duration;
      this.position = new MathVector(other.position);
      this.visualCode = other.visualCode;
      this.blend3WayValue = other.blend3WayValue;
    }

    public GhostKeyframe(
      int newDuration,
      MathVector newPosition,
      int newVisualCode,
      float new3WayBlend)
    {
      this.duration = newDuration;
      this.position = new MathVector(newPosition);
      this.visualCode = (short) newVisualCode;
      this.blend3WayValue = new3WayBlend;
    }

    public GhostKeyframe CopyFrom(GhostKeyframe other)
    {
      this.duration = other.duration;
      this.position = other.position;
      this.visualCode = other.visualCode;
      this.blend3WayValue = other.blend3WayValue;
      return this;
    }
  }
}
