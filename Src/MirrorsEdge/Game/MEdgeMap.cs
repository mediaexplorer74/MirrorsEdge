
// Type: game.MEdgeMap
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using midp;
using GameManager;
using support;
using System;
using System.Collections.Generic;

#nullable disable
namespace game
{
  public class MEdgeMap
  {
    public const int ATTRIB_COLLISION = 0;
    public const int ATTRIB_WALLRUN = 1;
    public const int ATTRIB_BALANCE = 2;
    public const int ATTRIB_BOOST = 3;
    public const int ATTRIB_NUM = 4;
    public const int SLOPE_NONE = 0;
    public const int SLOPE_NEG_X = 1;
    public const int SLOPE_POS_X = 2;
    public const int RUNNER_VISION_TYPE_NONE = 0;
    public const int RUNNER_VISION_TYPE_NEG_X = 1;
    public const int RUNNER_VISION_TYPE_POS_X = 2;
    public const int RUNNER_VISION_TYPE_BOTH_X = 3;
    public const int THEMENAME_MIDDAY = 0;
    public const int THEMENAME_DUSK = 1;
    public const int THEMENAME_INTERIOR = 2;
    public const int THEMENAME_NIGHT = 3;
    public const int THEMENAME_JAIL = 4;
    public const int THEMENAME_UNDERGROUND = 5;
    public const int BOX_TYPE_POPUP = 0;
    public const int BOX_TYPE_CUTSCENE = 1;
    public const int CUTSCENE_TARGET_NONE = 0;
    public const int CUTSCENE_TARGET_FAITH = 1;
    public const int PATH_C00_H01 = 0;
    public const int PATH_C00_H02 = 1;
    public const int PATH_CHOPPER_TEST = 2;
    public const int PATH_C01_02_P01 = 3;
    public const int PATH_C01_02_P02 = 4;
    public const int PATH_C01_02_P03 = 5;
    public const int PATH_C01_02_P05 = 6;
    public const int PATH_C01_02_P06 = 7;
    public const int PATH_C01_02_P07 = 8;
    public const int PATH_C01_02_P08 = 9;
    public const int PATH_C01_02_P11 = 10;
    public const int PATH_C01_02_P12 = 11;
    public const int PATH_C01_02_P14 = 12;
    public const int PATH_C01_02_P15 = 13;
    public const int PATH_C01_02_P04 = 14;
    public const int PATH_C02_P01 = 15;
    public const int PATH_C02_P02 = 16;
    public const int PATH_C02_P03 = 17;
    public const int PATH_C02_P04 = 18;
    public const int PATH_C02_P05 = 19;
    public const int PATH_C02_P06 = 20;
    public const int PATH_C02_P07 = 21;
    public const int PATH_C02_P08 = 22;
    public const int PATH_C02_P09 = 23;
    public const int PATH_C02_P10 = 24;
    public const int PATH_C02_11 = 25;
    public const int PATH_C02_P12 = 26;
    public const int PATH_C02_R01 = 27;
    public const int PATH_C02_R02 = 28;
    public const int PATH_C02_02_P02 = 29;
    public const int PATH_C02_02_P03 = 30;
    public const int PATH_C02_02_P04 = 31;
    public const int PATH_C02_02_P05 = 32;
    public const int PATH_C02_02_P06 = 33;
    public const int PATH_C02_02_P07 = 34;
    public const int PATH_C02_02_P08 = 35;
    public const int PATH_C02_02_P09 = 36;
    public const int PATH_C02_02_P10 = 37;
    public const int PATH_C02_02_P11 = 38;
    public const int PATH_C02_02_P12 = 39;
    public const int PATH_C02_02_P13 = 40;
    public const int PATH_C02_02_R01 = 41;
    public const int PATH_C02_02_R02 = 42;
    public const int PATH_C02_02_R03 = 43;
    public const int PATH_C02_02_R04 = 44;
    public const int PATH_C02_02_P14 = 45;
    public const int PATH_C02_02_P15 = 46;
    public const int PATH_C03_P01 = 47;
    public const int PATH_C03_P02 = 48;
    public const int PATH_C03_P03 = 49;
    public const int PATH_C03_P04 = 50;
    public const int PATH_C03_P05 = 51;
    public const int PATH_C03_P06 = 52;
    public const int PATH_C03_P07 = 53;
    public const int PATH_C03_02_P01 = 54;
    public const int PATH_C03_02_P02 = 55;
    public const int PATH_C03_02_P03 = 56;
    public const int PATH_C03_02_P04 = 57;
    public const int PATH_C03_02_P05 = 58;
    public const int PATH_C03_02_P06 = 59;
    public const int PATH_C03_02_P07 = 60;
    public const int PATH_C03_02_R01 = 61;
    public const int PATH_C03_02_P08 = 62;
    public const int PATH_C03_02_P09 = 63;
    public const int PATH_C03_02_P10 = 64;
    public const int PATH_C03_02_P11 = 65;
    public const int PATH_C03_02_P12 = 66;
    public const int PATH_C03_02_P13 = 67;
    public const int PATH_C03_02_P14 = 68;
    public const int PATH_C03_02_P15 = 69;
    public const int PATH_C03_02_P16 = 70;
    public const int PATH_C03_02_P17 = 71;
    public const int PATH_C03_02_P18 = 72;
    public const int PATH_C03_02_P19 = 73;
    public const int PATH_C03_02_P20 = 74;
    public const int PATH_C03_02_P21 = 75;
    public const int PATH_C03_02_P22 = 76;
    public const int PATH_C03_02_P23 = 77;
    public const int PATH_C03_02_P24 = 78;
    public const int PATH_C03_02_P25 = 79;
    public const int PATH_C03_02_P26 = 80;
    public const int PATH_C03_02_P27 = 81;
    public const int PATH_C03_02_P28 = 82;
    public const int PATH_C03_02_29 = 83;
    public const int PATH_C03_02_P30 = 84;
    public const int PATH_C03_02_P31 = 85;
    public const int PATH_C03_02_P32 = 86;
    public const int PATH_C03_02_P33 = 87;
    public const int PATH_C03_02_P34 = 88;
    public const int PATH_C03_02_P35 = 89;
    public const int PATH_C03_02_P36 = 90;
    public const int PATH_C03_02_P37 = 91;
    public const int PATH_C03_02_P38 = 92;
    public const int PATH_C03_02_R02 = 93;
    public const int PATH_C03_02_R03 = 94;
    public const int PATH_C03_02_R04 = 95;
    public const int PATH_C04_H01 = 96;
    public const int PATH_C04_H02 = 97;
    public const int PATH_C05_R01 = 98;
    public const int PATH_C05_P01 = 99;
    public const int PATH_C05_R02 = 100;
    public const int PATH_C05_P02 = 101;
    public const int PATH_C05_R03 = 102;
    public const int PATH_C05_P03 = 103;
    public const int PATH_C05_P04 = 104;
    public const int PATH_C05_P05 = 105;
    public const int PATH_C05_P06 = 106;
    public const int PATH_C05_P07 = 107;
    public const int PATH_C05_P08 = 108;
    public const int PATH_C05_P09 = 109;
    public const int PATH_C05_P10 = 110;
    public const int PATH_C05_P11 = 111;
    public const int PATH_C05_P12 = 112;
    public const int PATH_C05_P13 = 113;
    public const int PATH_C05_P14 = 114;
    public const int PATH_C05_P15 = 115;
    public const int PATH_C05_P16 = 116;
    public const int PATH_C05_P17 = 117;
    public const int PATH_C05_P18 = 118;
    public const int PATH_C05_P19 = 119;
    public const int PATH_C05_P20 = 120;
    public const int PATH_C05_P21 = 121;
    public const int PATH_C05_P22 = 122;
    public const int PATH_C05_P23 = 123;
    public const int PATH_C05_02_P01 = 124;
    public const int PATH_C05_02_P02 = 125;
    public const int PATH_C05_02_P03 = 126;
    public const int PATH_C05_02_P04 = 127;
    public const int PATH_C05_02_P05 = 128;
    public const int PATH_C05_02_P06 = 129;
    public const int PATH_C06_P01 = 130;
    public const int PATH_C06_P02 = 131;
    public const int PATH_C06_P03 = 132;
    public const int PATH_C06_02_H01 = 133;
    public const int PATH_C06_02_H02 = 134;
    public const int PATH_C06_02_R01 = 135;
    public const int CUTSCENE_INTRO_TUTE_01 = 0;
    public const int CUTSCENE_INTRO_TUTE_02 = 0;
    public const int CUTSCENE_INTRO_01 = 0;
    public const int CUTSCENE_INTRO_01_02 = 0;
    public const int CUTSCENE_INTRO_02 = 0;
    public const int CUTSCENE_INTRO_02_02 = 0;
    public const int CUTSCENE_INTRO_03 = 0;
    public const int CUTSCENE_INTRO_03_02 = 0;
    public const int CUTSCENE_INTRO_04 = 0;
    public const int CUTSCENE_INTRO_04_02 = 0;
    public const int CUTSCENE_INTRO_05 = 0;
    public const int CUTSCENE_INTRO_05_02 = 0;
    public const int CUTSCENE_INTRO_06 = 0;
    public const int CUTSCENE_INTRO_06_02 = 0;
    public const int COLL_SIDE_NONE = 0;
    public const int COLL_SIDE_NEG_X = 1;
    public const int COLL_SIDE_POS_X = 2;
    public const int COLL_SIDE_MASK_X = 3;
    public const int COLL_SIDE_NEG_Y = 4;
    public const int COLL_SIDE_POS_Y = 8;
    public const int COLL_SIDE_MASK_Y = 12;
    public const int COLL_SIDE_NEG_Z = 16;
    public const int COLL_SIDE_POS_Z = 32;
    public const int COLL_SIDE_MASK_Z = 48;
    public const int COLL_DIAG_NEG_X_NEG_Y = 64;
    public const int COLL_DIAG_POS_X_NEG_Y = 128;
    public const int COLL_DIAG_NEG_Y = 192;
    public const int COLL_DIAG_NEG_X_POS_Y = 256;
    public const int COLL_DIAG_POS_X_POS_Y = 512;
    public const int COLL_DIAG_POS_Y = 768;
    public const int COLL_DIAG_ANY = 960;
    public static int[] LEVEL_DATA_LOOKUP_ARRAY;
    private static readonly int[] LEVEL_DATA_LOOKUP_ARRAY_FULL = new int[6]
    {
      0,
      1,
      3,
      2,
      4,
      5
    };
    private static readonly int[] LEVEL_DATA_LOOKUP_ARRAY_TRIAL = new int[1];
    public static int[] RES_LOOKUP_ARRAY;
    private static readonly int[] RES_LOOKUP_ARRAY_FULL = new int[30]
    {
      18,
      19,
      6,
      66,
      65,
      54,
      13,
      7,
      68,
      9,
      21,
      8,
      22,
      12,
      77,
      9,
      10,
      67,
      26,
      11,
      28,
      12,
      33,
      13,
      14,
      15,
      15,
      19,
      16,
      17
    };
    private static readonly int[] RES_LOOKUP_ARRAY_TRIAL = new int[6]
    {
      7,
      6,
      16,
      15,
      13,
      3
    };
    public static short[] MAP_OBJECT_IDS;
    private static readonly short[] MAP_OBJECT_IDS_FULL = new short[9]
    {
      (short) 21,
      (short) 6,
      (short) 17,
      (short) 18,
      (short) 2,
      (short) 5,
      (short) 7,
      (short) 4,
      (short) 3
    };
    private static readonly short[] MAP_OBJECT_IDS_TRIAL = new short[4]
    {
      (short) 21,
      (short) 6,
      (short) 17,
      (short) 18
    };
    public static short[] MAP_STRING_IDS;
    private static readonly short[] MAP_STRING_IDS_FULL = new short[40]
    {
      (short) 2118,
      (short) 2137,
      (short) 2115,
      (short) 2117,
      (short) 2126,
      (short) 2119,
      (short) 2125,
      (short) 2120,
      (short) 2148,
      (short) 2143,
      (short) 2134,
      (short) 2147,
      (short) 2146,
      (short) 2144,
      (short) 2135,
      (short) 2132,
      (short) 2124,
      (short) 2121,
      (short) 2123,
      (short) 2150,
      (short) 2128,
      (short) 2149,
      (short) 2127,
      (short) 2140,
      (short) 2130,
      (short) 2139,
      (short) 2153,
      (short) 2129,
      (short) 2136,
      (short) 2131,
      (short) 2154,
      (short) 2157,
      (short) 2155,
      (short) 2156,
      (short) 2220,
      (short) 2152,
      (short) 2151,
      (short) 2218,
      (short) 2238,
      (short) 2334
    };
    private static readonly short[] MAP_STRING_IDS_TRIAL = new short[13]
    {
      (short) 2118,
      (short) 2137,
      (short) 2115,
      (short) 2117,
      (short) 2126,
      (short) 2119,
      (short) 2125,
      (short) 2120,
      (short) 2148,
      (short) 2143,
      (short) 2134,
      (short) 2147,
      (short) 2146
    };
    public static short[] MAP_CUTSCENE_IDS;
    private static readonly short[] MAP_CUTSCENE_IDS_FULL = new short[14];
    private static readonly short[] MAP_CUTSCENE_IDS_TRIAL = new short[2];
    public static int[] MATERIAL_LOOKUP_ARRAY;
    private static readonly int[] MATERIAL_LOOKUP_ARRAY_FULL = new int[6]
    {
      0,
      2,
      3,
      4,
      1,
      5
    };
    private static readonly int[] MATERIAL_LOOKUP_ARRAY_TRIAL = new int[6]
    {
      0,
      2,
      3,
      5,
      4,
      1
    };
    private int m_currentUniqueObjectID;
    private int m_theme;
    private ModelSet m_modelSet;
    private float m_minX;
    private float m_maxX;
    private float m_sectionWidthX;
    private int m_numSections;
    private short[] m_attribStartOffset;
    private short[] m_attribEndOffset;
    private CollShape[] m_attribArray;
    private bool m_loadingMap;
    private static float COLLISION_BOX_MIN = -9f;
    private static float COLLISION_BOX_MAX = 1f;
    private List<GameObject> m_staticObjectArray;
    private List<GameObject> m_dynamicObjectArray;
    private List<GameObject> m_collectableObjectArray;
    private List<GameObject> m_collectableObjectArrayRestart;
    private List<GameObject> m_objectUpdateArray;
    private List<GameObject> m_playerCollObjectArray;
    private List<GameObject> m_everythingCollObjectArray;
    private List<GameObject> m_allCollObjectArray;
    private GameObjectPlayer m_playerObject;
    private GameObjectCheckpoint m_checkpointObject;
    private int m_numCollectedCollectables;
    private Group m_mapNode;
    private microedition.m3g.Node m_skyDomeNode;
    private Texture2D m_steam1ScrollingTexture;
    private Texture2D m_steam2ScrollingTexture;
    private float m_steam1ScrollProgress;
    private float m_steam2ScrollProgress;
    private ChunkManager m_chunkManager;
    private DataCameraAnim[] m_cutscenes;
    private Path[] m_paths;
    private FallBox[] m_fallBoxes;
    private PainBox[] m_painBoxes;

