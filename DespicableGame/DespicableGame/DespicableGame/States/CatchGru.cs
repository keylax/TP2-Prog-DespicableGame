using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame.States
{
    public class CatchGru : AIStates
    {
        private readonly NonPlayerCharacter character;

        public CatchGru(NonPlayerCharacter character)
        {
            this.character = character;
        }

        public void OnUpdate()
        {
            //Note that at this point the destination is the current and the current is the previous (check this shit up)

            Tile chosenTile;

            GameManager test = GameManager.GetInstance();

            if (GameManager.GetInstance().Gru.Destination.PositionX == character.Destination.PositionX || GameManager.GetInstance().Gru.CurrentTile.PositionX == character.Destination.PositionX)
            {
                if (GameManager.GetInstance().Gru.Destination.PositionY < character.Destination.PositionY)
                {
                    chosenTile = character.Destination.TileUp;
                }
                else
                {
                    chosenTile = character.Destination.TileDown;
                }
            }
            else if (GameManager.GetInstance().Gru.Destination.PositionY == character.Destination.PositionY || GameManager.GetInstance().Gru.CurrentTile.PositionY == character.Destination.PositionY)
            {
                if (GameManager.GetInstance().Gru.Destination.PositionX < character.Destination.PositionX)
                {
                    chosenTile = character.Destination.TileLeft;
                }
                else
                {
                    chosenTile = character.Destination.TileRight;
                }
            }
            else
            {
                character.CurrentState = new Lurking(character, GameManager.GetInstance().Gru.CurrentTile);
                character.CurrentState.OnUpdate();
                return; //This is not very clean
            }

            character.CurrentTile = character.Destination; //the current tile is no longer where he was
            character.Destination = chosenTile; //a new destination has been chosen

            character.SetSpeedToDestination();

        }

    }
}