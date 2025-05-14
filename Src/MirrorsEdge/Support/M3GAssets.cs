
// Type: support.M3GAssets
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using microedition.m3g;
using midp;
using GameManager;
using System;

#nullable disable
namespace support
{
  public class M3GAssets
  {
    public const int CM_FLAGS_DEPTH_TEST = 1;
    public const int CM_FLAGS_DEPTH_WRITE = 2;
    public const int CM_FLAGS_COLOR_WRITE = 4;
    public const int CM_FLAGS_ALPHA_WRITE = 8;
    public const int LOAD_FLAG_APP_ENGINE = 1;
    public const int LOAD_FLAG_SCENE_MENU = 2;
    public const int LOAD_FLAG_SCENE_GAME = 4;
    public const int LOAD_FLAG_SCENE_GAME_TEMP = 8;
    public const int LOAD_FLAG_LOADING_SCREEN = 16;
    public const int POLYGON_MODE_NONE = 0;
    public const int POLYGON_MODE_BACK = 1;
    public const int COMP_MODE_REPLACE = 0;
    public const int COMP_MODE_ALPHA = 1;
    public const int COMP_MODE_ALPHA_THRESHOLD = 2;
    public const int COMP_MODE_ALPHA_ADD = 3;
    public const int COMP_MODE_REPLACE_NO_DEPTH = 4;
    public const int COMP_MODE_ALPHA_NO_DEPTH = 5;
    public const int MATERIAL_REPLACE = 0;
    public const int MATERIAL_REPLACE_NOCULL = 1;
    public const int MATERIAL_ALPHA = 2;
    public const int MATERIAL_ALPHA_THRESHOLD = 3;
    public const int MATERIAL_ALPHA_ADD = 4;
    public const int MATERIAL_REPLACE_SKYDOME = 5;
    public const int MATERIAL_ALPHA_MAP_TERTIARY = 6;
    public const int MATERIAL_REFLECTION_EFFECT = 7;
    public const int MATERIAL_GHOST_ALPHA = 8;
    public const int MATERIAL_PARTICLE_ALPHA_ADD = 9;
    public const int MATERIAL_PARTICLE_ALPHA = 10;
    public const int MODEL_NULL = -1;
    public const int CM_DEPTH_TEST = 1;
    public const int CM_DEPTH_WRITE = 2;
    public const int CM_COLOR_WRITE = 4;
    public const int CM_ALPHA_WRITE = 8;
    private static Type Image = (Type) null;
    private static Type Tex = (Type) null;
    private static Type TexGroup = (Type) null;
    private static Type Node = (Type) null;
    private static Type Model = (Type) null;
    public static short[] RES_ID_ARRAY;
    public static readonly short[] RES_ID_ARRAY_FULL = new short[97]
    {
      (short) -1,
      (short) 176,
      (short) 175,
      (short) 101,
      (short) 103,
      (short) 104,
      (short) 301,
      (short) 300,
      (short) 254,
      (short) byte.MaxValue,
      (short) 135,
      (short) 134,
      (short) 136,
      (short) 137,
      (short) 4,
      (short) 5,
      (short) 1,
      (short) 22,
      (short) 0,
      (short) 3,
      (short) 2,
      (short) 20,
      (short) 21,
      (short) 101,
      (short) 103,
      (short) 105,
      (short) 104,
      (short) 49,
      (short) 45,
      (short) 50,
      (short) 46,
      (short) 48,
      (short) 51,
      (short) 47,
      (short) 42,
      (short) 90,
      (short) 24,
      (short) 86,
      (short) 85,
      (short) 43,
      (short) 25,
      (short) 65,
      (short) 57,
      (short) 60,
      (short) 71,
      (short) 72,
      (short) 56,
      (short) 59,
      (short) 69,
      (short) 70,
      (short) 58,
      (short) 61,
      (short) 73,
      (short) 74,
      (short) 62,
      (short) 63,
      (short) 64,
      (short) 66,
      (short) 67,
      (short) 68,
      (short) 75,
      (short) 76,
      (short) 77,
      (short) 54,
      (short) 55,
      (short) 83,
      (short) 52,
      (short) 84,
      (short) 20,
      (short) 24,
      (short) 23,
      (short) 25,
      (short) 27,
      (short) 31,
      (short) 28,
      (short) 40,
      (short) 33,
      (short) 34,
      (short) 35,
      (short) 41,
      (short) 32,
      (short) 42,
      (short) 43,
      (short) 37,
      (short) 38,
      (short) 30,
      (short) 26,
      (short) 39,
      (short) 25,
      (short) 27,
      (short) 33,
      (short) 30,
      (short) 31,
      (short) 34,
      (short) 26,
      (short) 28,
      (short) 44
    };
    public static readonly short[] RES_ID_ARRAY_TRIAL = new short[53]
    {
      (short) -1,
      (short) 176,
      (short) 175,
      (short) 101,
      (short) 254,
      (short) byte.MaxValue,
      (short) 61,
      (short) 60,
      (short) 62,
      (short) 63,
      (short) 4,
      (short) 5,
      (short) 1,
      (short) 0,
      (short) 3,
      (short) 2,
      (short) 8,
      (short) 9,
      (short) 43,
      (short) 45,
      (short) 47,
      (short) 46,
      (short) 19,
      (short) 18,
      (short) 15,
      (short) 16,
      (short) 10,
      (short) 24,
      (short) 25,
      (short) 26,
      (short) 27,
      (short) 21,
      (short) 22,
      (short) 31,
      (short) 20,
      (short) 32,
      (short) 20,
      (short) 24,
      (short) 23,
      (short) 25,
      (short) 27,
      (short) 31,
      (short) 28,
      (short) 40,
      (short) 33,
      (short) 34,
      (short) 35,
      (short) 41,
      (short) 32,
      (short) 42,
      (short) 43,
      (short) 37,
      (short) 38
    };
    public static readonly short[] CULLING_TYPE_ARRAY = new short[2]
    {
      (short) 162,
      (short) 160
    };
    public static readonly short[] TEXTURE_FILTER_ARRAY = new short[2]
    {
      (short) 209,
      (short) 210
    };
    public static readonly short[] TEXTURE_BLENDING_ARRAY = new short[2]
    {
      (short) 228,
      (short) 227
    };
    public static readonly short[] BLENDING_ARRAY = new short[3]
    {
      (short) 68,
      (short) 64,
      (short) 65
    };
    private PolygonMode[] m_polyModeArray;
    private CompositingMode[] m_compModeArray;
    private Appearance[] m_appearanceArray;
    private M3GAssets_Image[] m_imageArray;
    private M3GAssets_Texture[] m_textureArray;
    private M3GAssets_TexGroup[] m_texGroupArray;
    private M3GAssets_File[] m_fileArray;
    private M3GAssets_Node[] m_nodeArray;
    private M3GAssets_Model[] m_modelArray;

