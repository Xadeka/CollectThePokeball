using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CollectThePokeball
{
    class Poisonball 
        : Pokeball
    {
        public Poisonball(int winHeight, int winWidth, Texture2D pokeball)
            : base(winHeight, winWidth, pokeball)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pokeball, location, Color.Purple);
        }

        public override bool IsCollected(Rectangle pikaRect)
        {
            if (pikaRect.Intersects(pokeRect))
            {
                return true;
            }
            return false;
        }
    }
}
