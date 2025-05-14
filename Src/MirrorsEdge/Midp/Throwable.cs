// Decompiled with JetBrains decompiler
// Type: midp.Throwable
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace midp
{
  public class Throwable : SystemException
  {
    private readonly string m_message;

    public Throwable() => this.m_message = nameof (Throwable);

    public Throwable(Throwable t) => this.m_message = t.m_message;

    public Throwable(string message) => this.m_message = message;

    public string getMessage() => this.m_message;

    public void printStackTrace()
    {
    }

    public virtual string toString() => this.m_message;
  }
}
