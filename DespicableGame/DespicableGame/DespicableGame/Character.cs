using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public abstract class Character
    {
        protected Texture2D drawing;
        protected Vector2 position;

        public Tile CurrentTile { get; set; }
        public Tile Destination { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }

        public Character(Texture2D sprite, Vector2 position, Tile actualCase)
        {
            SpeedX = 0;
            SpeedY = 0;

            drawing = sprite;
            this.position = position;
            CurrentTile = actualCase;
        }

        public abstract void Move();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(drawing, position, Color.White);
        }

    }
}