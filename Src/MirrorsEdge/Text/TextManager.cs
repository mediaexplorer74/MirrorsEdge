
// Type: text.TextManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;
using SharpDX;
using System;
using System.Collections.Generic;

#nullable disable
namespace text
{
  public class TextManager : StringConstants
  {
    public const int DYNAMIC_STRING_COUNT = 10;
    public const int DYNAMIC_STRING_LAST = -2;
    public const int DYNAMIC_STRING_FIRST = -12;
    public const int STRING_BUFFER_SIZE = 100;
    public const int MAX_STRING_BUFFERS = 25;
    private List<StringRenderer> m_EFIGS_stringRenderers;
    private List<StringRenderer> m_Other_stringRenderers;
    private string[] m_dynamicStrings;
    private string[] m_replaceStrings;
    private StringBuffer m_tempStringBuffer;
    private StringBuffer[] m_tempStringBuffers;
    private int m_tempBufferIndex;
    private static int[] millisDigits = new int[3];
    private WrappedString m_wrappedString;

    public TextManager()
    {
      this.m_EFIGS_stringRenderers = new List<StringRenderer>();
      this.m_Other_stringRenderers = new List<StringRenderer>();
      this.m_dynamicStrings = new string[10];
      this.m_replaceStrings = new string[3];
      this.m_tempStringBuffer = (StringBuffer) null;
      this.m_tempStringBuffers = (StringBuffer[]) null;
      this.m_tempBufferIndex = 0;
      this.m_wrappedString = new WrappedString();
    }

    public void Destructor()
    {
      this.m_EFIGS_stringRenderers = (List<StringRenderer>) null;
      this.m_Other_stringRenderers = (List<StringRenderer>) null;
      this.m_dynamicStrings = (string[]) null;
      this.m_replaceStrings = (string[]) null;
      this.m_tempStringBuffer = (StringBuffer) null;
      this.m_tempStringBuffers = (StringBuffer[]) null;
      if (this.m_wrappedString == null)
        return;
      this.m_wrappedString.Destructor();
      this.m_wrappedString = (WrappedString) null;
    }

    public void init()
    {
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      LocaleManager instance = LocaleManager.getInstance();
      // ISSUE: reference to a compiler-generated method
      instance.setStringIdBits(11);
      this.initStringBuffers();
      this.initFonts();
      this.initStringImage();
    }

    public string getString(int stringid)
    {
        string str = "";
        if (stringid < -2)
        {
            str = this.m_dynamicStrings[stringid - -12];
        }
        else
        {
            str = LocaleManager.getInstance().getString(stringid);
        }
        return str;
    }

    public string getLangString(int languageIndex)
    {
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      return languageIndex < 5 ? LocaleManager.getInstance().getString(2386 + languageIndex) : LocaleManager.getInstance().getString(2410 + (languageIndex - 5));
    }

    public int getLanguageCount() => LocaleManager.getInstance().getSupportedLocales().Length;

    public int getCurrentLanguage()
    {
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      LocaleManager instance = LocaleManager.getInstance();
      // ISSUE: reference to a compiler-generated method
      string[] supportedLocales = instance.getSupportedLocales();
      // ISSUE: reference to a compiler-generated method
      string locale = instance.getLocale();
      for (int currentLanguage = 0; currentLanguage < supportedLocales.Length; ++currentLanguage)
      {
        if (locale == supportedLocales[currentLanguage])
          return currentLanguage;
      }
      return -1;
    }

    public void setCurrentLanguage(int index)
    {
      //if (index == this.getCurrentLanguage())
      //  return;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      LocaleManager instance = LocaleManager.getInstance();
      // ISSUE: reference to a compiler-generated method
      this.setCurrentLocale(instance.getSupportedLocales()[index]);
    }

    public string getCurrentLocale() => LocaleManager.getInstance().getLocale();

    public void setCurrentLocale(string locale)
    {
      // ISSUE: reference to a compiler-generated method
      // ISSUE: variable of a compiler-generated type
      LocaleManager instance = LocaleManager.getInstance();
      // ISSUE: reference to a compiler-generated method
      instance.setLocale(locale);
    }

    private void initStringImage()
    {
    }

    public void drawString(Graphics g, int strId, int font, int x, int y, int anchor)
    {
      string str = this.getString(strId);
      this.drawString(g, str, font, x, y, anchor);
    }

