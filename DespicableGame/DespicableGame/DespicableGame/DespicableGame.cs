using System;
using System.Collections.Generic;
using System.Linq;
using DespicableGame.Factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DespicableGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DespicableGame : Microsoft.Xna.Framework.Game
    {
        readonly TimeSpan PAUSE_BUTTON_DELAY = new TimeSpan(0, 0, 0, 0, 250);
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;
        enum GameStates { MAIN_MENU, PAUSED, PLAYING }
        enum GameTextures { HORIZONTAL_WALL, VERTICAL_WALL, WARP_ENTRANCE, WARP_EXIT, GOAL, GRU, POLICE_OFFICER, NUMBER_OF_TEXTURES }

        Texture2D[] gameTextures;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameStates currentState;
        TimeSpan lastPauseButtonPress = new TimeSpan(0, 0, 0);

        PlayerCharacter Gru;
        Collectible goal;

        int level;

        List<Character> characters; //Minions and police officers
        List<Character> charactersToDelete; //Minions and police officers that should be deleted

        List<Collectible> collectibles; //Goals and powerups
        List<Collectible> collectiblesToDelete; //Goals and powerups that should be deleted

        //En attendant le RandomManager
        Random r = new Random();
        int goalTileX;
        int goalTileY;
        Vector2 warpEntreePos;

        Vector2[] warpExitsPos = new Vector2[4];

        //64 must be dividable by SPEED
        public const int VITESSE = 4;

        //Gru's statring position
        public const int DEPART_X = 6;
        public const int DEPART_Y = 7;

        private Labyrinth labyrinth;

        public DespicableGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            labyrinth = new Labyrinth();
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameTextures = new Texture2D[(int)GameTextures.NUMBER_OF_TEXTURES];

            characters = new List<Character>();
            charactersToDelete = new List<Character>();

            collectibles = new List<Collectible>();
            collectiblesToDelete = new List<Collectible>();

            currentState = GameStates.PLAYING;
            level = 1;

            InitGraphicsMode(SCREENWIDTH, SCREENHEIGHT, true);
            base.Initialize();
        }

        private bool InitGraphicsMode(int width, int height, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    //if ((dm.Width == width) && (dm.Height == height))
                    //{
                    // The mode is supported, so set the buffer formats, apply changes and return
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                    //}
                }
            }
            return false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameTextures[(int)GameTextures.HORIZONTAL_WALL] = Content.Load<Texture2D>("Sprites\\Hwall");
            gameTextures[(int)GameTextures.VERTICAL_WALL] = Content.Load<Texture2D>("Sprites\\Vwall");
            gameTextures[(int)GameTextures.POLICE_OFFICER] = Content.Load<Texture2D>("Sprites\\Police");
            gameTextures[(int)GameTextures.GRU] = Content.Load<Texture2D>("Sprites\\Gru");
            gameTextures[(int)GameTextures.GOAL] = Content.Load<Texture2D>("Sprites\\Dollar");
            gameTextures[(int)GameTextures.WARP_ENTRANCE] = Content.Load<Texture2D>("Sprites\\Warp1");
            gameTextures[(int)GameTextures.WARP_EXIT] = Content.Load<Texture2D>("Sprites\\Warp2");

            goalTileX = r.Next(14);
            goalTileY = r.Next(10);
            goal = CollectibleFactory.CreateCollectible(GetTexture(GameTextures.GOAL), new Vector2(labyrinth.GetTile(goalTileX, goalTileY).GetPosition().X, labyrinth.GetTile(goalTileX, goalTileY).GetPosition().Y), labyrinth.GetTile(goalTileX, goalTileY), CollectibleFactory.CollectibleType.GOAL);

            //Add Gru to character list
            Gru = (PlayerCharacter)CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.GRU, GetTexture(GameTextures.GRU), new Vector2(labyrinth.GetTile(DEPART_X, DEPART_Y).GetPosition().X, labyrinth.GetTile(DEPART_X, DEPART_Y).GetPosition().Y), labyrinth.GetTile(DEPART_X, DEPART_Y));
            characters.Add(Gru);

            //Add a test police officer
            characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, GetTexture(GameTextures.POLICE_OFFICER), new Vector2(labyrinth.GetTile(7, 9).GetPosition().X, labyrinth.GetTile(7, 9).GetPosition().Y), labyrinth.GetTile(7, 9)));

            //Add some money
            collectibles.Add(goal);

            //Teleporter entrance
            warpEntreePos = new Vector2(labyrinth.GetTile(7, 4).GetPosition().X - Tile.LIGN_SIZE, labyrinth.GetTile(7, 4).GetPosition().Y + Tile.LIGN_SIZE);

            //Teleporter exits
            warpExitsPos[0] = new Vector2(labyrinth.GetTile(0, 0).GetPosition().X, labyrinth.GetTile(0, 0).GetPosition().Y);
            warpExitsPos[1] = new Vector2(labyrinth.GetTile(Labyrinth.WIDTH - 1, 0).GetPosition().X, labyrinth.GetTile(Labyrinth.WIDTH - 1, 0).GetPosition().Y);
            warpExitsPos[2] = new Vector2(labyrinth.GetTile(0, Labyrinth.HEIGHT - 1).GetPosition().X, labyrinth.GetTile(0, Labyrinth.HEIGHT - 1).GetPosition().Y);
            warpExitsPos[3] = new Vector2(labyrinth.GetTile(Labyrinth.WIDTH - 1, Labyrinth.HEIGHT - 1).GetPosition().X, labyrinth.GetTile(Labyrinth.WIDTH - 1, Labyrinth.HEIGHT - 1).GetPosition().Y);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ProcessPlayerInputs(gameTime.TotalGameTime);

            switch (currentState)
            {
                case GameStates.MAIN_MENU:
                    //Nothing for now
                    break;

                case GameStates.PAUSED:
                    //Nothing for now
                    break;

                case GameStates.PLAYING:
                    UpdateGameLogic();
                    break;
            }



            base.Update(gameTime);
        }

        private void UpdateGameLogic()
        {
            foreach (Character c in characters)
            {
                c.Move();
            }

            foreach (Collectible collectible in collectibles)
            {
                collectible.FindCollisions(characters);
                if (!collectible.Active)
                {
                    if (collectible is Goal)
                    {
                        RespawnGoalAfterPickup();
                    }
                    else
                    {
                        collectiblesToDelete.Add(collectible);
                    }
                }
            }

            foreach (Collectible collectible in collectiblesToDelete)
            {
                collectibles.Remove(collectible);

            }
        }

        private void PauseButtonPressAction(TimeSpan totalGameTime)
        {
            if (totalGameTime - lastPauseButtonPress > PAUSE_BUTTON_DELAY)
            {
                lastPauseButtonPress = totalGameTime;
                if (currentState != GameStates.PLAYING)
                {
                    currentState = GameStates.PLAYING;
                }
                else
                {
                    currentState = GameStates.PAUSED;
                }
            }
        }

        private void ProcessPlayerInputs(TimeSpan totalGameTime)
        {
            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || padOneState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || padOneState.Buttons.Start == ButtonState.Pressed) PauseButtonPressAction(totalGameTime);

            if (Gru.Destination == null)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || padOneState.DPad.Up == ButtonState.Pressed)
                {
                    Gru.CheckMovement(Gru.CurrentTile.TileUp, 0, -VITESSE);
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Down) || padOneState.DPad.Down == ButtonState.Pressed)
                {
                    Gru.CheckMovement(Gru.CurrentTile.TileDown, 0, VITESSE);
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Left) || padOneState.DPad.Left == ButtonState.Pressed)
                {
                    Gru.CheckMovement(Gru.CurrentTile.TileLeft, -VITESSE, 0);
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Right) || padOneState.DPad.Right == ButtonState.Pressed)
                {
                    Gru.CheckMovement(Gru.CurrentTile.TileRight, VITESSE, 0);
                }
            }
        }

        private Texture2D GetTexture(GameTextures desiredTexture)
        {
            return gameTextures[(int)desiredTexture];
        }

        private void RespawnGoalAfterPickup()
        {
            if (Gru.GoalCollected == (level * 2 + 3))
            {
                collectiblesToDelete.Add(goal);
                //TODO: Spawn ship to end level
            }
            else
            {
                goalTileX = r.Next(14);
                goalTileY = r.Next(10);
                goal.Position = new Vector2(labyrinth.GetTile(goalTileX, goalTileY).GetPosition().X, labyrinth.GetTile(goalTileX, goalTileY).GetPosition().Y);
                goal.CurrentTile = labyrinth.GetTile(goalTileX, goalTileY);
                goal.Active = true;
            }


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            //Draw each tile
            for (int i = 0; i < Labyrinth.WIDTH; i++)
            {
                for (int j = 0; j < Labyrinth.HEIGHT; j++)
                {
                    labyrinth.GetTile(i, j).DrawWalls(spriteBatch, GetTexture(GameTextures.HORIZONTAL_WALL), GetTexture(GameTextures.VERTICAL_WALL));
                }
            }

            //Draw of the outside border
            labyrinth.DrawHorizontal(spriteBatch, GetTexture(GameTextures.HORIZONTAL_WALL));
            labyrinth.DrawVertical(spriteBatch, GetTexture(GameTextures.VERTICAL_WALL));

            //Draw of the teleporter entrance and exits
            spriteBatch.Draw(GetTexture(GameTextures.WARP_ENTRANCE), warpEntreePos, Color.White);
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(GetTexture(GameTextures.WARP_EXIT), warpExitsPos[i], Color.White);
            }

            //Draw the police officers and the minions
            foreach (Character c in characters)
            {
                c.Draw(spriteBatch);
            }

            //Draw the pickups
            foreach (Collectible collectible in collectibles)
            {
                collectible.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}