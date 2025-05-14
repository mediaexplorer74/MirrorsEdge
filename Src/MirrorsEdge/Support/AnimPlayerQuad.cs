
// Type: support.AnimPlayerQuad
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition;

#nullable disable
namespace support
{
  public class AnimPlayerQuad
  {
    public const int TERMINATE = 0;
    public const int LOOP = 1;
    public const int RESUME = 0;
    public const int RESTART = 2;
    public const int TERMINATE_RESUME = 0;
    public const int TERMINATE_RESTART = 2;
    public const int LOOP_RESUME = 1;
    public const int LOOP_RESTART = 3;
    public const int RESUME_TERMINATE = 0;
    public const int RESTART_TERMINATE = 2;
    public const int RESUME_LOOP = 1;
    public const int RESTART_LOOP = 3;
    private QuadManager m_quadManager;
    private int m_groupIndex;
    private QuadAttrib[] m_attribArray;
    private int[] m_timeArray;
    private int m_keyframeIndex;
    private int m_keyframeTime;
    private bool m_animating;
    private int m_startFrame;
    private int m_endFrame;
    private bool m_loop;
    private bool m_reverse;

    public int getGroupIndex() => this.m_groupIndex;

    public AnimPlayerQuad(QuadManager quadManager, int groupIndex)
    {
      this.m_quadManager = quadManager;
      this.m_groupIndex = groupIndex;
      this.m_attribArray = (QuadAttrib[]) null;
      this.m_timeArray = (int[]) null;
      this.m_keyframeIndex = 0;
      this.m_keyframeTime = 0;
      this.m_animating = false;
      this.m_startFrame = 0;
      this.m_endFrame = 0;
      this.m_loop = false;
      this.m_reverse = false;
    }

    public void Destructor()
    {
      if (this.m_attribArray != null)
      {
        for (int index = 0; index < this.m_attribArray.Length; ++index)
          this.m_attribArray[index].Destructor();
        this.m_attribArray = (QuadAttrib[]) null;
      }
      this.m_timeArray = (int[]) null;
    }

    public int getNumKeyframes() => this.m_timeArray.Length;

    public int getLastKeyframeIndex() => this.m_timeArray.Length - 1;

    public bool isAnimating() => this.m_animating;

    public void stopAnim() => this.m_animating = false;

    public bool getPlayingForward() => this.m_reverse;

    public void setNumAttributes(int numAttributes)
    {
      this.m_attribArray = new QuadAttrib[numAttributes];
      for (int index = 0; index < this.m_attribArray.Length; ++index)
        this.m_attribArray[index] = new QuadAttrib();
    }

    public void setupAttribute(int attribIndex, QuadMesh mesh, int attribute)
    {
      this.m_attribArray[attribIndex].mesh = mesh;
      this.m_attribArray[attribIndex].attribIndex = attribute;
    }

    public void setNumKeyframes(int numKeyframes)
    {
      this.m_timeArray = new int[numKeyframes];
      for (int index = this.m_attribArray.Length - 1; index != -1; --index)
        this.m_attribArray[index].valueArray = new int[numKeyframes];
      this.m_startFrame = 0;
      this.m_endFrame = numKeyframes - 1;
    }

    public void setKeyframeTime(int keyframeIndex, int time)
    {
      this.m_timeArray[keyframeIndex] = time;
    }

    public void setAttribValue(int attribIndex, int keyframeIndex, int valueF)
    {
      if (valueF == 6946816)
        valueF = 7995392;
      if (valueF == -3407872)
        valueF = -3932160;
      if (valueF == 3407872)
        valueF = 3932160;
      this.m_attribArray[attribIndex].valueArray[keyframeIndex] = valueF;
    }

    public void setMesh(QuadMesh quadMesh)
    {
      for (int index = this.m_attribArray.Length - 1; index != -1; --index)
        this.m_attribArray[index].mesh = quadMesh;
      this.refreshMeshes();
    }

