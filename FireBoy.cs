using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Swinburne_OOP_HD
{
    public class FireBoy : Character
    {
        public FireBoy(Point2D pos) : base("fireboy_movement.txt", "fire", "FireBoy", pos) { }

        public override void ProcessInput()
        {
            if (SplashKit.KeyDown(KeyCode.DKey))
                ChangeState("MoveRight");
            else if (SplashKit.KeyDown(KeyCode.AKey))
                ChangeState("MoveLeft");
            else if (SplashKit.KeyDown(KeyCode.WKey))
                ChangeState("Jump");
            else if (SplashKit.KeyDown(KeyCode.SKey))
                ChangeState("Fall");
            else
                ChangeState("Idle");
        }
    }
}
