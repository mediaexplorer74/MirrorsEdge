
// Type: game.GameObjectRivalGeneral
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using support;

#nullable disable
namespace game
{
  public class GameObjectRivalGeneral(
    MEdgeMap map,
    float posX,
    float posY,
    float posZ,
    int pathId) : GameObjectRival(map, 4, (int) M3GAssets.get("MODEL_RIVAL"), 0, posX, posY, posZ, pathId)
  {
  }
}
