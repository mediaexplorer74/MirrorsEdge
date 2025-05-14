
// Type: generic.AnimationManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;

#nullable disable
namespace generic
{
  public class AnimationManager
  {
    public const int SUB_IMAGE_ID = 0;
    public const int SUB_IMAGE_X = 1;
    public const int SUB_IMAGE_Y = 2;
    public const int SUB_IMAGE_WIDTH = 3;
    public const int SUB_IMAGE_HEIGHT = 4;
    public const int NUM_SUB_IMAGE_FIELDS = 5;
    public const int MAX_FILE_SIZE = 4096;
    public const int PRIMITIVE_SPRITE_ENUM = 0;
    public const int PRIMITIVE_CLIP_ENUM = 1;
    public const int PRIMITIVE_COLL_BOX_ENUM = 2;
    public const int PRIMITIVE_HOLLOW_RECTANGLE_ENUM = 3;
    public const int PRIMITIVE_FILLED_RECTANGLE_ENUM = 4;
    public const int PRIMITIVE_HOLLOW_ELLIPSE_ENUM = 5;
    public const int PRIMITIVE_FILLED_ELLIPSE_ENUM = 6;
    public const int PRIMITIVE_FIRE_POINT_ENUM = 7;
    public const int PRIMITIVE_LINE_ENUM = 8;
    public const int PRIMITIVE_TEXT_ENUM = 9;
    public const int PRIMITIVE_COLOR_ENUM = 10;
    public const int PRIMITIVE_XFLIPPED_SPRITE_ENUM = 11;
    public const int PRIMITIVE_RIGHT_TRIANGLE_ENUM = 12;
    public const int PRIMITIVE_ATTRIB_TYPE = 0;
    public const int PRIMITIVE_ATTRIB_X = 1;
    public const int PRIMITIVE_ATTRIB_Y = 2;
    public const int PRIMITIVE_ATTRIB_X2 = 3;
    public const int PRIMITIVE_ATTRIB_Y2 = 4;
    public const int PRIMITIVE_ATTRIB_SUBIMAGE = 3;
    public const int PRIMITIVE_ATTRIB_WIDTH = 3;
    public const int PRIMITIVE_ATTRIB_HEIGHT = 4;
    public const int PRIMITIVE_ATTRIB_COLOUR_INDEX = 1;
    public const int ANIMFLAG_XFLIPPED = 16384;
    public const int NUM_BANKS = 1;
    public static int ANIM_DATA_FILE = (int) ResourceManager.get("IDI_ANIMDATA_BIN");
    public static int IMAGE_DATA_FILE = (int) ResourceManager.get("IDI_IMAGE_BIN");
    public static int COLOR_DATA_FILE = (int) ResourceManager.get("IDI_COLOR_BIN");

    public static void constructAnimationManager(ref AnimationManagerData thisData)
    {
      for (int index = 0; index < 48; ++index)
        thisData.m_animPlayerPool[index] = new AnimPlayer();
      thisData.colourData = new sbyte[0];
      thisData.animNumFrames = new sbyte[3];
      thisData.animFrameOffset = new short[3];
      thisData.frameDuration = new short[14];
      thisData.frameNumPrimitives = new sbyte[14];
      thisData.framePrimitiveOffset = new int[14];
      thisData.primitiveData = new short[48];
      thisData.m_animImageArray = new Image[1][];
      for (int index = 0; index < 1; ++index)
        thisData.m_animImageArray[index] = new Image[1];
    }

    public static bool loadColorsFile(ref AnimationManagerData thisData, ResourceManager resMgr)
    {
      InputStream inputStream = resMgr.loadBinaryFile(AnimationManager.COLOR_DATA_FILE);
      thisData.colourData = new sbyte[ResourceManager.RESOURCE_FILESIZE_LIST[AnimationManager.COLOR_DATA_FILE]];
      inputStream.read(ref thisData.colourData);
      inputStream.close();
      return true;
    }

    public static void setColor(Graphics g, int index)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = index * 3;
      int red = (int) (byte) animManData.colourData[index1];
      int green = (int) (byte) animManData.colourData[index1 + 1];
      int blue = (int) (byte) animManData.colourData[index1 + 2];
      g.setColor(red, green, blue);
    }

