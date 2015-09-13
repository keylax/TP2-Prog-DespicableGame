using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    class Lurking : AIStates
    {
        private readonly NonPlayerCharacter character;
        private readonly Tile tileToLurkAround;

        public Lurking(NonPlayerCharacter character, Tile tileToLurkAround)
        {
            this.character = character;
            this.tileToLurkAround = tileToLurkAround;
        }

        public void OnUpdate()
        {
            //Note that at this point the destination is the current and the current is the previous
            if (character.SeesGru())
            {
                character.CurrentState = new CatchGru(character);
                character.CurrentState.OnUpdate();
            }
            else if (character.Destination == tileToLurkAround)
            {
                character.CurrentState = new Patrol(character);
                character.CurrentState.OnUpdate();
            }
            else
            {
                List<Tile> possibleDirections = new List<Tile>();
                Tile chosenTile = null;

                if (tileToLurkAround.PositionY < character.Destination.PositionY && character.Destination.TileUp != null)
                {
                    possibleDirections.Add(character.Destination.TileUp);
                }
                else if (character.Destination.TileDown != null)
                {
                    possibleDirections.Add(character.Destination.TileDown);
                }

                if (tileToLurkAround.PositionX < character.Destination.PositionX && character.Destination.TileLeft != null)
                {
                    possibleDirections.Add(character.Destination.TileLeft);
                }
                else if (character.Destination.TileRight != null)
                {
                    possibleDirections.Add(character.Destination.TileRight);
                }

                chosenTile = possibleDirections[0];

                character.CurrentTile = character.Destination; //the current tile is no longer where he was
                character.Destination = chosenTile; //a new destination has been chosen

                character.SetSpeedToDestination();
            }
        }

    }
}