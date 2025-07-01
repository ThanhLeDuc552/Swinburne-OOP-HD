using System;
using SplashKitSDK;
using System.Text.Json.Nodes;
using System.Resources;
using DotTiled;
using DotTiled.Serialization;
using DotTiled.Serialization.Tmx;
using System.ComponentModel.DataAnnotations;

namespace Swinburne_OOP_HD
{
    public class Program
    {

        public static void Main()
        {
            Window newWindow = new Window("FireBoy and WaterGirl", 800, 600);

            Loader loader = Loader.Default();
            Map map = loader.LoadMap(@"Level1.tmx");

            TileLayer tileLayer = map.Layers[0] as TileLayer;
            ObjectLayer objectLayer = map.Layers[1] as ObjectLayer;
            Tileset tileset = map.Tilesets[0];

            uint tileWidth = map.TileWidth;
            uint tileHeight = map.TileHeight;

            List<Tile> tiles = tileset.Tiles;
            foreach (Tile tile in tiles)
            {
                Console.WriteLine(tile.ID);
                Console.WriteLine(tile.Image.Value.Source.Value);
            }

            DrawingOptions opt = SplashKit.OptionPartBmp(0, 0, 16, 16);

            WaterGirl water = new WaterGirl(new Point2D() { Y = 0, X = 0 });
            while (!newWindow.CloseRequested) 
            {
                SplashKit.ClearScreen();
                SplashKit.ProcessEvents();

                water.DrawChar();
                water.ProcessInput();

                SplashKit.DrawInterface();
                SplashKit.RefreshScreen();
            }

            SplashKit.FreeAllSprites();
        }
    }
}