    public static void SetResources()
    {
      if (MirrorsEdge.TrialMode)
      {
        MEdgeMap.LEVEL_DATA_LOOKUP_ARRAY = MEdgeMap.LEVEL_DATA_LOOKUP_ARRAY_TRIAL;
        MEdgeMap.RES_LOOKUP_ARRAY = MEdgeMap.RES_LOOKUP_ARRAY_TRIAL;
        MEdgeMap.MAP_OBJECT_IDS = MEdgeMap.MAP_OBJECT_IDS_TRIAL;
        MEdgeMap.MAP_STRING_IDS = MEdgeMap.MAP_STRING_IDS_TRIAL;
        MEdgeMap.MAP_CUTSCENE_IDS = MEdgeMap.MAP_CUTSCENE_IDS_TRIAL;
        MEdgeMap.MATERIAL_LOOKUP_ARRAY = MEdgeMap.MATERIAL_LOOKUP_ARRAY_TRIAL;
      }
      else
      {
        MEdgeMap.LEVEL_DATA_LOOKUP_ARRAY = MEdgeMap.LEVEL_DATA_LOOKUP_ARRAY_FULL;
        MEdgeMap.RES_LOOKUP_ARRAY = MEdgeMap.RES_LOOKUP_ARRAY_FULL;
        MEdgeMap.MAP_OBJECT_IDS = MEdgeMap.MAP_OBJECT_IDS_FULL;
        MEdgeMap.MAP_STRING_IDS = MEdgeMap.MAP_STRING_IDS_FULL;
        MEdgeMap.MAP_CUTSCENE_IDS = MEdgeMap.MAP_CUTSCENE_IDS_FULL;
        MEdgeMap.MATERIAL_LOOKUP_ARRAY = MEdgeMap.MATERIAL_LOOKUP_ARRAY_FULL;
      }
    }

