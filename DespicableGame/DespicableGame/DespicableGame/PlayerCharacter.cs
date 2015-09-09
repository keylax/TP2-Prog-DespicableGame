using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    class PlayerCharacter : Character
    {
        public PlayerCharacter(Texture2D dessin, Vector2 position, Tile ActualCase)
            : base(dessin, position, ActualCase)
        {
            Destination = null;
        }

        //Algo assez ordinaire.  Pour que ça fonctionne, la vitesse doit être un diviseur entier de 64, pourrait être à revoir.
        public override void Mouvement()
        {
            if (Destination != null)
            {
                position.X += VitesseX;
                position.Y += VitesseY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    ActualCase = Destination;
                    Destination = null;
                }
            }
        }

        public void VerifierMouvement(Tile caseDestionation, int vitesseX, int vitesseY)
        {
            //Si la direction choisie n'est pas nulle
            if (caseDestionation != null)
            {
                //On vérifie si la case est un téléporteur
                Tile testTeleportation = TestTeleporter(caseDestionation);

                //Si non, on bouge
                if (testTeleportation == null)
                {
                    Destination = caseDestionation;
                    VitesseX = vitesseX;
                    VitesseY = vitesseY;
                }
                //Si oui, on se téléporte.
                else
                {
                    ActualCase = testTeleportation;
                    position = new Vector2(ActualCase.GetPosition().X, ActualCase.GetPosition().Y);
                }
            }
        }

        private Tile TestTeleporter(Tile laCase)
        {
            if (laCase is Teleporter)
            {
                return ((Teleporter)laCase).Teleport();
            }
            return null;
        }
    }
}
