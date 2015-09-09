using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    abstract class Character
    {
        protected Texture2D dessin;
        protected Vector2 position;

        public Tile ActualCase { get; set; }
        public Tile Destination { get; set; }
        public int VitesseX { get; set; }
        public int VitesseY { get; set; }

        public Character(Texture2D sprite, Vector2 position, Tile actualCase)
        {
            VitesseX = 0;
            VitesseY = 0;

            dessin = sprite;
            this.position = position;
            ActualCase = actualCase;
        }

        public abstract void Mouvement();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(dessin, position, Color.White);
        }
    }
}
