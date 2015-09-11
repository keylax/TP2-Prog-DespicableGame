using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;

namespace DespicableGame
{
    public abstract class Character : Subject
    {
        protected Texture2D drawing;
        protected Vector2 position;
        protected bool isFriendly;

        public Tile CurrentTile { get; set; }
        public Tile Destination { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }

        public bool IsFriendly
        {
            get
            {
                return isFriendly;
            }
        }

        public Character(Texture2D sprite, Vector2 position, Tile currentTile, bool isFriendly = true)
        {
            SpeedX = 0;
            SpeedY = 0;

            drawing = sprite;
            this.position = position;
            CurrentTile = currentTile;
            this.isFriendly = isFriendly;
        }

        public abstract void Move();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(drawing, position, Color.White);
        }

    }
}