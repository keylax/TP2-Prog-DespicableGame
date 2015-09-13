using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;
namespace DespicableGame
{
    class Trap : Collectible
    {
        private Character affectedCharacter;

        public Character AffectedCharacter
        {
            get
            {
                return affectedCharacter;
            }
        }

        public Trap(Texture2D drawing, Vector2 position, Tile CurrentTile)
            : base(drawing, position, CurrentTile)
        {
        }

        public override void Effect(Character character)
        {
            Active = false;
            character.SPEED = RandomManager.GetRandomInt(1,3-1);
            affectedCharacter = character;
            NotifyAllObservers(Subject.NotifyReason.TRAP_ACTIVATED);
        }



    }
}
