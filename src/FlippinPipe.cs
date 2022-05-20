using System.Reflection.Metadata;
using System.Collections.Generic;
using FlippinPipe.Entities;
using FlippinPipe.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FlippinPipe.Components;

namespace FlippinPipe
{
    public class FlippinPipeEngine : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public List<Entity> Entities = new();
        public List<GameSystem> Systems = new();
        public Entity Singleton;

        public FlippinPipeEngine()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            graphics.ApplyChanges();

            this.Systems.Add(new RenderSystem(this));

            var singleton = new Entity();
            singleton.Components.Add(new GlobalState() { GameState = GameStates.Menu });

            this.Singleton = singleton;

            this.Entities.Add(singleton);
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
