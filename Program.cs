using System;
using SplashKitSDK;

namespace Swinburne_OOP_HD
{
    public class Program
    {
        public static void Main()
        {
            Window newWindow = new Window("Just A Normal Dungeon", 800, 600);
            Player player1 = new Player(300, 300);

            do
            {
                SplashKit.ClearScreen();
                SplashKit.ProcessEvents();

                // add codes here
                player1.Draw();

                SplashKit.RefreshScreen();

            } while (!SplashKit.WindowCloseRequested("Just A Normal Dungeon"));
        }
    }
}
