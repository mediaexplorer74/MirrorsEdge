
// Type: support.QuadManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using microedition.m3g;
using midp;
using GameManager;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace support
{
  public class QuadManager
  {
    public const int SCOPE_NORMAL = 1;
    public const int SCOPE_WINDOW = 2;
    public const int SCOPE_POST_SCENE = 4;
    public const int SCOPE_BG_FADE = 8;
    public const int SCOPE_GAME_EFFECT = 16;
    public const int SCOPE_SPEEDRUN_HUD = 32;
    public const int SCOPE_REVEAL = 64;
    public const int SCOPE_MENU = 128;
    public const int SCOPE_MENU_WHITE = 256;
    public const int SCOPE_MGBUTTON = 512;
    public const int SCOPE_MAIN_MENU_LOGO_SMALL = 1024;
    public const int DEFAULT_SCOPE = 1;
    public const int QUAD_NULL = -1;
    private static Type Texture = (Type) null;
    private static Type Group = (Type) null;
    private static Type Mesh = (Type) null;
    private static Type Anim = (Type) null;
    private static Dictionary<string, short> Enums = new Dictionary<string, short>();
    public static short[] QUAD_TEXTURE;
    public static readonly short[] QUAD_TEXTURE_FULL = new short[58]
    {
      (short) 44,
      (short) 98,
      (short) 100,
      (short) 102,
      (short) 27,
      (short) 28,
      (short) 29,
      (short) 30,
      (short) 31,
      (short) 32,
      (short) 33,
      (short) 34,
      (short) 35,
      (short) 36,
      (short) 37,
      (short) 38,
      (short) 39,
      (short) 40,
      (short) 80,
      (short) 96,
      (short) 97,
      (short) 113,
      (short) 114,
      (short) 115,
      (short) 116,
      (short) 117,
      (short) 118,
      (short) 119,
      (short) 120,
      (short) 121,
      (short) 122,
      (short) 123,
      (short) 124,
      (short) 125,
      (short) 126,
      (short) sbyte.MaxValue,
      (short) 128,
      (short) 129,
      (short) 130,
      (short) 131,
      (short) 132,
      (short) 133,
      (short) 82,
      (short) 107,
      (short) 108,
      (short) 109,
      (short) 110,
      (short) 99,
      (short) 81,
      (short) 111,
      (short) 112,
      (short) 93,
      (short) 91,
      (short) 92,
      (short) 26,
      (short) 79,
      (short) 78,
      (short) 94
    };
    public static readonly short[] QUAD_TEXTURE_TRIAL = new short[29]
    {
      (short) 17,
      (short) 40,
      (short) 42,
      (short) 44,
      (short) 12,
      (short) 13,
      (short) 41,
      (short) 29,
      (short) 28,
      (short) 38,
      (short) 39,
      (short) 23,
      (short) 55,
      (short) 56,
      (short) 57,
      (short) 58,
      (short) 59,
      (short) 30,
      (short) 49,
      (short) 50,
      (short) 51,
      (short) 52,
      (short) 53,
      (short) 54,
      (short) 36,
      (short) 34,
      (short) 35,
      (short) 11,
      (short) 37
    };
    public static readonly short[] QUAD_BLENDING = new short[2]
    {
      (short) 64,
      (short) 65
    };
    public static readonly short[] QUAD_FILTER = new short[1]
    {
      (short) 209
    };
    private volatile bool m_threadLock;
    private World m_world;
    private Camera m_worldCamera;
    private CompositingMode[] m_compositingModeArray;
    private QuadImage[] m_imageArray;
    private QuadTexture[] m_textureArray;
    private QuadGroup[] m_groupArray;
    private QuadMesh[] m_meshArray;
    private AnimPlayerQuad[] m_animArray;
    private PolygonMode m_polyMode;
    private VertexArray m_commonPosVertexArray;
    private IndexBuffer m_indexBuffer;
    private bool m_splashCamera;
    private short[] texVertices = new short[8];

    public static void SetResources()
    {
      if (MirrorsEdge.TrialMode)
      {
        QuadManager.QUAD_TEXTURE = QuadManager.QUAD_TEXTURE_TRIAL;
        QuadManager.Texture = typeof (QuadManager.Texture_Trial);
        QuadManager.Group = typeof (QuadManager.Group_Trial);
        QuadManager.Mesh = typeof (QuadManager.Mesh_Trial);
        QuadManager.Anim = typeof (QuadManager.Anim_Trial);
      }
      else
      {
        QuadManager.QUAD_TEXTURE = QuadManager.QUAD_TEXTURE_FULL;
        QuadManager.Texture = typeof (QuadManager.Texture_Full);
        QuadManager.Group = typeof (QuadManager.Group_Full);
        QuadManager.Mesh = typeof (QuadManager.Mesh_Full);
        QuadManager.Anim = typeof (QuadManager.Anim_Full);
      }
    }

    public static short get(string name)
    {
      short num = -1;
      if (QuadManager.Enums.TryGetValue(name, out num))
        return num;
      if (name.StartsWith("TEXTURE_"))
        num = (short) (int) Enum.Parse(QuadManager.Texture, name, false);
      else if (name.StartsWith("GROUP_"))
        num = (short) (int) Enum.Parse(QuadManager.Group, name, false);
      else if (name.StartsWith("MESH_"))
        num = (short) (int) Enum.Parse(QuadManager.Mesh, name, false);
      else if (name.StartsWith("ANIM_"))
        num = (short) (int) Enum.Parse(QuadManager.Anim, name, false);
      QuadManager.Enums.Add(name, num);
      return num;
    }

    private async void threadLock()
    {
      while (this.m_threadLock)
      {
        //Thread.Sleep(1);
        await Task.Delay(1);
      }
      this.m_threadLock = true;
    }

    private void threadUnlock() => this.m_threadLock = false;

    public microedition.m3g.Node getWorld() => (microedition.m3g.Node) this.m_world;

    public VertexArray getPositionVertexArray() => this.m_commonPosVertexArray;

    public IndexBuffer getIndexBufferArray() => this.m_indexBuffer;

    public QuadManager()
    {
      this.m_threadLock = false;
      this.m_compositingModeArray = (CompositingMode[]) null;
      this.m_imageArray = (QuadImage[]) null;
      this.m_textureArray = (QuadTexture[]) null;
      this.m_groupArray = (QuadGroup[]) null;
      this.m_meshArray = (QuadMesh[]) null;
      this.m_animArray = (AnimPlayerQuad[]) null;
      this.m_polyMode = (PolygonMode) null;
      this.m_commonPosVertexArray = (VertexArray) null;
      this.m_indexBuffer = (IndexBuffer) null;
      this.m_world = new World();
      this.m_worldCamera = new Camera();
      this.m_worldCamera.setParallel(320f, 1.665625f, -10f, 10f);
      this.m_worldCamera.setOrientation(0.0f, 0.0f, 0.0f, 1f);
      this.m_worldCamera.setTranslation(266.5f, -160.5f, 0.0f);
      this.m_world.addChild((microedition.m3g.Node) this.m_worldCamera);
      this.m_world.setActiveCamera(this.m_worldCamera);
      Background background = new Background();
      background.setColorClearEnable(false);
      background.setDepthClearEnable(false);
      this.m_world.setBackground(background);
    }

    public void setSplashCamera()
    {
      if (this.m_splashCamera)
        return;
      this.m_worldCamera.setParallel(288f, 1.85069442f, -10f, 10f);
      this.m_worldCamera.setTranslation(266.5f, -160.5f, 0.0f);
      this.m_splashCamera = true;
    }

    public void resetCamera()
    {
      if (!this.m_splashCamera)
        return;
      this.m_worldCamera.setParallel(320f, 1.665625f, -10f, 10f);
      this.m_worldCamera.setTranslation(266.5f, -160.5f, 0.0f);
      this.m_splashCamera = false;
    }

    public void Destructor()
    {
      if (this.m_animArray != null)
      {
        for (int index = this.m_animArray.Length - 1; index != -1; --index)
          this.m_animArray[index].Destructor();
        this.m_animArray = (AnimPlayerQuad[]) null;
      }
      for (int index = 0; index != this.m_compositingModeArray.Length; ++index)
        this.m_compositingModeArray[index].Destructor();
      this.m_compositingModeArray = (CompositingMode[]) null;
      for (int index = 0; index != this.m_imageArray.Length; ++index)
        this.m_imageArray[index].Destructor();
      this.m_imageArray = (QuadImage[]) null;
      for (int index = 0; index != this.m_textureArray.Length; ++index)
        this.m_textureArray[index].Destructor();
      this.m_textureArray = (QuadTexture[]) null;
      for (int index = 0; index != this.m_groupArray.Length; ++index)
        this.m_groupArray[index].Destructor();
      this.m_groupArray = (QuadGroup[]) null;
      for (int index = 0; index != this.m_meshArray.Length; ++index)
        this.m_meshArray[index].Destructor();
      this.m_meshArray = (QuadMesh[]) null;
      this.m_world.Destructor();
      this.m_world = (World) null;
      this.m_worldCamera.Destructor();
      this.m_worldCamera = (Camera) null;
      this.m_polyMode.Destructor();
      this.m_polyMode = (PolygonMode) null;
      this.m_commonPosVertexArray.Destructor();
      this.m_commonPosVertexArray = (VertexArray) null;
      this.m_indexBuffer.Destructor();
      this.m_indexBuffer = (IndexBuffer) null;
    }

    public void loadQuadData(InputStream inStream, int screenWidth, int screenHeight)
    {
      int num1 = screenWidth;
      int num2 = screenHeight;
      DataInputStream dataInputStream = new DataInputStream(inStream);
      int length1 = (int) dataInputStream.readByte();
      this.m_compositingModeArray = new CompositingMode[length1];
      for (int index = 0; index != length1; ++index)
      {
        CompositingMode compositingMode = new CompositingMode();
        compositingMode.setAlphaThresholdx(0);
        compositingMode.setDepthTestEnable(false);
        compositingMode.setDepthWriteEnable(false);
        int mode = (int) QuadManager.QUAD_BLENDING[index];
        compositingMode.setBlending(mode);
        this.m_compositingModeArray[index] = compositingMode;
      }
      int length2 = (int) dataInputStream.readShort();
      this.m_imageArray = new QuadImage[length2];
      for (int index = 0; index != length2; ++index)
      {
        this.m_imageArray[index] = new QuadImage();
        this.m_imageArray[index].resId = (int) QuadManager.QUAD_TEXTURE[(int) dataInputStream.readShort()];
      }
      int length3 = (int) dataInputStream.readShort();
      this.m_textureArray = new QuadTexture[length3];
      for (int index = 0; index != length3; ++index)
      {
        QuadTexture quadTexture = new QuadTexture();
        this.m_textureArray[index] = quadTexture;
        quadTexture.imageIndex = (int) dataInputStream.readShort();
        quadTexture.blendMode = (int) dataInputStream.readShort();
        quadTexture.filtering = (int) QuadManager.QUAD_FILTER[(int) dataInputStream.readShort()];
        quadTexture.texX = (int) dataInputStream.readShort();
        quadTexture.texY = (int) dataInputStream.readShort();
        quadTexture.texW = (int) dataInputStream.readShort();
        quadTexture.texH = (int) dataInputStream.readShort();
      }
      int length4 = (int) dataInputStream.readShort();
      this.m_groupArray = new QuadGroup[length4];
      for (int index = 0; index != length4; ++index)
      {
        QuadGroup quadGroup = new QuadGroup();
        this.m_groupArray[index] = quadGroup;
        quadGroup.parentIndex = (int) dataInputStream.readShort();
        quadGroup.initVisible = dataInputStream.readByte() != (sbyte) 0;
        quadGroup.childGroupStart = (int) dataInputStream.readShort();
        quadGroup.childGroupNum = (int) dataInputStream.readShort();
        quadGroup.childMeshStart = (int) dataInputStream.readShort();
        quadGroup.childMeshNum = (int) dataInputStream.readShort();
      }
      int length5 = (int) dataInputStream.readShort();
      this.m_meshArray = new QuadMesh[length5];
      for (int index1 = 0; index1 != length5; ++index1)
      {
        QuadMesh quadMesh = new QuadMesh();
        this.m_meshArray[index1] = quadMesh;
        quadMesh.groupIndex = (int) dataInputStream.readShort();
        int index2 = (int) dataInputStream.readShort();
        quadMesh.textureIndex = index2;
        if (index2 != -1)
          quadMesh.texturePtr = this.m_textureArray[index2];
        quadMesh.scope = (int) dataInputStream.readShort();
        quadMesh.layer = (int) dataInputStream.readShort();
        int num3 = dataInputStream.readUnsignedByte();
        int num4 = dataInputStream.readUnsignedByte();
        int num5 = dataInputStream.readUnsignedByte();
        quadMesh.color = (uint) (-16777216 | num3 << 16 | num4 << 8 | num5);
        int num6 = (int) ((double) dataInputStream.readShort() * 533.0 / 480.0);
        quadMesh.attribF[0] = num6 << 16;
        quadMesh.attribF[1] = (int) dataInputStream.readShort() << 16;
        int num7 = (int) dataInputStream.readShort();
        if (num7 < 0)
          num7 += num1 + 1;
        int num8 = (int) ((double) num7 * 533.0 / 480.0) + 2;
        quadMesh.attribF[2] = num8 << 16;
        int num9 = (int) dataInputStream.readShort();
        if (num9 < 0)
          num9 += num2 + 1;
        quadMesh.attribF[3] = num9 << 16;
        quadMesh.attribF[5] = (int) dataInputStream.readShort() << 16;
        quadMesh.attribF[4] = dataInputStream.readInt();
        quadMesh.baseX = (float) quadMesh.attribF[0] * 1.52587891E-05f;
        quadMesh.attribF[0] = 0;
        quadMesh.baseY = (float) quadMesh.attribF[1] * 1.52587891E-05f;
        quadMesh.attribF[1] = 0;
        int num10 = (int) dataInputStream.readByte();
        if ((num10 & 2) != 0)
          quadMesh.baseX += (float) (num1 >> 1);
        else if ((num10 & 4) != 0)
          quadMesh.baseX += (float) num1;
        if ((num10 & 16) != 0)
          quadMesh.baseY += (float) (num2 >> 1);
        else if ((num10 & 32) != 0)
          quadMesh.baseY += (float) num2;
        quadMesh.initVisible = dataInputStream.readByte() != (sbyte) 0;
        if ((num8 & 1) == 1)
          quadMesh.baseX -= 0.5f;
        if ((num9 & 1) == 1)
          quadMesh.baseY -= 0.5f;
      }
      int length6 = (int) dataInputStream.readShort();
      this.m_animArray = new AnimPlayerQuad[length6];
      for (int index = 0; index != length6; ++index)
      {
        AnimPlayerQuad animPlayerQuad = new AnimPlayerQuad(this, (int) dataInputStream.readShort());
        this.m_animArray[index] = animPlayerQuad;
        int numAttributes = (int) dataInputStream.readByte();
        animPlayerQuad.setNumAttributes(numAttributes);
        for (int attribIndex = 0; attribIndex != numAttributes; ++attribIndex)
        {
          QuadMesh mesh = this.m_meshArray[(int) dataInputStream.readShort()];
          int attribute = (int) dataInputStream.readByte();
          animPlayerQuad.setupAttribute(attribIndex, mesh, attribute);
        }
        int numKeyframes = (int) dataInputStream.readByte();
        animPlayerQuad.setNumKeyframes(numKeyframes);
        for (int keyframeIndex = 0; keyframeIndex != numKeyframes; ++keyframeIndex)
        {
          int time = dataInputStream.readInt();
          animPlayerQuad.setKeyframeTime(keyframeIndex, time);
          for (int attribIndex = 0; attribIndex != numAttributes; ++attribIndex)
            animPlayerQuad.setAttribValue(attribIndex, keyframeIndex, dataInputStream.readInt());
        }
      }
      dataInputStream.close();
      if (this.m_polyMode == null)
      {
        this.m_polyMode = new PolygonMode();
        this.m_polyMode.setCulling(162);
      }
      if (this.m_commonPosVertexArray == null)
      {
        this.m_commonPosVertexArray = new VertexArray(4, 3, 2);
        this.m_commonPosVertexArray.set(0, 4, new short[12]
        {
          (short) -1,
          (short) -1,
          (short) 0,
          (short) 1,
          (short) -1,
          (short) 0,
          (short) -1,
          (short) 1,
          (short) 0,
          (short) 1,
          (short) 1,
          (short) 0
        });
      }
      if (this.m_indexBuffer != null)
        return;
      this.m_indexBuffer = (IndexBuffer) new TriangleStripArray(0, new int[1]
      {
        4
      });
    }

    public void loadQuads(int groupIndex)
    {
      QuadGroup group = this.m_groupArray[groupIndex];
      if (group.group == null)
        this.loadGroup(ref group);
      int childMeshStart = group.childMeshStart;
      int num1 = childMeshStart + group.childMeshNum;
      for (int meshIndex = childMeshStart; meshIndex != num1; ++meshIndex)
      {
        if (this.m_meshArray[meshIndex].mesh == null)
          this.loadMesh(meshIndex);
      }
      int childGroupStart = group.childGroupStart;
      int num2 = childGroupStart + group.childGroupNum;
      for (int groupIndex1 = childGroupStart; groupIndex1 != num2; ++groupIndex1)
        this.loadQuads(groupIndex1);
    }

    public bool loadQuads(int groupIndex, int numTextures)
    {
      int num1 = numTextures;
      QuadGroup group = this.m_groupArray[groupIndex];
      if (group.group == null)
        this.loadGroup(ref group);
      int childMeshStart = group.childMeshStart;
      int num2 = childMeshStart + group.childMeshNum;
      for (int meshIndex = childMeshStart; meshIndex != num2 && num1 != 0; ++meshIndex)
      {
        if (this.m_meshArray[meshIndex].mesh == null)
        {
          this.loadMesh(meshIndex);
          QuadTexture texturePtr = this.m_meshArray[meshIndex].texturePtr;
          int imageIndex = texturePtr.imageIndex;
          if (texturePtr.refCount == 1 && this.m_imageArray[imageIndex].refCount == 1)
            --num1;
        }
      }
      if (num1 != 0)
      {
        int childGroupStart = group.childGroupStart;
        int num3 = childGroupStart + group.childGroupNum;
        for (int groupIndex1 = childGroupStart; groupIndex1 != num3; ++groupIndex1)
          this.loadQuads(groupIndex1);
      }
      return num1 != 0;
    }

    private void loadGroup(ref QuadGroup quadGroup)
    {
      quadGroup.group = new microedition.m3g.Group();
      quadGroup.group.setRenderingEnable(quadGroup.initVisible);
      if (quadGroup.parentIndex == -1)
      {
        this.threadLock();
        this.m_world.addChild((microedition.m3g.Node) quadGroup.group);
        this.threadUnlock();
      }
      else
      {
        QuadGroup group = this.m_groupArray[quadGroup.parentIndex];
        if (group.group == null)
          this.loadGroup(ref group);
        this.threadLock();
        group.group.addChild((microedition.m3g.Node) quadGroup.group);
        this.threadUnlock();
      }
    }

    private void loadMesh(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      Appearance appearance = new Appearance();
      if (mesh.textureIndex < 0)
      {
        appearance.setCompositingMode(AppEngine.getM3GAssets().getCompositingMode(5));
      }
      else
      {
        QuadTexture texture = this.m_textureArray[mesh.textureIndex];
        appearance.setCompositingMode(this.m_compositingModeArray[texture.blendMode]);
        this.ensureTextureLoaded(ref texture);
        ++texture.refCount;
        appearance.setTexture(0, texture.texture);
      }
      appearance.setLayer(mesh.layer);
      appearance.setPolygonMode(this.m_polyMode);
      VertexBuffer vertexBuffer = new VertexBuffer();
      vertexBuffer.setDefaultColor(mesh.color);
      vertexBuffer.setPositions(this.m_commonPosVertexArray, 0.5f, (float[]) null);
      if (mesh.textureIndex >= 0)
      {
        QuadTexture texture = this.m_textureArray[mesh.textureIndex];
        float texMeshScale = this.m_imageArray[texture.imageIndex].texMeshScale;
        vertexBuffer.setTexCoords(0, texture.vertexArray, texMeshScale, (float[]) null);
      }
      mesh.vertexBuffer = vertexBuffer;
      microedition.m3g.Mesh child = new microedition.m3g.Mesh(mesh.vertexBuffer, this.m_indexBuffer, appearance);
      mesh.mesh = child;
      child.setScope(mesh.scope);
      this.threadLock();
      this.m_groupArray[mesh.groupIndex].group.addChild((microedition.m3g.Node) child);
      this.threadUnlock();
      child.setRenderingEnable(mesh.initVisible);
      this.updateMesh(mesh);
    }

    private void ensureTextureLoaded(ref QuadTexture quadTexture)
    {
      if (quadTexture.texture != null)
        return;
      QuadImage image1 = this.m_imageArray[quadTexture.imageIndex];
      if (image1.image == null)
      {
        ResourceManager resourceManager = AppEngine.getCanvas().getResourceManager();
        image1.image = resourceManager.loadM3GImage2D(image1.resId);
        Image2D image2 = image1.image;
        if ((double) image1.texMeshScale == 0.0)
        {
          int val1 = image2.getWidth() / Runtime.pixelScale;
          int val2 = image2.getHeight() / Runtime.pixelScale;
          int num = Math.Max(val1, val2);
          image1.texMeshScale = 1f / (float) num;
          image1.texMax = num;
          if (image2.texture2d.Width == 800 && image2.texture2d.Height == 480)
          {
            image1.texXMulti = (float) num / 480f;
            image1.texYMulti = (float) num / 320f;
          }
          else
          {
            image1.texXMulti = (float) num / (float) val1;
            image1.texYMulti = (float) num / (float) val2;
          }
        }
      }
      Texture2D texture2D = new Texture2D(image1.image);
      ++image1.refCount;
      texture2D.setFiltering(210, quadTexture.filtering);
      quadTexture.texture = texture2D;
      VertexArray vertexArray = new VertexArray(4, 2, 2);
      int texMax = image1.texMax;
      float texXmulti = image1.texXMulti;
      float texYmulti = image1.texYMulti;
      int texW = quadTexture.texW;
      int texH = quadTexture.texH;
      float num1;
      float num2;
      if (texW == 0)
      {
        num1 = 0.0f;
        num2 = (float) texMax;
      }
      else if (0 < texW)
      {
        num1 = (float) quadTexture.texX * texXmulti;
        num2 = num1 + (float) texW * texXmulti;
      }
      else
      {
        num2 = (float) quadTexture.texX * texXmulti;
        num1 = num2 - (float) texW * texXmulti;
      }
      float num3;
      float num4;
      if (texH == 0)
      {
        num3 = 0.0f;
        num4 = (float) texMax;
      }
      else if (0 < texH)
      {
        num4 = (float) texMax - (float) quadTexture.texY * texYmulti;
        num3 = (float) ((double) num4 - (double) texH * (double) texYmulti + (double) texYmulti / 2.0);
      }
      else
      {
        num3 = (float) texMax - (float) quadTexture.texY * texYmulti;
        num4 = num3 + (float) texH * texYmulti;
      }
      this.texVertices[0] = (short) num1;
      this.texVertices[1] = (short) num3;
      this.texVertices[2] = (short) num2;
      this.texVertices[3] = (short) num3;
      this.texVertices[4] = (short) num1;
      this.texVertices[5] = (short) num4;
      this.texVertices[6] = (short) num2;
      this.texVertices[7] = (short) num4;
      vertexArray.set(0, 4, this.texVertices);
      quadTexture.vertexArray = vertexArray;
    }

    public void addRawChild(microedition.m3g.Node child)
    {
      this.threadLock();
      M3GAssets.addNode((microedition.m3g.Group) this.m_world, child);
      this.threadUnlock();
    }

    public void removeRawChild(microedition.m3g.Node child)
    {
      this.threadLock();
      this.m_world.removeChild(child);
      this.threadUnlock();
    }

    public void freeQuads(int groupIndex)
    {
      QuadGroup group = this.m_groupArray[groupIndex];
      if (group.group == null)
        return;
      int childGroupStart = group.childGroupStart;
      int num1 = childGroupStart + group.childGroupNum;
      for (int groupIndex1 = childGroupStart; groupIndex1 != num1; ++groupIndex1)
        this.freeQuads(groupIndex1);
      int childMeshStart = group.childMeshStart;
      int num2 = childMeshStart + group.childMeshNum;
      for (int meshIndex = childMeshStart; meshIndex != num2; ++meshIndex)
        this.freeMesh(meshIndex);
      this.threadLock();
      if (group.parentIndex == -1)
        this.m_world.removeChild((microedition.m3g.Node) group.group);
      else
        this.m_groupArray[group.parentIndex].group.removeChild((microedition.m3g.Node) group.group);
      this.threadUnlock();
      group.group = (microedition.m3g.Group) null;
    }

    private void freeMesh(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      if (mesh.mesh == null)
        return;
      QuadGroup group = this.m_groupArray[mesh.groupIndex];
      this.threadLock();
      group.group.removeChild((microedition.m3g.Node) mesh.mesh);
      this.threadUnlock();
      mesh.mesh = (microedition.m3g.Mesh) null;
      if (mesh.textureIndex == -1)
        return;
      this.freeTexture(mesh.textureIndex);
    }

    private void freeTexture(int textureIndex)
    {
      QuadTexture texture = this.m_textureArray[textureIndex];
      --texture.refCount;
      if (texture.refCount != 0)
        return;
      texture.texture = (Texture2D) null;
      texture.vertexArray = (VertexArray) null;
      QuadImage image = this.m_imageArray[texture.imageIndex];
      --image.refCount;
      if (image.refCount != 0)
        return;
      image.image = (Image2D) null;
    }

    public void updateMesh(QuadMesh quadMesh)
    {
      if (quadMesh.mesh == null)
        return;
      float x = quadMesh.baseX + (float) quadMesh.attribF[0] * 1.52587891E-05f;
      float num = quadMesh.baseY + (float) quadMesh.attribF[1] * 1.52587891E-05f;
      int degrees = quadMesh.attribF[5];
      microedition.m3g.Mesh mesh = quadMesh.mesh;
      this.threadLock();
      mesh.setOrientationx(degrees, 0, 0, 65536);
      mesh.setTranslation(x, -num, 0.0f);
      mesh.setScalex(quadMesh.attribF[2], quadMesh.attribF[3], 0);
      mesh.setAlphaFactorx(quadMesh.attribF[4]);
      this.threadUnlock();
      quadMesh.modified = false;
    }

    public void updateAnim(int animIndex, int timeStep)
    {
      if (animIndex == -1 || this.m_animArray == null)
        return;
      this.m_animArray[animIndex].update(timeStep);
    }

    public void render(Graphics g)
    {
      Graphics3D graphics3D = AppEngine.getCanvas().getGraphics3D();
      this.threadLock();
      graphics3D.bindTarget((object) g);
      this.m_worldCamera.setScope(1);
      graphics3D.render(this.m_world);
      graphics3D.releaseTarget();
      this.threadUnlock();
    }

    public void render(Graphics3D g3D)
    {
      this.m_worldCamera.setScope(1);
      this.threadLock();
      g3D.render(this.m_world);
      this.threadUnlock();
    }

    public void render(Graphics g, int scope)
    {
      Graphics3D graphics3D = AppEngine.getCanvas().getGraphics3D();
      this.threadLock();
      graphics3D.bindTarget((object) g);
      this.m_worldCamera.setScope(scope);
      graphics3D.render(this.m_world);
      graphics3D.releaseTarget();
      this.threadUnlock();
    }

    public void render(Graphics3D g3D, int scope)
    {
      this.m_worldCamera.setScope(scope);
      this.threadLock();
      g3D.render(this.m_world);
      this.threadUnlock();
    }

    public AnimPlayerQuad getAnimPlayer(int animIndex)
    {
      return animIndex != -1 ? this.m_animArray[animIndex] : (AnimPlayerQuad) null;
    }

    public bool isAnimating(int animIndex)
    {
      return this.m_animArray != null && this.m_animArray[animIndex].isAnimating();
    }

    public int getAnimCurrentFrameIndex(int animIndex)
    {
      return this.m_animArray[animIndex].getCurrentKeyframe();
    }

    public int getAnimLastFrameIndex(int animIndex)
    {
      return this.m_animArray[animIndex].getLastKeyframeIndex();
    }

    public int getAnimFrameNum(int animIndex) => this.m_animArray[animIndex].getNumKeyframes();

    public void playAnim(int animIndex, int properties)
    {
      this.m_animArray[animIndex].playAnim(properties);
    }

    public void playAnimReverse(int animIndex, int properties)
    {
      this.m_animArray[animIndex].playAnimReverse(properties);
    }

    public void playAnimTo(int animIndex, int frameIndex)
    {
      if (animIndex == -1)
        return;
      this.m_animArray[animIndex].playAnimTo(frameIndex);
    }

    public void playAnimAcross(int animIndex, int properties, int startIndex, int endIndex)
    {
      this.m_animArray[animIndex].playAnimAcross(properties, startIndex, endIndex);
    }

    public void stopAnim(int animIndex) => this.m_animArray[animIndex].stopAnim();

    public void setAnimFrame(int animIndex, int index)
    {
      this.m_animArray[animIndex].snapToFrame(index);
    }

    public void setAnimTimeFS(int animIndex, int time)
    {
      this.m_animArray[animIndex].snapToTimeFS(time);
    }

    public void setAnimTime(int animIndex, int time)
    {
      this.m_animArray[animIndex].snapToTime(time);
    }

    public bool getGroupVisible(int groupIndex)
    {
      microedition.m3g.Group group = this.m_groupArray[groupIndex].group;
      return group != null && group.isRenderingEnabled();
    }

    public void setGroupVisible(int groupIndex, bool visible)
    {
      if (groupIndex == -1)
        return;
      this.m_groupArray[groupIndex].group?.setRenderingEnable(visible);
    }

    public bool getMeshVisible(int meshIndex)
    {
      microedition.m3g.Mesh mesh = this.m_meshArray[meshIndex].mesh;
      return mesh != null && mesh.isRenderingEnabled();
    }

    public void setMeshVisible(int meshIndex, bool visible)
    {
      if (meshIndex == -1)
        return;
      this.m_meshArray[meshIndex].mesh?.setRenderingEnable(visible);
    }

    public CompositingMode getCompositingMode(int compModeIndex)
    {
      return this.m_compositingModeArray[compModeIndex];
    }

    public QuadTexture getTexture(int texIndex) => this.m_textureArray[texIndex];

    public QuadGroup getGroup(int groupIndex) => this.m_groupArray[groupIndex];

    public QuadMesh getMesh(int meshIndex) => this.m_meshArray[meshIndex];

    public int getMeshX(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      int baseX = (int) mesh.baseX;
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        baseX += group.offsetX;
      }
      return baseX;
    }

    public int getMeshY(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      int baseY = (int) mesh.baseY;
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        baseY -= group.offsetY;
      }
      return baseY;
    }

    public int getMeshWidth(int meshIndex) => this.m_meshArray[meshIndex].attribF[2] >> 16;

    public int getMeshHeight(int meshIndex) => this.m_meshArray[meshIndex].attribF[3] >> 16;

    public int getMeshLeft(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      int num = mesh.attribF[0] - (mesh.attribF[2] >> 1);
      int meshLeft = (int) ((double) mesh.baseX + (double) (num >> 16));
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        meshLeft += group.offsetX;
      }
      return meshLeft;
    }

    public int getMeshRight(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      int num = mesh.attribF[0] + (mesh.attribF[2] >> 1);
      int meshRight = (int) ((double) mesh.baseX + (double) (num >> 16));
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        meshRight += group.offsetX;
      }
      return meshRight;
    }

    public int getMeshTop(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      int num = mesh.attribF[1] - (mesh.attribF[3] >> 1);
      int meshTop = (int) ((double) mesh.baseY + (double) (num >> 16));
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        meshTop -= group.offsetY;
      }
      return meshTop;
    }

    public int getMeshBottom(int meshIndex)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      int num = mesh.attribF[1] + (mesh.attribF[3] >> 1);
      int meshBottom = (int) ((double) mesh.baseY + (double) (num >> 16));
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        meshBottom -= group.offsetY;
      }
      return meshBottom;
    }

    public void getMeshBounds(
      int meshIndex,
      ref int left,
      ref int top,
      ref int width,
      ref int height)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      left = (int) ((double) mesh.baseX + (double) (mesh.attribF[0] - (mesh.attribF[2] >> 1) >> 16));
      top = (int) ((double) mesh.baseY + (double) (mesh.attribF[1] - (mesh.attribF[3] >> 1) >> 16));
      width = mesh.attribF[2] >> 16;
      height = mesh.attribF[3] >> 16;
      QuadGroup group;
      for (int index = mesh.groupIndex; index != -1; index = group.parentIndex)
      {
        group = this.m_groupArray[index];
        left += group.offsetX;
        top -= group.offsetY;
      }
    }

    public void setGroupPosition(int groupIndex, float x, float y)
    {
      QuadGroup group = this.m_groupArray[groupIndex];
      if (group.group == null)
        return;
      group.group.setTranslation(x, -y, 0.0f);
      group.offsetX = (int) x;
      group.offsetY = -(int) y;
    }

    public void setMeshPosition(int meshIndex, float x, float y, int anchor)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      mesh.baseX = x;
      mesh.baseY = y;
      if ((anchor & 1) != 0)
        mesh.baseX += (float) (mesh.attribF[2] >> 17);
      else if ((anchor & 4) != 0)
        mesh.baseX -= (float) (mesh.attribF[2] >> 17);
      if ((anchor & 8) != 0)
        mesh.baseY += (float) (mesh.attribF[3] >> 17);
      else if ((anchor & 32) != 0)
        mesh.baseY -= (float) (mesh.attribF[3] >> 17);
      this.updateMesh(mesh);
    }

    public void setMeshBounds(int meshIndex, float x, float y, float w, float h, int anchor)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      mesh.attribF[2] = (int) ((double) w * 65536.0);
      mesh.attribF[3] = (int) ((double) h * 65536.0);
      this.setMeshPosition(meshIndex, x, y, anchor);
    }

    public void setMeshAlpha(int meshIndex, float alpha)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      mesh.attribF[4] = (int) ((double) alpha * 65536.0);
      this.updateMesh(mesh);
    }

    public float getMeshAlpha(int meshIndex)
    {
      return (float) this.m_meshArray[meshIndex].attribF[4] / 65536f;
    }

    public void setMeshTexture(int meshIndex, int textureIndex)
    {
      if (meshIndex == -1 || textureIndex == -1)
        return;
      QuadTexture texture = this.m_textureArray[textureIndex];
      QuadMesh mesh = this.m_meshArray[meshIndex];
      if (mesh.textureIndex == textureIndex)
        return;
      mesh.textureIndex = textureIndex;
      if (mesh.mesh == null)
        return;
      this.threadLock();
      this.freeTexture(mesh.textureIndex);
      this.ensureTextureLoaded(ref texture);
      float texMeshScale = this.m_imageArray[texture.imageIndex].texMeshScale;
      mesh.vertexBuffer.setTexCoords(0, texture.vertexArray, texMeshScale, (float[]) null);
      Appearance appearance = mesh.mesh.getAppearance(0);
      appearance.setCompositingMode(this.m_compositingModeArray[texture.blendMode]);
      appearance.setTexture(0, texture.texture);
      mesh.textureIndex = textureIndex;
      ++texture.refCount;
      this.threadUnlock();
    }

    public bool isPointWithinMesh(int meshIndex, int x, int y)
    {
      return this.isPointWithinMesh(meshIndex, x, y, 0, 0);
    }

    public bool isPointWithinMesh(int meshIndex, int x, int y, int paddingX, int paddingY)
    {
      QuadMesh mesh = this.m_meshArray[meshIndex];
      if (mesh.mesh == null || !mesh.mesh.isRenderingEnabled())
        return false;
      int left = 0;
      int top = 0;
      int width = 0;
      int height = 0;
      this.getMeshBounds(meshIndex, ref left, ref top, ref width, ref height);
      int num1 = left - paddingX;
      top -= paddingY;
      int num2 = width + (paddingX << 1);
      height += paddingY << 1;
      return num1 <= x && x <= num1 + num2 && top <= y && y <= top + height;
    }

    public void getCenterPointWithinMesh(int meshIndex, out int x, out int y)
    {
      if (this.m_meshArray[meshIndex].mesh == null)
      {
        x = y = 0;
      }
      else
      {
        int left = 0;
        int top = 0;
        int width = 0;
        int height = 0;
        this.getMeshBounds(meshIndex, ref left, ref top, ref width, ref height);
        x = left + (width >> 1);
        y = top + (height >> 1);
      }
    }

    public void setClipFromMesh(Graphics g, int meshIndex)
    {
      int left = 0;
      int top = 0;
      int width = 0;
      int height = 0;
      this.getMeshBounds(meshIndex, ref left, ref top, ref width, ref height);
      g.setClip(left, top, width, height);
    }

    public enum Texture_Full
    {
      TEXTURE_EA_LOGO,
      TEXTURE_BUTTON_MAJOR,
      TEXTURE_BUTTON_MINOR,
      TEXTURE_BUTTON_POSITIVE,
      TEXTURE_BUTTON_NEGATIVE,
      TEXTURE_BAG_ICON,
      TEXTURE_LEVEL_COMPLETE_BG_0_0,
      TEXTURE_LEVEL_COMPLETE_BG_0_1,
      TEXTURE_LEVEL_COMPLETE_BG_1_1,
      TEXTURE_LEVEL_COMPLETE_BG_1_2,
      TEXTURE_LEVEL_COMPLETE_BG_2_1,
      TEXTURE_LEVEL_COMPLETE_BG_2_2,
      TEXTURE_LEVEL_COMPLETE_BG_3_1,
      TEXTURE_LEVEL_COMPLETE_BG_3_2,
      TEXTURE_LEVEL_COMPLETE_BG_4_1,
      TEXTURE_LEVEL_COMPLETE_BG_4_2,
      TEXTURE_LEVEL_COMPLETE_BG_5_1,
      TEXTURE_LEVEL_COMPLETE_BG_5_2,
      TEXTURE_LEVEL_COMPLETE_BG_6_1,
      TEXTURE_LEVEL_COMPLETE_BG_6_2,
      TEXTURE_MENU_INTRO_TITLE,
      TEXTURE_MENU_ARROW_LEFT_ACTIVE,
      TEXTURE_MENU_ARROW_RIGHT_ACTIVE,
      TEXTURE_MENU_ARROW_LEFT_DISABLED,
      TEXTURE_MENU_ARROW_RIGHT_DISABLED,
      TEXTURE_UNLOCKABLE_01,
      TEXTURE_UNLOCKABLE_02,
      TEXTURE_UNLOCKABLE_03,
      TEXTURE_UNLOCKABLE_04,
      TEXTURE_UNLOCKABLE_05,
      TEXTURE_UNLOCKABLE_06,
      TEXTURE_UNLOCKABLE_07,
      TEXTURE_UNLOCKABLE_08,
      TEXTURE_UNLOCKABLE_09,
      TEXTURE_UNLOCKABLE_10,
      TEXTURE_UNLOCKABLE_11,
      TEXTURE_UNLOCKABLE_12,
      TEXTURE_UNLOCKABLE_13,
      TEXTURE_UNLOCKABLE_14,
      TEXTURE_UNLOCKABLE_15,
      TEXTURE_UNLOCKABLE_16,
      TEXTURE_UNLOCKABLE_17,
      TEXTURE_UNLOCKABLE_18,
      TEXTURE_UNLOCKABLE_19,
      TEXTURE_UNLOCKABLE_20,
      TEXTURE_UNLOCKABLE_LOCKED,
      TEXTURE_MENU_INTRO_GAME_REVEAL_1,
      TEXTURE_MENU_INTRO_GAME_REVEAL_2,
      TEXTURE_MENU_INTRO_GAME_REVEAL_3,
      TEXTURE_PAIN_EFFECT,
      TEXTURE_MENU_RIBBON_1_RED_LEFT,
      TEXTURE_MENU_RIBBON_1_RED_RIGHT,
      TEXTURE_MENU_RIBBON_1_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_1_WHITE_RIGHT,
      TEXTURE_MENU_RIBBON_2_RED_LEFT,
      TEXTURE_MENU_RIBBON_2_RED_RIGHT,
      TEXTURE_MENU_RIBBON_2_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_2_WHITE_RIGHT,
      TEXTURE_MENU_RIBBON_3_RED_LEFT,
      TEXTURE_MENU_RIBBON_3_RED_RIGHT,
      TEXTURE_MENU_RIBBON_3_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_3_WHITE_RIGHT,
      TEXTURE_MENU_RIBBON_4_RED_LEFT,
      TEXTURE_MENU_RIBBON_4_RED_RIGHT,
      TEXTURE_MENU_RIBBON_4_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_4_WHITE_RIGHT,
      TEXTURE_MENU_MG_BUTTON,
      TEXTURE_MENU_TITLE,
      TEXTURE_TICKER_BAR,
      TEXTURE_WINDOW_BACKING_LEFTTOP,
      TEXTURE_WINDOW_BACKING_TOP,
      TEXTURE_WINDOW_BACKING_RIGHTTOP,
      TEXTURE_WINDOW_BACKING_LEFT,
      TEXTURE_WINDOW_BACKING_FILL,
      TEXTURE_WINDOW_BACKING_RIGHT,
      TEXTURE_WINDOW_BACKING_LEFTBOTTOM,
      TEXTURE_WINDOW_BACKING_BOTTOM,
      TEXTURE_WINDOW_BACKING_RIGHTBOTTOM,
      TEXTURE_WINDOW_SLIDER,
      TEXTURE_WINDOW_SOUND_OFF,
      TEXTURE_WINDOW_SOUND_ON,
      TEXTURE_BUTTON_EFFECT,
      TEXTURE_BADGE_ENABLED,
      TEXTURE_BADGE_DISABLED,
      TEXTURE_SPEEDRUN_STARS,
    }

    public enum Texture_Trial
    {
      TEXTURE_EA_LOGO,
      TEXTURE_BUTTON_MAJOR,
      TEXTURE_BUTTON_MINOR,
      TEXTURE_BUTTON_POSITIVE,
      TEXTURE_BUTTON_NEGATIVE,
      TEXTURE_BAG_ICON,
      TEXTURE_LEVEL_COMPLETE_BG_0_0,
      TEXTURE_LEVEL_COMPLETE_BG_1_1,
      TEXTURE_MENU_MG_BUTTON,
      TEXTURE_MENU_TITLE,
      TEXTURE_MENU_INTRO_TITLE,
      TEXTURE_MENU_ARROW_LEFT_ACTIVE,
      TEXTURE_MENU_ARROW_RIGHT_ACTIVE,
      TEXTURE_MENU_ARROW_LEFT_DISABLED,
      TEXTURE_MENU_ARROW_RIGHT_DISABLED,
      TEXTURE_MENU_INTRO_GAME_BG,
      TEXTURE_MENU_INTRO_GAME_REVEAL_1,
      TEXTURE_MENU_INTRO_GAME_REVEAL_2,
      TEXTURE_MENU_INTRO_GAME_REVEAL_3,
      TEXTURE_UPSELL_1,
      TEXTURE_UPSELL_1_REVEAL_1,
      TEXTURE_UPSELL_1_REVEAL_2,
      TEXTURE_UPSELL_2,
      TEXTURE_UPSELL_2_REVEAL_1,
      TEXTURE_UPSELL_2_REVEAL_2,
      TEXTURE_UPSELL_3,
      TEXTURE_UPSELL_3_REVEAL_1,
      TEXTURE_UPSELL_3_REVEAL_2,
      TEXTURE_UPSELL_4,
      TEXTURE_UPSELL_4_REVEAL_1,
      TEXTURE_UPSELL_4_REVEAL_2,
      TEXTURE_UPSELL_5,
      TEXTURE_UPSELL_5_REVEAL_1,
      TEXTURE_UPSELL_5_REVEAL_2,
      TEXTURE_PAIN_EFFECT,
      TEXTURE_MENU_RIBBON_1_RED_LEFT,
      TEXTURE_MENU_RIBBON_1_RED_RIGHT,
      TEXTURE_MENU_RIBBON_1_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_1_WHITE_RIGHT,
      TEXTURE_MENU_RIBBON_2_RED_LEFT,
      TEXTURE_MENU_RIBBON_2_RED_RIGHT,
      TEXTURE_MENU_RIBBON_2_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_2_WHITE_RIGHT,
      TEXTURE_MENU_RIBBON_3_RED_LEFT,
      TEXTURE_MENU_RIBBON_3_RED_RIGHT,
      TEXTURE_MENU_RIBBON_3_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_3_WHITE_RIGHT,
      TEXTURE_MENU_RIBBON_4_RED_LEFT,
      TEXTURE_MENU_RIBBON_4_RED_RIGHT,
      TEXTURE_MENU_RIBBON_4_WHITE_LEFT,
      TEXTURE_MENU_RIBBON_4_WHITE_RIGHT,
      TEXTURE_TICKER_BAR,
      TEXTURE_WINDOW_BACKING_LEFTTOP,
      TEXTURE_WINDOW_BACKING_TOP,
      TEXTURE_WINDOW_BACKING_RIGHTTOP,
      TEXTURE_WINDOW_BACKING_LEFT,
      TEXTURE_WINDOW_BACKING_FILL,
      TEXTURE_WINDOW_BACKING_RIGHT,
      TEXTURE_WINDOW_BACKING_LEFTBOTTOM,
      TEXTURE_WINDOW_BACKING_BOTTOM,
      TEXTURE_WINDOW_BACKING_RIGHTBOTTOM,
      TEXTURE_WINDOW_SLIDER,
      TEXTURE_WINDOW_SOUND_OFF,
      TEXTURE_WINDOW_SOUND_ON,
      TEXTURE_BUTTON_EFFECT,
      TEXTURE_SPEEDRUN_STARS,
    }

    public enum Group_Full
    {
      GROUP_LEVEL_COMPLETE_BACKGROUNDS,
      GROUP_SCENESTARTUP,
      GROUP_SCENEMENU,
      GROUP_SCENEGAME,
      GROUP_APPENGINE,
      GROUP_LEVEL_COMPLETE_BG_0_0,
      GROUP_LEVEL_COMPLETE_BG_0_1,
      GROUP_LEVEL_COMPLETE_BG_1_1,
      GROUP_LEVEL_COMPLETE_BG_1_2,
      GROUP_LEVEL_COMPLETE_BG_2_1,
      GROUP_LEVEL_COMPLETE_BG_2_2,
      GROUP_LEVEL_COMPLETE_BG_3_1,
      GROUP_LEVEL_COMPLETE_BG_3_2,
      GROUP_LEVEL_COMPLETE_BG_4_1,
      GROUP_LEVEL_COMPLETE_BG_4_2,
      GROUP_LEVEL_COMPLETE_BG_5_1,
      GROUP_LEVEL_COMPLETE_BG_5_2,
      GROUP_LEVEL_COMPLETE_BG_6_1,
      GROUP_LEVEL_COMPLETE_BG_6_2,
      GROUP_MENU_INTRO_TITLE,
      GROUP_MENU_LEVEL_SELECT,
      GROUP_MENU_LEADERBOARD,
      GROUP_UNLOCKABLES,
      GROUP_MENU_INTRO_GAME,
      GROUP_GAME_HUD_MENU,
      GROUP_GAME_HUD_GAME,
      GROUP_GAME_POST_EFFECT,
      GROUP_FADE,
      GROUP_TICKER_BAR,
      GROUP_MENU_MAIN,
      GROUP_BUTTON_EFFECT,
      GROUP_NETWORK_WAIT_EFFECT,
      GROUP_SLIDER,
      GROUP_LEVEL_COMPLETE,
      GROUP_ARROWS,
      GROUP_WINDOW,
      GROUP_BADGES,
      GROUP_SPEEDRUN_STARS,
      GROUP_MENU_MAIN_RIBBON_1_RED,
      GROUP_MENU_MAIN_RIBBON_2_RED,
      GROUP_MENU_MAIN_RIBBON_3_RED,
      GROUP_MENU_MAIN_RIBBON_4_RED,
      GROUP_MENU_MAIN_RIBBON_1_WHITE,
      GROUP_MENU_MAIN_RIBBON_2_WHITE,
      GROUP_MENU_MAIN_RIBBON_3_WHITE,
      GROUP_MENU_MAIN_RIBBON_4_WHITE,
      GROUP_WINDOW_SCROLLBAR,
      GROUP_WINDOW_BUTTON_MAJOR,
      GROUP_WINDOW_BUTTON_MINOR,
      GROUP_WINDOW_BACKING,
      GROUP_SPEEDRUN_STARS_MENU,
      GROUP_SPEEDRUN_STARS_HUD,
      GROUP_SPEEDRUN_STARS_LEVEL_COMPLETE,
    }

    public enum Group_Trial
    {
      GROUP_LEVEL_COMPLETE_BACKGROUNDS,
      GROUP_SCENESTARTUP,
      GROUP_SCENEMENU,
      GROUP_SCENEGAME,
      GROUP_APPENGINE,
      GROUP_LEVEL_COMPLETE_BG_0_0,
      GROUP_LEVEL_COMPLETE_BG_1_1,
      GROUP_MENU_INTRO_TITLE,
      GROUP_MENU_LEVEL_SELECT,
      GROUP_MENU_INTRO_GAME,
      GROUP_MENU_UPSELL,
      GROUP_UPSELL_1_REVEAL,
      GROUP_UPSELL_2_REVEAL,
      GROUP_UPSELL_3_REVEAL,
      GROUP_UPSELL_4_REVEAL,
      GROUP_UPSELL_5_REVEAL,
      GROUP_GAME_HUD_MENU,
      GROUP_GAME_HUD_GAME,
      GROUP_GAME_POST_EFFECT,
      GROUP_FADE,
      GROUP_TICKER_BAR,
      GROUP_MENU_MAIN,
      GROUP_BUTTON_EFFECT,
      GROUP_SLIDER,
      GROUP_LEVEL_COMPLETE,
      GROUP_ARROWS,
      GROUP_WINDOW,
      GROUP_SPEEDRUN_STARS,
      GROUP_MENU_MAIN_RIBBON_1_RED,
      GROUP_MENU_MAIN_RIBBON_2_RED,
      GROUP_MENU_MAIN_RIBBON_3_RED,
      GROUP_MENU_MAIN_RIBBON_4_RED,
      GROUP_MENU_MAIN_RIBBON_1_WHITE,
      GROUP_MENU_MAIN_RIBBON_2_WHITE,
      GROUP_MENU_MAIN_RIBBON_3_WHITE,
      GROUP_MENU_MAIN_RIBBON_4_WHITE,
      GROUP_WINDOW_SCROLLBAR,
      GROUP_WINDOW_BUTTON_MAJOR,
      GROUP_WINDOW_BUTTON_MINOR,
      GROUP_WINDOW_BACKING,
      GROUP_SPEEDRUN_STARS_MENU,
      GROUP_SPEEDRUN_STARS_HUD,
      GROUP_SPEEDRUN_STARS_LEVEL_COMPLETE,
    }

    public enum Mesh_Full
    {
      MESH_LEVEL_COMPLETE_BG_0_0,
      MESH_LEVEL_COMPLETE_BG_0_1,
      MESH_LEVEL_COMPLETE_BG_1_1,
      MESH_LEVEL_COMPLETE_BG_1_2,
      MESH_LEVEL_COMPLETE_BG_2_1,
      MESH_LEVEL_COMPLETE_BG_2_2,
      MESH_LEVEL_COMPLETE_BG_3_1,
      MESH_LEVEL_COMPLETE_BG_3_2,
      MESH_LEVEL_COMPLETE_BG_4_1,
      MESH_LEVEL_COMPLETE_BG_4_2,
      MESH_LEVEL_COMPLETE_BG_5_1,
      MESH_LEVEL_COMPLETE_BG_5_2,
      MESH_LEVEL_COMPLETE_BG_6_1,
      MESH_LEVEL_COMPLETE_BG_6_2,
      MESH_EA_LOGO,
      MESH_MENU_INTRO_TITLE,
      MESH_MENU_LEVEL_SELECT_ARROW_LEFT_ACTIVE,
      MESH_MENU_LEVEL_SELECT_ARROW_RIGHT_ACTIVE,
      MESH_MENU_LEVEL_SELECT_ARROW_LEFT_DISABLED,
      MESH_MENU_LEVEL_SELECT_ARROW_RIGHT_DISABLED,
      MESH_MENU_LEVEL_SELECT_BUTTON_POSITIVE,
      MESH_MENU_LEVEL_SELECT_BUTTON_NEGATIVE,
      MESH_MENU_LEADERBOARD_BUTTON_WORLD,
      MESH_MENU_LEADERBOARD_BUTTON_FRIENDS,
      MESH_MENU_LEADERBOARD_BUTTON_LOGOUT,
      MESH_MENU_LEADERBOARD_BUTTON_BACK,
      MESH_UNLOCKABLE_01,
      MESH_UNLOCKABLE_02,
      MESH_UNLOCKABLE_03,
      MESH_UNLOCKABLE_04,
      MESH_UNLOCKABLE_05,
      MESH_UNLOCKABLE_06,
      MESH_UNLOCKABLE_07,
      MESH_UNLOCKABLE_08,
      MESH_UNLOCKABLE_09,
      MESH_UNLOCKABLE_10,
      MESH_UNLOCKABLE_11,
      MESH_UNLOCKABLE_12,
      MESH_UNLOCKABLE_13,
      MESH_UNLOCKABLE_14,
      MESH_UNLOCKABLE_15,
      MESH_UNLOCKABLE_16,
      MESH_UNLOCKABLE_17,
      MESH_UNLOCKABLE_18,
      MESH_UNLOCKABLE_19,
      MESH_UNLOCKABLE_20,
      MESH_UNLOCKABLE_LOCKED,
      MESH_MENU_INTRO_GAME_BG,
      MESH_MENU_INTRO_GAME_REVEAL_1,
      MESH_MENU_INTRO_GAME_REVEAL_2,
      MESH_MENU_INTRO_GAME_REVEAL_3,
      MESH_HUD_BAG_ICON_MENU,
      MESH_HUD_BAG_ICON_GAME,
      MESH_GAME_HEALTH_EFFECT,
      MESH_GAME_PAIN_EFFECT,
      MESH_BG_FADE,
      MESH_FADE,
      MESH_TICKER_BAR,
      MESH_PAUSE_MENU_FADE,
      MESH_MENU_RIBBON_1_SELECT_BUTTON,
      MESH_MENU_RIBBON_2_SELECT_BUTTON,
      MESH_MENU_RIBBON_3_SELECT_BUTTON,
      MESH_MENU_RIBBON_4_SELECT_BUTTON,
      MESH_MENU_BUTTON_MG,
      MESH_MENU_TITLE,
      MESH_MENU_RIBBON_1_RED_LEFT,
      MESH_MENU_RIBBON_1_RED_CENTER,
      MESH_MENU_RIBBON_1_RED_RIGHT,
      MESH_MENU_RIBBON_2_RED_LEFT,
      MESH_MENU_RIBBON_2_RED_CENTER,
      MESH_MENU_RIBBON_2_RED_RIGHT,
      MESH_MENU_RIBBON_3_RED_LEFT,
      MESH_MENU_RIBBON_3_RED_CENTER,
      MESH_MENU_RIBBON_3_RED_RIGHT,
      MESH_MENU_RIBBON_4_RED_LEFT,
      MESH_MENU_RIBBON_4_RED_CENTER,
      MESH_MENU_RIBBON_4_RED_RIGHT,
      MESH_MENU_RIBBON_1_WHITE_LEFT,
      MESH_MENU_RIBBON_1_WHITE_CENTER,
      MESH_MENU_RIBBON_1_WHITE_RIGHT,
      MESH_MENU_RIBBON_2_WHITE_LEFT,
      MESH_MENU_RIBBON_2_WHITE_CENTER,
      MESH_MENU_RIBBON_2_WHITE_RIGHT,
      MESH_MENU_RIBBON_3_WHITE_LEFT,
      MESH_MENU_RIBBON_3_WHITE_CENTER,
      MESH_MENU_RIBBON_3_WHITE_RIGHT,
      MESH_MENU_RIBBON_4_WHITE_LEFT,
      MESH_MENU_RIBBON_4_WHITE_CENTER,
      MESH_MENU_RIBBON_4_WHITE_RIGHT,
      MESH_BUTTON_EFFECT,
      MESH_NETWORK_WAIT_EFFECT,
      MESH_SLIDER_BUTTON,
      MESH_SLIDER_ICON_OFF,
      MESH_SLIDER_ICON_ON,
      MESH_LEVEL_COMPLETE_BAG_ICON,
      MESH_ARROW_LEFT_ACTIVE,
      MESH_ARROW_RIGHT_ACTIVE,
      MESH_ARROW_LEFT_DISABLED,
      MESH_ARROW_RIGHT_DISABLED,
      MESH_WINDOW_SCROLLBAR_BACKING,
      MESH_WINDOW_SCROLLBAR_VISIBLE,
      MESH_WINDOW_BUTTON_MAJOR,
      MESH_WINDOW_BUTTON_MINOR,
      MESH_WINDOW_BACKING_LEFTTOP,
      MESH_WINDOW_BACKING_TOP,
      MESH_WINDOW_BACKING_RIGHTTOP,
      MESH_WINDOW_BACKING_LEFT,
      MESH_WINDOW_BACKING_FILL,
      MESH_WINDOW_BACKING_RIGHT,
      MESH_WINDOW_BACKING_LEFTBOTTOM,
      MESH_WINDOW_BACKING_BOTTOM,
      MESH_WINDOW_BACKING_RIGHTBOTTOM,
      MESH_BADGES_BADGE_DISABLED,
      MESH_BADGES_BADGE_ENABLED,
      MESH_SPEEDRUN_STARS_TOTAL,
      MESH_SPEEDRUN_STARS_1_1,
      MESH_SPEEDRUN_STARS_1_2,
      MESH_SPEEDRUN_STARS_1_3,
      MESH_SPEEDRUN_STARS_2_1,
      MESH_SPEEDRUN_STARS_2_2,
      MESH_SPEEDRUN_STARS_2_3,
      MESH_SPEEDRUN_STARS_3_1,
      MESH_SPEEDRUN_STARS_3_2,
      MESH_SPEEDRUN_STARS_3_3,
      MESH_SPEEDRUN_STARS_HUD_1,
      MESH_SPEEDRUN_STARS_HUD_2,
      MESH_SPEEDRUN_STARS_HUD_3,
      MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_1,
      MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_2,
      MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_3,
    }

    public enum Mesh_Trial
    {
      MESH_LEVEL_COMPLETE_BG_0_0,
      MESH_LEVEL_COMPLETE_BG_1_1,
      MESH_EA_LOGO,
      MESH_MENU_INTRO_TITLE,
      MESH_MENU_LEVEL_SELECT_ARROW_LEFT_ACTIVE,
      MESH_MENU_LEVEL_SELECT_ARROW_RIGHT_ACTIVE,
      MESH_MENU_LEVEL_SELECT_ARROW_LEFT_DISABLED,
      MESH_MENU_LEVEL_SELECT_ARROW_RIGHT_DISABLED,
      MESH_MENU_LEVEL_SELECT_BUTTON_POSITIVE,
      MESH_MENU_LEVEL_SELECT_BUTTON_NEGATIVE,
      MESH_MENU_INTRO_GAME_BG,
      MESH_MENU_INTRO_GAME_REVEAL_1,
      MESH_MENU_INTRO_GAME_REVEAL_2,
      MESH_MENU_INTRO_GAME_REVEAL_3,
      MESH_MENU_UPSELL_MENU,
      MESH_MENU_UPSELL_BUY,
      MESH_UPSELL_1_BACK,
      MESH_UPSELL_1_FRONT,
      MESH_UPSELL_2_BACK,
      MESH_UPSELL_2_FRONT,
      MESH_UPSELL_3_BACK,
      MESH_UPSELL_3_FRONT,
      MESH_UPSELL_4_BACK,
      MESH_UPSELL_4_FRONT,
      MESH_UPSELL_5_BACK,
      MESH_UPSELL_5_FRONT,
      MESH_UPSELL_1_REVEAL_1,
      MESH_UPSELL_1_REVEAL_2,
      MESH_UPSELL_2_REVEAL_1,
      MESH_UPSELL_2_REVEAL_2,
      MESH_UPSELL_3_REVEAL_1,
      MESH_UPSELL_3_REVEAL_2,
      MESH_UPSELL_4_REVEAL_1,
      MESH_UPSELL_4_REVEAL_2,
      MESH_UPSELL_5_REVEAL_1,
      MESH_UPSELL_5_REVEAL_2,
      MESH_HUD_BAG_ICON_MENU,
      MESH_HUD_BAG_ICON_GAME,
      MESH_GAME_HEALTH_EFFECT,
      MESH_GAME_PAIN_EFFECT,
      MESH_BG_FADE,
      MESH_FADE,
      MESH_TICKER_BAR,
      MESH_PAUSE_MENU_FADE,
      MESH_MENU_RIBBON_1_SELECT_BUTTON,
      MESH_MENU_RIBBON_2_SELECT_BUTTON,
      MESH_MENU_RIBBON_3_SELECT_BUTTON,
      MESH_MENU_RIBBON_4_SELECT_BUTTON,
      MESH_MENU_BUTTON_MG,
      MESH_MENU_BUTTON_UPSELL,
      MESH_MENU_TITLE,
      MESH_MENU_RIBBON_1_RED_LEFT,
      MESH_MENU_RIBBON_1_RED_CENTER,
      MESH_MENU_RIBBON_1_RED_RIGHT,
      MESH_MENU_RIBBON_2_RED_LEFT,
      MESH_MENU_RIBBON_2_RED_CENTER,
      MESH_MENU_RIBBON_2_RED_RIGHT,
      MESH_MENU_RIBBON_3_RED_LEFT,
      MESH_MENU_RIBBON_3_RED_CENTER,
      MESH_MENU_RIBBON_3_RED_RIGHT,
      MESH_MENU_RIBBON_4_RED_LEFT,
      MESH_MENU_RIBBON_4_RED_CENTER,
      MESH_MENU_RIBBON_4_RED_RIGHT,
      MESH_MENU_RIBBON_1_WHITE_LEFT,
      MESH_MENU_RIBBON_1_WHITE_CENTER,
      MESH_MENU_RIBBON_1_WHITE_RIGHT,
      MESH_MENU_RIBBON_2_WHITE_LEFT,
      MESH_MENU_RIBBON_2_WHITE_CENTER,
      MESH_MENU_RIBBON_2_WHITE_RIGHT,
      MESH_MENU_RIBBON_3_WHITE_LEFT,
      MESH_MENU_RIBBON_3_WHITE_CENTER,
      MESH_MENU_RIBBON_3_WHITE_RIGHT,
      MESH_MENU_RIBBON_4_WHITE_LEFT,
      MESH_MENU_RIBBON_4_WHITE_CENTER,
      MESH_MENU_RIBBON_4_WHITE_RIGHT,
      MESH_BUTTON_EFFECT,
      MESH_SLIDER_BUTTON,
      MESH_SLIDER_ICON_OFF,
      MESH_SLIDER_ICON_ON,
      MESH_LEVEL_COMPLETE_BAG_ICON,
      MESH_ARROW_LEFT_ACTIVE,
      MESH_ARROW_RIGHT_ACTIVE,
      MESH_ARROW_LEFT_DISABLED,
      MESH_ARROW_RIGHT_DISABLED,
      MESH_WINDOW_SCROLLBAR_BACKING,
      MESH_WINDOW_SCROLLBAR_VISIBLE,
      MESH_WINDOW_BUTTON_MAJOR,
      MESH_WINDOW_BUTTON_MINOR,
      MESH_WINDOW_BACKING_LEFTTOP,
      MESH_WINDOW_BACKING_TOP,
      MESH_WINDOW_BACKING_RIGHTTOP,
      MESH_WINDOW_BACKING_LEFT,
      MESH_WINDOW_BACKING_FILL,
      MESH_WINDOW_BACKING_RIGHT,
      MESH_WINDOW_BACKING_LEFTBOTTOM,
      MESH_WINDOW_BACKING_BOTTOM,
      MESH_WINDOW_BACKING_RIGHTBOTTOM,
      MESH_SPEEDRUN_STARS_TOTAL,
      MESH_SPEEDRUN_STARS_1_1,
      MESH_SPEEDRUN_STARS_1_2,
      MESH_SPEEDRUN_STARS_1_3,
      MESH_SPEEDRUN_STARS_2_1,
      MESH_SPEEDRUN_STARS_2_2,
      MESH_SPEEDRUN_STARS_2_3,
      MESH_SPEEDRUN_STARS_3_1,
      MESH_SPEEDRUN_STARS_3_2,
      MESH_SPEEDRUN_STARS_3_3,
      MESH_SPEEDRUN_STARS_HUD_1,
      MESH_SPEEDRUN_STARS_HUD_2,
      MESH_SPEEDRUN_STARS_HUD_3,
      MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_1,
      MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_2,
      MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_3,
    }

    public enum Anim_Full
    {
      ANIM_MENU_INTRO_GAME,
      ANIM_GAME_PAIN_EFFECT,
      ANIM_BG_FADE,
      ANIM_FADE,
      ANIM_MENU_RIBBON_1_WIDTH,
      ANIM_MENU_RIBBON_2_WIDTH,
      ANIM_MENU_RIBBON_3_WIDTH,
      ANIM_MENU_RIBBON_4_WIDTH,
      ANIM_BUTTON_EFFECT,
      ANIM_NETWORK_WAIT_EFFECT,
      ANIM_WINDOW_BACKING_WIDTH,
      ANIM_WINDOW_BACKING_HEIGHT,
      ANIM_SPEEDRUN_STARS_HUD,
    }

    public enum Anim_Trial
    {
      ANIM_MENU_INTRO_GAME,
      ANIM_MENU_UPSELL_TRANS_SCREENS,
      ANIM_MENU_UPSELL_TRANS_TEXT,
      ANIM_MENU_UPSELL_IDLE,
      ANIM_GAME_PAIN_EFFECT,
      ANIM_BG_FADE,
      ANIM_FADE,
      ANIM_MENU_RIBBON_1_WIDTH,
      ANIM_MENU_RIBBON_2_WIDTH,
      ANIM_MENU_RIBBON_3_WIDTH,
      ANIM_MENU_RIBBON_4_WIDTH,
      ANIM_BUTTON_EFFECT,
      ANIM_WINDOW_BACKING_WIDTH,
      ANIM_WINDOW_BACKING_HEIGHT,
      ANIM_SPEEDRUN_STARS_HUD,
    }
  }
}
