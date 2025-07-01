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
        private List<BaseLayer> _layers;
        private List<Tileset> _tilesets;

        private uint _tileWidth;
        private uint _tileHeight;
        private uint _mapWidth;
        private uint _mapHeight;

        private Dictionary<uint, Bitmap> _bricks; // Dictionary to hold tile GID, Bitmap
        private Dictionary<uint, DrawingOptions> _rotationOptions;

        /*
        private List<Water> _waterTiles;
        private List<Lava> _lavaTiles;
        private List<Mud> _mudTiles;
        */
        public Level(string tmxFilePath)
        {
            Loader loader = Loader.Default();
            Map map = loader.LoadMap(tmxFilePath);

            _layers = map.Layers;
            _tileWidth = map.TileWidth;
            _tileHeight = map.TileHeight;
            _mapWidth = map.Width;
            _mapHeight = map.Height;

            _bricks = new Dictionary<uint, Bitmap>();
            _rotationOptions = new Dictionary<uint, DrawingOptions>
            {
                { 0u, SplashKit.OptionRotateBmp(0) },
                { 2684354560u, SplashKit.OptionRotateBmp(90) },
                { 3221225472u, SplashKit.OptionRotateBmp(180) },
                { 1610612736u, SplashKit.OptionRotateBmp(270) }
            };

            // Logic for processing tilesets
            foreach (Tileset tileset in map.Tilesets) 
            {
                foreach (Tile tile in tileset.Tiles)
                {
                    string imgPath = tile.Image.Value.Source.Value;
                    string[] partial = imgPath.Split('/');
                    string newImgPath = "";
                    for (int i = 1; i < partial.Length; i++)
                    {
                        if (i == partial.Length - 1) // Last part is the image name
                        {
                            newImgPath += partial[i];
                        }
                        else
                        {
                            newImgPath += partial[i] + "/";
                        }
                    }

                    Bitmap bmp = SplashKit.LoadBitmap($"{tileset.Name} ({tile.ID})", newImgPath);
                    bmp.SetCellDetails((int)tile.Width, (int)tile.Height, 1, 1, 1);

                    _bricks[tile.ID + tileset.FirstGID] = bmp;
                }
            }
        }

        public void Draw()
        {
            foreach (BaseLayer layer in _layers)
            {
                if (layer is TileLayer tileLayer)
                {
                    DrawTileLayer(tileLayer);
                }
                /*
                else if (layer is ImageLayer imageLayer)
                {
                    DrawImageLayer(imageLayer);
                }
                else if (layer is ObjectLayer objectLayer)
                {
                    DrawObjectLayer(objectLayer);
                }
                */
            }
        }

        private void DrawImageLayer(ImageLayer imageLayer) 
        {
        
        }

        private void DrawObjectLayer(ObjectLayer objectLayer) 
        {
        
        }

        private void DrawTileLayer(TileLayer tileLayer)
        {
            uint[] GIDs = tileLayer.Data.Value.GlobalTileIDs.Value; // Tile global ids within the tileset and its position within the map
            FlippingFlags[] flags = tileLayer.Data.Value.FlippingFlags.Value;

            for (uint i = 0; i < _mapHeight; i++)
            {
                for (uint j = 0; j < _mapWidth; j++)
                {
                    uint gid = GIDs[i * _mapWidth + j];
                    if (_bricks.ContainsKey(gid))
                    {
                        uint key = (uint)flags[i * tileLayer.Width + j];
                        SplashKit.DrawBitmap(_bricks[gid], j * _tileWidth, i * _tileHeight, _rotationOptions[key]);
                    }
                }
            }
        }
    }
}
