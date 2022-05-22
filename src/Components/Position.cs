using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace FlippinPipe.Components
{
    public class Position : Component
    {
        public Vector2 Coordinates;
        public Rectangle Rectangle;
        public Rectangle Destination => new Rectangle((int)Coordinates.X, (int)Coordinates.Y, Rectangle.Width, Rectangle.Height);

    }
}