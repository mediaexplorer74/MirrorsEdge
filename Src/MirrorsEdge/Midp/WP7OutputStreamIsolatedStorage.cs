
// Type: midp.WP7OutputStreamIsolatedStorage
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.IO;
using System.IO.IsolatedStorage;

#nullable disable
namespace midp
{
    public class WP7OutputStreamIsolatedStorage : OutputStream
    {
        private IsolatedStorageFile isoFile;
        private IsolatedStorageFileStream m_Stream;

        public WP7OutputStreamIsolatedStorage(string fileName)
        {
            this.isoFile = IsolatedStorageFile.GetUserStoreForApplication();
            if (!this.isoFile.FileExists(fileName))
                this.m_Stream = this.isoFile.CreateFile(fileName);
            else
                this.m_Stream = this.isoFile.OpenFile(fileName, FileMode.Truncate);
        }

        public bool loadSuccessful() => this.m_Stream != null;

        public override bool close()
        {
            if (this.m_Stream == null)
                return false;
            this.m_Stream.Dispose();
            this.m_Stream = (IsolatedStorageFileStream)null;
            return true;
        }

        public override void write(byte writeByte)
        {
            if (this.m_Stream == null)
                throw new System.Exception("File Not Found");
            this.m_Stream.WriteByte(writeByte);
        }
    }
}
