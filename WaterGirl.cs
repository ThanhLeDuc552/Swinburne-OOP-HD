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
                ChangeState("MoveRight");
            else if (SplashKit.KeyDown(KeyCode.LeftKey))
                ChangeState("MoveLeft");
            else if (SplashKit.KeyDown(KeyCode.UpKey))
                ChangeState("Jump");
            else if (SplashKit.KeyDown(KeyCode.DownKey))
                ChangeState("Fall");
            else
                ChangeState("Idle");
        }
    }
}
