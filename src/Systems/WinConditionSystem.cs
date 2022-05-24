using System;
using System.Linq;
using FlippinPipe.Components;
using Microsoft.Xna.Framework;

namespace FlippinPipe.Systems
{
    public class WinConditionSystem : GameSystem
    {
        public WinConditionSystem(FlippinPipeEngine engine) : base(engine)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var state = Engine.Singleton.GetComponent<GlobalState>();

            if (state.CurrentPuzzle != null)
            {
                var answer = string.Join("", state.PuzzleAnswer
                    .OrderBy(x => x.Key)
                    .Select(x => new string(x.Key, x.Count())));

                var currentPuzzle = string.Join("", Engine.Entities.Where(x => x.HasTypes(typeof(PuzzleKey)))
                    .OrderBy(x => x.GetComponent<PuzzleKey>().Order)
                    .Select(x => x.GetComponent<PuzzleKey>().Key));

                if (currentPuzzle == answer)
                {
                    Console.WriteLine("You WIN");
                    state.GameState = GameStates.GameWin;
                }
            }
        }
    }
}