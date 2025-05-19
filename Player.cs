using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Swinburne_OOP_HD
{
    internal class Player
    {
        private Bitmap _bitmap;
        private Point2D _location;

        public Player(int x, int y)
        {
            _bitmap = new Bitmap("Smith", "Idle_East_0.png");
            _location.X = x;
            _location.Y = y;
        }

        public void Draw()
        {
            _bitmap.Draw(_location.X, _location.Y);
        }
    }
}
