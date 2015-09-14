using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DespicableGame.Observer;
using DespicableGame.States;
using DespicableGame.Factory;
namespace DespicableGame
{
    public class PlayerCharacter : Character
    {
        private const int STARTING_LIVES = 3;
        private int goalCollected;
        private int lives;
        private Collectible powerUpInStore;
        private bool unleashed;

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
            unleashed = false;
            powerUpInStore = null;
            ResetLives();
            baseSpeed = 4;
            Speed = baseSpeed;
            goalCollected = 0;
        }

        public void ResetLives()
        {
            
            lives = STARTING_LIVES;
        }

        //64 must be dividable by speed
        public override void Act()
        {
            if (Destination != null && Stunned == false)
            {
                position.X += SpeedX;
                position.Y += SpeedY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    SpeedX = 0;
                    SpeedY = 0;
                    currentTile = Destination;
                }
            }
        }

        public void CheckMovement(Tile tileDestination, int vitesseX, int vitesseY)
        {
            if (tileDestination != null)
            {
                //Check if the tile is a teleporter
                Tile testTeleportation = TestTeleporter(tileDestination);

                //If not, we move
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
                    Destination = CurrentTile;
                    SpeedX = 0;
                    SpeedY = 0;
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
                    if (this.currentTile == character.CurrentTile)
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
            CheckMovement(this.CurrentTile.TileDown, 0, Speed);
        }

        public void Up()
        {
            CheckMovement(this.CurrentTile.TileUp, 0, -Speed);
        }

        public void Right()
        {
            CheckMovement(this.CurrentTile.TileRight, Speed, 0);
        }

        public void Left()
        {
            CheckMovement(this.CurrentTile.TileLeft, -Speed, 0);
        }

        public void ActivatePowerup()
        {
            if (powerUpInStore != null)
            {
                switch (((Powerup)powerUpInStore).Type)
                {
                    case Powerup.PowerupType.SPEEDBOOST:
                        Speed = 8;
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

        public void UnleashMinions()
        {
            if (unleashed == false)
            {
                NotifyAllObservers(NotifyReason.MINION_SPAWN);
                unleashed = true;
            }
        }

        public void ResetMinionsAndMoney()
        {
            unleashed = false;
            goalCollected = 0;
        }

    }
}