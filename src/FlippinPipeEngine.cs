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
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

        public List<Entity> Entities = new();
        public List<GameSystem> Systems = new();
        public Entity Singleton;

        public SpriteFont MainFont;

        public FlippinPipeEngine()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;

            Graphics.ApplyChanges();

            this.Systems.Add(new RenderSystem(this));
            this.Systems.Add(new MenuSystem(this));
            this.Systems.Add(new GenerationSystem(this));

            var singleton = new Entity();
            singleton.Components.Add(new GlobalState() { GameState = GameStates.Menu });

            this.Singleton = singleton;

            this.Entities.Add(singleton);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            MainFont = Content.Load<SpriteFont>("arial");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            // Exit();

            this.Systems.ForEach(x => x.Update(gameTime));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.Systems.ForEach(x => x.Draw(gameTime));

            //base.Draw(gameTime);
            SpriteBatch.End();
        }
    }
}
