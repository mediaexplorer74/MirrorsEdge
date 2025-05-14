// Decompiled with JetBrains decompiler
// Type: midp.PlayerListener
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace midp
{
  public abstract class PlayerListener : meObject
  {
    public const string CLOSED = "closed";
    public const string DEVICE_AVAILABLE = "deviceAvailable";
    public const string DEVICE_UNAVAILABLE = "deviceUnavailable";
    public const string DURATION_UPDATED = "durationUpdated";
    public const string END_OF_MEDIA = "endOfMedia";
    public const string ERROR = "error";
    public const string STARTED = "started";
    public const string STOPPED = "stopped";
    public const string VOLUME_CHANGED = "volumeChanged";

    public override void Destructor()
    {
    }

    public override meClass getClass() => throw new NotImplementedException();

    public abstract void playerUpdate(Player player, string _event, object eventData);
  }
}
