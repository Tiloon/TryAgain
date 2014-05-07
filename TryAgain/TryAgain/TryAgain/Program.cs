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
        static void Main(string[] args) // ROFL OMG JE SUIS TROP UTILE ET JE SAIS UTILISER GIT TRES BIEN OUI MONSIEUR
        {
            if (args.Length == 0)
            {
                Connection.Init("user.json");
            }
            else
            {
                Connection.Init(args[0] + ".json");
            }
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