    public void drawString(Graphics g, string str, int font, int x, int y, int anchor)
    {
      this.getStringRenderer(font).drawString(g, str, x, y, anchor);
    }

    public void drawString(
      Graphics g,
      StringBuffer strBuffer,
      int font,
      int x,
      int y,
      int anchor)
    {
      string str = strBuffer.toString();
      this.drawString(g, str, font, x, y, anchor);
    }

    public void drawSubString(
      Graphics g,
      string str,
      int start,
      int end,
      int font,
      int x,
      int y,
      int anchor)
    {
      this.getStringRenderer(font).drawSubstring(g, str, start, end, x, y, anchor);
    }

    public int getLineHeight(int font)
    {
      return this.getStringRenderer(font).getHeight() / Runtime.pixelScale;
    }

    public int getBaseline(int font)
    {
      return this.getStringRenderer(font).getBaselinePosition() / Runtime.pixelScale;
    }

    public int getLeading(int font)
    {
      return this.getStringRenderer(font).getLeading() / Runtime.pixelScale;
    }

    public int getStringWidth(int stringid, int font)
    {
      return this.getStringRenderer(font).stringWidth(this.getString(stringid)) / Runtime.pixelScale;
    }

    public int getStringWidth(string str, int font)
    {
      return this.getStringRenderer(font).stringWidth(str) / Runtime.pixelScale;
    }

    public int getSubStringWidth(string str, int offset, int len, int font)
    {
      return this.getStringRenderer(font).substringWidth(str, offset, len) / Runtime.pixelScale;
    }

    public int getStringWidth(StringBuffer str, int font)
    {
      return this.getStringRenderer(font).stringWidth(str.toString()) / Runtime.pixelScale;
    }

    public int getLinesHeight(int font, int numLines)
    {
      StringRenderer stringRenderer = this.getStringRenderer(font);
      return (numLines * stringRenderer.getHeight() - stringRenderer.getLeading()) / Runtime.pixelScale;
    }

    private void initFonts()
    {
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_FONTS_BIN")));
      int num = dataInputStream.readInt();
      for (int index = 0; index < num; ++index)
      {
        string face = dataInputStream.readUTF();
        string filename = dataInputStream.readUTF();
        float size = dataInputStream.readFloat() * (float) Runtime.pixelScale;
        dataInputStream.readInt();
        bool enabled = dataInputStream.readInt() != 0;
        int xoffset = dataInputStream.readInt();
        int yoffset = dataInputStream.readInt();
        int blurradius = dataInputStream.readInt();
        int color1 = dataInputStream.readInt();
        int color2 = dataInputStream.readInt();
        int colour = dataInputStream.readInt();
        dataInputStream.readInt();
        Font font = (Font) null;
        if (face != null && face.Length > 0)
          font = Font.getFont(face, size);
        else if (filename != null && filename.Length > 0)
          font = Font.getFontFromFile(filename, size);
        SystemStringRendererIPhone sr1 = new SystemStringRendererIPhone(font);
        sr1.setStrokeColor(color2);
        sr1.useIPhoneFontDropShadow(enabled);
        sr1.setIPhoneDropShadowParameters(xoffset, yoffset, blurradius);
        sr1.setIPhoneDropShadowColor(colour);
        sr1.setColor(color1);
        this.m_EFIGS_stringRenderers.Add((StringRenderer) new CachedStringRenderer((StringRenderer) sr1));
        if (index != 0)
          font = Font.getFontFromFile("MyriadProBlack.otf", size);
        SystemStringRendererIPhone sr2 = new SystemStringRendererIPhone(font);
        sr2.setStrokeColor(color2);
        sr2.useIPhoneFontDropShadow(enabled);
        sr2.setIPhoneDropShadowParameters(xoffset, yoffset, blurradius);
        sr2.setIPhoneDropShadowColor(colour);
        sr2.setColor(color1);
        this.m_Other_stringRenderers.Add((StringRenderer) new CachedStringRenderer((StringRenderer) sr2));
      }
      dataInputStream.Destructor();
    }

