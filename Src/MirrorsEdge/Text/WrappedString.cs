// Decompiled with JetBrains decompiler
// Type: text.WrappedString
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using System;

#nullable disable
namespace text
{
  public class WrappedString
  {
    private int MAX_WRAP_OFFSETS = (int) byte.MaxValue;
    public int m_fontId;
    private short[] m_wrapOffsetArray;
    private short[] m_wrapLengthArray;
    private int m_numWrappedLines;
    private string m_wrapString;
    private bool m_forMenu;
    private int m_LineWidth;

    public WrappedString()
    {
      this.m_fontId = 0;
      this.m_wrapOffsetArray = new short[this.MAX_WRAP_OFFSETS];
      this.m_wrapLengthArray = new short[this.MAX_WRAP_OFFSETS];
      this.m_numWrappedLines = 0;
      this.m_wrapString = (string) null;
    }

    public WrappedString(WrappedString other)
    {
      this.m_fontId = other.m_fontId;
      Array.Copy((Array) other.m_wrapOffsetArray, (Array) this.m_wrapOffsetArray, other.m_wrapOffsetArray.Length);
      Array.Copy((Array) other.m_wrapLengthArray, (Array) this.m_wrapLengthArray, other.m_wrapLengthArray.Length);
      this.m_numWrappedLines = other.m_numWrappedLines;
      this.m_wrapString = new string(other.m_wrapString.ToCharArray(), 0, other.m_wrapString.Length);
    }

    public virtual void Destructor()
    {
      this.m_wrapOffsetArray = (short[]) null;
      this.m_wrapLengthArray = (short[]) null;
      this.m_wrapString = (string) null;
    }

    public void clearString()
    {
      this.m_numWrappedLines = 0;
      this.m_wrapString = (string) null;
    }

    public void wrapSubString(
      string str,
      int fontId,
      int lineWidth,
      int startCharIndex,
      int numChars,
      bool forMenu)
    {
      this.m_forMenu = forMenu;
      this.m_LineWidth = lineWidth;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_wrapString = str;
      this.m_fontId = fontId;
      StringRenderer stringRenderer = textManager.getStringRenderer(fontId);
      int num1 = startCharIndex + numChars;
      int offset = startCharIndex;
      int num2 = offset - 1;
      int num3 = -1;
      bool flag = false;
      this.m_numWrappedLines = 0;
      for (int index = startCharIndex; index != num1; ++index)
      {
        char ch = str[index];
        switch (ch)
        {
          case '\n':
          case '|':
            if (num3 == -1)
            {
              this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
              this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (num2 - offset + 1);
            }
            else
            {
              this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
              this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (index - offset);
            }
            ++this.m_numWrappedLines;
            offset = index + 1;
            num2 = offset - 1;
            num3 = -1;
            flag = false;
            break;
          case ' ':
            if (index != offset && num3 != -1)
            {
              num2 = index - 1;
              num3 = -1;
              flag = true;
              break;
            }
            break;
          default:
            if (lineWidth < stringRenderer.substringWidth(str, offset, index - offset + 1) / Runtime.pixelScale)
            {
              if (!forMenu || flag)
              {
                if (num3 == -1)
                {
                  this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
                  this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (num2 - offset + 1);
                  offset = index;
                  num2 = offset;
                  num3 = offset;
                }
                else if (num3 == offset)
                {
                  this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
                  this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (index - offset);
                  offset = index;
                  num2 = offset;
                  num3 = offset;
                }
                else
                {
                  this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
                  this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (num2 - offset + 1);
                  offset = num3;
                  num2 = offset;
                  num3 = offset;
                }
                ++this.m_numWrappedLines;
                flag = false;
                break;
              }
              break;
            }
            if (ch == '-' && index != offset)
            {
              num2 = index;
              num3 = -1;
              flag = false;
              break;
            }
            if (num3 == -1)
            {
              num3 = index;
              break;
            }
            break;
        }
      }
      if (num3 != -1)
      {
        this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
        this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (num1 - offset);
        ++this.m_numWrappedLines;
      }
      else
      {
        if (offset == num2)
          return;
        this.m_wrapOffsetArray[this.m_numWrappedLines] = (short) offset;
        this.m_wrapLengthArray[this.m_numWrappedLines] = (short) (num2 - offset + 1);
        ++this.m_numWrappedLines;
      }
    }

    public void wrapString(string str, int fontId, int lineWidth, bool forMenu)
    {
      this.wrapSubString(str, fontId, lineWidth, 0, str.Length, forMenu);
    }

    public void wrapString(int stringId, int fontId, int lineWidth, bool forMenu)
    {
      string str = AppEngine.getCanvas().getTextManager().getString(stringId);
      this.wrapSubString(str, fontId, lineWidth, 0, str.Length, forMenu);
    }

