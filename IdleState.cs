namespace Swinburne_OOP_HD
{
    public class IdleState : ICharacterState
    {
        public string Name => "Idle";
        public void Enter(Character character) => character.SetCurrentActionByName("Idle");
        public void HandleInput(Character character) { /* handle idle-specific input */ }
        public void Update(Character character) { /* idle update logic */ }
        public void Exit(Character character) { }
    }

    public class MoveRightState : ICharacterState
    {
        public string Name => "MoveRight";
        public void Enter(Character character) => character.SetCurrentActionByName("MoveRight");
        public void HandleInput(Character character) { character.Position = new Point2D { X = character.Position.X + 0.1, Y = character.Position.Y }; }
        public void Update(Character character) { }
        public void Exit(Character character) { }
    }

    public class MoveLeftState : IcharacterState
    {
        public string Name => "MoveLeft";
        public void Enter(Character character) => character.SetCurrentActionByName("MoveLeft");
        public void HandleInput(Character character) { character.Position = new Point2D { X = character.Position.X - 0.1, Y = character.Position.Y }; }
        public void Update(Character character) { }
        public void Exit(Character character) { }
    }

    public class JumpState : ICharacterState
    {
        public string Name => "Jump";
        public void Enter(Character character) => character.SetCurrentActionByName("Jump");
        public void HandleInput(Character character) { /* handle jump-specific input */ }
        public void Update(Character character) { /* jump update logic */ }
        public void Exit(Character character) { }
    }

    public class FallState : ICharacterState
    {
        public string Name => "Fall";
        public void Enter(Character character) => character.SetCurrentActionByName("Fall");
        public void HandleInput(Character character) { /* handle fall-specific input */ }
        public void Update(Character character) { /* fall update logic */ }
        public void Exit(Character character) { }
    }
}

