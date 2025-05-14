
// Type: microedition.m3g.KeyframeSequence
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace microedition.m3g
{
  public class KeyframeSequence : Object3D
  {
    public const int LINEAR = 176;
    public const int SLERP = 177;
    public const int SPLINE = 178;
    public const int SQUAD = 179;
    public const int STEP = 180;
    public const int LOOP = 193;
    public const int CONSTANT = 192;
    private const int CURRENT = 0;
    private const int NEXT = 1;
    public new const int M3G_UNIQUE_CLASS_ID = 19;
    private int m_KeyframeCount;
    private int m_ComponentCount;
    private int m_Interpolation;
    private int m_Duration;
    private int m_RepeatMode;
    private int m_ValidRangeFirst;
    private int m_ValidRangeLast;
    private int[] m_Times;
    private float[] m_Values;
    private int[] m_CalcIndices = new int[2];
    private float[] m_DeltaTimeCache;
    private float[] m_TangentCache;
    private int m_ValidRangeFirstTime;
    private int m_ValidRangeLastTime;
    private bool m_DeltaTimeCacheValid;
    private bool m_TangentCacheValid;
    public static float s_pointNine = 0.9f;
    private static float[] value = new float[1];
    private static float[] currentValue = new float[4];
    private static float[] nextValue = new float[4];

    public KeyframeSequence()
    {
      this.m_KeyframeCount = 0;
      this.m_ComponentCount = 0;
      this.m_Interpolation = 0;
      this.m_Duration = 0;
      this.m_RepeatMode = 0;
      this.m_ValidRangeFirst = 0;
      this.m_ValidRangeLast = 0;
      this.m_Times = (int[]) null;
      this.m_ValidRangeFirstTime = 0;
      this.m_ValidRangeLastTime = 0;
      this.m_Values = (float[]) null;
      this.m_DeltaTimeCache = (float[]) null;
      this.m_TangentCache = new float[0];
      this.m_DeltaTimeCacheValid = false;
      this.m_TangentCacheValid = false;
    }

    public KeyframeSequence(int numKeyframes, int numComponents, int interpolation)
    {
      this.m_KeyframeCount = 0;
      this.m_ComponentCount = 0;
      this.m_Interpolation = 0;
      this.m_Duration = 0;
      this.m_RepeatMode = 0;
      this.m_ValidRangeFirst = 0;
      this.m_ValidRangeLast = 0;
      this.m_Times = (int[]) null;
      this.m_ValidRangeFirstTime = 0;
      this.m_ValidRangeLastTime = 0;
      this.m_Values = (float[]) null;
      this.m_DeltaTimeCache = (float[]) null;
      this.m_TangentCache = new float[0];
      this.m_DeltaTimeCacheValid = false;
      this.m_TangentCacheValid = false;
      this.setInterpolation(interpolation);
      this.setKeyframeSize(numKeyframes, numComponents);
      this.setRepeatMode(192);
      this.setValidRange(0, numKeyframes - 1);
    }

    public override void Destructor()
    {
      if (this.m_Times != null)
        this.m_Times = (int[]) null;
      if (this.m_Values != null)
        this.m_Values = (float[]) null;
      if (this.m_DeltaTimeCache == null)
        return;
      this.m_DeltaTimeCache = (float[]) null;
    }

    private void validateIndex(int index)
    {
    }

    private void validateValueArrayNotNull(float[] value)
    {
    }

    private void validateValueArrayLength(float[] value, int startIndex)
    {
    }

    public void setInterpolation(int interpolation)
    {
      this.m_Interpolation = interpolation;
      this.invalidateDeltaTimeCache();
    }

    public void setKeyframeSize(int numKeyframes, int numComponents)
    {
      this.m_KeyframeCount = numKeyframes;
      this.m_ComponentCount = numComponents;
      if (this.m_Times != null)
        this.m_Times = (int[]) null;
      this.m_Times = new int[numKeyframes];
      if (this.m_Values != null)
        this.m_Values = (float[]) null;
      this.m_Values = new float[numKeyframes * numComponents];
      this.invalidateDeltaTimeCache();
    }

    public override void updateAnimationProperty(int property, float[] value)
    {
    }

    public override void updateAnimationProperty(AnimationTrack track, int time)
    {
    }

    public override void updateAnimationProperty(int property, int[] value)
    {
    }

    public int getComponentCount() => this.m_ComponentCount;

    public int getDuration() => this.m_Duration;

    public int getInterpolationType() => this.m_Interpolation;

    private int getKeyframeTime(int index)
    {
      this.validateIndex(index);
      return this.m_Times[index];
    }

    private void getKeyframeValue(int index, ref float[] value)
    {
      this.validateIndex(index);
      this.validateValueArrayLength(value, 0);
      Array.Copy((Array) this.m_Values, index * this.getComponentCount(), (Array) value, 0, this.getComponentCount());
    }

    public int getKeyframe(int index, ref float[] value)
    {
      if (value != null)
        this.getKeyframeValue(index, ref value);
      return this.getKeyframeTime(index);
    }

    public int getKeyframeCount() => this.m_KeyframeCount;

    public int getRepeatMode() => this.m_RepeatMode;

    public int getValidRangeFirst() => this.m_ValidRangeFirst;

    public int getValidRangeLast() => this.m_ValidRangeLast;

    public void setDuration(int duration)
    {
      if (duration == this.m_Duration)
        return;
      this.m_Duration = duration;
      this.invalidateDeltaTimeCache();
    }

    private void setKeyframeTime(int index, int time)
    {
      int keyframeTime = this.getKeyframeTime(index);
      if (time == keyframeTime)
        return;
      this.m_Times[index] = time;
      this.invalidateDeltaTimeCache();
    }

    private void setKeyframeValue(int index, float[] value, int startIndex)
    {
      this.validateValueArrayNotNull(value);
      this.validateValueArrayLength(value, startIndex);
      Array.Copy((Array) value, startIndex, (Array) this.m_Values, index * this.getComponentCount(), this.getComponentCount());
      this.invalidateTangentCache();
    }

    public void setKeyframe(int index, int time, float[] value, int startIndex)
    {
      this.validateIndex(index);
      this.setKeyframeTime(index, time);
      this.setKeyframeValue(index, value, startIndex);
    }

    public void setRepeatMode(int mode)
    {
      if (mode == this.m_RepeatMode)
        return;
      this.m_RepeatMode = mode;
      this.invalidateDeltaTimeCache();
    }

    public void setValidRange(int first, int last)
    {
      this.validateIndex(first);
      this.validateIndex(last);
      if (first != this.m_ValidRangeFirst)
      {
        this.m_ValidRangeFirst = first;
        this.invalidateDeltaTimeCache();
      }
      if (last == this.m_ValidRangeLast)
        return;
      this.m_ValidRangeLast = last;
      this.invalidateDeltaTimeCache();
    }

    private static int calcCurrentIndex(
      int sequenceTimeInt,
      int validRangeFirst,
      int validRangeLast,
      int[] times)
    {
      int num1 = validRangeLast;
      int num2 = validRangeFirst;
      for (int index1 = num1 - num2; index1 > 5; index1 = num1 - num2)
      {
        int index2 = num1 + num2 >> 1;
        if (times[index2] > sequenceTimeInt)
          num1 = index2;
        else
          num2 = index2;
      }
      int index = num1;
      while (index > num2 && times[index] > sequenceTimeInt)
        --index;
      return index;
    }

    private static int calcPrevIndex(
      int index,
      int validRangeFirst,
      int validRangeLast,
      int repeatMode)
    {
      if (index != validRangeFirst)
        return index - 1;
      return repeatMode == 193 ? validRangeLast : validRangeFirst;
    }

    private static int calcNextIndex(
      int index,
      int validRangeFirst,
      int validRangeLast,
      int repeatMode)
    {
      if (index != validRangeLast)
        return index + 1;
      return repeatMode == 193 ? validRangeFirst : validRangeLast;
    }

    private static void calcIndicesAndTimes(
      KeyframeSequence self,
      int sequenceTimeInt,
      int repeatMode)
    {
      self.updateDeltaTimeCache();
      int validRangeFirst = self.getValidRangeFirst();
      int validRangeLast = self.getValidRangeLast();
      int[] times = self.m_Times;
      int[] calcIndices = self.m_CalcIndices;
      if (validRangeFirst > validRangeLast)
        return;
      int index;
      int num;
      if (sequenceTimeInt < self.m_ValidRangeFirstTime || sequenceTimeInt >= self.m_ValidRangeLastTime)
      {
        index = validRangeLast;
        num = validRangeFirst;
      }
      else
      {
        index = KeyframeSequence.calcCurrentIndex(sequenceTimeInt, validRangeFirst, validRangeLast, times);
        num = KeyframeSequence.calcNextIndex(index, validRangeFirst, validRangeLast, repeatMode);
      }
      calcIndices[0] = index;
      calcIndices[1] = num;
    }

    private static float getInterpolant(
      KeyframeSequence self,
      int current,
      float sequenceTime,
      int repeatMode)
    {
      int keyframeTime = self.getKeyframeTime(current);
      float num = sequenceTime - (float) keyframeTime;
      if ((double) num < 0.0)
        num += (float) self.getDuration();
      float deltaTime = self.getDeltaTime(current);
      return (double) num < (double) deltaTime ? num / deltaTime : 1f;
    }

    public float sample(float sequenceTime, int channel)
    {
      this.sample(sequenceTime, channel, ref KeyframeSequence.value);
      return KeyframeSequence.value[0];
    }

    public void sample(float sequenceTime, int channel, ref float[] value)
    {
      this.validateValueArrayNotNull(value);
      this.validateValueArrayLength(value, 0);
      int sequenceTimeInt = (int) sequenceTime;
      int repeatMode = this.getRepeatMode();
      int componentCount = this.getComponentCount();
      if (repeatMode == 192)
      {
        if (sequenceTimeInt < this.getKeyframeTime(this.getValidRangeFirst()))
        {
          Array.Copy((Array) this.m_Values, this.getValidRangeFirst() * this.getComponentCount(), (Array) value, 0, componentCount);
          return;
        }
        if (sequenceTimeInt >= this.getKeyframeTime(this.getValidRangeLast()))
        {
          Array.Copy((Array) this.m_Values, this.getValidRangeLast() * this.getComponentCount(), (Array) value, 0, componentCount);
          return;
        }
      }
      if (repeatMode == 193)
      {
        int duration = this.getDuration();
        if (sequenceTimeInt >= duration)
        {
          int num = sequenceTimeInt / duration;
          sequenceTime -= (float) (duration * num);
          sequenceTimeInt = (int) sequenceTime;
        }
      }
      KeyframeSequence.calcIndicesAndTimes(this, sequenceTimeInt, repeatMode);
      int[] calcIndices = this.m_CalcIndices;
      int num1 = calcIndices[0];
      this.getKeyframeValue(num1, ref KeyframeSequence.currentValue);
      int interpolationType = this.getInterpolationType();
      if (interpolationType == 180)
      {
        Array.Copy((Array) this.m_Values, num1 * this.getComponentCount(), (Array) value, 0, componentCount);
      }
      else
      {
        int num2 = calcIndices[1];
        this.getKeyframeValue(num2, ref KeyframeSequence.nextValue);
        float interpolant = KeyframeSequence.getInterpolant(this, num1, sequenceTime, repeatMode);
        switch (interpolationType - 176)
        {
          case 0:
            KeyframeSequence.interpolateValueLINEAR(interpolant, KeyframeSequence.currentValue, KeyframeSequence.nextValue, ref value, componentCount);
            break;
          case 1:
            KeyframeSequence.interpolateValueSLERP(interpolant, KeyframeSequence.currentValue, KeyframeSequence.nextValue, ref value);
            break;
          case 2:
            this.updateTangentCache();
            int index1;
            float[] incomingTangent1 = this.getIncomingTangent(num1, out index1);
            int index2;
            float[] outgoingTangent1 = this.getOutgoingTangent(num2, out index2);
            KeyframeSequence.interpolateValueSPLINE(interpolant, KeyframeSequence.currentValue, KeyframeSequence.nextValue, incomingTangent1, index1, outgoingTangent1, index2, value, componentCount);
            break;
          case 3:
            this.updateTangentCache();
            int index3;
            float[] incomingTangent2 = this.getIncomingTangent(num1, out index3);
            int index4;
            float[] outgoingTangent2 = this.getOutgoingTangent(num2, out index4);
            KeyframeSequence.interpolateValueSPLINE(interpolant, KeyframeSequence.currentValue, KeyframeSequence.nextValue, incomingTangent2, index3, outgoingTangent2, index4, value, componentCount);
            break;
        }
      }
    }

    private float getDeltaTime(int index) => this.m_DeltaTimeCache[index];

    private void invalidateDeltaTimeCache()
    {
      this.m_DeltaTimeCacheValid = false;
      this.invalidateTangentCache();
    }

    private void updateDeltaTimeCache()
    {
      if (this.m_DeltaTimeCacheValid)
        return;
      int keyframeCount = this.getKeyframeCount();
      if (this.m_DeltaTimeCache == null)
        this.m_DeltaTimeCache = new float[keyframeCount];
      int validRangeFirst = this.getValidRangeFirst();
      int validRangeLast = this.getValidRangeLast();
      if (validRangeFirst < validRangeLast)
      {
        int num1 = this.getKeyframeTime(0);
        for (int index = 0; index < keyframeCount; ++index)
        {
          int num2 = 0;
          if (index >= validRangeFirst)
          {
            if (index < validRangeLast)
            {
              int keyframeTime = this.getKeyframeTime(index + 1);
              num2 = keyframeTime - num1;
              num1 = keyframeTime;
            }
            else if (index == validRangeLast && this.getRepeatMode() == 193)
              num2 = this.getDuration() - num1 + this.getKeyframeTime(validRangeFirst);
          }
          this.m_DeltaTimeCache[index] = (float) num2;
        }
      }
      else if (validRangeFirst == validRangeLast)
        this.m_DeltaTimeCache[validRangeFirst] = this.getRepeatMode() != 193 ? 0.0f : (float) this.getDuration();
      this.m_ValidRangeFirstTime = this.getKeyframeTime(this.getValidRangeFirst());
      this.m_ValidRangeLastTime = this.getKeyframeTime(this.getValidRangeLast());
      this.m_DeltaTimeCacheValid = true;
    }

    private void invalidateTangentCache() => this.m_TangentCacheValid = false;

    private float[] getIncomingTangent(int current, out int index)
    {
      index = current * 2 * this.getComponentCount();
      return this.m_TangentCache;
    }

    private float[] getOutgoingTangent(int next, out int index)
    {
      index = (next * 2 + 1) * this.getComponentCount();
      return this.m_TangentCache;
    }

    private void updateTangentCache()
    {
      this.updateDeltaTimeCache();
      if (this.m_TangentCacheValid)
        return;
      int keyframeCount = this.getKeyframeCount();
      int componentCount = this.getComponentCount();
      if (this.m_TangentCache == null)
        this.m_TangentCache = new float[keyframeCount * 2 * this.getComponentCount()];
      int validRangeFirst = this.getValidRangeFirst();
      int validRangeLast = this.getValidRangeLast();
      int repeatMode = this.getRepeatMode();
      float[] numArray1 = new float[componentCount];
      float[] numArray2 = new float[componentCount];
      if (validRangeFirst <= validRangeLast)
      {
        for (int index1 = 0; index1 < keyframeCount; ++index1)
        {
          float num1 = 0.0f;
          float num2 = 0.0f;
          int index2;
          int index3;
          if (index1 < validRangeFirst || index1 > validRangeLast)
          {
            index2 = index1;
            index3 = index1;
          }
          else
          {
            index2 = KeyframeSequence.calcPrevIndex(index1, validRangeFirst, validRangeLast, repeatMode);
            index3 = KeyframeSequence.calcNextIndex(index1, validRangeFirst, validRangeLast, repeatMode);
            bool flag1 = index1 == validRangeFirst && repeatMode == 192;
            bool flag2 = index1 == validRangeLast && repeatMode == 192;
            if (!flag1 && !flag2)
            {
              float deltaTime1 = this.getDeltaTime(index1);
              float deltaTime2 = this.getDeltaTime(index2);
              float num3 = deltaTime1 + deltaTime2;
              if ((double) deltaTime1 > 0.0 && (double) num3 > 0.0)
                num1 = 2f * deltaTime1 / num3;
              if ((double) deltaTime2 > 0.0 && (double) num3 > 0.0)
                num2 = 2f * deltaTime2 / num3;
            }
          }
          this.getKeyframeValue(index2, ref numArray1);
          this.getKeyframeValue(index3, ref numArray2);
          int index4;
          float[] incomingTangent = this.getIncomingTangent(index1, out index4);
          int index5;
          float[] outgoingTangent = this.getOutgoingTangent(index1, out index5);
          for (int index6 = 0; index6 < componentCount; ++index6)
          {
            float num4 = (float) (0.5 * ((double) numArray2[index6] - (double) numArray1[index6]));
            incomingTangent[index6 + index4] = num4 * num1;
            outgoingTangent[index6 + index5] = num4 * num2;
          }
        }
      }
      this.m_TangentCacheValid = true;
    }

    private static void interpolateValue(
      float currentInterpolant,
      float nextInterpolant,
      float[] current,
      float[] next,
      ref float[] value,
      int componentCount)
    {
      for (int index = 0; index < componentCount; ++index)
        value[index] = (float) ((double) currentInterpolant * (double) current[index] + (double) nextInterpolant * (double) next[index]);
    }

    private static void interpolateValueLINEAR(
      float t,
      float[] current,
      float[] next,
      ref float[] value,
      int componentCount)
    {
      KeyframeSequence.interpolateValue(1f - t, t, current, next, ref value, componentCount);
    }

    private static void interpolateValueSPLINE(
      float t,
      float[] current,
      float[] next,
      float[] incomingTangent,
      int index1,
      float[] outgoingTangent,
      int index2,
      float[] value,
      int componentCount)
    {
      float num1 = t * t;
      float num2 = num1 * t;
      float num3 = (float) (2.0 * (double) num2 - 3.0 * (double) num1 + 1.0);
      float num4 = 1f - num3;
      float num5 = num2 - num1;
      float num6 = num5 - num1 + t;
      for (int index = 0; index < componentCount; ++index)
        value[index] = (float) ((double) current[index] * (double) num3 + (double) incomingTangent[index + index1] * (double) num6 + (double) next[index] * (double) num4 + (double) outgoingTangent[index + index2] * (double) num5);
    }

    private static void interpolateValueSLERP(
      float t,
      float[] current,
      float[] next,
      ref float[] value)
    {
      float num = (float) ((double) current[0] * (double) next[0] + (double) current[1] * (double) next[1] + (double) current[2] * (double) next[2] + (double) current[3] * (double) next[3]);
      float currentInterpolant = 1f - t;
      float nextInterpolant = t;
      if ((double) num < 0.0)
        nextInterpolant = -nextInterpolant;
      int componentCount = 4;
      KeyframeSequence.interpolateValue(currentInterpolant, nextInterpolant, current, next, ref value, componentCount);
    }

    public override int getM3GUniqueClassID() => 19;
  }
}
