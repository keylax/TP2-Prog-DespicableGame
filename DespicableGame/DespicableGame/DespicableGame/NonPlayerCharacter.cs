using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public class NonPlayerCharacter : Character
    {
        public NonPlayerCharacter(Texture2D dessin, Vector2 position, Tile ActualCase)
            : base(dessin, position, ActualCase)
        {
            Destination = MouvementIA(ActualCase);
        }

        public override void Move()
        {
            if (Destination != null)
            {
                position.X += SpeedX;
                position.Y += SpeedY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    CurrentTile = Destination;
                    Destination = MouvementIA(CurrentTile);
                }
            }
        }

        //AI totalement random et qui ne peut pas entrer dans les téléporteurs.  À revoir absolument.
        private Tile MouvementIA(Tile AI_Case)
        {
            Random r = new Random();

            while (true)
            {
                int choixRandom = r.Next(4);

                if (choixRandom == 0)
                {
                    //Plus efficace qu'un &&, dès que la première condition courante est remplie, on arrête le test
                    if (!(AI_Case.TileUp == null || AI_Case.TileUp is Teleporter))
                    {
                        SpeedX = 0;
                        SpeedY = -DespicableGame.VITESSE;
                        return AI_Case.TileUp;
                    }
                }

                if (choixRandom == 1)
                {
                    if (!(AI_Case.TileDown == null || AI_Case.TileDown is Teleporter))
                    {
                        SpeedX = 0;
                        SpeedY = DespicableGame.VITESSE;
                        return AI_Case.TileDown;
                    }
                }

                if (choixRandom == 2)
                {
                    if (!(AI_Case.TileLeft == null || AI_Case.TileLeft is Teleporter))
                    {
                        SpeedX = -DespicableGame.VITESSE;
                        SpeedY = 0;
                        return AI_Case.TileLeft;
                    }
                }

                if (choixRandom == 3)
                {
                    if (!(AI_Case.TileRight == null || AI_Case.TileRight is Teleporter))
                    {
                        SpeedX = DespicableGame.VITESSE;
                        SpeedY = 0;
                        return AI_Case.TileRight;
                    }
                }
            }
        }
    }
}
