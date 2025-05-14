
// Type: microedition.m3g.SkinnedMesh
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;
using System.Collections.Generic;

#nullable disable
namespace microedition.m3g
{
  public class SkinnedMesh : Mesh
  {
    private const int MAX_WEIGHTS_PER_VERTEX = 4;
    public new const int M3G_UNIQUE_CLASS_ID = 16;
    private Group m_Skeleton;
    private bool m_Legacy;
    public List<SkinnedMesh.Bone> m_Bones;
    public Dictionary<Node, int> m_BoneIndex;
    public Dictionary<Node, int[]> m_WeightsByBone;
    public int[] m_SummedWeights;
    public Transform[] m_TransformArrayCached;
    public Node[] m_BoneArrayCached;
    private VertexArray m_SkinIndices;
    private VertexArray m_SkinWeights;

    public SkinnedMesh(
      VertexBuffer vertices,
      ref IndexBuffer[] submeshes,
      ref Appearance[] appearances,
      Group skeleton)
      : base(vertices, ref submeshes, ref appearances)
    {
      this.m_Skeleton = (Group) null;
      this.m_Legacy = true;
      this.m_WeightsByBone = (Dictionary<Node, int[]>) null;
      this.m_SummedWeights = (int[]) null;
      this.m_TransformArrayCached = (Transform[]) null;
      this.m_SkinIndices = (VertexArray) null;
      this.m_SkinWeights = (VertexArray) null;
      this.m_Bones = new List<SkinnedMesh.Bone>();
      this.m_BoneArrayCached = (Node[]) null;
      this.m_BoneIndex = new Dictionary<Node, int>();
      this.setSkeleton(skeleton);
    }

    public SkinnedMesh(
      VertexBuffer vertices,
      IndexBuffer submesh,
      Appearance appearance,
      Group skeleton)
      : base(vertices, submesh, appearance)
    {
      this.m_Skeleton = (Group) null;
      this.m_Legacy = true;
      this.m_WeightsByBone = (Dictionary<Node, int[]>) null;
      this.m_SummedWeights = (int[]) null;
      this.m_TransformArrayCached = (Transform[]) null;
      this.m_SkinIndices = (VertexArray) null;
      this.m_SkinWeights = (VertexArray) null;
      this.m_Bones = new List<SkinnedMesh.Bone>();
      this.m_BoneArrayCached = (Node[]) null;
      this.m_BoneIndex = new Dictionary<Node, int>();
      this.setSkeleton(skeleton);
    }

    public SkinnedMesh()
    {
      this.m_Skeleton = (Group) null;
      this.m_Legacy = true;
      this.m_WeightsByBone = (Dictionary<Node, int[]>) null;
      this.m_SummedWeights = (int[]) null;
      this.m_TransformArrayCached = (Transform[]) null;
      this.m_SkinIndices = (VertexArray) null;
      this.m_SkinWeights = (VertexArray) null;
      this.m_Bones = new List<SkinnedMesh.Bone>();
      this.m_BoneArrayCached = (Node[]) null;
      this.m_BoneIndex = new Dictionary<Node, int>();
    }

    public override void Destructor()
    {
      this.m_Skeleton = (Group) null;
      this.m_BoneIndex.Clear();
      this.m_WeightsByBone = (Dictionary<Node, int[]>) null;
      for (int index = 0; index < this.m_Bones.Count; ++index)
      {
        this.m_Bones[index].m_Target = (Node) null;
        if (this.m_TransformArrayCached != null)
          this.m_TransformArrayCached[index] = (Transform) null;
        this.m_Bones[index].m_RestTransform = (Transform) null;
      }
      this.m_SkinIndices = (VertexArray) null;
      this.m_SkinWeights = (VertexArray) null;
      base.Destructor();
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      SkinnedMesh parent = (SkinnedMesh) ret;
      Group skeleton1 = this.getSkeleton();
      Group dest = (Group) skeleton1.duplicate();
      parent.m_Bones = new List<SkinnedMesh.Bone>(this.m_Bones.Count);
      foreach (SkinnedMesh.Bone bone in this.m_Bones)
        parent.m_Bones.Add(new SkinnedMesh.Bone(bone));
      Group skeleton2 = parent.m_Skeleton;
      parent.m_Skeleton = dest;
      parent.m_Skeleton.setParent((Node) parent);
      parent.duplicateSkeleton(this, (Node) skeleton1, (Node) dest);
      if (parent.m_TransformArrayCached != null && parent.m_TransformArrayCached.Length == this.m_Bones.Count)
        return;
      int length = parent.m_TransformArrayCached != null ? parent.m_TransformArrayCached.Length : 0;
      int count = this.m_Bones.Count;
      Transform[] transformArray = new Transform[this.m_Bones.Count];
      for (int index = 0; index < length; ++index)
        transformArray[index] = parent.m_TransformArrayCached[index];
      for (int index = length; index < count; ++index)
        transformArray[index] = new Transform();
      parent.m_TransformArrayCached = transformArray;
    }

