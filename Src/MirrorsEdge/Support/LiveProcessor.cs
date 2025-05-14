// Decompiled with JetBrains decompiler
// Type: support.LiveProcessor
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using mirrorsedge_wp7;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using text;

#nullable disable
namespace support
{
  public class LiveProcessor
  {
    private const int LeaderboardPageSize = 29;
    private static ContentManager Content;
    private static GraphicsDevice graphicsDevice;
    private static SpriteBatch spriteBatch;
    private static SpriteFont mainFont;
    private static object achievementsLockObject = new object();
    public static AchievementCollection m_Achievements = (AchievementCollection) null;
    public static LeaderboardReader leaderboardReader;
    public static LiveProcessor.GameState gamestate = LiveProcessor.GameState.WaitingForSignIn;
    public static SignedInGamer m_SignedGamer = (SignedInGamer) null;
    private static List<char> alreadyCheckedBadChars = new List<char>();
    private static StringBuilder DebugStringBuilder = new StringBuilder();

    public static void Init(
      ContentManager Content_,
      GraphicsDevice graphicsDevice_,
      SpriteBatch spriteBatch_)
    {
      LiveProcessor.Content = Content_;
      LiveProcessor.graphicsDevice = graphicsDevice_;
      LiveProcessor.spriteBatch = spriteBatch_;
      LiveProcessor.mainFont = LiveProcessor.Content.Load<SpriteFont>("fonts/Font12");
      SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(LiveProcessor.GamerSignedInCallback);
    }

    public static void OnUpdate()
    {
      if (MirrorsEdge.GS_Supported || LiveProcessor.gamestate != LiveProcessor.GameState.WaitingForSignIn)
        return;
      LiveProcessor.gamestate = LiveProcessor.GameState.ReadyAchievements;
    }

    public static void checkSymbols(ref string s)
    {
      ReadOnlyCollection<char> characters = LiveProcessor.mainFont.Characters;
      bool flag = false;
      foreach (char ch in s)
      {
        if (!characters.Contains(ch))
        {
          if (!LiveProcessor.alreadyCheckedBadChars.Contains(ch))
            LiveProcessor.alreadyCheckedBadChars.Add(ch);
          flag = true;
        }
      }
      if (!flag)
        return;
      LiveProcessor.DebugStringBuilder.Length = 0;
      LiveProcessor.DebugStringBuilder.Append('?', s.Length);
      s = LiveProcessor.DebugStringBuilder.ToString();
    }

    public static void Draw(int page)
    {
      if (MirrorsEdge.TrialMode || !MirrorsEdge.GS_Supported)
        return;
      Vector2 position = new Vector2(0.0f, -10f);
      LiveProcessor.spriteBatch.Begin();
      Rectangle bounds = LiveProcessor.graphicsDevice.Viewport.Bounds;
      switch (LiveProcessor.gamestate)
      {
        case LiveProcessor.GameState.UpdateNeeded:
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          string s1 = LocaleManager.getInstance().getString(2392);
          LiveProcessor.checkSymbols(ref s1);
          position.X = (float) (((double) bounds.Width - (double) LiveProcessor.mainFont.MeasureString(s1).X) / 2.0);
          position.Y = (float) (bounds.Height / 2);
          LiveProcessor.spriteBatch.DrawString(LiveProcessor.mainFont, s1, position, Color.Red, 0.0f, new Vector2(0.0f, 0.0f), 1f, SpriteEffects.None, 0.0f);
          goto case LiveProcessor.GameState.ReadyAchievements;
        case LiveProcessor.GameState.WaitingForSignIn:
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          string s2 = LocaleManager.getInstance().getString(2421);
          LiveProcessor.checkSymbols(ref s2);
          position.X = (float) (((double) bounds.Width - (double) LiveProcessor.mainFont.MeasureString(s2).X) / 2.0);
          position.Y = (float) (bounds.Height / 2);
          LiveProcessor.spriteBatch.DrawString(LiveProcessor.mainFont, s2, position, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1f, SpriteEffects.None, 0.0f);
          goto case LiveProcessor.GameState.ReadyAchievements;
        case LiveProcessor.GameState.WaitingForAchivements:
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          string s3 = LocaleManager.getInstance().getString(2422);
          LiveProcessor.checkSymbols(ref s3);
          position.X = (float) (((double) bounds.Width - (double) LiveProcessor.mainFont.MeasureString(s3).X) / 2.0);
          position.Y = (float) (bounds.Height / 2);
          LiveProcessor.spriteBatch.DrawString(LiveProcessor.mainFont, s3, position, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1f, SpriteEffects.None, 0.0f);
          goto case LiveProcessor.GameState.ReadyAchievements;
        case LiveProcessor.GameState.Error:
        case LiveProcessor.GameState.ErrorLeaderboard:
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          string s4 = LocaleManager.getInstance().getString(2420);
          LiveProcessor.checkSymbols(ref s4);
          position.X = (float) (((double) bounds.Width - (double) LiveProcessor.mainFont.MeasureString(s4).X) / 2.0);
          position.Y = (float) (bounds.Height / 2);
          LiveProcessor.spriteBatch.DrawString(LiveProcessor.mainFont, s4, position, Color.Red, 0.0f, new Vector2(0.0f, 0.0f), 1f, SpriteEffects.None, 0.0f);
          goto case LiveProcessor.GameState.ReadyAchievements;
        case LiveProcessor.GameState.ReadyAchievements:
          LiveProcessor.spriteBatch.End();
          break;
        default:
          throw new NotImplementedException();
      }
    }