    public int getCurrentKeyframe()
    {
      return this.m_keyframeTime == 0 || !this.m_reverse ? this.m_keyframeIndex : this.m_keyframeIndex + 1;
    }

    public void snapToFrame(int frameIndex)
    {
      this.m_keyframeIndex = frameIndex;
      this.m_keyframeTime = 0;
      this.m_animating = false;
      this.refreshMeshes();
    }

    public void playAnim(int properties)
    {
      if (!this.m_animating || (properties & 2) != 0)
      {
        this.m_keyframeIndex = 0;
        this.m_keyframeTime = 0;
        this.m_animating = true;
        this.refreshMeshes();
      }
      this.m_startFrame = 0;
      this.m_endFrame = this.getLastKeyframeIndex();
      this.m_loop = (properties & 1) != 0;
      this.m_reverse = false;
      this.m_animating = this.m_keyframeIndex != this.m_endFrame;
    }

    public void playAnimReverse(int properties)
    {
      if (!this.m_animating || (properties & 2) != 0)
      {
        this.m_keyframeIndex = this.getLastKeyframeIndex();
        this.m_keyframeTime = 0;
        this.m_animating = true;
        this.refreshMeshes();
      }
      this.m_startFrame = this.getLastKeyframeIndex();
      this.m_endFrame = 0;
      this.m_loop = (properties & 1) != 0;
      this.m_reverse = true;
      this.m_animating = this.m_keyframeIndex != this.m_endFrame || this.m_keyframeTime != 0;
    }

    public void playAnimTo(int destIndex)
    {
      if (this.m_keyframeTime == 0 && this.m_keyframeIndex == destIndex)
      {
        this.m_animating = false;
      }
      else
      {
        this.m_animating = true;
        this.m_endFrame = destIndex;
        this.m_loop = false;
        this.m_reverse = this.m_keyframeIndex >= destIndex;
        if (this.m_reverse && this.m_keyframeTime != 0)
          this.m_startFrame = this.m_keyframeIndex + 1;
        else
          this.m_startFrame = this.m_keyframeIndex;
      }
    }

    public void playAnimAcross(int properties, int startIndex, int endIndex)
    {
      this.m_animating = true;
      this.m_startFrame = startIndex;
      this.m_endFrame = endIndex;
      this.m_loop = (properties & 1) != 0;
      this.m_reverse = endIndex <= startIndex;
      if (this.m_reverse)
      {
        if (!this.m_animating || (properties & 2) != 0 || this.m_keyframeIndex < endIndex || startIndex <= this.m_keyframeIndex)
        {
          this.m_keyframeIndex = startIndex;
          this.m_keyframeTime = 0;
          this.refreshMeshes();
        }
      }
      else if (!this.m_animating || (properties & 2) != 0 || this.m_keyframeIndex < startIndex || endIndex <= this.m_keyframeIndex)
      {
        this.m_keyframeIndex = startIndex;
        this.m_keyframeTime = 0;
        this.refreshMeshes();
      }
      this.m_animating = this.m_keyframeIndex != this.m_endFrame || this.m_keyframeTime != 0;
    }

    public void snapToTime(int time) => this.snapToTime(this.m_startFrame, time);

    public void snapToTimeFS(int timeConst)
    {
      bool reverse = this.m_reverse;
      bool animating = this.m_animating;
      int startFrame = this.m_startFrame;
      int endFrame = this.m_endFrame;
      this.m_keyframeIndex = this.m_startFrame;
      this.m_keyframeTime = timeConst;
      this.m_animating = false;
      this.refreshMeshes();
      this.m_animating = animating;
      this.m_reverse = reverse;
      this.m_startFrame = startFrame;
      this.m_endFrame = endFrame;
    }