    public int getUniqueGameObjectID(GameObject gameObject)
    {
      return AppEngine.getLevelData().getCurrentLevelIndex() << 16 | gameObject.getID();
    }

    public int getTheme() => this.m_theme;

    public ModelSet getModelSet() => this.m_modelSet;

    public int getModelId(int setIndex) => this.m_modelSet.getModelId(setIndex);

    public MEdgeMap()
    {
      this.m_theme = -1;
      this.m_modelSet = (ModelSet) null;
      this.m_minX = 0.0f;
      this.m_maxX = 0.0f;
      this.m_sectionWidthX = 0.0f;
      this.m_numSections = 0;
      this.m_attribStartOffset = (short[]) null;
      this.m_attribEndOffset = (short[]) null;
      this.m_attribArray = (CollShape[]) null;
      this.m_loadingMap = false;
      this.m_staticObjectArray = new List<GameObject>();
      this.m_dynamicObjectArray = new List<GameObject>();
      this.m_collectableObjectArray = new List<GameObject>();
      this.m_collectableObjectArrayRestart = new List<GameObject>();
      this.m_objectUpdateArray = new List<GameObject>();
      this.m_playerCollObjectArray = new List<GameObject>();
      this.m_everythingCollObjectArray = new List<GameObject>();
      this.m_allCollObjectArray = new List<GameObject>();
      this.m_playerObject = (GameObjectPlayer) null;
      this.m_checkpointObject = (GameObjectCheckpoint) null;
      this.m_numCollectedCollectables = 0;
      this.m_mapNode = (Group) null;
      this.m_skyDomeNode = (microedition.m3g.Node) null;
      this.m_steam1ScrollingTexture = (Texture2D) null;
      this.m_steam2ScrollingTexture = (Texture2D) null;
      this.m_steam1ScrollProgress = 0.0f;
      this.m_steam2ScrollProgress = 0.0f;
      this.m_chunkManager = new ChunkManager();
      this.m_cutscenes = (DataCameraAnim[]) null;
      this.m_paths = (Path[]) null;
      this.m_fallBoxes = (FallBox[]) null;
      this.m_painBoxes = (PainBox[]) null;
      this.m_currentUniqueObjectID = 0;
    }

    public void Destructor()
    {
      int length1 = this.m_attribArray.Length;
      for (int index = 0; index != length1; ++index)
      {
        if (this.m_attribArray[index] != null)
        {
          this.m_attribArray[index].Destructor();
          this.m_attribArray[index] = (CollShape) null;
        }
      }
      int length2 = this.m_paths.Length;
      for (int index = 0; index < length2; ++index)
      {
        this.m_paths[index].Destructor();
        this.m_paths[index] = (Path) null;
      }
      int length3 = this.m_fallBoxes.Length;
      for (int index = 0; index < length3; ++index)
      {
        this.m_fallBoxes[index].Destructor();
        this.m_fallBoxes[index] = (FallBox) null;
      }
      int length4 = this.m_painBoxes.Length;
      for (int index = 0; index < length4; ++index)
      {
        this.m_painBoxes[index].Destructor();
        this.m_painBoxes[index] = (PainBox) null;
      }
      this.m_modelSet = (ModelSet) null;
      this.m_attribStartOffset = (short[]) null;
      this.m_attribEndOffset = (short[]) null;
      this.m_attribArray = (CollShape[]) null;
      this.m_loadingMap = false;
      this.clearObjectList(ref this.m_staticObjectArray, true);
      this.clearObjectList(ref this.m_dynamicObjectArray, true);
      this.m_collectableObjectArrayRestart.Clear();
      this.m_collectableObjectArrayRestart = (List<GameObject>) null;
      this.clearObjectList(ref this.m_collectableObjectArray, true);
      this.clearObjectList(ref this.m_objectUpdateArray, true);
      this.clearObjectList(ref this.m_playerCollObjectArray, true);
      this.clearObjectList(ref this.m_everythingCollObjectArray, true);
      this.clearObjectList(ref this.m_allCollObjectArray, true);
      this.m_playerObject = (GameObjectPlayer) null;
      this.m_checkpointObject = (GameObjectCheckpoint) null;
      this.m_mapNode = (Group) null;
      this.m_skyDomeNode = (microedition.m3g.Node) null;
      this.m_steam1ScrollingTexture = (Texture2D) null;
      this.m_steam2ScrollingTexture = (Texture2D) null;
      this.m_chunkManager.Destructor();
      this.m_cutscenes = (DataCameraAnim[]) null;
      this.m_paths = (Path[]) null;
      this.m_fallBoxes = (FallBox[]) null;
      this.m_painBoxes = (PainBox[]) null;
    }

