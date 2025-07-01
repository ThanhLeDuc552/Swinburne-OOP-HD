using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using static System.Collections.Specialized.BitVector32;

namespace Swinburne_OOP_HD
{
    abstract public class Character
    {
        // Ingame properties
        private Point2D _pos;
        private bool _isDead;
        private string _type;
        private string _name;

        // Action resources for different character actions
        private ActionResource _moveRight;
        private ActionResource _moveLeft;
        private ActionResource _jump;
        private ActionResource _fall;
        private ActionResource _idle;
        private ActionResource _currAction;

        // Sprite scaling factor and calculation
        private const float SPRITE_SCALE = 0.1f;
        private const double HEAD_TO_LEG_ORIGINAL_DISTANCE = 50.0; // Original distance between head and leg in moveRight mode
        private const double HEAD_TO_LEG_SCALED_DISTANCE = HEAD_TO_LEG_ORIGINAL_DISTANCE * SPRITE_SCALE; // Scaled distance (= 5.0)
        
        // Action names
        private const string MOVE_RIGHT = "MoveRight";
        private const string MOVE_LEFT = "MoveLeft";
        private const string JUMP = "Jump";
        private const string FALL = "Fall";
        private const string IDLE = "Idle";

        // Vectors
        private Vector2D _velocityX;
        private Vector2D _velocityY;

        private ActionResource CreateActionResource(string actionName, string charName)
        {
            ActionResource action = new ActionResource();
            action.Name = actionName;
            action.Bitmap = SplashKit.BitmapNamed(charName + actionName);
            action.Script = SplashKit.AnimationScriptNamed(charName + actionName);
            action.Animation = SplashKit.CreateAnimation(action.Script, actionName);
            action.Options = SplashKit.OptionWithAnimation(action.Animation);
            action.Sprite = SplashKit.CreateSprite(charName + actionName, action.Bitmap, action.Script);

            return action;
        }

        public Character(string bundleFile, string type, string name, Point2D pos)
        {
            _pos = pos;
            _isDead = false;
            _type = type;
            _name = name;

            SplashKit.LoadResourceBundle(name, bundleFile);
            InitActions(name);
            SetScale();
            SetCurrentAction(_idle);
        }

        private void InitVectors()
        {
            //_velocityX = new Vector2D() {X = ;
            //_velocityY = new Vector2D();
        }

        private void InitActions(string name)
        {
            // MoveRight initialization
            _moveRight = CreateActionResource(MOVE_RIGHT, name);
            _moveRight.Sprite.StartAnimation(0);

            // MoveLeft initialization
            _moveLeft = CreateActionResource(MOVE_LEFT, name);
            _moveLeft.Sprite.StartAnimation(0);

            // Jump initialization
            _jump = CreateActionResource(JUMP, name);
            _jump.Sprite.StartAnimation(0);

            // Fall initialization
            _fall = CreateActionResource(FALL, name);
            _fall.Sprite.StartAnimation(0);

            // Idle initialization
            _idle = CreateActionResource(IDLE, name);
            _idle.Sprite.StartAnimation(0);
        }

        public void DrawChar()
        {
            (double offsetX, double offsetY) = CalculateSpriteOffset(_currAction);

            // Draw with calculated offsets
            _currAction.Sprite.Draw(offsetX + _pos.X, offsetY + _pos.Y);
            _currAction.Sprite.UpdateAnimation();
        }

        // Method to calculate sprite offsets based on action
        private (double offsetX, double offsetY) CalculateSpriteOffset(ActionResource action)
        {
            // Extract center points
            double idleCenterX = _idle.Sprite.SpriteCenterPoint.X;
            double idleCenterY = _idle.Sprite.SpriteCenterPoint.Y;
            double currCenterX = action.Sprite.SpriteCenterPoint.X;
            double currCenterY = action.Sprite.SpriteCenterPoint.Y;

            // Calculate height adjustments
            double idleHeightAdj = _idle.Sprite.Height * SPRITE_SCALE;
            double currHeightAdj = action.Sprite.Height * SPRITE_SCALE;

            // Calculate width adjustments
            double idleWidthAdj = _idle.Sprite.Width * SPRITE_SCALE;
            double currWidthAdj = action.Sprite.Width * SPRITE_SCALE;

            // Calculate base offsets
            double offsetX = idleCenterX - currCenterX;
            double offsetY = idleCenterY - currCenterY + (idleHeightAdj - currHeightAdj) / 2.0;

            // Apply action-specific X adjustments
            if (action.Name == _moveLeft.Name)
            {
                offsetX += (currWidthAdj - idleWidthAdj) / 2.0;
            }
            else if (action.Name == _moveRight.Name)
            {
                offsetX += (currWidthAdj - idleWidthAdj) / 2.0 - (currWidthAdj / 2.0 - HEAD_TO_LEG_SCALED_DISTANCE);
            }

            return (offsetX, offsetY);
        }

        
        protected void SetAnchorPoints(Point2D idlePt, Point2D fallPt, Point2D jumpPt, Point2D moveRightPt, Point2D moveLeftPt)
        {
            _idle.Sprite.AnchorPoint = idlePt;
            _fall.Sprite.AnchorPoint = fallPt;
            _jump.Sprite.AnchorPoint = jumpPt;
            _moveRight.Sprite.AnchorPoint = moveRightPt;
            _moveLeft.Sprite.AnchorPoint = moveLeftPt;

            _idle.Sprite.MoveFromAnchorPoint = true;
            _moveLeft.Sprite.MoveFromAnchorPoint = true;
            _moveRight.Sprite.MoveFromAnchorPoint = true;
            _jump.Sprite.MoveFromAnchorPoint = true;
            _fall.Sprite.MoveFromAnchorPoint = true;
        }
        

        private void SetScale()
        {
            _idle.Sprite.Scale = SPRITE_SCALE;
            _fall.Sprite.Scale = SPRITE_SCALE;
            _jump.Sprite.Scale = SPRITE_SCALE;
            _moveRight.Sprite.Scale = SPRITE_SCALE;
            _moveLeft.Sprite.Scale = SPRITE_SCALE;
        }

        private void SetCurrentAction(ActionResource action)
        {
            if (_currAction.Name != action.Name)
            {
                _currAction = action;
            }
        }

        public void MoveRight()
        {
            _pos.X += 0.1;
            SetCurrentAction(_moveRight);
        }

        public void MoveLeft()
        {
            _pos.X -= 0.1;
            SetCurrentAction(_moveLeft);
        }

        public void Jump()
        {
            SetCurrentAction(_jump);
        }

        public void Fall()
        {
            SetCurrentAction(_fall);
        }

        public void Idle()
        {
            SetCurrentAction(_idle);
        }
        
        public void Gravity()
        {
            // walking on ground

        }

        public void SetStartPosition(Point2D pos)
        {
            _pos.X = pos.X - _idle.Sprite.SpriteCenterPoint.X;
            _pos.Y = pos.Y - _idle.Sprite.SpriteCenterPoint.Y;
        }

        abstract public void ProcessInput();
         
        public Point2D Position
        {
            get 
            {
                return _pos;
            }
            set
            {
                _pos = value;
            }
        }

        public void ClearResources()
        {
            
        }
    }
}
