using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    public class WorkHard : AIStates
    {
        private readonly NonPlayerCharacter character;

        public WorkHard(NonPlayerCharacter character)
        {
            this.character = character;
            character.Speed = 8;
        }

        public void OnUpdate()
        {
            if (RandomManager.GetRandomTrueFalse(300))
            {
                ((Minion)character).JustMinionThings();
            }

            if (!character.SeesGru())
            {
                character.CurrentState = new Wander(character);
                character.CurrentState.OnUpdate();
            }
            else
            {
                //Note that at this point the destination is the current and the current is the previous

                Tile chosenTile = null;

                List<Tile> possibleTiles = new List<Tile>();

                if (character.Destination.TileUp != null)
                {
                    possibleTiles.Add(character.Destination.TileUp);
                }

                if (character.Destination.TileRight != null && !(character.Destination.TileRight is Teleporter))
                {
                    possibleTiles.Add(character.Destination.TileRight);
                }

                if (character.Destination.TileDown != null)
                {
                    possibleTiles.Add(character.Destination.TileDown);
                }

                if (character.Destination.TileLeft != null && !(character.Destination.TileLeft is Teleporter))
                {
                    possibleTiles.Add(character.Destination.TileLeft);
                }

                chosenTile = possibleTiles[0];

                float distanceFromPossibleTile = -1;
                float currentTileDistance;

                foreach (Tile t in possibleTiles)
                {
                    currentTileDistance = GameManager.GetInstance().Gru.DistanceToTile(t);

                    if (distanceFromPossibleTile < currentTileDistance)
                    {
                        chosenTile = t;
                        distanceFromPossibleTile = currentTileDistance;
                    }
                }

                character.CurrentTile = character.Destination; //the current tile is no longer where he was
                character.Destination = chosenTile; //a new destination has been chosen

                character.SetSpeedToDestination();
            }

        }

    }
}