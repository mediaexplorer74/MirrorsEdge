// Decompiled with JetBrains decompiler
// Type: util.TimerClass
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

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
