// Decompiled with JetBrains decompiler
// Type: game.ChunkRunnerVision
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;
using midp;
using support;
using System;

#nullable disable
namespace game
{
  public class ChunkRunnerVision
  {
    private int m_runnerVisionType;
    private MathOrthoBox m_rvBounds;
    private Node m_runnerVisionNode;
    private int m_curInverseIntensity;
    private int m_targetInverseIntensity;

    public ChunkRunnerVision(DataInputStream dis, Node chunkNode)
    {
      this.m_runnerVisionType = 0;
      this.m_curInverseIntensity = (int) byte.MaxValue;
      this.m_targetInverseIntensity = (int) byte.MaxValue;
      this.m_runnerVisionType = (int) dis.readByte();
      this.m_runnerVisionNode = chunkNode;
      this.m_rvBounds.min.x = (float) dis.readInt() * 1.52587891E-05f;
      this.m_rvBounds.min.y = (float) dis.readInt() * 1.52587891E-05f;
      this.m_rvBounds.max.x = (float) dis.readInt() * 1.52587891E-05f;
      this.m_rvBounds.max.y = (float) dis.readInt() * 1.52587891E-05f;
    }

    public void Destructor() => this.m_runnerVisionNode = (Node) null;

    public int getType() => this.m_runnerVisionType;

    public void updateIntensity(float timeStepSecs, MathVector playerPosition, int facingDir)
    {
      float num1 = 0.0f;
      if (facingDir == -1 && this.m_runnerVisionType == 1)
      {
        if ((double) this.m_rvBounds.min.x <= (double) playerPosition.x)
          num1 = Math.Min(1f, Math.Max(0.0f, (float) ((15.0 - (double) (playerPosition.x - this.m_rvBounds.max.x)) / 10.0)));
      }
      else if (facingDir == 1 && this.m_runnerVisionType == 2)
      {
        if ((double) playerPosition.x <= (double) this.m_rvBounds.max.x)
          num1 = Math.Min(1f, Math.Max(0.0f, (float) ((15.0 - (double) (this.m_rvBounds.min.x - playerPosition.x)) / 10.0)));
      }
      else if (this.m_runnerVisionType == 3)
      {
        if ((double) playerPosition.x <= (double) this.m_rvBounds.min.x)
          num1 = Math.Min(1f, Math.Max(0.0f, (float) ((15.0 - (double) (this.m_rvBounds.min.x - playerPosition.x)) / 10.0)));
        else if ((double) this.m_rvBounds.max.x <= (double) playerPosition.x)
          num1 = Math.Min(1f, Math.Max(0.0f, (float) ((15.0 - (double) (playerPosition.x - this.m_rvBounds.max.x)) / 10.0)));
        else if ((double) this.m_rvBounds.min.x <= (double) playerPosition.x && (double) playerPosition.x <= (double) this.m_rvBounds.max.x)
          num1 = 1f;
      }
      float num2 = 0.0f;
      if ((double) playerPosition.y < (double) this.m_rvBounds.min.y)
        num2 = this.m_rvBounds.min.y - playerPosition.y;
      else if ((double) this.m_rvBounds.max.y < (double) playerPosition.y)
        num2 = playerPosition.y - this.m_rvBounds.max.y;
      this.m_targetInverseIntensity = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) Math.Min(1f, Math.Max(0.0f, (float) ((5.0 - (double) num2) / 2.0))) * (double) num1);
      if (this.m_curInverseIntensity == this.m_targetInverseIntensity)
        return;
      if (this.m_curInverseIntensity < this.m_targetInverseIntensity)
        this.m_curInverseIntensity = Math.Min(this.m_curInverseIntensity + Math.Max(1, (int) ((double) timeStepSecs * 500.0)), this.m_targetInverseIntensity);
      else if (this.m_targetInverseIntensity < this.m_curInverseIntensity)
        this.m_curInverseIntensity = Math.Max(this.m_curInverseIntensity - Math.Max(1, (int) ((double) timeStepSecs * 500.0)), this.m_targetInverseIntensity);
      M3GAssets.applyColor(this.m_runnerVisionNode, (uint) (-65536 | this.m_curInverseIntensity << 8 | this.m_curInverseIntensity));
    }
  }
}
