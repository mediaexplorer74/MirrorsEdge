// Decompiled with JetBrains decompiler
// Type: midp.InputStream
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System.IO;

#nullable disable
namespace midp
{
  public abstract class InputStream : meObject
  {
    public const int SEEK_SET = 0;
    public const int SEEK_CUR = 1;
    public const int SEEK_END = 2;

    public override meClass getClass() => (meClass) new InputStreamClass();

    public override void Destructor()
    {
      this.close();
      base.Destructor();
    }

    public virtual int available() => 0;

    public virtual void close()
    {
    }

    public virtual void mark(int readlimit)
    {
    }

    public virtual bool markSupported() => false;

    public abstract int read();

    public virtual int read(ref byte[] b) => this.read(ref b, 0, b.Length);

    public virtual int read(ref sbyte[] b) => this.read(ref b, 0, b.Length);

    public virtual int read(ref byte[] b, int len) => this.read(ref b, 0, len);

    public virtual int read(ref sbyte[] b, int len) => this.read(ref b, 0, len);

    public virtual int read(ref byte[] b, int off, int len)
    {
      int index = off;
      int num1 = 0;
      while (num1 < len)
      {
        int num2 = this.read();
        if (num2 == -1)
        {
          if (num1 == 0)
            return -1;
          break;
        }
        b[index] = (byte) (num2 & (int) byte.MaxValue);
        ++num1;
        ++index;
      }
      return num1;
    }

    public virtual int read(ref sbyte[] b, int off, int len)
    {
      int index = off;
      int num1 = 0;
      while (num1 < len)
      {
        int num2 = this.read();
        if (num2 == -1)
        {
          if (num1 == 0)
            return -1;
          break;
        }
        b[index] = (sbyte) (num2 & (int) byte.MaxValue);
        ++num1;
        ++index;
      }
      return num1;
    }

    public virtual void reset()
    {
    }

    public virtual int skip(int n)
    {
      int num = 0;
      while (num < n && this.read() != -1)
        ++num;
      return num;
    }

    public virtual int getPosition() => -1;

    public virtual bool seek(int offset, int from) => false;

    public virtual int size() => 0;

    public abstract Stream getWP7Stream();
  }
}
