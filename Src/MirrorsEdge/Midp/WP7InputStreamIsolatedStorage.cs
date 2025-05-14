
// Type: midp.WP7InputStreamIsolatedStorage
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.IO;
using System.IO.IsolatedStorage;

#nullable disable
namespace midp
{
  public class WP7InputStreamIsolatedStorage : InputStream
  {
    //private IsolatedStorageFile isoFile = default;
    //private IsolatedStorageFileStream m_Stream = default;
    private FileStream m_Stream = default;

    protected WP7InputStreamIsolatedStorage(string fileName)
    {
        // Use FileStream instead of Stream, as Stream is abstract and cannot be instantiated.
        try
        {
            this.m_Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }
        catch { }
    }

    public static WP7InputStreamIsolatedStorage getResourceAsStream(string name)
    {
      name = "res/" + name;
      WP7InputStreamIsolatedStorage resourceAsStream = new WP7InputStreamIsolatedStorage(name);
      if (resourceAsStream.loadSuccessful())
        return resourceAsStream;
      return (WP7InputStreamIsolatedStorage) null;
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
      this.m_Stream.Close();
      this.m_Stream = (IsolatedStorageFileStream) null;
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

    public override Stream getWP7Stream() => (Stream) this.m_Stream;

    public override int size()
    {
      return this.m_Stream != null ? (int) this.m_Stream.Length : throw new FileNotFoundException();
    }
  }
}
