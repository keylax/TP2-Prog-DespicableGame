using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.States
{
    class SpawnBanana : AIStates
    {
        private readonly NonPlayerCharacter character;

        public SpawnBanana(NonPlayerCharacter character)
        {
            this.character = character;
        }

        public void OnUpdate()
        {
            GameManager.GetInstance().Notify(character, Observer.Subject.NotifyReason.BANANA);
        }

    }
}