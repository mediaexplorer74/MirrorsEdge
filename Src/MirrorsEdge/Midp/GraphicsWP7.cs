
// Type: midp.GraphicsWP7
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;
using System;

#nullable disable
namespace midp
{
  public class GraphicsWP7 : midp.Graphics
  {
    private DisplayWP7 m_display;
    private RenderTarget2D m_target;
    private RenderTarget2D m_prev_target;
    private float m_scale;
    private int m_clipX;
    private int m_clipY;
    private int m_clipWidth;
    private int m_clipHeight;
    private bool clip;
    private SpriteBatch spriteBatch;
    private Microsoft.Xna.Framework.Graphics.Texture2D blank;
    private RasterizerState clipState;
    private RasterizerState noClipState;
    private Color currentColor = Color.White;
    private int lineWidth = 1;

    public GraphicsWP7(DisplayWP7 display)
    {
      this.m_display = display;
      this.spriteBatch = MirrorsEdge.spriteBatch;
      this.blank = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
      this.blank.SetData<Color>(new Color[1]{ Color.White });
      this.clipState = new RasterizerState();
      this.clipState.CullMode = CullMode.CullCounterClockwiseFace;
      this.clipState.ScissorTestEnable = true;
      this.noClipState = new RasterizerState();
      this.noClipState.CullMode = CullMode.CullCounterClockwiseFace;
      this.noClipState.ScissorTestEnable = false;
      this.clip = false;
      this.m_target = (RenderTarget2D) null;
      this.m_prev_target = (RenderTarget2D) null;
      this.m_scale = MirrorsEdge.Scaling;//1.5f;
      this.m_clipX = 0;
      this.m_clipY = 0;
      this.m_clipWidth = this.m_display.getWidth();
      this.m_clipHeight = this.m_display.getHeight();
    }

    public GraphicsWP7(RenderTarget2D target)
    {
      this.m_display = (DisplayWP7) null;
      this.spriteBatch = MirrorsEdge.spriteBatch;
      this.blank = new Microsoft.Xna.Framework.Graphics.Texture2D(MirrorsEdge.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
      this.blank.SetData<Color>(new Color[1]{ Color.White });
      this.clipState = new RasterizerState();
      this.clipState.CullMode = CullMode.CullCounterClockwiseFace;
      this.clipState.ScissorTestEnable = true;
      this.noClipState = new RasterizerState();
      this.noClipState.CullMode = CullMode.CullCounterClockwiseFace;
      this.noClipState.ScissorTestEnable = false;
      this.clip = false;
      this.m_target = target;
      this.m_prev_target = (RenderTarget2D) null;
      this.m_scale = MirrorsEdge.Scaling;//2f;
      this.m_clipX = 0;
      this.m_clipY = 0;
      this.m_clipWidth = (int) ((double) this.m_target.Width / (double) this.m_scale);
      this.m_clipHeight = (int) ((double) this.m_target.Height / (double) this.m_scale);
    }

    private void setCurrentTarget() => this.setCurrentTarget(false);

    private void setCurrentTarget(bool forText)
    {
      RenderTargetBinding[] renderTargets = MirrorsEdge.graphicsDevice.GetRenderTargets();
      this.m_prev_target = (RenderTarget2D) null;
      
      if (renderTargets != null && renderTargets.Length > 0)
        this.m_prev_target = renderTargets[0].RenderTarget as RenderTarget2D;

      if (this.m_prev_target != this.m_target)
        MirrorsEdge.graphicsDevice.SetRenderTarget(this.m_target);
      
      if (this.m_target != null)
        this.setScissorRectangle();
      
      Matrix translation = Matrix.CreateTranslation((float) this.getTranslateX(), (float) this.getTranslateY(), 0.0f);
      if (!forText)
        translation *= Matrix.CreateScale(this.m_scale, this.m_scale, 1f);

            // this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, default, default,
            //     this.clip ? this.clipState : this.noClipState, (Effect)null);//, translation);

            this.spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, 
                SamplerState.PointClamp,
                        null,
                        this.clip ? this.clipState : this.noClipState, 
                        null, 
                        MirrorsEdge.globalTransformation);
        }

