// Decompiled with JetBrains decompiler
// Type: midp.OutputStream
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public abstract class OutputStream : meObject
  {
    public override meClass getClass() => (meClass) new OutputStreamClass();

    public override void Destructor()
    {
      this.close();
      base.Destructor();
    }

    public abstract void write(byte writeByte);

    public virtual void write(sbyte[] b) => this.write(b, 0, b.Length);

    public virtual void write(sbyte[] b, int off, int len)
    {
      int num = off + len;
      for (int index = off; index != num; ++index)
        this.write((byte) ((uint) b[index] & (uint) byte.MaxValue));
    }

    public virtual void write(byte[] destArray, int len)
    {
      for (int index = 0; index != len; ++index)
        this.write(destArray[index]);
    }

    public virtual void write(sbyte[] destArray, int len)
    {
      for (int index = 0; index != len; ++index)
        this.write((byte) destArray[index]);
    }

    public virtual bool close() => true;

    public static WP7OutputStreamIsolatedStorage getResourceAsStream(string name)
    {
      return new WP7OutputStreamIsolatedStorage(name);
    }
  }
}
