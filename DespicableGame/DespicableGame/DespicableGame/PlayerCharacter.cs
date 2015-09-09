using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    class PlayerCharacter : Character
    {
        public PlayerCharacter(Texture2D drawing, Vector2 position, Tile CurrentTile)
            : base(drawing, position, CurrentTile)
        {
            Destination = null;
        }

        //Algo assez ordinaire. Pour que ça fonctionne, la vitesse doit être un diviseur entier de 64, pourrait être à revoir.
        public override void Move()
        {
            if (Destination != null)
            {
                position.X += SpeedX;
                position.Y += SpeedY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    CurrentTile = Destination;
                    Destination = null;
                }
            }
        }

        public void CheckMovement(Tile tileDestination, int vitesseX, int vitesseY)
        {
            //If direction is not null
            if (tileDestination != null)
            {
                //Check if the tile is a teleporter
                Tile testTeleportation = TestTeleporter(tileDestination);

                //Is not, we move
                if (testTeleportation == null)
                {
                    Destination = tileDestination;
                    SpeedX = vitesseX;
                    SpeedY = vitesseY;
                }
                //If so, we teleport ourselves
                else
                {
                    CurrentTile = testTeleportation;
                    position = new Vector2(CurrentTile.GetPosition().X, CurrentTile.GetPosition().Y);
                }
            }
        }

        private Tile TestTeleporter(Tile theTile)
        {
            if (theTile is Teleporter)
            {
                return ((Teleporter)theTile).Teleport();
            }
            return null;
        }

    }
}