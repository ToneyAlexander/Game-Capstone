namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents an empty IGameState.
    /// </summary>
    public sealed class NullGameState : IGameState
    {
        public static NullGameState Instance = new NullGameState();

        private NullGameState()
        {
        }

        #region IGameState
        public void Enter(Game game)
        {
            // No-op
        }

        public void Exit(Game game)
        {
            // No-op
        }
        #endregion
    }
}