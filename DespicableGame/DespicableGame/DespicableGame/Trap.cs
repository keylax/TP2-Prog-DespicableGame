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

        TimeSpan lastActivated = new TimeSpan(0, 0, 0);
        public bool Activated { get; set; }

        private List<Character> affectedCharacters = new List<Character>();
        public TimeSpan LastActivated
        {
            get
            {
                return lastActivated;
            }
            set
            {
                lastActivated = value;
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
            affectedCharacters.Add(character);
            NotifyAllObservers(Subject.NotifyReason.TRAP_ACTIVATED);
        }

        public void EffectExpire()
        {
            foreach (Character character in affectedCharacters)
            {
                character.SPEED = 4;
            }
            affectedCharacters.Clear();
        }

        public void Disarm()
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
