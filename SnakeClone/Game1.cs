using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using SnakeClone.Utilities;
using System.Collections.Generic;
using System.Linq;
using static SnakeClone.Utilities.Utils;

namespace SnakeClone
{
    public class Game1 : Game
    {
        private Utils utilities = new Utils();
        Random random = new Random();
        private Movement movement = new Movement();

        Texture2D snakeTexture;
        Texture2D appleTexture;
        Texture2D snakeGrowX;
        Texture2D snakeGrowY;

        List<Apple> apples;
        Queue<Snake> snakes;
        Snake snake;

        List<Vector2> snakePositions;

        float snakeSpeed;
        string lastDirection = "";
        int score = 0;
        bool gameOver;

        SpriteFont spScore;
        SpriteFont spCurrSpeed;
        SpriteFont spGameOver;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Random random = new Random();
            snakePositions = new List<Vector2>();
            //snakePosition = new Vector2(_graphics.PreferredBackBufferHeight / 2, _graphics.PreferredBackBufferWidth / 2);
            apples = new List<Apple>();
            snakes = new Queue<Snake>();
            //apple.position = new Vector2(random.Next(0, _graphics.PreferredBackBufferHeight), random.Next(0, _graphics.PreferredBackBufferWidth));
            snakeSpeed = 85f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            snakeTexture = Content.Load<Texture2D>("snake");
            appleTexture = Content.Load<Texture2D>("apple");
            snakeGrowX = Content.Load<Texture2D>("snakeGrowX");
            snakeGrowY = Content.Load<Texture2D>("snakeGrowY");
            Apple apple = new Apple(appleTexture, new Vector2(random.Next(0, _graphics.PreferredBackBufferHeight), random.Next(0, _graphics.PreferredBackBufferWidth)));
            apples.Add(apple);
            snake = new Snake(snakeTexture);
            snake.position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            snakes.Enqueue(snake);
            snakePositions.Add(snake.position);
            spScore = Content.Load<SpriteFont>("score");
            spCurrSpeed = Content.Load<SpriteFont>("currentSpeed");
            spGameOver = Content.Load<SpriteFont>("gameOver");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!gameOver)
            {
                // TODO: Add your update logic here
                VectorAndLastDirection vld = movement.Move(snakes.ElementAt(0).position, snakeSpeed, gameTime, true, snakes.ElementAt(0).lastDirection);
                snakes.ElementAt(0).prevPosition = snakes.ElementAt(0).position;
                snakes.ElementAt(0).position = vld.position;
                snakePositions[0] = vld.position;
                snakes.ElementAt(0).lastDirection = snakes.ElementAt(0).currDirection;
                snakes.ElementAt(0).currDirection = vld.lastDirection;
                for (int i = 1; i < snakePositions.Count; i++)
                {
                    snakes.ElementAt(i).prevPosition = snakes.ElementAt(i).position;
                    switch (snakes.ElementAt(i - 1).lastDirection)
                    {
                        case "up":
                            snakes.ElementAt(i).position = new Vector2(snakes.ElementAt(i - 1).prevPosition.X, snakes.ElementAt(i - 1).prevPosition.Y);// + snakeTexture.Height);
                            break;
                        case "down":
                            snakes.ElementAt(i).position = new Vector2(snakes.ElementAt(i - 1).prevPosition.X, snakes.ElementAt(i - 1).prevPosition.Y);// - snakeTexture.Height);
                            break;
                        case "left":
                            snakes.ElementAt(i).position = new Vector2(snakes.ElementAt(i - 1).prevPosition.X, snakes.ElementAt(i - 1).prevPosition.Y);
                            break;
                        case "right":
                            snakes.ElementAt(i).position = new Vector2(snakes.ElementAt(i - 1).prevPosition.X, snakes.ElementAt(i - 1).prevPosition.Y);
                            break;
                    }
                    snakePositions[i] = snakes.ElementAt(i).position;
                    snakes.ElementAt(i).lastDirection = snakes.ElementAt(i - 1).lastDirection;
                }

                for (int i = 0; i < apples.Count(); i++)
                {
                    //Make sure it doesn't leave the bounds
                    Vector2 keepAppleInBounds = utilities.KeepInBounds(apples[i].position, _graphics, apples[i].texture);
                    apples[i].position.X = keepAppleInBounds.X;
                    apples[i].position.Y = keepAppleInBounds.Y;

                    if (utilities.Collides(snakeTexture, apples[i].texture, snakes.ElementAt(0).position, apples[i].position))
                    {
                        Snake addSnake = new Snake(snakeTexture);
                        switch (snakes.ElementAt(0).currDirection)
                        {
                            case "up":
                                addSnake.position = new Vector2(snakes.ElementAt(snakes.Count() - 1).position.X, snakes.ElementAt(snakes.Count() - 1).position.Y);// + snakeTexture.Height);
                                break;
                            case "down":
                                addSnake.position = new Vector2(snakes.ElementAt(snakes.Count() - 1).position.X, snakes.ElementAt(snakes.Count() - 1).position.Y);// - snakeTexture.Height);
                                break;
                            case "left":
                                addSnake.position = new Vector2(snakes.ElementAt(snakes.Count() - 1).position.X, snakes.ElementAt(snakes.Count() - 1).position.Y);
                                break;
                            case "right":
                                addSnake.position = new Vector2(snakes.ElementAt(snakes.Count() - 1).position.X, snakes.ElementAt(snakes.Count() - 1).position.Y);
                                break;
                        }
                        snakePositions.Add(addSnake.position);
                        snakes.Enqueue(addSnake);
                        snakeSpeed += 8f;
                        score += 100;
                        apples.RemoveAt(i);
                    }
                }

                if (apples.Count() == 0)
                {
                    Apple apple = new Apple(appleTexture, new Vector2(random.Next(0, _graphics.PreferredBackBufferHeight), random.Next(0, _graphics.PreferredBackBufferWidth)));
                    apples.Add(apple);
                }

                Vector2 keepSnakeInBounds = utilities.KeepInBounds(snakes.ElementAt(0).position, _graphics, snakeTexture);
                snakes.ElementAt(0).position = keepSnakeInBounds;

                if (utilities.CollideWithBounds(snakes.ElementAt(0).position, _graphics, snakeTexture))
                {
                    gameOver = true;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (gameOver)
            {
                _spriteBatch.DrawString(spCurrSpeed, "GAME OVER" + Environment.NewLine + "Score: " + score,
                    new Vector2((_graphics.PreferredBackBufferWidth - spGameOver.MeasureString("GAME OVER").X) / 2,
                    (_graphics.PreferredBackBufferHeight / 2) - spGameOver.MeasureString("GAME OVER").Y), Color.White);
            }
            else
            {
                //snake.Draw(_spriteBatch);
                for (int i = 0; i < snakes.Count(); i++)
                {
                    snakes.ElementAt(i).Draw(_spriteBatch);
                    //_spriteBatch.Draw(snakeTexture, snakes.ElementAt(i).position, null, Color.White, 0f,
                    //    new Vector2(snakePositions[i].X, snakePositions[i].Y), Vector2.One, SpriteEffects.None, 0f);
                }
                foreach (Apple apple in apples)
                {
                    apple.Draw(_spriteBatch);
                }
                _spriteBatch.DrawString(spScore, "Score: " + score, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(spCurrSpeed, "Current Speed: " + snakeSpeed, new Vector2(0, 15), Color.White);
            }
            _spriteBatch.End();

            if (gameOver)
            {
                KeyboardState kstate = Keyboard.GetState();
                if (kstate.IsKeyDown(Keys.R))
                {
                    gameOver = false;
                    Initialize();
                }
            }

            base.Draw(gameTime);
        }
    }
}
