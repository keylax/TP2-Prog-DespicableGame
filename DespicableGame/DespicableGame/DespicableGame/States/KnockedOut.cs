using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    class KnockedOut : AIStates
    {
        private readonly NonPlayerCharacter character;
        private int previousSpeedX;
        private int previousSpeedY;

        public KnockedOut(NonPlayerCharacter character)
        {
            this.character = character;
            previousSpeedX = character.SpeedX;
            character.SpeedX = 0;
            previousSpeedY = character.SpeedY;
            character.SpeedY = 0;
        }

        public void OnUpdate()
        {
            character.SpeedX = previousSpeedX;
            character.SpeedY = previousSpeedY;

            character.CurrentState = new Patrol(character);
        }

    }
}