
// Type: support.WP7_Accelerometer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


//using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace support
{
  public class WP7_Accelerometer
  {
    private static WP7_Accelerometer singleton;
    private AccelerationSample[] sample;
    private Vector3 m_Acceleration;
    private AccelerationSample[] m_Buffer;
    private int m_Buffer_ptr;
    private int m_Buffer_length;
    private DateTimeOffset m_lastTime;
    private float m_samplesPerSecond;
    private Accelerometer accelerometer;
    private Vector3 accelerometerReading = new Vector3();
    private object accelerometerLockObject = new object();

    public static WP7_Accelerometer getAccelerometerWP7()
    {
      if (WP7_Accelerometer.singleton == null)
      {
        WP7_Accelerometer.singleton = new WP7_Accelerometer();
        WP7_Accelerometer.singleton.SetFrequency(30f);
        WP7_Accelerometer.singleton.SetBufferSize(1);
      }
      return WP7_Accelerometer.singleton;
    }

    public void SetFrequency(float samplesPerSecond)
    {
      this.m_samplesPerSecond = samplesPerSecond;
      if ((double) samplesPerSecond != 0.0)
        this.accelerometer.Start();
      else
        this.accelerometer.Stop();
    }

    public float GetFrequency() => this.m_samplesPerSecond;

    public void SetBufferSize(int samples) => this.m_Buffer = new AccelerationSample[samples];

    public int GetSamples(int samples, ref AccelerationSample[] buffer)
    {
      lock (this.accelerometerLockObject)
      {
        int num = samples;
        int degrees_ccw = 0;
        int index = 0;
        if (buffer == null || buffer.Length < samples)
          buffer = new AccelerationSample[samples];
        buffer[0] = new AccelerationSample();
        buffer[0].acceleration = new Vector3(0.0f, 0.0f, -1f);
        for (; samples != 0 && this.m_Buffer_length > 0; --this.m_Buffer_length)
        {
          --this.m_Buffer_ptr;
          if (this.m_Buffer_ptr < 0)
            this.m_Buffer_ptr = this.m_Buffer.Length - 1;
          AccelerationSample accelerationSample = this.m_Buffer[this.m_Buffer_ptr];
          buffer[index].timestep = accelerationSample.timestep;
          buffer[index].acceleration = WP7_Accelerometer.mapAcceleration(degrees_ccw, ref accelerationSample.acceleration);
          ++index;
          --samples;
        }
        return num - samples;
      }
    }

    public void getRawXYZ(ref float rawX, ref float rawY, ref float rawZ)
    {
      if (this.GetSamples(1, ref this.sample) > 0)
        this.m_Acceleration = this.sample[0].acceleration;
      rawX = this.m_Acceleration.X;
      rawY = this.m_Acceleration.Y;
      rawZ = this.m_Acceleration.Z;
    }

    private WP7_Accelerometer()
    {
      this.accelerometer = new Accelerometer();
      if (this.accelerometer.State == 1)
        this.accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(this.AccelerometerReadingChanged);
      this.m_samplesPerSecond = 0.0f;
      this.m_Buffer = new AccelerationSample[1];
      this.m_Buffer_length = 0;
      this.m_Buffer_ptr = 0;
      this.m_lastTime = new DateTimeOffset(0L, new TimeSpan(0L));
    }

    private static Vector3 mapAcceleration(int degrees_ccw, ref Vector3 acceleration)
    {
      switch (degrees_ccw)
      {
        case -90:
          return new Vector3(acceleration.Y, -acceleration.X, acceleration.Z);
        case 0:
          return acceleration;
        case 90:
          return new Vector3(-acceleration.Y, acceleration.X, acceleration.Z);
        case 180:
          return new Vector3(-acceleration.X, -acceleration.Y, acceleration.Z);
        default:
          return acceleration;
      }
    }

    private void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs args)
    {
      lock (this.accelerometerLockObject)
      {
        this.accelerometerReading.X = (float) args.X;
        this.accelerometerReading.Y = (float) args.Y;
        this.accelerometerReading.Z = (float) args.Z;
        this.m_Buffer[this.m_Buffer_ptr] = new AccelerationSample();
        this.m_Buffer[this.m_Buffer_ptr].acceleration = new Vector3(this.accelerometerReading.X, this.accelerometerReading.Y, this.accelerometerReading.Z);
        this.m_Buffer[this.m_Buffer_ptr].timestep = Math.Max(0, (int) ((double) (args.Timestamp.Ticks - this.m_lastTime.Ticks) / 10000.0));
        this.m_lastTime = args.Timestamp;
        ++this.m_Buffer_ptr;
        if (this.m_Buffer_ptr >= this.m_Buffer.Length)
          this.m_Buffer_ptr = 0;
        ++this.m_Buffer_length;
        if (this.m_Buffer_length <= this.m_Buffer.Length)
          return;
        this.m_Buffer_length = this.m_Buffer.Length;
      }
    }
  }

    internal class AccelerometerReadingEventArgs
    {
        internal float X;
        internal float Y;
        internal float Z;
        internal DateTimeOffset Timestamp;
    }

    internal class Accelerometer
    {
        internal int State;
        internal EventHandler<AccelerometerReadingEventArgs> ReadingChanged;

        internal void Start()
        {
            throw new NotImplementedException();
        }

        internal void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
