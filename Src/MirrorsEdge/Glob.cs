using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace GameManager
{

    public static class Glob
    {
        public static float Time { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static Point WindowSize { get; set; }

        public static void Update(GameTime gt)
        {
            double ts = gt.ElapsedGameTime.TotalSeconds;
            Time = (float)ts; 
        }            

    }
}
