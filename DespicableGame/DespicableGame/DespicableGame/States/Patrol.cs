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
            //Note that at this point the destination is the current and the current is the previous
            Tile chosenTile;
            List<Tile> possibleTiles = new List<Tile>();

            if (!(character.Destination.TileUp == null || character.Destination.TileUp is Teleporter || character.Destination.TileUp == character.CurrentTile))
            {
                possibleTiles.Add(character.Destination.TileUp);
            }

            if (!(character.Destination.TileDown == null || character.Destination.TileDown is Teleporter || character.Destination.TileDown == character.CurrentTile))
            {
                possibleTiles.Add(character.Destination.TileDown);
            }

            if (!(character.Destination.TileLeft == null || character.Destination.TileLeft is Teleporter || character.Destination.TileLeft == character.CurrentTile))
            {
                possibleTiles.Add(character.Destination.TileLeft);
            }

            if (!(character.Destination.TileRight == null || character.Destination.TileRight is Teleporter || character.Destination.TileRight == character.CurrentTile))
            {
                possibleTiles.Add(character.Destination.TileRight);
            }

            if (possibleTiles.Count == 0)
            {
                chosenTile = character.CurrentTile;
            }
            else
            {
                int randomChoice = RandomManager.GetRandomInt(0, possibleTiles.Count - 1);
                chosenTile = possibleTiles[randomChoice];
            }
            
            character.CurrentTile = character.Destination; //the current tile is no longer where he was
            character.Destination = chosenTile; //a new destination has been chosen

            if (character.Destination == character.CurrentTile.TileUp)
            {
                character.SpeedX = 0;
                character.SpeedY = -Character.SPEED;
                //character.Destination = character.CurrentTile.TileUp;
            }
            else if (character.Destination == character.CurrentTile.TileDown)
            {
                character.SpeedX = 0;
                character.SpeedY = Character.SPEED;
                //character.Destination = character.CurrentTile.TileDown;
            }
            else if (character.Destination == character.CurrentTile.TileLeft)
            {
                character.SpeedX = -Character.SPEED;
                character.SpeedY = 0;
                //character.Destination = character.CurrentTile.TileLeft;
            }
            else if (character.Destination == character.CurrentTile.TileRight)
            {
                character.SpeedX = Character.SPEED;
                character.SpeedY = 0;
                //character.Destination = character.CurrentTile.TileRight;
            }
        }

    }
}