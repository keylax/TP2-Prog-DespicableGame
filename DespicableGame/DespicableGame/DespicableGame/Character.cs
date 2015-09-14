using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;

namespace DespicableGame
{
    public abstract class Character : Subject, Observer.Observer
    {
        protected Texture2D drawing;
        protected Vector2 position;
        protected bool isFriendly;
        protected Tile currentTile;
        private bool stunned;
        //64 must be dividable by SPEED
        public int Speed { get; set; }
        protected int baseSpeed;

        public bool Stunned
        {
            get
            {
                return stunned;
            }
            set
            {
                stunned = value;
            }
        }

        public Tile CurrentTile
        {
            get { return currentTile; }

            set
            {
                currentTile = value;
                position = new Vector2(currentTile.GetPosition().X, currentTile.GetPosition().Y);
            }
        }

        public Texture2D Drawing
        {
            get { return drawing; }
        }

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
            Destination = currentTile;
            this.isFriendly = isFriendly;
            stunned = false;
        }

        public abstract void Act();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(drawing, position, Color.White);
        }

        public void Notify(Subject subject, Subject.NotifyReason reason)
        {
            switch (reason)
            {
                case Subject.NotifyReason.TRAP_EXPIRED:
                    Speed = baseSpeed;
                    break;
                case Subject.NotifyReason.SPEEDBOOST_EXPIRED:
                    Speed = baseSpeed;
                    break;
                case Subject.NotifyReason.WOKE_UP:
                    stunned = false;
                    break; 
            }
        }
    }
}