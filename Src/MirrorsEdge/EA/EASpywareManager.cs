
// Type: ea.EASpywareManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using text;

#nullable disable
namespace ea
{
  public class EASpywareManager
  {
    private static EASpywareManager getInstance_instance = new EASpywareManager();
    private bool m_gotBanner;
    private bool m_gettingBanner;
    private bool m_gettingBannerError;
    private Image m_bannerImage;
    private string m_bannerURL;
    private Image m_offlineBannerImage;
    private string m_offlineBannerURL;
    private bool m_gotTickers;
    private bool m_gettingTicker;
    private bool m_gettingTickerError;
    private int[] m_offlineTickerStrings;
    private string[] m_offlineTickerURLs;
    private string[] m_tickerStrings;
    private string[] m_tickerURLs;
    private bool m_handlerRegistered;
    private int m_hardwareId;
    private int m_prodId;
    private int m_appSellId;
    private string m_langCode;
    private string m_appVersion;
    private TextManager m_textManager;

    public static EASpywareManager getInstance() => EASpywareManager.getInstance_instance;

    public EASpywareManager()
    {
      this.m_hardwareId = -1;
      this.m_prodId = -1;
      this.m_appSellId = -1;
      this.m_langCode = (string) null;
      this.m_appVersion = (string) null;
      this.m_handlerRegistered = false;
      this.m_gotBanner = false;
      this.m_gettingBanner = false;
      this.m_gettingBannerError = false;
      this.m_bannerImage = (Image) null;
      this.m_bannerURL = (string) null;
      this.m_gotTickers = false;
      this.m_gettingTicker = false;
      this.m_gettingTickerError = false;
      this.m_tickerStrings = new string[0];
      this.m_tickerURLs = (string[]) null;
      this.m_offlineBannerImage = (Image) null;
      this.m_offlineBannerURL = (string) null;
      this.m_offlineTickerStrings = new int[0];
      this.m_offlineTickerURLs = (string[]) null;
      this.m_textManager = (TextManager) null;
      this.isConfigured();
    }

    public int getHardwareId() => this.m_hardwareId;

    public int getProdId() => this.m_prodId;

    public string getLangCode() => this.m_langCode;

    public string getAppVersion() => this.m_appVersion;

    public int getAppSellId() => this.m_appSellId;

    public bool isConfigured()
    {
      return this.m_hardwareId != -1 && this.m_prodId != -1 && this.m_appSellId != -1 && this.m_langCode != null && this.m_appVersion != null;
    }

    public bool isEnabled() => false;

    public void setEnabled(bool enabled)
    {
    }

    public void MTXpause()
    {
    }

    public void MTXresume()
    {
    }

    public void appStarted()
    {
    }

    public void logEvent(int eventId) => EASpywareManager.getInstance().isConfigured();

    public void logEvent(int eventId, string symbol1)
    {
    }

    public void logEvent(int eventId, string symbol1, string symbol2)
    {
    }

    public void logEvent(int eventId, int value1)
    {
    }

    public void logEvent(int eventId, int value1, int value2)
    {
    }

    public void setLanguage(string lang)
    {
      if (lang.Length != 2)
        this.m_langCode = lang.Substring(0, 2);
      else
        this.m_langCode = lang;
    }

    public void setTextManager(TextManager textManager) => this.m_textManager = textManager;

    public void retryTickers()
    {
      this.m_gettingTickerError = false;
      this.refreshTickers();
    }

    public void refreshTickers()
    {
      if (!this.m_handlerRegistered || this.m_gettingTicker || this.m_gettingTickerError || this.m_gettingBanner)
        return;
      this.m_gettingTicker = true;
    }

    public int getTickerCount()
    {
      if (!this.m_gotTickers && !this.m_gettingTicker)
        this.refreshTickers();
      return this.m_tickerStrings.Length != 0 ? this.m_tickerStrings.Length : this.m_offlineTickerStrings.Length;
    }

    public string getTickerString(int index)
    {
      if (!this.m_gotTickers && !this.m_gettingTicker)
        this.refreshTickers();
      if (this.m_tickerStrings.Length != 0)
      {
        if (index < this.m_tickerStrings.Length)
          return this.m_tickerStrings[index];
      }
      else if (index < this.m_offlineTickerStrings.Length)
        return this.m_textManager.getString(this.m_offlineTickerStrings[index]);
      return (string) null;
    }

    public string getTickerURL(int index)
    {
      if (!this.m_gotTickers && !this.m_gettingTicker)
        this.refreshTickers();
      if (this.m_tickerStrings.Length != 0)
      {
        if (index < this.m_tickerStrings.Length)
          return this.m_tickerURLs[index];
      }
      else if (index < this.m_offlineTickerStrings.Length)
        return this.m_offlineTickerURLs[index];
      return (string) null;
    }

    public void refreshBanner()
    {
      if (!this.m_handlerRegistered || this.m_gettingBanner || this.m_gettingBannerError || this.m_gotBanner || this.m_gettingTicker)
        return;
      this.m_gettingBanner = true;
    }

    public Image getBannerImage()
    {
      if (!this.m_gotBanner && !this.m_gettingBanner)
        this.refreshBanner();
      return this.m_bannerImage != null ? this.m_bannerImage : this.m_offlineBannerImage;
    }

    public string getBannerURL()
    {
      if (!this.m_gotBanner && !this.m_gettingBanner)
        this.refreshBanner();
      return this.m_bannerURL != null ? this.m_bannerURL : this.m_offlineBannerURL;
    }

    public void showMoreGames()
    {
    }
  }
}
