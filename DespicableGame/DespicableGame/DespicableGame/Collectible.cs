using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DespicableGame
{
    public class Collectible
    {
        protected Texture2D drawing;
        protected Vector2 position;
        public Tile CurrentTile { get; set; }


        public Collectible(Texture2D sprite, Vector2 position, Tile currentTile)
        {

            drawing = sprite;
            this.position = position;
            CurrentTile = currentTile;
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(drawing, position, Color.White);
        }
    }
}