    public void snapToTime(int frame, int timeConst)
    {
      int timeStep = timeConst;
      bool reverse = this.m_reverse;
      bool animating = this.m_animating;
      int startFrame = this.m_startFrame;
      int endFrame1 = this.m_endFrame;
      this.m_keyframeIndex = frame;
      this.m_keyframeTime = 0;
      this.m_animating = true;
      if (timeStep < 0)
      {
        timeStep = -timeStep;
        this.m_reverse = !this.m_reverse;
        int endFrame2 = this.m_endFrame;
        this.m_endFrame = this.m_startFrame;
        this.m_startFrame = endFrame2;
      }
      this.update(timeStep);
      this.m_animating = animating;
      this.m_reverse = reverse;
      this.m_startFrame = startFrame;
      this.m_endFrame = endFrame1;
    }

    public void update(int timeStep)
    {
      if (!this.m_animating)
        return;
      if (this.m_reverse)
      {
        this.m_keyframeTime -= timeStep;
        while (this.m_animating && this.m_keyframeTime < 0)
          this.previousFrame();
      }
      else
      {
        this.m_keyframeTime += timeStep;
        while (this.m_animating && this.m_keyframeTime >= this.m_timeArray[this.m_keyframeIndex])
          this.nextFrame();
      }
      this.refreshMeshes();
    }

    private void previousFrame()
    {
      if (this.m_keyframeIndex == this.m_endFrame)
      {
        if (this.m_loop)
        {
          this.m_keyframeIndex = this.m_startFrame - 1;
          this.m_keyframeTime += this.m_timeArray[this.m_keyframeIndex];
        }
        else
        {
          this.m_keyframeTime = 0;
          this.m_keyframeIndex = this.m_endFrame;
          this.m_animating = false;
        }
      }
      else
      {
        --this.m_keyframeIndex;
        this.m_keyframeTime += this.m_timeArray[this.m_keyframeIndex];
      }
    }

    private void nextFrame()
    {
      if (this.m_keyframeIndex == this.m_endFrame - 1)
      {
        if (this.m_loop)
        {
          this.m_keyframeTime -= this.m_timeArray[this.m_keyframeIndex];
          this.m_keyframeIndex = this.m_startFrame;
        }
        else
        {
          this.m_keyframeTime = 0;
          this.m_keyframeIndex = this.m_endFrame;
          this.m_animating = false;
        }
      }
      else
      {
        this.m_keyframeTime -= this.m_timeArray[this.m_keyframeIndex];
        ++this.m_keyframeIndex;
      }
    }

    public float calculateCurrentKeyframeProgress()
    {
      return this.m_keyframeTime == 0 ? (float) this.m_keyframeIndex : (float) this.m_keyframeIndex + (float) this.m_keyframeTime / (float) this.m_timeArray[this.m_keyframeIndex];
    }

    private void refreshMeshes()
    {
      if (this.m_keyframeTime == 0)
      {
        for (int index = this.m_attribArray.Length - 1; index != -1; --index)
        {
          QuadAttrib attrib = this.m_attribArray[index];
          int attribIndex = attrib.attribIndex;
          attrib.mesh.attribF[attribIndex] = attrib.valueArray[this.m_keyframeIndex];
          attrib.mesh.modified = true;
        }
      }
      else
      {
        int b = MathExt.Fdiv(this.m_keyframeTime, this.m_timeArray[this.m_keyframeIndex]);
        for (int index = this.m_attribArray.Length - 1; index != -1; --index)
        {
          QuadAttrib attrib = this.m_attribArray[index];
          int num1 = attrib.valueArray[this.m_keyframeIndex];
          int num2 = attrib.valueArray[this.m_keyframeIndex + 1];
          int attribIndex = attrib.attribIndex;
          if (num1 == num2)
          {
            attrib.mesh.attribF[attribIndex] = num1;
          }
          else
          {
            int num3 = num1 + MathExt.Fmul(num2 - num1, b);
            attrib.mesh.attribF[attribIndex] = num3;
          }
          attrib.mesh.modified = true;
        }
      }
      for (int index = this.m_attribArray.Length - 1; index != -1; --index)
      {
        if (this.m_attribArray[index].mesh.modified)
          this.m_quadManager.updateMesh(this.m_attribArray[index].mesh);
      }
    }
  }
}
