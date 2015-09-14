using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    public class RunAway : AIStates
    {
        private readonly NonPlayerCharacter character;

        public RunAway(NonPlayerCharacter character)
        {
            this.character = character;
        }

        public void OnUpdate()
        {
            if (RandomManager.GetRandomTrueFalse(200))
            {
                GameManager.GetInstance().Notify(character, Observer.Subject.NotifyReason.BANANA);
                character.CurrentState.OnUpdate();
            }

            if (!character.SeesGru())
            {
                character.CurrentState = new Wander(character);
                character.CurrentState.OnUpdate();
            }
            else
            {
                
            }

        }

    }
}