    protected virtual void duplicateToAddWeight(SkinnedMesh that, Node thisBone, Node thatBone)
    {
      int[] numArray;
      if (this.m_WeightsByBone.TryGetValue(thisBone, out numArray) && numArray != null)
      {
        int length = numArray.Length;
        for (int firstVertex = 0; firstVertex < length; ++firstVertex)
        {
          int weight = numArray[firstVertex];
          if (weight != 0)
            that.addTransform(thatBone, weight, firstVertex, 1);
        }
      }
      Group group1 = Group.m3g_cast((Object3D) thisBone);
      if (group1 == null)
        return;
      Group group2 = Group.m3g_cast((Object3D) thatBone);
      int childCount = group1.getChildCount();
      for (int index = 0; index < childCount; ++index)
        this.duplicateToAddWeight(that, group1.getChild(index), group2.getChild(index));
    }

    public override int getReferences(ref Object3D[] references)
    {
      int references1 = base.getReferences(ref references);
      int num1 = references1;
      if (this.getSkeleton() != null)
        ++references1;
      if (references != null && this.getSkeleton() != null)
      {
        Object3D[] object3DArray = references;
        int index = num1;
        int num2 = index + 1;
        Group skeleton = this.getSkeleton();
        object3DArray[index] = (Object3D) skeleton;
      }
      return references1;
    }

    protected override void findReferences(ref Object3DFinder finder)
    {
      base.findReferences(ref finder);
      finder.find((Object3D) this.getSkeleton());
    }

    protected override void animateReferences(int time)
    {
      if (!this.isRenderingEnabled())
        return;
      base.animateReferences(time);
      this.getSkeleton().animate(time);
    }

    public void setSkeleton(Group skeleton)
    {
      SkinnedMesh.requireBoneNotNull((Node) skeleton);
      this.requireVertexBufferNotNull();
      this.m_Skeleton = skeleton;
      this.m_Skeleton.setParent((Node) this);
      if (!this.isLegacy())
        return;
      this.addTransformWithoutWeightCheck((Node) this.m_Skeleton, 0, 0, this.getVertexBuffer().getVertexCount());
    }

    public void setLegacy(bool enable) => this.m_Legacy = enable;

    protected bool isLegacy() => this.m_Legacy;

    protected int[] getWeights(Node bone)
    {
      SkinnedMesh.requireBoneNotNull(bone);
      int[] weights = (int[]) null;
      if (this.m_WeightsByBone.TryGetValue(bone, out weights))
        SkinnedMesh.ensureBoneWeightsNotNull(weights);
      return weights;
    }

    public VertexArray getSkinIndices()
    {
      this.requireVertexBufferNotNull();
      VertexBuffer vertexBuffer = this.getVertexBuffer();
      VertexArray skinIndices = vertexBuffer.getSkinIndices();
      if (skinIndices == null)
      {
        if (this.m_SkinIndices == null)
        {
          int vertexCount = vertexBuffer.getVertexCount();
          this.m_SkinIndices = new VertexArray(vertexCount, 4, 1);
          Node[] boneArray = this.getBoneArray();
          int[] numArray1 = new int[4];
          int[] numArray2 = new int[4];
          byte[] src = new byte[4];
          for (int firstVertex = 0; firstVertex < vertexCount; ++firstVertex)
          {
            for (int index = 0; index < 4; ++index)
            {
              numArray1[index] = 0;
              numArray2[index] = 0;
            }
            for (int index1 = 0; index1 < boneArray.Length; ++index1)
            {
              int num = Math.Abs(this.getWeights(boneArray[index1])[firstVertex]);
              if (num != 0)
              {
                for (int index2 = 0; index2 < 4; ++index2)
                {
                  if (num > numArray2[index2])
                  {
                    for (int index3 = 3; index3 > index2; --index3)
                    {
                      numArray1[index3] = numArray1[index3 - 1];
                      numArray2[index3] = numArray2[index3 - 1];
                    }
                    numArray1[index2] = index1;
                    numArray2[index2] = num;
                    break;
                  }
                }
              }
            }
            for (int index = 0; index < 4; ++index)
            {
              int num = numArray1[index];
              if (num > (int) sbyte.MaxValue)
                num -= 256;
              src[index] = (byte) num;
            }
            this.m_SkinIndices.set(firstVertex, 1, src);
          }
        }
        skinIndices = this.m_SkinIndices;
      }
      return skinIndices;
    }

