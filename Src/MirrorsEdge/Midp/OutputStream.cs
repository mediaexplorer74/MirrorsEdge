
// Type: midp.OutputStream
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


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
