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
        public enum CollectibleType { POWERUP, GOAL, TRAP, SHIP, BANANA }

        public static Collectible CreateCollectible(CollectibleType collectibleType, Vector2 position, Tile currentTile)
        {
            Collectible newCollectible = null;

            switch (collectibleType)
            {
                case CollectibleType.POWERUP:
                    int randomType = RandomManager.GetRandomInt(0, 2-1);
                    switch (randomType)
                    {
                        case 0:
                            newCollectible = new Powerup(DespicableGame.GetTexture(DespicableGame.GameTextures.SPEEDBOOST), position, currentTile, Powerup.PowerupType.SPEEDBOOST);
                            break;
                        case 1:
                            newCollectible = new Powerup(DespicableGame.GetTexture(DespicableGame.GameTextures.PLAYERTRAP_COLLECTIBLE), position, currentTile, Powerup.PowerupType.PLAYERTRAP);
                            break;
                    }
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

                case CollectibleType.BANANA:
                    newCollectible = new Ship(DespicableGame.GetTexture(DespicableGame.GameTextures.LEVEL_EXIT), position, currentTile);
                    break;
            }

            return newCollectible;
        }

    }
}