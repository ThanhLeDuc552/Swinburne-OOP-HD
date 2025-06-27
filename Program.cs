using System;
using SplashKitSDK;
using System.Text.Json.Nodes;
using System.Resources;
using DotTiled;
using DotTiled.Serialization;
using DotTiled.Serialization.Tmx;

namespace Swinburne_OOP_HD
{
    public class Program
    {

        public static void Main()
        {
            Window newWindow = new Window("FireBoy and WaterGirl", 800, 600);

            Loader loader = Loader.Default();
            Map map = loader.LoadMap(@"Level1.tmx");

            TileLayer layer0 = map.Layers[0] as TileLayer;

            Console.WriteLine(layer0.X);
            Console.WriteLine(layer0.Y);
            Console.WriteLine(layer0.Width);
            Console.WriteLine(layer0.Height);
            foreach (var i in layer0.Data.Value.GlobalTileIDs.Value) {
                Console.WriteLine(i);
            }

            Bitmap bricks = new Bitmap("Brick", "blocks.png");
            bricks.SetCellDetails(16, 16, 8, 8, 64);

            DrawingOptions opt = SplashKit.OptionPartBmp(0, 0, 16, 16);

            FireBoy fire = new FireBoy(new Point2D() { X = 0, Y = 0 });
            //WaterGirl water = new WaterGirl(new Point2D() { Y = 0, X = 0 });
            while (!newWindow.CloseRequested) 
            {
                SplashKit.ClearScreen();
                SplashKit.ProcessEvents();

                for (int i = 0; i < 29; i++)
                {
                    for (int j = 0; j < 39; j++)
                    {
                        if (layer0.Data.Value.GlobalTileIDs.Value[j + 39 * i] == 6)
                        {
                            SplashKit.DrawBitmap(bricks, 16 * j, 16 * i, opt);
                        }
                    }
                }

                //water.DrawChar();
                fire.DrawChar();

                //water.ProcessInput();
                fire.ProcessInput();

                SplashKit.DrawInterface();
                SplashKit.RefreshScreen();
            }

            SplashKit.FreeAllSprites();
        }
    }
}