    public static int GetNumberOfEntries() => LiveProcessor.leaderboardReader.Entries.Count;

    public static bool IsFriend(Gamer g)
    {
      return LiveProcessor.m_SignedGamer != null && LiveProcessor.m_SignedGamer.IsFriend(g);
    }

    protected static void GamerSignedInCallback(object sender, SignedInEventArgs args)
    {
      LiveProcessor.m_SignedGamer = args.Gamer;
      if (LiveProcessor.m_SignedGamer == null || LiveProcessor.gamestate != LiveProcessor.GameState.WaitingForSignIn)
        return;
      MirrorsEdge.TrialMode = Guide.IsTrialMode;
      if (MirrorsEdge.TrialMode)
      {
        LiveProcessor.gamestate = LiveProcessor.GameState.Error;
      }
      else
      {
        LiveProcessor.gamestate = LiveProcessor.GameState.WaitingForAchivements;
        LiveProcessor.m_SignedGamer.BeginGetAchievements(new AsyncCallback(LiveProcessor.GetAchievementsCallback), (object) LiveProcessor.m_SignedGamer);
      }
    }

    protected static void GetAchievementsCallback(IAsyncResult result)
    {
      LiveProcessor.m_SignedGamer = result.AsyncState as SignedInGamer;
      if (LiveProcessor.m_SignedGamer == null)
        return;
      lock (LiveProcessor.achievementsLockObject)
      {
        LiveProcessor.m_Achievements = LiveProcessor.m_SignedGamer.EndGetAchievements(result);
        LiveProcessor.gamestate = LiveProcessor.GameState.ReadyAchievements;
      }
    }

    protected static void AwardAchievementCallback(IAsyncResult result)
    {
      LiveProcessor.m_SignedGamer = result.AsyncState as SignedInGamer;
      if (LiveProcessor.m_SignedGamer == null)
        return;
      LiveProcessor.m_SignedGamer.EndAwardAchievement(result);
      LiveProcessor.m_SignedGamer.BeginGetAchievements(new AsyncCallback(LiveProcessor.GetAchievementsCallback), (object) LiveProcessor.m_SignedGamer);
    }

    public static void AwardAchievement(string achievementKey)
    {
      if (MirrorsEdge.TrialMode || !MirrorsEdge.GS_Supported)
        return;
      //LiveProcessor.m_SignedGamer = Gamer.SignedInGamers[PlayerIndex.One];
      if (LiveProcessor.m_SignedGamer == null)
        return;
      lock (LiveProcessor.achievementsLockObject)
      {
        if (LiveProcessor.m_Achievements == null)
          return;
        foreach (Achievement achievement in LiveProcessor.m_Achievements)
        {
          if (achievement.Key == achievementKey)
          {
            if (achievement.IsEarned)
              break;
            LiveProcessor.m_SignedGamer.BeginAwardAchievement(achievementKey,
                new AsyncCallback(LiveProcessor.AwardAchievementCallback), (object) LiveProcessor.m_SignedGamer);
            break;
          }
        }
      }
    }

    public static bool NewLeaderboardRecord(int LevelIndex, int score, byte[] data, int size)
    {
      if (MirrorsEdge.TrialMode || !MirrorsEdge.GS_Supported || LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
        return false;
      //LiveProcessor.m_SignedGamer = Gamer.SignedInGamers[PlayerIndex.One];
      if (LiveProcessor.m_SignedGamer == null)
        return false;
      LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestTimeLifeTime, LevelIndex);
      LeaderboardEntry leaderboard = LiveProcessor.m_SignedGamer.LeaderboardWriter.GetLeaderboard(leaderboardId);
      leaderboard.Rating = (long) score;
      new BinaryWriter(leaderboard.Columns.GetValueStream("TimeBlob")).Write(data, 0, size);
      leaderboard.Columns.SetValue("TimeStamp", DateTime.Now);
      return true;
    }

