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
            var allSquares = Engine.Entities.Where(x => x.HasTypes(typeof(Position), typeof(PuzzleKey)));

            var mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var cell in allSquares)
                {
                    var myPosition = cell.GetComponent<Position>();
                    var mySelect = cell.GetComponent<Selected>();

                    if (myPosition.Destination.Contains(mouse.Position))
                    {
                        mySelect.IsSelected = true;
                     }
                    else
                    {
                        mySelect.IsSelected = false;
                    }
                }
            }
        }
    }
}