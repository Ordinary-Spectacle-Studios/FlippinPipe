using System;
using FlippinPipe.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlippinPipe.UiHelpers
{
    public static class ButtonService
    {
        public static Rectangle GetButtonPosition(FlippinPipeEngine engine, ButtonPositions pos, int index)
        {
            var height = engine.Graphics.PreferredBackBufferHeight;
            var width = engine.Graphics.PreferredBackBufferWidth;

            var rect = new Rectangle();
            switch (pos)
            {
                case ButtonPositions.TopLeft:
                    rect = new Rectangle(0, 0, width / 3, height / 3);
                    break;
                case ButtonPositions.TopCenter:
                    rect = new Rectangle(width / 3, 0, width / 3, height / 3);
                    break;
                case ButtonPositions.TopRight:
                    rect = new Rectangle(width / 3 * 2, 0, width / 3, height / 3);
                    break;
                case ButtonPositions.BottomLeft:
                    rect = new Rectangle(0, height / 3 * 2, width / 3, height / 3);
                    break;
                case ButtonPositions.BottomCenter:
                    rect = new Rectangle(width / 3, height / 3 * 2, width / 3, height / 3);
                    break;
                case ButtonPositions.BottomRight:
                    rect = new Rectangle(width / 3 * 2, height / 3 * 2, width / 3, height / 3);
                    break;

            }
            return rect;
        }

        public static void MakeButton(FlippinPipeEngine engine, string text, ButtonPositions position, int index, Action act)
        {
            var mouseState = Mouse.GetState();
            var color = Color.White;
            var rect = new Texture2D(engine.GraphicsDevice, 1, 1);

            var height = engine.Graphics.PreferredBackBufferHeight;
            var width = engine.Graphics.PreferredBackBufferWidth;
            var segmentWidth = width / 3;
            var segmentHeight = height / 3;



            var offset = (index * 60);

            var destRect = GetButtonPosition(engine, position, index);
            if (destRect.Contains(mouseState.Position))
            {
                rect.SetData(new[] { Color.Red });
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    act();
                }
            }
            else
            {
                rect.SetData(new[] { Color.White });
            }

            engine.SpriteBatch.Draw(rect, destRect, color);
            var textSize = engine.MainFont.MeasureString(text);

            var center = new Vector2(destRect.X + (segmentWidth / 2) - (textSize.X / 2), destRect.Y + segmentHeight / 2);

            engine.SpriteBatch.DrawString(engine.MainFont, text, center, Color.Black);
        }
    }
}
