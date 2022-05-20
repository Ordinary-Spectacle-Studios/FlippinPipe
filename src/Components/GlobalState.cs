namespace FlippinPipe.Components
{
    public class GlobalState : Component
    {
        public GameStates GameState;
    }

    public enum GameStates
    {
        Menu,
        Game,
        GameWin,
        GameLoss
    }
}