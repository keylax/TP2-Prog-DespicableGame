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

        public static Collectible CreateCollectible(CollectibleType collectibleType, Vector2 position, Tile currentTile)
        {
            Collectible newCollectible = null;

            switch (collectibleType)
            {
                case CollectibleType.POWERUP:
                    newCollectible = new Speedboost(DespicableGame.GetTexture(DespicableGame.GameTextures.SPEEDBOOST), position, currentTile);
                    break;

                case CollectibleType.GOAL:
                    newCollectible = new Goal(DespicableGame.GetTexture(DespicableGame.GameTextures.GOAL), position, currentTile);
                    break;

                case CollectibleType.TRAP:
                    newCollectible =  new Trap(DespicableGame.GetTexture(DespicableGame.GameTextures.TRAP), position, currentTile);
                    break;

                case CollectibleType.SHIP:
                    newCollectible = new Ship(DespicableGame.GetTexture(DespicableGame.GameTextures.LEVEL_EXIT), position, currentTile);
                    break;
            }

            return newCollectible;
        }

    }
}