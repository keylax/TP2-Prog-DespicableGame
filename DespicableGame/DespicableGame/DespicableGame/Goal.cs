using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;

namespace DespicableGame
{
    class Goal : Collectible
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
            Active = false;

            NotifyAllObservers(Subject.NotifyReason.MONEY_DESTROYED);
        }

    }
}