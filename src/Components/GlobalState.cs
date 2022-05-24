using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FlippinPipe.FileHelpers;

namespace FlippinPipe.Components
{
    public class GlobalState : Component
    {
        public GameStates GameState = GameStates.Menu;
        public string CurrentPuzzle;
        public bool CanMove = true;
        public float TimeSinceMove = 0;
        public IEnumerable<IGrouping<char, char>> PuzzleAnswer;
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