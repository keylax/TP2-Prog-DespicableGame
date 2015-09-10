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
        public enum CollectibleType { POWERUP, GOAL, TRAP, SHIP }

        public static Collectible CreateCollectible(Texture2D drawing, Vector2 position, Tile currentTile, CollectibleType collectibleType)
        {
            Collectible newCollectible = null;

            switch (collectibleType)
            {
                case CollectibleType.POWERUP:
                    //    newCollectible = new Powerups(drawing, position, currentTile);
                    break;

                case CollectibleType.GOAL:
                    newCollectible = new Goal(drawing, position, currentTile);
                    break;

                case CollectibleType.TRAP:
                    //    newCollectible =  new Trap(drawing, position, currentTile);
                    break;

                case CollectibleType.SHIP:

                    break;

            }

            return newCollectible;
        }


    }
}