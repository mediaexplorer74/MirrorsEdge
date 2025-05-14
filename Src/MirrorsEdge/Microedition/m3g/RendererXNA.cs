// Decompiled with JetBrains decompiler
// Type: microedition.m3g.RendererXNA
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace microedition.m3g
{
  public class RendererXNA : Renderer
  {
    private const int m_MaxTextureUnits = 2;
    private GraphicsDevice device;
    private Stack<Matrix> modelMatrixStack = new Stack<Matrix>();
    private Matrix projMat;
    private Matrix viewMat;
    private Matrix worldMat = Matrix.Identity;
    private BasicEffect basicEffect;
    private BasicEffect basicEffectLighted;
    private BasicEffect basicEffect2D;
    private DualTextureEffect multiTexture;
    private AlphaTestEffect alphaTestEffect;
    private SkinnedEffect skinnedEffect;
    private AppearanceBase m_cachedAppearance;
    private int numTex;
    private Fog m_cachedFog;
    private Fog m_GlobalFog;
    private float[] m_CurrentColour = new float[4];
    private bool alphaTest;
    public static BlendState blendStateAlpha;
    public static BlendState blendStateAlphaAdd;
    public static BlendState blendStateModulate;
    public static BlendState blendStateModulatex2;
    private RasterizerState rasterStateCullClockwise;
    private RasterizerState rasterStateCullCounter;
    private RasterizerState rasterStateCullNone;
    private DepthStencilState depthStateNone;
    private DepthStencilState depthStateWriteOnly;
    private DepthStencilState depthStateBufferOnly;
    private DepthStencilState depthStateBoth;
    private SamplerState samplerStateWrapU;
    private SamplerState samplerStateWrapV;
    private SamplerState samplerStateWrap;
    private SamplerState samplerStateClamp;
    private Matrix[] m_boneTransforms = new Matrix[72];

    public RendererXNA(GraphicsDevice device)
    {
      this.modelMatrixStack.Push(Matrix.Identity);
      this.projMat = Matrix.Identity;
      this.viewMat = Matrix.Identity;
      this.basicEffect = new BasicEffect(device);
      this.basicEffectLighted = new BasicEffect(device);
      this.basicEffect2D = new BasicEffect(device);
      this.basicEffectLighted.EnableDefaultLighting();
      this.multiTexture = new DualTextureEffect(device);
      this.alphaTestEffect = new AlphaTestEffect(device);
      this.skinnedEffect = new SkinnedEffect(device);
      this.skinnedEffect.WeightsPerVertex = 4;
      this.skinnedEffect.DirectionalLight0.Enabled = false;
      this.device = device;
      this.numTex = 0;
      this.m_cachedFog = (Fog) null;
      this.initBlendStates();
      this.initRasterStates();
      this.initDepthStates();
      this.initSamplerStates();
    }

    private void initBlendStates()
    {
      RendererXNA.blendStateAlpha = new BlendState();
      RendererXNA.blendStateAlpha.AlphaSourceBlend = Blend.SourceAlpha;
      RendererXNA.blendStateAlpha.AlphaDestinationBlend = Blend.InverseSourceAlpha;
      RendererXNA.blendStateAlpha.AlphaBlendFunction = BlendFunction.Add;
      RendererXNA.blendStateAlpha.ColorSourceBlend = Blend.SourceAlpha;
      RendererXNA.blendStateAlpha.ColorDestinationBlend = Blend.InverseSourceAlpha;
      RendererXNA.blendStateAlpha.ColorBlendFunction = BlendFunction.Add;
      RendererXNA.blendStateAlphaAdd = new BlendState();
      RendererXNA.blendStateAlphaAdd.AlphaSourceBlend = Blend.SourceAlpha;
      RendererXNA.blendStateAlphaAdd.AlphaDestinationBlend = Blend.One;
      RendererXNA.blendStateAlphaAdd.AlphaBlendFunction = BlendFunction.Add;
      RendererXNA.blendStateAlphaAdd.ColorSourceBlend = Blend.SourceAlpha;
      RendererXNA.blendStateAlphaAdd.ColorDestinationBlend = Blend.One;
      RendererXNA.blendStateModulate = new BlendState();
      RendererXNA.blendStateModulate.AlphaSourceBlend = Blend.DestinationColor;
      RendererXNA.blendStateModulate.AlphaDestinationBlend = Blend.Zero;
      RendererXNA.blendStateModulate.AlphaBlendFunction = BlendFunction.Add;
      RendererXNA.blendStateModulate.ColorSourceBlend = Blend.DestinationColor;
      RendererXNA.blendStateModulate.ColorDestinationBlend = Blend.Zero;
      RendererXNA.blendStateModulatex2 = new BlendState();
      RendererXNA.blendStateModulatex2.AlphaSourceBlend = Blend.DestinationColor;
      RendererXNA.blendStateModulatex2.AlphaDestinationBlend = Blend.SourceColor;
      RendererXNA.blendStateModulatex2.AlphaBlendFunction = BlendFunction.Add;
    }

    private void initRasterStates()
    {
      this.rasterStateCullClockwise = new RasterizerState();
      this.rasterStateCullClockwise.CullMode = CullMode.CullClockwiseFace;
      this.rasterStateCullClockwise.ScissorTestEnable = true;
      this.rasterStateCullCounter = new RasterizerState();
      this.rasterStateCullCounter.CullMode = CullMode.CullCounterClockwiseFace;
      this.rasterStateCullCounter.ScissorTestEnable = true;
      this.rasterStateCullNone = new RasterizerState();
      this.rasterStateCullNone.CullMode = CullMode.None;
      this.rasterStateCullNone.ScissorTestEnable = true;
    }

    private void initDepthStates()
    {
      this.depthStateNone = new DepthStencilState();
      this.depthStateNone.DepthBufferEnable = false;
      this.depthStateNone.DepthBufferWriteEnable = false;
      this.depthStateWriteOnly = new DepthStencilState();
      this.depthStateWriteOnly.DepthBufferEnable = false;
      this.depthStateWriteOnly.DepthBufferWriteEnable = true;
      this.depthStateBufferOnly = new DepthStencilState();
      this.depthStateBufferOnly.DepthBufferEnable = true;
      this.depthStateBufferOnly.DepthBufferWriteEnable = false;
      this.depthStateBoth = new DepthStencilState();
      this.depthStateBoth.DepthBufferEnable = true;
      this.depthStateBoth.DepthBufferWriteEnable = true;
    }

    private void initSamplerStates()
    {
      this.samplerStateWrapU = new SamplerState();
      this.samplerStateWrapU.AddressU = TextureAddressMode.Wrap;
      this.samplerStateWrapU.AddressV = TextureAddressMode.Clamp;
      this.samplerStateWrapV = new SamplerState();
      this.samplerStateWrapV.AddressU = TextureAddressMode.Clamp;
      this.samplerStateWrapV.AddressV = TextureAddressMode.Wrap;
      this.samplerStateWrap = new SamplerState();
      this.samplerStateWrap.AddressU = TextureAddressMode.Wrap;
      this.samplerStateWrap.AddressV = TextureAddressMode.Wrap;
      this.samplerStateClamp = new SamplerState();
      this.samplerStateClamp.AddressU = TextureAddressMode.Clamp;
      this.samplerStateClamp.AddressV = TextureAddressMode.Clamp;
    }

    public override void clear(Background background)
    {
      Color color1 = Color.Black;
      ClearOptions options = ClearOptions.Target;
      bool flag = false;
      if (background != null)
      {
        if (background.isColorClearEnabled())
        {
          uint color2 = background.getColor();
          float num = 0.003921569f;
          color1 = new Color((float) ((color2 & 16711680U) >> 16) * num, (float) ((color2 & 65280U) >> 8) * num, (float) (color2 & (uint) byte.MaxValue) * num) * ((float) ((color2 & 4278190080U) >> 24) * num);
          flag = true;
        }
        if (background.isDepthClearEnabled())
        {
          if (background.isColorClearEnabled())
          {
            options |= ClearOptions.DepthBuffer;
            flag = true;
          }
          else
          {
            options = ClearOptions.DepthBuffer;
            flag = true;
          }
        }
      }
      else
      {
        options = ClearOptions.Target | ClearOptions.DepthBuffer;
        flag = true;
      }
      if (!flag)
        return;
      this.device.Clear(options, color1, 1f, 0);
    }

    public override void setViewport(int x, int y, int width, int height)
    {
      this.device.Viewport = new Viewport(x, y, width, height);
    }

    public override void bind(int w, int h)
    {
      this.m_cachedAppearance = (AppearanceBase) null;
      this.m_cachedFog = (Fog) null;
      this.clearEffects();
    }

    public override void popModelTransform()
    {
    }

    public override void pushModelTransform(Transform transform)
    {
      transform.get(ref this.worldMat);
    }

    public void clearEffects()
    {
      this.basicEffect.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      this.basicEffectLighted.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      this.basicEffect2D.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      this.multiTexture.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      this.multiTexture.Texture2 = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      this.skinnedEffect.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
    }

    private void processTexture(Texture2D texture)
    {
      if (texture == null || texture.getImage() == null)
        return;
      Microsoft.Xna.Framework.Graphics.Texture2D texture2d = texture.getImage().texture2d;
      this.device.SamplerStates[0] = (texture2d.Width & ~(texture2d.Width - 1)) == texture2d.Width && (texture2d.Height & ~(texture2d.Height - 1)) == texture2d.Height ? (texture.getWrappingS() != 241 || texture.getWrappingT() != 241 ? (texture.getWrappingS() != 240 || texture.getWrappingT() != 240 ? (texture.getWrappingS() != 241 || texture.getWrappingT() != 240 ? this.samplerStateWrapV : this.samplerStateWrapU) : this.samplerStateClamp) : this.samplerStateWrap) : this.samplerStateClamp;
      if (this.numTex == 0)
      {
        this.basicEffect.Texture = texture2d;
        this.basicEffect.TextureEnabled = true;
        this.basicEffectLighted.Texture = texture2d;
        this.basicEffectLighted.TextureEnabled = true;
        this.basicEffect2D.Texture = texture2d;
        this.basicEffect2D.TextureEnabled = true;
        this.alphaTestEffect.Texture = texture2d;
        this.skinnedEffect.Texture = texture2d;
      }
      else
      {
        this.multiTexture.Texture = this.basicEffect.Texture;
        this.multiTexture.Texture2 = texture2d;
      }
      ++this.numTex;
    }

    private void processSkin(VertexArray skinIndices, VertexArray skinWeights)
    {
    }

    private void processApperance(Appearance appearance)
    {
      this.numTex = 0;
      for (int index = 0; index < appearance.getNumTextures(); ++index)
        this.processTexture(appearance.getTexture(index));
      if (this.numTex == 0)
      {
        this.basicEffect.TextureEnabled = false;
        this.basicEffect.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
        this.basicEffectLighted.TextureEnabled = false;
        this.basicEffectLighted.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
        this.basicEffect2D.TextureEnabled = false;
        this.basicEffect2D.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
        this.skinnedEffect.Texture = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
      }
      if (appearance.getPolygonMode() != null)
      {
        CullMode cullMode = CullMode.CullClockwiseFace;
        switch (appearance.getPolygonMode().getWinding())
        {
          case 168:
            cullMode = CullMode.CullClockwiseFace;
            break;
          case 169:
            cullMode = CullMode.CullCounterClockwiseFace;
            break;
        }
        switch (appearance.getPolygonMode().getCulling())
        {
          case 160:
            this.device.RasterizerState = cullMode != CullMode.CullClockwiseFace ? this.rasterStateCullCounter : this.rasterStateCullClockwise;
            break;
          case 161:
            this.device.RasterizerState = cullMode != CullMode.CullClockwiseFace ? this.rasterStateCullClockwise : this.rasterStateCullCounter;
            break;
          case 162:
            this.device.RasterizerState = this.rasterStateCullNone;
            break;
        }
      }
      else
        this.device.RasterizerState = RasterizerState.CullClockwise;
      CompositingMode compositingMode = appearance.getCompositingMode();
      if (compositingMode == null)
      {
        this.device.DepthStencilState = DepthStencilState.Default;
        this.device.BlendState = BlendState.Opaque;
      }
      else
      {
        float alphaThreshold = compositingMode.getAlphaThreshold();
        if ((double) alphaThreshold < 0.0039215688593685627)
        {
          this.alphaTest = false;
        }
        else
        {
          this.alphaTestEffect.AlphaFunction = CompareFunction.Greater;
          this.alphaTestEffect.ReferenceAlpha = (int) ((double) alphaThreshold * (double) byte.MaxValue);
          this.alphaTest = true;
        }
        this.device.DepthStencilState = !compositingMode.isDepthTestEnabled() || !compositingMode.isDepthWriteEnabled() ? (compositingMode.isDepthTestEnabled() || compositingMode.isDepthWriteEnabled() ? (!compositingMode.isDepthTestEnabled() || compositingMode.isDepthWriteEnabled() ? this.depthStateWriteOnly : this.depthStateBufferOnly) : this.depthStateNone) : this.depthStateBoth;
        switch (compositingMode.getBlending())
        {
          case 64:
            this.device.BlendState = RendererXNA.blendStateAlpha;
            break;
          case 65:
          case 69:
            this.device.BlendState = RendererXNA.blendStateAlphaAdd;
            break;
          case 66:
            this.device.BlendState = RendererXNA.blendStateModulate;
            break;
          case 67:
            this.device.BlendState = RendererXNA.blendStateModulatex2;
            break;
          default:
            this.device.BlendState = appearance.getTexture(0) == null || appearance.getTexture(0).getImage().m_ResName.IndexOf("map_objects") < 0 ? BlendState.Opaque : RendererXNA.blendStateAlpha;
            break;
        }
      }
      Fog fog = appearance.getFog();
      if (fog == null || fog.getMode() == -1)
        fog = this.m_GlobalFog;
      if (fog == null || fog.getMode() == -1 || compositingMode.getBlending() == 65)
      {
        this.basicEffect.FogEnabled = false;
        this.basicEffectLighted.FogEnabled = false;
        this.basicEffect2D.FogEnabled = false;
        this.alphaTestEffect.FogEnabled = false;
        this.multiTexture.FogEnabled = false;
        this.skinnedEffect.FogEnabled = false;
      }
      else
      {
        if (fog == this.m_cachedFog)
          return;
        int color = fog.getColor();
        Vector3 vector3 = new Vector3();
        float num = 0.003921569f;
        vector3.X = (float) ((color & 16711680) >> 16) * num;
        vector3.Y = (float) ((color & 65280) >> 8) * num;
        vector3.Z = (float) (color & (int) byte.MaxValue) * num;
        this.alphaTestEffect.FogColor = this.basicEffect2D.FogColor = this.basicEffectLighted.FogColor = this.basicEffect.FogColor = this.multiTexture.FogColor = this.skinnedEffect.FogColor = vector3;
        this.alphaTestEffect.FogStart = this.basicEffect2D.FogStart = this.basicEffectLighted.FogStart = this.basicEffect.FogStart = this.multiTexture.FogStart = this.skinnedEffect.FogStart = fog.getNearDistance();
        this.alphaTestEffect.FogEnd = this.basicEffect2D.FogEnd = this.basicEffectLighted.FogEnd = this.basicEffect.FogEnd = this.multiTexture.FogEnd = this.skinnedEffect.FogEnd = fog.getFarDistance();
        this.alphaTestEffect.FogEnabled = this.basicEffect2D.FogEnabled = this.basicEffectLighted.FogEnabled = this.basicEffect.FogEnabled = this.multiTexture.FogEnabled = this.skinnedEffect.FogEnabled = true;
      }
    }

    private void ModulateColours(VertexBuffer vertices, Appearance appearance, float alphaFactor)
    {
      this.m_CurrentColour[0] = 1f;
      this.m_CurrentColour[1] = 1f;
      this.m_CurrentColour[2] = 1f;
      this.m_CurrentColour[3] = alphaFactor;
      if (vertices.getDefaultColor() != uint.MaxValue)
      {
        int defaultColor = (int) vertices.getDefaultColor();
        int num1 = defaultColor >> 16 & (int) byte.MaxValue;
        int num2 = defaultColor >> 8 & (int) byte.MaxValue;
        int num3 = defaultColor & (int) byte.MaxValue;
        int num4 = defaultColor >> 24 & (int) byte.MaxValue;
        float num5 = 0.003921569f;
        this.m_CurrentColour[0] *= (float) num1 * num5;
        this.m_CurrentColour[1] *= (float) num2 * num5;
        this.m_CurrentColour[2] *= (float) num3 * num5;
        this.m_CurrentColour[3] *= (float) num4 * num5;
      }
      if (vertices.getColors() != null)
      {
        if ((double) this.m_CurrentColour[0] != 1.0 || (double) this.m_CurrentColour[1] != 1.0 || (double) this.m_CurrentColour[2] != 1.0)
          this.EnableAmbientColour();
        else
          this.DisableColouring();
      }
      else
      {
        this.DisableColouring();
        this.basicEffect.DiffuseColor = new Vector3(this.m_CurrentColour[0], this.m_CurrentColour[1], this.m_CurrentColour[2]);
        this.basicEffectLighted.DiffuseColor = this.basicEffect.DiffuseColor;
        this.basicEffect2D.DiffuseColor = this.basicEffect.DiffuseColor;
        this.alphaTestEffect.DiffuseColor = this.basicEffect.DiffuseColor;
        this.multiTexture.DiffuseColor = this.basicEffect.DiffuseColor;
        this.skinnedEffect.DiffuseColor = this.basicEffect.DiffuseColor;
      }
    }

    private void EnableAmbientColour()
    {
      this.basicEffectLighted.AmbientLightColor = this.basicEffect.AmbientLightColor = this.skinnedEffect.AmbientLightColor = new Vector3(this.m_CurrentColour[0], this.m_CurrentColour[1], this.m_CurrentColour[2]);
      this.basicEffect2D.LightingEnabled = this.basicEffect.LightingEnabled = true;
    }

    private void DisableColouring()
    {
      this.basicEffect2D.LightingEnabled = this.basicEffect.LightingEnabled = false;
      this.basicEffect.AmbientLightColor = this.basicEffectLighted.AmbientLightColor = new Vector3(1f, 1f, 1f);
      this.basicEffect.DiffuseColor = new Vector3(1f, 1f, 1f);
      this.basicEffectLighted.DiffuseColor = this.basicEffect.DiffuseColor;
      this.basicEffect2D.DiffuseColor = this.basicEffect.DiffuseColor;
      this.alphaTestEffect.DiffuseColor = this.basicEffect.DiffuseColor;
      this.multiTexture.DiffuseColor = this.basicEffect.DiffuseColor;
      this.skinnedEffect.DiffuseColor = this.basicEffect.DiffuseColor;
      this.skinnedEffect.AmbientLightColor = new Vector3(1f, 1f, 1f);
    }

    public override void render(
      VertexBuffer vertices,
      IndexBuffer primitives,
      AppearanceBase appearance,
      float alphaFactor)
    {
      if ((double) alphaFactor == 0.0)
        return;
      bool flag = appearance != this.m_cachedAppearance;
      Appearance appearance1 = (Appearance) appearance;
      this.ModulateColours(vertices, appearance1, alphaFactor);
      if (flag)
      {
        this.processApperance(appearance1);
        this.m_cachedAppearance = appearance;
      }
      vertices.updateVertexData(appearance1, primitives.getMinIndex(), primitives.getMaxIndex());
      primitives.updateIndexData();
      this.renderIndexBuffer(primitives, vertices, alphaFactor);
    }

    public override void render(
      VertexBuffer vertices,
      VertexArray skinIndices,
      VertexArray skinWeights,
      Transform[] matrixPalette,
      IndexBuffer primitives,
      AppearanceBase appearance,
      float alphaFactor)
    {
      bool flag = appearance != this.m_cachedAppearance;
      Appearance appearance1 = (Appearance) appearance;
      this.ModulateColours(vertices, appearance1, alphaFactor);
      if (flag)
      {
        this.processApperance(appearance1);
        this.m_cachedAppearance = appearance;
      }
      this.processSkin(skinIndices, skinWeights);
      vertices.updateSkinnedVertexData(appearance1);
      primitives.updateIndexData();
      for (int index = 0; index < matrixPalette.Length; ++index)
        matrixPalette[index].get(ref this.m_boneTransforms[index]);
      for (int length = matrixPalette.Length; length < this.m_boneTransforms.Length; ++length)
        this.m_boneTransforms[length] = Matrix.Identity;
      this.skinnedEffect.SetBoneTransforms(this.m_boneTransforms);
      this.renderSkinnedIndexBuffer(primitives, vertices, alphaFactor);
    }

    public void clearAppearanceCache()
    {
      this.m_cachedAppearance = (AppearanceBase) null;
      this.m_cachedFog = (Fog) null;
    }

    private void renderIndexBuffer(
      IndexBuffer primitives,
      VertexBuffer vertices,
      float alphaFactor)
    {
      Effect currentEffect;
      if (this.alphaTest)
      {
        currentEffect = (Effect) this.alphaTestEffect;
        this.alphaTestEffect.View = this.viewMat;
        this.alphaTestEffect.Projection = this.projMat;
        this.alphaTestEffect.World = this.worldMat;
        this.alphaTestEffect.Alpha = alphaFactor;
        this.alphaTestEffect.VertexColorEnabled = vertices.getColors() != null;
      }
      else if (vertices.getNormals() == null)
      {
        currentEffect = (Effect) this.basicEffect2D;
        this.basicEffect2D.View = this.viewMat;
        this.basicEffect2D.Projection = this.projMat;
        this.basicEffect2D.World = this.worldMat;
        this.basicEffect2D.Alpha = alphaFactor;
        this.basicEffect2D.VertexColorEnabled = vertices.getColors() != null;
      }
      else if (this.numTex < 2)
      {
        currentEffect = (Effect) this.basicEffect;
        this.basicEffect.View = this.viewMat;
        this.basicEffect.Projection = this.projMat;
        this.basicEffect.World = this.worldMat;
        this.basicEffect.Alpha = alphaFactor;
        this.basicEffect.VertexColorEnabled = vertices.getColors() != null;
      }
      else
      {
        currentEffect = (Effect) this.multiTexture;
        this.multiTexture.View = this.viewMat;
        this.multiTexture.Projection = this.projMat;
        this.multiTexture.World = this.worldMat;
        this.multiTexture.Alpha = alphaFactor;
        this.multiTexture.VertexColorEnabled = vertices.getColors() != null;
      }
      if (vertices.getNormals() != null)
        this.renderPrimitives<Vertex>(currentEffect, vertices.getFinalVertices(), primitives, vertices);
      else
        this.renderPrimitives<Vertex2D>(currentEffect, vertices.getFinalVertices2D(), primitives, vertices);
    }

    private void renderSkinnedIndexBuffer(
      IndexBuffer primitives,
      VertexBuffer vertices,
      float alphaFactor)
    {
      this.skinnedEffect.View = this.viewMat;
      this.skinnedEffect.Projection = this.projMat;
      this.skinnedEffect.World = this.worldMat;
      this.skinnedEffect.Alpha = alphaFactor;
      this.renderPrimitives<SkinnedVertex>((Effect) this.skinnedEffect, vertices.getFinalSkinnedVertices(), primitives, vertices);
    }

    private void renderPrimitives<T>(
      Effect currentEffect,
      T[] final_verts,
      IndexBuffer primitives,
      VertexBuffer vertices)
      where T : struct, IVertexType
    {
      ushort[] stripLengths = primitives.getStripLengths();
      short[] explicitIndices = primitives.getExplicitIndices();
      bool flag1 = primitives.isStripped();
      bool flag2 = primitives.isImplicit();
      int primitiveType1 = primitives.getPrimitiveType();
      PrimitiveType primitiveType2 = PrimitiveType.TriangleStrip;
      switch (primitiveType1)
      {
        case 8:
          primitiveType2 = !flag1 ? PrimitiveType.TriangleList : PrimitiveType.TriangleStrip;
          break;
        case 9:
          primitiveType2 = !flag1 ? PrimitiveType.LineList : PrimitiveType.LineStrip;
          break;
        case 10:
          primitiveType2 = PrimitiveType.TriangleStrip;
          if (!flag1)
            break;
          break;
      }
      int primitiveCount1 = primitives.getPrimitiveCount();
      bool flag3 = !flag1 && primitiveCount1 > 0 && !flag2 && vertices.useVertexBuffer && vertices.finalVertexBuffer != null;
      if (flag3)
      {
        this.device.SetVertexBuffer(vertices.finalVertexBuffer);
        this.device.Indices = primitives.finalIndexBuffer;
      }
      for (int index1 = 0; index1 < currentEffect.CurrentTechnique.Passes.Count; ++index1)
      {
        currentEffect.CurrentTechnique.Passes[index1].Apply();
        if (flag1)
        {
          int length = stripLengths.Length;
          ushort[] numArray = stripLengths;
          if (!flag2)
          {
            short[] indexData = explicitIndices;
            int indexOffset = 0;
            for (int index2 = 0; index2 < length; ++index2)
            {
              int num = (int) numArray[index2];
              int primitiveCount2 = 1 + (num - 3);
              this.device.DrawUserIndexedPrimitives<T>(primitiveType2, final_verts, 0, vertices.getVertexCount(), indexData, indexOffset, primitiveCount2);
              indexOffset += num;
            }
          }
          else
          {
            int firstIndex = primitives.getFirstIndex();
            for (int index3 = 0; index3 < length; ++index3)
            {
              int num = (int) numArray[index3];
              int primitiveCount3 = 1 + (num - 3);
              this.device.DrawUserPrimitives<T>(primitiveType2, final_verts, firstIndex, primitiveCount3);
              firstIndex += num;
            }
          }
        }
        else if (primitiveCount1 > 0)
        {
          if (!flag2)
          {
            if (flag3)
            {
              this.device.DrawIndexedPrimitives(primitiveType2, 0, 0, vertices.getVertexCount(), 0, primitiveCount1);
            }
            else
            {
              short[] indexData = explicitIndices;
              this.device.DrawUserIndexedPrimitives<T>(primitiveType2, final_verts, 0, vertices.getVertexCount(), indexData, 0, primitiveCount1);
            }
          }
          else
          {
            int firstIndex = primitives.getFirstIndex();
            this.device.DrawUserPrimitives<T>(primitiveType2, final_verts, firstIndex, primitiveCount1);
          }
        }
      }
      if (!flag3)
        return;
      this.device.SetVertexBuffer((Microsoft.Xna.Framework.Graphics.VertexBuffer) null);
    }

    public override void setProjectionAndViewTransform(Transform projection, Transform view)
    {
      this.worldMat = Matrix.Identity;
      projection.get(ref this.projMat);
      view.get(ref this.viewMat);
    }

    public override void release() => this.device.SetRenderTarget((RenderTarget2D) null);
  }
}
