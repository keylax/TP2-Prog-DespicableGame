using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.States;
namespace DespicableGame.States
{
    class PowerupMinion : Minion
    {
        public PowerupMinion(Texture2D dessin, Vector2 position, Tile currentTile, bool isFriendly)
            : base(dessin, position, currentTile, isFriendly)
        {
            CurrentState = new Wander(this);
        }

        public override void JustMinionThings()
        {
            GameManager.GetInstance().Notify(this, Observer.Subject.NotifyReason.MINION_DROP_POWERUP);
        }
    }
}