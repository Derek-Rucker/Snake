using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeClone
{
    class Snake
    {
        public Vector2 position;
        public Texture2D texture;
        public Vector2 prevPosition;

        public Snake(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(texture, position, null, Color.White, 0f,
            //    new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            sb.Draw(texture, position, Color.White);
        }

        //public Snake Grow(Vector2 prevPosition)
        //{
        //    Snake newSnake = new Snake(texture, position);
            
        //}
    }
}
