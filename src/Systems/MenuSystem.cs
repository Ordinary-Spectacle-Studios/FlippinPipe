using System.Text;
using System;
using FlippinPipe.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FlippinPipe.UiHelpers;

namespace FlippinPipe.Systems
{
    public class MenuSystem : GameSystem
    {
        public MenuSystem(FlippinPipeEngine engine) : base(engine)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var state = this.Engine.Singleton.GetComponent<GlobalState>();

            if (state.GameState == GameStates.Menu)
            {

                ButtonService.MakeButton(Engine, "Options", ButtonPositions.BottomLeft, 0, () =>
                {
                    // Show Options
                });

                ButtonService.MakeButton(Engine, "Play", ButtonPositions.BottomCenter, 0, () =>
                {
                    state.GameState = GameStates.GameStart;
                });

                ButtonService.MakeButton(Engine, "Credits", ButtonPositions.BottomRight, 0, () =>
                {
                    // Show Credits
                });

                ButtonService.MakeButton(Engine, "Exit Game", ButtonPositions.TopRight, 0, () =>
                {
                    Engine.Exit();

                });

            Engine.SpriteBatch.Draw(Engine.Textures.Frame, new Rectangle(0, 0, Engine.Graphics.PreferredBackBufferWidth, Engine.Graphics.PreferredBackBufferHeight), Color.White);


            }

            //this.Engine.SpriteBatch.Draw(, Vector2.Zero, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}