using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    public enum Limits { haut = 1, bas = 2, gauche = 4, droite = 8 };

    class Tile
    {
        private int contour;
        private Vector2 position;
        private int ordreX;
        private int ordreY;

        //chaque case a des références sur toutes les cases qui l'entoure
        private Tile tileUp = null;
        private Tile tileDown = null;
        private Tile tileLeft = null;
        private Tile tileRight = null;

        public Tile TileUp
        {
            get { return tileUp; }
            set { tileUp = value; }
        }

        public Tile TileDown
        {
            get { return tileDown; }
            set { tileDown = value; }
        }

        public Tile TileLeft
        {
            get { return tileLeft; }
            set { tileLeft = value; }
        }

        public Tile TileRight
        {
            get { return tileRight; }
            set { tileRight = value; }
        }

        //Décalage en pixel du labyrinthe par rapport à la position 0,0
        //Pourrait être porté par la classe labyrinthe
        public const int GAP_X = 64;
        public const int GAP_Y = 64;

        public const int SIZE_TILE = 64;

        public const int OFFSET_TILE = 56;

        public const int LIGN_SIZE = 8;

        //Pour le téléporteur, qui est un type de case spéciale
        protected Tile()
        {

        }

        public Tile(int contour, int ordreX, int ordreY)
        {
            //Contour: ce qu'on vérifie c'est les présences bit à bit: premier bit = mur haut, second = mur bas, troisième = gauche, quatrière droite
            this.contour = contour;
            this.ordreX = ordreX;
            this.ordreY = ordreY;
            this.position.X = ordreX * SIZE_TILE + GAP_X;
            this.position.Y = ordreY * SIZE_TILE + GAP_Y;
        }

        public int Contour
        {
            get { return contour; }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public bool IsWallTop()
        {
            //Binary comparaison on the 1st bit
            if ((contour & (int)Limits.haut) != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsWallDown()
        {
            //Binary comparaison on the 2nd bit
            if ((contour & (int)Limits.bas) != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsWallLeft()
        {
            //Binary comparaison on the 3rd bit
            if ((contour & (int)Limits.gauche) != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsWallRight()
        {
            //Binary comparaison on the 4th bit
            if ((contour & (int)Limits.droite) != 0)
            {
                return true;
            }
            return false;
        }

        public void DrawWalls(SpriteBatch spriteBatch, Texture2D horizontale, Texture2D verticale)
        {
            //If there is a wall, we draw it
            if (IsWallTop())
            {
                spriteBatch.Draw(horizontale, position, Color.White);
            }

            if (IsWallDown())
            {
                position.Y += OFFSET_TILE;
                spriteBatch.Draw(horizontale, position, Color.White);
                position.Y -= OFFSET_TILE;
            }

            if (IsWallLeft())
            {
                spriteBatch.Draw(verticale, position, Color.White);
            }

            if (IsWallRight())
            {
                position.X += OFFSET_TILE;
                spriteBatch.Draw(verticale, position, Color.White);
                position.X -= OFFSET_TILE;
            }
        }

    }
}