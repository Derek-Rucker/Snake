using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using static SnakeClone.Utilities.Utils;

namespace SnakeClone
{
    public class Movement
    {
        KeyboardState kstate, prevKstate;
        public VectorAndLastDirection Move(Vector2 position, float speed, GameTime gameTime, bool isSnake = false, string lastDirection = "")
        {
            prevKstate = kstate;
            kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                //position.Y -= speed;
                position.Y -= speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds/60);
                lastDirection = "up";
            }
            else if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                position.Y += speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                //position.Y += speed;
                lastDirection = "down";
            }
            else if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                position.X -= speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                //position.X -= speed;
                lastDirection = "left";
            }
            else if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                position.X += speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                //position.X += speed;
                lastDirection = "right";
            }

            if (kstate.GetPressedKeyCount() == 0 && isSnake)
            {

                if (lastDirection != string.Empty)
                {
                    if (lastDirection == "up")
                    {
                        position.Y -= speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                    }
                    if (lastDirection == "down")
                    {
                        position.Y += speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                    }
                    if (lastDirection == "left")
                    {
                        position.X -= speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                    }
                    if (lastDirection == "right")
                    {
                        position.X += speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 60);
                    }
                }
            }

            VectorAndLastDirection vld = new VectorAndLastDirection();
            vld.position = position;
            vld.lastDirection = lastDirection;

            return vld;
        }
    }
}
