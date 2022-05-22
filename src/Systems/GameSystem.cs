using Microsoft.Xna.Framework;

namespace FlippinPipe.Systems
{
    public abstract class GameSystem
    {
        public FlippinPipeEngine Engine;

        public GameSystem(FlippinPipeEngine engine)
        {
            this.Engine = engine;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}