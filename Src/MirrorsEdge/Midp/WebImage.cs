
// Type: midp.WebImage
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace midp
{
  internal class WebImage : Image
  {
    protected string m_url;

    public WebImage(string url)
      : base(false)
    {
      this.m_url = url;
    }

    public override void getRGB(
      ref int[] rgbData,
      int offset,
      int scanlength,
      int x,
      int y,
      int width,
      int height)
    {
      rgbData = (int[]) null;
    }

    public virtual void run()
    {
    }

    public void SetResult(WebImage.WebImageResult result)
    {
    }

    public WebImage.WebImageResult GetResult() => WebImage.WebImageResult.WIRESULT_FAILED;

    public enum WebImageResult
    {
      WIRESULT_PENDING,
      WIRESULT_SUCCESS,
      WIRESULT_FAILED,
    }
  }
}