    public VertexArray getSkinWeights()
    {
      this.requireVertexBufferNotNull();
      VertexBuffer vertexBuffer = this.getVertexBuffer();
      VertexArray skinWeights = vertexBuffer.getSkinWeights();
      if (skinWeights == null)
      {
        if (this.m_SkinWeights == null)
        {
          VertexArray skinIndices = this.getSkinIndices();
          int componentCount = skinIndices.getComponentCount();
          int vertexCount = vertexBuffer.getVertexCount();
          this.m_SkinWeights = new VertexArray(vertexCount, componentCount, 4);
          Node[] boneArray = this.getBoneArray();
          byte[] values = new byte[4];
          int[] numArray = new int[4];
          float[] src = new float[4];
          for (int firstVertex = 0; firstVertex < vertexCount; ++firstVertex)
          {
            for (int index = 0; index < componentCount; ++index)
              numArray[index] = 0;
            skinIndices.get(firstVertex, 1, ref values);
            int val1 = 0;
            int num1 = 0;
            for (int index1 = 0; index1 < 4; ++index1)
            {
              int index2 = (int) values[index1] & (int) byte.MaxValue;
              int weight = this.getWeights(boneArray[index2])[firstVertex];
              val1 = Math.Min(val1, weight);
              num1 += Math.Abs(weight);
              numArray[index1] = weight;
            }
            float num2 = 1f;
            if (num1 > 0)
              num2 /= (float) num1;
            for (int index = 0; index < componentCount; ++index)
              src[index] = num1 <= 0 ? (index != 0 ? 0.0f : 1f) : (float) (numArray[index] - val1) * num2;
            this.m_SkinWeights.set(firstVertex, 1, src);
          }
        }
        skinWeights = this.m_SkinWeights;
      }
      return skinWeights;
    }

    protected void addBone(Node bone)
    {
      SkinnedMesh.requireBoneNotNull(bone);
      if (this.m_BoneIndex.TryGetValue(bone, out int _))
        return;
      Transform transform = new Transform();
      this.getTransformTo(bone, transform);
      this.m_BoneIndex[bone] = this.m_Bones.Count;
      this.m_Bones.Add(new SkinnedMesh.Bone()
      {
        m_Target = bone,
        m_RestTransform = transform
      });
    }

    protected void addWeight(Node bone, int weight, int firstVertex, int numVertices)
    {
      SkinnedMesh.requireBoneNotNull(bone);
      this.requireVertexBufferNotNull();
      int vertexCount = this.getVertexBuffer().getVertexCount();
      SkinnedMesh.requireValidVertexRange(firstVertex, numVertices, vertexCount);
      if (this.m_WeightsByBone == null)
        this.m_WeightsByBone = new Dictionary<Node, int[]>();
      int[] numArray1 = (int[]) null;
      this.m_WeightsByBone.TryGetValue(bone, out numArray1);
      if (numArray1 == null)
        numArray1 = new int[vertexCount];
      if (this.m_SummedWeights == null || this.m_SummedWeights.Length == 0)
        this.m_SummedWeights = new int[vertexCount];
      int[] numArray2 = numArray1;
      int num1 = firstVertex + numVertices;
      for (int index = firstVertex; index < num1; ++index)
      {
        int num2 = numArray2[index];
        this.m_SummedWeights[index] -= Math.Abs(num2);
        int num3 = num2 + weight;
        this.m_SummedWeights[index] += Math.Abs(num3);
        numArray2[index] = num3;
      }
      if (this.m_WeightsByBone.ContainsKey(bone))
        this.m_WeightsByBone[bone] = numArray1;
      else
        this.m_WeightsByBone.Add(bone, numArray1);
    }

    public void clearTemporaryData()
    {
      this.m_WeightsByBone = (Dictionary<Node, int[]>) null;
      this.m_SummedWeights = new int[0];
    }

    public Transform[] getBoneTransforms()
    {
      int count = this.m_Bones.Count;
      if (this.m_TransformArrayCached == null || this.m_TransformArrayCached.Length != count)
      {
        int length = this.m_TransformArrayCached != null ? this.m_TransformArrayCached.Length : 0;
        int num = count;
        Transform[] transformArray = new Transform[this.m_Bones.Count];
        for (int index = 0; index < length; ++index)
          transformArray[index] = this.m_TransformArrayCached[index];
        for (int index = length; index < num; ++index)
          transformArray[index] = new Transform();
        this.m_TransformArrayCached = transformArray;
      }
      for (int index = 0; index < count; ++index)
      {
        Node target = this.m_Bones[index].m_Target;
        Transform transform = this.m_TransformArrayCached[index];
        target.getTransformTo((Node) this, transform);
        transform.postMultiply(this.m_Bones[index].m_RestTransform);
      }
      return this.m_TransformArrayCached;
    }

