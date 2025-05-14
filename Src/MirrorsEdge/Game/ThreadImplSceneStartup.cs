
// Type: game.ThreadImplSceneStartup
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class ThreadImplSceneStartup
  {
    public static void Start(object o) => (o as SceneStartup).Run();
  }
}
