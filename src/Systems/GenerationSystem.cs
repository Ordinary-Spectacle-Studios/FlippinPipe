using System.Linq;
using System;
using FlippinPipe.Components;
using FlippinPipe.FileHelpers;
using Microsoft.Xna.Framework;
using FlippinPipe.Entities;

namespace FlippinPipe.Systems
{
    public class GenerationSystem : GameSystem
    {
        public GenerationSystem(FlippinPipeEngine engine) : base(engine)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var rand = new Random();
            var state = this.Engine.Singleton.GetComponent<GlobalState>();
            if (state.GameState == GameStates.GameStart)
            {
                var puzzles = DataLoaderService.LoadPuzzles();
                var puzzle = puzzles.ElementAt(rand.Next(0, puzzles.Count));
                state.CurrentPuzzle = puzzle;
                state.GameState = GameStates.Game;

                foreach (var pieces in puzzle.puzzle)
                {
                    var row = 0;
                    var column = 0;
                    foreach (var letter in pieces)
                    {
                        var entity = new Entity();
                        entity.Components.Add(new Position() { Coordinates = new Vector2(row, column) });
                        column++;
                        Engine.Entities.Add(entity);
                    }
                    row++;
                }
            }
        }
    }
}