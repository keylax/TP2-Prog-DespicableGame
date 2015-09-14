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

        public NonPlayerCharacter(Texture2D drawing, Vector2 position, Tile currentTile, bool isFriendly)
            : base(drawing, position, currentTile, isFriendly)
        {
            CurrentState = new Patrol(this);
            baseSpeed = 2;
            Speed = baseSpeed;
        }

        public void Alert()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.ALERTED_POLICE);
        }

        public void Calm()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.POLICE_OFFICER);
        }

        public void Lurk()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.LURKING_POLICE);
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
                SpeedY = -Speed;
            }
            else if (Destination == CurrentTile.TileDown)
            {
                SpeedX = 0;
                SpeedY = Speed;
            }
            else if (Destination == CurrentTile.TileLeft)
            {
                SpeedX = -Speed;
                SpeedY = 0;
            }
            else if (Destination == CurrentTile.TileRight)
            {
                SpeedX = Speed;
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