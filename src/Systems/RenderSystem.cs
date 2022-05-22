using System;
using System.Globalization;
using System.Linq;
using FlippinPipe.Components;
using Microsoft.Xna.Framework;

namespace FlippinPipe.Systems
{
    public class RenderSystem : GameSystem
    {
        public RenderSystem(FlippinPipeEngine engine) : base(engine)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            var heightOffset = 300;
            var widthOffset = 50;
            // var sizes = 40;

            foreach (var entity in Engine.Entities.Where(x => x.HasTypes(typeof(Position), typeof(Render))))
            {
                var myRender = entity.GetComponent<Render>();
                var myPosition = entity.GetComponent<Position>();

                //Console.WriteLine("Entity: " + entity.ShortId());
                Engine.SpriteBatch.Draw(myRender.Texture, myPosition.Destination, Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}