    public static void SetResources()
    {
      if (MirrorsEdge.TrialMode)
      {
        M3GAssets.RES_ID_ARRAY = M3GAssets.RES_ID_ARRAY_TRIAL;
        M3GAssets.Image = typeof (M3GAssets.Image_Trial);
        M3GAssets.Tex = typeof (M3GAssets.Tex_Trial);
        M3GAssets.TexGroup = typeof (M3GAssets.TexGroup_Trial);
        M3GAssets.Node = typeof (M3GAssets.Node_Trial);
        M3GAssets.Model = typeof (M3GAssets.Model_Trial);
      }
      else
      {
        M3GAssets.RES_ID_ARRAY = M3GAssets.RES_ID_ARRAY_FULL;
        M3GAssets.Image = typeof (M3GAssets.Image_Full);
        M3GAssets.Tex = typeof (M3GAssets.Tex_Full);
        M3GAssets.TexGroup = typeof (M3GAssets.TexGroup_Full);
        M3GAssets.Node = typeof (M3GAssets.Node_Full);
        M3GAssets.Model = typeof (M3GAssets.Model_Full);
      }
    }

    public static short get(string name)
    {
      if (name.StartsWith("IMAGE_"))
        return (short) (int) Enum.Parse(M3GAssets.Image, name, false);
      if (name.StartsWith("TEX_GROUP_"))
        return (short) (int) Enum.Parse(M3GAssets.TexGroup, name, false);
      if (name.StartsWith("TEX_"))
        return (short) (int) Enum.Parse(M3GAssets.Tex, name, false);
      if (name.StartsWith("NODE_"))
        return (short) (int) Enum.Parse(M3GAssets.Node, name, false);
      return name.StartsWith("MODEL_") ? (short) (int) Enum.Parse(M3GAssets.Model, name, false) : (short) 0;
    }

    public PolygonMode getPolygonMode(int index) => this.m_polyModeArray[index];

    public CompositingMode getCompositingMode(int index) => this.m_compModeArray[index];

    public Appearance getAppearance(int index) => this.m_appearanceArray[index];

    public M3GAssets_Image getImage(int index) => this.m_imageArray[index];

    public M3GAssets()
    {
      this.m_polyModeArray = (PolygonMode[]) null;
      this.m_compModeArray = (CompositingMode[]) null;
      this.m_appearanceArray = (Appearance[]) null;
      this.m_imageArray = (M3GAssets_Image[]) null;
      this.m_textureArray = (M3GAssets_Texture[]) null;
      this.m_texGroupArray = (M3GAssets_TexGroup[]) null;
      this.m_fileArray = (M3GAssets_File[]) null;
      this.m_nodeArray = (M3GAssets_Node[]) null;
      this.m_modelArray = (M3GAssets_Model[]) null;
    }

    public void Destructor()
    {
      for (int index = 0; index != this.m_modelArray.Length; ++index)
      {
        if (this.m_modelArray[index] != null)
          this.m_modelArray[index].Destructor();
        this.m_modelArray[index] = (M3GAssets_Model) null;
      }
      this.m_modelArray = (M3GAssets_Model[]) null;
      for (int index = 0; index != this.m_nodeArray.Length; ++index)
      {
        if (this.m_nodeArray[index] != null)
          this.m_nodeArray[index].Destructor();
        this.m_nodeArray[index] = (M3GAssets_Node) null;
      }
      this.m_nodeArray = (M3GAssets_Node[]) null;
      for (int index = 0; index != this.m_fileArray.Length; ++index)
      {
        if (this.m_fileArray[index] != null)
          this.m_fileArray[index].Destructor();
        this.m_fileArray[index] = (M3GAssets_File) null;
      }
      this.m_fileArray = (M3GAssets_File[]) null;
      for (int index = 0; index != this.m_texGroupArray.Length; ++index)
      {
        if (this.m_texGroupArray[index] != null)
          this.m_texGroupArray[index].Destructor();
        this.m_texGroupArray[index] = (M3GAssets_TexGroup) null;
      }
      this.m_texGroupArray = (M3GAssets_TexGroup[]) null;
      for (int index = 0; index != this.m_textureArray.Length; ++index)
      {
        if (this.m_textureArray[index] != null)
          this.m_textureArray[index].Destructor();
        this.m_textureArray[index] = (M3GAssets_Texture) null;
      }
      this.m_textureArray = (M3GAssets_Texture[]) null;
      for (int index = 0; index != this.m_imageArray.Length; ++index)
      {
        if (this.m_imageArray[index] != null)
          this.m_imageArray[index].Destructor();
        this.m_imageArray[index] = (M3GAssets_Image) null;
      }
      this.m_imageArray = (M3GAssets_Image[]) null;
      for (int index = 0; index != this.m_appearanceArray.Length; ++index)
        this.m_appearanceArray[index].Destructor();
      this.m_appearanceArray = (Appearance[]) null;
      for (int index = 0; index != this.m_compModeArray.Length; ++index)
        this.m_compModeArray[index].Destructor();
      this.m_compModeArray = (CompositingMode[]) null;
      for (int index = 0; index != this.m_polyModeArray.Length; ++index)
        this.m_polyModeArray[index].Destructor();
      this.m_polyModeArray = (PolygonMode[]) null;
    }

