
// Type: UI.LeaderboardListWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
//using Microsoft.Xna.Framework.GamerServices;
using midp;
using support;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using text;

#nullable disable
namespace UI
{
  public class LeaderboardListWindow : Window
  {
    private const int NETWORK_DOWN_MESSAGE_BUFFER = 30;
    private LeaderboardWindow m_owner;
    private List<LeaderboardList> m_globalLists;
    private List<LeaderboardReader> m_leaderboards;
    private int m_currentIndex;
    private bool m_networkDown;
    private WrappedString m_networkDownMessage;
    private int NETWORK_DOWN_MESSAGE_FONT = 2;
    private int m_WaitingForLeaderboardN;

    public LeaderboardListWindow(LeaderboardWindow owner, int x, int y, int width, int height)
      : base(x, y, width, height)
    {
      this.m_WaitingForLeaderboardN = -1;
      this.m_owner = owner;
      this.m_globalLists = new List<LeaderboardList>();
      this.m_leaderboards = new List<LeaderboardReader>();
      this.m_currentIndex = 0;
      this.m_networkDown = false;
      this.m_networkDownMessage = new WrappedString();
      int num = 0;
      LevelData levelData = AppEngine.getLevelData();
      int numUnlockedLevels = levelData.getNumUnlockedLevels();
      int levelNum = levelData.getLevelNum();
      for (int levelIndex = 0; levelIndex < levelNum; ++levelIndex)
      {
        Level level = levelData.getLevel(levelIndex);
        if (levelIndex == 0 || levelIndex < numUnlockedLevels || level.isLevelComplete())
          ++num;
      }
      while (this.m_globalLists.Count < num)
        this.m_globalLists.Add((LeaderboardList) null);
      while (this.m_leaderboards.Count < num)
        this.m_leaderboards.Add((LeaderboardReader) null);
      for (int index = 0; index < num; ++index)
        this.m_leaderboards[index] = (LeaderboardReader) null;
      this.m_networkDownMessage.wrapString(2420, this.NETWORK_DOWN_MESSAGE_FONT, this.m_width - 30, false);
      this.SetListIdx(0);
    }

    public override void Destructor()
    {
      this.m_owner = (LeaderboardWindow) null;
      for (int index = 0; index < this.m_globalLists.Count; ++index)
      {
        if (this.m_globalLists[index] != null)
          this.m_globalLists[index].Destructor();
      }
      this.m_globalLists.Clear();
      this.m_networkDownMessage.Destructor();
      this.m_networkDownMessage = (WrappedString) null;
      this.m_leaderboards.Clear();
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      for (int index = 0; index < this.m_leaderboards.Count; ++index)
      {
        if (index == this.m_WaitingForLeaderboardN && this.m_globalLists[index] == null && this.m_leaderboards[index] == null)
        {
          if (LiveProcessor.gamestate == LiveProcessor.GameState.ReadyLeaderboard)
          {
            this.leaderboardFinishedLoading(index);
          }
          else
          {
            //Thread.Sleep(1);
            Task.Delay(1);
            if (LiveProcessor.gamestate == LiveProcessor.GameState.ErrorLeaderboard)
            {
              this.m_WaitingForLeaderboardN = -1;
              this.m_networkDown = true;
              AppEngine.getCanvas().getWindowStore().getNetworkWaitEffect().stop();
            }
          }
        }
      }
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_networkDown)
      {
        this.m_networkDownMessage.draw(g, left + (this.m_width >> 1), top + 95 + 94, 18);
      }
      else
      {
        if (this.m_globalLists[this.m_currentIndex] == null)
          return;
        base.render(g, top, left);
      }
    }

    public void SetListIdx(int idx)
    {
      AppEngine canvas = AppEngine.getCanvas();
      WindowElement element = this.getElement(0);
      if (element != null)
        this.removeElement(element);
      this.m_clientBoundsH = 0;
      this.m_currentIndex = idx;
      if (!AppEngine.networkIsReachable())
      {
        this.m_networkDown = true;
        this.setClientOffsetY(0);
        this.adjustClientArea();
      }
      else
      {
        this.m_networkDown = false;
        if (this.m_WaitingForLeaderboardN >= 0)
          return;
        bool flag = false;
        if (this.m_globalLists[idx] != null)
          this.addElement((WindowElement) this.m_globalLists[idx]);
        else
          flag = true;
        if (flag && this.m_leaderboards[idx] == null && (LiveProcessor.gamestate == LiveProcessor.GameState.ReadyAchievements || LiveProcessor.gamestate == LiveProcessor.GameState.ReadyLeaderboard || LiveProcessor.gamestate == LiveProcessor.GameState.ErrorLeaderboard))
        {
          LiveProcessor.StartLeaderboard(idx);
          switch (LiveProcessor.gamestate)
          {
            case LiveProcessor.GameState.WaitingForLeaderboard:
              this.m_WaitingForLeaderboardN = idx;
              canvas.getWindowStore().getNetworkWaitEffect().play(canvas.getWidth() >> 1, canvas.getHeight() >> 1);
              break;
            case LiveProcessor.GameState.ErrorLeaderboard:
              this.m_networkDown = true;
              break;
          }
        }
        this.setClientOffsetY(0);
        this.adjustClientArea();
      }
    }

    public int GetListIdx() => this.m_currentIndex;

    private void leaderboardFinishedLoading(int idx)
    {
      this.m_leaderboards[idx] = LiveProcessor.leaderboardReader;
      LeaderboardList leaderboardList = new LeaderboardList(this.m_owner, idx, this.m_leaderboards[idx], this.m_clientWidth - 20, false);
      this.m_globalLists[idx] = leaderboardList;
      if (idx == this.m_currentIndex)
        this.addElement((WindowElement) leaderboardList);
      AppEngine.getCanvas().getWindowStore().getNetworkWaitEffect().stop();
      this.m_WaitingForLeaderboardN = -1;
    }
  }
}
