// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Object3D
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using System;

#nullable disable
namespace microedition.m3g
{
  public class Object3D : IComparable<Object3D>
  {
    public const int M3G_UNIQUE_CLASS_ID = 0;
    private static uint counter = 0;
    private AnimationTrack[] m_AnimationTracks;
    private int m_AnimationTrackCount;
    private int m_UserID;
    private Object3D[] m_AnimationReferenceArray = new Object3D[0];
    public uint m_Number;
    private static float[] value1 = new float[24];
    private static float[] valueFinal = new float[24];

    public int CompareTo(Object3D other)
    {
      return other == null ? 1 : this.m_Number.CompareTo(other.m_Number);
    }

    public Object3D()
    {
      this.m_Number = Object3D.counter;
      ++Object3D.counter;
    }

    ~Object3D()
    {
    }

    public virtual void Destructor()
    {
      if (this.m_AnimationTracks == null)
        return;
      Array.Clear((Array) this.m_AnimationTracks, 0, this.m_AnimationTracks.Length);
      this.m_AnimationTracks = (AnimationTrack[]) null;
    }

    public void createAnimationTracks(int count)
    {
      this.m_AnimationTracks = new AnimationTrack[count];
      this.m_AnimationTrackCount = count;
    }

    public void addAnimationTrack(int index, AnimationTrack animationTrack)
    {
      this.m_AnimationTracks[index] = animationTrack;
    }

    public int animate(int time)
    {
      this.animateReferences(time);
      if (this.getAnimationTrackCount() == 0)
        return 0;
      for (int index = 0; index < this.m_AnimationTracks.Length; ++index)
      {
        AnimationTrack animationTrack = this.m_AnimationTracks[index];
        if ((double) animationTrack.m_Controller.m_Weight != 0.0)
          this.updateAnimationProperty(animationTrack, time);
      }
      this.postAnimate(time);
      return 0;
    }

    public int animate(int time1, int time2, float blend)
    {
      int num1 = int.MaxValue;
      Object3D[] references1 = (Object3D[]) null;
      int references2 = this.getReferences(ref references1);
      if (references2 > 0)
      {
        if (this.m_AnimationReferenceArray.Length < references2)
          this.m_AnimationReferenceArray = new Object3D[references2];
        Object3D[] animationReferenceArray = this.m_AnimationReferenceArray;
        this.getReferences(ref animationReferenceArray);
        for (int index = 0; index < references2; ++index)
        {
          int num2 = animationReferenceArray[index].animate(time1, time2, blend);
          if (num2 < num1)
            num1 = num2;
        }
      }
      for (int index1 = 0; index1 < this.getAnimationTrackCount(); ++index1)
      {
        AnimationTrack animationTrack = this.getAnimationTrack(index1);
        int targetProperty = animationTrack.getTargetProperty();
        float[] sampleValue1 = animationTrack.getSampleValue(time1);
        int length = sampleValue1.Length;
        for (int index2 = 0; index2 < length; ++index2)
          Object3D.value1[index2] = sampleValue1[index2];
        float[] sampleValue2 = animationTrack.getSampleValue(time2);
        if (targetProperty == 268 && (double) sampleValue2[0] * (double) Object3D.value1[0] + (double) sampleValue2[1] * (double) Object3D.value1[1] + (double) sampleValue2[2] * (double) Object3D.value1[2] + (double) sampleValue2[3] * (double) Object3D.value1[3] < 0.0)
        {
          Object3D.value1[0] *= -1f;
          Object3D.value1[1] *= -1f;
          Object3D.value1[2] *= -1f;
          Object3D.value1[3] *= -1f;
        }
        for (int index3 = 0; index3 < length; ++index3)
          Object3D.valueFinal[index3] = GMath.lerp(blend, Object3D.value1[index3], sampleValue2[index3]);
        this.updateAnimationProperty(targetProperty, Object3D.valueFinal);
      }
      this.postAnimate(time1);
      return num1;
    }

    public virtual void updateAnimationProperty(int property, float[] value)
    {
    }

    public virtual void updateAnimationProperty(AnimationTrack track, int time)
    {
    }

    public virtual void updateAnimationProperty(int property, int[] value)
    {
    }

    public virtual void postAnimate(int time)
    {
    }

    public Object3D duplicate()
    {
      Object3D instance = (Object3D) Activator.CreateInstance(this.GetType());
      this.duplicateTo(ref instance);
      return instance;
    }

    protected virtual void duplicateTo(ref Object3D ret)
    {
      ret.setUserID(this.getUserID());
      int animationTrackCount = this.getAnimationTrackCount();
      ret.createAnimationTracks(animationTrackCount);
      for (int index = 0; index < animationTrackCount; ++index)
      {
        AnimationTrack animationTrack = this.getAnimationTrack(index);
        ret.addAnimationTrack(index, animationTrack);
      }
    }

    public Object3D find(int userID)
    {
      if (this.getUserID() == userID)
        return this;
      Object3DFinder finder = new Object3DFinder(userID);
      this.findReferences(ref finder);
      return finder.getFound();
    }

    protected virtual void findReferences(ref Object3DFinder finder)
    {
      for (int index = this.getAnimationTrackCount() - 1; index >= 0; --index)
        finder.find((Object3D) this.getAnimationTrack(index));
    }

    protected virtual void animateReferences(int time)
    {
    }

    protected void animateReferencesObject3D(int time)
    {
    }

    public AnimationTrack getAnimationTrack(int index) => this.m_AnimationTracks[index];

    public int getAnimationTrackCount()
    {
      return this.m_AnimationTracks == null ? 0 : this.m_AnimationTracks.Length;
    }

    public virtual int getReferences(ref Object3D[] references)
    {
      int num1 = 0;
      int num2 = num1;
      int references1 = num1 + this.getAnimationTrackCount();
      if (references != null && references.Length != 0)
      {
        int animationTrackCount = this.getAnimationTrackCount();
        for (int index = 0; index < animationTrackCount; ++index)
          references[num2++] = (Object3D) this.getAnimationTrack(index);
      }
      return references1;
    }

    public int getUserID() => this.m_UserID;

    public void removeAnimationTrack(AnimationTrack animationTrack)
    {
      int num = Array.IndexOf<AnimationTrack>(this.m_AnimationTracks, animationTrack);
      if (num == -1)
        return;
      AnimationTrack[] animationTrackArray = new AnimationTrack[this.m_AnimationTracks.Length - 1];
      for (int index = 0; index < num; ++index)
        animationTrackArray[index] = this.m_AnimationTracks[index];
      for (int index = num; index < this.m_AnimationTracks.Length - 1; ++index)
        animationTrackArray[index] = this.m_AnimationTracks[index + 1];
      this.m_AnimationTracks = animationTrackArray;
      this.m_AnimationTrackCount = this.m_AnimationTracks.Length;
    }

    public void setUserID(int userID) => this.m_UserID = userID;

    public virtual int getM3GUniqueClassID() => 0;
  }
}