    public void loadData()
    {
      if (this.m_polyModeArray != null)
        return;
      float num = 1.52587891E-05f;
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_M3G_ASSETS_BIN")));
      int length1 = dataInputStream.readUnsignedShort();
      this.m_polyModeArray = new PolygonMode[length1];
      for (int index = 0; index != length1; ++index)
      {
        PolygonMode polygonMode = M3GAssets.createPolygonMode((int) M3GAssets.CULLING_TYPE_ARRAY[(int) dataInputStream.readShort()]);
        this.m_polyModeArray[index] = polygonMode;
      }
      int length2 = dataInputStream.readUnsignedShort();
      this.m_compModeArray = new CompositingMode[length2];
      for (int index = 0; index != length2; ++index)
      {
        CompositingMode compositingMode = M3GAssets.createCompositingMode(dataInputStream.readUnsignedByte(), (float) dataInputStream.readInt() * num, (int) M3GAssets.BLENDING_ARRAY[(int) dataInputStream.readShort()]);
        this.m_compModeArray[index] = compositingMode;
      }
      int length3 = dataInputStream.readUnsignedShort();
      this.m_appearanceArray = new Appearance[length3];
      for (int index = 0; index != length3; ++index)
      {
        PolygonMode polyMode = this.m_polyModeArray[(int) dataInputStream.readShort()];
        CompositingMode compMode = this.m_compModeArray[(int) dataInputStream.readShort()];
        int layer = (int) dataInputStream.readShort();
        Appearance appearanace = M3GAssets.createAppearanace(compMode, polyMode, layer);
        this.m_appearanceArray[index] = appearanace;
      }
      int length4 = dataInputStream.readUnsignedShort();
      this.m_imageArray = new M3GAssets_Image[length4];
      for (int index = 0; index != length4; ++index)
      {
        int resId = (int) M3GAssets.RES_ID_ARRAY[(int) dataInputStream.readShort()];
        this.m_imageArray[index] = new M3GAssets_Image(resId);
      }
      int length5 = dataInputStream.readUnsignedShort();
      this.m_textureArray = new M3GAssets_Texture[length5];
      for (int index = 0; index != length5; ++index)
      {
        Appearance appearance = this.m_appearanceArray[(int) dataInputStream.readShort()];
        M3GAssets_Image image = this.m_imageArray[(int) dataInputStream.readShort()];
        int textureFilter1 = (int) M3GAssets.TEXTURE_FILTER_ARRAY[(int) dataInputStream.readShort()];
        int textureFilter2 = (int) M3GAssets.TEXTURE_FILTER_ARRAY[(int) dataInputStream.readShort()];
        int textureBlending = (int) M3GAssets.TEXTURE_BLENDING_ARRAY[(int) dataInputStream.readShort()];
        this.m_textureArray[index] = new M3GAssets_Texture(appearance, image, textureFilter1, textureFilter2, textureBlending);
      }
      int length6 = dataInputStream.readUnsignedShort();
      this.m_texGroupArray = new M3GAssets_TexGroup[length6];
      for (int index1 = 0; index1 != length6; ++index1)
      {
        M3GAssets_Texture texture1 = this.m_textureArray[(int) dataInputStream.readShort()];
        int numElements = (int) dataInputStream.readByte();
        M3GAssets_TexGroup m3GassetsTexGroup = new M3GAssets_TexGroup(numElements, texture1);
        this.m_texGroupArray[index1] = m3GassetsTexGroup;
        for (int index2 = 0; index2 != numElements; ++index2)
        {
          M3GAssets_Texture texture2 = this.m_textureArray[(int) dataInputStream.readShort()];
          int resId = (int) M3GAssets.RES_ID_ARRAY[(int) dataInputStream.readShort()];
          m3GassetsTexGroup.addElement(resId, texture2);
        }
      }
      int length7 = dataInputStream.readUnsignedShort();
      this.m_fileArray = new M3GAssets_File[length7];
      for (int index = 0; index != length7; ++index)
      {
        int resId = (int) M3GAssets.RES_ID_ARRAY[(int) dataInputStream.readShort()];
        this.m_fileArray[index] = new M3GAssets_File(resId);
      }
      int length8 = dataInputStream.readUnsignedShort();
      this.m_nodeArray = new M3GAssets_Node[length8];
      for (int index = 0; index != length8; ++index)
      {
        M3GAssets_File file = this.m_fileArray[(int) dataInputStream.readShort()];
        int resId = (int) M3GAssets.RES_ID_ARRAY[(int) dataInputStream.readShort()];
        bool allowDirectLoad = dataInputStream.readBoolean();
        this.m_nodeArray[index] = new M3GAssets_Node(file, resId, allowDirectLoad);
      }
      int length9 = dataInputStream.readUnsignedShort();
      this.m_modelArray = new M3GAssets_Model[length9];
      for (int index = 0; index != length9; ++index)
      {
        M3GAssets_Node node = this.m_nodeArray[(int) dataInputStream.readShort()];
        bool duplicate = dataInputStream.readBoolean();
        bool commit = dataInputStream.readBoolean();
        M3GAssets_Texture texture = (M3GAssets_Texture) null;
        if (dataInputStream.readByte() == (sbyte) 1)
          texture = this.m_textureArray[(int) dataInputStream.readShort()];
        M3GAssets_TexGroup texGroup = (M3GAssets_TexGroup) null;
        if (dataInputStream.readByte() == (sbyte) 1)
          texGroup = this.m_texGroupArray[(int) dataInputStream.readShort()];
        this.m_modelArray[index] = new M3GAssets_Model(node, texture, texGroup, duplicate, commit);
      }
      dataInputStream.close();
    }

