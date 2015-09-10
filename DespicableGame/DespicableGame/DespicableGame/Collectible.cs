﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DespicableGame
{
    public abstract class Collectible
    {
        public bool Active{get; set;}
        protected Texture2D drawing;
        protected Vector2 position;
        public Tile CurrentTile { get; set; }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Collectible(Texture2D sprite, Vector2 position, Tile currentTile)
        {
            Active = true;
            drawing = sprite;
            this.position = position;
            CurrentTile = currentTile;
        }

        public abstract void Effect(Character character);

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(drawing, position, Color.White);
        }

        public void FindCollisions(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                if (character is PlayerCharacter)
                {
                    if (this.CurrentTile == character.Destination)
                    {
                        Effect(character);
                    }
                }
            }
        }
    }
}
