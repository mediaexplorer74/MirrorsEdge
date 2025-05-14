// Decompiled with JetBrains decompiler
// Type: support.IFFWriter
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace support
{
  public class IFFWriter
  {
    private sbyte[] m_chunkId = new sbyte[5];
    private DataOutputStream m_outStream;
    private ByteArrayOutputStream m_chunkBuffer;
    private DataOutputStream m_dataBuffer;

    public IFFWriter(DataOutputStream outStream)
    {
      this.m_outStream = outStream;
      this.m_chunkBuffer = new ByteArrayOutputStream();
      this.m_dataBuffer = new DataOutputStream((OutputStream) this.m_chunkBuffer);
    }

    public void Destructor()
    {
      this.storeCurrentChunk();
      this.m_outStream.close();
      this.m_outStream = (DataOutputStream) null;
      this.m_chunkBuffer = (ByteArrayOutputStream) null;
      this.m_dataBuffer = (DataOutputStream) null;
    }

    public DataOutputStream writeChunk(string typeId)
    {
      this.storeCurrentChunk();
      int index1 = 0;
      for (int index2 = 0; index2 != 4; ++index2)
        this.m_chunkId[index2] = typeId[index1] != char.MinValue ? (sbyte) typeId[index1++] : (sbyte) 32;
      this.m_chunkId[4] = (sbyte) 0;
      return this.m_dataBuffer;
    }

    private void storeCurrentChunk()
    {
      int t = this.m_chunkBuffer.size();
      if (t == 0)
        return;
      this.m_outStream.write(this.m_chunkId, 4);
      this.m_outStream.writeInt(t);
      this.m_chunkBuffer.writeTo((OutputStream) this.m_outStream);
      if ((t & 1) == 1)
        this.m_outStream.write((byte) 0);
      this.m_chunkBuffer.reset();
    }
  }
}
