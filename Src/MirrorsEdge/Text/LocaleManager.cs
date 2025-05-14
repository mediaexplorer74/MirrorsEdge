
// Type: text.LocaleManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using GameManager;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace text
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  public class LocaleManager
  {
    public const int LOCALE_NONE = 0;
    public const int LOCALE_ENGLISH = 1;
    public const int LOCALE_FRENCH = 2;
    public const int LOCALE_GERMAN = 3;
    public const int LOCALE_ITALIAN = 4;
    public const int LOCALE_SPANISH = 5;
    public const int LOCALE_JAPANESE = 6;
    public const int LOCALE_KOREAN = 7;
    public const int LOCALE_SIMPL_CHINESE = 8;
    public const int LOCALE_RUSSIAN = 9;
    public const int LOCALE_POLISH = 10;
    public const int LOCALE_PORTUGUESE = 11;
    public const int LOCALE_BRAZILIAN_PORTUGUESE = 12;
    public const int LOCALE_COLUMBIAN_SPANISH = 13;
    public const int LOCALE_TRAD_CHINESE = 14;
    public const int LOCALE_COUNT = 15;
    public const int LOCALE_ARABIC = 15;
    private static Dictionary<int, string> m_StringTable = new Dictionary<int, string>();
    private List<string> m_supportedLocales;
    private string m_currentLocale;
    private int m_currentLocaleIndex;
    private int m_stringPoolIdShift;
    private int m_stringIdMask;
    private static readonly LocaleManager s_instance = new LocaleManager();
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;
    private static StringBuilder sb = new StringBuilder(8);

    public static string GetLocaleCode(int locale)
    {
      switch (locale)
      {
        case 1:
          return "en";
        case 2:
          return "fr";
        case 3:
          return "de";
        case 4:
          return "it";
        case 5:
          return "es-ES";
        case 6:
          return "ja-JP";
        case 7:
          return "ko-KR";
        case 8:
          return "zh-CN";
        case 9:
          return "ru-RU";
        case 10:
          return "pl-PL";
        case 11:
          return "pt-PT";
        case 12:
          return "pt-BR";
        case 13:
          return "es-CO";
        case 14:
          return "zh-TW";
        case 15:
          return "ar";
        default:
          return (string) null;
      }
    }

    public static int GetDeviceLanguage(int defaultLocal)
    {
      string name = CultureInfo.CurrentCulture.Name;
      if (name == "zh-HK" || name == "zh-MO" || name == "zh_HK" || name == "zh_MO")
        return 14;
      for (int locale = 1; locale < 15; ++locale)
      {
        // ISSUE: reference to a compiler-generated method
        string localeCode = LocaleManager.GetLocaleCode(locale);
        if (localeCode == name || localeCode.Replace('-', '_') == name)
          return locale;
      }
      for (int locale = 1; locale < 15; ++locale)
      {
        // ISSUE: reference to a compiler-generated method
        if (name.StartsWith(LocaleManager.GetLocaleCode(locale)))
          return locale;
      }
      for (int locale = 1; locale < 15; ++locale)
      {
        // ISSUE: reference to a compiler-generated method
        if (LocaleManager.GetLocaleCode(locale).Substring(0, 2) == name.Substring(0, 2))
          return locale;
      }
      return defaultLocal;
    }

    internal LocaleManager()
    {
      // ISSUE: reference to a compiler-generated field
      this.m_supportedLocales = new List<string>();
      // ISSUE: reference to a compiler-generated field
      this.m_currentLocale = (string) null;
      // ISSUE: reference to a compiler-generated field
      this.m_currentLocaleIndex = -1;
      // ISSUE: reference to a compiler-generated field
      this.m_stringPoolIdShift = 0;
      // ISSUE: reference to a compiler-generated field
      this.m_stringIdMask = 0;
      for (int locale = 1; locale < 15; ++locale)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        this.m_supportedLocales.Add(LocaleManager.GetLocaleCode(locale));
      }
      // ISSUE: reference to a compiler-generated method
      this.setStringIdBits(16);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      this.setLocale(LocaleManager.GetLocaleCode(LocaleManager.GetDeviceLanguage(1)) ?? this.m_supportedLocales[0]);
    }

    public virtual void Destructor()
    {
      // ISSUE: reference to a compiler-generated field
      this.m_supportedLocales = (List<string>) null;
      // ISSUE: reference to a compiler-generated field
      this.m_currentLocale = (string) null;
    }

    public static LocaleManager getInstance() => LocaleManager.s_instance;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) LocaleManager.resourceMan, (object) null))
          LocaleManager.resourceMan = new ResourceManager("MirrorsEdge.ME_Resource", /*typeof (ME_Resource).Assembly*/default);
        return LocaleManager.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => LocaleManager.resourceCulture;
      set
      {
        LocaleManager.resourceCulture = value;
        LocaleManager.m_StringTable.Clear();
      }
    }

    public string getString(int stringId)
    {
      string str;
      // ISSUE: reference to a compiler-generated field
      if (!LocaleManager.m_StringTable.TryGetValue(stringId, out str))
      {
        // ISSUE: reference to a compiler-generated field
        int num = stringId & this.m_stringIdMask;
        // ISSUE: reference to a compiler-generated field
        LocaleManager.sb.Clear();
        // ISSUE: reference to a compiler-generated field
        LocaleManager.sb.Append("TEXT_");
        // ISSUE: reference to a compiler-generated field
        LocaleManager.sb.Append(num);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        str = LocaleManager.ResourceManager.GetString(LocaleManager.sb.ToString(), LocaleManager.resourceCulture) ?? "XXXXXX " + (object) num + " XXXXXX";
        // ISSUE: reference to a compiler-generated field
        LocaleManager.m_StringTable.Add(stringId, str);
      }
      return str;
    }

    public void setStringIdBits(int shift)
    {
      // ISSUE: reference to a compiler-generated field
      this.m_stringPoolIdShift = shift;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.m_stringIdMask = (1 << this.m_stringPoolIdShift) - 1;
    }

    public string[] getSupportedLocales() => this.m_supportedLocales.ToArray();

    public string getLocale() => this.m_currentLocale;

    public bool IsCurrentLanguageHieroglyphic()
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return this.m_currentLocaleIndex + 1 == 6 || this.m_currentLocaleIndex + 1 == 7 || this.m_currentLocaleIndex + 1 == 8 || this.m_currentLocaleIndex + 1 == 14;
    }

    public void setLocale(string locale)
    {
      // ISSUE: reference to a compiler-generated method
      int localeIndex = this.getLocaleIndex(locale);
      // ISSUE: reference to a compiler-generated field
      if (this.m_currentLocaleIndex == localeIndex || localeIndex == -1)
        return;
      // ISSUE: reference to a compiler-generated field
      this.m_currentLocaleIndex = localeIndex;
      // ISSUE: reference to a compiler-generated field
      this.m_currentLocale = locale;
      LocaleManager.Culture = new CultureInfo(locale);
      // ISSUE: reference to a compiler-generated method
      FontWP7Font.SetShadowForHieroglyphic(this.IsCurrentLanguageHieroglyphic());
    }

    public int getLocaleIndex(string locale)
    {
      if (locale == null)
      {
        // ISSUE: reference to a compiler-generated field
        return this.m_currentLocaleIndex;
      }
      int localeIndex = 0;
      // ISSUE: reference to a compiler-generated field
      foreach (string supportedLocale in this.m_supportedLocales)
      {
        if (supportedLocale == locale)
          return localeIndex;
        ++localeIndex;
      }
      return -1;
    }
  }
}
