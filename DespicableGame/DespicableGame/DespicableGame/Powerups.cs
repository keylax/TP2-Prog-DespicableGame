using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DespicableGame
{
    class Powerups: Collectible
    {
        public Powerups(Texture2D drawing, Vector2 position, Tile CurrentTile): base(drawing, position, CurrentTile)
        {

        }
    }
}
