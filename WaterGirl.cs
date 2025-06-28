using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Swinburne_OOP_HD
{
    internal class WaterGirl : Character
    {
        public WaterGirl(Point2D pos) : base("watergirl_movement.txt", "water", "WaterGirl", pos) { }

        public override void ProcessInput()
        {
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                MoveRight();
            }
            else if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                MoveLeft();
            }
            else if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Jump();
            }
            else if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Fall();
            }
            else
            {
                Idle();
            }
        }
    }
}
