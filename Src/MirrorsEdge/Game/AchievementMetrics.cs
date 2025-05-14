// Decompiled with JetBrains decompiler
// Type: game.AchievementMetrics
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace game
{
  public class AchievementMetrics
  {
    public int deaths;
    public int kills;
    public int rivalKills;
    public int disarms;
    public int enemyFalls;
    public int bags;
    public int badLandings;
    public int advancedMoves;
    public float runDistance;
    public float wallRunDistance;
    public float climbDistance;
    public float zipLineDistance;
    public float balanceDistance;
    public float slideDistance;
    public float fallDistance;
    public int time;

    public AchievementMetrics()
    {
      this.deaths = 0;
      this.kills = 0;
      this.rivalKills = 0;
      this.disarms = 0;
      this.enemyFalls = 0;
      this.bags = 0;
      this.badLandings = 0;
      this.advancedMoves = 0;
      this.runDistance = 0.0f;
      this.wallRunDistance = 0.0f;
      this.climbDistance = 0.0f;
      this.zipLineDistance = 0.0f;
      this.balanceDistance = 0.0f;
      this.slideDistance = 0.0f;
      this.fallDistance = 0.0f;
      this.time = 0;
    }

    public static AchievementMetrics operator +(AchievementMetrics one, AchievementMetrics rhs)
    {
      one.deaths += rhs.deaths;
      one.kills += rhs.kills;
      one.rivalKills += rhs.rivalKills;
      one.disarms += rhs.disarms;
      one.enemyFalls += rhs.enemyFalls;
      one.bags += rhs.bags;
      one.badLandings += rhs.badLandings;
      one.advancedMoves += rhs.advancedMoves;
      one.runDistance += rhs.runDistance;
      one.wallRunDistance += rhs.wallRunDistance;
      one.climbDistance += rhs.climbDistance;
      one.zipLineDistance += rhs.zipLineDistance;
      one.balanceDistance += rhs.balanceDistance;
      one.slideDistance += rhs.slideDistance;
      one.fallDistance += rhs.fallDistance;
      one.time += rhs.time;
      return one;
    }

    public void read(DataInputStream dis)
    {
      if (dis.available() < 64)
        return;
      this.deaths = dis.readInt();
      this.kills = dis.readInt();
      this.rivalKills = dis.readInt();
      this.disarms = dis.readInt();
      this.enemyFalls = dis.readInt();
      this.bags = dis.readInt();
      this.badLandings = dis.readInt();
      this.advancedMoves = dis.readInt();
      this.runDistance = dis.readFloat();
      this.wallRunDistance = dis.readFloat();
      this.climbDistance = dis.readFloat();
      this.zipLineDistance = dis.readFloat();
      this.balanceDistance = dis.readFloat();
      this.slideDistance = dis.readFloat();
      this.fallDistance = dis.readFloat();
      this.time = dis.readInt();
    }

    public void write(DataOutputStream dos)
    {
      dos.writeInt(this.deaths);
      dos.writeInt(this.kills);
      dos.writeInt(this.rivalKills);
      dos.writeInt(this.disarms);
      dos.writeInt(this.enemyFalls);
      dos.writeInt(this.bags);
      dos.writeInt(this.badLandings);
      dos.writeInt(this.advancedMoves);
      dos.writeFloat(this.runDistance);
      dos.writeFloat(this.wallRunDistance);
      dos.writeFloat(this.climbDistance);
      dos.writeFloat(this.zipLineDistance);
      dos.writeFloat(this.balanceDistance);
      dos.writeFloat(this.slideDistance);
      dos.writeFloat(this.fallDistance);
      dos.writeInt(this.time);
    }
  }
}
