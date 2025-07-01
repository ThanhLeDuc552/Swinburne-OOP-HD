namespace Swinburne_OOP_HD 
{ 
    public interface ICharacterState
    {
        void Enter(Character character);
        void HandleInput(Character character);
        void Update(Character character);
        void Exit(Character character);
        string Name { get; }
    }
}
