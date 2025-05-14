
// Type: support.IFFReader
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;

#nullable disable
namespace support
{
  public class IFFReader
  {
    private DataInputStream m_inStream;
    private bool m_outOfChunks;
    private sbyte[] m_curChunkId = new sbyte[5];
    private int m_curChunkSize;
    private byte[] chunkData = new byte[10000];

    public IFFReader(DataInputStream inStream)
    {
      this.m_inStream = inStream;
      this.m_outOfChunks = false;
      this.m_curChunkSize = 0;
      GameCommon.fillArray(ref this.m_curChunkId, (sbyte) 0);
      this.readChunkHeader();
    }

    public void Destructor()
    {
      this.m_inStream = (DataInputStream) null;
      this.m_curChunkId = (sbyte[]) null;
      this.chunkData = (byte[]) null;
    }

    public bool isReadComplete() => this.m_outOfChunks;

    private void readChunkHeader()
    {
      if (this.m_inStream.available() < 8)
      {
        this.m_outOfChunks = true;
      }
      else
      {
        this.m_inStream.read(ref this.m_curChunkId, 4);
        this.m_curChunkSize = this.m_inStream.readInt();
        if (this.m_inStream.available() >= this.m_curChunkSize)
          return;
        this.m_outOfChunks = true;
      }
    }

    public bool isIdOfCurrectChunk(string id)
    {
      if (this.m_outOfChunks)
        return false;
      for (int index = 0; index < 4; ++index)
      {
        if ((int) id[index] != (int) (ushort) this.m_curChunkId[index])
          return false;
      }
      return true;
    }

    public InputStream readChunk()
    {
      if (this.m_outOfChunks)
        return (InputStream) null;
      if (this.chunkData.Length < this.m_curChunkSize)
        this.chunkData = new byte[this.m_curChunkSize];
      int curChunkSize = this.m_curChunkSize;
      this.m_inStream.read(ref this.chunkData, 0, this.m_curChunkSize);
      if ((this.m_curChunkSize & 1) == 1 && this.m_inStream.available() != 0)
        this.m_inStream.skip(1);
      this.readChunkHeader();
      return (InputStream) new ByteArrayInputStream(this.chunkData, 0, curChunkSize);
    }

    public InputStream readChunk(string id)
    {
      while (!this.m_outOfChunks)
      {
        if (this.isIdOfCurrectChunk(id))
          return this.readChunk();
        this.skipChunk();
      }
      return (InputStream) null;
    }

    public void skipChunk()
    {
      if (this.m_outOfChunks)
        return;
      this.m_inStream.skip(this.m_curChunkSize + (this.m_curChunkSize & 1));
      this.readChunkHeader();
    }
  }
}
