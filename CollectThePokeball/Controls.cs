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

namespace CollectThePokeball
{
    public class Controls
    {
        MainGame mg;

        public Controls(MainGame mg)
        {
            this.mg = mg;
        }

        public void CheckForUserInput(int multiplier)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                mg.Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mg.pikaLocation.X -= 3 * multiplier;
                mg.pikaLocation.Y -= 4 * multiplier;
                mg.isFlipped = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mg.pikaLocation.X += 3 * multiplier;
                mg.pikaLocation.Y -= 4 * multiplier;
                mg.isFlipped = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mg.pikaLocation.X -= 3 * multiplier;
                mg.pikaLocation.Y += 4 * multiplier;
                mg.isFlipped = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mg.pikaLocation.X += 3 * multiplier;
                mg.pikaLocation.Y += 4 * multiplier;
                mg.isFlipped = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
                mg.pikaLocation.Y -= 5 * multiplier;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mg.pikaLocation.X -= 5 * multiplier;
                mg.isFlipped = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                mg.pikaLocation.Y += 5 * multiplier;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mg.pikaLocation.X += 5 * multiplier;
                mg.isFlipped = true;
            }
        }
    }
}
