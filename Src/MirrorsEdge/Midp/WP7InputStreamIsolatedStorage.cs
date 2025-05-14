
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
    private IsolatedStorageFile isoFile;
    private IsolatedStorageFileStream m_Stream;

    protected WP7InputStreamIsolatedStorage(string fileName)
    {
      this.isoFile = IsolatedStorageFile.GetUserStoreForApplication();
      if (!this.isoFile.FileExists(fileName))
        return;
      this.m_Stream = this.isoFile.OpenFile(fileName, FileMode.Open);
    }

    public static WP7InputStreamIsolatedStorage getResourceAsStream(string name)
    {
      WP7InputStreamIsolatedStorage resourceAsStream = new WP7InputStreamIsolatedStorage(name);
      if (resourceAsStream.loadSuccessful())
        return resourceAsStream;
      return (WP7InputStreamIsolatedStorage) null;
    }

    public bool loadSuccessful() => this.m_Stream != null;

    public override int read()
    {
      return this.m_Stream != null
                ? this.m_Stream.ReadByte() 
                : throw new System.Exception("File Not Found");
    }

    public override void close()
    {
      if (this.m_Stream == null)
        return;
      this.m_Stream.Dispose();
      this.m_Stream = (IsolatedStorageFileStream) null;
    }

    public override int read(ref byte[] b, int off, int len)
    {
      if (this.m_Stream == null)
        throw new System.Exception("File Not Found");
      return this.m_Stream.Read(b, off, len);
    }

    public override int available()
    {
      if (this.m_Stream == null)
        throw new System.Exception("File Not Found");
      return (int) (this.m_Stream.Length - this.m_Stream.Position);
    }

    public override Stream getWP7Stream() => (Stream) this.m_Stream;

    public override int size()
    {
      return this.m_Stream != null ? (int) this.m_Stream.Length : throw new System.Exception("File Not Found");
    }
  }
}
