
// Type: midp.meObject
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace midp
{
  public abstract class meObject
  {
    public virtual void Destructor()
    {
    }

    public abstract meClass getClass();

    public virtual bool equals(meObject obj) => this == obj;

    public virtual int hashCode() => this.GetHashCode();
  }
}
