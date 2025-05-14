// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Transformable
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace microedition.m3g
{
  public class Transformable : Object3D
  {
    private float m_TranslationX;
    private float m_TranslationY;
    private float m_TranslationZ;
    private float m_ScaleX;
    private float m_ScaleY;
    private float m_ScaleZ;
    private int m_Qx;
    private int m_Qy;
    private int m_Qz;
    private int m_Qw;
    private float[] m_TranslationAccumulator = new float[3];
    private float[] m_ScaleAccumulator = new float[3];
    private float[] m_OrientationAccumulator = new float[4];
    private bool m_AnimateTranslation;
    private bool m_AnimateScale;
    private bool m_AnimateOrientation;
    private Transform m_Transform;
    private Transform m_CachedTransform;
    private bool m_CachedTransformIsValid;
    private static int[] q = new int[4];

    private void verifyValues(float[] values, int length)
    {
    }

    private void verifyValues(float[] values)
    {
    }

    private void verifyValues(int[] values, int length)
    {
    }

    private void verifyValues(int[] values)
    {
    }

    public Transformable()
    {
      this.m_TranslationX = 0.0f;
      this.m_TranslationY = 0.0f;
      this.m_TranslationZ = 0.0f;
      this.m_ScaleX = 1f;
      this.m_ScaleY = 1f;
      this.m_ScaleZ = 1f;
      this.m_Qx = 0;
      this.m_Qy = 0;
      this.m_Qz = 0;
      this.m_Qw = 65536;
      this.m_AnimateTranslation = false;
      this.m_AnimateScale = false;
      this.m_AnimateOrientation = false;
      this.m_Transform = (Transform) null;
      this.m_CachedTransform = new Transform();
      this.m_CachedTransformIsValid = false;
      this.m_TranslationAccumulator[0] = 0.0f;
      this.m_TranslationAccumulator[1] = 0.0f;
      this.m_TranslationAccumulator[2] = 0.0f;
      this.m_ScaleAccumulator[0] = 0.0f;
      this.m_ScaleAccumulator[1] = 0.0f;
      this.m_ScaleAccumulator[2] = 0.0f;
      this.m_OrientationAccumulator[0] = 0.0f;
      this.m_OrientationAccumulator[1] = 0.0f;
      this.m_OrientationAccumulator[2] = 0.0f;
      this.m_OrientationAccumulator[3] = 0.0f;
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      Transformable transformable = (Transformable) ret;
      float[] values1 = new float[3];
      this.getTranslation(ref values1);
      transformable.setTranslation(values1[0], values1[1], values1[2]);
      float[] values2 = new float[3];
      this.getScale(ref values2);
      transformable.setScale(values2[0], values2[1], values2[2]);
      int[] values3 = new int[4];
      this.getOrientationQuatx(ref values3);
      transformable.setOrientationQuatx(values3[0], values3[1], values3[2], values3[3]);
      transformable.setTransform(ref this.m_Transform);
    }

    public override void updateAnimationProperty(int property, float[] value)
    {
      base.updateAnimationProperty(property, value);
      switch (property)
      {
        case 268:
          this.m_AnimateOrientation = true;
          if ((double) this.m_OrientationAccumulator[0] * (double) value[0] + (double) this.m_OrientationAccumulator[1] * (double) value[1] + (double) this.m_OrientationAccumulator[2] * (double) value[2] + (double) this.m_OrientationAccumulator[3] * (double) value[3] < 0.0)
          {
            this.m_OrientationAccumulator[0] -= value[0];
            this.m_OrientationAccumulator[1] -= value[1];
            this.m_OrientationAccumulator[2] -= value[2];
            this.m_OrientationAccumulator[3] -= value[3];
            break;
          }
          this.m_OrientationAccumulator[0] += value[0];
          this.m_OrientationAccumulator[1] += value[1];
          this.m_OrientationAccumulator[2] += value[2];
          this.m_OrientationAccumulator[3] += value[3];
          break;
        case 270:
          this.m_AnimateScale = true;
          this.m_ScaleAccumulator[0] += value[0];
          this.m_ScaleAccumulator[1] += value[1];
          this.m_ScaleAccumulator[2] += value[2];
          break;
        case 275:
          this.m_AnimateTranslation = true;
          this.m_TranslationAccumulator[0] += value[0];
          this.m_TranslationAccumulator[1] += value[1];
          this.m_TranslationAccumulator[2] += value[2];
          break;
      }
    }

    public override void updateAnimationProperty(AnimationTrack track, int time)
    {
      base.updateAnimationProperty(track, time);
      switch (track.m_Property)
      {
        case 268:
          float[] sampleValue1 = track.getSampleValue(time);
          this.m_AnimateOrientation = true;
          if ((double) this.m_OrientationAccumulator[0] * (double) sampleValue1[0] + (double) this.m_OrientationAccumulator[1] * (double) sampleValue1[1] + (double) this.m_OrientationAccumulator[2] * (double) sampleValue1[2] + (double) this.m_OrientationAccumulator[3] * (double) sampleValue1[3] < 0.0)
          {
            this.m_OrientationAccumulator[0] -= sampleValue1[0];
            this.m_OrientationAccumulator[1] -= sampleValue1[1];
            this.m_OrientationAccumulator[2] -= sampleValue1[2];
            this.m_OrientationAccumulator[3] -= sampleValue1[3];
            break;
          }
          this.m_OrientationAccumulator[0] += sampleValue1[0];
          this.m_OrientationAccumulator[1] += sampleValue1[1];
          this.m_OrientationAccumulator[2] += sampleValue1[2];
          this.m_OrientationAccumulator[3] += sampleValue1[3];
          break;
        case 270:
          float[] sampleValue2 = track.getSampleValue(time);
          this.m_AnimateScale = true;
          this.m_ScaleAccumulator[0] += sampleValue2[0];
          this.m_ScaleAccumulator[1] += sampleValue2[1];
          this.m_ScaleAccumulator[2] += sampleValue2[2];
          break;
        case 275:
          float[] sampleValue3 = track.getSampleValue(time);
          this.m_AnimateTranslation = true;
          this.m_TranslationAccumulator[0] += sampleValue3[0];
          this.m_TranslationAccumulator[1] += sampleValue3[1];
          this.m_TranslationAccumulator[2] += sampleValue3[2];
          break;
      }
    }

    public override void updateAnimationProperty(int property, int[] value)
    {
      base.updateAnimationProperty(property, value);
    }

    public override void postAnimate(int time)
    {
      base.postAnimate(time);
      if (this.m_AnimateTranslation)
        this.setTranslation(this.m_TranslationAccumulator[0], this.m_TranslationAccumulator[1], this.m_TranslationAccumulator[2]);
      if (this.m_AnimateScale)
        this.setScale(this.m_ScaleAccumulator[0], this.m_ScaleAccumulator[1], this.m_ScaleAccumulator[2]);
      if (this.m_AnimateOrientation && (double) this.m_OrientationAccumulator[0] * (double) this.m_OrientationAccumulator[0] + (double) this.m_OrientationAccumulator[1] * (double) this.m_OrientationAccumulator[1] + (double) this.m_OrientationAccumulator[2] * (double) this.m_OrientationAccumulator[2] + (double) this.m_OrientationAccumulator[3] * (double) this.m_OrientationAccumulator[3] != 0.0)
        this.setOrientationQuat(this.m_OrientationAccumulator[0], this.m_OrientationAccumulator[1], this.m_OrientationAccumulator[2], this.m_OrientationAccumulator[3]);
      for (int index = 0; index < 3; ++index)
      {
        this.m_TranslationAccumulator[index] = 0.0f;
        this.m_ScaleAccumulator[index] = 0.0f;
      }
      for (int index = 0; index < 4; ++index)
        this.m_OrientationAccumulator[index] = 0.0f;
      this.m_AnimateTranslation = false;
      this.m_AnimateOrientation = false;
      this.m_AnimateScale = false;
    }

    public void setTransform(ref Transform transform)
    {
      if (transform != null)
      {
        if (this.m_Transform == null)
          this.m_Transform = new Transform(transform);
        else
          this.m_Transform.set(transform);
      }
      else
        this.m_Transform = (Transform) null;
      this.m_CachedTransformIsValid = false;
    }

    public void setTransform()
    {
      this.m_Transform = (Transform) null;
      this.m_CachedTransformIsValid = false;
    }

    public void getTransform(ref Transform transform)
    {
      if (this.m_Transform == null)
        transform.setIdentity();
      else
        transform.set(this.m_Transform);
    }

    public void getTranslation(ref float[] values)
    {
      this.verifyValues(values, 3);
      values[0] = this.m_TranslationX;
      values[1] = this.m_TranslationY;
      values[2] = this.m_TranslationZ;
    }

    public void getTranslationx(ref int[] values)
    {
      this.verifyValues(values, 3);
      values[0] = (int) ((double) this.m_TranslationX * 65536.0);
      values[1] = (int) ((double) this.m_TranslationY * 65536.0);
      values[2] = (int) ((double) this.m_TranslationZ * 65536.0);
    }

    public float getXTranslation() => this.m_TranslationX;

    public float getYTranslation() => this.m_TranslationY;

    public float getZTranslation() => this.m_TranslationZ;

    public int getXTranslationx() => (int) ((double) this.m_TranslationX * 65536.0);

    public int GetYTranslationx() => (int) ((double) this.m_TranslationY * 65536.0);

    public int getZTranslationx() => (int) ((double) this.m_TranslationZ * 65536.0);

    public void getScale(ref float[] values)
    {
      this.verifyValues(values, 3);
      values[0] = this.m_ScaleX;
      values[1] = this.m_ScaleY;
      values[2] = this.m_ScaleZ;
    }

    public void getScalex(ref int[] values)
    {
      this.verifyValues(values, 3);
      values[0] = (int) ((double) this.m_ScaleX * 65536.0 + 0.5);
      values[1] = (int) ((double) this.m_ScaleY * 65536.0 + 0.5);
      values[2] = (int) ((double) this.m_ScaleZ * 65536.0 + 0.5);
    }

    public void getOrientation(ref float[] values)
    {
    }

    public void getOrientation(float values)
    {
    }

    public void getOrientationx(ref int[] values)
    {
    }

    public void getOrientationx(int values)
    {
    }

    public void getOrientationQuat(ref float[] values)
    {
      this.verifyValues(values, 4);
      values[0] = (float) this.m_Qx * 1.52587891E-05f;
      values[1] = (float) this.m_Qy * 1.52587891E-05f;
      values[2] = (float) this.m_Qz * 1.52587891E-05f;
      values[3] = (float) this.m_Qw * 1.52587891E-05f;
    }

    public void getOrientationQuatx(ref int[] values)
    {
      this.verifyValues(values, 4);
      values[0] = this.m_Qx;
      values[1] = this.m_Qy;
      values[2] = this.m_Qz;
      values[3] = this.m_Qw;
    }

    public void setOrientation(float degrees, float x, float y, float z)
    {
      this.setOrientationx((int) ((double) degrees * 65536.0 + 0.5), (int) ((double) x * 65536.0 + 0.5), (int) ((double) y * 65536.0 + 0.5), (int) ((double) z * 65536.0 + 0.5));
    }

    public void setOrientationx(int degrees, int x, int y, int z)
    {
      Transform.angleAxisToQuatx(degrees, x, y, z, Transformable.q);
      this.setOrientationQuatx(Transformable.q[0], Transformable.q[1], Transformable.q[2], Transformable.q[3]);
    }

    public void setOrientationQuat(float qx, float qy, float qz, float qw)
    {
      this.setOrientationQuatx((int) ((double) qx * 65536.0 + 0.5), (int) ((double) qy * 65536.0 + 0.5), (int) ((double) qz * 65536.0 + 0.5), (int) ((double) qw * 65536.0 + 0.5));
    }

    public void setOrientationQuatx(int qxConst, int qyConst, int qzConst, int qwConst)
    {
      int num1 = qxConst;
      int num2 = qyConst;
      int num3 = qzConst;
      int num4 = qwConst;
      float num5 = (float) num1 * 1.52587891E-05f;
      float num6 = (float) num2 * 1.52587891E-05f;
      float num7 = (float) num3 * 1.52587891E-05f;
      float num8 = (float) num4 * 1.52587891E-05f;
      long num9 = (long) (65536.0 / Math.Sqrt((double) num5 * (double) num5 + (double) num6 * (double) num6 + (double) num7 * (double) num7 + (double) num8 * (double) num8));
      int num10 = (int) (num9 * (long) num1 >> 16);
      int num11 = (int) (num9 * (long) num2 >> 16);
      int num12 = (int) (num9 * (long) num3 >> 16);
      int num13 = (int) (num9 * (long) num4 >> 16);
      if (this.m_Qx == num10 && this.m_Qy == num11 && this.m_Qz == num12 && this.m_Qw == num13)
        return;
      this.m_Qx = num10;
      this.m_Qy = num11;
      this.m_Qz = num12;
      this.m_Qw = num13;
      this.m_CachedTransformIsValid = false;
    }

    public void setScale(float sx, float sy, float sz)
    {
      if ((double) this.m_ScaleX == (double) sx && (double) this.m_ScaleY == (double) sy && (double) this.m_ScaleZ == (double) sz)
        return;
      this.m_ScaleX = sx;
      this.m_ScaleY = sy;
      this.m_ScaleZ = sz;
      this.m_CachedTransformIsValid = false;
    }

    public void setScalex(int sx, int sy, int sz)
    {
      this.setScale((float) sx * 1.52587891E-05f, (float) sy * 1.52587891E-05f, (float) sz * 1.52587891E-05f);
    }

    public void setTranslation(float x, float y, float z)
    {
      if ((double) this.m_TranslationX == (double) x && (double) this.m_TranslationY == (double) y && (double) this.m_TranslationZ == (double) z)
        return;
      this.m_TranslationX = x;
      this.m_TranslationY = y;
      this.m_TranslationZ = z;
      this.m_CachedTransformIsValid = false;
    }

    public void setTranslationx(int x, int y, int z)
    {
      this.setTranslation((float) x * 1.52587891E-05f, (float) y * 1.52587891E-05f, (float) z * 1.52587891E-05f);
    }

    public void translate(float x, float y, float z)
    {
      if ((double) x == 0.0 && (double) y == 0.0 && (double) z == 0.0)
        return;
      this.m_TranslationX += x;
      this.m_TranslationY += y;
      this.m_TranslationZ += z;
      this.m_CachedTransformIsValid = false;
    }

    public void translatex(int x, int y, int z)
    {
      this.translate((float) x * 1.52587891E-05f, (float) y * 1.52587891E-05f, (float) z * 1.52587891E-05f);
    }

    public void getCompositeTransform(ref Transform transform)
    {
      this.updateCachedTransform();
      transform.set(this.m_CachedTransform);
    }

    public void getCompositeTransformCumulative(ref Transform transform)
    {
      this.updateCachedTransform();
      transform.postMultiply(this.m_CachedTransform);
    }

    private void updateCachedTransform()
    {
      if (this.m_CachedTransformIsValid)
        return;
      Transform cachedTransform = this.m_CachedTransform;
      cachedTransform.setIdentity();
      if ((double) this.m_TranslationX != 0.0 || (double) this.m_TranslationY != 0.0 || (double) this.m_TranslationZ != 0.0)
        cachedTransform.postTranslate(this.m_TranslationX, this.m_TranslationY, this.m_TranslationZ);
      if (this.m_Qw != 65536 || this.m_Qx != 0 || this.m_Qy != 0 || this.m_Qz != 0)
        cachedTransform.postRotateQuatx(this.m_Qx, this.m_Qy, this.m_Qz, this.m_Qw);
      if ((double) this.m_ScaleX != 1.0 || (double) this.m_ScaleY != 1.0 || (double) this.m_ScaleZ != 1.0)
        cachedTransform.postScale(this.m_ScaleX, this.m_ScaleY, this.m_ScaleZ);
      if (this.m_Transform != null)
        cachedTransform.postMultiply(this.m_Transform);
      this.m_CachedTransformIsValid = true;
    }
  }
}
