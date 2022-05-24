using System.Net;
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
            if (state.GameState == GameStates.GameWin)
            {
                var removed = Engine.Entities.Where(x => x.HasTypes(typeof(PuzzleKey))).ToList();
                foreach (var entity in removed)
                {
                    Engine.Entities.Remove(entity);
                }
                state.GameState = GameStates.GameStart;
            }
            if (state.GameState == GameStates.GameStart)
            {
                var puzzles = DataLoaderService.LoadPuzzles();
                var puzzleSet = puzzles.ElementAt(rand.Next(0, puzzles.Count));
                var puzzle = puzzleSet.puzzle.ElementAt(rand.Next(0, puzzleSet.puzzle.Count()));

                state.CurrentPuzzle = puzzle;
                state.PuzzleAnswer = puzzle.GroupBy(x => x);

                var matches = new Dictionary<string, Texture2D>();
                var column = 0;
                foreach (var letter in puzzle)
                {
                    var entity = new Entity();
                    entity.Components.Add(new Position()
                    {
                        Coordinates = new Vector2((column * 35) + 100, 300),
                        Rectangle = new Rectangle(0, 0, 35, 35)
                    });
                    entity.Components.Add(new PuzzleKey() { Key = letter.ToString(), Order = column });

                    var filteredTiles = Engine.Textures.Scene.Tiles
                        .Where(x => !matches.Select(x => x.Value)
                        .Contains(x));
                    var tile = filteredTiles.ElementAt(rand.Next(0, filteredTiles.Count()));

                    if (matches.ContainsKey(letter.ToString()))
                    {
                        tile = matches[letter.ToString()];
                    }
                    else
                    {
                        matches.Add(letter.ToString(), tile);
                    }
                    //rect.SetData(new[] { color });
                    entity.Components.Add(new Render() { Texture = tile });
                    entity.Components.Add(new Selected());

                    column++;
                    Engine.Entities.Add(entity);
                }
                state.GameState = GameStates.Game;
            }
        }
    }
}