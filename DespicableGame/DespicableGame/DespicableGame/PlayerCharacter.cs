using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;

namespace DespicableGame
{
    public class PlayerCharacter : Character
    {
        private const int STARTING_LIVES = 3;
        private int goalCollected;
        private int lives;
        private Collectible powerUpInStore;

        public Collectible PowerUpInStore
        {
            get
            {
                return powerUpInStore;
            }
            set
            {
                powerUpInStore = value;
            }
        }

        public int GoalCollected
        {
            get { return goalCollected; }
            set
            {
                NotifyAllObservers(Subject.NotifyReason.MONEY_GAINED);
                goalCollected = value;
            }
        }

        public int Lives
        {
            get { return lives; }
        }

        public PlayerCharacter(Texture2D drawing, Vector2 position, Tile currentTile)
            : base(drawing, position, currentTile)
        {
            powerUpInStore = null;
            SetPlayerToStartingValues();
        }

        public void SetPlayerToStartingValues()
        {
            goalCollected = 0;
            lives = STARTING_LIVES;
            Destination = null;
        }

        //Algo assez ordinaire. Pour que ça fonctionne, la vitesse doit être un diviseur entier de 64, pourrait être à revoir.
        public override void Act()
        {
            if (Destination != null)
            {
                position.X += SpeedX;
                position.Y += SpeedY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    currentTile = Destination;
                    Destination = null;
                }
            }
        }

        public void CheckMovement(Tile tileDestination, int vitesseX, int vitesseY)
        {
            //If direction is not null
            if (tileDestination != null)
            {
                //Check if the tile is a teleporter
                Tile testTeleportation = TestTeleporter(tileDestination);

                //Is not, we move
                if (testTeleportation == null)
                {
                    Destination = tileDestination;
                    SpeedX = vitesseX;
                    SpeedY = vitesseY;
                }
                //If so, we teleport ourselves
                else
                {
                    CurrentTile = testTeleportation;
                }
            }
        }

        private Tile TestTeleporter(Tile theTile)
        {
            if (theTile is Teleporter)
            {
                return ((Teleporter)theTile).Teleport();
            }
            return null;
        }

        public void FindCollisions(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                if (!character.IsFriendly)
                {
                    if (this.Destination == character.Destination || this.currentTile == character.Destination)
                    {
                        LoseLife();
                    }
                }
            }
        }

        public void LoseLife()
        {
            lives--;
            NotifyAllObservers(Subject.NotifyReason.LIFE_LOST);
        }

        public void Down()
        {
            CheckMovement(this.CurrentTile.TileDown, 0, SPEED);
        }

        public void Up()
        {
            CheckMovement(this.CurrentTile.TileUp, 0, -SPEED);
        }

        public void Right()
        {
            CheckMovement(this.CurrentTile.TileRight, SPEED, 0);
        }

        public void Left()
        {
            CheckMovement(this.CurrentTile.TileLeft, -SPEED, 0);
        }

        public void ActivatePowerup()
        {
            if (powerUpInStore != null)
            {
                switch (((Powerup)powerUpInStore).Type)
                {
                    case Powerup.PowerupType.SPEEDBOOST:
                        SPEED = 8;
                        NotifyAllObservers(NotifyReason.SPEEDBOOST_ACTIVATED);
                        powerUpInStore = null;
                        break;

                    case Powerup.PowerupType.PLAYERTRAP:
                        NotifyAllObservers(NotifyReason.PLAYERTRAP_ACTIVATED);
                        powerUpInStore = null;
                        break;
                }
            }
        }
    }
}