namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents something that can change the state of a Game.
    /// </summary>
    public interface IGameStateChanger
    {
        void ChangeGameState();
    }
}