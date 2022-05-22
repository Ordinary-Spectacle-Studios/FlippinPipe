using System.Runtime.Serialization;
using FlippinPipe.FileHelpers;

namespace FlippinPipe.Components
{
    public class GlobalState : Component
    {
        public GameStates GameState = GameStates.Menu;
        public PuzzleData CurrentPuzzle;
    }

    public enum GameStates
    {
        Menu,
        Game,
        GameStart,
        GameWin,
        GameLoss
    }
}