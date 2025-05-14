
// Type: microedition.m3g.TransformStack
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class TransformStack
  {
    private int m_Capacity;
    private int m_Position;
    private Transform[] m_Array;

    public TransformStack()
    {
      this.m_Capacity = 0;
      this.m_Position = 0;
      this.m_Array = (Transform[]) null;
    }

    public TransformStack(int capacity)
    {
      this.m_Capacity = 0;
      this.m_Position = 0;
      this.m_Array = (Transform[]) null;
      this.setCapacity(capacity);
    }

    public void setCapacity(int capacity)
    {
      this.m_Capacity = capacity;
      this.m_Array = new Transform[this.m_Capacity];
    }

    public Transform top() => this.m_Array[this.m_Position];

    public void push()
    {
      this.m_Array[this.m_Position + 1] = this.top();
      ++this.m_Position;
    }

    public void pop() => --this.m_Position;

    public void load(Transform transform) => this.m_Array[this.m_Position] = transform;

    public void loadIdentity() => this.top().setIdentity();

    public void translate(float x, float y, float z) => this.top().postTranslate(x, y, z);

    public void scale(float x, float y, float z) => this.top().postScale(x, y, z);

    public void loadAndClear(Transform transform)
    {
      this.m_Position = 0;
      this.m_Array[this.m_Position] = transform;
    }
  }
}
