
// Type: GameManager.MirrorsEdge
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
//using Microsoft.Phone.Info;
//using Microsoft.Phone.Shell;
//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using midp;
using support;
using System;
using System.Collections.Generic;
using text;

#nullable disable
namespace GameManager
{
  public class MirrorsEdge : Game
  {
    public static GraphicsDeviceManager graphics;
    public static GraphicsDevice graphicsDevice;
    public static SpriteBatch spriteBatch;
    public static ContentManager content;
    public static ContentManager defaultImageContent;
    private static Texture2D defaultImage;
    private static long defaultImageTime;
    public static int SCREEN_WIDTH = 800;
    public static int SCREEN_HEIGHT = 480;
    public static bool externalMusic = MediaPlayer.State == MediaState.Playing || !MediaPlayer.GameHasControl;
    public static bool displayMusicQuestion = true;
    public static bool displayTitanWarning = false;
    public static MirrorsEdge m_MirrorsEdge = (MirrorsEdge) null;
    public static bool TrialMode = false;
    public static bool displayTitleUpdateMessage = false;
    public static bool marketplaceCalled = false;
    public static bool active;
    public static bool GS_Supported = true;
    public int[] frame = new int[10];
    public int current_frame;
    public SpriteFont ffont;
    public static MonkeyApp m_monkeyAppMIDlet = (MonkeyApp) null;
    public static DisplayWP7 m_monkeyAppDisplay = (DisplayWP7) null;
    public static bool m_ReturnFromTombstone = false;
    private List<string> dialogButtons;

    public MirrorsEdge()
    {
      MirrorsEdge.m_MirrorsEdge = this;
      MirrorsEdge.graphics = new GraphicsDeviceManager((Game) this);
      this.Content.RootDirectory = "Content";
      MirrorsEdge.content = this.Content;
      MirrorsEdge.defaultImageContent = new ContentManager((IServiceProvider) this.Services, "Content");
      this.TargetElapsedTime = TimeSpan.FromTicks(400000L);
      MirrorsEdge.graphics.PreferredBackBufferWidth = MirrorsEdge.SCREEN_WIDTH;
      MirrorsEdge.graphics.PreferredBackBufferHeight = MirrorsEdge.SCREEN_HEIGHT;
      MirrorsEdge.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
      MirrorsEdge.graphics.PreferMultiSampling = false;

      MirrorsEdge.graphics.IsFullScreen = false;//true; // set it True for w10m/release
      MirrorsEdge.graphics.SynchronizeWithVerticalRetrace = false; // ?

      MirrorsEdge.graphics.GraphicsProfile = GraphicsProfile.HiDef; // experimental

      //MirrorsEdge.graphics.ApplyChanges();
      MirrorsEdge.TrialMode = false;//Guide.IsTrialMode;
      ResourceManager.SetResources();
      this.IsFixedTimeStep = false;
    }

    private void GameDeactivated(object sender, DeactivatedEventArgs e)
    {
    }

    private void GameActivated(object sender, ActivatedEventArgs e)
    {
    }

    private string GetOSVersionString() => "1.0";

    protected override void Initialize()
    {
      if (1==0)//(PhoneApplicationService.Current.StartupMode == null)
      {
        MirrorsEdge.m_ReturnFromTombstone = true;
      }
      else
      {
        MirrorsEdge.m_ReturnFromTombstone = false;
        InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream("gamesett2");
        if (resourceAsStream != null)
        {
          resourceAsStream.close();
        }
        else
        {
            string deviceManufacturer = "Microsoft";//DeviceStatus.DeviceManufacturer;
            MirrorsEdge.displayTitanWarning = false;//DeviceStatus.DeviceName.IndexOf("PI39100") >= 0;
        }
      }
      MirrorsEdge.graphicsDevice = this.GraphicsDevice;
      MirrorsEdge.spriteBatch = new SpriteBatch(MirrorsEdge.graphicsDevice);
      MirrorsEdge.m_monkeyAppMIDlet = new MonkeyApp();
      MirrorsEdge.m_monkeyAppDisplay = new DisplayWP7();
      Runtime.getRuntime().setMIDlet((MIDlet) MirrorsEdge.m_monkeyAppMIDlet, (Display) MirrorsEdge.m_monkeyAppDisplay);
      MirrorsEdge.m_monkeyAppDisplay.showNotify();
      //try
      //{
      //  GamerServicesDispatcher.WindowHandle = this.Window.Handle;
      //  GamerServicesDispatcher.Initialize((IServiceProvider) this.Services);
      //}
      //catch (GamerServicesNotAvailableException ex)
      //{
        MirrorsEdge.GS_Supported = false;
      //}
      //catch (NotSupportedException ex)
      //{
      //  MirrorsEdge.GS_Supported = false;
      //}
      //catch (InvalidOperationException ex)
      //{
      //  MirrorsEdge.GS_Supported = false;
      //}
      base.Initialize();
    }

