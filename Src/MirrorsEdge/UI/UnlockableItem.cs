
// Type: UI.UnlockableItem
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using Microsoft.Xna.Framework.Media;
using midp;
using support;
using System;
using System.IO;
using text;

#nullable disable
namespace UI
{
  public class UnlockableItem : WindowElement
  {
    public const int WIDTH = 170;
    public const int HEIGHT = 114;
    public const int THUMB_WIDTH = 160;
    public const int THUMB_HEIGHT = 107;
    protected int m_quadId;
    protected int m_saveableId;
    protected string m_descriptionString;
    protected string m_lockedString;
    protected int m_numberOfBadges;
    protected int m_thumbWidth;
    protected int m_thumbHeight;
    protected int m_thumbX;
    protected int m_thumbY;

    public UnlockableItem(int quadId, int saveableId, int itemDesc, int numBadges)
    {
      this.m_quadId = quadId;
      this.m_saveableId = saveableId;
      this.m_descriptionString = (string) null;
      this.m_lockedString = (string) null;
      this.m_numberOfBadges = numBadges;
      this.m_thumbX = 0;
      this.m_thumbY = 0;
      this.m_thumbWidth = 0;
      this.m_thumbHeight = 0;
      AppEngine canvas = AppEngine.getCanvas();
      TextManager textManager = canvas.getTextManager();
      QuadManager quadManager = canvas.getQuadManager();
      this.m_descriptionString = textManager.getString(itemDesc);
      string string1 = string.Concat((object) numBadges);
      textManager.dynamicString(-12, 2304, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      this.m_lockedString = stringBuffer.toString();
      int meshWidth = quadManager.getMeshWidth(this.m_quadId);
      int meshHeight = quadManager.getMeshHeight(this.m_quadId);
      if (meshWidth > meshHeight)
      {
        float num = 160f / (float) meshWidth;
        this.m_thumbWidth = (int) ((double) meshWidth * (double) num);
        this.m_thumbHeight = (int) ((double) meshHeight * (double) num);
      }
      else
      {
        float num = 107f / (float) meshHeight;
        this.m_thumbWidth = (int) ((double) meshWidth * (double) num);
        this.m_thumbHeight = (int) ((double) meshHeight * (double) num);
      }
      this.setWidth(170);
      this.setHeight(114);
    }

    public bool isUnlocked()
    {
      return AppEngine.getAchievementData().getBadgeCount() >= this.m_numberOfBadges;
    }

    public int getQuadId()
    {
      return this.isUnlocked() ? this.m_quadId : (int) QuadManager.get("MESH_UNLOCKABLE_LOCKED");
    }

    public string getDescription()
    {
      return this.isUnlocked() ? this.m_descriptionString : this.m_lockedString;
    }

    public int getThumbWidth() => this.m_thumbWidth;

    public int getThumbHeight() => this.m_thumbHeight;

    public int getThumbX() => this.m_thumbX;

    public int getThumbY() => this.m_thumbY;

    
    public void saveToLibrary()
    {
        WP7InputStream wp7InputStream = new WP7InputStream("res/" + ResourceManager.ID_TO_FILENAME(this.m_saveableId) + ".jpg");
        if (!wp7InputStream.loadSuccessful())
            return;

        using (var pictureStream = wp7InputStream.getWP7Stream())
        {
            if (pictureStream != null)
            {
                var pictureName = "Mirror'sEdge_" + ResourceManager.ID_TO_FILENAME(this.m_saveableId) + ".jpg";
                var pictureBytes = new byte[pictureStream.Length];
                pictureStream.Read(pictureBytes, 0, pictureBytes.Length);

                // Save the picture using a custom implementation since MediaLibrary does not have SavePicture
                SavePictureToMediaLibrary(pictureName, pictureBytes);
            }
        }
        wp7InputStream.close();
    }

        private void SavePictureToMediaLibrary(string pictureName, byte[] pictureBytes)
        {
            // Custom implementation to save the picture
            using (var mediaLibrary = new MediaLibrary())
            {
                // Replace Environment.GetFolderPath with a hardcoded path or a valid method to retrieve the Pictures folder
                var picturesFolderPath = System.IO.Path.Combine("C:\\Users\\Public\\Pictures", pictureName);
                File.WriteAllBytes(picturesFolderPath, pictureBytes);
                // Optionally, you can log or notify the user that the picture was saved successfully
            }
        }


    public override void render(Graphics g, int top, int left)
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      this.m_thumbX = left + this.m_x + 5;
      this.m_thumbY = top + this.m_y + 3;
      int quadId = this.getQuadId();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_UNLOCKABLES"), true);
      quadManager.setMeshVisible(quadId, true);
      quadManager.setMeshBounds(quadId, (float) this.m_thumbX, (float) this.m_thumbY, (float) this.m_thumbWidth, (float) this.m_thumbHeight, 9);
      quadManager.render(g);
      quadManager.setMeshVisible(quadId, false);
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_UNLOCKABLES"), false);
    }
  }
}
