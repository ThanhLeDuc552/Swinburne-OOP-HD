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
            {
                MoveRight();
            }
            else if (SplashKit.KeyDown(KeyCode.AKey))
            {
                MoveLeft();
            }
            else if (SplashKit.KeyDown(KeyCode.WKey))
            {
                Rotate();
            }
            else
            {
                Idle();
            }
        }
    }
}
