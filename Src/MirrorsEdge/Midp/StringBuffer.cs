// Decompiled with JetBrains decompiler
// Type: midp.StringBuffer
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System.Text;

#nullable disable
namespace midp
{
  public class StringBuffer : meObject
  {
    private StringBuilder m_str;

    public StringBuffer() => this.m_str = new StringBuilder();

    public StringBuffer(int beginLength) => this.m_str = new StringBuilder(beginLength);

    public StringBuffer(string str)
    {
      this.m_str = new StringBuilder(str.Length);
      this.append(str);
    }

    public new void Destructor()
    {
      this.m_str = (StringBuilder) null;
      base.Destructor();
    }

    public override meClass getClass() => (meClass) null;

    public StringBuffer append(bool b)
    {
      this.append(b ? "true" : "false");
      return this;
    }

    public StringBuffer append(sbyte c)
    {
      this.m_str.Append((char) c);
      return this;
    }

    public StringBuffer append(char c)
    {
      this.m_str.Append(c);
      return this;
    }

    public StringBuffer append(char[] str)
    {
      this.m_str.Append(str);
      return this;
    }

    public StringBuffer append(char[] str, int offset, int len) => this;

    public StringBuffer append(double d)
    {
      this.m_str.Append(d);
      return this;
    }

    public StringBuffer append(float f)
    {
      this.m_str.Append(f);
      return this;
    }

    public StringBuffer append(int i)
    {
      this.m_str.Append(i);
      return this;
    }

    public StringBuffer append(uint i)
    {
      this.m_str.Append(i);
      return this;
    }

    public StringBuffer append(long l)
    {
      this.m_str.Append(l);
      return this;
    }

    public StringBuffer append(meObject obj)
    {
      this.m_str.Append(obj.getClass().getName());
      return this;
    }

    public StringBuffer append(string str)
    {
      this.m_str.Append(str);
      return this;
    }

    public int capacity() => this.m_str.Capacity;

    public char charAt(int index) => this.m_str[index];

    public StringBuffer delete_(int start, int end) => this;

    public StringBuffer deleteCharAt(int index) => this;

    public void ensureCapacity(int minimumCapacity)
    {
    }

    public void copyCString(string dst, int maxCopy) => this.m_str.Append(dst, 0, maxCopy);

    public void getChars(int srcBegin, int srcEnd, char[] dst, int dstBegin)
    {
    }

    public StringBuffer insert(int offset, bool b) => this;

    public StringBuffer insert(int offset, ushort c) => this;

    public StringBuffer insert(int offset, double d) => this;

    public StringBuffer insert(int offset, float f) => this;

    public StringBuffer insert(int offset, int i) => this;

    public StringBuffer insert(int offset, long l) => this;

    public StringBuffer insert(int offset, object obj) => this;

    public StringBuffer insert(int offset, string str) => this;

    public int length() => this.m_str.Length;

    public StringBuffer reverse() => this;

    public void setCharAt(int index, ushort ch)
    {
    }

    public void setLength(int newLength)
    {
      if (this.m_str.Length > newLength)
        this.m_str.Length = newLength;
    }

    public string toString() => this.length() > 0 ? this.m_str.ToString() : "";
  }
}
