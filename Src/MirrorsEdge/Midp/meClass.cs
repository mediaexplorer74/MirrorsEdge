// Decompiled with JetBrains decompiler
// Type: midp.meClass
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public abstract class meClass : meObject
  {
    public override void Destructor() => base.Destructor();

    public abstract string getName();

    public static InputStream getResourceAsStream(string name)
    {
      WP7InputStream resourceAsStream = new WP7InputStream(name);
      if (resourceAsStream.loadSuccessful())
        return (InputStream) resourceAsStream;
      return (InputStream) null;
    }

    public abstract meObject newInstance();

    public override meClass getClass() => this;
  }
}