    public Appearance getTexturedAppearance(int texIndex) => this.m_textureArray[texIndex].get();

    public Appearance loadTexturedAppearance(int texIndex, int loadFlags)
    {
      return this.m_textureArray[texIndex].loadCached(loadFlags);
    }

    public M3GAssets_AppGroup loadTextureGroup(int texGroupIndex, int loadFlags)
    {
      return this.m_texGroupArray[texGroupIndex].loadCached(loadFlags);
    }

    public microedition.m3g.Node loadNode(int nodeIndex)
    {
      return this.m_nodeArray[nodeIndex].loadUnique();
    }

    public microedition.m3g.Node loadNodeCached(int nodeIndex, int loadFlags)
    {
      return this.m_nodeArray[nodeIndex].loadUniqueCached(loadFlags);
    }

    public microedition.m3g.Node loadModel(int modelIndex, int loadFlags)
    {
      return this.m_modelArray[modelIndex].loadCached(loadFlags);
    }

    public void freeCaches(int loadFlags)
    {
      for (int index = this.m_modelArray.Length - 1; index != -1; --index)
        this.m_modelArray[index].freeCached(loadFlags);
      for (int index = this.m_nodeArray.Length - 1; index != -1; --index)
        this.m_nodeArray[index].freeCached(loadFlags);
      for (int index = this.m_texGroupArray.Length - 1; index != -1; --index)
        this.m_texGroupArray[index].freeCached(loadFlags);
      for (int index = this.m_textureArray.Length - 1; index != -1; --index)
        this.m_textureArray[index].freeCached(loadFlags);
    }

    public static PolygonMode createPolygonMode(int cullType)
    {
      PolygonMode polygonMode = new PolygonMode();
      polygonMode.setCulling(cullType);
      return polygonMode;
    }

    public static CompositingMode createCompositingMode(
      int flags,
      float alphaThreshold,
      int blending)
    {
      CompositingMode compositingMode = new CompositingMode();
      compositingMode.setDepthTestEnable((flags & 1) != 0);
      compositingMode.setDepthWriteEnable((flags & 2) != 0);
      compositingMode.setColorWriteEnable((flags & 4) != 0);
      compositingMode.setAlphaWriteEnable((flags & 8) != 0);
      compositingMode.setAlphaThreshold(alphaThreshold);
      compositingMode.setBlending(blending);
      return compositingMode;
    }

    public static Appearance createAppearanace(
      CompositingMode compMode,
      PolygonMode polyMode,
      int layer)
    {
      Appearance appearanace = new Appearance();
      appearanace.setCompositingMode(compMode);
      appearanace.setLayer(layer);
      appearanace.setPolygonMode(polyMode);
      return appearanace;
    }

    public static Texture2D createTexture2D(
      Image2D image,
      int levelFilter,
      int imageFilter,
      int blending)
    {
      Texture2D texture2D = new Texture2D(image);
      texture2D.setFiltering(levelFilter, imageFilter);
      texture2D.setBlending(blending);
      return texture2D;
    }

    public static Appearance createTexturedAppearance(
      Appearance app,
      Image2D image,
      int levelFilter,
      int imageFilter,
      int blending)
    {
      Appearance texturedAppearance = (Appearance) app.duplicate();
      Texture2D texture2D = M3GAssets.createTexture2D(image, levelFilter, imageFilter, blending);
      if ((image.getFormat() & 32768) != 0)
        texture2D.setFiltering(208, imageFilter);
      texturedAppearance.setTexture(0, texture2D);
      return texturedAppearance;
    }

    public static void applyAppearance(microedition.m3g.Node node, Appearance app)
    {
      switch (node)
      {
        case Mesh mesh:
          int submeshCount = mesh.getSubmeshCount();
          for (int index = 0; index < submeshCount; ++index)
            mesh.setAppearance(index, app);
          SkinnedMesh skinnedMesh = SkinnedMesh.m3g_cast((Object3D) node);
          if (skinnedMesh == null)
            break;
          M3GAssets.applyAppearance((microedition.m3g.Node) skinnedMesh.getSkeleton(), app);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.applyAppearance(group.getChild(index), app);
          break;
      }
    }

    public static void applyAppearanceGroup(microedition.m3g.Node node, M3GAssets_AppGroup appGroup)
    {
      switch (node)
      {
        case Mesh mesh:
          int submeshCount = mesh.getSubmeshCount();
          for (int index = 0; index < submeshCount; ++index)
          {
            int userId = mesh.getAppearance(index).getUserID();
            mesh.setAppearance(index, appGroup.getAppearance(userId));
          }
          SkinnedMesh skinnedMesh = SkinnedMesh.m3g_cast((Object3D) mesh);
          if (skinnedMesh == null)
            break;
          M3GAssets.applyAppearanceGroup((microedition.m3g.Node) skinnedMesh.getSkeleton(), appGroup);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.applyAppearanceGroup(group.getChild(index), appGroup);
          break;
      }
    }

