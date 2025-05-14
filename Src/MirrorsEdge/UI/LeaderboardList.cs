
// Type: UI.LeaderboardList
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using midp;
using mirrorsedge_wp7;
using SevenZip;
using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;
using text;

#nullable disable
namespace UI
{
  public class LeaderboardList : WindowElement
  {
    public const int LIST_BG_ODD = 2135969872;
    public const int LIST_BG_EVEN = 2141233312;
    public const int ITEM_HEIGHT = 35;
    public const int MAX_ENTRIES = 29;
    public const int COL_RANK_X = 10;
    public const int COL_NAME_X = 70;
    public const int COL_TIME_X = 380;
    private bool m_isFriend;
    public int STAT_FONT = 2;
    private LeaderboardWindow m_owner;
    private Image m_image;
    private int m_levelNum;
    private int m_selectedItem;
    private bool m_startedEffect;
    private bool m_stopping;
    private bool m_confirmationWait;
    private List<LeaderboardEntry> m_statIDs;
    private LeaderboardEntry m_pendingStat;

    public LeaderboardList(
      LeaderboardWindow owner,
      int levelNum,
      LeaderboardReader board,
      int width,
      bool isFriend)
    {
      this.m_isFriend = isFriend;
      this.m_owner = owner;
      this.m_image = (Image) null;
      this.m_levelNum = levelNum;
      this.m_selectedItem = -1;
      this.m_startedEffect = false;
      this.m_statIDs = new List<LeaderboardEntry>();
      this.m_pendingStat = (LeaderboardEntry) null;
      this.m_stopping = false;
      this.m_confirmationWait = false;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.setWidth(width);
      int val2 = Math.Min(29, board.Entries.Count);
      int num1 = 35 * Runtime.pixelScale;
      int width1 = width * Runtime.pixelScale;
      Image image = Image.createImage(width1, num1 * Math.Max(5, val2));
      Graphics graphics = image.getGraphics();
      FontWP7Font.SetBitmapGraphics(true);
      FontWP7Font.SetDontScaleBitmapGraphics(true);
      graphics.setClip(0, 0, width1, num1 * Math.Max(5, val2));
      bool flag = true;
      int num2 = 0;
      for (int index = 0; index < val2; ++index)
      {
        LeaderboardEntry entry = board.Entries[index];
        StringBuffer stringBuffer1 = textManager.clearStringBuffer();
        textManager.appendIntToBuffer(stringBuffer1, index + 1);
        string str1 = stringBuffer1.toString();
        StringBuffer stringBuffer2 = textManager.clearStringBuffer();
        string gamertag = entry.Gamer.Gamertag;
        textManager.appendStringToBuffer(gamertag);
        string str2 = stringBuffer2.toString();
        StringBuffer stringBuffer3 = textManager.clearStringBuffer();
        int valueInt32 = entry.Columns.GetValueInt32("BestTime");
        textManager.appendMillisTimeToBuffer(stringBuffer3, valueInt32, 2);
        string str3 = stringBuffer3.toString();
        int num3 = !flag ? 2141233312 : 2135969872;
        graphics.setColor(num3 >> 16 & (int) byte.MaxValue, num3 >> 8 & (int) byte.MaxValue, num3 & (int) byte.MaxValue, num3 >> 24 & (int) byte.MaxValue);
        graphics.fillRect(0, num2 * Runtime.pixelScale / 2, width1 / 2, 35 * Runtime.pixelScale / 2);
        int y = num2 + 17;
        StringRenderer stringRenderer = textManager.getStringRenderer(this.STAT_FONT);
        int color = stringRenderer.getColor();
                stringRenderer.setColor(128);//(entry.Gamer.Gamertag == Gamer.SignedInGamers[PlayerIndex.One].Gamertag ? 128 : 0);
        textManager.drawString(graphics, str1, this.STAT_FONT, 17, y, 17);
        textManager.drawString(graphics, str2, this.STAT_FONT, 70, y, 17);
        textManager.drawString(graphics, str3, this.STAT_FONT, 380, y, 17);
        stringRenderer.setColor(color);
        flag = !flag;
        num2 += 35;
        this.m_statIDs.Add(entry);
      }
      if (this.m_statIDs.Count == 0)
      {
        int y = 87;
        textManager.drawString(graphics, 2395, this.STAT_FONT, (width >> 1) - 20, y, 18);
      }
      FontWP7Font.SetBitmapGraphics(false);
      FontWP7Font.SetDontScaleBitmapGraphics(false);
      this.m_image = image;
      this.setHeight(35 * Math.Max(1, val2));
    }

