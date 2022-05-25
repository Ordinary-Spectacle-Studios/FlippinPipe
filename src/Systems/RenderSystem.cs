using System;
using System.Globalization;
using System.Linq;
using FlippinPipe.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlippinPipe.Systems
{
    public class RenderSystem : GameSystem
    {
        public RenderSystem(FlippinPipeEngine engine) : base(engine)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.SpriteBatch.Draw(Engine.Textures.Scene.Background, new Rectangle(0, 0, Engine.Graphics.PreferredBackBufferWidth, Engine.Graphics.PreferredBackBufferHeight), Color.White);
            Texture2D selectedTexture = null;
            Rectangle selectedPos = new Rectangle();

            foreach (var entity in Engine.Entities.Where(x => x.HasTypes(typeof(Position), typeof(Render))))
            {
                var myRender = entity.GetComponent<Render>();
                var myPosition = entity.GetComponent<Position>();
                var mykey = entity.GetComponent<PuzzleKey>();
                var mySelect = entity.GetComponent<Selected>();

                //Console.WriteLine("Entity: " + entity.ShortId());
                if (mySelect.IsSelected)
                {
                    var modified = myPosition.Destination with { X = myPosition.Destination.X - 35, Y = myPosition.Destination.Y - 35, Width = 140, Height = 140 };
                    selectedTexture = myRender.Texture;
                    selectedPos = modified;
                    //Engine.SpriteBatch.Draw(myRender.Texture, modified, Color.White);
                }
                else
                {
                    Engine.SpriteBatch.Draw(myRender.Texture, myPosition.Destination, Color.White);
                }

                Engine.SpriteBatch.DrawString(Engine.MainFont, mykey.Key, myPosition.Coordinates, Color.Orange);
            }
            if (selectedTexture != null)
                Engine.SpriteBatch.Draw(selectedTexture, selectedPos, Color.White);

            Engine.SpriteBatch.Draw(Engine.Textures.Frame, new Rectangle(0, 0, Engine.Graphics.PreferredBackBufferWidth, Engine.Graphics.PreferredBackBufferHeight), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}