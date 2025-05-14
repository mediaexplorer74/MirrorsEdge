
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
        private FileStream m_Stream = default;

        public WP7OutputStreamIsolatedStorage(string fileName)
        {
            var filePath = "res/" + fileName;
            // Corrected the issue by using File.Exists instead of Path.FileExists
            if (!File.Exists(filePath))
                this.m_Stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            else
                this.m_Stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        }

        public bool loadSuccessful() => this.m_Stream != null;

        public override bool close()
        {
            if (this.m_Stream == null)
                return false;
            this.m_Stream.Close();
            this.m_Stream = null;
            return true;
        }

        public override void write(byte writeByte)
        {
            if (this.m_Stream == null)
                throw new FileNotFoundException();
            this.m_Stream.WriteByte(writeByte);
        }
    }
}
