using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    class NonPlayerCharacter : Character
    {
        public NonPlayerCharacter(Texture2D dessin, Vector2 position, Tile ActualCase)
            : base(dessin, position, ActualCase)
        {
            Destination = MouvementIA(ActualCase);
        }

        public override void Mouvement()
        {
            if (Destination != null)
            {
                position.X += VitesseX;
                position.Y += VitesseY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    ActualCase = Destination;
                    Destination = MouvementIA(ActualCase);
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
                        VitesseX = 0;
                        VitesseY = -DespicableGame.VITESSE;
                        return AI_Case.TileUp;
                    }
                }

                if (choixRandom == 1)
                {
                    if (!(AI_Case.TileDown == null || AI_Case.TileDown is Teleporter))
                    {
                        VitesseX = 0;
                        VitesseY = DespicableGame.VITESSE;
                        return AI_Case.TileDown;
                    }
                }

                if (choixRandom == 2)
                {
                    if (!(AI_Case.TileLeft == null || AI_Case.TileLeft is Teleporter))
                    {
                        VitesseX = -DespicableGame.VITESSE;
                        VitesseY = 0;
                        return AI_Case.TileLeft;
                    }
                }

                if (choixRandom == 3)
                {
                    if (!(AI_Case.TileRight == null || AI_Case.TileRight is Teleporter))
                    {
                        VitesseX = DespicableGame.VITESSE;
                        VitesseY = 0;
                        return AI_Case.TileRight;
                    }
                }
            }
        }
    }
}
