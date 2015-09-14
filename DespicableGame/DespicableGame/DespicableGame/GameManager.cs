using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DespicableGame.Observer;
using DespicableGame.Factory;
using Microsoft.Xna.Framework;
using DespicableGame.States;
namespace DespicableGame
{
    class GameManager : Observer.Observer
    {
        private int MINIMUM_GOAL_TO_COLLECT = 3;
        private int EXTRA_GOAL_TO_COLLECT_PER_LEVEL = 2;
        //Gru's starting position
        private const int DEPART_X = 6;
        private const int DEPART_Y = 3;

        private List<Vector2> policeHouseTiles;

        private List<Character> characters; //Minions and police officers
        private List<Character> charactersToDelete; //Minions and police officers that should be deleted
        private List<Character> charactersToCreate; //Minions and police officers that will be created at end of frame

        private List<Countdown> countDowns; //Countdowns that are active at a given time
        private List<Countdown> countDownsToDelete; //Countdowns that have reached 0 should be deleted
        private List<Countdown> newCountDowns; //Contains new countdowns before they are added to the main list

        private List<Collectible> collectibles; //Goals and powerups
        private List<Collectible> collectiblesToDelete; //Goals and powerups that should be deleted
        private List<Collectible> collectiblesToCreate; //Goals and powerups that will be created at end of frame
        private Labyrinth labyrinth;
        Vector2 warpEntreePos;
        Vector2[] warpExitsPos = new Vector2[4];
        private bool shouldStartGame;
        private bool shouldStartLevel;
        private PlayerCharacter gru;

        private int level;

        private static GameManager manager;

        public static GameManager GetInstance()
        {
            if (manager == null)
            {
                manager = new GameManager();
            }
            return manager;
        }

        private GameManager()
        {
            labyrinth = new Labyrinth();
            policeHouseTiles = new List<Vector2>();

            policeHouseTiles.Add(new Vector2(6, 0));
            policeHouseTiles.Add(new Vector2(7, 0));
            policeHouseTiles.Add(new Vector2(0, 4));
            policeHouseTiles.Add(new Vector2(0, 5));
            policeHouseTiles.Add(new Vector2(6, 9));
            policeHouseTiles.Add(new Vector2(7, 9));
            policeHouseTiles.Add(new Vector2(13, 4));
            policeHouseTiles.Add(new Vector2(13, 5));

            countDowns = new List<Countdown>();
            newCountDowns = new List<Countdown>();

            characters = new List<Character>();
            charactersToDelete = new List<Character>();
            charactersToCreate = new List<Character>();
            countDownsToDelete = new List<Countdown>();

            Countdown firstTrapCountDown = new Countdown(0, 0, 30 - level, Subject.NotifyReason.SPAWN_NEW_TRAP);
            firstTrapCountDown.AddObserver(this);
            countDowns.Add(firstTrapCountDown);
            Countdown firstPowerupCountDown = new Countdown(0, 0, 25 - level, Subject.NotifyReason.SPAWN_NEW_POWERUP);
            firstPowerupCountDown.AddObserver(this);
            countDowns.Add(firstPowerupCountDown);

            collectibles = new List<Collectible>();
            collectiblesToDelete = new List<Collectible>();
            collectiblesToCreate = new List<Collectible>();

            level = 1;

            gru = (PlayerCharacter)CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.GRU, new Vector2(labyrinth.GetTile(DEPART_X, DEPART_Y).GetPosition().X, labyrinth.GetTile(DEPART_X, DEPART_Y).GetPosition().Y), labyrinth.GetTile(DEPART_X, DEPART_Y));
            characters.Add(gru);
            gru.AddObserver(this);

            StartGame();

            shouldStartGame = false;

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
            foreach (Countdown cd in countDowns)
            {
                cd.CountDown -= timeSinceLastFrame;
                if (cd.CountDown.TotalMilliseconds <= 0)
                {
                    countDownsToDelete.Add(cd);
                }
            }

            foreach (Character c in characters)
            {
                c.Act();
            }

            GetNewCountDowns();
            DetectAndProcessCollisions();
            RemoveDeadObjects();
            CreateNewObjects();

            if (shouldStartGame)
            {
                StartGame();
                shouldStartGame = false;
            }

            if (shouldStartLevel)
            {
                StartLevel();
                shouldStartLevel = false;
            }
        }

        public Vector2 WarpEntreePos
        {
            get { return warpEntreePos; }
        }

        public Vector2[] WarpExitsPos
        {
            get { return warpExitsPos; }
        }

        private void StartGame()
        {
            StartLevel();
            gru.ResetMinions();
            gru.SetPlayerToStartingValues();
        }

