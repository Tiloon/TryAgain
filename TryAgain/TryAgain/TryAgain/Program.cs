using System;
using TryAgain.Sounds;
using TryAgain.Online;

namespace TryAgain
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Connection.Connect(); 
            using (Game1 game = new Game1())
            {
                game.Run();
            }
            Themes.Stop();
            Connection.Stop();
        }
    }
#endif
}

