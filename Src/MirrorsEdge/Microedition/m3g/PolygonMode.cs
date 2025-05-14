// Decompiled with JetBrains decompiler
// Type: microedition.m3g.PolygonMode
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class PolygonMode : Object3D
  {
    public const int CULL_BACK = 160;
    public const int CULL_FRONT = 161;
    public const int CULL_NONE = 162;
    public const int SHADE_FLAT = 164;
    public const int SHADE_SMOOTH = 165;
    public const int WINDING_CCW = 168;
    public const int WINDING_CW = 169;
    internal const int m3gPolygonMode_CULL_BACK = 160;
    internal const int m3gPolygonMode_CULL_FRONT = 161;
    internal const int m3gPolygonMode_CULL_NONE = 162;
    internal const int m3gPolygonMode_WINDING_CCW = 168;
    internal const int m3gPolygonMode_WINDING_CW = 169;
    public new const int M3G_UNIQUE_CLASS_ID = 8;
    private int m_Culling;
    private int m_Winding;
    private int m_Shading;
    private bool m_TwoSidedLighting;
    private bool m_LocalCameraLighting;
    private bool m_PerspectiveCorrection;

    public static int m3gPolygonMode_GetCulling(PolygonMode self) => self.getCulling();

    public static int m3gPolygonMode_GetWinding(PolygonMode self) => self.getWinding();

    public static bool m3gPolygonMode_IsTwoSidedLightingEnabled(PolygonMode self)
    {
      return self.isTwoSidedLightingEnabled();
    }

    public static bool m3gPolygonMode_IsLocalCameraLightingEnabled(PolygonMode self)
    {
      return self.isLocalCameraLightingEnabled();
    }

    public static bool m3gPolygonMode_isPerspectiveCorrectionEnabled(PolygonMode self)
    {
      return self.isPerspectiveCorrectionEnabled();
    }

    public static void m3gPolygonMode_SetCulling(ref PolygonMode self, int mode)
    {
      self.setCulling(mode);
    }

    public static void m3gPolygonMode_setWinding(ref PolygonMode self, int mode)
    {
      self.setWinding(mode);
    }

    public static void m3gPolygonMode_SetLocalCameraLightingEnable(
      ref PolygonMode self,
      bool enable)
    {
      self.setLocalCameraLightingEnable(enable);
    }

    public static void m3gPolygonMode_SetPerspectiveCorrectionEnable(
      ref PolygonMode self,
      bool enable)
    {
      self.setPerspectiveCorrectionEnable(enable);
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      PolygonMode polygonMode = (PolygonMode) ret;
      polygonMode.setCulling(this.getCulling());
      polygonMode.setWinding(this.getWinding());
      polygonMode.setShading(this.getShading());
      polygonMode.setTwoSidedLightingEnable(this.isTwoSidedLightingEnabled());
      polygonMode.setLocalCameraLightingEnable(this.isLocalCameraLightingEnabled());
      polygonMode.setPerspectiveCorrectionEnable(this.isPerspectiveCorrectionEnabled());
    }

    public PolygonMode()
    {
      this.m_Culling = 160;
      this.m_Winding = 168;
      this.m_Shading = 165;
      this.m_TwoSidedLighting = false;
      this.m_LocalCameraLighting = false;
      this.m_PerspectiveCorrection = false;
    }

    public void setCulling(int mode) => this.m_Culling = mode;

    public int getCulling() => this.m_Culling;

    public void setWinding(int mode) => this.m_Winding = mode;

    public int getWinding() => this.m_Winding;

    public void setShading(int mode) => this.m_Shading = mode;

    public int getShading() => this.m_Shading;

    public void setTwoSidedLightingEnable(bool enable) => this.m_TwoSidedLighting = enable;

    public bool isTwoSidedLightingEnabled() => this.m_TwoSidedLighting;

    public void setLocalCameraLightingEnable(bool enable) => this.m_LocalCameraLighting = enable;

    public bool isLocalCameraLightingEnabled() => this.m_LocalCameraLighting;

    public void setPerspectiveCorrectionEnable(bool enable)
    {
      this.m_PerspectiveCorrection = enable;
    }

    public bool isPerspectiveCorrectionEnabled() => this.m_PerspectiveCorrection;

    public override int getM3GUniqueClassID() => 8;

    public static PolygonMode m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 8 ? (PolygonMode) obj : (PolygonMode) null;
    }
  }
}
