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
            character.Speed = 4;
            character.Calm();
        }

        public void OnUpdate()
        {
            //Note that at this point the destination is the current and the current is the previous
            character.Speed = 4;

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

                if (tileToLurkAround.PositionY < character.Destination.PositionY && character.Destination.TileUp != null && character.Destination.TileUp != character.CurrentTile)
                {
                    possibleDirections.Add(character.Destination.TileUp);
                }
                else if (tileToLurkAround.PositionX < character.Destination.PositionX && character.Destination.TileLeft != null && character.Destination.TileLeft != character.CurrentTile)
                {
                    possibleDirections.Add(character.Destination.TileLeft);
                }
                else if (character.Destination.TileDown != null && character.Destination.TileDown != character.CurrentTile)
                {
                    possibleDirections.Add(character.Destination.TileDown);
                }
                else if (character.Destination.TileRight != null && character.Destination.TileRight != character.CurrentTile)
                {
                    possibleDirections.Add(character.Destination.TileRight);
                }

                if (possibleDirections.Count == 0)
                {
                    character.CurrentState = new Patrol(character);
                    character.CurrentState.OnUpdate();
                }
                else
                {
                    int counter = 0;

                    while (chosenTile == null)
                    {
                        if (possibleDirections.Count == counter)
                        {
                            chosenTile = character.CurrentTile;
                        }
                        else
                        {
                            chosenTile = possibleDirections[counter];
                        }

                        counter++;
                    }

                    character.CurrentTile = character.Destination; //the current tile is no longer where he was
                    character.Destination = chosenTile; //a new destination has been chosen

                    character.SetSpeedToDestination();
                }
               
            }
        }

    }
}