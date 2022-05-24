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
            foreach (var entity in Engine.Entities.Where(x => x.HasTypes(typeof(Position), typeof(Render))))
            {
                var myRender = entity.GetComponent<Render>();
                var myPosition = entity.GetComponent<Position>();
                var mykey = entity.GetComponent<PuzzleKey>();

                //Console.WriteLine("Entity: " + entity.ShortId());
                Engine.SpriteBatch.Draw(myRender.Texture, myPosition.Destination, Color.White);
                Engine.SpriteBatch.DrawString(Engine.MainFont, mykey.Key, myPosition.Coordinates, Color.Red);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}