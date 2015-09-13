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
            //TODO: if gru is "visible", then gogo power CatchGru
            //Note that at this point the destination is the current and the current is the previous (check this shit up)

            Tile playerTile = new Tile(0, 0, 0);
            Tile chosenTile;

            if (playerTile.PositionX == character.Destination.PositionX)
            {
                if (playerTile.PositionY < character.Destination.PositionY)
                {
                    chosenTile = character.Destination.TileUp;
                }
                else
                {
                    chosenTile = character.Destination.TileDown;
                }
            }
            else if (playerTile.PositionY == character.Destination.PositionY)
            {
                if (playerTile.PositionX < character.Destination.PositionX)
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
                character.CurrentState = new Patrol(character); //Lurking! With last seen player position
                chosenTile = character.Destination;
            }

            character.CurrentTile = character.Destination; //the current tile is no longer where he was
            character.Destination = chosenTile; //a new destination has been chosen

            if (character.Destination == character.CurrentTile.TileUp)
            {
                character.SpeedX = 0;
                character.SpeedY = -character.SPEED;
                //character.Destination = character.CurrentTile.TileUp;
            }
            else if (character.Destination == character.CurrentTile.TileDown)
            {
                character.SpeedX = 0;
                character.SpeedY = character.SPEED;
                //character.Destination = character.CurrentTile.TileDown;
            }
            else if (character.Destination == character.CurrentTile.TileLeft)
            {
                character.SpeedX = -character.SPEED;
                character.SpeedY = 0;
                //character.Destination = character.CurrentTile.TileLeft;
            }
            else if (character.Destination == character.CurrentTile.TileRight)
            {
                character.SpeedX = character.SPEED;
                character.SpeedY = 0;
                //character.Destination = character.CurrentTile.TileRight;
            }


        }

    }
}