    private void resetCurrentTarget()
    {
      this.spriteBatch.End();
      Graphics3D.render_target.clearAppearanceCache();
      if (this.m_target != this.m_prev_target)
        MirrorsEdge.graphicsDevice.SetRenderTarget(this.m_prev_target);
      this.m_prev_target = (RenderTarget2D) null;
    }

    public override void clipRect(int x, int y, int width, int height)
    {
      int x1 = Math.Max(this.m_clipX, x);
      int num1 = Math.Min(this.m_clipX + this.m_clipWidth, x + width);
      int y1 = Math.Max(this.m_clipY, y);
      int num2 = Math.Min(this.m_clipY + this.m_clipHeight, y + height);
      this.setClip(x1, y1, num1 - x1, num2 - y1);
    }

    public override void copyArea(
      int x_src,
      int y_src,
      int width,
      int height,
      int x_dest,
      int y_dest,
      int anchor)
    {
    }

    public override void drawArc(
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int arcAngle)
    {
      if (arcAngle < 360)
        return;
      int num1 = x + width / 3;
      int num2 = x + (width << 1) / 3;
      int num3 = x + width;
      int num4 = y + height / 3;
      int num5 = y + (height << 1) / 3;
      int num6 = y + height;
      this.drawLine(num1, y, num2, y);
      this.drawLine(num2, y, num3, num4);
      this.drawLine(num3, num4, num3, num5);
      this.drawLine(num3, num5, num2, num6);
      this.drawLine(num2, num6, num1, num6);
      this.drawLine(num1, num6, x, num5);
      this.drawLine(x, num5, x, num4);
      this.drawLine(x, num4, num1, y);
    }

    public override void drawChar(char character, int x, int y, int anchor)
    {
    }

    public override void drawChars(
      char[] data,
      int offset,
      int length,
      int x,
      int y,
      int anchor)
    {
    }

    public override void drawImage(Image img, int xConst, int yConst, int anchor)
    {
      int width = img.getWidth();
      int height = img.getHeight();
      this.drawRegion(img, 0, 0, width, height, 0, xConst, yConst, anchor);
    }