    public StringRenderer getStringRenderer(int font)
    {
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      int localeIndex = LocaleManager.getInstance().getLocaleIndex((string) null);
      return localeIndex < 5 || localeIndex + 1 == 11 || localeIndex + 1 == 12 || localeIndex + 1 == 13 
                ? this.m_EFIGS_stringRenderers[font] : this.m_Other_stringRenderers[font];
    }

    public string replaceFirst(string baseString, string string0)
    {
      this.m_replaceStrings[0] = string0;
      return this.replace(baseString, this.m_replaceStrings);
    }

    public string replace(string baseString, string[] strings)
    {
      int length = baseString.Length;
      StringBuffer stringBuffer = this.clearStringBuffer();
      bool flag = false;
      for (int index1 = 0; index1 < length; ++index1)
      {
        char c = baseString[index1];
        if (flag && c >= '0' && c <= '9')
        {
          if (c == '\\')
          {
            stringBuffer.append('\\');
          }
          else
          {
            int index2 = (int) c - 48;
            string str = strings[index2];
            stringBuffer.append(str);
          }
          flag = false;
        }
        else if (c == '\\')
          flag = true;
        else
          stringBuffer.append(c);
      }
      return stringBuffer.toString();
    }

    public void dynamicString(int slotid, int baseString, int string0)
    {
      string baseString1 = this.getString(baseString);
      this.m_dynamicStrings[slotid - -12] = this.replaceFirst(baseString1, this.getString(string0));
    }

    public void dynamicString(int slotid, int baseString, int string0, int string1)
    {
      string baseString1 = this.getString(baseString);
      string[] replaceStrings = this.m_replaceStrings;
      replaceStrings[0] = this.getString(string0);
      replaceStrings[1] = this.getString(string1);
      this.m_dynamicStrings[slotid - -12] = this.replace(baseString1, replaceStrings);
    }

    public void dynamicString(int slotid, int baseString, string string1)
    {
      string baseString1 = this.getString(baseString);
      this.m_dynamicStrings[slotid - -12] = this.replaceFirst(baseString1, string1);
    }

    public void dynamicString(int slotid, int baseString, string string0, string string1)
    {
      string baseString1 = this.getString(baseString);
      string[] replaceStrings = this.m_replaceStrings;
      replaceStrings[0] = string0;
      replaceStrings[1] = string1;
      string str = this.replace(baseString1, replaceStrings);
      this.m_dynamicStrings[slotid - -12] = str;
    }

    public void dynamicString(
      int slotid,
      int baseString,
      string string0,
      string string1,
      string string2)
    {
      string baseString1 = this.getString(baseString);
      string[] replaceStrings = this.m_replaceStrings;
      replaceStrings[0] = string0;
      replaceStrings[1] = string1;
      replaceStrings[2] = string2;
      this.m_dynamicStrings[slotid - -12] = this.replace(baseString1, replaceStrings);
    }

    public void dynamicString(int slotid, string str) => this.m_dynamicStrings[slotid - -12] = str;

    public string getDynamicString(int slotid) => this.m_dynamicStrings[slotid - -12];

    public void dynamicString(int slotid, StringBuffer str)
    {
      this.m_dynamicStrings[slotid - -12] = str.toString();
    }

    private void initStringBuffers()
    {
      this.m_tempStringBuffers = new StringBuffer[25];
      for (int index = 0; index < 25; ++index)
        this.m_tempStringBuffers[index] = new StringBuffer(100);
      this.m_tempStringBuffer = (StringBuffer) null;
      this.m_tempBufferIndex = 0;
    }

    public StringBuffer clearStringBuffer()
    {
      ++this.m_tempBufferIndex;
      if (this.m_tempBufferIndex >= 25)
        this.m_tempBufferIndex = 0;
      this.m_tempStringBuffer = this.m_tempStringBuffers[this.m_tempBufferIndex];
      this.m_tempStringBuffer.setLength(0);
      return this.m_tempStringBuffer;
    }

    public void appendStringIdToBuffer(int stringId)
    {
      this.m_tempStringBuffer.append(this.getString(stringId));
    }

    public void appendStringIdToBuffer(StringBuffer stringBuffer, int stringId)
    {
      stringBuffer.append(this.getString(stringId));
    }

    public void appendStringToBuffer(string stringToAppend)
    {
      this.m_tempStringBuffer.append(stringToAppend);
    }

