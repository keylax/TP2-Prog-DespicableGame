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
        public enum TrapType { SLOW, SNARE }
        TimeSpan lastActivated = new TimeSpan(0, 0, 0);
        public bool Activated { get; set; }
        public TrapType trapType { get; set; }
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
            DetermineType();
        }

        public override void Effect(Character character)
        {
            Activated = true;

            switch (trapType)
            {
                case TrapType.SLOW:
                    character.SPEED = 2;
                    break;
                case TrapType.SNARE:
                    character.SPEED = 1;
                    break;
            }
            affectedCharacters.Add(character);
            NotifyAllObservers(Subject.NotifyReason.TRAP_ACTIVATED);
        }

        public void EffectExpire()
        {
            switch (trapType)
            {
                case TrapType.SLOW:
                    foreach (Character character in affectedCharacters)
                    {
                        character.SPEED = 4;
                    }
                    break;
                case TrapType.SNARE:
                    foreach (Character character in affectedCharacters)
                    {
                        character.SPEED = 4;
                    }
                    break;
            }
            affectedCharacters.Clear();
        }

        public void Disarm()
        {
            Activated = false;
        }

        private void DetermineType()
        {
            int type = RandomManager.GetRandomInt(0, 2);
            switch (type)
            {
                case 1:
                    trapType = TrapType.SLOW;
                    break;
                case 2:
                    trapType = TrapType.SNARE;
                    break;
            }
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
