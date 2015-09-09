using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    class Labyrinth
    {
        private Tile[][] Cases;
        public const int LARGEUR = 14;
        public const int HAUTEUR = 10;

        //Génération des cases, outils fait maison pour le construire... en partie
        public Labyrinth()
        {
            Cases = new Tile[LARGEUR][];

            for (int i = 0; i < LARGEUR; i++)
            {
                Cases[i] = new Tile[HAUTEUR];
            }

            Cases[0][0] = new Tile(5, 0, 0);
            Cases[0][1] = new Tile(6, 0, 1);
            Cases[0][2] = new Tile(5, 0, 2);
            Cases[0][3] = new Tile(6, 0, 3);
            Cases[0][4] = new Tile(13, 0, 4);
            Cases[0][5] = new Tile(14, 0, 5);
            Cases[0][6] = new Tile(5, 0, 6);
            Cases[0][7] = new Tile(4, 0, 7);
            Cases[0][8] = new Tile(12, 0, 8);
            Cases[0][9] = new Tile(6, 0, 9);

            Cases[1][0] = new Tile(3, 1, 0);
            Cases[1][1] = new Tile(9, 1, 1);
            Cases[1][2] = new Tile(10, 1, 2);
            Cases[1][3] = new Tile(1, 1, 3);
            Cases[1][4] = new Tile(4, 1, 4);
            Cases[1][5] = new Tile(12, 1, 5);
            Cases[1][6] = new Tile(2, 1, 6);
            Cases[1][7] = new Tile(3, 1, 7);
            Cases[1][8] = new Tile(5, 1, 8);
            Cases[1][9] = new Tile(10, 1, 9);

            Cases[2][0] = new Tile(3, 2, 0);
            Cases[2][1] = new Tile(13, 2, 1);
            Cases[2][2] = new Tile(4, 2, 2);
            Cases[2][3] = new Tile(2, 2, 3);
            Cases[2][4] = new Tile(9, 2, 4);
            Cases[2][5] = new Tile(6, 2, 5);
            Cases[2][6] = new Tile(3, 2, 6);
            Cases[2][7] = new Tile(11, 2, 7);
            Cases[2][8] = new Tile(3, 2, 8);
            Cases[2][9] = new Tile(7, 2, 9);

            Cases[3][0] = new Tile(1, 3, 0);
            Cases[3][1] = new Tile(12, 3, 1);
            Cases[3][2] = new Tile(10, 3, 2);
            Cases[3][3] = new Tile(9, 3, 3);
            Cases[3][4] = new Tile(6, 3, 4);
            Cases[3][5] = new Tile(9, 3, 5);
            Cases[3][6] = new Tile(8, 3, 6);
            Cases[3][7] = new Tile(6, 3, 7);
            Cases[3][8] = new Tile(1, 3, 8);
            Cases[3][9] = new Tile(10, 3, 9);

            Cases[4][0] = new Tile(3, 4, 0);
            Cases[4][1] = new Tile(5, 4, 1);
            Cases[4][2] = new Tile(6, 4, 2);
            Cases[4][3] = new Tile(5, 4, 3);
            Cases[4][4] = new Tile(8, 4, 4);
            Cases[4][5] = new Tile(4, 4, 5);
            Cases[4][6] = new Tile(6, 4, 6);
            Cases[4][7] = new Tile(9, 4, 7);
            Cases[4][8] = new Tile(8, 4, 8);
            Cases[4][9] = new Tile(6, 4, 9);

            Cases[5][0] = new Tile(9, 5, 0);
            Cases[5][1] = new Tile(2, 5, 1);
            Cases[5][2] = new Tile(3, 5, 2);
            Cases[5][3] = new Tile(3, 5, 3);
            Cases[5][4] = new Tile(5, 5, 4);
            Cases[5][5] = new Tile(10, 5, 5);
            Cases[5][6] = new Tile(9, 5, 6);
            Cases[5][7] = new Tile(4, 5, 7);
            Cases[5][8] = new Tile(4, 5, 8);
            Cases[5][9] = new Tile(10, 5, 9);

            Cases[6][0] = new Tile(7, 6, 0);
            Cases[6][1] = new Tile(3, 6, 1);
            Cases[6][2] = new Tile(9, 6, 2);
            Cases[6][3] = new Tile(8, 6, 3);
            Cases[6][4] = new Tile(10, 6, 4);
            Cases[6][5] = new Tile(5, 6, 5);
            Cases[6][6] = new Tile(12, 6, 6);
            Cases[6][7] = new Tile(2, 6, 7);
            Cases[6][8] = new Tile(3, 6, 8);
            Cases[6][9] = new Tile(7, 6, 9);

            Cases[7][0] = new Tile(11, 7, 0);
            Cases[7][1] = new Tile(1, 7, 1);
            Cases[7][2] = new Tile(4, 7, 2);
            Cases[7][3] = new Tile(12, 7, 3);
            Cases[7][4] = new Tile(6, 7, 4);
            Cases[7][5] = new Tile(3, 7, 5);
            Cases[7][6] = new Tile(5, 7, 6);
            Cases[7][7] = new Tile(2, 7, 7);
            Cases[7][8] = new Tile(3, 7, 8);
            Cases[7][9] = new Tile(11, 7, 9);

            Cases[8][0] = new Tile(13, 8, 0);
            Cases[8][1] = new Tile(2, 8, 1);
            Cases[8][2] = new Tile(9, 8, 2);
            Cases[8][3] = new Tile(6, 8, 3);
            Cases[8][4] = new Tile(9, 8, 4);
            Cases[8][5] = new Tile(8, 8, 5);
            Cases[8][6] = new Tile(2, 8, 6);
            Cases[8][7] = new Tile(9, 8, 7);
            Cases[8][8] = new Tile(0, 8, 8);
            Cases[8][9] = new Tile(14, 8, 9);

            Cases[9][0] = new Tile(5, 9, 0);
            Cases[9][1] = new Tile(8, 9, 1);
            Cases[9][2] = new Tile(6, 9, 2);
            Cases[9][3] = new Tile(9, 9, 3);
            Cases[9][4] = new Tile(4, 9, 4);
            Cases[9][5] = new Tile(12, 9, 5);
            Cases[9][6] = new Tile(10, 9, 6);
            Cases[9][7] = new Tile(5, 9, 7);
            Cases[9][8] = new Tile(8, 9, 8);
            Cases[9][9] = new Tile(14, 9, 9);

            Cases[10][0] = new Tile(3, 10, 0);
            Cases[10][1] = new Tile(5, 10, 1);
            Cases[10][2] = new Tile(0, 10, 2);
            Cases[10][3] = new Tile(12, 10, 3);
            Cases[10][4] = new Tile(8, 10, 4);
            Cases[10][5] = new Tile(12, 10, 5);
            Cases[10][6] = new Tile(6, 10, 6);
            Cases[10][7] = new Tile(3, 10, 7);
            Cases[10][8] = new Tile(5, 10, 8);
            Cases[10][9] = new Tile(6, 10, 9);

            Cases[11][0] = new Tile(3, 11, 0);
            Cases[11][1] = new Tile(3, 11, 1);
            Cases[11][2] = new Tile(1, 11, 2);
            Cases[11][3] = new Tile(6, 11, 3);
            Cases[11][4] = new Tile(5, 11, 4);
            Cases[11][5] = new Tile(12, 11, 5);
            Cases[11][6] = new Tile(8, 11, 6);
            Cases[11][7] = new Tile(2, 11, 7);
            Cases[11][8] = new Tile(3, 11, 8);
            Cases[11][9] = new Tile(3, 11, 9);

            Cases[12][0] = new Tile(9, 12, 0);
            Cases[12][1] = new Tile(2, 12, 1);
            Cases[12][2] = new Tile(11, 12, 2);
            Cases[12][3] = new Tile(1, 12, 3);
            Cases[12][4] = new Tile(8, 12, 4);
            Cases[12][5] = new Tile(12, 12, 5);
            Cases[12][6] = new Tile(4, 12, 6);
            Cases[12][7] = new Tile(8, 12, 7);
            Cases[12][8] = new Tile(10, 12, 8);
            Cases[12][9] = new Tile(3, 12, 9);

            Cases[13][0] = new Tile(13, 13, 0);
            Cases[13][1] = new Tile(8, 13, 1);
            Cases[13][2] = new Tile(12, 13, 2);
            Cases[13][3] = new Tile(10, 13, 3);
            Cases[13][4] = new Tile(13, 13, 4);
            Cases[13][5] = new Tile(14, 13, 5);
            Cases[13][6] = new Tile(9, 13, 6);
            Cases[13][7] = new Tile(12, 13, 7);
            Cases[13][8] = new Tile(12, 13, 8);
            Cases[13][9] = new Tile(10, 13, 9);

            for (int i = 0; i < LARGEUR; i++)
            {
                for (int j = 0; j < HAUTEUR; j++)
                {
                    if (!Cases[i][j].IsWallTop())
                    {
                        Cases[i][j].TileUp = Cases[i][j - 1];
                    }

                    if (!Cases[i][j].IsWallDown())
                    {
                        Cases[i][j].TileDown = Cases[i][j + 1];
                    }

                    if (!Cases[i][j].IsWallLeft())
                    {
                        Cases[i][j].TileLeft = Cases[i - 1][j];
                    }

                    if (!Cases[i][j].IsWallRight())
                    {
                        Cases[i][j].TileRight = Cases[i + 1][j];
                    }
                }
            }

            //Pour tester le policier, on lui offre une ouverture
            Cases[6][9].TileUp = Cases[6][8];
            Cases[7][9].TileUp = Cases[7][8];

            //On place le téléporteur
            Teleporter teleporter = new Teleporter(Cases[0][0], Cases[13][0], Cases[0][9], Cases[13][9]);
            Cases[6][4].TileRight = teleporter;
            Cases[7][4].TileLeft = teleporter;

        }

        public Tile GetCase(int x, int y)
        {
            return Cases[x][y];
        }

        public void DessinerHorizontal(SpriteBatch spriteBatch, Texture2D horizontale)
        {
            Vector2 haut = Cases[0][0].GetPosition();
            haut.Y -= Tile.LIGN_SIZE;

            Vector2 bas = Cases[0][HAUTEUR - 1].GetPosition();
            bas.Y += Tile.SIZE_TILE;

            for (int i = 0; i < LARGEUR; i++)
            {
                spriteBatch.Draw(horizontale, haut, Color.White);
                spriteBatch.Draw(horizontale, bas, Color.White);
                haut.X += Tile.SIZE_TILE;
                bas.X += Tile.SIZE_TILE;
            }
        }

        public void DessinerVertical(SpriteBatch spriteBatch, Texture2D verticale)
        {
            Vector2 gauche = Cases[0][0].GetPosition();
            gauche.X -= Tile.LIGN_SIZE;

            Vector2 droite = Cases[LARGEUR-1][0].GetPosition();
            droite.X += Tile.SIZE_TILE;

            for (int i = 0; i < HAUTEUR; i++)
            {
                spriteBatch.Draw(verticale, gauche, Color.White);
                spriteBatch.Draw(verticale, droite, Color.White);
                gauche.Y += Tile.SIZE_TILE;
                droite.Y += Tile.SIZE_TILE;
            }
        }
    }
}