    protected override void LoadContent()
    {
      LiveProcessor.Init(this.Content, MirrorsEdge.graphics.GraphicsDevice, MirrorsEdge.spriteBatch);
      Runtime.getRuntime().startMIDlet();
      WP7_TouchManager.Init();
      if (!MirrorsEdge.m_ReturnFromTombstone)
        MirrorsEdge.defaultImage = MirrorsEdge.defaultImageContent.Load<Texture2D>("Default");
      this.TryMusicQuestion();
    }

    protected override void UnloadContent()
    {
      Runtime.getRuntime().destroyMIDlet((MIDlet) MirrorsEdge.m_monkeyAppMIDlet);
      MirrorsEdge.m_monkeyAppMIDlet.Destructor();
      MirrorsEdge.m_monkeyAppMIDlet = (MonkeyApp) null;
      MirrorsEdge.m_monkeyAppDisplay.Destructor();
      MirrorsEdge.m_monkeyAppDisplay = (DisplayWP7) null;
    }

    protected void UpdateDialogGetMBResult1(IAsyncResult userResult)
    {
      int? nullable = default;//Guide.EndShowMessageBox(userResult);
      if (nullable.HasValue)
      {
        if (nullable.Value > 0)
        {
          MirrorsEdge.externalMusic = false;
          MediaPlayer.Stop();
        }
        MirrorsEdge.displayMusicQuestion = false;
        MirrorsEdge.defaultImageTime = DateTime.Now.Ticks / 10000L;
        this.checkTitanWarning();
      }
      else
      {
        MirrorsEdge.displayMusicQuestion = false;
        MirrorsEdge.defaultImageTime = DateTime.Now.Ticks / 10000L;
        this.checkTitanWarning();
      }
    }

    private void TryMusicQuestion()
    {
      if (MirrorsEdge.externalMusic && !MirrorsEdge.m_ReturnFromTombstone)
      {
        this.dialogButtons = new List<string>();
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        this.dialogButtons.Add(LocaleManager.getInstance().getString(2054));

        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        this.dialogButtons.Add(LocaleManager.getInstance().getString(2053));

        // ISSUE: reference to a compiler-generated method
       
        //Guide.BeginShowMessageBox(LocaleManager.getInstance().getString(2264), LocaleManager.getInstance().getString(2391),
        //    (IEnumerable<string>) this.dialogButtons, 1, MessageBoxIcon.None, new AsyncCallback(this.UpdateDialogGetMBResult1), (object) null);
      }
      else
      {
        MirrorsEdge.displayMusicQuestion = false;
        MirrorsEdge.defaultImageTime = DateTime.Now.Ticks / 10000L;
        this.checkTitanWarning();
      }
    }

    protected void UpdateDialogGetMBResult3(IAsyncResult userResult)
    {
      int num = default;//Guide.EndShowMessageBox(userResult).HasValue ? 1 : 0;
      MirrorsEdge.displayTitanWarning = false;
    }

    private void checkTitanWarning()
    {
      if (!MirrorsEdge.displayTitanWarning)
        return;
      this.dialogButtons = new List<string>();
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      this.dialogButtons.Add(LocaleManager.getInstance().getString(2094));
    }

    protected override void Update(GameTime gameTime)
    {
      if (!MirrorsEdge.displayMusicQuestion)
      {
        if (!MirrorsEdge.displayTitleUpdateMessage)
        {
          if (!MirrorsEdge.displayTitanWarning)
          {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
              if (LiveProcessor.gamestate <= LiveProcessor.GameState.WaitingForAchivements && LiveProcessor.gamestate != LiveProcessor.GameState.UpdateNeeded)
                this.Exit();
              else
                Runtime.getRuntime().OnHardBackKeyEvent();
            }
            if (MirrorsEdge.defaultImage != null && DateTime.Now.Ticks / 10000L - MirrorsEdge.defaultImageTime > 2000L)
            {
              MirrorsEdge.defaultImage = (Texture2D) null;
              MirrorsEdge.defaultImageContent.Unload();
              MirrorsEdge.defaultImageContent = (ContentManager) null;
            }
            if (MirrorsEdge.defaultImage == null)
            {
              LiveProcessor.OnUpdate();
              if (LiveProcessor.gamestate <= LiveProcessor.GameState.WaitingForAchivements)
              {
                if (LiveProcessor.gamestate != LiveProcessor.GameState.UpdateNeeded)
                  goto label_13;
              }
              WP7_TouchManager.Process();
              MirrorsEdge.m_monkeyAppMIDlet.update();
            }
          }
        }
      }
label_13:
      try
      {
        if (MirrorsEdge.GS_Supported && !MirrorsEdge.displayMusicQuestion && !MirrorsEdge.displayTitanWarning 
            && LiveProcessor.gamestate != LiveProcessor.GameState.UpdateNeeded)
        { 
            //GamerServicesDispatcher.Update();
        }
         
        base.Update(gameTime);
      }
      catch (Microsoft.Xna.Framework.GameUpdateRequiredException ex)
      {
        this.HandleGameUpdateRequired(ex);
      }
    }

