using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.States;
namespace DespicableGame.Factory
{
    public static class CharacterFactory
    {
        public enum CharacterType { GRU, MINION_BANANA, MINON_TRAP, MINION_FREAK, POLICE_OFFICER, POWERUP_MINION }

        public static Character CreateCharacter(CharacterType newCharacterType, Vector2 position, Tile CurrentTile)
        {
            Character newCharacter = null;

            switch (newCharacterType)
            {
                case CharacterType.GRU:
                    newCharacter = CreateGru(position, CurrentTile);
                    break;

                case CharacterType.MINION_BANANA:
                    newCharacter = CreateBananaMinion(position, CurrentTile);
                    break;

                case CharacterType.POWERUP_MINION:
                    newCharacter = CreatePowerupMinion(position, CurrentTile);
                    break;

                case CharacterType.POLICE_OFFICER:
                    newCharacter = CreatePoliceOfficer(position, CurrentTile);
                    break;
            }

            return newCharacter;
        }

        private static Character CreateGru(Vector2 position, Tile CurrentTile)
        {
            return new PlayerCharacter(DespicableGame.GetTexture(DespicableGame.GameTextures.GRU), position, CurrentTile);
        }

        private static NonPlayerCharacter CreatePoliceOfficer(Vector2 position, Tile CurrentTile)
        {
            return new NonPlayerCharacter(DespicableGame.GetTexture(DespicableGame.GameTextures.POLICE_OFFICER), position, CurrentTile, false);
        }

        private static BananaMinion CreateBananaMinion(Vector2 position, Tile CurrentTile)
        {
            return new BananaMinion(DespicableGame.GetTexture(DespicableGame.GameTextures.BANANA_MINION), position, CurrentTile, true);
        }

        private static PowerupMinion CreatePowerupMinion(Vector2 position, Tile CurrentTile)
        {
            return new PowerupMinion(DespicableGame.GetTexture(DespicableGame.GameTextures.POWERUP_MINION), position, CurrentTile, true);
        }
    }
}