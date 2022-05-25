using System.Threading;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
using FlippinPipe.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlippinPipe.Systems
{
    public class InteractionSystem : GameSystem
    {
        public InteractionSystem(FlippinPipeEngine engine) : base(engine)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var state = this.Engine.Singleton.GetComponent<GlobalState>();
            state.TimeSinceMove += gameTime.ElapsedGameTime.Milliseconds;
            if (!state.CanMove && state.TimeSinceMove > 250)
            {
                state.CanMove = true;
            }

            var allSquares = Engine.Entities.Where(x => x.HasTypes(typeof(Position), typeof(PuzzleKey), typeof(Selected)));
            var mouse = Mouse.GetState();

            var selectedCell = allSquares.FirstOrDefault(x => x.GetComponent<Selected>().IsSelected);
            if (mouse.LeftButton == ButtonState.Pressed && state.CanMove)
            {
                foreach (var cell in allSquares)
                {
                    var myPosition = cell.GetComponent<Position>();
                    var mySelected = cell.GetComponent<Selected>();

                    if (myPosition.Destination.Contains(mouse.Position))
                    {
                        mySelected.IsSelected = true;
                    }
                    else
                    {
                        mySelected.IsSelected = false;
                    }
                }

            }
            else if (mouse.LeftButton == ButtonState.Released && selectedCell != null)
            {
                state.CanMove = false;
                state.TimeSinceMove = 0;
                var cell = selectedCell;
                var myPosition = cell.GetComponent<Position>();
                var myKey = cell.GetComponent<PuzzleKey>();

                if (myPosition.Destination.Contains(mouse.Position))
                {
                    var order = myKey.Order;
                    var rightSide = allSquares.Where(x => x.GetComponent<PuzzleKey>().Order >= order);

                    rightSide = rightSide.OrderByDescending(x => x.GetComponent<PuzzleKey>().Order);
                    var offset = 0;
                    foreach (var x in rightSide)
                    {
                        var key = x.GetComponent<PuzzleKey>();
                        var mySelect = cell.GetComponent<Selected>();
                        var pos = x.GetComponent<Position>();
                        var newOrder = order + offset++;
                        key.Order = newOrder;
                        pos.Coordinates.X = (newOrder * 35) + 100;
                        mySelect.IsSelected = false;

                    }
                }
            }

        }
    }
}