        private void StartLevel()
        {
            characters.Clear();
            characters.Add(gru);
            charactersToCreate.Clear();
            charactersToDelete.Clear();

            collectibles.Clear();
            collectiblesToCreate.Clear();
            collectiblesToDelete.Clear();
            countDownsToDelete.Clear();

            characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, new Vector2(labyrinth.GetTile(6, 0).GetPosition().X, labyrinth.GetTile(6, 0).GetPosition().Y), labyrinth.GetTile(6, 0)));
            if (level > 1) characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, new Vector2(labyrinth.GetTile(0, 4).GetPosition().X, labyrinth.GetTile(0, 4).GetPosition().Y), labyrinth.GetTile(0, 4)));
            if (level > 2) characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, new Vector2(labyrinth.GetTile(7, 9).GetPosition().X, labyrinth.GetTile(7, 9).GetPosition().Y), labyrinth.GetTile(7, 9)));
            if (level > 3) characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, new Vector2(labyrinth.GetTile(13, 5).GetPosition().X, labyrinth.GetTile(13, 5).GetPosition().Y), labyrinth.GetTile(13, 5)));

            gru.CurrentTile = labyrinth.GetTile(DEPART_X, DEPART_Y);
            gru.Destination = gru.CurrentTile;
            gru.SpeedX = 0;
            gru.SpeedY = 0;

            SpawnGoal();
        }

        public PlayerCharacter Gru
        {
            get { return gru; }
        }

        private void GetNewCountDowns()
        {
            foreach (Countdown cd in newCountDowns)
            {
                countDowns.Add(cd);
            }
            newCountDowns.Clear();
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

            foreach (Countdown cd in countDownsToDelete)
            {
                countDowns.Remove(cd);
            }
            countDownsToDelete.Clear();
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
            Vector2 randomTile;
            do
            {
                randomTile = RandomManager.GetRandomVector(Labyrinth.WIDTH, Labyrinth.HEIGHT);
            } while (IsTileInPoliceHouse(randomTile));
            Vector2 visualPosition = new Vector2(labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().X, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().Y);
            Collectible newShip = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.SHIP, visualPosition, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y));
            collectiblesToCreate.Add(newShip);
            newShip.AddObserver(this);
        }

        private void SpawnPowerup()
        {
            Vector2 randomTile;
            do
            {
                randomTile = RandomManager.GetRandomVector(Labyrinth.WIDTH, Labyrinth.HEIGHT);
            } while (IsTileInPoliceHouse(randomTile));
            Vector2 visualPosition = new Vector2(labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().X, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().Y);
            Collectible newPowerup = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.POWERUP, visualPosition, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y));
            collectiblesToCreate.Add(newPowerup);
            newPowerup.AddObserver(this);
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
            Vector2 randomTile;
            do
            {
                randomTile = RandomManager.GetRandomVector(Labyrinth.WIDTH, Labyrinth.HEIGHT);
            } while (IsTileInPoliceHouse(randomTile));
            Vector2 visualPosition = new Vector2(labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().X, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().Y);
            Collectible newGoal = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.GOAL, visualPosition, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y));
            collectiblesToCreate.Add(newGoal);
            newGoal.AddObserver(this);
        }

        private void SpawnPlayerTrap()
        {
            Vector2 visualPosition = new Vector2(labyrinth.GetTile(gru.CurrentTile.PositionX, gru.CurrentTile.PositionY).GetPosition().X, labyrinth.GetTile(gru.CurrentTile.PositionX, gru.CurrentTile.PositionY).GetPosition().Y);
            Collectible newPlayerTrap = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.TRAP, visualPosition, labyrinth.GetTile(gru.CurrentTile.PositionX, gru.CurrentTile.PositionY));
            collectiblesToCreate.Add(newPlayerTrap);
            newPlayerTrap.AddObserver(this);
        }

        private void SpawnTrap()
        {
            Vector2 randomTile;
            do
            {
                randomTile = RandomManager.GetRandomVector(Labyrinth.WIDTH, Labyrinth.HEIGHT);
            } while (IsTileInPoliceHouse(randomTile));
            Vector2 visualPosition = new Vector2(labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().X, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y).GetPosition().Y);
            Collectible newTrap = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.TRAP, visualPosition, labyrinth.GetTile((int)randomTile.X, (int)randomTile.Y));
            collectiblesToCreate.Add(newTrap);
            newTrap.AddObserver(this);
        }

        private bool IsTileInPoliceHouse(Vector2 tile)
        {
            foreach (Vector2 policeTile in policeHouseTiles)
            {
                if (tile == policeTile)
                {
                    return true;
                }
            }

            return false;
        }

        public void SpawnBanana(Tile spawnTile)
        {
            Vector2 visualPosition = new Vector2(labyrinth.GetTile(spawnTile.PositionX, spawnTile.PositionY).GetPosition().X, labyrinth.GetTile(spawnTile.PositionX, spawnTile.PositionY).GetPosition().Y);
            Collectible newBanana = CollectibleFactory.CreateCollectible(CollectibleFactory.CollectibleType.BANANA, visualPosition, spawnTile);
            collectiblesToCreate.Add(newBanana);
            newBanana.AddObserver(this);
        }

        public void SpawnBananaMinion()
        {
            Vector2 visualPosition = new Vector2(labyrinth.GetTile(gru.CurrentTile.PositionX, gru.CurrentTile.PositionY).GetPosition().X, labyrinth.GetTile(gru.CurrentTile.PositionX, gru.CurrentTile.PositionY).GetPosition().Y);
            Character newBananaMinion = CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.MINION_BANANA, visualPosition, labyrinth.GetTile(gru.CurrentTile.PositionX, gru.CurrentTile.PositionY));
            charactersToCreate.Add(newBananaMinion);
            newBananaMinion.AddObserver(this);
        }

        public void KillBananaMinion()
        {
            foreach (Character character in characters)
            {
                if (character is BananaMinion)
                {
                    charactersToDelete.Add(character);
                }
            }
        }

        public string GetGoalProgress()
        {
            return gru.GoalCollected.ToString() + "$ out of " + (level * EXTRA_GOAL_TO_COLLECT_PER_LEVEL + MINIMUM_GOAL_TO_COLLECT).ToString() + "$ needed";
        }

        public string GetLivesRemaining()
        {
            return "Lives remaining: " + gru.Lives.ToString();
        }

        public string GetCurrentLevel()
        {
            return "Level: " + level.ToString();
        }

        public void Notify(Subject subject, Subject.NotifyReason reason)
        {
            switch (reason)
            {
                case Subject.NotifyReason.MONEY_DESTROYED:
                    RespawnGoalAfterPickup();
                    break;

                case Subject.NotifyReason.MONEY_GAINED:
                    //nothing?
                    break;

                case Subject.NotifyReason.LIFE_LOST:
                    if (gru.Lives < 1)
                    {
                        level = 1;
                        shouldStartGame = true;
                    }
                    else
                    {
                        shouldStartLevel = true;
                    }
                    break;

                case Subject.NotifyReason.EXIT_REACHED:
                    level++;
                    gru.ResetMinions();
                    shouldStartGame = true;
                    break;

                case Subject.NotifyReason.EXIT_DESTROYED:
                    RespawnGoalAfterPickup();
                    break;

                case Subject.NotifyReason.TRAP_ACTIVATED:
                    Countdown trapCountDown = new Countdown(0, 0, 3, Subject.NotifyReason.TRAP_EXPIRED);
                    trapCountDown.AddObserver(((Trap)subject).AffectedCharacter);
                    countDowns.Add(trapCountDown);
                    break;

                case Subject.NotifyReason.SPEEDBOOST_ACTIVATED:
                    Countdown powerupCountDown = new Countdown(0, 0, 2, Subject.NotifyReason.SPEEDBOOST_EXPIRED);
                    powerupCountDown.AddObserver((PlayerCharacter)subject);
                    countDowns.Add(powerupCountDown);
                    break;

                case Subject.NotifyReason.PLAYERTRAP_ACTIVATED:
                    SpawnPlayerTrap();
                    break;

                case Subject.NotifyReason.SPAWN_NEW_TRAP:
                    SpawnTrap();
                    Countdown newTrapCountDown = new Countdown(0, 0, 30 - level, Subject.NotifyReason.SPAWN_NEW_TRAP);
                    newTrapCountDown.AddObserver(this);
                    newCountDowns.Add(newTrapCountDown);
                    break;

                case Subject.NotifyReason.SPAWN_NEW_POWERUP:
                    SpawnPowerup();
                    Countdown newPowerupCountDown = new Countdown(0, 0, 25 - level, Subject.NotifyReason.SPAWN_NEW_POWERUP);
                    newPowerupCountDown.AddObserver(this);
                    newCountDowns.Add(newPowerupCountDown);
                    break;

                case Subject.NotifyReason.BANANA:
                    SpawnBanana(((Character)subject).CurrentTile);
                    break;

                case Subject.NotifyReason.STUNNED:
                    Countdown bananaCountDown = new Countdown(0, 0, 1, Subject.NotifyReason.WOKE_UP);
                    bananaCountDown.AddObserver(((Banana)subject).AffectedCharacter);
                    countDowns.Add(bananaCountDown);
                    break;

                case Subject.NotifyReason.BANANA_MINION_SPAWN:
                    SpawnBananaMinion();
                    Countdown bananaMinionCountDown = new Countdown(0, 0, 7, Subject.NotifyReason.BANANA_MINION_KILL);
                    bananaMinionCountDown.AddObserver(this);
                    countDowns.Add(bananaMinionCountDown);
                    break;

                case Subject.NotifyReason.BANANA_MINION_KILL:
                    KillBananaMinion();
                    break;

            }
        }

    }
}