    public void addTransform(Node bone, int weight, int firstVertex, int numVertices)
    {
      SkinnedMesh.requireWeightNotZero(weight);
      this.addTransformWithoutWeightCheck(bone, weight, firstVertex, numVertices);
    }

    private void addTransformWithoutWeightCheck(
      Node bone,
      int weight,
      int firstVertex,
      int numVertices)
    {
      this.requireIsLegacy();
      SkinnedMesh.requireValidVertexRange(firstVertex, numVertices, (int) ushort.MaxValue);
      this.addBone(bone);
      this.addWeight(bone, weight, firstVertex, numVertices);
    }

    protected Node[] getBoneArray()
    {
      if (this.m_BoneArrayCached == null || this.m_BoneArrayCached.Length != this.m_Bones.Count)
      {
        this.m_BoneArrayCached = new Node[this.m_Bones.Count];
        for (int index = 0; index < this.m_Bones.Count; ++index)
          this.m_BoneArrayCached[index] = this.m_Bones[index].m_Target;
      }
      return this.m_BoneArrayCached;
    }

    public Node[] getBones()
    {
      this.requireIsNotLegacy();
      return this.getBoneArray();
    }

    private Transform getBoneTransform(Node bone)
    {
      SkinnedMesh.requireBoneNotNull(bone);
      int index;
      this.m_BoneIndex.TryGetValue(bone, out index);
      return this.m_Bones[index].m_RestTransform;
    }

    private void getBoneTransform(Node bone, ref Transform transform)
    {
      transform.set(this.getBoneTransform(bone));
    }

    public int getBoneVertices(Node bone, int[] indices, float[] weights)
    {
      this.requireIsLegacy();
      SkinnedMesh.requireBoneNotNull(bone);
      this.requireVertexBufferNotNull();
      int[] numArray1;
      if (!this.m_WeightsByBone.TryGetValue(bone, out numArray1) || numArray1 == null)
        return 0;
      bool flag = indices.Length == 0;
      int vertexCount = this.getVertexBuffer().getVertexCount();
      int[] numArray2 = numArray1;
      int[] summedWeights = this.m_SummedWeights;
      int boneVertices = 0;
      for (int index = 0; index < vertexCount; ++index)
      {
        int num1 = numArray2[index];
        if (num1 != 0)
        {
          if (!flag)
          {
            indices[boneVertices] = index;
            int num2 = summedWeights[index];
            weights[boneVertices] = (float) num1 / (float) num2;
          }
          ++boneVertices;
        }
      }
      return boneVertices;
    }

    public Group getSkeleton() => this.m_Skeleton;

    public void setBones(Node[] bones)
    {
      this.requireIsNotLegacy();
      for (int index = 0; index < bones.Length; ++index)
        this.addBone(bones[index]);
    }

    private void requireIsNotLegacy()
    {
    }

    private void requireIsLegacy()
    {
    }

    private void requireVertexBufferNotNull()
    {
    }

    private static void requireWeightNotZero(int weight)
    {
    }

    private static void ensureBoneWeightsNotNull(int[] weights)
    {
    }

    private void duplicateSkeleton(SkinnedMesh sourcemesh, Node source, Node dest)
    {
      int index1;
      if (sourcemesh.m_BoneIndex.TryGetValue(source, out index1))
      {
        this.m_Bones[index1].m_Target = dest;
        this.m_BoneIndex[dest] = index1;
      }
      if (!(source is Group group1))
        return;
      Group group2 = dest as Group;
      int childCount = group1.getChildCount();
      for (int index2 = 0; index2 < childCount; ++index2)
        this.duplicateSkeleton(sourcemesh, group1.getChild(index2), group2.getChild(index2));
    }

    private static void requireBoneNotNull(Node bone)
    {
    }

    private static void requireValidVertexRange(int firstVertex, int numVertices, int vertexCount)
    {
    }

    public override int getM3GUniqueClassID() => 16;

    public static SkinnedMesh m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 16 ? (SkinnedMesh) obj : (SkinnedMesh) null;
    }

    public class Bone
    {
      public Node m_Target;
      public Transform m_RestTransform;

      public Bone()
      {
      }

      public Bone(SkinnedMesh.Bone other)
      {
        this.m_Target = other.m_Target;
        this.m_RestTransform = other.m_RestTransform;
      }
    }
  }
}