    public static void applyScope(microedition.m3g.Node node, int newScope)
    {
      switch (node)
      {
        case Mesh mesh:
          mesh.setScope(newScope);
          if (!(node is SkinnedMesh skinnedMesh))
            break;
          M3GAssets.applyScope((microedition.m3g.Node) skinnedMesh.getSkeleton(), newScope);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.applyScope(group.getChild(index), newScope);
          break;
      }
    }

    public static void applyColor(microedition.m3g.Node node, uint color)
    {
      switch (node)
      {
        case Mesh mesh:
          mesh.getVertexBuffer().setDefaultColor(color);
          if (!(node is SkinnedMesh skinnedMesh))
            break;
          M3GAssets.applyColor((microedition.m3g.Node) skinnedMesh.getSkeleton(), color);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.applyColor(group.getChild(index), color);
          break;
      }
    }

    public static void applyAlphaFactor(microedition.m3g.Node node, float alphaFactor)
    {
      switch (node)
      {
        case Mesh mesh:
          mesh.setAlphaFactor(alphaFactor);
          if (!(node is SkinnedMesh skinnedMesh))
            break;
          M3GAssets.applyAlphaFactor((microedition.m3g.Node) skinnedMesh.getSkeleton(), alphaFactor);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.applyAlphaFactor(group.getChild(index), alphaFactor);
          break;
      }
    }

    public static void applyUniqueVertexBuffer(microedition.m3g.Node node)
    {
      VertexBuffer[] oldBuffer = (VertexBuffer[]) null;
      VertexBuffer[] newBuffer = (VertexBuffer[]) null;
      M3GAssets.applyUniqueVertexBuffer(node, oldBuffer, newBuffer);
    }

    private static void applyUniqueVertexBuffer(
      microedition.m3g.Node node,
      VertexBuffer[] oldBuffer,
      VertexBuffer[] newBuffer)
    {
      switch (node)
      {
        case Mesh mesh when oldBuffer != null && newBuffer != null:
          VertexBuffer vertexBuffer1 = mesh.getVertexBuffer();
          VertexBuffer vertexBuffer2;
          if (vertexBuffer1 == oldBuffer[0])
          {
            vertexBuffer2 = newBuffer[0];
          }
          else
          {
            oldBuffer[0] = vertexBuffer1;
            vertexBuffer2 = (VertexBuffer) vertexBuffer1.duplicate();
            newBuffer[0] = vertexBuffer2;
          }
          mesh.setVertexBuffer(vertexBuffer2);
          if (!(node is SkinnedMesh skinnedMesh))
            break;
          M3GAssets.applyUniqueVertexBuffer((microedition.m3g.Node) skinnedMesh.getSkeleton(), oldBuffer, newBuffer);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.applyUniqueVertexBuffer(group.getChild(index), oldBuffer, newBuffer);
          break;
      }
    }

    public static void commit(microedition.m3g.Node node)
    {
      switch (node)
      {
        case Mesh mesh:
          mesh.commit();
          if (!(node is SkinnedMesh skinnedMesh))
            break;
          M3GAssets.commit((microedition.m3g.Node) skinnedMesh.getSkeleton());
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.commit(group.getChild(index));
          break;
      }
    }

    public static void orphanNode(microedition.m3g.Node node)
    {
      if (node == null)
        return;
      microedition.m3g.Node parent = node.getParent();
      if (parent == null || !(parent is Group group))
        return;
      group.removeChild(node);
    }

    public static void addNode(Group parent, microedition.m3g.Node child)
    {
      M3GAssets.orphanNode(child);
      parent.addChild(child);
    }

    public static void removeNode(Group parent, microedition.m3g.Node child)
    {
      parent.removeChild(child);
    }

    public static void cacheSkinTransforms(microedition.m3g.Node node)
    {
      switch (node)
      {
        case Mesh _:
          if (!(node is SkinnedMesh object_))
            break;
          Loader.SkinnedMesh_warmCache(object_);
          break;
        case Group group:
          int childCount = group.getChildCount();
          for (int index = 0; index < childCount; ++index)
            M3GAssets.cacheSkinTransforms(group.getChild(index));
          break;
      }
    }

    public enum Image_Full
    {
      IMAGE_CITYSCAPE,
      IMAGE_LOADING_BORDER_ORANGE,
      IMAGE_LOADING_BORDER_RED,
      IMAGE_LOADING_BORDER_BLUE,
      IMAGE_FAITH_EXTERNAL_MIDDAY,
      IMAGE_FAITH_EXTERNAL_DUSK,
      IMAGE_FAITH_EXTERNAL_NIGHT,
      IMAGE_FAITH_INTERNAL_GEN,
      IMAGE_FAITH_INTERNAL_JAIL,
      IMAGE_FAITH_INTERNAL_UNDERGROUND,
      IMAGE_FAITH_GHOST,
      IMAGE_CHOPPER,
      IMAGE_RIVAL,
      IMAGE_RIVAL_BOSS,
      IMAGE_POLICE_LIGHT,
      IMAGE_POLICE_RIOT,
      IMAGE_EFFECT_COLLECT,
      IMAGE_EFFECT_BULLET,
      IMAGE_MAP_GRAFFITI_ALPHA,
      IMAGE_MAP_MIDDAY_BACKDROP,
      IMAGE_MAP_MIDDAY_BUILDING,
      IMAGE_MAP_MIDDAY_OBJECTS_1,
      IMAGE_MAP_MIDDAY_OBJECTS_2,
      IMAGE_MAP_DUSK_BACKDROP,
      IMAGE_MAP_DUSK_BUILDING,
      IMAGE_MAP_DUSK_OBJECTS_1,
      IMAGE_MAP_DUSK_OBJECTS_2,
      IMAGE_MAP_NIGHT_BACKDROP,
      IMAGE_MAP_NIGHT_BUILDING,
      IMAGE_MAP_NIGHT_OBJECTS_1,
      IMAGE_MAP_NIGHT_OBJECTS_2,
      IMAGE_MAP_GEN_INT_1,
      IMAGE_MAP_GEN_INT_2,
      IMAGE_MAP_GEN_INT_3,
      IMAGE_MAP_JAIL_1,
      IMAGE_MAP_JAIL_2,
      IMAGE_MAP_JAIL_3,
      IMAGE_MAP_UNDERGROUND_1,
      IMAGE_MAP_UNDERGROUND_2,
      IMAGE_MAP_UNDERGROUND_3,
      IMAGE_EFFECT_SMOKE,
      IMAGE_EFFECT_STEAMJET,
      IMAGE_PIGEON,
      IMAGE_EFFECT_PARTICLES,
      IMAGE_PLANE,
    }