    public static bool loadSubimageFile(ref AnimationManagerData thisData, ResourceManager resMgr)
    {
      DataInputStream dataInputStream = new DataInputStream(resMgr.loadBinaryFile(AnimationManager.IMAGE_DATA_FILE));
      for (int index = 0; index < 12; ++index)
      {
        thisData.m_subImages[index][0] = (short) dataInputStream.readUnsignedShort();
        thisData.m_subImages[index][1] = (short) dataInputStream.readUnsignedShort();
        thisData.m_subImages[index][2] = (short) dataInputStream.readUnsignedShort();
        thisData.m_subImages[index][3] = (short) dataInputStream.readUnsignedShort();
        thisData.m_subImages[index][4] = (short) dataInputStream.readUnsignedShort();
      }
      return true;
    }

    public static bool loadAnimFile(ref AnimationManagerData thisData, ResourceManager resMgr)
    {
      DataInputStream dataInputStream = new DataInputStream(resMgr.loadBinaryFile(AnimationManager.ANIM_DATA_FILE));
      int index1 = 0;
      int num1 = 0;
      for (int index2 = 0; index2 != 3; ++index2)
      {
        int num2 = dataInputStream.readUnsignedByte();
        thisData.animNumFrames[index2] = (sbyte) num2;
        thisData.animFrameOffset[index2] = (short) index1;
        int num3 = 0;
        while (num3 != num2)
        {
          thisData.frameDuration[index1] = dataInputStream.readShort();
          int num4 = dataInputStream.readUnsignedByte();
          thisData.frameNumPrimitives[index1] = (sbyte) num4;
          thisData.framePrimitiveOffset[index1] = num1;
          for (int index3 = 0; index3 != num4; ++index3)
          {
            int num5 = dataInputStream.readUnsignedByte();
            thisData.primitiveData[num1++] = (short) num5;
            switch (num5)
            {
              case 0:
              case 11:
                int num6 = num1 - 1;
                thisData.primitiveData[num6 + 3] = dataInputStream.readShort();
                thisData.primitiveData[num6 + 1] = dataInputStream.readShort();
                thisData.primitiveData[num6 + 2] = dataInputStream.readShort();
                num1 = num6 + 4;
                break;
              case 1:
              case 2:
              case 3:
              case 4:
              case 5:
              case 6:
              case 8:
                short[] primitiveData1 = thisData.primitiveData;
                int index4 = num1;
                int num7 = index4 + 1;
                int num8 = (int) dataInputStream.readShort();
                primitiveData1[index4] = (short) num8;
                short[] primitiveData2 = thisData.primitiveData;
                int index5 = num7;
                int num9 = index5 + 1;
                int num10 = (int) dataInputStream.readShort();
                primitiveData2[index5] = (short) num10;
                short[] primitiveData3 = thisData.primitiveData;
                int index6 = num9;
                int num11 = index6 + 1;
                int num12 = (int) dataInputStream.readShort();
                primitiveData3[index6] = (short) num12;
                short[] primitiveData4 = thisData.primitiveData;
                int index7 = num11;
                num1 = index7 + 1;
                int num13 = (int) dataInputStream.readShort();
                primitiveData4[index7] = (short) num13;
                break;
              case 7:
                short[] primitiveData5 = thisData.primitiveData;
                int index8 = num1;
                int num14 = index8 + 1;
                int num15 = (int) dataInputStream.readShort();
                primitiveData5[index8] = (short) num15;
                short[] primitiveData6 = thisData.primitiveData;
                int index9 = num14;
                num1 = index9 + 1;
                int num16 = (int) dataInputStream.readShort();
                primitiveData6[index9] = (short) num16;
                break;
              case 10:
                thisData.primitiveData[num1++] = (short) dataInputStream.readUnsignedByte();
                break;
              case 12:
                short[] primitiveData7 = thisData.primitiveData;
                int index10 = num1;
                int num17 = index10 + 1;
                int num18 = (int) dataInputStream.readShort();
                primitiveData7[index10] = (short) num18;
                short[] primitiveData8 = thisData.primitiveData;
                int index11 = num17;
                int num19 = index11 + 1;
                int num20 = (int) dataInputStream.readShort();
                primitiveData8[index11] = (short) num20;
                short[] primitiveData9 = thisData.primitiveData;
                int index12 = num19;
                int num21 = index12 + 1;
                int num22 = (int) dataInputStream.readShort();
                primitiveData9[index12] = (short) num22;
                short[] primitiveData10 = thisData.primitiveData;
                int index13 = num21;
                num1 = index13 + 1;
                int num23 = (int) dataInputStream.readShort();
                primitiveData10[index13] = (short) num23;
                break;
            }
          }
          ++num3;
          ++index1;
        }
      }
      return true;
    }

    public static bool loadImage(ResourceManager resourceManager, int resID)
    {
      return AnimationManager.loadImage(AppEngine.getCanvas().getAnimManData(), resourceManager, resID);
    }

