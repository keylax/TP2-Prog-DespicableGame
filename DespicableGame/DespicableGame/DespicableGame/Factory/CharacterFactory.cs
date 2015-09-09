using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame.Factory
{
    public static class CharacterFactory
    {
        public enum CharacterType { GRU, MINION_BANANA, MINON_TRAP, MINION_FREAK, POLICE_OFFICER }

        public static Character CreateCharacter(CharacterType newCharacterType, Texture2D drawing, Vector2 position, Tile CurrentTile)
        {
            Character newCharacter = null;

            switch (newCharacterType)
            {
                case CharacterType.GRU:
                    newCharacter = CreateGru(drawing, position, CurrentTile);
                    break;

                //case CharacterType.MINION_BANANA:

                //    break;

                //case CharacterType.MINION_FREAK:

                //    break;

                //case CharacterType.MINON_TRAP:

                //    break;

                case CharacterType.POLICE_OFFICER:
                    newCharacter = CreatePoliceOfficer(drawing, position, CurrentTile);
                    break;
            }

            return newCharacter;
        }

        private static Character CreateGru(Texture2D drawing, Vector2 position, Tile CurrentTile)
        {
            return new PlayerCharacter(drawing, position, CurrentTile);
        }

        private static NonPlayerCharacter CreatePoliceOfficer(Texture2D drawing, Vector2 position, Tile CurrentTile)
        {
            return new NonPlayerCharacter(drawing, position, CurrentTile);
        }


    }
}