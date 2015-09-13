using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;

namespace DespicableGame
{
    class Banana : Collectible
    {
        public Banana(Texture2D drawing, Vector2 position, Tile CurrentTile)
            : base(drawing, position, CurrentTile)
        {

        }

        public override void Effect(Character character)
        {
            character.Notify(this, NotifyReason.STUNNED);
            Active = false;
        }

    }
}