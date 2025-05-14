
// Type: midp.meClass
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


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
