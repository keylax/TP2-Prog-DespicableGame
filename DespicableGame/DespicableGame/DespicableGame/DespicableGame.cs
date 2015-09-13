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

        enum GameStates { PAUSED, PLAYING }
        public enum GameTextures { HORIZONTAL_WALL, VERTICAL_WALL, WARP_ENTRANCE, WARP_EXIT, GOAL, GRU, POLICE_OFFICER, LEVEL_EXIT, TRAP, SPEEDBOOST, POWERUP_IN_STORE, PLAYERTRAP_COLLECTIBLE, NUMBER_OF_TEXTURES }

        static Texture2D[] gameTextures = new Texture2D[(int)GameTextures.NUMBER_OF_TEXTURES];

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont textFont;
        Gamepad gamePad;
        GameStates currentState;
        TimeSpan lastPauseButtonPress = new TimeSpan(0, 0, 0);
        TimeSpan totalGameTime = new TimeSpan(0, 0, 0);

        public DespicableGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            currentState = GameStates.PLAYING;

            InitGraphicsMode(SCREENWIDTH, SCREENHEIGHT, false);
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
            gameTextures[(int)GameTextures.LEVEL_EXIT] = Content.Load<Texture2D>("Sprites\\SpaceShip");
            gameTextures[(int)GameTextures.TRAP] = Content.Load<Texture2D>("Sprites\\Trap");
            gameTextures[(int)GameTextures.SPEEDBOOST] = Content.Load<Texture2D>("Sprites\\Speedboost");
            gameTextures[(int)GameTextures.POWERUP_IN_STORE] = Content.Load<Texture2D>("Sprites\\PowerupInStore");
            gameTextures[(int)GameTextures.PLAYERTRAP_COLLECTIBLE] = Content.Load<Texture2D>("Sprites\\PlayerTrap_Collectible");
            textFont = Content.Load<SpriteFont>("Fonts/gamefont");

            gamePad = new Gamepad(GameManager.GetInstance().Gru, this);
            gamePad.RegisterKeyMapping();
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
            totalGameTime = gameTime.TotalGameTime;
            switch (currentState)
            {
                case GameStates.PAUSED:
                    //Nothing for now
                    break;

                case GameStates.PLAYING:
                    GameManager.GetInstance().ProcessFrame(gameTime.ElapsedGameTime);
                    break;
            }

            base.Update(gameTime);
        }

        public void PauseButtonPressAction()
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

            if (GameManager.GetInstance().Gru.Destination == null || GameManager.GetInstance().Gru.CurrentTile == GameManager.GetInstance().Gru.Destination)
            {
                gamePad.GetPressedButtons(padOneState);
                gamePad.Update();
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
                    GameManager.GetInstance().Labyrinth.GetTile(i, j).DrawWalls(spriteBatch, GetTexture(GameTextures.HORIZONTAL_WALL), GetTexture(GameTextures.VERTICAL_WALL));
                }
            }

            //Draw of the outside border
            GameManager.GetInstance().Labyrinth.DrawHorizontal(spriteBatch, GetTexture(GameTextures.HORIZONTAL_WALL));
            GameManager.GetInstance().Labyrinth.DrawVertical(spriteBatch, GetTexture(GameTextures.VERTICAL_WALL));

            //Draw of the teleporter entrance and exits
            spriteBatch.Draw(GetTexture(GameTextures.WARP_ENTRANCE), GameManager.GetInstance().WarpEntreePos, Color.White);
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(GetTexture(GameTextures.WARP_EXIT), GameManager.GetInstance().WarpExitsPos[i], Color.White);
            }

            //Draw the police officers and the minions
            foreach (Character c in GameManager.GetInstance().Characters)
            {
                c.Draw(spriteBatch);
            }

            //Draw the pickups
            foreach (Collectible collectible in GameManager.GetInstance().Collectibles)
            {
                collectible.Draw(spriteBatch);
            }

            if (currentState == GameStates.PAUSED)
            {
                spriteBatch.DrawString(textFont, "Game Paused", new Vector2(SCREENWIDTH / 7, SCREENHEIGHT / 3), Color.Yellow,
                  0, Vector2.One, 2f, SpriteEffects.None, 0.5f);
            }
            spriteBatch.Draw(GetTexture(GameTextures.POWERUP_IN_STORE), new Vector2(1000, 75), Color.White);
            if (GameManager.GetInstance().Gru.PowerUpInStore != null)
            {
                spriteBatch.Draw(GameManager.GetInstance().Gru.PowerUpInStore.Drawing, new Vector2(1005, 80), Color.White);
            }
            //Draw the player info elements
            spriteBatch.DrawString(textFont, GameManager.GetInstance().GetCurrentLevel(), new Vector2(1, 1), Color.Yellow,
                0, Vector2.One, 0.8f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(textFont, GameManager.GetInstance().GetGoalProgress(), new Vector2(240, 1), Color.Yellow,
                0, Vector2.One, 0.8f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(textFont, GameManager.GetInstance().GetLivesRemaining(), new Vector2(775, 1), Color.Yellow,
                0, Vector2.One, 0.8f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static Texture2D GetTexture(GameTextures desiredTexture)
        {
            return gameTextures[(int)desiredTexture];
        }

    }
}