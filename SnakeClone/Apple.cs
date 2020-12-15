using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeClone
{
    class Apple
    {
        public Vector2 position;
        public Texture2D texture;
        public bool isCollected;

        public Apple(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, Color.White, 0f,
                new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
        }
    }
}
