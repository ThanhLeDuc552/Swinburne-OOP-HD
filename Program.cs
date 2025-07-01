using System;
using SplashKitSDK;
using System.Text.Json.Nodes;
using System.Resources;
using DotTiled;
using DotTiled.Serialization;
using DotTiled.Serialization.Tmx;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace Swinburne_OOP_HD
{
    

    public class Program
    {
        public static void Main()
        {
            Window newWindow = new Window("FireBoy and WaterGirl", 640, 480);

            Level level1 = new Level("Level1.tmx"); // Create a new level instance
            Loader loader = Loader.Default(); // Map parser
            Map map = loader.LoadMap("Level1.tmx"); // Load map
            FireBoy fireBoy = new FireBoy(new Point2D() { X = 0, Y = 0}); // Create a FireBoy instance

            foreach (BaseLayer layer in map.Layers)
            {
                Console.WriteLine($"Layer: {layer.Name}, Type: {layer.GetType().Name}, Visible: {layer.Visible}");
            } // Get layer information

            Dictionary<uint, Bitmap> bricks = new Dictionary<uint, Bitmap>(); // For matching tiles and ID manually (dont have tool to map automatically)

            ObjectLayer objectLayer = map.Layers[1] as ObjectLayer; // get the object layer

            List<DotTiled.Object> objects = objectLayer.Objects;
            foreach (var obj in objects)
            {
                Console.WriteLine(obj.Name);
                Console.WriteLine(obj.Type);
                Console.WriteLine(obj.X);
                Console.WriteLine(obj.Y);
                Console.WriteLine(obj.Width);
                Console.WriteLine(obj.Height);
                Console.WriteLine(obj.ID);
                foreach (var prop in obj.Properties)
                {
                    IProperty<string> newProp = prop as IProperty<string>;
                    Console.WriteLine($"{prop.Name} ({prop.Type}) : {newProp.Value}");
                    if (newProp.Value == "fire")
                    {
                        Point2D startPos = new Point2D() { X = obj.X, Y = obj.Y };
                        fireBoy.SetStartPosition(startPos); // Set the starting position for FireBoy
                    }
                }
                Console.WriteLine();
            }
            
            
            while (!newWindow.CloseRequested) 
            {
                SplashKit.ClearScreen(SplashKitSDK.Color.Black);
                SplashKit.ProcessEvents();

                level1.Draw();
                fireBoy.DrawChar(); // Draw the FireBoy character
                fireBoy.HandleInput();
                fireBoy.Update();
                SplashKit.DrawInterface();
                SplashKit.RefreshScreen();
            }
        }
    }
}
