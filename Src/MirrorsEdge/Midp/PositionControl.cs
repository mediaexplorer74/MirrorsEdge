// Decompiled with JetBrains decompiler
// Type: midp.PositionControl
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public interface PositionControl : Control
  {
    void setPosition(float x, float y, float z);

    void setVelocity(float x, float y, float z);

    void setReferenceDistance(float dist);

    void setRolloffFactor(float factor);

    void setSourceRelative(bool set);
  }
}
