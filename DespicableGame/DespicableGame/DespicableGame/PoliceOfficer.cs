using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.States;

namespace DespicableGame
{
    public class PoliceOfficer : NonPlayerCharacter
    {
        public PoliceOfficer(Texture2D dessin, Vector2 position, Tile currentTile, bool isFriendly)
            : base(dessin, position, currentTile, isFriendly)
        {
            CurrentState = new Patrol(this);
        }

        public void Alert()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.ALERTED_POLICE_OFFICER);
        }

        public void Calm()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.POLICE_OFFICER);
        }

        public void Lurk()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.LURKING_POLICE_OFFICER);
        }

        public void Scare()
        {
            drawing = DespicableGame.GetTexture(DespicableGame.GameTextures.SCARED_POLICE_OFFICER);
        }

    }
}