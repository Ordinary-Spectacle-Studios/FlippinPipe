using System.ComponentModel;
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

        public TextureData Textures;

        public FlippinPipeEngine()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Textures = new TextureData();
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
            this.Systems.Add(new WinConditionSystem(this));
            this.Systems.Add(new InteractionSystem(this));

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
            Textures.Frame = Content.Load<Texture2D>("img/base/baseframe");

            Textures.Scene.Background = Content.Load<Texture2D>("img/scenes/graveyard/graveyard_bg");
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/coffin"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/crow"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/fence"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/hand"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/marker"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/roses"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/shovel"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/skull"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/tombstone"));
            Textures.Scene.Tiles.Add(Content.Load<Texture2D>("img/scenes/graveyard/tiles/urn"));

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

            ///GraphicsDevice.Clear(Color.CornflowerBlue);

            this.Systems.ForEach(x => x.Draw(gameTime));

            //base.Draw(gameTime);
            SpriteBatch.End();
        }
    }

    public record TextureData
    {
        public Texture2D Frame;
        public Texture2D CloseBtn;
        public Texture2D OptionsBtn;
        public Scene Scene = new();
    }
    public record Scene
    {
        public Texture2D Background;
        public List<Texture2D> Tiles = new();
    }
}