    private void HandleGameUpdateRequired(Microsoft.Xna.Framework.GameUpdateRequiredException e)
    {
      if (!MirrorsEdge.GS_Supported)
        return;
      MirrorsEdge.displayTitleUpdateMessage = true;
      LiveProcessor.gamestate = LiveProcessor.GameState.UpdateNeeded;
      this.dialogButtons = new List<string>();
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      this.dialogButtons.Add(LocaleManager.getInstance().getString(2054));
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      this.dialogButtons.Add(LocaleManager.getInstance().getString(2053));
    }

    protected void UpdateDialogGetMBResult2(IAsyncResult userResult)
    {
      int? nullable = default;//Guide.EndShowMessageBox(userResult);
      if (nullable.HasValue)
      {
        if (nullable.Value > 0)
        {
            //if (Guide.IsTrialMode)
            //    Guide.ShowMarketplace(PlayerIndex.One);
            //else
            //{
            //    new MarketplaceDetailTask()
            //    {
            //        ContentType = ((MarketplaceContentType)1)
            //    }.Show();
            //}

          MirrorsEdge.marketplaceCalled = true;
        }
        MirrorsEdge.displayTitleUpdateMessage = false;
      }
      else
        MirrorsEdge.displayTitleUpdateMessage = false;
      MirrorsEdge.defaultImageTime = DateTime.Now.Ticks / 10000L;
    }

    protected override void EndRun()
    {
      //SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
      //if (MirrorsEdge.TrialMode && signedInGamer != null && signedInGamer.Privileges.AllowPurchaseContent)
      //  Guide.ShowMarketplace(PlayerIndex.One);
      base.EndRun();
    }

    protected override void Draw(GameTime gameTime)
    {
      if (!MirrorsEdge.displayMusicQuestion)
      {
        if (MirrorsEdge.displayTitleUpdateMessage)
        {
          if (1==0)//(!Guide.IsVisible)
          {
            // ISSUE: reference to a compiler-generated method
         
            //Guide.BeginShowMessageBox(LocaleManager.getInstance().getString(2393), LocaleManager.getInstance().getString(2394),
            //    (IEnumerable<string>) this.dialogButtons, 1, MessageBoxIcon.Alert, new AsyncCallback(this.UpdateDialogGetMBResult2), (object) null);
          }
          else
            this.GraphicsDevice.Clear(new Color(84, 84, 84));
        }
        else if (MirrorsEdge.displayTitanWarning)
        {
          if (1==0)//(!Guide.IsVisible)
          {
            // ISSUE: reference to a compiler-generated method
          
            //Guide.BeginShowMessageBox(LocaleManager.getInstance().getString(2447), 
            //    LocaleManager.getInstance().getString(2446), (IEnumerable<string>) this.dialogButtons, 0, MessageBoxIcon.None, new AsyncCallback(this.UpdateDialogGetMBResult3), (object) null);
          }
          else
            this.GraphicsDevice.Clear(new Color(84, 84, 84));
        }
        else if (MirrorsEdge.defaultImage != null)
        {
          MirrorsEdge.spriteBatch.Begin();
          MirrorsEdge.spriteBatch.Draw(MirrorsEdge.defaultImage, new Vector2(0.0f, 0.0f), Color.White);
          MirrorsEdge.spriteBatch.End();
        }
        else if (LiveProcessor.gamestate <= LiveProcessor.GameState.WaitingForAchivements && LiveProcessor.gamestate != LiveProcessor.GameState.UpdateNeeded)
        {
          this.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
          this.GraphicsDevice.Clear(Color.Black);
          LiveProcessor.Draw(0);
        }
        else
        {
          this.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
          MirrorsEdge.m_monkeyAppDisplay.refresh();
        }
      }
      else
        this.GraphicsDevice.Clear(new Color(84, 84, 84));
      base.Draw(gameTime);
    }

    protected override void OnActivated(object sender, EventArgs args)
    {
      if (!MirrorsEdge.active)
      {
        MirrorsEdge.active = true;
        if (!MirrorsEdge.externalMusic && !MediaPlayer.GameHasControl)
          MirrorsEdge.externalMusic = true;
        WP7_TouchManager.Init();

        try
        {
            Runtime.getRuntime().startMIDlet();
        }
        catch { }
      }
      base.OnActivated(sender, args);
      if (!MirrorsEdge.marketplaceCalled && (!MirrorsEdge.externalMusic || MirrorsEdge.m_ReturnFromTombstone || !MirrorsEdge.displayMusicQuestion))
        return;
      MirrorsEdge.marketplaceCalled = false;
      MirrorsEdge.displayMusicQuestion = false;
      MirrorsEdge.defaultImageTime = DateTime.Now.Ticks / 10000L;
    }

    protected override void OnDeactivated(object sender, EventArgs args)
    {
      if (MirrorsEdge.active)
      {
        MirrorsEdge.active = false;
        Runtime.getRuntime().pauseMIDlet();
      }
      base.OnDeactivated(sender, args);
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
      AppEngine.getCanvas().saveRMSAppSettings();
      base.OnExiting(sender, args);
    }

    public void suspendAudio()
    {
    }

    public void resumeAudio()
    {
    }
  }

    internal class ActivatedEventArgs
    {
    }

    internal class DeactivatedEventArgs
    {
    }
}
