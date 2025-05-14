
// Type: microedition.m3g.Appearance
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class Appearance : AppearanceBase
  {
    private const int NUM_TEXTURES = 2;
    public new const int M3G_UNIQUE_CLASS_ID = 3;
    private PointSpriteMode m_PointSpriteMode;
    private Texture2D[] m_Textures;
    private Material m_Material;
    private Fog m_Fog;
    public bool m_Mirror;

    public override void Destructor()
    {
      if (this.m_PointSpriteMode != null)
        this.m_PointSpriteMode.Destructor();
      this.m_PointSpriteMode = (PointSpriteMode) null;
      if (this.m_Textures[0] != null)
        this.m_Textures[0].Destructor();
      this.m_Textures[0] = (Texture2D) null;
      if (this.m_Textures[1] != null)
        this.m_Textures[1].Destructor();
      this.m_Textures[1] = (Texture2D) null;
      if (this.m_Material != null)
        this.m_Material.Destructor();
      this.m_Material = (Material) null;
      if (this.m_Fog != null)
        this.m_Fog.Destructor();
      this.m_Fog = (Fog) null;
      base.Destructor();
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      Appearance appearance = (Appearance) ret;
      appearance.setPointSpriteMode(this.getPointSpriteMode());
      for (int index = 0; index < 2; ++index)
        appearance.setTexture(index, this.getTexture(index));
      appearance.setMaterial(this.getMaterial());
      appearance.setFog(this.getFog());
      appearance.m_Mirror = this.m_Mirror;
    }

    public override int getReferences(ref Object3D[] references)
    {
      int references1 = base.getReferences(ref references);
      int num = references1;
      if (this.m_PointSpriteMode != null)
        ++references1;
      if (this.m_Material != null)
        ++references1;
      if (this.m_Fog != null)
        ++references1;
      for (int index = 0; index < 2; ++index)
      {
        if (this.getTexture(index) != null)
          ++references1;
      }
      if (references != null)
      {
        if (this.m_PointSpriteMode != null)
          references[num++] = (Object3D) this.m_PointSpriteMode;
        if (this.m_Material != null)
          references[num++] = (Object3D) this.m_Material;
        if (this.m_Fog != null)
          references[num++] = (Object3D) this.m_Fog;
        for (int index = 0; index < 2; ++index)
        {
          Texture2D texture = this.getTexture(index);
          if (texture != null)
            references[num++] = (Object3D) texture;
        }
      }
      return references1;
    }

    protected override void findReferences(ref Object3DFinder finder)
    {
      base.findReferences(ref finder);
      finder.find((Object3D) this.getPointSpriteMode());
      finder.find((Object3D) this.getMaterial());
      finder.find((Object3D) this.getFog());
      for (int index = 0; index < 2; ++index)
        finder.find((Object3D) this.getTexture(index));
    }

    protected override void animateReferences(int time)
    {
      if (this.getMaterial() != null)
        this.getMaterial().animate(time);
      if (this.getFog() != null)
        this.getFog().animate(time);
      for (int index = 0; index < 2; ++index)
      {
        if (this.getTexture(index) != null)
          this.getTexture(index).animate(time);
      }
    }

    public Appearance()
    {
      this.m_PointSpriteMode = (PointSpriteMode) null;
      this.m_Textures = new Texture2D[2];
      this.m_Material = (Material) null;
      this.m_Fog = (Fog) null;
      for (int index = 0; index < 2; ++index)
        this.m_Textures[index] = (Texture2D) null;
      this.m_Mirror = false;
    }

    public void setFog(Fog fog) => this.m_Fog = fog;

    public Fog getFog() => this.m_Fog;

    public void setTexture(int index, Texture2D texture)
    {
      this.validateTextureUnit(index);
      this.m_Textures[index] = texture;
    }

    public Texture2D getTexture(int index)
    {
      this.validateTextureUnit(index);
      return this.m_Textures[index];
    }

    public void setMaterial(Material material) => this.m_Material = material;

    public Material getMaterial() => this.m_Material;

    public void setPointSpriteMode(PointSpriteMode pointSpriteMode)
    {
      this.m_PointSpriteMode = pointSpriteMode;
    }

    public PointSpriteMode getPointSpriteMode() => this.m_PointSpriteMode;

    private void validateTextureUnit(int index)
    {
    }

    public int getNumTextures() => this.m_Textures.Length;

    public override int getM3GUniqueClassID() => 3;

    public static Appearance m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 3 ? (Appearance) obj : (Appearance) null;
    }
  }
}
