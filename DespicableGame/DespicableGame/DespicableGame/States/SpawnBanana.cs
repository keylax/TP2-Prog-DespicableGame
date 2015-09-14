using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Factory;
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
            character.CurrentState = new Wander(character);
        }

    }
}