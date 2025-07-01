using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using DotTiled;
using DotTiled.Serialization;
using DotTiled.Serialization.Tmx;

namespace Swinburne_OOP_HD
{
    class Level
    {
        private Dictionary<Bitmap, int> _tiles;
        //_background;
        private Dictionary<GameObject, int> _objects;

        public Level(string tmxFilePath)
        {
            Loader loader = Loader.Default();
            Map map = loader.LoadMap(tmxFilePath);
        }
    }
}
