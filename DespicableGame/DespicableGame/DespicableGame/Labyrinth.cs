using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public class Labyrinth
    {
        private Tile[][] Tiles;
        public const int WIDTH = 14;
        public const int HEIGHT = 10;

        //Tile generation, homemade tools to build it (partially...)
        public Labyrinth()
        {
            Tiles = new Tile[WIDTH][];

            for (int i = 0; i < WIDTH; i++)
            {
                Tiles[i] = new Tile[HEIGHT];
            }

            Tiles[0][0] = new Tile(5, 0, 0);
            Tiles[0][1] = new Tile(6, 0, 1);
            Tiles[0][2] = new Tile(5, 0, 2);
            Tiles[0][3] = new Tile(6, 0, 3);
            Tiles[0][4] = new Tile(13, 0, 4);
            Tiles[0][5] = new Tile(14, 0, 5);
            Tiles[0][6] = new Tile(5, 0, 6);
            Tiles[0][7] = new Tile(4, 0, 7);
            Tiles[0][8] = new Tile(12, 0, 8);
            Tiles[0][9] = new Tile(6, 0, 9);

            Tiles[1][0] = new Tile(3, 1, 0);
            Tiles[1][1] = new Tile(9, 1, 1);
            Tiles[1][2] = new Tile(10, 1, 2);
            Tiles[1][3] = new Tile(1, 1, 3);
            Tiles[1][4] = new Tile(4, 1, 4);
            Tiles[1][5] = new Tile(12, 1, 5);
            Tiles[1][6] = new Tile(2, 1, 6);
            Tiles[1][7] = new Tile(3, 1, 7);
            Tiles[1][8] = new Tile(5, 1, 8);
            Tiles[1][9] = new Tile(10, 1, 9);

            Tiles[2][0] = new Tile(3, 2, 0);
            Tiles[2][1] = new Tile(13, 2, 1);
            Tiles[2][2] = new Tile(4, 2, 2);
            Tiles[2][3] = new Tile(2, 2, 3);
            Tiles[2][4] = new Tile(9, 2, 4);
            Tiles[2][5] = new Tile(6, 2, 5);
            Tiles[2][6] = new Tile(3, 2, 6);
            Tiles[2][7] = new Tile(11, 2, 7);
            Tiles[2][8] = new Tile(3, 2, 8);
            Tiles[2][9] = new Tile(7, 2, 9);

            Tiles[3][0] = new Tile(1, 3, 0);
            Tiles[3][1] = new Tile(12, 3, 1);
            Tiles[3][2] = new Tile(10, 3, 2);
            Tiles[3][3] = new Tile(9, 3, 3);
            Tiles[3][4] = new Tile(6, 3, 4);
            Tiles[3][5] = new Tile(9, 3, 5);
            Tiles[3][6] = new Tile(8, 3, 6);
            Tiles[3][7] = new Tile(6, 3, 7);
            Tiles[3][8] = new Tile(1, 3, 8);
            Tiles[3][9] = new Tile(10, 3, 9);

            Tiles[4][0] = new Tile(3, 4, 0);
            Tiles[4][1] = new Tile(5, 4, 1);
            Tiles[4][2] = new Tile(6, 4, 2);
            Tiles[4][3] = new Tile(5, 4, 3);
            Tiles[4][4] = new Tile(8, 4, 4);
            Tiles[4][5] = new Tile(4, 4, 5);
            Tiles[4][6] = new Tile(6, 4, 6);
            Tiles[4][7] = new Tile(9, 4, 7);
            Tiles[4][8] = new Tile(8, 4, 8);
            Tiles[4][9] = new Tile(6, 4, 9);

            Tiles[5][0] = new Tile(9, 5, 0);
            Tiles[5][1] = new Tile(2, 5, 1);
            Tiles[5][2] = new Tile(3, 5, 2);
            Tiles[5][3] = new Tile(3, 5, 3);
            Tiles[5][4] = new Tile(5, 5, 4);
            Tiles[5][5] = new Tile(10, 5, 5);
            Tiles[5][6] = new Tile(9, 5, 6);
            Tiles[5][7] = new Tile(4, 5, 7);
            Tiles[5][8] = new Tile(4, 5, 8);
            Tiles[5][9] = new Tile(10, 5, 9);

            Tiles[6][0] = new Tile(7, 6, 0);
            Tiles[6][1] = new Tile(3, 6, 1);
            Tiles[6][2] = new Tile(9, 6, 2);
            Tiles[6][3] = new Tile(8, 6, 3);
            Tiles[6][4] = new Tile(10, 6, 4);
            Tiles[6][5] = new Tile(5, 6, 5);
            Tiles[6][6] = new Tile(12, 6, 6);
            Tiles[6][7] = new Tile(2, 6, 7);
            Tiles[6][8] = new Tile(3, 6, 8);
            Tiles[6][9] = new Tile(7, 6, 9);

            Tiles[7][0] = new Tile(11, 7, 0);
            Tiles[7][1] = new Tile(1, 7, 1);
            Tiles[7][2] = new Tile(4, 7, 2);
            Tiles[7][3] = new Tile(12, 7, 3);
            Tiles[7][4] = new Tile(6, 7, 4);
            Tiles[7][5] = new Tile(3, 7, 5);
            Tiles[7][6] = new Tile(5, 7, 6);
            Tiles[7][7] = new Tile(2, 7, 7);
            Tiles[7][8] = new Tile(3, 7, 8);
            Tiles[7][9] = new Tile(11, 7, 9);

            Tiles[8][0] = new Tile(13, 8, 0);
            Tiles[8][1] = new Tile(2, 8, 1);
            Tiles[8][2] = new Tile(9, 8, 2);
            Tiles[8][3] = new Tile(6, 8, 3);
            Tiles[8][4] = new Tile(9, 8, 4);
            Tiles[8][5] = new Tile(8, 8, 5);
            Tiles[8][6] = new Tile(2, 8, 6);
            Tiles[8][7] = new Tile(9, 8, 7);
            Tiles[8][8] = new Tile(0, 8, 8);
            Tiles[8][9] = new Tile(14, 8, 9);

            Tiles[9][0] = new Tile(5, 9, 0);
            Tiles[9][1] = new Tile(8, 9, 1);
            Tiles[9][2] = new Tile(6, 9, 2);
            Tiles[9][3] = new Tile(9, 9, 3);
            Tiles[9][4] = new Tile(4, 9, 4);
            Tiles[9][5] = new Tile(12, 9, 5);
            Tiles[9][6] = new Tile(10, 9, 6);
            Tiles[9][7] = new Tile(5, 9, 7);
            Tiles[9][8] = new Tile(8, 9, 8);
            Tiles[9][9] = new Tile(14, 9, 9);

            Tiles[10][0] = new Tile(3, 10, 0);
            Tiles[10][1] = new Tile(5, 10, 1);
            Tiles[10][2] = new Tile(0, 10, 2);
            Tiles[10][3] = new Tile(12, 10, 3);
            Tiles[10][4] = new Tile(8, 10, 4);
            Tiles[10][5] = new Tile(12, 10, 5);
            Tiles[10][6] = new Tile(6, 10, 6);
            Tiles[10][7] = new Tile(3, 10, 7);
            Tiles[10][8] = new Tile(5, 10, 8);
            Tiles[10][9] = new Tile(6, 10, 9);

            Tiles[11][0] = new Tile(3, 11, 0);
            Tiles[11][1] = new Tile(3, 11, 1);
            Tiles[11][2] = new Tile(1, 11, 2);
            Tiles[11][3] = new Tile(6, 11, 3);
            Tiles[11][4] = new Tile(5, 11, 4);
            Tiles[11][5] = new Tile(12, 11, 5);
            Tiles[11][6] = new Tile(8, 11, 6);
            Tiles[11][7] = new Tile(2, 11, 7);
            Tiles[11][8] = new Tile(3, 11, 8);
            Tiles[11][9] = new Tile(3, 11, 9);

            Tiles[12][0] = new Tile(9, 12, 0);
            Tiles[12][1] = new Tile(2, 12, 1);
            Tiles[12][2] = new Tile(11, 12, 2);
            Tiles[12][3] = new Tile(1, 12, 3);
            Tiles[12][4] = new Tile(8, 12, 4);
            Tiles[12][5] = new Tile(12, 12, 5);
            Tiles[12][6] = new Tile(4, 12, 6);
            Tiles[12][7] = new Tile(8, 12, 7);
            Tiles[12][8] = new Tile(10, 12, 8);
            Tiles[12][9] = new Tile(3, 12, 9);

            Tiles[13][0] = new Tile(13, 13, 0);
            Tiles[13][1] = new Tile(8, 13, 1);
            Tiles[13][2] = new Tile(12, 13, 2);
            Tiles[13][3] = new Tile(10, 13, 3);
            Tiles[13][4] = new Tile(13, 13, 4);
            Tiles[13][5] = new Tile(14, 13, 5);
            Tiles[13][6] = new Tile(9, 13, 6);
            Tiles[13][7] = new Tile(12, 13, 7);
            Tiles[13][8] = new Tile(12, 13, 8);
            Tiles[13][9] = new Tile(10, 13, 9);

            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    if (!Tiles[i][j].IsWallTop())
                    {
                        Tiles[i][j].TileUp = Tiles[i][j - 1];
                    }

                    if (!Tiles[i][j].IsWallDown())
                    {
                        Tiles[i][j].TileDown = Tiles[i][j + 1];
                    }

                    if (!Tiles[i][j].IsWallLeft())
                    {
                        Tiles[i][j].TileLeft = Tiles[i - 1][j];
                    }

                    if (!Tiles[i][j].IsWallRight())
                    {
                        Tiles[i][j].TileRight = Tiles[i + 1][j];
                    }
                }
            }

            //Pour tester le policier, on lui offre une ouverture
            Tiles[6][9].TileUp = Tiles[6][8];
            Tiles[7][9].TileUp = Tiles[7][8];

            //Placing the teleporters
            Teleporter teleporter = new Teleporter(Tiles[0][0], Tiles[13][0], Tiles[0][9], Tiles[13][9]);
            Tiles[6][4].TileRight = teleporter;
            Tiles[7][4].TileLeft = teleporter;

        }

        public Tile GetCase(int x, int y)
        {
            return Tiles[x][y];
        }

        public void DrawHorizontal(SpriteBatch spriteBatch, Texture2D horizontale)
        {
            Vector2 haut = Tiles[0][0].GetPosition();
            haut.Y -= Tile.LIGN_SIZE;

            Vector2 bas = Tiles[0][HEIGHT - 1].GetPosition();
            bas.Y += Tile.SIZE_TILE;

            for (int i = 0; i < WIDTH; i++)
            {
                spriteBatch.Draw(horizontale, haut, Color.White);
                spriteBatch.Draw(horizontale, bas, Color.White);
                haut.X += Tile.SIZE_TILE;
                bas.X += Tile.SIZE_TILE;
            }
        }

        public void DrawVertical(SpriteBatch spriteBatch, Texture2D verticale)
        {
            Vector2 gauche = Tiles[0][0].GetPosition();
            gauche.X -= Tile.LIGN_SIZE;

            Vector2 droite = Tiles[WIDTH-1][0].GetPosition();
            droite.X += Tile.SIZE_TILE;

            for (int i = 0; i < HEIGHT; i++)
            {
                spriteBatch.Draw(verticale, gauche, Color.White);
                spriteBatch.Draw(verticale, droite, Color.White);
                gauche.Y += Tile.SIZE_TILE;
                droite.Y += Tile.SIZE_TILE;
            }
        }

    }
}