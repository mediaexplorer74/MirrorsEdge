
// Type: util.TimerClass
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace util
{
  public class TimerClass : meClass
  {
    public override string getName() => "java.util.Timer";

    public override meObject newInstance() => (meObject) new Timer();
  }
}
