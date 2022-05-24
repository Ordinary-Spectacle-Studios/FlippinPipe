using System.Collections.Generic;
using System.Linq;
using System;
using FlippinPipe.Components;
using FlippinPipe.FileHelpers;
using Microsoft.Xna.Framework;
using FlippinPipe.Entities;
using Microsoft.Xna.Framework.Graphics;

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
                var puzzleSet = puzzles.ElementAt(rand.Next(0, puzzles.Count));
                var puzzle = puzzleSet.puzzle.ElementAt(rand.Next(0, puzzleSet.puzzle.Count()));

                state.CurrentPuzzle = puzzle;
                state.PuzzleAnswer =  puzzle.GroupBy(x => x);

                var colorMatches = new Dictionary<string, Color>();
                var column = 0;
                foreach (var letter in puzzle)
                {
                    var entity = new Entity();
                    entity.Components.Add(new Position()
                    {
                        Coordinates = new Vector2(column * 35, 0),
                        Rectangle = new Rectangle(0, 0, 35, 35)
                    });
                    entity.Components.Add(new PuzzleKey() { Key = letter.ToString(), Order = column });

                    var rect = new Texture2D(Engine.GraphicsDevice, 1, 1);
                    var color = new Color { A = 255, R = ((byte)rand.Next(0, 255)), G = ((byte)rand.Next(0, 255)), B = ((byte)rand.Next(0, 255)) };
                    if (colorMatches.ContainsKey(letter.ToString()))
                    {
                        color = colorMatches[letter.ToString()];
                    }
                    else
                    {
                        colorMatches.Add(letter.ToString(), color);
                    }
                    rect.SetData(new[] { color });
                    entity.Components.Add(new Render() { Texture = rect });
                    entity.Components.Add(new Selected());

                    column++;
                    Engine.Entities.Add(entity);
                }
                state.GameState = GameStates.Game;
            }
        }
    }
}