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
                var puzzle = puzzles.ElementAt(rand.Next(0, puzzles.Count));
                state.CurrentPuzzle = puzzle;
                state.GameState = GameStates.Game;

                var colorMatches = new Dictionary<string, Color>();
                foreach (var pieces in puzzle.puzzle)
                {
                    var row = 0;
                    var column = 0;
                    foreach (var letter in pieces)
                    {
                        var entity = new Entity();
                        entity.Components.Add(new Position()
                        {
                            Coordinates = new Vector2(column * 35, row * 35),
                            Rectangle = new Rectangle(0, 0, 35, 35)
                        });
                        entity.Components.Add(new PuzzleKey() { Key = letter.ToString() });

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
                    row++;
                    Console.WriteLine(pieces);
                }
            }
        }
    }
}