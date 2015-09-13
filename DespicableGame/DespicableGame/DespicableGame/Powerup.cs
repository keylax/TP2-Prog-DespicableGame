using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;
namespace DespicableGame
{
    class Powerup: Collectible
    {
        public enum PowerupType { SPEEDBOOST, PLAYERTRAP}

        private PowerupType type;

        public PowerupType Type
        {
            get
            {
                return type;
            }
        }
        public Powerup(Texture2D drawing, Vector2 position, Tile CurrentTile, PowerupType type): base(drawing, position, CurrentTile)
        {
            this.type = type;
        }

        public override void Effect(Character character)
        {
            if (character is PlayerCharacter)
            {
                ((PlayerCharacter)character).PowerUpInStore = this;
            }
            Active = false;
        }

    }
}
