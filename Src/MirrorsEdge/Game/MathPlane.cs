// Decompiled with JetBrains decompiler
// Type: game.MathPlane
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game
{
  public struct MathPlane
  {
    public MathVector origin;
    public MathVector basis1;
    public MathVector basis2;

    public MathPlane(MathPlane other)
    {
      this.origin = other.origin;
      this.basis1 = other.basis1;
      this.basis2 = other.basis2;
    }

    public MathPlane(
      float originX,
      float originY,
      float originZ,
      float basis1X,
      float basis1Y,
      float basis1Z,
      float basis2X,
      float basis2Y,
      float basis2Z)
    {
      this.origin = new MathVector(originX, originY, originZ);
      this.basis1 = new MathVector(basis1X, basis1Y, basis1Z);
      this.basis2 = new MathVector(basis2X, basis2Y, basis2Z);
    }

    public MathPlane(
      MathVector newOrigin,
      float basis1X,
      float basis1Y,
      float basis1Z,
      float basis2X,
      float basis2Y,
      float basis2Z)
    {
      this.origin = newOrigin;
      this.basis1 = new MathVector(basis1X, basis1Y, basis1Z);
      this.basis2 = new MathVector(basis2X, basis2Y, basis2Z);
    }

    public MathPlane(MathVector newOrigin, MathVector newBasis1, MathVector newBasis2)
    {
      this.origin = newOrigin;
      this.basis1 = newBasis1;
      this.basis2 = newBasis2;
    }

    public MathPlane CopyFrom(MathPlane other)
    {
      this.origin = other.origin;
      this.basis1 = other.basis1;
      this.basis2 = other.basis2;
      return this;
    }
  }
}
