using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeClone.Utilities
{
    public class Utils
    {
        public Vector2 KeepInBounds(Vector2 position, GraphicsDeviceManager _graphics, Texture2D texture)
        {
            Vector2 inBounds = new Vector2();

            if (position.X > _graphics.PreferredBackBufferWidth - texture.Width)
                position.X = _graphics.PreferredBackBufferWidth - texture.Width;
            else if (position.X <= 0)
                position.X = 0;
            inBounds.X = position.X;

            if (position.Y > _graphics.PreferredBackBufferHeight - texture.Height)
                position.Y = _graphics.PreferredBackBufferHeight - texture.Height;
            else if (position.Y <= 0)
                position.Y = 0;
            inBounds.Y = position.Y;

            return inBounds;
        }

        public bool Collides(Texture2D firstTexture, Texture2D secondTexture, Vector2 firstPosition, Vector2 secondPosition)
        {
            bool collides = false;

            Rectangle firstRect = new Rectangle((int)firstPosition.X, (int)firstPosition.Y, firstTexture.Width, firstTexture.Height);
            Rectangle secondRect = new Rectangle((int)secondPosition.X, (int)secondPosition.Y, secondTexture.Width, secondTexture.Height);

            if (firstRect.Intersects(secondRect))
                collides = true;

            return collides;
        }

        public bool CollideWithBounds(Vector2 position, GraphicsDeviceManager _graphics, Texture2D texture)
        {
            bool collides = false;

            if (position.X >= _graphics.PreferredBackBufferWidth - texture.Width ||
                    position.Y >= _graphics.PreferredBackBufferHeight - texture.Height)
                collides = true;
            else if (position.X <= 0 || position.Y <= 0)
                collides = true;

            return collides;
        }

        public class VectorAndLastDirection
        {
            public Vector2 position;
            public string lastDirection;
        }
    }
}