    public void draw(Graphics g, int x, int y, int anchor)
    {
      this.drawLinesWithFont(g, this.m_fontId, x, y, anchor, 0, this.m_numWrappedLines, false, this.m_fontId);
    }

    public void drawWithFont(
      Graphics g,
      int fontId,
      int x,
      int y,
      int anchor,
      bool inverseColors,
      int m_fontId1)
    {
      this.drawLinesWithFont(g, fontId, x, y, anchor, 0, this.m_numWrappedLines, inverseColors, m_fontId1);
    }

    public void drawLines(Graphics g, int x, int y, int anchor, int startLine, int numLines)
    {
      this.drawLinesWithFont(g, this.m_fontId, x, y, anchor, startLine, numLines, false, this.m_fontId);
    }

    public void drawLinesWithFont(
      Graphics g,
      int fontId,
      int x,
      int y,
      int anchor,
      int startLine,
      int numLines,
      bool inverseColors,
      int fontId1)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      StringRenderer stringRenderer1 = textManager.getStringRenderer(fontId);
      int clipY = g.getClipY();
      int num1 = clipY + g.getClipHeight();
      int y1 = y;
      int anchor1 = anchor;
      int num2 = stringRenderer1.getHeight() / Runtime.pixelScale;
      int num3 = stringRenderer1.getLeading() / Runtime.pixelScale;
      int num4 = num2;
      int num5 = startLine + numLines;
      if ((anchor & 16) != 0)
      {
        y1 -= numLines * num2 - num3 >> 1;
        anchor1 = anchor1 & -17 | 8;
      }
      else if ((anchor & 32) != 0)
      {
        y1 -= numLines * num2 - num3;
        anchor1 = anchor1 & -33 | 8;
      }
      for (int index = startLine; index != num5; ++index)
      {
        int wrapOffset = (int) this.m_wrapOffsetArray[index];
        int wrapLength = (int) this.m_wrapLengthArray[index];
        if (0 < wrapLength && y1 < num1 && clipY < y1 + num4)
        {
          if (fontId != fontId1)
          {
            if (this.m_forMenu && textManager.getSubStringWidth(this.m_wrapString, wrapOffset, wrapLength, fontId) > this.m_LineWidth)
            {
              fontId = 15;
              if (textManager.getSubStringWidth(this.m_wrapString, wrapOffset, wrapLength, fontId) >= this.m_LineWidth)
              {
                fontId = 18;
                if (textManager.getSubStringWidth(this.m_wrapString, wrapOffset, wrapLength, fontId) >= this.m_LineWidth)
                  fontId = 5;
              }
            }
            if (inverseColors)
            {
              StringRenderer stringRenderer2 = textManager.getStringRenderer(fontId);
              int color = stringRenderer2.getColor();
              stringRenderer2.setColor(8421504);
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x + 1, y1 + 1, anchor1);
              stringRenderer2.setColor(0);
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x, y1, anchor1);
              stringRenderer2.setColor(color);
            }
            else if (this.m_forMenu)
            {
              StringRenderer stringRenderer3 = textManager.getStringRenderer(fontId);
              int color = stringRenderer3.getColor();
              stringRenderer3.setColor(0);
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x + 1, y1 + 1, anchor1);
              stringRenderer3.setColor(16777215);
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x, y1, anchor1);
              stringRenderer3.setColor(color);
            }
            else
            {
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId1, x + 1, y1 + 1, anchor1);
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x, y1, anchor1);
            }
          }
          else
          {
            StringRenderer stringRenderer4 = textManager.getStringRenderer(fontId);
            int color = stringRenderer4.getColor();
            if (inverseColors)
            {
              stringRenderer4.setColor(8421504);
              textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x + 1, y1 + 1, anchor1);
              stringRenderer4.setColor(0);
            }
            textManager.drawSubString(g, this.m_wrapString, wrapOffset, wrapLength, fontId, x, y1, anchor1);
            stringRenderer4.setColor(color);
          }
        }
        y1 += num4;
      }
    }

    public int getNumWrappedLines() => this.m_numWrappedLines;

    public int getWrappedTextHeight()
    {
      StringRenderer stringRenderer = AppEngine.getCanvas().getTextManager().getStringRenderer(this.m_fontId);
      return (this.m_numWrappedLines * stringRenderer.getHeight() - stringRenderer.getLeading()) / Runtime.pixelScale;
    }

    public int getLinesInHeight(int height)
    {
      StringRenderer stringRenderer = AppEngine.getCanvas().getTextManager().getStringRenderer(this.m_fontId);
      return (height - stringRenderer.getLeading()) / stringRenderer.getHeight() / Runtime.pixelScale;
    }

    public int length() => this.m_wrapString.Length;
  }
}