    public override void drawLine(int x1Const, int y1Const, int x2Const, int y2Const)
    {
      float x1 = (float) x1Const;
      float x2 = (float) x2Const;
      float y1 = (float) y1Const;
      float y2 = (float) y2Const;
      if ((double) x2 < (double) x1)
      {
        float num1 = x2;
        x2 = x1;
        x1 = num1;
        float num2 = y2;
        y2 = y1;
        y1 = num2;
      }
      float rotation = (float) Math.Atan2((double) y2 - (double) y1, (double) x2 - (double) x1);
      float x3 = Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2));
      this.setCurrentTarget();
      this.spriteBatch.Draw(this.blank, new Vector2(x1, y1), new Rectangle?(), this.currentColor, rotation, Vector2.Zero, 
          new Vector2(x3, (float) this.lineWidth), SpriteEffects.None, 0.0f);
      this.resetCurrentTarget();
    }

    public override void drawRect(int x, int y, int width, int height)
    {
      this.setCurrentTarget();
      this.spriteBatch.Draw(this.blank, new Vector2((float) x, (float) y), new Rectangle?(), this.currentColor,
          0.0f, Vector2.Zero, new Vector2((float) width, (float) this.lineWidth), SpriteEffects.None, 0.0f);

      this.spriteBatch.Draw(this.blank, new Vector2((float) x, (float) (y + height)), new Rectangle?(), this.currentColor,
          0.0f, Vector2.Zero, new Vector2((float) width, (float) this.lineWidth), SpriteEffects.None, 0.0f);

      this.spriteBatch.Draw(this.blank, new Vector2((float) x, (float) y), new Rectangle?(), this.currentColor, 
          1.57079637f, Vector2.Zero, new Vector2((float) height, (float) this.lineWidth), SpriteEffects.None, 0.0f);

      this.spriteBatch.Draw(this.blank, new Vector2((float) (x + width), (float) y), new Rectangle?(), this.currentColor, 
          1.57079637f, Vector2.Zero, new Vector2((float) height, (float) this.lineWidth), SpriteEffects.None, 0.0f);
      this.resetCurrentTarget();
    }

    public override void drawRegion(
      Image src,
      int x_src,
      int y_src,
      int width,
      int height,
      int transform,
      int x_dest,
      int y_dest,
      int anchor)
    {
      if (src == null)
        return;
      if ((anchor & 2) != 0)
        x_dest -= width >> 1;
      else if ((anchor & 4) != 0)
        x_dest -= width;
      if ((anchor & 16) != 0)
        y_dest -= height >> 1;
      else if ((anchor & 32) != 0)
        y_dest -= height;
      int destLeft = x_dest;
      int destTop = y_dest;
      int destRight = x_dest + width;
      int destBottom = y_dest + height;
      int srcLeft = x_src;
      int srcTop = y_src;
      int srcRight = x_src + width;
      int srcBottom = y_src + height;
      if (transform == 2)
      {
        int num = destLeft;
        destLeft = destRight;
        destRight = num;
      }
      this.drawScaledRegion(src, srcLeft, srcTop, srcRight, srcBottom, destLeft, destTop, destRight, destBottom);
    }

    public override void drawScaledRegion(
      Image src,
      int src_left,
      int src_top,
      int src_right,
      int src_bottom,
      int dest_left,
      int dest_top,
      int dest_right,
      int dest_bottom)
    {
      if (!(src is ImageWP7))
        return;
      this.setCurrentTarget();
      SpriteEffects effects = SpriteEffects.None;
      if (dest_left > dest_right)
      {
        effects = SpriteEffects.FlipHorizontally;
        int num = dest_left;
        dest_left = dest_right;
        dest_right = num;
      }
      this.spriteBatch.Draw((src as ImageWP7).m_texture, new Rectangle(dest_left, dest_top, dest_right - dest_left, dest_bottom - dest_top), 
          new Rectangle?(new Rectangle(src_left, src_top, src_right - src_left, src_bottom - src_top)), Color.White, 0.0f, new Vector2(0.0f, 0.0f), effects, 0.0f);
      this.resetCurrentTarget();
    }

    public override void drawRGB(
      int[] rgbData,
      int offset,
      int scanlength,
      int x,
      int y,
      int width,
      int height,
      bool processAlpha)
    {
    }

    public override void drawRoundRect(
      int x,
      int y,
      int width,
      int height,
      int arcWidth,
      int arcHeight)
    {
    }

    public override void drawString(string str, int x, int y, int anchor)
    {
      this.drawString(str, x, y, anchor, 0);
    }

    public override void drawString(string str, int xConst, int yConst, int anchor, int flags)
    {
      if (str.Length == 0)
        return;
      FontWP7Font font = this.getFont() as FontWP7Font;
      int num1 = font.stringWidth(str);
      int num2 = 0;
      if ((anchor & 2) != 0)
        num2 = -(num1 >> 1);
      if ((anchor & 4) != 0)
        num2 = -num1;
      this.setCurrentTarget(true);
      font.drawString(this.spriteBatch, str, (float) (xConst + num2), (float) yConst, this.currentColor);
      this.resetCurrentTarget();
    }

    public override void drawSubstring(
      string str,
      int offset,
      int len,
      int x,
      int y,
      int anchor)
    {
      this.drawSubstring(str, offset, len, x, y, anchor, 0);
    }

    public override void drawSubstring(
      string str,
      int offset,
      int len,
      int x,
      int y,
      int anchor,
      int flags)
    {
      this.drawString(str.Substring(offset, len), x, y, anchor, flags);
    }

    public override void fillArc(
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int arcAngle)
    {
    }

    public override void fillRect(int x, int y, int width, int height)
    {
      this.setCurrentTarget();
      this.spriteBatch.Draw(this.blank, new Vector2((float) x, (float) y), new Rectangle?(), 
          this.currentColor, 0.0f, Vector2.Zero, new Vector2((float) width, (float) height), 
          SpriteEffects.None, 0.0f);
      this.resetCurrentTarget();
    }

    public override void fillRoundRect(
      int x,
      int y,
      int width,
      int height,
      int arcWidth,
      int arcHeight)
    {
    }

    public override void fillTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
    {
    }

    public override int getClipHeight() => this.m_clipHeight;

    public override int getClipWidth() => this.m_clipWidth;

    public override int getClipX() => this.m_clipX;

    public override int getClipY() => this.m_clipY;

    public override int getDisplayColor(int color) => color;

    public override int getGrayScale() => 0;

    public override int getStrokeStyle() => 0;

    public override void setClip(int x_, int y_, int width_, int height_)
    {
      int num1 = x_;
      int num2 = y_;
      int num3 = width_;
      int num4 = height_;
      int width;
      int height;
      if (this.m_display != null)
      {
        Displayable current = this.m_display.getCurrent();
        width = current.getWidth();
        height = current.getHeight();
      }
      else
      {
        width = this.m_target.Width;
        height = this.m_target.Height;
      }
      this.m_clipX = num1;
      this.m_clipY = num2;
      this.m_clipWidth = num3;
      this.m_clipHeight = num4;
      int num5 = (int)(num1 * Runtime.pixelScale);
      int num6 = (int)(num2 * Runtime.pixelScale);
      int num7 = (int)(num3 * Runtime.pixelScale);
      int num8 = (int)(num4 * Runtime.pixelScale);
      int num9 = (int)(width * Runtime.pixelScale);
      int num10 = (int)(height * Runtime.pixelScale);
      if (this.m_display == null)
        return;
      this.setScissorRectangle();
    }

    private void setScissorRectangle()
    {
      int num1;
      int num2;
      if (this.m_display != null)
      {
        Displayable current = this.m_display.getCurrent();
        num1 = current.getWidth();
        num2 = current.getHeight();
      }
      else
      {
        num1 = (int) ((double) this.m_target.Width / (double) this.m_scale);
        num2 = (int) ((double) this.m_target.Height / (double) this.m_scale);
      }
      if (this.m_clipX <= 0 && this.m_clipY <= 0 && this.m_clipWidth >= num1 && this.m_clipHeight >= num2)
      {
        MirrorsEdge.graphicsDevice.ScissorRectangle = this.m_display == null
                    ? new Rectangle(0, 0, this.m_target.Width, this.m_target.Height)
                    : new Rectangle(0, 0, (int)MirrorsEdge.SCREEN_WIDTH, (int)MirrorsEdge.SCREEN_HEIGHT);
        this.clip = false;
      }
      else
      {
        MirrorsEdge.graphicsDevice.ScissorRectangle = new Rectangle((int) ((double) this.m_scale * (double) this.m_clipX), 
            (int) ((double) this.m_scale * (double) this.m_clipY), (int) ((double) this.m_scale * (double) this.m_clipWidth), 
            (int) ((double) this.m_scale * (double) this.m_clipHeight));
        this.clip = true;
      }
    }

    public override void setStrokeStyle(int style)
    {
    }

    public override void translate(int x, int y) => base.translate(x, y);

    public Renderer createRenderer() => (Renderer) null;

    public override void setColor(int red, int green, int blue, int alpha)
    {
      base.setColor(red, green, blue, alpha);
      this.currentColor = new Color(red, green, blue, alpha);
    }

    public override void bind2D()
    {
    }

    public float getScale() => this.m_scale;
  }
}