    public override void Destructor()
    {
      if (this.m_statIDs == null)
        return;
      this.m_image = (Image) null;
      this.m_owner = (LeaderboardWindow) null;
      this.m_statIDs.Clear();
      this.m_statIDs = (List<LeaderboardEntry>) null;
      this.m_pendingStat = (LeaderboardEntry) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      if (!this.m_confirmationWait)
        return;
      if (AppEngine.getCanvas().getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_POSITIVE)
      {
        AppEngine.getCanvas().getWindowStore().clearWindowResult();
        Stream valueStream = this.m_pendingStat.Columns.GetValueStream("BestTimeBlob");
        valueStream.Position = 0L;
        byte[] numArray = new byte[5];
        MemoryStream outStream = new MemoryStream();
        if (valueStream.Read(numArray, 0, 5) != 5)
        {
          AppEngine.getCanvas().getWindowStore().clearWindowResult();
          this.m_selectedItem = -1;
          this.m_startedEffect = false;
          this.m_pendingStat = (LeaderboardEntry) null;
          this.m_stopping = false;
          this.m_confirmationWait = false;
          this.m_owner.Show();
        }
        else
        {
          Decoder decoder = new Decoder();
          decoder.SetDecoderProperties(numArray);
          long outSize = 0;
          for (int index = 0; index < 8; ++index)
          {
            int num = valueStream.ReadByte();
            if (num < 0)
            {
              AppEngine.getCanvas().getWindowStore().clearWindowResult();
              this.m_selectedItem = -1;
              this.m_startedEffect = false;
              this.m_pendingStat = (LeaderboardEntry) null;
              this.m_stopping = false;
              this.m_confirmationWait = false;
              this.m_owner.Show();
              return;
            }
            outSize |= (long) (byte) num << 8 * index;
          }
          long inSize = valueStream.Length - valueStream.Position;
          decoder.Code(valueStream, (Stream) outStream, inSize, outSize, (ICodeProgress) null);
          byte[] array = outStream.ToArray();
          outStream.Close();
          OutputStream resourceAsStream = (OutputStream) OutputStream.getResourceAsStream("ChallengeGhost" + (MirrorsEdge.TrialMode ? "_trial" : ""));
          if (resourceAsStream != null)
          {
            resourceAsStream.write(array, array.Length);
            resourceAsStream.close();
          }
        }
        AppEngine canvas = AppEngine.getCanvas();
        AppEngine.getLevelData().setCurrentLevelByIndex(LevelData.GameMode.GAME_MODE_CHALLENGE, this.m_levelNum);
        canvas.getSceneMenu().stateTransitionFade(SceneMenu.MenuState.STATE_TRANSITION_TO_GAME);
        int valueInt32 = this.m_pendingStat.Columns.GetValueInt32("BestTime");
        canvas.setChallengeTime(valueInt32);
        this.m_owner.close(WindowResult.WINDOW_RESULT_NONE);
        this.m_pendingStat = (LeaderboardEntry) null;
      }
      else
      {
        if (AppEngine.getCanvas().getWindowStore().getWindowResult() != WindowResult.WINDOW_RESULT_NEGATIVE)
          return;
        AppEngine.getCanvas().getWindowStore().clearWindowResult();
        this.m_selectedItem = -1;
        this.m_startedEffect = false;
        this.m_pendingStat = (LeaderboardEntry) null;
        this.m_stopping = false;
        this.m_confirmationWait = false;
        this.m_owner.Show();
      }
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_image != null)
        g.drawScaledRegion(this.m_image, 0, 0, this.m_image.getWidth(), this.m_image.getHeight(), left, top, left + this.m_image.getWidth() / Runtime.pixelScale, top + this.m_image.getHeight() / Runtime.pixelScale);
      AppEngine canvas = AppEngine.getCanvas();
      if (this.m_selectedItem == -1)
        return;
      if (!this.m_startedEffect)
      {
        canvas.getWindowStore().getNetworkWaitEffect().play(left + this.m_x + (this.m_width >> 1), top + this.m_y + this.m_selectedItem * 35 + 17);
        this.m_startedEffect = true;
      }
      else
      {
        if (this.m_pendingStat == null)
          return;
        if (!this.m_stopping && canvas.getWindowStore().getNetworkWaitEffect().isAnimating())
        {
          canvas.getWindowStore().getNetworkWaitEffect().stop();
          this.m_stopping = true;
        }
        else
        {
          if (canvas.getWindowStore().getNetworkWaitEffect().isAnimating() || this.m_confirmationWait)
            return;
          this.m_owner.Hide();
          canvas.getWindowStore().pushWindow((Window) new ConfirmationWindow(2372, 2053, 2054));
          this.m_confirmationWait = true;
        }
      }
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_selectedItem != -1 || this.m_owner.IsDragging())
        return false;
      this.m_selectedItem = (y - 10) / 35;
      if (this.m_selectedItem < 0 || this.m_selectedItem >= this.m_statIDs.Count)
      {
        this.m_selectedItem = -1;
        return false;
      }
      LeaderboardEntry statId = this.m_statIDs[this.m_selectedItem];
      if (statId.Columns.GetValueStream("BestTimeBlob").Length > 0L)
        this.m_pendingStat = statId;
      else
        this.m_selectedItem = -1;
      return true;
    }
  }
}
