using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;
namespace DespicableGame
{
    class Trap : Collectible, Observer.Observer
    {
        public bool Activated { get; set; }
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
            Activated = false;
        }

        public override void Effect(Character character)
        {
            Activated = true;
            character.SPEED = RandomManager.GetRandomInt(1,3-1);
            affectedCharacter = character;
            NotifyAllObservers(Subject.NotifyReason.TRAP_ACTIVATED);
        }

        public void Notify(Subject subject, Subject.NotifyReason reason)
        {
            Activated = false;
        }

        public override void FindCollisions(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                if (this.CurrentTile == character.Destination)
                {
                    Effect(character);
                }

            }
        }
    }
}