    private void clearObjectList(ref List<GameObject> l, bool removeListItslef)
    {
      foreach (GameObject gameObject in l)
        gameObject?.Destructor();
      l.Clear();
      if (!removeListItslef)
        return;
      l = (List<GameObject>) null;
    }

    public void loadMap(int resId, World world)
    {
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      LevelData levelData = AppEngine.getLevelData();
      int currentLevelIndex = levelData.getCurrentLevelIndex();
      Level level = levelData.getLevel(currentLevelIndex);
      bool flag = !MirrorsEdge.TrialMode ? levelData.getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN || levelData.getGameMode() == LevelData.GameMode.GAME_MODE_CHALLENGE : levelData.getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN;
      this.m_loadingMap = true;
      this.m_mapNode = new Group();
      M3GAssets.addNode((Group) world, (microedition.m3g.Node) this.m_mapNode);
      DataInputStream dis = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile(resId));
      int resLookup = MEdgeMap.RES_LOOKUP_ARRAY[dis.readInt()];
      this.m_theme = (int) dis.readByte();
      this.m_modelSet = AppEngine.getLevelData().getModelSet(MEdgeMap.LEVEL_DATA_LOOKUP_ARRAY[dis.readInt()]);
      float posX = (float) dis.readInt() * 1.52587891E-05f;
      float posY = (float) dis.readInt() * 1.52587891E-05f;
      float posZ = (float) dis.readInt() * 1.52587891E-05f;
      GameObjectRunner.FacingDir facingDir = (GameObjectRunner.FacingDir) dis.readByte();
      GameObjectPlayer newObject = new GameObjectPlayer(this, posX, posY, posZ, facingDir);
      newObject.setFacingDir(facingDir);
      this.addObject((GameObject) newObject);
      this.m_minX = (float) dis.readInt() * 1.52587891E-05f;
      this.m_maxX = (float) dis.readInt() * 1.52587891E-05f;
      this.m_sectionWidthX = (float) dis.readInt() * 1.52587891E-05f;
      this.m_numSections = (int) dis.readShort();
      int length1 = 4 * this.m_numSections;
      this.m_attribStartOffset = new short[length1];
      this.m_attribEndOffset = new short[length1];
      for (int index = 0; index != length1; ++index)
      {
        this.m_attribStartOffset[index] = dis.readShort();
        this.m_attribEndOffset[index] = dis.readShort();
      }
      int length2 = (int) this.m_attribEndOffset[this.m_attribEndOffset.Length - 1];
      this.m_attribArray = new CollShape[length2];
      for (int index1 = 0; index1 != length2; ++index1)
      {
        float x1 = (float) dis.readInt() * 1.52587891E-05f;
        float y1 = (float) dis.readInt() * 1.52587891E-05f;
        float x2 = (float) dis.readInt() * 1.52587891E-05f;
        float y2 = (float) dis.readInt() * 1.52587891E-05f;
        int num = (int) dis.readByte();
        int index2 = dis.readInt();
        int materialLookup = MEdgeMap.MATERIAL_LOOKUP_ARRAY[index2];
        CollShape collShape = (CollShape) null;
        switch (num)
        {
          case 0:
            collShape = (CollShape) new CollOrthoHexahedron(x1, y1, MEdgeMap.COLLISION_BOX_MIN, x2, y2, MEdgeMap.COLLISION_BOX_MAX);
            break;
          case 1:
            collShape = (CollShape) new CollOrthoRampNegX(x1, y1, MEdgeMap.COLLISION_BOX_MIN, x2, y2, MEdgeMap.COLLISION_BOX_MAX);
            break;
          case 2:
            collShape = (CollShape) new CollOrthoRampPosX(x1, y1, MEdgeMap.COLLISION_BOX_MIN, x2, y2, MEdgeMap.COLLISION_BOX_MAX);
            break;
        }
        collShape.m_materialId = materialLookup;
        this.m_attribArray[index1] = collShape;
      }
      MapPalette mapPalette = new MapPalette(resLookup, this.m_modelSet);
      this.m_chunkManager.load(dis, ref mapPalette, this.m_mapNode);
      int length3 = (int) dis.readShort();
      this.m_paths = new Path[length3];
      for (int index = 0; index < length3; ++index)
        this.m_paths[index] = new Path(dis);
      int length4 = (int) dis.readShort();
      this.m_fallBoxes = new FallBox[length4];
      for (int index = 0; index < length4; ++index)
        this.m_fallBoxes[index] = new FallBox(dis);
      int length5 = (int) dis.readShort();
      this.m_painBoxes = new PainBox[length5];
      for (int index = 0; index < length5; ++index)
        this.m_painBoxes[index] = new PainBox(dis);
      int num1 = dis.readUnsignedShort();
      for (int index = 0; index != num1; ++index)
      {
        int num2 = (int) MEdgeMap.MAP_OBJECT_IDS[dis.readInt()];
        float num3 = (float) dis.readInt() * 1.52587891E-05f;
        float num4 = (float) dis.readInt() * 1.52587891E-05f;
        float num5 = (float) dis.readInt() * 1.52587891E-05f;
        int pathId = dis.readInt();
        ChunkRunnerVision runnerVisionChunk = (ChunkRunnerVision) null;
        if (dis.readByte() == (sbyte) 1)
          this.m_chunkManager.getGraphicsReference(dis, ref runnerVisionChunk);
        int flags = AppEngine.getGameObjectData().getFlags(num2);
        switch (num2)
        {
          case 2:
            if (!MirrorsEdge.TrialMode)
            {
              this.addObject((GameObject) new GameObjectPoliceLight(this, num3, num4, num5, pathId));
              break;
            }
            break;
          case 3:
            if (!MirrorsEdge.TrialMode)
            {
              this.addObject((GameObject) new GameObjectPoliceRiot(this, num3, num4, num5, pathId));
              break;
            }
            break;
          case 4:
            if (!MirrorsEdge.TrialMode)
            {
              this.addObject((GameObject) new GameObjectRivalGeneral(this, num3, num4, num5, pathId));
              break;
            }
            break;
          case 5:
            if (!MirrorsEdge.TrialMode)
            {
              this.addObject((GameObject) new GameObjectRivalBoss(this, num3, num4, num5, pathId));
              break;
            }
            break;
          case 6:
            this.addObject((GameObject) new GameObjectChopper(this, num3, num4, num5, pathId));
            break;
          case 7:
            this.addObject((GameObject) new GameObjectPlaneLight(this, num3, num4, num5));
            break;
          case 21:
            this.addObject((GameObject) new GameObjectEffectPigeonTakeoff(this, num3, num4, num5));
            break;
          default:
            if ((flags & 8) == 8)
            {
              if (!flag)
              {
                if (level.isBagFound(this.m_collectableObjectArray.Count))
                {
                  this.m_collectableObjectArray.Add((GameObject) null);
                  this.m_collectableObjectArrayRestart.Add((GameObject) null);
                  ++this.m_currentUniqueObjectID;
                  break;
                }
                this.addObject((GameObject) new GameObjectCollectable(this, ref mapPalette, num2, num3, num4, num5));
                break;
              }
              break;
            }
            this.addObject(new GameObject(this, num2, num3, num4, num5));
            break;
        }
      }
      int num6 = dis.readUnsignedByte();
      for (int index = 0; index < num6; ++index)
        this.addObject((GameObject) new GameObjectFinish(this, (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f));
      int num7 = dis.readUnsignedByte();
      for (int index = 0; index < num7; ++index)
      {
        float min_x = (float) dis.readInt() * 1.52587891E-05f;
        float min_y = (float) dis.readInt() * 1.52587891E-05f;
        float max_x = (float) dis.readInt() * 1.52587891E-05f;
        float max_y = (float) dis.readInt() * 1.52587891E-05f;
        ChunkRunnerVision runnerVisionChunk = (ChunkRunnerVision) null;
        microedition.m3g.Node graphicsReference = this.m_chunkManager.getGraphicsReference(dis, ref runnerVisionChunk);
        this.addObject((GameObject) new GameObjectCheckpoint(this, this.m_modelSet, min_x, min_y, max_x, max_y, graphicsReference, runnerVisionChunk.getType() != 1));
      }
      int num8 = dis.readUnsignedShort();
      for (int index = 0; index < num8; ++index)
        this.addObject((GameObject) new GameObjectAmbientSound(this, MEdgeMap.RES_LOOKUP_ARRAY[dis.readInt()], (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f));
      int num9 = dis.readUnsignedShort();
      for (int index = 0; index < num9; ++index)
        this.addObject((GameObject) new GameObjectSoundOneShot(this, MEdgeMap.RES_LOOKUP_ARRAY[dis.readInt()], (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f));
      int num10 = dis.readUnsignedShort();
      for (int index = 0; index < num10; ++index)
        this.addObject((GameObject) new GameObjectMusicCue(this, MEdgeMap.RES_LOOKUP_ARRAY[dis.readInt()], (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f, (float) dis.readInt() * 1.52587891E-05f));
      int num11 = (int) dis.readByte();
      for (int index = 0; index != num11; ++index)
      {
        float x1 = (float) dis.readInt() * 1.52587891E-05f;
        float y1 = (float) dis.readInt() * 1.52587891E-05f;
        float x2 = (float) dis.readInt() * 1.52587891E-05f;
        float y2 = (float) dis.readInt() * 1.52587891E-05f;
        float endOffset = (float) dis.readInt() * 1.52587891E-05f;
        this.addObject((GameObject) new GameObjectZipLine(this, ref mapPalette, x1, y1, x2, y2, endOffset));
      }
      int num12 = (int) dis.readShort();
      for (int index = 0; index != num12; ++index)
      {
        int stringId = (int) MEdgeMap.MAP_STRING_IDS[dis.readInt()];
        float minX = (float) dis.readInt() * 1.52587891E-05f;
        float minY = (float) dis.readInt() * 1.52587891E-05f;
        float maxX = (float) dis.readInt() * 1.52587891E-05f;
        float maxY = (float) dis.readInt() * 1.52587891E-05f;
        if (!flag)
          this.addObject((GameObject) new GameObjectPopupBox(this, stringId, minX, minY, maxX, maxY));
      }
      int num13 = (int) dis.readShort();
      for (int index = 0; index != num13; ++index)
      {
        int cutsceneId = (int) MEdgeMap.MAP_CUTSCENE_IDS[dis.readInt()];
        float minX = (float) dis.readInt() * 1.52587891E-05f;
        float minY = (float) dis.readInt() * 1.52587891E-05f;
        float maxX = (float) dis.readInt() * 1.52587891E-05f;
        float maxY = (float) dis.readInt() * 1.52587891E-05f;
        if (!flag)
          this.addObject((GameObject) new GameObjectCutsceneTrigger(this, cutsceneId, minX, minY, maxX, maxY));
      }
      int length6 = (int) dis.readShort();
      this.m_cutscenes = new DataCameraAnim[length6];
      for (int index = 0; index < length6; ++index)
        this.m_cutscenes[index] = new DataCameraAnim(dis);
      dis.close();
      this.m_skyDomeNode = mapPalette.getNode(150);
      if (this.m_skyDomeNode != null)
        M3GAssets.addNode((Group) world, this.m_skyDomeNode);
      Appearance texturedAppearance1 = m3Gassets.getTexturedAppearance((int) M3GAssets.get("TEX_EFFECT_SMOKE_ALPHA_ADD"));
      if (texturedAppearance1 != null)
        this.m_steam1ScrollingTexture = texturedAppearance1.getTexture(0);
      Appearance texturedAppearance2 = m3Gassets.getTexturedAppearance((int) M3GAssets.get("TEX_EFFECT_STEAMJET_ALPHA_ADD"));
      if (texturedAppearance2 != null)
        this.m_steam2ScrollingTexture = texturedAppearance2.getTexture(0);
      this.m_steam1ScrollProgress = 0.0f;
      this.m_steam2ScrollProgress = 0.0f;
      this.m_loadingMap = false;
    }

    public void freeMap()
    {
      this.m_playerObject = (GameObjectPlayer) null;
      this.clearObjectList(ref this.m_staticObjectArray, false);
      this.clearObjectList(ref this.m_dynamicObjectArray, false);
      this.m_collectableObjectArrayRestart.Clear();
      this.clearObjectList(ref this.m_collectableObjectArray, false);
      this.clearObjectList(ref this.m_objectUpdateArray, false);
      this.clearObjectList(ref this.m_playerCollObjectArray, false);
      this.clearObjectList(ref this.m_everythingCollObjectArray, false);
      this.clearObjectList(ref this.m_allCollObjectArray, false);
      int length = this.m_attribArray.Length;
      for (int index = 0; index != length; ++index)
      {
        this.m_attribArray[index].Destructor();
        this.m_attribArray[index] = (CollShape) null;
      }
      this.m_mapNode = (Group) null;
      this.m_skyDomeNode = (microedition.m3g.Node) null;
    }

    public void resetCheckpoint()
    {
      foreach (GameObject staticObject in this.m_staticObjectArray)
        staticObject.resetCheckpoint();
      for (int index = this.m_dynamicObjectArray.Count - 1; index != -1; --index)
        this.removeDynamicObject(this.m_dynamicObjectArray[index], false);
      this.m_numCollectedCollectables = 0;
      foreach (GameObject collectableObject in this.m_collectableObjectArray)
      {
        if (collectableObject == null)
          ++this.m_numCollectedCollectables;
        else
          collectableObject.resetCheckpoint();
      }
    }

    public void resetLevel()
    {
      foreach (GameObject staticObject in this.m_staticObjectArray)
        staticObject.resetLevel();
      for (int index = this.m_dynamicObjectArray.Count - 1; index != -1; --index)
        this.removeDynamicObject(this.m_dynamicObjectArray[index], false);
      this.m_collectableObjectArray.Clear();
      foreach (GameObject gameObject in this.m_collectableObjectArrayRestart)
        this.m_collectableObjectArray.Add(gameObject);
      this.m_numCollectedCollectables = 0;
      foreach (GameObject collectableObject in this.m_collectableObjectArray)
      {
        if (collectableObject == null)
          ++this.m_numCollectedCollectables;
        else
          collectableObject.resetLevel();
      }
      if (this.m_checkpointObject == null)
        return;
      this.m_checkpointObject.deactivate();
      this.m_checkpointObject = (GameObjectCheckpoint) null;
    }

    public List<GameObject> getCollectableList() => this.m_collectableObjectArray;

    public void updateCollectableList()
    {
      for (int index = 0; index < this.m_collectableObjectArray.Count; ++index)
      {
        GameObject collectableObject = this.m_collectableObjectArray[index];
        if (collectableObject != null && !collectableObject.isActive())
          this.m_collectableObjectArray[index] = (GameObject) null;
      }
    }

    public int getNumPlayerCheckObjects() => this.m_playerCollObjectArray.Count;

    public GameObject getPlayerCheckObject(int index) => this.m_playerCollObjectArray[index];

    public GameObjectPlayer getPlayerObject() => this.m_playerObject;

    public GameObjectCheckpoint getCheckpointObject() => this.m_checkpointObject;

    public void setCheckpointObject(GameObjectCheckpoint cpObject)
    {
      if (this.m_checkpointObject != null)
        this.m_checkpointObject.deactivate();
      this.m_checkpointObject = cpObject;
      for (int index = this.m_staticObjectArray.Count - 1; index != -1; --index)
        this.m_staticObjectArray[index].checkpointActivated();
      this.updateCollectableList();
      SpywareManager.getInstance().trackCheckpointTriggered(this.getUniqueGameObjectID((GameObject) cpObject));
      AppEngine.getCanvas().getSceneGame().checkpointReached();
    }

    public int getNumCollectablesCollected() => this.m_numCollectedCollectables;

    public int getNumCollectablesTotal() => this.m_collectableObjectArray.Count;

    public bool isAllCollectablesFound()
    {
      return this.m_numCollectedCollectables == this.m_collectableObjectArray.Count;
    }

    public void addObject(GameObject newObject)
    {
      if (newObject.getID() == -1)
      {
        newObject.setID(this.m_currentUniqueObjectID);
        ++this.m_currentUniqueObjectID;
      }
      int type = newObject.getType();
      byte collidesWith = AppEngine.getGameObjectData().getCollidesWith(type);
      if (newObject.isFlagSet(1))
        this.m_playerObject = newObject as GameObjectPlayer;
      if (newObject.isFlagSet(16))
        this.m_objectUpdateArray.Add(newObject);
      switch (collidesWith)
      {
        case 1:
          this.m_playerCollObjectArray.Add(newObject);
          this.m_allCollObjectArray.Add(newObject);
          break;
        case 2:
          this.m_everythingCollObjectArray.Add(newObject);
          this.m_allCollObjectArray.Add(newObject);
          break;
      }
      if (newObject.isFlagSet(8))
      {
        this.m_collectableObjectArray.Add(newObject);
        this.m_collectableObjectArrayRestart.Add(newObject);
      }
      else if (this.m_loadingMap)
      {
        newObject.markObjectStatic();
        this.m_staticObjectArray.Add(newObject);
      }
      else
        this.m_dynamicObjectArray.Add(newObject);
    }

    public void removeObject(GameObject doomedObject)
    {
      if (doomedObject.isFlagSet(8))
      {
        SpywareManager.getInstance().trackBagPickedUp(this.getUniqueGameObjectID(doomedObject));
        ++this.m_numCollectedCollectables;
        AppEngine.getCanvas().getSceneGame().notifyBagCollected();
      }
      doomedObject.deactivate();
    }

    private void removeDynamicObject(GameObject doomedObject, bool callDestructor)
    {
      int type = doomedObject.getType();
      byte collidesWith = AppEngine.getGameObjectData().getCollidesWith(type);
      if (this.m_playerObject == doomedObject)
        this.m_playerObject = (GameObjectPlayer) null;
      if (doomedObject.isFlagSet(16))
        this.m_objectUpdateArray.Remove(doomedObject);
      switch (collidesWith)
      {
        case 1:
          this.m_playerCollObjectArray.Remove(doomedObject);
          this.m_allCollObjectArray.Remove(doomedObject);
          break;
        case 2:
          this.m_everythingCollObjectArray.Remove(doomedObject);
          this.m_allCollObjectArray.Remove(doomedObject);
          break;
      }
      this.m_dynamicObjectArray.Remove(doomedObject);
      if (!callDestructor)
      {
        switch (doomedObject)
        {
          case GameObjectBullet _:
          case GameObjectEffectPickup _:
            break;
          default:
            goto label_10;
        }
      }
      doomedObject.Destructor();
label_10:
      doomedObject = (GameObject) null;
    }

    public microedition.m3g.Node getSkyDomeNode() => this.m_skyDomeNode;

    public ChunkLayer getForegroundLayer() => this.m_chunkManager.getForegroundLayer();

    public void updateObjects(int timeStepMillis)
    {
      for (int index = 0; index < this.m_objectUpdateArray.Count; ++index)
      {
        GameObject objectUpdate = this.m_objectUpdateArray[index];
        objectUpdate.update(timeStepMillis);
        objectUpdate.updateAnimation(timeStepMillis);
      }
      for (int index = this.m_dynamicObjectArray.Count - 1; index != -1; --index)
      {
        GameObject dynamicObject = this.m_dynamicObjectArray[index];
        if (!dynamicObject.isActive())
          this.removeDynamicObject(dynamicObject, true);
      }
      this.m_steam1ScrollProgress -= 0.0004f * (float) timeStepMillis;
      this.m_steam2ScrollProgress -= 1f / 500f * (float) timeStepMillis;
      while ((double) this.m_steam1ScrollProgress < 0.0)
        ++this.m_steam1ScrollProgress;
      while ((double) this.m_steam2ScrollProgress < 0.0)
        ++this.m_steam2ScrollProgress;
      if (this.m_steam1ScrollingTexture != null)
        this.m_steam1ScrollingTexture.setTranslation(0.0f, this.m_steam1ScrollProgress, 0.0f);
      if (this.m_steam2ScrollingTexture == null)
        return;
      this.m_steam2ScrollingTexture.setTranslation(0.0f, this.m_steam2ScrollProgress, 0.0f);
    }

    public void updateObjectCollisions()
    {
      int count1 = this.m_everythingCollObjectArray.Count;
      for (int index1 = 0; index1 != count1; ++index1)
      {
        GameObject everythingCollObject1 = this.m_everythingCollObjectArray[index1];
        if (everythingCollObject1.isActive())
        {
          everythingCollObject1.validateCollShape();
          CollShape globalShape = everythingCollObject1.m_globalShape;
          for (int index2 = 0; index2 != index1; ++index2)
          {
            GameObject everythingCollObject2 = this.m_everythingCollObjectArray[index2];
            if (everythingCollObject2.isActive() && CollShape.testIntersection(globalShape, everythingCollObject2.m_globalShape))
            {
              everythingCollObject1.collidedWith(everythingCollObject2);
              everythingCollObject2.collidedWith(everythingCollObject1);
            }
          }
        }
      }
      if (this.m_playerObject == null || !this.m_playerObject.isActive())
        return;
      int count2 = this.m_playerCollObjectArray.Count;
      CollShape globalShape1 = this.m_playerObject.m_globalShape;
      foreach (GameObject playerCollObject in this.m_playerCollObjectArray)
      {
        if (playerCollObject.isActive())
        {
          playerCollObject.validateCollShape();
          if (CollShape.testIntersection(globalShape1, playerCollObject.m_globalShape))
          {
            this.m_playerObject.collidedWith(playerCollObject);
            playerCollObject.collidedWith((GameObject) this.m_playerObject);
          }
        }
      }
    }

    public void updateChunkVisibility(int timeStepMillis, GameCamera gameCamera)
    {
      this.m_chunkManager.update((float) timeStepMillis * (1f / 1000f));
    }

    private int getAttribSectionOffset(int attribType, float x)
    {
      return attribType * this.m_numSections + Math.Max(0, Math.Min(this.m_numSections - 1, (int) (((double) x - (double) this.m_minX) / (double) this.m_sectionWidthX)));
    }

    public void getAttribSectionBounds(
      int attribType,
      float x,
      ref int startIndex,
      ref int endIndex)
    {
      if ((double) x < (double) this.m_minX || (double) this.m_maxX < (double) x)
      {
        startIndex = 0;
        endIndex = 0;
      }
      else
      {
        int attribSectionOffset = this.getAttribSectionOffset(attribType, x);
        startIndex = (int) this.m_attribStartOffset[attribSectionOffset];
        endIndex = (int) this.m_attribEndOffset[attribSectionOffset];
      }
    }

    public void getAttribSectionBounds(
      int attribType,
      MathLine line,
      float t1,
      float t2,
      ref int startIndex,
      ref int endIndex)
    {
      if ((double) line.direction.x == 0.0)
      {
        this.getAttribSectionBounds(attribType, line.origin.x, ref startIndex, ref endIndex);
      }
      else
      {
        float val1 = line.origin.x + t1 * line.direction.x;
        float val2 = line.origin.x + t2 * line.direction.x;
        this.getAttribSectionBounds(attribType, Math.Min(val1, val2), Math.Max(val1, val2), ref startIndex, ref endIndex);
      }
    }

    public void getAttribSectionBounds(
      int attribType,
      float minX,
      float maxX,
      ref int startIndex,
      ref int endIndex)
    {
      if ((double) maxX < (double) this.m_minX || (double) this.m_maxX < (double) minX)
      {
        startIndex = 0;
        endIndex = 0;
      }
      else
      {
        startIndex = (int) this.m_attribStartOffset[this.getAttribSectionOffset(attribType, minX)];
        endIndex = (int) this.m_attribEndOffset[this.getAttribSectionOffset(attribType, maxX)];
      }
    }

    public CollShape getAttributeShapeAt(MathVector point, int attribTypeIndex)
    {
      int startIndex = 0;
      int endIndex = 0;
      this.getAttribSectionBounds(attribTypeIndex, point.x, ref startIndex, ref endIndex);
      for (int index = startIndex; index != endIndex; ++index)
      {
        if (this.m_attribArray[index].intersects(point))
          return this.m_attribArray[index];
      }
      return (CollShape) null;
    }

    public CollShape getAttributeShapeAt(MathLine line, int attribTypeIndex)
    {
      int startIndex = 0;
      int endIndex = 0;
      this.getAttribSectionBounds(attribTypeIndex, line.origin.x, ref startIndex, ref endIndex);
      CollShape collShape = (CollShape) null;
      float num = -1f;
      float minT = 0.0f;
      float maxT = 0.0f;
      for (int index = startIndex; index != endIndex; ++index)
      {
        CollShape attrib = this.m_attribArray[index];
        if (attrib.intersects(line, ref minT, ref maxT))
        {
          if ((double) minT <= 0.0 && 0.0 <= (double) maxT)
          {
            collShape = attrib;
            num = 0.0f;
          }
          else if (0.0 <= (double) minT && ((double) num < 0.0 || (double) minT < (double) num))
          {
            collShape = attrib;
            num = minT;
          }
        }
      }
      return (double) num < 0.0 ? (CollShape) null : collShape;
    }

    public bool intersects(MathVector point, int attribTypeIndex)
    {
      CollShape colShape = (CollShape) null;
      return this.intersects(point, attribTypeIndex, ref colShape);
    }

    public bool intersects(MathLine line, int attribTypeIndex, ref float minT, ref float maxT)
    {
      float min2 = minT;
      float max2 = maxT;
      bool flag = false;
      int startIndex = 0;
      int endIndex = 0;
      this.getAttribSectionBounds(attribTypeIndex, line, minT, maxT, ref startIndex, ref endIndex);
      float minT1 = 0.0f;
      float maxT1 = 0.0f;
      for (int index = startIndex; index != endIndex; ++index)
      {
        if (this.m_attribArray[index].intersects(line, ref minT1, ref maxT1) && GameCommon.boundsIntersect(minT1, maxT1, min2, max2))
        {
          if (flag)
          {
            minT = Math.Min(minT, minT1);
            maxT = Math.Max(maxT, maxT1);
          }
          else
          {
            minT = minT1;
            maxT = maxT1;
            flag = true;
          }
        }
      }
      if (flag)
      {
        for (int index = startIndex; index != endIndex; ++index)
        {
          if (this.m_attribArray[index].intersects(line, ref minT1, ref maxT1) && GameCommon.boundsIntersect(minT1, maxT1, minT, maxT))
          {
            minT = Math.Min(minT, minT1);
            maxT = Math.Max(maxT, maxT1);
          }
        }
      }
      return flag;
    }

    public bool intersects(CollShape shape, int attribTypeIndex)
    {
      int startIndex = 0;
      int endIndex = 0;
      MathOrthoBox bounds = shape.getBounds();
      this.getAttribSectionBounds(attribTypeIndex, bounds.min.x, bounds.max.x, ref startIndex, ref endIndex);
      for (int index = startIndex; index != endIndex; ++index)
      {
        if (CollShape.testIntersection(this.m_attribArray[index], shape))
          return true;
      }
      return false;
    }

    public bool intersects(MathVector point, int attribTypeIndex, ref CollShape colShape)
    {
      int startIndex = 0;
      int endIndex = 0;
      this.getAttribSectionBounds(attribTypeIndex, point.x, ref startIndex, ref endIndex);
      for (int index = startIndex; index != endIndex; ++index)
      {
        if (this.m_attribArray[index].intersects(point))
        {
          colShape = this.m_attribArray[index];
          return true;
        }
      }
      return false;
    }

    public bool calculateCollision(MathLine line, ref float tValue, int attribTypeIndex)
    {
      tValue = 1E+09f;
      bool collision = false;
      float minT = 0.0f;
      float maxT = 0.0f;
      int startIndex = 0;
      int endIndex = 0;
      this.getAttribSectionBounds(attribTypeIndex, line.origin.x, ref startIndex, ref endIndex);
      for (int index = startIndex; index != endIndex; ++index)
      {
        if (this.m_attribArray[index].intersects(line, ref minT, ref maxT) && 0.0 <= (double) minT && (!collision || (double) minT < (double) tValue))
        {
          tValue = minT;
          collision = true;
        }
      }
      return collision;
    }

    public float calculateCollisionMoveOut(MathLine line, int attribTypeIndex)
    {
      int startIndex = 0;
      int endIndex = 0;
      float minT = 0.0f;
      this.getAttribSectionBounds(attribTypeIndex, line.origin.x, ref startIndex, ref endIndex);
      for (int index = startIndex; index != endIndex; ++index)
      {
        if (this.m_attribArray[index].intersects(line.origin))
        {
          float maxT = 0.0f;
          this.m_attribArray[index].intersects(line, ref minT, ref maxT);
          return maxT;
        }
      }
      return 0.0f;
    }

    public GameObject getGameObjectAt(MathLine line, GameObject excludeObject)
    {
      int count = this.m_allCollObjectArray.Count;
      foreach (GameObject allCollObject in this.m_allCollObjectArray)
      {
        if (allCollObject != excludeObject && allCollObject.isActive())
        {
          allCollObject.validateCollShape();
          float minT = 0.0f;
          float maxT = 1f;
          if (allCollObject.m_globalShape.intersects(line, ref minT, ref maxT) && GameCommon.boundsIntersect(0.0f, 1f, minT, maxT))
            return allCollObject;
        }
      }
      return (GameObject) null;
    }

    public int moveObject(
      ref MathVector position,
      ref MathVector velocity,
      float timeStepSecs,
      CollShape objectShape)
    {
      int num1 = 0;
      float num2 = timeStepSecs;
      MathVector move = velocity * num2;
      MathVector position1 = position + move;
      objectShape.setPosition(position1);
      int startIndex = 0;
      int endIndex = 0;
      MathOrthoBox bounds = objectShape.getBounds();
      this.getAttribSectionBounds(0, bounds.min.x, bounds.max.x, ref startIndex, ref endIndex);
      float adjustMoveMultiple = 0.0f;
      CollShape collidedShape = (CollShape) null;
      MathVector collisionNormal = new MathVector();
      if (CollShape.moveShape(objectShape, ref position, move, ref this.m_attribArray, startIndex, endIndex, ref adjustMoveMultiple, ref collidedShape, ref collisionNormal))
      {
        if (0.0099999997764825821 < (double) collisionNormal.x)
          num1 |= 1;
        else if ((double) collisionNormal.x < -0.0099999997764825821)
          num1 |= 2;
        if (0.0099999997764825821 < (double) collisionNormal.y)
          num1 |= 4;
        else if ((double) collisionNormal.y < -0.0099999997764825821)
          num1 |= 8;
        if (0.0099999997764825821 < (double) collisionNormal.z)
          num1 |= 16;
        else if ((double) collisionNormal.z < -0.0099999997764825821)
          num1 |= 32;
      }
      return num1;
    }

    public int moveObjectPhysics(
      ref MathVector position,
      ref MathVector velocity,
      float timeStepSecs,
      CollShape objectShape,
      ref CollShape collidedShape,
      ref MathVector collisionNormal)
    {
      int num1 = 0;
      float num2 = timeStepSecs;
      while ((double) num2 != 0.0 && (num1 & 960) == 0 && !velocity.isZero())
      {
        MathVector move = velocity * num2;
        MathVector position1 = position + move;
        objectShape.setPosition(position1);
        int startIndex = 0;
        int endIndex = 0;
        MathOrthoBox bounds = objectShape.getBounds();
        this.getAttribSectionBounds(0, bounds.min.x, bounds.max.x, ref startIndex, ref endIndex);
        float adjustMoveMultiple = 0.0f;
        if (CollShape.moveShape(objectShape, ref position, move, ref this.m_attribArray, startIndex, endIndex, ref adjustMoveMultiple, ref collidedShape, ref collisionNormal))
        {
          if (0.0099999997764825821 < (double) collisionNormal.x)
            num1 |= 1;
          else if ((double) collisionNormal.x < -0.0099999997764825821)
            num1 |= 2;
          if (0.0099999997764825821 < (double) collisionNormal.y)
            num1 |= 4;
          else if ((double) collisionNormal.y < -0.0099999997764825821)
            num1 |= 8;
          if (0.0099999997764825821 < (double) collisionNormal.z)
            num1 |= 16;
          else if ((double) collisionNormal.z < -0.0099999997764825821)
            num1 |= 32;
          if (0.0099999997764825821 < (double) collisionNormal.y)
          {
            if (0.0099999997764825821 < (double) collisionNormal.x)
              num1 |= 64;
            else if ((double) collisionNormal.x < -0.0099999997764825821)
              num1 |= 128;
          }
          else if ((double) collisionNormal.y < -0.0099999997764825821)
          {
            if (0.0099999997764825821 < (double) collisionNormal.x)
              num1 |= 256;
            else if ((double) collisionNormal.x < -0.0099999997764825821)
              num1 |= 512;
          }
          MathVector other = collisionNormal;
          other *= -other.dot(velocity);
          MathVector mathVector1 = velocity + other;
          float num3 = objectShape.getBounce() * collidedShape.getBounce();
          other *= num3;
          MathVector mathVector2 = mathVector1 * (objectShape.getInverseResistance() * collidedShape.getInverseResistance());
          velocity.set(other);
          velocity += mathVector2;
          if (!collisionNormal.isOrthogonal() && 0.0 < (double) collisionNormal.y)
            velocity.y += 0.01f;
          if (0.0 <= (double) adjustMoveMultiple)
            num2 -= num2 * adjustMoveMultiple;
        }
        else
          num2 = 0.0f;
      }
      return num1;
    }

    public DataCameraAnim getCutscene(int idx) => this.m_cutscenes[idx];

    public Path getPath(int idx) => this.m_paths[idx];

    public bool isInFallBox(GameObject testObj)
    {
      for (int index = 0; index < this.m_fallBoxes.Length; ++index)
      {
        if (this.m_fallBoxes[index].contains(testObj.m_position))
          return true;
      }
      return false;
    }

    public bool isInPainBox(GameObject testObj)
    {
      for (int index = 0; index < this.m_painBoxes.Length; ++index)
      {
        if (this.m_painBoxes[index].intersects(testObj))
          return true;
      }
      return false;
    }
  }
}
