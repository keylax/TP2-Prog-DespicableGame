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
        public AIStates CurrentState { get; set; }

        public NonPlayerCharacter(Texture2D dessin, Vector2 position, Tile currentTile, bool isFriendly)
            : base(dessin, position, currentTile, isFriendly)
        {
            CurrentState = new Patrol(this);
        }

        public override void Act()
        {
            if (Destination != null && Stunned == false)
            {
                position.X += SpeedX;
                position.Y += SpeedY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    CurrentState.OnUpdate();                   
                }
            }
        }

        public void SetSpeedToDestination()
        {
            if (Destination == CurrentTile.TileUp)
            {
                SpeedX = 0;
                SpeedY = -SPEED;
            }
            else if (Destination == CurrentTile.TileDown)
            {
                SpeedX = 0;
                SpeedY = SPEED;
            }
            else if (Destination == CurrentTile.TileLeft)
            {
                SpeedX = -SPEED;
                SpeedY = 0;
            }
            else if (Destination == CurrentTile.TileRight)
            {
                SpeedX = SPEED;
                SpeedY = 0;
            }
        }

        public bool SeesGru()
        {
            Tile exploreTile = Destination;
            bool foundGru = false;

            while (!foundGru && exploreTile.TileRight != null)
            {
                exploreTile = exploreTile.TileRight;

                if (exploreTile == GameManager.GetInstance().Gru.CurrentTile || GameManager.GetInstance().Gru.Destination == exploreTile)
                {
                    foundGru = true;
                }
            }

            exploreTile = Destination;

            while (!foundGru && exploreTile.TileLeft != null)
            {
                exploreTile = exploreTile.TileLeft;

                if (exploreTile == GameManager.GetInstance().Gru.CurrentTile || GameManager.GetInstance().Gru.Destination == exploreTile)
                {
                    foundGru = true;
                }
            }

            exploreTile = Destination;

            while (!foundGru && exploreTile.TileUp != null)
            {
                exploreTile = exploreTile.TileUp;

                if (exploreTile == GameManager.GetInstance().Gru.CurrentTile || GameManager.GetInstance().Gru.Destination == exploreTile)
                {
                    foundGru = true;
                }
            }

            exploreTile = Destination;

            while (!foundGru && exploreTile.TileDown != null)
            {
                exploreTile = exploreTile.TileDown;

                if (exploreTile == GameManager.GetInstance().Gru.CurrentTile || GameManager.GetInstance().Gru.Destination == exploreTile)
                {
                    foundGru = true;
                }
            }

            return foundGru;
        }

    }
}