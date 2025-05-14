// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Camera
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace microedition.m3g
{
  public class Camera : Node
  {
    public const int GENERIC = 48;
    public const int PARALLEL = 49;
    public const int PERSPECTIVE = 50;
    public new const int M3G_UNIQUE_CLASS_ID = 5;
    private float[] cachedMat = new float[16];
    private int m_ProjectionMode;
    private float m_Fovy;
    private float m_AspectRatio;
    private float m_Near;
    private float m_Far;
    private Transform m_ProjectionTransform = new Transform();
    private bool m_CachedTransformIsValid;

    public Camera()
    {
      this.m_ProjectionMode = 48;
      this.m_Fovy = 0.0f;
      this.m_AspectRatio = 0.0f;
      this.m_Near = 0.0f;
      this.m_Far = 0.0f;
      this.m_ProjectionTransform = new Transform();
      this.m_CachedTransformIsValid = true;
    }

    public int getProjection(ref float[] @params)
    {
      if (@params != null)
      {
        @params[0] = this.m_Fovy;
        @params[1] = this.m_AspectRatio;
        @params[2] = this.m_Near;
        @params[3] = this.m_Far;
      }
      return this.m_ProjectionMode;
    }

    public int getProjection(Transform transform)
    {
      if (transform != null)
      {
        this.updateCachedProjectionTransform();
        transform.set(this.m_ProjectionTransform);
      }
      return this.m_ProjectionMode;
    }

    public void setGeneric(Transform transform)
    {
      this.m_ProjectionMode = 48;
      this.m_ProjectionTransform.set(transform);
      this.m_CachedTransformIsValid = true;
    }

    public void setParallel(float fovy, float aspectRatio, float nearClip, float farClip)
    {
      this.m_Fovy = fovy;
      this.m_AspectRatio = aspectRatio;
      this.m_Near = nearClip;
      this.m_Far = farClip;
      this.m_ProjectionMode = 49;
      this.m_CachedTransformIsValid = false;
    }

    public void setPerspective(float fovy, float aspectRatio, float nearClip, float farClip)
    {
      this.m_Fovy = fovy / 1.5f;
      this.m_AspectRatio = aspectRatio;
      this.m_Near = nearClip;
      if ((double) this.m_Near == 0.0)
        this.m_Near = 0.01f;
      this.m_Far = farClip;
      this.m_ProjectionMode = 50;
      this.m_CachedTransformIsValid = false;
    }

    public void setPerspectivex(int fovy, int aspectRatio, int nearClip, int farClip)
    {
      this.setPerspective((float) fovy * 1.52587891E-05f, (float) aspectRatio * 1.52587891E-05f, (float) nearClip * 1.52587891E-05f, (float) farClip * 1.52587891E-05f);
    }

    private void updateCachedProjectionTransform()
    {
      switch (this.m_ProjectionMode)
      {
        case 49:
          float fovy = this.m_Fovy;
          float num1 = this.m_AspectRatio * fovy;
          float num2 = this.m_Far - this.m_Near;
          this.cachedMat[0] = 2f / num1;
          this.cachedMat[1] = 0.0f;
          this.cachedMat[2] = 0.0f;
          this.cachedMat[3] = 0.0f;
          this.cachedMat[4] = 0.0f;
          this.cachedMat[5] = 2f / fovy;
          this.cachedMat[6] = 0.0f;
          this.cachedMat[7] = 0.0f;
          this.cachedMat[8] = 0.0f;
          this.cachedMat[9] = 0.0f;
          this.cachedMat[10] = -1f / num2;
          this.cachedMat[11] = -this.m_Near / num2;
          this.cachedMat[12] = 0.0f;
          this.cachedMat[13] = 0.0f;
          this.cachedMat[14] = 0.0f;
          this.cachedMat[15] = 1f;
          this.m_ProjectionTransform.set(this.cachedMat);
          break;
        case 50:
          float num3 = (float) Math.Tan((double) (this.m_Fovy * ((float) Math.PI / 360f)));
          float num4 = this.m_AspectRatio * num3;
          float num5 = this.m_Far - this.m_Near;
          float num6 = 1f / num4;
          float num7 = 1f / num3;
          float num8 = -this.m_Far / num5;
          float num9 = (float) -((double) this.m_Near * (double) this.m_Far) / num5;
          this.cachedMat[0] = num6;
          this.cachedMat[1] = 0.0f;
          this.cachedMat[2] = 0.0f;
          this.cachedMat[3] = 0.0f;
          this.cachedMat[4] = 0.0f;
          this.cachedMat[5] = num7;
          this.cachedMat[6] = 0.0f;
          this.cachedMat[7] = 0.0f;
          this.cachedMat[8] = 0.0f;
          this.cachedMat[9] = 0.0f;
          this.cachedMat[10] = num8;
          this.cachedMat[11] = num9;
          this.cachedMat[12] = 0.0f;
          this.cachedMat[13] = 0.0f;
          this.cachedMat[14] = -1f;
          this.cachedMat[15] = 0.0f;
          this.m_ProjectionTransform.set(this.cachedMat);
          break;
      }
      this.m_CachedTransformIsValid = true;
    }

    public override int getM3GUniqueClassID() => 5;

    public static Camera m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 5 ? (Camera) obj : (Camera) null;
    }
  }
}
