using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;

namespace DespicableGame
{
    class Ship : Trap
    {
        public Ship(Texture2D drawing, Vector2 position, Tile CurrentTile) : base(drawing, position, CurrentTile)
        {

        }

        public override void Effect(Character character)
        {
            if (character is PlayerCharacter)
            {
                NotifyAllObservers(Subject.NotifyReason.EXIT_REACHED);
            }
            Active = false;
        }

    }
}