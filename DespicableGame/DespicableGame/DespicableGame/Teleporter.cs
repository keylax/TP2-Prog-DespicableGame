using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public class Teleporter : Tile
    {
        Random r = new Random();

        public Teleporter(Tile caseHaut, Tile caseBas, Tile caseGauche, Tile caseDroite)
        {
            this.TileUp = caseHaut;
            this.TileDown = caseBas;
            this.TileLeft = caseGauche;
            this.TileRight = caseDroite;
        }

        public Tile Teleport()
        {
            int randomNumber = r.Next(4);

            switch (randomNumber)
            {
                case 0:
                    return TileUp;
                case 1:
                    return TileDown;
                case 2:
                    return TileLeft;
                case 3:
                    return TileRight;
                default:
                    return null;
            }
        }

    }
}