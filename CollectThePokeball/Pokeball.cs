using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CollectThePokeball
{
    class Pokeball
    {
        protected Vector2 location;
        int winHeight, winWidth;
        Random rand;
        protected Texture2D pokeball;
        protected Rectangle pokeRect;

        public Pokeball(int winHeight, int winWidth, Texture2D pokeball)
        {
            this.winHeight = winHeight;
            this.winWidth = winWidth;
            this.pokeball = pokeball;
            rand = new Random();

            SpawnNewBall();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pokeball, location, Color.White);
        }

        public void SpawnNewBall()
        {
            //24px is the width of the pokeball sprite
            location.X = rand.Next(winWidth - 24);
            location.Y = rand.Next(winHeight - 24);

            pokeRect = new Rectangle((int)location.X, (int)location.Y, pokeball.Width, pokeball.Height);
        }

        public virtual bool IsCollected(Rectangle pikaRect)
        {
            if (pikaRect.Intersects(pokeRect))
            {
                SpawnNewBall();
                return true;
            }

            return false;
        }
    }
}