    public enum Image_Trial
    {
      IMAGE_CITYSCAPE,
      IMAGE_LOADING_BORDER_ORANGE,
      IMAGE_LOADING_BORDER_RED,
      IMAGE_LOADING_BORDER_BLUE,
      IMAGE_FAITH_EXTERNAL_MIDDAY,
      IMAGE_FAITH_GHOST,
      IMAGE_CHOPPER,
      IMAGE_EFFECT_COLLECT,
      IMAGE_EFFECT_BULLET,
      IMAGE_MAP_MIDDAY_BACKDROP,
      IMAGE_MAP_MIDDAY_BUILDING,
      IMAGE_MAP_MIDDAY_OBJECTS_1,
      IMAGE_MAP_MIDDAY_OBJECTS_2,
      IMAGE_EFFECT_SMOKE,
      IMAGE_EFFECT_STEAMJET,
      IMAGE_PIGEON,
      IMAGE_EFFECT_PARTICLES,
      IMAGE_PLANE,
    }

    public enum Tex_Full
    {
      TEX_CITYSCAPE_REPLACE,
      TEX_CITYSCAPE_ALPHA,
      TEX_CITYSCAPE_ALPHA_THRESHOLD,
      TEX_LOADING_BORDER_ORANGE,
      TEX_LOADING_BORDER_RED,
      TEX_LOADING_BORDER_BLUE,
      TEX_LOADING_TEXT,
      TEX_FAITH_EXTERNAL_MIDDAY_REPLACE,
      TEX_FAITH_EXTERNAL_MIDDAY_ALPHA_THRESHOLD,
      TEX_FAITH_EXTERNAL_DUSK_REPLACE,
      TEX_FAITH_EXTERNAL_DUSK_ALPHA_THRESHOLD,
      TEX_FAITH_EXTERNAL_NIGHT_REPLACE,
      TEX_FAITH_EXTERNAL_NIGHT_ALPHA_THRESHOLD,
      TEX_FAITH_INTERNAL_GEN_REPLACE,
      TEX_FAITH_INTERNAL_GEN_ALPHA_THRESHOLD,
      TEX_FAITH_INTERNAL_JAIL_REPLACE,
      TEX_FAITH_INTERNAL_JAIL_ALPHA_THRESHOLD,
      TEX_FAITH_INTERNAL_UNDERGROUND_REPLACE,
      TEX_FAITH_INTERNAL_UNDERGROUND_ALPHA_THRESHOLD,
      TEX_FAITH_GHOST_ALPHA,
      TEX_CHOPPER_REPLACE,
      TEX_CHOPPER_ALPHA,
      TEX_CHOPPER_ALPHA_ADD,
      TEX_RIVAL,
      TEX_RIVAL_BOSS,
      TEX_POLICE_LIGHT,
      TEX_POLICE_RIOT,
      TEX_EFFECT_COLLECT_ALPHA_ADD,
      TEX_EFFECT_BULLET_ALPHA_ADD,
      TEX_MAP_GRAFFITI_ALPHA,
      TEX_MAP_MIDDAY_SKY_DOME_REPLACE,
      TEX_MAP_MIDDAY_TERTIARY_ALPHA,
      TEX_MAP_MIDDAY_SECONDARY_REPLACE,
      TEX_MAP_MIDDAY_SECONDARY_ALPHA,
      TEX_MAP_MIDDAY_BUILDING,
      TEX_MAP_MIDDAY_OBJECTS_1_REPLACE,
      TEX_MAP_MIDDAY_OBJECTS_1_ALPHA,
      TEX_MAP_MIDDAY_OBJECTS_1_ALPHA_THRESHOLD,
      TEX_MAP_MIDDAY_OBJECTS_1_ALPHA_ADD,
      TEX_MAP_MIDDAY_OBJECTS_2_REPLACE,
      TEX_MAP_MIDDAY_OBJECTS_2_ALPHA,
      TEX_MAP_MIDDAY_OBJECTS_2_ALPHA_THRESHOLD,
      TEX_MAP_MIDDAY_OBJECTS_2_ALPHA_ADD,
      TEX_MAP_DUSK_SKY_DOME_REPLACE,
      TEX_MAP_DUSK_TERTIARY_ALPHA,
      TEX_MAP_DUSK_SECONDARY_REPLACE,
      TEX_MAP_DUSK_SECONDARY_ALPHA,
      TEX_MAP_DUSK_BUILDING,
      TEX_MAP_DUSK_OBJECTS_1_REPLACE,
      TEX_MAP_DUSK_OBJECTS_1_ALPHA,
      TEX_MAP_DUSK_OBJECTS_1_ALPHA_THRESHOLD,
      TEX_MAP_DUSK_OBJECTS_1_ALPHA_ADD,
      TEX_MAP_DUSK_OBJECTS_2_REPLACE,
      TEX_MAP_DUSK_OBJECTS_2_ALPHA,
      TEX_MAP_DUSK_OBJECTS_2_ALPHA_THRESHOLD,
      TEX_MAP_DUSK_OBJECTS_2_ALPHA_ADD,
      TEX_MAP_NIGHT_SKY_DOME_REPLACE,
      TEX_MAP_NIGHT_TERTIARY_ALPHA,
      TEX_MAP_NIGHT_SECONDARY_REPLACE,
      TEX_MAP_NIGHT_SECONDARY_ALPHA,
      TEX_MAP_NIGHT_BUILDING_REPLACE,
      TEX_MAP_NIGHT_BUILDING_ALPHA,
      TEX_MAP_NIGHT_BUILDING_ALPHA_THRESHOLD,
      TEX_MAP_NIGHT_BUILDING_ALPHA_ADD,
      TEX_MAP_NIGHT_OBJECTS_1_REPLACE,
      TEX_MAP_NIGHT_OBJECTS_1_ALPHA,
      TEX_MAP_NIGHT_OBJECTS_1_ALPHA_THRESHOLD,
      TEX_MAP_NIGHT_OBJECTS_1_ALPHA_ADD,
      TEX_MAP_NIGHT_OBJECTS_2_REPLACE,
      TEX_MAP_NIGHT_OBJECTS_2_ALPHA,
      TEX_MAP_NIGHT_OBJECTS_2_ALPHA_THRESHOLD,
      TEX_MAP_NIGHT_OBJECTS_2_ALPHA_ADD,
      TEX_MAP_GEN_INT_1_REPLACE,
      TEX_MAP_GEN_INT_1_ALPHA,
      TEX_MAP_GEN_INT_1_ALPHA_THRESHOLD,
      TEX_MAP_GEN_INT_2_REPLACE,
      TEX_MAP_GEN_INT_2_ALPHA,
      TEX_MAP_GEN_INT_2_ALPHA_THRESHOLD,
      TEX_MAP_GEN_INT_3_REPLACE,
      TEX_MAP_GEN_INT_3_ALPHA,
      TEX_MAP_GEN_INT_3_ALPHA_THRESHOLD,
      TEX_MAP_JAIL_1_REPLACE,
      TEX_MAP_JAIL_1_ALPHA,
      TEX_MAP_JAIL_1_ALPHA_THRESHOLD,
      TEX_MAP_JAIL_2_REPLACE,
      TEX_MAP_JAIL_2_ALPHA,
      TEX_MAP_JAIL_2_ALPHA_THRESHOLD,
      TEX_MAP_JAIL_3_REPLACE,
      TEX_MAP_JAIL_3_ALPHA,
      TEX_MAP_JAIL_3_ALPHA_THRESHOLD,
      TEX_MAP_UNDERGROUND_1_REPLACE,
      TEX_MAP_UNDERGROUND_1_ALPHA,
      TEX_MAP_UNDERGROUND_1_ALPHA_THRESHOLD,
      TEX_MAP_UNDERGROUND_2_REPLACE,
      TEX_MAP_UNDERGROUND_2_ALPHA,
      TEX_MAP_UNDERGROUND_2_ALPHA_THRESHOLD,
      TEX_MAP_UNDERGROUND_3_REPLACE,
      TEX_MAP_UNDERGROUND_3_ALPHA,
      TEX_MAP_UNDERGROUND_3_ALPHA_THRESHOLD,
      TEX_EFFECT_SMOKE_ALPHA_ADD,
      TEX_EFFECT_STEAMJET_ALPHA_ADD,
      TEX_PIGEON,
      TEX_EFFECT_PARTICLES_ALPHA_ADD,
      TEX_EFFECT_PARTICLES_ALPHA,
      TEX_PLANE,
    }

