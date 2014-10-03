#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace CollectThePokeball
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Controls controls;

        Texture2D pikachu;
        Texture2D pokeball;

        SpriteFont font;

        SoundEffect ding;
        SoundEffect crash;
        Song chiptune;

        public Vector2 pikaLocation;

        bool gameOver;
        public bool isFlipped;

        Pokeball pokeB;
        Stack<Poisonball> poisonB;
        int totalPoints;

        public MainGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pikaLocation = new Vector2(400, 300);
            totalPoints = 0;
            poisonB = new Stack<Poisonball>();
            gameOver = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pikachu = this.Content.Load<Texture2D>("pikachu");
            pokeball = this.Content.Load<Texture2D>("pokeball");
            font = this.Content.Load<SpriteFont>("score");
            ding = this.Content.Load<SoundEffect>("ding1");
            crash = this.Content.Load<SoundEffect>("crash");
            chiptune = this.Content.Load<Song>("chiptune");

            pokeB = new Pokeball(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, pokeball);

            MediaPlayer.Play(chiptune);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {
                controls = new Controls(this);
                controls.CheckForUserInput(1);
                IsOnEdge();

                //Make a new Rect for pikachu and pass it to IsCollected()
                if (pokeB.IsCollected(new Rectangle((int)pikaLocation.X, (int)pikaLocation.Y, pikachu.Width * 2, pikachu.Height * 2)))
                {
                    totalPoints++;
                    ding.Play();
                }

                foreach (Poisonball ball in poisonB)
                {
                    if (ball.IsCollected(new Rectangle((int)pikaLocation.X, (int)pikaLocation.Y, pikachu.Width * 2, pikachu.Height * 2)))
                    {
                        gameOver = true;
                        crash.Play();
                        break;
                    }
                }

                //Adds a new Poisonball every 5 points
                if ((int)(totalPoints / 2) * 2 > poisonB.Count)
                {
                        poisonB.Push(new Poisonball(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, pokeball));
                }
            }
            else
            {
                MediaPlayer.Stop();

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    gameOver = false;
                    totalPoints = 0;
                    poisonB.Clear();
                    pokeB.SpawnNewBall();
                    MediaPlayer.Play(chiptune);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            //Background();

            spriteBatch.Begin();

            if (!gameOver)
            {
                pokeB.Draw(spriteBatch);

                foreach (Poisonball ball in poisonB)
                {
                    ball.Draw(spriteBatch);
                }

                if (isFlipped)
                {
                    spriteBatch.Draw(pikachu,
                                     new Rectangle((int)pikaLocation.X, (int)pikaLocation.Y, pikachu.Width * 2, pikachu.Height * 2),
                                     null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                }
                else
                {
                    spriteBatch.Draw(pikachu, new Rectangle((int)pikaLocation.X, (int)pikaLocation.Y, pikachu.Width * 2, pikachu.Height * 2), Color.White);
                }
            }
            else
            {
                spriteBatch.DrawString(font, "Game Over. Press R to restart.", new Vector2(450, 450), Color.Black);
            }

            spriteBatch.DrawString(font, "Score: " + totalPoints, new Vector2(25, 450), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void IsOnEdge()
        {
            if (pikaLocation.X > graphics.PreferredBackBufferWidth + pikachu.Width)
            {
                pikaLocation.X = 0 - pikachu.Width;
            }
            else if (pikaLocation.X < 0 - pikachu.Width)
            {
                pikaLocation.X = graphics.PreferredBackBufferWidth + pikachu.Width;
            }

            if (pikaLocation.Y > graphics.PreferredBackBufferHeight + pikachu.Height)
            {
                pikaLocation.Y = 0 - pikachu.Height;
            }
            else if (pikaLocation.Y < 0 - pikachu.Height)
            {
                pikaLocation.Y = graphics.PreferredBackBufferHeight + pikachu.Height;
            }
        }

        public void Background()
        {
            Random r = new Random();
            int i = r.Next(1, 6);
            switch (i)
            {
                case 1:
                    GraphicsDevice.Clear(Color.Red);
                    break;
                case 2:
                    GraphicsDevice.Clear(Color.Yellow);
                    break;
                case 3:
                    GraphicsDevice.Clear(Color.Blue);
                    break;
                case 4:
                    GraphicsDevice.Clear(Color.Purple);
                    break;
                case 5:
                    GraphicsDevice.Clear(Color.Magenta);
                    break;
            }
        }
    }
}