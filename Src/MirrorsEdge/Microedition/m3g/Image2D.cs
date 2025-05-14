
// Type: microedition.m3g.Image2D
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using midp;
using mirrorsedge_wp7;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace microedition.m3g
{
  public class Image2D : Object3D
  {
    public const int ALPHA = 96;
    public const int LUMINANCE = 97;
    public const int LUMINANCE_ALPHA = 98;
    public const int RGB = 99;
    public const int RGBA = 100;
    public const int RGB565 = 101;
    public const int RGBA5551 = 102;
    public const int RGBA4444 = 103;
    public const int RGB_ATITC = 120;
    public const int RGBA_ATITC = 121;
    public const int RGB_PVRTC_2BPP = 122;
    public const int RGBA_PVRTC_2BPP = 123;
    public const int RGB_PVRTC_4BPP = 124;
    public const int RGBA_PVRTC_4BPP = 125;
    public const int NO_MIPMAPS = 32768;
    public new const int M3G_UNIQUE_CLASS_ID = 10;
    private int m_Format;
    private int m_Width;
    private int m_Height;
    private bool m_Mutable;
    private int m_BitsPerPixel;
    private int m_NumMipMaps;
    private int m_MipMapDataLength;
    public Microsoft.Xna.Framework.Graphics.Texture2D texture2d;
    private ContentManager contentManager;
    public static byte[] data = (byte[]) null;
    public static Dictionary<string, Microsoft.Xna.Framework.Graphics.Texture2D> m_TexturesCache = new Dictionary<string, Microsoft.Xna.Framework.Graphics.Texture2D>();
    private static StringBuilder sb = new StringBuilder(256);
    public string m_ResName = "";

    public override void Destructor() => base.Destructor();

    ~Image2D()
    {
      this.texture2d = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      if (this.contentManager == null)
        return;
      this.contentManager.Unload();
      this.contentManager = (ContentManager) null;
    }

    public Image2D()
    {
      this.m_Format = 0;
      this.m_Width = 0;
      this.m_Height = 0;
      this.m_Mutable = true;
      this.m_BitsPerPixel = 0;
      this.m_NumMipMaps = 0;
      this.m_MipMapDataLength = 0;
      this.texture2d = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
    }

    public Image2D(int format, int width, int height)
    {
      this.m_Format = 0;
      this.m_Width = 0;
      this.m_Height = 0;
      this.m_Mutable = true;
      this.m_BitsPerPixel = 0;
      this.m_NumMipMaps = 0;
      this.m_MipMapDataLength = 0;
      this.set(format, width, height);
      this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, width, height);
    }

    public void CreateGrayTargetBitmap()
    {
      this.texture2d = (Microsoft.Xna.Framework.Graphics.Texture2D) new RenderTarget2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, false, SurfaceFormat.Alpha8, DepthFormat.None, 1, RenderTargetUsage.PreserveContents);
    }

    public void CreateColorTargetBitmap(int depth)
    {
      this.texture2d = (Microsoft.Xna.Framework.Graphics.Texture2D) new RenderTarget2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, false, SurfaceFormat.Color, DepthFormat.Depth16, 1, RenderTargetUsage.PreserveContents);
    }

    public Image2D(int format, int width, int height, byte[] image)
    {
      this.m_Format = 0;
      this.m_Width = 0;
      this.m_Height = 0;
      this.m_Mutable = true;
      this.m_BitsPerPixel = 0;
      this.m_NumMipMaps = 0;
      this.m_MipMapDataLength = 0;
      this.set(format, width, height);
      this.commit(image, 52);
    }

    public Image2D(int format, Image image)
    {
      this.m_Format = 0;
      this.m_Width = 0;
      this.m_Height = 0;
      this.m_Mutable = true;
      this.m_BitsPerPixel = 0;
      this.m_NumMipMaps = 0;
      this.m_MipMapDataLength = 0;
      this.set(format, image);
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      Image2D image2D = (Image2D) ret;
      image2D.m_Format = this.m_Format;
      image2D.m_Width = this.m_Width;
      image2D.m_Height = this.m_Height;
      image2D.m_Mutable = this.m_Mutable;
      image2D.m_BitsPerPixel = this.m_BitsPerPixel;
      image2D.texture2d = this.texture2d;
    }

    private static void requirePowerOf2(int dim)
    {
    }

    private static int getFormatBitsPerPixel(int format)
    {
      int formatBitsPerPixel = 0;
      switch (format & (int) byte.MaxValue)
      {
        case 96:
        case 97:
          formatBitsPerPixel = 8;
          break;
        case 98:
          formatBitsPerPixel = 16;
          break;
        case 99:
          formatBitsPerPixel = 24;
          break;
        case 100:
          formatBitsPerPixel = 32;
          break;
        case 101:
          formatBitsPerPixel = 16;
          break;
        case 102:
          formatBitsPerPixel = 16;
          break;
        case 103:
          formatBitsPerPixel = 16;
          break;
        case 120:
          formatBitsPerPixel = 4;
          break;
        case 121:
          formatBitsPerPixel = 8;
          break;
        case 122:
        case 123:
          formatBitsPerPixel = 2;
          break;
        case 124:
        case 125:
          formatBitsPerPixel = 4;
          break;
      }
      return formatBitsPerPixel;
    }

    private static int getMipMapLevelsForSize(int width, int height)
    {
      Image2D.requirePowerOf2(width);
      Image2D.requirePowerOf2(height);
      int mapLevelsForSize = 0;
      while (true)
      {
        int num = 1 << mapLevelsForSize;
        if (num <= width || num <= height)
          ++mapLevelsForSize;
        else
          break;
      }
      return mapLevelsForSize;
    }

    private static int getDataSizeForFormat(int format, int width, int height)
    {
      int formatBitsPerPixel = Image2D.getFormatBitsPerPixel(format);
      int dataSizeForFormat = (width * height * formatBitsPerPixel + 7) / 8;
      switch (format & (int) byte.MaxValue)
      {
        case 120:
          if (dataSizeForFormat < 8)
          {
            dataSizeForFormat = 8;
            break;
          }
          break;
        case 121:
          if (dataSizeForFormat < 16)
          {
            dataSizeForFormat = 16;
            break;
          }
          break;
        case 122:
        case 123:
        case 124:
        case 125:
          if (dataSizeForFormat < 32)
          {
            dataSizeForFormat = 32;
            break;
          }
          break;
      }
      return dataSizeForFormat;
    }

    public static bool isCompressedFormat(int format)
    {
      switch (format & (int) byte.MaxValue)
      {
        case 122:
        case 123:
        case 124:
        case 125:
          return true;
        default:
          return false;
      }
    }

    private static bool shouldHaveMipMaps(int format)
    {
      switch (format)
      {
        case 122:
        case 123:
        case 124:
        case 125:
          if ((format & 32768) == 0)
            return true;
          break;
      }
      return false;
    }

    public void set(int format, int width, int height)
    {
      Image2D.requirePowerOf2(width);
      Image2D.requirePowerOf2(height);
      bool flag = Image2D.shouldHaveMipMaps(format);
      if (!flag)
        format |= 32768;
      this.m_Format = format;
      this.m_Width = width;
      this.m_Height = height;
      this.m_NumMipMaps = flag ? Image2D.getMipMapLevelsForSize(width, height) : 1;
      this.m_MipMapDataLength = this.m_NumMipMaps;
      this.m_BitsPerPixel = Image2D.getFormatBitsPerPixel(format);
    }

    public void set(int format, Image image)
    {
      int width = image.getWidth();
      int height = image.getHeight();
      this.set(format, width, height);
      int length = width * height;
      int[] rgbData = new int[length];
      image.getRGB(ref rgbData, 0, width, 0, 0, width, height);
      int[] numArray = new int[length];
      for (int index = 0; index < height; ++index)
        Array.Copy((Array) rgbData, index * width, (Array) numArray, (height - index - 1) * width, width);
      this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, width, height);
      this.texture2d.SetData<int>(numArray);
    }

    public void copyData(Image2D image)
    {
      this.Discard();
      this.set(this.m_Format, this.m_Width, this.m_Height);
      this.texture2d = image.texture2d;
      this.m_Mutable = false;
    }

    public void commit(byte[] palette, byte[] indices)
    {
      if (palette == null || palette.Length == 0)
        return;
      switch (this.m_Format & (int) byte.MaxValue)
      {
        case 99:
          this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, this.getNumMipMapLevels() > 1, SurfaceFormat.Color);
          Color[] data1 = new Color[this.m_Width * this.m_Height];
          for (int level = 0; level < this.getNumMipMapLevels(); ++level)
          {
            int mipMapWidth = this.getMipMapWidth(level);
            int mipMapHeight = this.getMipMapHeight(level);
            for (int index1 = 0; index1 < mipMapHeight; ++index1)
            {
              for (int index2 = 0; index2 < mipMapWidth; ++index2)
              {
                int num1 = 0;
                int num2 = 0;
                int num3 = 0;
                for (int index3 = 0; index3 < 1 << level; ++index3)
                {
                  for (int index4 = 0; index4 < 1 << level; ++index4)
                  {
                    uint index5 = (uint) (((index1 << level) + index3) * this.m_Width + (index2 << level) + index4);
                    uint index6 = (uint) (((int) indices[(int) index5] & (int) byte.MaxValue) * 3);
                    num1 += (int) palette[(int) index6] & (int) byte.MaxValue;
                    num2 += (int) palette[(int) (index6 + 1U)] & (int) byte.MaxValue;
                    num3 += (int) palette[(int) (index6 + 2U)] & (int) byte.MaxValue;
                  }
                }
                int r = num1 >> level * 2;
                int g = num2 >> level * 2;
                int b = num3 >> level * 2;
                data1[index1 * mipMapWidth + index2] = new Color(r, g, b);
              }
              this.texture2d.SetData<Color>(level, new Rectangle?(new Rectangle(0, 0, mipMapWidth, mipMapHeight)), data1, 0, data1.Length);
            }
          }
          break;
        case 100:
          this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, this.getNumMipMapLevels() > 1, SurfaceFormat.Color);
          Color[] data2 = new Color[this.m_Width * this.m_Height];
          for (int level = 0; level < this.getNumMipMapLevels(); ++level)
          {
            int mipMapWidth = this.getMipMapWidth(level);
            int mipMapHeight = this.getMipMapHeight(level);
            for (int index7 = 0; index7 < mipMapHeight; ++index7)
            {
              for (int index8 = 0; index8 < mipMapWidth; ++index8)
              {
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                for (int index9 = 0; index9 < 1 << level; ++index9)
                {
                  for (int index10 = 0; index10 < 1 << level; ++index10)
                  {
                    uint index11 = (uint) (((index7 << level) + index9) * this.m_Width + (index8 << level) + index10);
                    uint index12 = (uint) (((int) indices[(int) index11] & (int) byte.MaxValue) * 4);
                    num4 += (int) palette[(int) index12] & (int) byte.MaxValue;
                    num5 += (int) palette[(int) (index12 + 1U)] & (int) byte.MaxValue;
                    num6 += (int) palette[(int) (index12 + 2U)] & (int) byte.MaxValue;
                    num7 += (int) palette[(int) (index12 + 3U)] & (int) byte.MaxValue;
                  }
                }
                int r = num4 >> level * 2;
                int g = num5 >> level * 2;
                int b = num6 >> level * 2;
                int a = num7 >> level * 2;
                data2[index7 * mipMapWidth + index8] = new Color(r, g, b, a);
              }
            }
            this.texture2d.SetData<Color>(level, new Rectangle?(new Rectangle(0, 0, mipMapWidth, mipMapHeight)), data2, 0, data2.Length);
          }
          break;
      }
      this.m_Mutable = false;
    }

    private void encodeUInt32LE(uint value, sbyte[] data)
    {
      data[0] = (sbyte) ((int) value & (int) byte.MaxValue);
      data[1] = (sbyte) ((int) (value >> 8) & (int) byte.MaxValue);
      data[2] = (sbyte) ((int) (value >> 16) & (int) byte.MaxValue);
      data[3] = (sbyte) ((int) (value >> 24) & (int) byte.MaxValue);
    }

    private uint decodeUInt32LE(sbyte[] data)
    {
      return (uint) ((int) data[0] & (int) byte.MaxValue | ((int) data[1] & (int) byte.MaxValue) << 8 | ((int) data[2] & (int) byte.MaxValue) << 16 | ((int) data[3] & (int) byte.MaxValue) << 24);
    }

    public bool canCommit()
    {
      switch (this.m_Format & (int) byte.MaxValue)
      {
        case 99:
        case 100:
          return true;
        default:
          return false;
      }
    }

    public static void clearImage2DLoader()
    {
      Image2D.data = (byte[]) null;
      Image2D.m_TexturesCache.Clear();
    }

    public void commit(BinaryReader in_, int pixelSize, string path, int index)
    {
      Image2D.sb.Length = 0;
      Image2D.sb.Append(path);
      Image2D.sb.Append('#');
      Image2D.sb.Append(index);
      string key = Image2D.sb.ToString();
      if (Image2D.m_TexturesCache.TryGetValue(key, out this.texture2d))
        return;
      switch (this.m_Format & (int) byte.MaxValue)
      {
        case 99:
          this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, false, SurfaceFormat.Color);
          Color[] data = new Color[this.m_Width * this.m_Height];
          for (int index1 = 0; index1 < this.m_Height; ++index1)
          {
            for (int index2 = 0; index2 < this.m_Width; ++index2)
            {
              int r = (int) in_.ReadByte();
              int g = (int) in_.ReadByte();
              int b = (int) in_.ReadByte();
              data[index1 * this.m_Width + index2] = new Color(r, g, b);
            }
          }
          this.texture2d.SetData<Color>(data, 0, data.Length);
          break;
        case 100:
          this.texture2d = pixelSize != this.m_Width * this.m_Height ? new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, false, SurfaceFormat.Color) : new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, false, SurfaceFormat.Dxt5);
          if (pixelSize <= 1048576)
          {
            if (Image2D.data == null)
              Image2D.data = new byte[1048576];
            in_.Read(Image2D.data, 0, pixelSize);
            this.texture2d.SetData<byte>(Image2D.data, 0, pixelSize);
            break;
          }
          byte[] numArray = new byte[pixelSize];
          in_.Read(numArray, 0, pixelSize);
          this.texture2d.SetData<byte>(numArray, 0, pixelSize);
          break;
      }
      Image2D.m_TexturesCache.Add(key, this.texture2d);
    }

    public void commit(byte[] pixels, int index)
    {
      if (pixels == null || pixels.Length == 0)
        return;
      switch (this.m_Format & (int) byte.MaxValue)
      {
        case 99:
          this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, this.getNumMipMapLevels() > 1, SurfaceFormat.Color);
          int num1 = 0;
          int num2 = 0;
          Color[] data1 = new Color[this.m_Width * this.m_Height];
          for (int level = 0; level < this.getNumMipMapLevels(); ++level)
          {
            int mipMapWidth = this.getMipMapWidth(level);
            int mipMapHeight = this.getMipMapHeight(level);
            if (level > 0)
            {
              if (this.getMipMapHeight(level - 1) != mipMapHeight)
                num1 = level;
              if (this.getMipMapWidth(level - 1) != mipMapWidth)
                num2 = level;
            }
            for (int index1 = 0; index1 < mipMapHeight; ++index1)
            {
              int num3 = index1 << num1;
              for (int index2 = 0; index2 < mipMapWidth; ++index2)
              {
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                int num7 = index2 << num2;
                for (int index3 = 0; index3 < 1 << num1; ++index3)
                {
                  int num8 = (num3 + index3) * this.m_Width;
                  for (int index4 = 0; index4 < 1 << num2; ++index4)
                  {
                    int index5 = (num8 + num7 + index4) * 3 + index;
                    num4 += (int) pixels[index5];
                    num5 += (int) pixels[index5 + 1];
                    num6 += (int) pixels[index5 + 2];
                  }
                }
                int r = num4 >> num1 + num2;
                int g = num5 >> num1 + num2;
                int b = num6 >> num1 + num2;
                data1[index1 * mipMapWidth + index2] = new Color(r, g, b);
              }
            }
            this.texture2d.SetData<Color>(level, new Rectangle?(new Rectangle(0, 0, mipMapWidth, mipMapHeight)), data1, 0, data1.Length);
          }
          break;
        case 100:
          this.texture2d = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, this.m_Width, this.m_Height, this.getNumMipMapLevels() > 1, SurfaceFormat.Color);
          int num9 = 0;
          int num10 = 0;
          Color[] data2 = new Color[this.m_Width * this.m_Height];
          for (int level = 0; level < this.getNumMipMapLevels(); ++level)
          {
            int mipMapWidth = this.getMipMapWidth(level);
            int mipMapHeight = this.getMipMapHeight(level);
            if (level > 0)
            {
              if (this.getMipMapHeight(level - 1) != mipMapHeight)
                num9 = level;
              if (this.getMipMapWidth(level - 1) != mipMapWidth)
                num10 = level;
            }
            for (int index6 = 0; index6 < mipMapHeight; ++index6)
            {
              for (int index7 = 0; index7 < mipMapWidth; ++index7)
              {
                int num11 = 0;
                int num12 = 0;
                int num13 = 0;
                int num14 = 0;
                for (int index8 = 0; index8 < 1 << num9; ++index8)
                {
                  for (int index9 = 0; index9 < 1 << num10; ++index9)
                  {
                    int index10 = (((index6 << num9) + index8) * this.m_Width + (index7 << num10) + index9) * 4 + index;
                    num11 += (int) pixels[index10];
                    num12 += (int) pixels[index10 + 1];
                    num13 += (int) pixels[index10 + 2];
                    num14 += (int) pixels[index10 + 3];
                  }
                }
                int r = num11 >> num9 + num10;
                int g = num12 >> num9 + num10;
                int b = num13 >> num9 + num10;
                int a = num14 >> num9 + num10;
                data2[index6 * mipMapWidth + index7] = new Color(r, g, b, a);
              }
            }
            this.texture2d.SetData<Color>(level, new Rectangle?(new Rectangle(0, 0, mipMapWidth, mipMapHeight)), data2, 0, data2.Length);
          }
          break;
      }
      this.m_Mutable = false;
    }

    public int getNumMipMapLevels() => this.m_NumMipMaps;

    protected int getMipMapDataSize(int level)
    {
      if (level >= this.m_MipMapDataLength)
        level = this.m_MipMapDataLength - 1;
      return Image2D.getDataSizeForFormat(this.m_Format, this.getMipMapWidth(level), this.getMipMapHeight(level));
    }

    protected int getMipMapWidth(int level)
    {
      int mipMapWidth = this.m_Width >> level;
      if (mipMapWidth == 0)
        mipMapWidth = 1;
      return mipMapWidth;
    }

    protected int getMipMapHeight(int level)
    {
      int mipMapHeight = this.m_Height >> level;
      if (mipMapHeight == 0)
        mipMapHeight = 1;
      return mipMapHeight;
    }

    protected void Discard()
    {
      this.texture2d = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      this.m_MipMapDataLength = 0;
    }

    public int getFormat() => this.m_Format;

    public int getWidth() => this.m_Width;

    public int getHeight() => this.m_Height;

    public int getBytesPerPixel()
    {
      return Image2D.getFormatBitsPerPixel(this.m_Format & (int) byte.MaxValue) >> 3;
    }

    public bool isMutable() => this.m_Mutable;

    public void loadTexture(string name)
    {
      if (this.texture2d != null)
        return;
      name = name.Substring(0, name.Length - 4);
      this.contentManager = new ContentManager((IServiceProvider) MirrorsEdge.m_MirrorsEdge.Services, "Content");
      this.m_ResName = name;
      try
      {
        this.texture2d = this.contentManager.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(name);
      }
      catch (System.Exception ex)
      {
      }
    }

    public override int getM3GUniqueClassID() => 10;

    public static Image2D m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 10 ? (Image2D) obj : (Image2D) null;
    }
  }
}
