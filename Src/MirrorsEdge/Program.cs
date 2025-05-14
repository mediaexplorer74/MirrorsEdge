using System;

namespace mirrorsedge_wp7
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new MirrorsEdge())
                game.Run();
        }
    }

}