    public static void StartLeaderboard(int LevelIndex)
    {
      if (LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
      {
        LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
      }
      else
      {
         LiveProcessor.m_SignedGamer = null;//Gamer.SignedInGamers[PlayerIndex.One];
        if (LiveProcessor.m_SignedGamer == null)
        {
          LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
        }
        else
        {
          LiveProcessor.gamestate = LiveProcessor.GameState.WaitingForLeaderboard;
          try
          {
            LeaderboardReader.BeginRead(LeaderboardIdentity.Create(LeaderboardKey.BestTimeLifeTime, LevelIndex), (Gamer) LiveProcessor.m_SignedGamer, 29, new AsyncCallback(LiveProcessor.LeaderboardReadCallback), (object) LiveProcessor.m_SignedGamer);
          }
          catch (Exception ex)
          {
            LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
          }
        }
      }
    }

    protected static void LeaderboardReadCallback(IAsyncResult result)
    {
      if (LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
        return;
      LiveProcessor.m_SignedGamer = result.AsyncState as SignedInGamer;
      if (LiveProcessor.m_SignedGamer != null)
      {
        try
        {
          LiveProcessor.leaderboardReader = LeaderboardReader.EndRead(result);
          if (LiveProcessor.gamestate != LiveProcessor.GameState.WaitingForLeaderboard)
            return;
          LiveProcessor.gamestate = LiveProcessor.GameState.ReadyLeaderboard;
        }
        catch (Exception ex)
        {
          LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
        }
      }
      else
        LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
    }

    protected static void LeaderboardPageUpCallback(IAsyncResult result)
    {
      if (LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
        return;
      LiveProcessor.m_SignedGamer = result.AsyncState as SignedInGamer;
      if (LiveProcessor.m_SignedGamer != null)
      {
        try
        {
          LiveProcessor.leaderboardReader.EndPageUp(result);
          if (LiveProcessor.gamestate != LiveProcessor.GameState.WaitingForLeaderboard)
            return;
          LiveProcessor.gamestate = LiveProcessor.GameState.ReadyLeaderboard;
        }
        catch (Exception ex)
        {
          LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
        }
      }
      else
        LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
    }

    protected static void LeaderboardPageDownCallback(IAsyncResult result)
    {
      if (LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
        return;
      if (result.AsyncState is SignedInGamer)
      {
        try
        {
          LiveProcessor.leaderboardReader.EndPageDown(result);
          if (LiveProcessor.gamestate != LiveProcessor.GameState.WaitingForLeaderboard)
            return;
          LiveProcessor.gamestate = LiveProcessor.GameState.ReadyLeaderboard;
        }
        catch (Exception ex)
        {
          LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
        }
      }
      else
        LiveProcessor.gamestate = LiveProcessor.GameState.ErrorLeaderboard;
    }

    public static void LeaderboardPrevPage()
    {
      if (LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
        return;
      //LiveProcessor.m_SignedGamer = Gamer.SignedInGamers[PlayerIndex.One];
      if (LiveProcessor.m_SignedGamer == null)
        return;
      LiveProcessor.gamestate = LiveProcessor.GameState.WaitingForLeaderboard;
      LiveProcessor.leaderboardReader.BeginPageUp(new AsyncCallback(LiveProcessor.LeaderboardPageUpCallback), (object) LiveProcessor.m_SignedGamer);
    }

    public static void LeaderboardNextPage()
    {
      if (LiveProcessor.gamestate == LiveProcessor.GameState.UpdateNeeded)
        return;
      //LiveProcessor.m_SignedGamer = Gamer.SignedInGamers[PlayerIndex.One];
      if (LiveProcessor.m_SignedGamer == null)
        return;
      LiveProcessor.gamestate = LiveProcessor.GameState.WaitingForLeaderboard;
      LiveProcessor.leaderboardReader.BeginPageDown(new AsyncCallback(LiveProcessor.LeaderboardPageDownCallback), (object) LiveProcessor.m_SignedGamer);
    }

    public enum GameState
    {
      UpdateNeeded,
      WaitingForSignIn,
      WaitingForAchivements,
      Error,
      WaitingForLeaderboard,
      ReadyAchievements,
      ReadyLeaderboard,
      ErrorLeaderboard,
      Idle,
    }
  }
}