    public enum Tex_Trial
    {
      TEX_CITYSCAPE_REPLACE,
      TEX_CITYSCAPE_ALPHA,
      TEX_CITYSCAPE_ALPHA_THRESHOLD,
      TEX_LOADING_BORDER_ORANGE,
      TEX_LOADING_BORDER_RED,
      TEX_LOADING_BORDER_BLUE,
      TEX_LOADING_TEXT,
      TEX_FAITH_EXTERNAL_MIDDAY_REPLACE,
      TEX_FAITH_EXTERNAL_MIDDAY_ALPHA_THRESHOLD,
      TEX_FAITH_GHOST_ALPHA,
      TEX_CHOPPER_REPLACE,
      TEX_CHOPPER_ALPHA,
      TEX_CHOPPER_ALPHA_ADD,
      TEX_EFFECT_COLLECT_ALPHA_ADD,
      TEX_EFFECT_BULLET_ALPHA_ADD,
      TEX_MAP_MIDDAY_SKY_DOME_REPLACE,
      TEX_MAP_MIDDAY_TERTIARY_ALPHA,
      TEX_MAP_MIDDAY_SECONDARY_REPLACE,
      TEX_MAP_MIDDAY_SECONDARY_ALPHA,
      TEX_MAP_MIDDAY_BUILDING,
      TEX_MAP_MIDDAY_OBJECTS_1_REPLACE,
      TEX_MAP_MIDDAY_OBJECTS_1_ALPHA,
      TEX_MAP_MIDDAY_OBJECTS_1_ALPHA_THRESHOLD,
      TEX_MAP_MIDDAY_OBJECTS_1_ALPHA_ADD,
      TEX_MAP_MIDDAY_OBJECTS_2_REPLACE,
      TEX_MAP_MIDDAY_OBJECTS_2_ALPHA,
      TEX_MAP_MIDDAY_OBJECTS_2_ALPHA_THRESHOLD,
      TEX_MAP_MIDDAY_OBJECTS_2_ALPHA_ADD,
      TEX_EFFECT_SMOKE_ALPHA_ADD,
      TEX_EFFECT_STEAMJET_ALPHA_ADD,
      TEX_PIGEON,
      TEX_EFFECT_PARTICLES_ALPHA_ADD,
      TEX_EFFECT_PARTICLES_ALPHA,
      TEX_PLANE,
    }

