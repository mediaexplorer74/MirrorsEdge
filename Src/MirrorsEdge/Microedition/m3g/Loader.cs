
// Type: microedition.m3g.Loader
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace microedition.m3g
{
  internal class Loader
  {
    private const int M3G_FILE_IDENTIFIER_LENGTH = 12;
    public static bool Loader_no_mipmap = false;
    public static string currentObjectName = "";
    private static byte[] float_buffer = new byte[4];
    private static BinaryReader float_reader = new BinaryReader((Stream) new MemoryStream(Loader.float_buffer));
    private static byte[] M3G_FILE_IDENTIFIER = new byte[12]
    {
      (byte) 171,
      (byte) 74,
      (byte) 83,
      (byte) 82,
      (byte) 49,
      (byte) 56,
      (byte) 52,
      (byte) 187,
      (byte) 13,
      (byte) 10,
      (byte) 26,
      (byte) 10
    };
    private static float[] matrix = new float[16];
    private static byte[] identifier = new byte[12];

    private static uint reverseBytes(uint value)
    {
      return (uint) ((int) ((value & 4278190080U) >> 24) & (int) byte.MaxValue | (int) ((value & 16711680U) >> 8) | ((int) value & 65280) << 8 | ((int) value & (int) byte.MaxValue) << 24);
    }

    private static short reverseBytes(short value)
    {
      return (short) (((int) value & 65280) >> 8 & (int) byte.MaxValue | ((int) value & (int) byte.MaxValue) << 8);
    }

    private static short readShortLE(BinaryReader in_) => in_.ReadInt16();

    private static int readUShortLE(BinaryReader in_)
    {
      return (int) Loader.readShortLE(in_) & (int) ushort.MaxValue;
    }

    private static int readIntLE(BinaryReader in_) => in_.ReadInt32();

    private static float readFloatLE(BinaryReader in_)
    {
      in_.Read(Loader.float_buffer, 0, 4);
      Loader.float_reader.BaseStream.Position = 0L;
      return Loader.float_reader.ReadSingle();
    }

    private static AnimationTrack createAnimationTrack() => new AnimationTrack();

    private static void AnimationTrack_setKeyframeSequence(
      AnimationTrack object_,
      KeyframeSequence sequence)
    {
      object_.setKeyframeSequence(sequence);
    }

    private static void AnimationTrack_setProperty(AnimationTrack object_, int property)
    {
      object_.setProperty(property);
    }

    private static Image2D createImage2D() => new Image2D();

    private static void Image2D_set(Image2D object_, int format, int width, int height)
    {
      object_.set(format, width, height);
    }

    private static void Image2D_commit(Image2D object_, byte[] palette, byte[] indices)
    {
      object_.commit(palette, indices);
    }

    private static void Image2D_commit(Image2D object_, byte[] pixels) => object_.commit(pixels, 0);

    private static KeyframeSequence createKeyframeSequence() => new KeyframeSequence();

    private static void KeyframeSequence_setInterpolation(
      KeyframeSequence object_,
      int interpolation)
    {
      object_.setInterpolation(interpolation);
    }

    private static void KeyframeSequence_setKeyframeSize(
      KeyframeSequence object_,
      int keyframeCount,
      int componentCount)
    {
      object_.setKeyframeSize(keyframeCount, componentCount);
    }

    private static Mesh createMesh() => new Mesh();

    private static void Mesh_setSubmeshCount(Mesh object_, int count)
    {
      object_.setSubmeshCount(count);
    }

    private static void Mesh_setVertexBuffer(Mesh object_, VertexBuffer vertexBuffer)
    {
      object_.setVertexBuffer(vertexBuffer);
    }

    private static void Mesh_preparePositionsForSkinning(Mesh object_)
    {
      object_.preparePositionsForSkinning();
    }

    private static SkinnedMesh createSkinnedMesh() => new SkinnedMesh();

    private static void SkinnedMesh_setSkeleton(SkinnedMesh object_, Group skeleton)
    {
      object_.setSkeleton(skeleton);
    }

    public static void SkinnedMesh_warmCache(SkinnedMesh object_)
    {
      object_.getBoneTransforms();
      object_.getVertexBuffer().setSkinIndices(object_.getSkinIndices());
      object_.getVertexBuffer().setSkinWeights(object_.getSkinWeights());
      object_.clearTemporaryData();
    }

    private static void SkinnedMesh_setLegacy(SkinnedMesh object_, bool enable)
    {
      object_.setLegacy(enable);
    }

    private static Texture2D createTexture2D() => new Texture2D();

    private static TriangleStripArray createTriangleStripArray() => new TriangleStripArray();

    private static void IndexBuffer_setIndices(IndexBuffer object_, int[] indices)
    {
      object_.setIndices(indices);
    }

    private static void IndexBuffer_setIndices(IndexBuffer object_, short[] indices)
    {
      object_.setIndices(indices);
    }

    private static void IndexBuffer_setFirstIndex(IndexBuffer object_, int firstIndex)
    {
      object_.setFirstIndex(firstIndex);
    }

    private static void TriangleStripArray_setStripLengths(
      TriangleStripArray object_,
      int[] lengths)
    {
      object_.setStripLengths(lengths);
    }

    private static VertexArray createVertexArray() => new VertexArray();

    private static void VertexArray_setFormat(
      VertexArray object_,
      int vertexCount,
      int componentCount,
      int componentType)
    {
      object_.setFormat(vertexCount, componentCount, componentType);
    }

    private static bool loadSection(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      if (in_.BaseStream.Position == in_.BaseStream.Length || in_.ReadByte() == byte.MaxValue)
        return false;
      int num = Loader.readIntLE(in_);
      Loader.readIntLE(in_);
      int length = num - 13;
      ChunkInputStream in_1 = new ChunkInputStream(in_, length);
      do
        ;
      while (Loader.loadObject(in_1, objectList, rootObjectList));
      in_.ReadInt32();
      return true;
    }

    private static bool loadObject(
      ChunkInputStream in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      if (in_.available() == 0)
        return false;
      int num1 = (int) ((BinaryReader) in_).ReadByte();
      if (num1 == -1)
        return false;
      int length = Loader.readIntLE((BinaryReader) in_);
      ChunkInputStream in_1 = new ChunkInputStream((BinaryReader) in_, length);
      Object3D object_ = (Object3D) null;
      switch (num1)
      {
        case 0:
          int num2 = (int) ((BinaryReader) in_1).ReadByte();
          int num3 = (int) ((BinaryReader) in_1).ReadByte();
          ((BinaryReader) in_1).ReadBoolean();
          Loader.readIntLE((BinaryReader) in_1);
          Loader.readIntLE((BinaryReader) in_1);
          byte[] buffer = new byte[length - 11];
          ((BinaryReader) in_1).Read(buffer, 0, length - 11);
          break;
        case 1:
          object_ = (Object3D) Loader.loadAnimationController((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 2:
          object_ = (Object3D) Loader.loadAnimationTrack((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 3:
          object_ = (Object3D) Loader.loadAppearance((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 5:
          object_ = (Object3D) Loader.loadCamera((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 6:
          object_ = (Object3D) Loader.loadCompositingMode((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 8:
          object_ = (Object3D) Loader.loadPolygonMode((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 9:
          object_ = (Object3D) new Group();
          Loader.loadGroup((Group) object_, (BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 10:
          object_ = (Object3D) Loader.loadImage2D((BinaryReader) in_1, objectList, rootObjectList);
          (object_ as Image2D).loadTexture(Loader.currentObjectName);
          break;
        case 11:
          object_ = (Object3D) Loader.loadTriangleStripArray((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 14:
          object_ = (Object3D) Loader.createMesh();
          Loader.loadMesh((Mesh) object_, (BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 16:
          object_ = (Object3D) Loader.loadSkinnedMesh((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 17:
          object_ = (Object3D) Loader.loadTexture2D((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 19:
          object_ = (Object3D) Loader.loadKeyframeSequence((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 20:
          object_ = (Object3D) Loader.loadVertexArray((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 21:
          object_ = (Object3D) Loader.loadVertexBuffer((BinaryReader) in_1, objectList, rootObjectList);
          break;
        case 22:
          object_ = (Object3D) Loader.loadWorld((BinaryReader) in_1, objectList, rootObjectList);
          break;
      }
      objectList.Add(object_);
      if (object_ != null)
        rootObjectList.Add(object_);
      return true;
    }

    private static void loadObject3D(
      Object3D object_,
      BinaryReader in_,
      List<Object3D> objects,
      List<Object3D> rootObjects)
    {
      int userID = Loader.readIntLE(in_);
      object_.setUserID(userID);
      int count1 = Loader.readIntLE(in_);
      object_.createAnimationTracks(count1);
      for (int index = 0; index < count1; ++index)
      {
        AnimationTrack reference = (AnimationTrack) Loader.getReference(in_, objects, rootObjects);
        object_.addAnimationTrack(index, reference);
      }
      int num = Loader.readIntLE(in_);
      for (int index = 0; index < num; ++index)
      {
        Loader.readIntLE(in_);
        int count2 = Loader.readIntLE(in_);
        byte[] buffer = new byte[count2];
        in_.Read(buffer, 0, count2);
      }
    }

    private static void loadTransformable(
      Transformable object_,
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      if (in_.ReadBoolean())
      {
        float x1 = Loader.readFloatLE(in_);
        float y1 = Loader.readFloatLE(in_);
        float z1 = Loader.readFloatLE(in_);
        object_.setTranslation(x1, y1, z1);
        float sx = Loader.readFloatLE(in_);
        float sy = Loader.readFloatLE(in_);
        float sz = Loader.readFloatLE(in_);
        object_.setScale(sx, sy, sz);
        float degrees = Loader.readFloatLE(in_);
        float x2 = Loader.readFloatLE(in_);
        float y2 = Loader.readFloatLE(in_);
        float z2 = Loader.readFloatLE(in_);
        object_.setOrientation(degrees, x2, y2, z2);
      }
      if (!in_.ReadBoolean())
        return;
      for (int index = 0; index < 16; ++index)
        Loader.matrix[index] = Loader.readFloatLE(in_);
      Transform transform = new Transform();
      transform.set(Loader.matrix);
      object_.setTransform(ref transform);
    }

    private static void loadNode(
      Node object_,
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Loader.loadTransformable((Transformable) object_, in_, objectList, rootObjectList);
      bool enable1 = in_.ReadBoolean();
      bool enable2 = in_.ReadBoolean();
      int num1 = (int) in_.ReadByte();
      int scope = in_.ReadInt32();
      if (in_.ReadBoolean())
      {
        int num2 = (int) in_.ReadByte();
        int num3 = (int) in_.ReadByte();
        Loader.getReference(in_, objectList, (List<Object3D>) null);
        Loader.getReference(in_, objectList, (List<Object3D>) null);
      }
      object_.setRenderingEnable(enable1);
      object_.setPickingEnable(enable2);
      object_.setAlphaFactor((float) num1 / (float) byte.MaxValue);
      object_.setScope(scope);
    }

    private static void loadGroup(
      Group object_,
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Loader.loadNode((Node) object_, in_, objectList, rootObjectList);
      int num = Loader.readIntLE(in_);
      for (int index = 0; index < num; ++index)
      {
        Node reference = (Node) Loader.getReference(in_, objectList, rootObjectList);
        object_.addChild(reference);
      }
    }

    private static AnimationController loadAnimationController(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      AnimationController object_ = new AnimationController();
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      float speed = Loader.readFloatLE(in_);
      float weight = Loader.readFloatLE(in_);
      int start = Loader.readIntLE(in_);
      int end = Loader.readIntLE(in_);
      float sequenceTime = Loader.readFloatLE(in_);
      int worldTime = Loader.readIntLE(in_);
      object_.setActiveInterval(start, end);
      object_.setPosition(sequenceTime, worldTime);
      object_.setSpeed(speed, worldTime);
      object_.setWeight(weight);
      return object_;
    }

    private static AnimationTrack loadAnimationTrack(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      AnimationTrack animationTrack = Loader.createAnimationTrack();
      Loader.loadObject3D((Object3D) animationTrack, in_, objectList, rootObjectList);
      KeyframeSequence reference1 = (KeyframeSequence) Loader.getReference(in_, objectList, rootObjectList);
      AnimationController reference2 = (AnimationController) Loader.getReference(in_, objectList, rootObjectList);
      int property = Loader.readIntLE(in_);
      Loader.AnimationTrack_setKeyframeSequence(animationTrack, reference1);
      Loader.AnimationTrack_setProperty(animationTrack, property);
      animationTrack.setController(reference2);
      return animationTrack;
    }

    private static Appearance loadAppearance(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Appearance object_ = new Appearance();
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      byte layer = in_.ReadByte();
      object_.setLayer((int) layer);
      CompositingMode reference1 = (CompositingMode) Loader.getReference(in_, objectList, rootObjectList);
      object_.setCompositingMode(reference1);
      Fog reference2 = (Fog) Loader.getReference(in_, objectList, rootObjectList);
      object_.setFog(reference2);
      PolygonMode reference3 = (PolygonMode) Loader.getReference(in_, objectList, rootObjectList);
      object_.setPolygonMode(reference3);
      Material reference4 = (Material) Loader.getReference(in_, objectList, rootObjectList);
      object_.setMaterial(reference4);
      int num = Loader.readIntLE(in_);
      for (int index = 0; index < num; ++index)
      {
        Texture2D reference5 = (Texture2D) Loader.getReference(in_, objectList, rootObjectList);
        object_.setTexture(index, reference5);
      }
      return object_;
    }

    private static Camera loadCamera(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Camera object_ = new Camera();
      Loader.loadNode((Node) object_, in_, objectList, rootObjectList);
      int num = (int) in_.ReadByte();
      if (num == 48)
      {
        Transform transform = new Transform();
        for (int index = 0; index != 16; ++index)
          Loader.matrix[index] = Loader.readFloatLE(in_);
        transform.set(Loader.matrix);
        object_.setGeneric(transform);
      }
      else
      {
        float fovy = Loader.readFloatLE(in_);
        float aspectRatio = Loader.readFloatLE(in_);
        float nearClip = Loader.readFloatLE(in_);
        float farClip = Loader.readFloatLE(in_);
        if (num == 49)
          object_.setParallel(fovy, aspectRatio, nearClip, farClip);
        else
          object_.setPerspective(fovy, aspectRatio, nearClip, farClip);
      }
      return object_;
    }

    private static CompositingMode loadCompositingMode(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      CompositingMode object_ = new CompositingMode();
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      bool enable1 = in_.ReadBoolean();
      bool enable2 = in_.ReadBoolean();
      bool enable3 = in_.ReadBoolean();
      bool enable4 = in_.ReadBoolean();
      int mode = (int) in_.ReadByte();
      float threshold = (float) in_.ReadByte() / (float) byte.MaxValue;
      float factor = Loader.readFloatLE(in_);
      float units = Loader.readFloatLE(in_);
      object_.setDepthTestEnable(enable1);
      object_.setDepthWriteEnable(enable2);
      object_.setColorWriteEnable(enable3);
      object_.setAlphaWriteEnable(enable4);
      object_.setBlending(mode);
      object_.setAlphaThreshold(threshold);
      object_.setDepthOffset(factor, units);
      return object_;
    }

    private static Image2D loadImage2D(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Image2D image2D = Loader.createImage2D();
      Loader.loadObject3D((Object3D) image2D, in_, objectList, rootObjectList);
      int format = (int) in_.ReadByte();
      if (Loader.Loader_no_mipmap)
      {
        format |= 32768;
        Loader.Loader_no_mipmap = false;
      }
      bool flag = in_.ReadBoolean();
      int width = Loader.readIntLE(in_);
      int height = Loader.readIntLE(in_);
      Loader.Image2D_set(image2D, format, width, height);
      if (!flag)
      {
        byte[] numArray1 = (byte[]) null;
        int count1 = Loader.readIntLE(in_);
        if (count1 > 0)
        {
          numArray1 = new byte[count1];
          in_.Read(numArray1, 0, count1);
        }
        int count2 = Loader.readIntLE(in_);
        byte[] numArray2 = new byte[count2];
        in_.Read(numArray2, 0, count2);
        if (numArray1 != null)
          Loader.Image2D_commit(image2D, numArray1, numArray2);
        else
          Loader.Image2D_commit(image2D, numArray2);
      }
      return image2D;
    }

    private static void loadIndexBuffer(
      IndexBuffer object_,
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      switch (in_.ReadByte())
      {
        case 0:
          int firstIndex1 = Loader.readIntLE(in_);
          Loader.IndexBuffer_setFirstIndex(object_, firstIndex1);
          break;
        case 1:
          int firstIndex2 = (int) in_.ReadByte();
          Loader.IndexBuffer_setFirstIndex(object_, firstIndex2);
          break;
        case 2:
          int firstIndex3 = (int) Loader.readShortLE(in_);
          Loader.IndexBuffer_setFirstIndex(object_, firstIndex3);
          break;
        case 128:
          int length1 = Loader.readIntLE(in_);
          short[] indices1 = new short[length1];
          for (int index = 0; index < length1; ++index)
            indices1[index] = (short) Loader.readIntLE(in_);
          Loader.IndexBuffer_setIndices(object_, indices1);
          break;
        case 129:
          int length2 = Loader.readIntLE(in_);
          short[] indices2 = new short[length2];
          for (int index = 0; index < length2; ++index)
            indices2[index] = (short) in_.ReadByte();
          Loader.IndexBuffer_setIndices(object_, indices2);
          break;
        case 130:
          int length3 = Loader.readIntLE(in_);
          short[] indices3 = new short[length3];
          for (int index = 0; index < length3; ++index)
            indices3[index] = Loader.readShortLE(in_);
          Loader.IndexBuffer_setIndices(object_, indices3);
          break;
      }
    }

    private static KeyframeSequence loadKeyframeSequence(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      KeyframeSequence keyframeSequence = Loader.createKeyframeSequence();
      Loader.loadObject3D((Object3D) keyframeSequence, in_, objectList, rootObjectList);
      int interpolation = (int) in_.ReadByte();
      int mode = (int) in_.ReadByte();
      int num1 = (int) in_.ReadByte();
      int duration = Loader.readIntLE(in_);
      int first = Loader.readIntLE(in_);
      int last = Loader.readIntLE(in_);
      int componentCount = Loader.readIntLE(in_);
      int keyframeCount = Loader.readIntLE(in_);
      Loader.KeyframeSequence_setInterpolation(keyframeSequence, interpolation);
      keyframeSequence.setRepeatMode(mode);
      keyframeSequence.setDuration(duration);
      Loader.KeyframeSequence_setKeyframeSize(keyframeSequence, keyframeCount, componentCount);
      keyframeSequence.setValidRange(first, last);
      switch (num1)
      {
        case 0:
          float[] numArray1 = new float[componentCount];
          for (int index1 = 0; index1 < keyframeCount; ++index1)
          {
            int time = Loader.readIntLE(in_);
            for (int index2 = 0; index2 < componentCount; ++index2)
              numArray1[index2] = Loader.readFloatLE(in_);
            keyframeSequence.setKeyframe(index1, time, numArray1, 0);
          }
          break;
        case 2:
          float[] numArray2 = new float[4];
          float[] numArray3 = new float[4];
          for (int index = 0; index < componentCount; ++index)
            numArray2[index] = Loader.readFloatLE(in_);
          for (int index = 0; index < componentCount; ++index)
            numArray3[index] = Loader.readFloatLE(in_);
          float[] numArray4 = new float[componentCount];
          for (int index3 = 0; index3 < keyframeCount; ++index3)
          {
            int time = Loader.readIntLE(in_);
            for (int index4 = 0; index4 < componentCount; ++index4)
            {
              float num2 = (float) Loader.readUShortLE(in_) * 1.52590219E-05f * numArray3[index4] + numArray2[index4];
              numArray4[index4] = num2;
            }
            keyframeSequence.setKeyframe(index3, time, numArray4, 0);
          }
          break;
      }
      return keyframeSequence;
    }

    private static void loadMesh(
      Mesh object_,
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Loader.loadNode((Node) object_, in_, objectList, rootObjectList);
      VertexBuffer reference1 = (VertexBuffer) Loader.getReference(in_, objectList, rootObjectList);
      Loader.Mesh_setVertexBuffer(object_, reference1);
      int count = Loader.readIntLE(in_);
      Loader.Mesh_setSubmeshCount(object_, count);
      for (int index = 0; index < count; ++index)
      {
        IndexBuffer reference2 = (IndexBuffer) Loader.getReference(in_, objectList, rootObjectList);
        object_.setIndexBuffer(index, reference2);
        Appearance reference3 = (Appearance) Loader.getReference(in_, objectList, rootObjectList);
        object_.setAppearance(index, reference3);
      }
    }

    private static SkinnedMesh loadSkinnedMesh(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      SkinnedMesh skinnedMesh = Loader.createSkinnedMesh();
      Loader.loadMesh((Mesh) skinnedMesh, in_, objectList, rootObjectList);
      Loader.Mesh_preparePositionsForSkinning((Mesh) skinnedMesh);
      Group reference1 = (Group) Loader.getReference(in_, objectList, rootObjectList);
      Loader.SkinnedMesh_setSkeleton(skinnedMesh, reference1);
      Loader.SkinnedMesh_setLegacy(skinnedMesh, true);
      int num = Loader.readIntLE(in_);
      for (int index = 0; index < num; ++index)
      {
        Node reference2 = (Node) Loader.getReference(in_, objectList, rootObjectList);
        int firstVertex = Loader.readIntLE(in_);
        int numVertices = Loader.readIntLE(in_);
        int weight = Loader.readIntLE(in_);
        skinnedMesh.addTransform(reference2, weight, firstVertex, numVertices);
      }
      Loader.SkinnedMesh_warmCache(skinnedMesh);
      return skinnedMesh;
    }

    private static PolygonMode loadPolygonMode(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      PolygonMode object_ = new PolygonMode();
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      int mode1 = (int) in_.ReadByte();
      int mode2 = (int) in_.ReadByte();
      int mode3 = (int) in_.ReadByte();
      bool enable1 = in_.ReadBoolean();
      bool enable2 = in_.ReadBoolean();
      bool enable3 = in_.ReadBoolean();
      object_.setCulling(mode1);
      object_.setShading(mode2);
      object_.setWinding(mode3);
      object_.setTwoSidedLightingEnable(enable1);
      object_.setLocalCameraLightingEnable(enable2);
      object_.setPerspectiveCorrectionEnable(enable3);
      return object_;
    }

    private static Texture2D loadTexture2D(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      Texture2D texture2D = Loader.createTexture2D();
      Loader.loadTransformable((Transformable) texture2D, in_, objectList, rootObjectList);
      Image2D reference = (Image2D) Loader.getReference(in_, objectList, rootObjectList);
      texture2D.setImage(reference);
      uint num1 = (uint) in_.ReadByte();
      uint num2 = (uint) in_.ReadByte();
      uint num3 = (uint) in_.ReadByte();
      int blending = (int) in_.ReadByte();
      int wrapS = (int) in_.ReadByte();
      int wrapT = (int) in_.ReadByte();
      int levelFilter = (int) in_.ReadByte();
      int imageFilter = (int) in_.ReadByte();
      texture2D.setBlendColor((int) num1 << 16 | (int) num2 << 8 | (int) num3);
      texture2D.setBlending(blending);
      texture2D.setWrapping(wrapS, wrapT);
      texture2D.setFiltering(levelFilter, imageFilter);
      return texture2D;
    }

    private static TriangleStripArray loadTriangleStripArray(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      TriangleStripArray triangleStripArray = Loader.createTriangleStripArray();
      Loader.loadIndexBuffer((IndexBuffer) triangleStripArray, in_, objectList, rootObjectList);
      int length = Loader.readIntLE(in_);
      int[] lengths = new int[length];
      for (int index = 0; index < length; ++index)
        lengths[index] = Loader.readIntLE(in_);
      Loader.TriangleStripArray_setStripLengths(triangleStripArray, lengths);
      return triangleStripArray;
    }

    private static VertexArray loadVertexArray(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      VertexArray vertexArray = Loader.createVertexArray();
      Loader.loadObject3D((Object3D) vertexArray, in_, objectList, rootObjectList);
      int componentType = (int) in_.ReadByte();
      int componentCount = (int) in_.ReadByte();
      int num1 = (int) in_.ReadByte();
      int num2 = (int) Loader.readShortLE(in_);
      Loader.VertexArray_setFormat(vertexArray, num2, componentCount, componentType);
      if (num1 == 0)
      {
        switch (componentType)
        {
          case 1:
            byte[] src1 = new byte[num2 * componentCount];
            for (int index = 0; index < num2 * componentCount; ++index)
              src1[index] = in_.ReadByte();
            vertexArray.set(0, num2, src1);
            break;
          case 2:
          case 5:
            short[] src2 = new short[num2 * componentCount];
            for (int index = 0; index < num2 * componentCount; ++index)
              src2[index] = Loader.readShortLE(in_);
            vertexArray.set(0, num2, src2);
            break;
          case 3:
            int[] src3 = new int[num2 * componentCount];
            for (int index = 0; index < num2 * componentCount; ++index)
              src3[index] = Loader.readIntLE(in_);
            vertexArray.set(0, num2, src3);
            break;
          case 4:
            float[] src4 = new float[num2 * componentCount];
            for (int index = 0; index < num2 * componentCount; ++index)
              src4[index] = Loader.readFloatLE(in_);
            vertexArray.set(0, num2, src4);
            break;
        }
      }
      return vertexArray;
    }

    private static VertexBuffer loadVertexBuffer(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      VertexBuffer object_ = new VertexBuffer();
      Loader.loadObject3D((Object3D) object_, in_, objectList, rootObjectList);
      uint num1 = (uint) in_.ReadByte();
      uint num2 = (uint) in_.ReadByte();
      uint num3 = (uint) in_.ReadByte();
      uint num4 = (uint) in_.ReadByte();
      object_.setDefaultColor((uint) ((int) num1 << 16 | (int) num2 << 8 | (int) num3 | (int) num4 << 24));
      VertexArray reference1 = (VertexArray) Loader.getReference(in_, objectList, rootObjectList);
      float[] bias1 = new float[3]
      {
        Loader.readFloatLE(in_),
        Loader.readFloatLE(in_),
        Loader.readFloatLE(in_)
      };
      float scale1 = Loader.readFloatLE(in_);
      object_.setPositions(reference1, scale1, bias1);
      VertexArray reference2 = (VertexArray) Loader.getReference(in_, objectList, rootObjectList);
      object_.setNormals(reference2);
      VertexArray reference3 = (VertexArray) Loader.getReference(in_, objectList, rootObjectList);
      object_.setColors(reference3);
      int num5 = Loader.readIntLE(in_);
      for (int index = 0; index < num5; ++index)
      {
        VertexArray reference4 = (VertexArray) Loader.getReference(in_, objectList, rootObjectList);
        float[] bias2 = new float[3]
        {
          Loader.readFloatLE(in_),
          Loader.readFloatLE(in_),
          Loader.readFloatLE(in_)
        };
        float scale2 = Loader.readFloatLE(in_);
        object_.setTexCoords(index, reference4, scale2, bias2);
      }
      return object_;
    }

    private static World loadWorld(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      World object_ = new World();
      Loader.loadGroup((Group) object_, in_, objectList, rootObjectList);
      Camera reference1 = (Camera) Loader.getReference(in_, objectList, rootObjectList);
      Background reference2 = (Background) Loader.getReference(in_, objectList, rootObjectList);
      object_.setActiveCamera(reference1);
      object_.setBackground(reference2);
      return object_;
    }

    private static Object3D getReference(
      BinaryReader in_,
      List<Object3D> objectList,
      List<Object3D> rootObjectList)
    {
      int index = Loader.readIntLE(in_);
      Object3D reference = objectList[index];
      if (rootObjectList != null && rootObjectList.Contains(reference))
        rootObjectList.Remove(reference);
      return reference;
    }

    public static List<Object3D> load(byte[] data, int offset)
    {
      return Loader.load((InputStream) new ByteArrayInputStream(data, offset, data.Length - offset));
    }

    public static List<Object3D> load(string filename)
    {
      return Loader.load((InputStream) new WP7InputStream(filename));
    }

    public static List<Object3D> load(InputStream stream)
    {
      if (stream == null)
        return new List<Object3D>();
      DataInputStream dataInputStream = new DataInputStream(stream);
      dataInputStream.read(ref Loader.identifier, 0, 12);
      bool flag = true;
      for (int index = 0; index < 12; ++index)
      {
        if (((int) Loader.identifier[index] & (int) byte.MaxValue) != ((int) Loader.M3G_FILE_IDENTIFIER[index] & (int) byte.MaxValue))
        {
          flag = false;
          break;
        }
      }
      if (!flag)
        return new List<Object3D>();
      List<Object3D> rootObjectList = new List<Object3D>(30);
      List<Object3D> objectList = new List<Object3D>(30);
      objectList.Add((Object3D) null);
      BinaryReader in_ = new BinaryReader(dataInputStream.getWP7Stream());
      int num = 0;
      while (Loader.loadSection(in_, objectList, rootObjectList))
        ++num;
      List<Object3D> object3DList = new List<Object3D>(rootObjectList.Count);
      for (int index = 0; index < rootObjectList.Count; ++index)
      {
        Object3D object3D = rootObjectList[index];
        object3DList.Add(object3D);
      }
      return object3DList;
    }
  }
}
