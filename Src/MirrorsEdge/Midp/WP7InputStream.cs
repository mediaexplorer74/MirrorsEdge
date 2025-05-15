
// Type: midp.WP7InputStream
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework;
using System.IO;

#nullable disable
namespace midp
{
  public class WP7InputStream : InputStream
  {
    private Stream m_Stream;

    public WP7InputStream(string fileName)
    {
      try
      {
        this.m_Stream = TitleContainer.OpenStream(fileName);
      }
      catch (Exception ex)
      {
        this.m_Stream = (Stream) null;
      }
    }

    public bool loadSuccessful() => this.m_Stream != null;

    public override int read()
    {
      return this.m_Stream != null ? this.m_Stream.ReadByte() : throw new FileNotFoundException();
    }

    public override void close()
    {
      if (this.m_Stream == null)
        return;
      this.m_Stream.Dispose();
      this.m_Stream = null;
    }

    public override int read(ref byte[] b, int off, int len)
    {
      if (this.m_Stream == null)
        throw new FileNotFoundException();
      return this.m_Stream.Read(b, off, len);
    }

    public override int available()
    {
      if (this.m_Stream == null)
        throw new FileNotFoundException();
      return (int) (this.m_Stream.Length - this.m_Stream.Position);
    }

    public override Stream getWP7Stream() => this.m_Stream;
  }
}
