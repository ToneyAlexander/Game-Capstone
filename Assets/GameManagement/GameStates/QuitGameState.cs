namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an IGameState where a Game is quit.
    /// </summary>
    public sealed class QuitGameState : IGameState
    {
        #region IGameState
        public void Enter(Game game)
        {
            game.Quit();
        }

        public void Exit(Game game)
        {
            // No-op
        }
        #endregion
    }
}