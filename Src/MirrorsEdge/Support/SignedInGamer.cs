using System;

namespace support
{
    public class SignedInGamer
    {
        internal static EventHandler<SignedInEventArgs> SignedIn;
        public LeaderboardWriter LeaderboardWriter;

        public void BeginGetAchievements(AsyncCallback asyncCallback, object m_SignedGamer)
        {
            throw new NotImplementedException();
        }

        internal void EndAwardAchievement(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        internal AchievementCollection EndGetAchievements(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        internal bool IsFriend(Gamer g)
        {
            throw new NotImplementedException();
        }
    }
}