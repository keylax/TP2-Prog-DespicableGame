using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DespicableGame.Factory
{
    public static class CollectibleFactory
    {
        public enum CollectibleType { POWERUP, GOAL, TRAP }

        public static Collectible CreateCollectible(Texture2D drawing, Vector2 position, Tile currentTile, CollectibleType collectibleType)
        {

            if (CollectibleType.POWERUP == collectibleType)
            {
                return new Powerups(drawing, position, currentTile);
            }
            else if (CollectibleType.GOAL == collectibleType)
            {
                return new Goal(drawing, position, currentTile);
            }
            else if (CollectibleType.TRAP == collectibleType)
            {
                return new Trap(drawing, position, currentTile);
            }
            return null;
        }
    }
}
