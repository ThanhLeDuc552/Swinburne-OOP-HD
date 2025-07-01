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
        public FireBoy(Point2D pos) : base("fireboy_movement.txt", "fire", "FireBoy", pos) 
        {
            
            Point2D idlePt = new Point2D() { X = 107, Y = 410 };
            Point2D fallPt = new Point2D() { X = 107, Y = 410 };
            Point2D jumpPt = new Point2D() { X = 107, Y = 336 };
            Point2D moveRightPt = new Point2D() { X = 250, Y = 270 };
            Point2D moveLeftPt = new Point2D() { X = 96, Y = 270 };
            SetAnchorPoints(idlePt, fallPt, jumpPt, moveRightPt, moveLeftPt);
            
        }

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
                Jump();
            }
            else if (SplashKit.KeyDown(KeyCode.SKey))
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