    public static bool loadImage(
      AnimationManagerData thisData,
      ResourceManager resourceManager,
      int resID)
    {
      int index1 = 0;
      while (index1 < 1 && resID != (int) ResourceManager.IMAGE_RES_IDS[index1])
        ++index1;
      if (1 == index1)
        return false;
      int index2 = 0;
      if (thisData.m_animImageArray[index2][index1] == null)
      {
        Image image = resourceManager.loadImage(resID);
        thisData.m_animImageArray[index2][index1] = image;
      }
      return true;
    }

    public static bool unloadImage(int resID)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index = 0;
      while (index < 1 && resID != (int) ResourceManager.IMAGE_RES_IDS[index])
        ++index;
      if (1 == index)
        return false;
      if (animManData.m_animImageArray[0][index] != null)
      {
        animManData.m_animImageArray[0][index].Destructor();
        animManData.m_animImageArray[0][index] = (Image) null;
      }
      return true;
    }

    public static AnimPlayer createAnimPlayer() => new AnimPlayer();

    private static int getNumAttributes(int primitiveType)
    {
      switch (primitiveType)
      {
        case 0:
        case 11:
          return 4;
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 8:
        case 12:
          return 5;
        case 7:
          return 3;
        case 10:
          return 2;
        default:
          return 0;
      }
    }

    public static void drawAnimFrame(Graphics g, int animID, int frameID, int x, int y)
    {
      if (animID < 0)
        return;
      AnimationManager.drawNormalAnimFrame(g, animID, frameID, x, y);
    }

    private static void drawNormalAnimFrame(Graphics g, int animID, int frameID, int x, int y)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      for (int index2 = 0; index2 != frameNumPrimitive; ++index2)
      {
        short primitiveType = animManData.primitiveData[primitiveIndex];
        switch (primitiveType)
        {
          case 0:
          case 11:
            int num1 = x + (int) animManData.primitiveData[primitiveIndex + 1];
            int num2 = y + (int) animManData.primitiveData[primitiveIndex + 2];
            short[] subImage = animManData.m_subImages[(int) animManData.primitiveData[primitiveIndex + 3]];
            int index3 = (int) subImage[0];
            int srcLeft = (int) subImage[1];
            int srcTop = (int) subImage[2];
            int srcRight = srcLeft + (int) subImage[3];
            int srcBottom = srcTop + (int) subImage[4];
            int destLeft = num1;
            int destTop = num2;
            int destRight = num1 + (srcRight - srcLeft) / Runtime.pixelScale;
            int destBottom = num2 + (srcBottom - srcTop) / Runtime.pixelScale;
            Image src = animManData.m_animImageArray[animManData.m_curBank][index3];
            g.drawScaledRegion(src, srcLeft, srcTop, srcRight, srcBottom, destLeft, destTop, destRight, destBottom);
            goto case 2;
          case 2:
          case 7:
            primitiveIndex += AnimationManager.getNumAttributes((int) primitiveType);
            continue;
          default:
            AnimationManager.drawPrimitive(g, x, y, primitiveIndex, (int) primitiveType);
            goto case 2;
        }
      }
    }

    private static bool drawNormalAnimFrameExt(
      Graphics g,
      int animID,
      int frameID,
      int x,
      int y,
      int extraAnimId,
      int extraIndex,
      int[] firepoint)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      bool flag = false;
      int num1 = extraAnimId >= 0 ? (int) animManData.animFrameOffset[extraAnimId] : -1;
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive1 = (int) animManData.frameNumPrimitives[index1];
      for (int index2 = 0; index2 != frameNumPrimitive1; ++index2)
      {
        short primitiveType1 = animManData.primitiveData[primitiveIndex];
        switch (primitiveType1)
        {
          case 0:
          case 11:
            int x_dest1 = x + (int) animManData.primitiveData[primitiveIndex + 1];
            int y_dest1 = y + (int) animManData.primitiveData[primitiveIndex + 2];
            int index3 = (int) animManData.primitiveData[primitiveIndex + 3];
            short[] subImage1 = animManData.m_subImages[index3];
            int index4 = (int) subImage1[0];
            Image src1 = animManData.m_animImageArray[animManData.m_curBank][index4];
            int transform1 = primitiveType1 == (short) 11 ? 2 : 0;
            g.drawRegion(src1, (int) subImage1[1], (int) subImage1[2], (int) subImage1[3], (int) subImage1[4], transform1, x_dest1, y_dest1, 9);
            if (num1 >= 0)
            {
              int animNumFrame = (int) animManData.animNumFrames[extraAnimId];
              short[] primitiveData = animManData.primitiveData;
              for (int index5 = 0; index5 < animNumFrame; ++index5)
              {
                int index6 = animManData.framePrimitiveOffset[num1 + index5];
                int num2 = (int) primitiveData[index6 + 3] & (int) ushort.MaxValue;
                if (index3 == num2)
                {
                  int num3 = (int) primitiveData[index6 + 1];
                  int num4 = (int) primitiveData[index6 + 2];
                  int frameNumPrimitive2 = (int) animManData.frameNumPrimitives[num1 + index5];
                  if (firepoint != null)
                  {
                    flag = true;
                    int index7 = index6;
                    int num5 = frameNumPrimitive2 - 1;
                    for (int index8 = 0; index8 < num5; ++index8)
                    {
                      int primitiveType2 = (int) primitiveData[index7];
                      index7 += AnimationManager.getNumAttributes(primitiveType2);
                    }
                    firepoint[0] = x_dest1 + (int) primitiveData[index7 + 1] - num3;
                    firepoint[1] = y_dest1 + (int) primitiveData[index7 + 2] - num4;
                  }
                  if (extraIndex < frameNumPrimitive2 - 1)
                  {
                    for (int index9 = 0; index9 < extraIndex; ++index9)
                    {
                      int primitiveType3 = (int) primitiveData[index6];
                      index6 += AnimationManager.getNumAttributes(primitiveType3);
                    }
                    int num6 = (int) primitiveData[index6 + 1] - num3;
                    int num7 = (int) primitiveData[index6 + 2] - num4;
                    short[] subImage2 = animManData.m_subImages[(int) primitiveData[index6 + 3]];
                    int index10 = (int) subImage2[0] & (int) ushort.MaxValue;
                    Image src2 = animManData.m_animImageArray[animManData.m_curBank][index10];
                    if (src1 != null)
                    {
                      int transform2;
                      if (primitiveType1 == (short) 11)
                      {
                        transform2 = 2;
                        int num8 = (int) subImage2[3];
                        int num9 = (int) subImage1[3];
                        num6 = -num6 - (num8 - num9);
                      }
                      else
                        transform2 = 0;
                      int x_dest2 = num6 + x_dest1;
                      int y_dest2 = num7 + y_dest1;
                      g.drawRegion(src2, (int) subImage2[1], (int) subImage2[2], (int) subImage2[3], (int) subImage2[4], transform2, x_dest2, y_dest2, 9);
                      break;
                    }
                    break;
                  }
                  break;
                }
              }
              goto case 2;
            }
            else
              goto case 2;
          case 2:
          case 7:
            primitiveIndex += AnimationManager.getNumAttributes((int) primitiveType1);
            continue;
          default:
            AnimationManager.drawPrimitive(g, x, y, primitiveIndex, (int) primitiveType1);
            goto case 2;
        }
      }
      return flag;
    }

    private static void drawXFlippedAnimFrame(Graphics g, int animID, int frameID, int x, int y)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      for (int index2 = 0; index2 != frameNumPrimitive; ++index2)
      {
        short primitiveType = animManData.primitiveData[primitiveIndex];
        switch (primitiveType)
        {
          case 0:
          case 11:
            short[] subImage = animManData.m_subImages[(int) animManData.primitiveData[primitiveIndex + 3]];
            int x_dest = x - (int) animManData.primitiveData[primitiveIndex + 1] - (int) subImage[3];
            int y_dest = y + (int) animManData.primitiveData[primitiveIndex + 2];
            int index3 = (int) subImage[0];
            int transform = primitiveType == (short) 11 ? 0 : 2;
            g.drawRegion(animManData.m_animImageArray[animManData.m_curBank][index3], (int) subImage[1], (int) subImage[2], (int) subImage[3], (int) subImage[4], transform, x_dest, y_dest, 9);
            goto case 2;
          case 2:
          case 7:
            primitiveIndex += AnimationManager.getNumAttributes((int) primitiveType);
            continue;
          default:
            AnimationManager.drawXFlippedPrimitive(g, x, y, primitiveIndex, (int) primitiveType);
            goto case 2;
        }
      }
    }

    private static bool drawAnimFrameExt(
      Graphics g,
      int animID,
      int frameID,
      int x,
      int y,
      int extraAnimId,
      int extraIndex,
      int[] firepoint)
    {
      return animID >= 0 && AnimationManager.drawNormalAnimFrameExt(g, animID, frameID, x, y, extraAnimId, extraIndex, firepoint);
    }

    private static bool drawPrimitive(
      Graphics g,
      int x,
      int y,
      int primitiveIndex,
      int primitiveType)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      switch (primitiveType)
      {
        case 1:
          int x1 = x + (int) animManData.primitiveData[primitiveIndex + 1];
          int y1 = y + (int) animManData.primitiveData[primitiveIndex + 2];
          int width1 = (int) animManData.primitiveData[primitiveIndex + 3];
          int height1 = (int) animManData.primitiveData[primitiveIndex + 4];
          g.clipRect(x1, y1, width1, height1);
          break;
        case 3:
        case 4:
        case 5:
        case 6:
          int x2 = x + (int) animManData.primitiveData[primitiveIndex + 1];
          int y2 = y + (int) animManData.primitiveData[primitiveIndex + 2];
          int width2 = (int) animManData.primitiveData[primitiveIndex + 3];
          int height2 = (int) animManData.primitiveData[primitiveIndex + 4];
          switch (primitiveType - 3)
          {
            case 0:
              g.drawRect(x2, y2, width2 - 1, height2 - 1);
              break;
            case 1:
              g.fillRect(x2, y2, width2, height2);
              break;
            case 2:
              g.drawArc(x2, y2, width2, height2, 0, 360);
              break;
            case 3:
              g.fillArc(x2, y2, width2, height2, 0, 360);
              break;
          }
          break;
        case 8:
          int x1_1 = x + (int) animManData.primitiveData[primitiveIndex + 1];
          int y1_1 = y + (int) animManData.primitiveData[primitiveIndex + 2];
          int x2_1 = x + (int) animManData.primitiveData[primitiveIndex + 3];
          int y2_1 = y + (int) animManData.primitiveData[primitiveIndex + 4];
          g.drawLine(x1_1, y1_1, x2_1, y2_1);
          break;
        case 10:
          AnimationManager.setColor(g, (int) animManData.primitiveData[primitiveIndex + 1]);
          break;
        case 12:
          int num1 = x + (int) animManData.primitiveData[1];
          int y1_2 = y + (int) animManData.primitiveData[2];
          int x2_2 = x + (int) animManData.primitiveData[3];
          int num2 = y + (int) animManData.primitiveData[4];
          g.fillTriangle(num1, y1_2, x2_2, num2, num1, num2);
          break;
      }
      return false;
    }

    private static bool drawXFlippedPrimitive(
      Graphics g,
      int x,
      int y,
      int primitiveIndex,
      int primitiveType)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      switch (primitiveType)
      {
        case 3:
        case 4:
        case 5:
        case 6:
          int width = (int) animManData.primitiveData[primitiveIndex + 3];
          int height = (int) animManData.primitiveData[primitiveIndex + 4];
          int x1 = x - ((int) animManData.primitiveData[primitiveIndex + 1] + width);
          int y1 = y + (int) animManData.primitiveData[primitiveIndex + 2];
          switch (primitiveType - 3)
          {
            case 0:
              g.drawRect(x1, y1, width - 1, height - 1);
              break;
            case 1:
              g.fillRect(x1, y1, width, height);
              break;
            case 2:
              g.drawArc(x1, y1, width, height, 0, 360);
              break;
            case 3:
              g.fillArc(x1, y1, width, height, 0, 360);
              break;
          }
          break;
        case 8:
          int x1_1 = x - (int) animManData.primitiveData[primitiveIndex + 1];
          int y1_1 = y + (int) animManData.primitiveData[primitiveIndex + 2];
          int x2_1 = x - (int) animManData.primitiveData[primitiveIndex + 3];
          int y2 = y + (int) animManData.primitiveData[primitiveIndex + 4];
          g.drawLine(x1_1, y1_1, x2_1, y2);
          break;
        case 10:
          AnimationManager.setColor(g, (int) animManData.primitiveData[primitiveIndex + 1]);
          break;
        case 12:
          int num1 = x + (int) animManData.primitiveData[1];
          int y1_2 = y + (int) animManData.primitiveData[2];
          int x2_2 = x + (int) animManData.primitiveData[3];
          int num2 = y + (int) animManData.primitiveData[4];
          g.fillTriangle(num1, y1_2, x2_2, num2, num1, num2);
          break;
      }
      return false;
    }

    public static short getNumAnims() => 3;

    public static int getAnimFrameCount(int animID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      return animID < 0 || animID >= 3 ? -1 : (int) animManData.animNumFrames[animID];
    }

    public static short getAnimFrameDuration(int animID, int frameID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      return animManData.frameDuration[(int) animManData.animFrameOffset[animID] + frameID];
    }

    private static int getPrimitiveX(int primitiveShortIndex)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      switch (animManData.primitiveData[primitiveShortIndex])
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 9:
        case 11:
        case 12:
          return (int) animManData.primitiveData[primitiveShortIndex + 1];
        case 8:
          int num1 = (int) animManData.primitiveData[primitiveShortIndex + 1];
          int num2 = (int) animManData.primitiveData[primitiveShortIndex + 3];
          return num1 >= num2 ? num2 : num1;
        default:
          return 0;
      }
    }

    private static int getPrimitiveY(int primitiveShortIndex)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      switch (animManData.primitiveData[primitiveShortIndex])
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 9:
        case 11:
        case 12:
          return (int) animManData.primitiveData[primitiveShortIndex + 2];
        case 8:
          int num1 = (int) animManData.primitiveData[primitiveShortIndex + 2];
          int num2 = (int) animManData.primitiveData[primitiveShortIndex + 4];
          return num1 >= num2 ? num2 : num1;
        default:
          return 0;
      }
    }

    private static int getPrimitiveWidth(int primitiveShortIndex)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      switch (animManData.primitiveData[primitiveShortIndex])
      {
        case 0:
        case 11:
          int index = (int) animManData.primitiveData[primitiveShortIndex + 3] & (int) ushort.MaxValue;
          return (int) animManData.m_subImages[index][3];
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
          return (int) animManData.primitiveData[primitiveShortIndex + 3];
        case 8:
          int num = (int) animManData.primitiveData[primitiveShortIndex + 1] - (int) animManData.primitiveData[primitiveShortIndex + 3];
          return num >= 0 ? num : -num;
        case 9:
          return 0;
        case 12:
          return (int) animManData.primitiveData[3] - (int) animManData.primitiveData[1];
        default:
          return 0;
      }
    }

    private static int getPrimitiveHeight(int primitiveShortIndex)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      switch (animManData.primitiveData[primitiveShortIndex])
      {
        case 0:
        case 11:
          int index = (int) animManData.primitiveData[primitiveShortIndex + 3] & (int) ushort.MaxValue;
          return (int) animManData.m_subImages[index][4];
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
          return (int) animManData.primitiveData[primitiveShortIndex + 4];
        case 8:
          int num = (int) animManData.primitiveData[primitiveShortIndex + 2] - (int) animManData.primitiveData[primitiveShortIndex + 4];
          return num >= 0 ? num : -num;
        case 9:
          return 0;
        case 12:
          return (int) animManData.primitiveData[4] - (int) animManData.primitiveData[2];
        default:
          return 0;
      }
    }

    public static int getAnimFrameX(int animID, int frameID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveShortIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      if (frameNumPrimitive == 0)
        return 0;
      int animFrameX = int.MaxValue;
      for (int index2 = 0; index2 != frameNumPrimitive; ++index2)
      {
        int primitiveType = (int) animManData.primitiveData[primitiveShortIndex];
        if (primitiveType != 10)
        {
          int primitiveX = AnimationManager.getPrimitiveX(primitiveShortIndex);
          if (primitiveX < animFrameX)
            animFrameX = primitiveX;
        }
        primitiveShortIndex += AnimationManager.getNumAttributes(primitiveType);
      }
      return animFrameX;
    }

    public static int getAnimFrameY(int animID, int frameID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveShortIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      if (frameNumPrimitive == 0)
        return 0;
      int animFrameY = int.MaxValue;
      for (int index2 = 0; index2 != frameNumPrimitive; ++index2)
      {
        int primitiveType = (int) animManData.primitiveData[primitiveShortIndex];
        if (primitiveType != 10)
        {
          int primitiveY = AnimationManager.getPrimitiveY(primitiveShortIndex);
          if (primitiveY < animFrameY)
            animFrameY = primitiveY;
        }
        primitiveShortIndex += AnimationManager.getNumAttributes(primitiveType);
      }
      return animFrameY;
    }

    public static int getAnimFrameWidth(int animID, int frameID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveShortIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      if (frameNumPrimitive == 0)
        return 0;
      int num1 = int.MaxValue;
      int num2 = int.MinValue;
      for (int index2 = 0; index2 != frameNumPrimitive; ++index2)
      {
        int primitiveType = (int) animManData.primitiveData[primitiveShortIndex];
        if (primitiveType != 10)
        {
          int primitiveX = AnimationManager.getPrimitiveX(primitiveShortIndex);
          int num3 = primitiveX + AnimationManager.getPrimitiveWidth(primitiveShortIndex);
          if (primitiveX < num1)
            num1 = primitiveX;
          if (num3 > num2)
            num2 = num3;
        }
        primitiveShortIndex += AnimationManager.getNumAttributes(primitiveType);
      }
      return num2 - num1;
    }

    public static int getAnimFrameHeight(int animID, int frameID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int primitiveShortIndex = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      if (frameNumPrimitive == 0)
        return 0;
      int num1 = int.MaxValue;
      int num2 = int.MinValue;
      for (int index2 = 0; index2 != frameNumPrimitive; ++index2)
      {
        int primitiveType = (int) animManData.primitiveData[primitiveShortIndex];
        if (primitiveType != 10)
        {
          int primitiveY = AnimationManager.getPrimitiveY(primitiveShortIndex);
          int num3 = primitiveY + AnimationManager.getPrimitiveHeight(primitiveShortIndex);
          if (primitiveY < num1)
            num1 = primitiveY;
          if (num3 > num2)
            num2 = num3;
        }
        primitiveShortIndex += AnimationManager.getNumAttributes(primitiveType);
      }
      return num2 - num1;
    }

    public static int getAnimFrameFirePointCount(int animID, int frameID)
    {
      animID &= -16385;
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int index2 = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      int frameFirePointCount = 0;
      for (int index3 = 0; index3 != frameNumPrimitive; ++index3)
      {
        int primitiveType = (int) animManData.primitiveData[index2];
        if (primitiveType == 7)
          ++frameFirePointCount;
        index2 += AnimationManager.getNumAttributes(primitiveType);
      }
      return frameFirePointCount;
    }

    public static bool getAnimFrameFirePoint(
      ref int[] result,
      int animID,
      int frameID,
      int firePointIDConst)
    {
      bool flag = false;
      if ((animID & 16384) != 0)
      {
        animID &= -16385;
        flag = true;
      }
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int num = firePointIDConst;
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int index2 = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      for (int index3 = 0; index3 != frameNumPrimitive; ++index3)
      {
        int primitiveType = (int) animManData.primitiveData[index2];
        if (primitiveType == 7)
        {
          if (num == 0)
          {
            result[0] = (int) animManData.primitiveData[index2 + 1];
            result[1] = (int) animManData.primitiveData[index2 + 2];
            if (flag)
              result[0] = -result[0];
            return true;
          }
          --num;
        }
        index2 += AnimationManager.getNumAttributes(primitiveType);
      }
      result[0] = 0;
      result[1] = 0;
      return false;
    }

    public static bool getAnimFrameFirePoint(
      int[] result,
      int animID,
      int frameID,
      int firePointIDConst)
    {
      bool flag = false;
      if ((animID & 16384) != 0)
      {
        animID &= -16385;
        flag = true;
      }
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int num = firePointIDConst;
      int index1 = (int) animManData.animFrameOffset[animID] + frameID;
      int index2 = animManData.framePrimitiveOffset[index1];
      int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
      for (int index3 = 0; index3 != frameNumPrimitive; ++index3)
      {
        int primitiveType = (int) animManData.primitiveData[index2];
        if (primitiveType == 7)
        {
          if (num == 0)
          {
            result[0] = (int) animManData.primitiveData[index2 + 1];
            result[1] = (int) animManData.primitiveData[index2 + 2];
            if (flag)
              result[0] = -result[0];
            return true;
          }
          --num;
        }
        index2 += AnimationManager.getNumAttributes(primitiveType);
      }
      result[0] = 0;
      result[1] = 0;
      return false;
    }

    public static bool getAnimFrameCollisionBox(
      ref int[] result,
      int animID,
      int frameID,
      int boxID)
    {
      bool flag = false;
      if ((animID & 16384) != 0)
      {
        animID &= -16385;
        flag = true;
      }
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      if (animID != -1)
      {
        int index1 = (int) animManData.animFrameOffset[animID] + frameID;
        int index2 = animManData.framePrimitiveOffset[index1];
        int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
        int num1 = 0;
        for (int index3 = 0; index3 != frameNumPrimitive; ++index3)
        {
          int primitiveType = (int) animManData.primitiveData[index2];
          if (primitiveType == 2)
          {
            if (num1 == boxID)
            {
              result[0] = (int) animManData.primitiveData[index2 + 1];
              result[1] = (int) animManData.primitiveData[index2 + 2];
              result[2] = (int) animManData.primitiveData[index2 + 3];
              result[3] = (int) animManData.primitiveData[index2 + 4];
              int num2 = flag ? 1 : 0;
              return true;
            }
            ++num1;
          }
          index2 += AnimationManager.getNumAttributes(primitiveType);
        }
      }
      result[0] = 0;
      result[1] = 0;
      result[2] = 0;
      result[3] = 0;
      return false;
    }

    public static bool getAnimFrameCollisionBox(int[] result, int animID, int frameID, int boxID)
    {
      bool flag = false;
      if ((animID & 16384) != 0)
      {
        animID &= -16385;
        flag = true;
      }
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      if (animID != -1)
      {
        int index1 = (int) animManData.animFrameOffset[animID] + frameID;
        int index2 = animManData.framePrimitiveOffset[index1];
        int frameNumPrimitive = (int) animManData.frameNumPrimitives[index1];
        int num1 = 0;
        for (int index3 = 0; index3 != frameNumPrimitive; ++index3)
        {
          int primitiveType = (int) animManData.primitiveData[index2];
          if (primitiveType == 2)
          {
            if (num1 == boxID)
            {
              result[0] = (int) animManData.primitiveData[index2 + 1];
              result[1] = (int) animManData.primitiveData[index2 + 2];
              result[2] = (int) animManData.primitiveData[index2 + 3];
              result[3] = (int) animManData.primitiveData[index2 + 4];
              int num2 = flag ? 1 : 0;
              return true;
            }
            ++num1;
          }
          index2 += AnimationManager.getNumAttributes(primitiveType);
        }
      }
      result[0] = 0;
      result[1] = 0;
      result[2] = 0;
      result[3] = 0;
      return false;
    }

    public static Image getImage(int imageIndex)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      return imageIndex < 0 || imageIndex >= 1 ? (Image) null : animManData.m_animImageArray[animManData.m_curBank][imageIndex];
    }

    public static void drawAnim(Graphics g, AnimPlayer p, int x, int y)
    {
      AnimationManager.drawAnimFrame(g, p.getAnimID(), p.getCurrAnimFrame(), x, y);
    }

    public static bool startAnimReverse(int animID, int bitFlag)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = -1;
      for (int index2 = 0; index2 < 48; ++index2)
      {
        if (animManData.m_animPlayerPool[index2].isAnimating())
        {
          if (animManData.m_animPlayerPool[index2].getAnimID() == animID)
            return true;
        }
        else if (index1 < 0)
          index1 = index2;
      }
      if (index1 < 0)
        return false;
      animManData.m_animPlayerPool[index1].startAnim(animID, bitFlag);
      animManData.m_animPlayerPool[index1].setReverse(true);
      return true;
    }

    public static bool startAnim(int animID, int bitFlag)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      int index1 = -1;
      for (int index2 = 0; index2 < 48; ++index2)
      {
        if (animManData.m_animPlayerPool[index2].isAnimating())
        {
          if (animManData.m_animPlayerPool[index2].getAnimID() == animID)
            return true;
        }
        else if (index1 < 0)
          index1 = index2;
      }
      if (index1 < 0)
        return false;
      animManData.m_animPlayerPool[index1].startAnim(animID, bitFlag);
      animManData.m_animPlayerPool[index1].setReverse(false);
      return true;
    }

    public static void updateAnims(int timeStep)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      for (int index = 0; index < 48; ++index)
        animManData.m_animPlayerPool[index].updateAnim(timeStep);
    }

    public static bool drawAnim(Graphics g, int animID, int x, int y)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      for (int index = 0; index < 48; ++index)
      {
        AnimPlayer animPlayer = animManData.m_animPlayerPool[index];
        if (animPlayer.getAnimID() == animID)
        {
          AnimationManager.drawAnimFrame(g, animID, animPlayer.getCurrAnimFrame(), x, y);
          return true;
        }
      }
      return false;
    }

    public static bool drawAnim(Graphics g, ref AnimPlayer p, int x, int y)
    {
      if (p.getAnimID() < 0)
        return false;
      AnimationManager.drawAnimFrame(g, p.getAnimID(), p.getCurrAnimFrame(), x, y);
      return true;
    }

    public static void stopAnim(int animID)
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      for (int index = 0; index < 48; ++index)
      {
        if (animManData.m_animPlayerPool[index].getAnimID() == animID)
          animManData.m_animPlayerPool[index].setAnimating(false);
      }
    }

    public static void stopAllAnims()
    {
      AnimationManagerData animManData = AppEngine.getCanvas().getAnimManData();
      for (int index = 0; index < 48; ++index)
        animManData.m_animPlayerPool[index].setAnimating(false);
    }

    public static void setBank(int bank) => AppEngine.getCanvas().getAnimManData().m_curBank = bank;
  }
}
