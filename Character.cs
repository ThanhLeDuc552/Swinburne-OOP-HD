using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Swinburne_OOP_HD
{
    public struct ActionResource
    {
        public string name;
        public Bitmap bitmap;
        public AnimationScript script;
        public Animation anim;
        public DrawingOptions opts;
        public Sprite sprite;
    }

    abstract public class Character
    {
        Point2D _pos;
        bool _isDead;
        string _type;
        string _moveRightSprite;
        ActionResource _moveRight;
        ActionResource _moveLeft;
        ActionResource _jump;
        ActionResource _fall;
        ActionResource _idle;
        ActionResource _currAction;
        string _name;


        public Character(string bundleFile, string type, string name, Point2D pos)
        {
            _pos = pos;
            _isDead = false;
            _type = type;
            _name = name;
            SplashKit.LoadResourceBundle(name, bundleFile);
            InitActions(name);
            _currAction = _idle;
        }

        public void InitActions(string name)
        {
            // MoveRight initialization
            _moveRight.name = "MoveRight";
            _moveRight.bitmap = SplashKit.BitmapNamed(name + _moveRight.name);
            _moveRight.script = SplashKit.AnimationScriptNamed(name + _moveRight.name);
            _moveRight.anim = SplashKit.CreateAnimation(_moveRight.script, _moveRight.name);
            _moveRight.opts = SplashKit.OptionWithAnimation(_moveRight.anim);
            _moveRight.sprite = SplashKit.CreateSprite(name + _moveRight.name, _moveRight.bitmap, _moveRight.script);
            _moveRight.sprite.StartAnimation(0);

            // MoveLeft initialization
            _moveLeft.name = "MoveLeft";
            _moveLeft.bitmap = SplashKit.BitmapNamed(name + _moveLeft.name);
            _moveLeft.script = SplashKit.AnimationScriptNamed(name + _moveLeft.name);
            _moveLeft.anim = SplashKit.CreateAnimation(_moveLeft.script, _moveLeft.name);
            _moveLeft.opts = SplashKit.OptionWithAnimation(_moveLeft.anim);
            _moveLeft.sprite = SplashKit.CreateSprite(name + _moveLeft.name, _moveLeft.bitmap, _moveLeft.script);
            _moveLeft.sprite.StartAnimation(0);

            // Jump initialization
            _jump.name = "Jump";
            _jump.bitmap = SplashKit.BitmapNamed(name + _jump.name);
            _jump.script = SplashKit.AnimationScriptNamed(name + _jump.name);
            _jump.anim = SplashKit.CreateAnimation(_jump.script, _jump.name);
            _jump.opts = SplashKit.OptionWithAnimation(_jump.anim);
            _jump.sprite = SplashKit.CreateSprite(name + _jump.name, _jump.bitmap, _jump.script);
            _jump.sprite.StartAnimation(0);

            // Fall initialization
            _fall.name = "Fall";
            _fall.bitmap = SplashKit.BitmapNamed(name + _fall.name);
            _fall.script = SplashKit.AnimationScriptNamed(name + _fall.name);
            _fall.anim = SplashKit.CreateAnimation(_fall.script, _fall.name);
            _fall.opts = SplashKit.OptionWithAnimation(_fall.anim);
            _fall.sprite = SplashKit.CreateSprite(name + _fall.name, _fall.bitmap, _fall.script);
            _fall.sprite.StartAnimation(0);

            // Idle initialization
            _idle.name = "Idle";
            _idle.bitmap = SplashKit.BitmapNamed(name + _idle.name);
            _idle.script = SplashKit.AnimationScriptNamed(name + _idle.name);
            _idle.anim = SplashKit.CreateAnimation(_idle.script, _idle.name);
            _idle.opts = SplashKit.OptionWithAnimation(_idle.anim);
            _idle.sprite = SplashKit.CreateSprite(name + _idle.name, _idle.bitmap, _idle.script);
            _idle.sprite.StartAnimation(0);

            SplashKit.SpriteSetAnchorPoint(_idle.sprite, new Point2D() { X = 0, Y = 0 });
            SplashKit.SpriteSetAnchorPoint(_moveLeft.sprite, new Point2D() { X = 0, Y = 0 });
            SplashKit.SpriteSetAnchorPoint(_moveRight.sprite, new Point2D() { X = 0, Y = 0 });
            SplashKit.SpriteSetAnchorPoint(_jump.sprite, new Point2D() { X = 0, Y = 0 });
            SplashKit.SpriteSetAnchorPoint(_fall.sprite, new Point2D() { X = 0, Y = 0 });

            // Sprites scale

            _idle.sprite.Scale = 0.1f;
            _fall.sprite.Scale = 0.1f;
            _moveLeft.sprite.Scale = 0.1f;
            _moveRight.sprite.Scale = 0.1f;
            _jump.sprite.Scale = 0.1f;

            Console.WriteLine($"Idle sprite anchor point: X = {_idle.sprite.AnchorPoint.X}, Y = {_idle.sprite.AnchorPoint.Y}");
            Console.WriteLine($"Move Left sprite anchor point: X = {_moveLeft.sprite.AnchorPoint.X}, Y = {_moveLeft.sprite.AnchorPoint.Y}");
            Console.WriteLine($"Move Right sprite anchor point: X = {_moveRight.sprite.AnchorPoint.X}, Y = {_moveRight.sprite.AnchorPoint.Y}");
            Console.WriteLine($"Jump sprite anchor point: X = {_jump.sprite.AnchorPoint.X}, Y = {_jump.sprite.AnchorPoint.Y}");
            Console.WriteLine($"Fall sprite anchor point: X = {_fall.sprite.AnchorPoint.X}, Y = {_fall.sprite.AnchorPoint.Y}");
        }

        public void DrawChar()
        {
            _currAction.sprite.Draw();
            _currAction.sprite.UpdateAnimation();
        }

        public void MoveRight()
        {
            //_pos.X += 0.1;
            if (_currAction.sprite.Name != _moveRight.name)
            {
                _currAction = _moveRight;
            }
        }

        public void MoveLeft()
        {
            //_pos.X -= 0.1;
            if (_currAction.sprite.Name != _moveLeft.name)
            {
                _currAction = _moveLeft;
            }
        }

        public void Jump()
        {
            for (int i = 0; i<3; i++)
            {
                _pos.Y -= 1;
            }

            for (int i = 3; i>0; i--)
            {
                _pos.Y += 1;
            }
        }

        public void Fall()
        {

        }

        public void Idle()
        {
            if (_currAction.sprite.Name != _idle.name)
            {
                _currAction = _idle;
            }
        }

        abstract public void ProcessInput();
        
        public void Rotate()
        {
            _idle.sprite.Rotation += 3;
        }
        public Point2D Position
        {
            get { return _pos; }
        }
    }
}
