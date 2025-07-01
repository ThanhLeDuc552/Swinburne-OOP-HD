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

        // Vectors
        private Vector2D _velocityX;
        private Vector2D _velocityY;

        private ICharacterState _currentState;
        private Dictionary<string, ICharacterState> _states;

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

            InitStates();
            ChangeState("Idle");
        }

        private void InitStates()
        {
            _states = new Dictionary<string, ICharacterState>
            {
                { "Idle", new IdleState() },
                { "MoveRight", new MoveRightState() },
                { "MoveLeft", new MoveLeftState() },
                { "Jump", new JumpState() },
                { "Fall", new FallState() }
            };
        }

        public void ChangeState(string stateName)
        {
            _currentState?.Exit(this);
            _currentState = _states[stateName];
            _currentState.Enter(this);
        }

        public void HandleInput() => _currentState.HandleInput(this);
        public void Update() => _currentState.Update(this);

        public void SetCurrentActionByName(string actionName)
        {
            switch (actionName)
            {
                case "Idle": _currAction = _idle; break;
                case "MoveRight": _currAction = _moveRight; break;
                case "MoveLeft": _currAction = _moveLeft; break;
                case "Jump": _currAction = _jump; break;
                case "Fall": _currAction = _fall; break;
            }
        }

        private void InitActions(string name)
        {
            // MoveRight initialization
            _moveRight = CreateActionResource("MoveRight", name);
            _moveRight.Sprite.StartAnimation(0);

            // MoveLeft initialization
            _moveLeft = CreateActionResource("MoveLeft", name);
            _moveLeft.Sprite.StartAnimation(0);

            // Jump initialization
            _jump = CreateActionResource("Jump", name);
            _jump.Sprite.StartAnimation(0);

            // Fall initialization
            _fall = CreateActionResource("Fall", name);
            _fall.Sprite.StartAnimation(0);

            // Idle initialization
            _idle = CreateActionResource("Idle", name);
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

        private void SetScale()
        {
            _idle.Sprite.Scale = SPRITE_SCALE;
            _fall.Sprite.Scale = SPRITE_SCALE;
            _jump.Sprite.Scale = SPRITE_SCALE;
            _moveRight.Sprite.Scale = SPRITE_SCALE;
            _moveLeft.Sprite.Scale = SPRITE_SCALE;
        }
        
        public void Gravity() { }

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

        public void ClearResources() { }
    }
}
