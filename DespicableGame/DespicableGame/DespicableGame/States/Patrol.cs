using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    class Patrol : AIStates
    {
        private readonly NonPlayerCharacter character;

        public Patrol(NonPlayerCharacter character)
        {
            this.character = character;
        }

        public void OnUpdate()
        {
            List<Tile> possibleTiles = new List<Tile>();

            if (!(character.CurrentTile.TileUp == null || character.CurrentTile.TileUp is Teleporter))
            {
                possibleTiles.Add(character.CurrentTile.TileUp);
            }

            if (!(character.CurrentTile.TileDown == null || character.CurrentTile.TileDown is Teleporter))
            {
                possibleTiles.Add(character.CurrentTile.TileDown);
            }

            if (!(character.CurrentTile.TileLeft == null || character.CurrentTile.TileLeft is Teleporter))
            {
                possibleTiles.Add(character.CurrentTile.TileLeft);
            }

            if (!(character.CurrentTile.TileRight == null || character.CurrentTile.TileRight is Teleporter))
            {
                possibleTiles.Add(character.CurrentTile.TileRight);
            }

            int randomChoice = RandomManager.GetRandomInt(0, possibleTiles.Count - 1);

            character.Destination = possibleTiles[randomChoice];

            if (character.Destination == character.CurrentTile.TileUp)
            {
                character.SpeedX = 0;
                character.SpeedY = -Character.SPEED;
                character.Destination = character.CurrentTile.TileUp;
            }
            else if (character.Destination == character.CurrentTile.TileDown)
            {
                character.SpeedX = 0;
                character.SpeedY = Character.SPEED;
                character.Destination = character.CurrentTile.TileDown;
            }
            else if (character.Destination == character.CurrentTile.TileLeft)
            {
                character.SpeedX = -Character.SPEED;
                character.SpeedY = 0;
                character.Destination = character.CurrentTile.TileLeft;
            }
            else
            {
                character.SpeedX = Character.SPEED;
                character.SpeedY = 0;
                character.Destination = character.CurrentTile.TileRight;
            }
        }

    }
}