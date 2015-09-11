using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DespicableGame.Observer;
using DespicableGame.Factory;
using Microsoft.Xna.Framework;

namespace DespicableGame
{
    class GameManager : Observer.Observer
    {
        private int MINIMUM_GOAL_TO_COLLECT = 3;
        private int EXTRA_GOAL_TO_COLLECT_PER_LEVEL = 2;
        //Gru's starting position
        private const int DEPART_X = 6;
        private const int DEPART_Y = 7;

        private List<Character> characters; //Minions and police officers
        private List<Character> charactersToDelete; //Minions and police officers that should be deleted
        private List<Character> charactersToCreate; //Minions and police officers that will be created at end of frame

        private List<Collectible> collectibles; //Goals and powerups
        private List<Collectible> collectiblesToDelete; //Goals and powerups that should be deleted
        private List<Collectible> collectiblesToCreate; //Goals and powerups that will be created at end of frame

        private Labyrinth labyrinth;
        Vector2 warpEntreePos;
        Vector2[] warpExitsPos = new Vector2[4];

        private PlayerCharacter gru;

        int level;

        public GameManager()
        {
            labyrinth = new Labyrinth();

            characters = new List<Character>();
            charactersToDelete = new List<Character>();
            charactersToCreate = new List<Character>();

            collectibles = new List<Collectible>();
            collectiblesToDelete = new List<Collectible>();
            collectiblesToCreate = new List<Collectible>();

            level = 1;

            StartLevel();

            //Teleporter entrance
            warpEntreePos = new Vector2(labyrinth.GetTile(7, 4).GetPosition().X - Tile.LIGN_SIZE, labyrinth.GetTile(7, 4).GetPosition().Y + Tile.LIGN_SIZE);

            //Teleporter exits
            warpExitsPos[0] = new Vector2(labyrinth.GetTile(0, 0).GetPosition().X, labyrinth.GetTile(0, 0).GetPosition().Y);
            warpExitsPos[1] = new Vector2(labyrinth.GetTile(Labyrinth.WIDTH - 1, 0).GetPosition().X, labyrinth.GetTile(Labyrinth.WIDTH - 1, 0).GetPosition().Y);
            warpExitsPos[2] = new Vector2(labyrinth.GetTile(0, Labyrinth.HEIGHT - 1).GetPosition().X, labyrinth.GetTile(0, Labyrinth.HEIGHT - 1).GetPosition().Y);
            warpExitsPos[3] = new Vector2(labyrinth.GetTile(Labyrinth.WIDTH - 1, Labyrinth.HEIGHT - 1).GetPosition().X, labyrinth.GetTile(Labyrinth.WIDTH - 1, Labyrinth.HEIGHT - 1).GetPosition().Y);
        }

        public Labyrinth Labyrinth
        {
            get { return labyrinth; }
        }

        public List<Character> Characters
        {
            get { return characters; }
        }

        public List<Collectible> Collectibles
        {
            get { return collectibles; }
        }

        public void ProcessFrame(TimeSpan timeSinceLastFrame)
        {
            //Timers!

            foreach (Character c in characters)
            {
                c.Move();
            }
            DetectAndProcessCollisions();
            RemoveDeadObjects();
            CreateNewObjects();
        }

        public Vector2 WarpEntreePos
        {
            get { return warpEntreePos; }
        }

        public Vector2[] WarpExitsPos
        {
            get { return warpExitsPos; }
        }

        private void StartLevel()
        {
            characters.Clear();
            charactersToCreate.Clear();
            charactersToDelete.Clear();

            collectibles.Clear();
            collectiblesToCreate.Clear();
            collectiblesToDelete.Clear();

            gru = (PlayerCharacter)CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.GRU, new Vector2(labyrinth.GetTile(DEPART_X, DEPART_Y).GetPosition().X, labyrinth.GetTile(DEPART_X, DEPART_Y).GetPosition().Y), labyrinth.GetTile(DEPART_X, DEPART_Y));
            characters.Add(gru);

            characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, new Vector2(labyrinth.GetTile(7, 9).GetPosition().X, labyrinth.GetTile(7, 9).GetPosition().Y), labyrinth.GetTile(7, 9)));

            SpawnGoal();
        }

        private void RemoveDeadObjects()
        {
            foreach (Character character in charactersToDelete)
            {
                characters.Remove(character);
            }
            charactersToDelete.Clear();

            foreach (Collectible collectible in collectiblesToDelete)
            {
                collectibles.Remove(collectible);
            }
            collectiblesToDelete.Clear();
        }

        private void CreateNewObjects()
        {
            foreach (Character character in charactersToCreate)
            {
                characters.Add(character);
            }
            charactersToCreate.Clear();

            foreach (Collectible collectible in collectiblesToCreate)
            {
                collectibles.Add(collectible);
            }
            collectiblesToCreate.Clear();
        }

        private void DetectAndProcessCollisions()
        {
            gru.FindCollisions(characters);

            foreach (Collectible collectible in collectibles)
            {
                collectible.FindCollisions(characters);
                if (!collectible.Active)
                {
                    collectiblesToDelete.Add(collectible);
                }
            }
        }

        private void SpawnShip()
        {
            Vector2 randomTile = RandomManager.GetRandomVector(Labyrinth.WIDTH, Labyrinth.HEIGHT);
            Vector2 visualPosition = new Vector2(labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().X, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().Y);
            Collectible newShip = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.SHIP, visualPosition, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y));
            collectiblesToCreate.Add(newShip);
            newShip.AddObserver(this);
        }

        private void RespawnGoalAfterPickup()
        {
            if (gru.GoalCollected >= level * EXTRA_GOAL_TO_COLLECT_PER_LEVEL + MINIMUM_GOAL_TO_COLLECT)
            {
                SpawnShip();
            }
            else
            {
                SpawnGoal();
            }
        }

        private void SpawnGoal()
        {
            Vector2 randomTile = RandomManager.GetRandomVector(Labyrinth.WIDTH, Labyrinth.HEIGHT);
            Vector2 visualPosition = new Vector2(labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().X, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().Y);
            Collectible newGoal = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.GOAL, visualPosition, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y));
            collectiblesToCreate.Add(newGoal);
            newGoal.AddObserver(this);
        }





        public void Notify(Subject subject, Subject.NotifyReason reason)
        {
            switch (reason)
            {
                case Subject.NotifyReason.MONEY_DESTROYED:
                    RespawnGoalAfterPickup();
                    break;

                case Subject.NotifyReason.MONEY_GAINED:
                    //TODO: display new money amount
                    break;

                case Subject.NotifyReason.LIFE_LOST:
                    if (gru.Lives < 1)
                    {
                        level = 1;
                        StartLevel();
                        //TODO: display you lsot message
                    }
                    //TODO: display remaining lives
                    break;

                case Subject.NotifyReason.EXIT_REACHED:
                    level++;
                    StartLevel();
                    break;
            }
        }

    }
}