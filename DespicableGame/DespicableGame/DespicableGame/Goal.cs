using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DespicableGame
{
    class Goal: Trap
    {
        public Goal(Texture2D drawing, Vector2 position, Tile CurrentTile): base(drawing, position, CurrentTile)
        {

        }

        public override void Effect(Character character)
        {
            if (character is PlayerCharacter)
            {
                ((PlayerCharacter)character).GoalCollected++;
            }
            this.Active = false;
        }

    }
}