    public enum TexGroup_Full
    {
      TEX_GROUP_CITYSCAPE,
      TEX_GROUP_LOADING_ORANGE,
      TEX_GROUP_LOADING_RED,
      TEX_GROUP_LOADING_BLUE,
      TEX_GROUP_FAITH_EXTERNAL_MIDDAY,
      TEX_GROUP_FAITH_EXTERNAL_DUSK,
      TEX_GROUP_FAITH_EXTERNAL_NIGHT,
      TEX_GROUP_FAITH_INTERNAL_GEN,
      TEX_GROUP_FAITH_INTERNAL_JAIL,
      TEX_GROUP_FAITH_INTERNAL_UNDERGROUND,
      TEX_GROUP_CHOPPER,
      TEX_GROUP_MAP_MIDDAY,
      TEX_GROUP_MAP_DUSK,
      TEX_GROUP_MAP_NIGHT,
      TEX_GROUP_MAP_GEN_INT,
      TEX_GROUP_MAP_JAIL,
      TEX_GROUP_MAP_UNDERGROUND,
    }

    public enum TexGroup_Trial
    {
      TEX_GROUP_CITYSCAPE,
      TEX_GROUP_LOADING_ORANGE,
      TEX_GROUP_LOADING_RED,
      TEX_GROUP_LOADING_BLUE,
      TEX_GROUP_FAITH_EXTERNAL_MIDDAY,
      TEX_GROUP_CHOPPER,
      TEX_GROUP_MAP_MIDDAY,
    }

    public enum Node_Full
    {
      NODE_CITYSCAPE,
      NODE_CITYSCAPE_CAMERA_LOOK_FROM,
      NODE_CITYSCAPE_CAMERA_LOOK_AT,
      NODE_LOADING,
      NODE_LOADING_SHORT,
      NODE_FAITH,
      NODE_FAITH_ORIGIN,
      NODE_CHOPPER,
      NODE_RIVAL,
      NODE_RIVAL_BOSS,
      NODE_POLICE_LIGHT,
      NODE_POLICE_RIOT,
      NODE_CAMERA_LOOK_FROM,
      NODE_CAMERA_LOOK_AT,
      NODE_EFFECT_COLLECT_LARGE,
      NODE_EFFECT_COLLECT_SMALL,
      NODE_EFFECT_BULLET,
      NODE_PIGEON,
      NODE_PLANE,
    }

    public enum Node_Trial
    {
      NODE_CITYSCAPE,
      NODE_CITYSCAPE_CAMERA_LOOK_FROM,
      NODE_CITYSCAPE_CAMERA_LOOK_AT,
      NODE_LOADING,
      NODE_LOADING_SHORT,
      NODE_FAITH,
      NODE_FAITH_ORIGIN,
      NODE_CHOPPER,
      NODE_CAMERA_LOOK_FROM,
      NODE_CAMERA_LOOK_AT,
      NODE_EFFECT_COLLECT_LARGE,
      NODE_EFFECT_COLLECT_SMALL,
      NODE_EFFECT_BULLET,
      NODE_PIGEON,
      NODE_PLANE,
    }

    public enum Model_Full
    {
      MODEL_CITYSCAPE,
      MODEL_LOADING_ORANGE,
      MODEL_LOADING_RED,
      MODEL_LOADING_BLUE,
      MODEL_LOADING_ORANGE_SHORT,
      MODEL_LOADING_RED_SHORT,
      MODEL_LOADING_BLUE_SHORT,
      MODEL_FAITH_EXTERNAL_MIDDAY,
      MODEL_FAITH_EXTERNAL_DUSK,
      MODEL_FAITH_EXTERNAL_NIGHT,
      MODEL_FAITH_INTERNAL_GEN,
      MODEL_FAITH_INTERNAL_JAIL,
      MODEL_FAITH_INTERNAL_UNDERGROUND,
      MODEL_FAITH_GHOST,
      MODEL_CHOPPER,
      MODEL_RIVAL,
      MODEL_RIVAL_BOSS,
      MODEL_POLICE_LIGHT,
      MODEL_POLICE_RIOT,
      MODEL_EFFECT_COLLECT_LARGE,
      MODEL_EFFECT_COLLECT_SMALL,
      MODEL_EFFECT_BULLET,
      MODEL_PIGEON,
      MODEL_PLANE,
    }

    public enum Model_Trial
    {
      MODEL_CITYSCAPE,
      MODEL_LOADING_ORANGE,
      MODEL_LOADING_RED,
      MODEL_LOADING_BLUE,
      MODEL_LOADING_ORANGE_SHORT,
      MODEL_LOADING_RED_SHORT,
      MODEL_LOADING_BLUE_SHORT,
      MODEL_FAITH_EXTERNAL_MIDDAY,
      MODEL_FAITH_GHOST,
      MODEL_CHOPPER,
      MODEL_EFFECT_COLLECT_LARGE,
      MODEL_EFFECT_COLLECT_SMALL,
      MODEL_EFFECT_BULLET,
      MODEL_PIGEON,
      MODEL_PLANE,
    }
  }
}
