using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.States;

namespace DespicableGame
{
    public class NonPlayerCharacter : Character
    {
        private AIStates currentState;

        public NonPlayerCharacter(Texture2D dessin, Vector2 position, Tile currentTile, bool isFriendly)
            : base(dessin, position, currentTile, isFriendly)
        {
            currentState = new Patrol(this);
            currentState.OnUpdate();
        }

        public override void Act()
        {
            if (Destination != null)
            {
                position.X += SpeedX;
                position.Y += SpeedY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    currentState.OnUpdate();
                    //CurrentTile = Destination;
                    
                }
            }
        }

    }
}