    public void appendStringToBuffer(char[] stringToAppend)
    {
      this.m_tempStringBuffer.append(stringToAppend);
    }

    public void appendIntToBuffer(int num) => this.appendIntToBuffer(this.m_tempStringBuffer, num);

    public void appendIntToBuffer(StringBuffer stringBuffer, int integer)
    {
      if (integer == 0)
      {
        stringBuffer.append('0');
      }
      else
      {
        int num1 = integer;
        if (num1 < 0)
        {
          stringBuffer.append('-');
          num1 = -num1;
        }
        int num2 = 1000000000;
        bool flag = false;
        for (; num2 > 0; num2 /= 10)
        {
          int num3 = num1 / num2;
          if (num3 != 0 || flag)
          {
            flag = true;
            stringBuffer.append((char) (48 + num3));
          }
          num1 -= num3 * num2;
        }
      }
    }

    public void appendIntToBufferWithThousandSep(int integer)
    {
      StringBuffer tempStringBuffer = this.m_tempStringBuffer;
      if (integer == 0)
      {
        tempStringBuffer.append('0');
      }
      else
      {
        int num1 = integer;
        if (num1 < 0)
        {
          tempStringBuffer.append('-');
          num1 = -num1;
        }
        int num2 = 1000000000;
        bool flag = false;
        for (; num2 > 0; num2 /= 10)
        {
          int num3 = num1 / num2;
          if (num3 != 0 || flag)
          {
            flag = true;
            tempStringBuffer.append((char) (48 + num3));
            if (num2 == 1000000000 || num2 == 1000000 || num2 == 1000)
              tempStringBuffer.append(this.getString(2064));
          }
          num1 -= num3 * num2;
        }
      }
    }

    public void appendIntToBufferWithLeadingZeros(int num, int digits)
    {
      StringBuffer tempStringBuffer = this.m_tempStringBuffer;
      int index1 = tempStringBuffer.length();
      int num1 = 0;
      if (num < 0)
      {
        tempStringBuffer.setLength(index1 + 1);
        tempStringBuffer.setCharAt(index1, (ushort) 45);
        ++num1;
        num = -num;
      }
      int num2 = index1 + num1 - 1;
      tempStringBuffer.setLength(index1 + digits);
      for (int index2 = index1 + digits - 1; index2 != num2; --index2)
      {
        tempStringBuffer.setCharAt(index2, (ushort) (48 + num % 10));
        num /= 10;
      }
    }

    public void appendMillisTimeToBuffer(
      StringBuffer stringBuffer,
      int millis,
      int millisDecimalPoints)
    {
      bool flag = millis < 0;
      int num1 = Math.Abs(millis);
      int num2 = num1 / 1000;
      int num3 = num2 / 60;
      int integer1 = num3 / 60;
      int num4 = num1 % 1000;
      int integer2 = num2 % 60;
      int integer3 = num3 % 60;
      if (flag)
        stringBuffer.append('-');
      if (integer1 > 0)
      {
        if (integer1 <= 9)
          stringBuffer.append('0');
        this.appendIntToBuffer(stringBuffer, integer1);
        this.appendStringIdToBuffer(stringBuffer, 2059);
      }
      if (integer3 <= 9)
        stringBuffer.append('0');
      this.appendIntToBuffer(stringBuffer, integer3);
      this.appendStringIdToBuffer(stringBuffer, 2059);
      if (integer2 <= 9)
        stringBuffer.append('0');
      this.appendIntToBuffer(stringBuffer, integer2);
      if (0 >= millisDecimalPoints)
        return;
      this.appendStringIdToBuffer(stringBuffer, 2060);
      TextManager.millisDigits[0] = num4 / 100;
      TextManager.millisDigits[1] = num4 / 10 % 10;
      TextManager.millisDigits[2] = num4 % 10;
      int num5 = Math.Min(millisDecimalPoints, 3);
      for (int index = 0; index != num5; ++index)
        stringBuffer.append(TextManager.millisDigits[index]);
    }

    public string convertIntToStringWithThousandSep(int integer)
    {
      StringBuffer stringBuffer = this.clearStringBuffer();
      this.appendIntToBufferWithThousandSep(integer);
      return stringBuffer.toString();
    }

    public WrappedString getWrappedString() => this.m_wrappedString;
  }
}
