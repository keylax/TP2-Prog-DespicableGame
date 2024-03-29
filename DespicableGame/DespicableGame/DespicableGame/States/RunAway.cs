﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    class RunAway : AIStates
    {
        private readonly PoliceOfficer character;

        public RunAway(PoliceOfficer character)
        {
            this.character = character;
            character.Speed = 4;
            character.Scare();
        }

        public void OnUpdate()
        {
            if (!character.SeesGru())
            {
                character.CurrentState = new Patrol(character);
                character.CurrentState.OnUpdate();
            }
            else if (!GameManager.GetInstance().Gru.LooksScary)
            {
                character.CurrentState = new CatchGru(character);
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