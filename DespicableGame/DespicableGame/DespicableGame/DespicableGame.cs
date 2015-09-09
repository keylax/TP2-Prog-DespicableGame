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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;

        PlayerCharacter Gru;
        List<Character> characters; //Minions and police officers

        Texture2D murHorizontal;
        Texture2D murVertical;
        Texture2D warpEntrance;
        Texture2D bananas;

        Vector2 warpEntreePos;

        Texture2D[] warpExits = new Texture2D[4];
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
            characters = new List<Character>();
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

            murHorizontal = Content.Load<Texture2D>("Sprites\\Hwall");
            murVertical = Content.Load<Texture2D>("Sprites\\Vwall");
            bananas = Content.Load<Texture2D>("Sprites\\banana");

            Gru = (PlayerCharacter)CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.GRU, Content.Load<Texture2D>("Sprites\\Gru"), new Vector2(labyrinth.GetCase(DEPART_X, DEPART_Y).GetPosition().X, labyrinth.GetCase(DEPART_X, DEPART_Y).GetPosition().Y), labyrinth.GetCase(DEPART_X, DEPART_Y));

            //Add a police officer
            characters.Add(CharacterFactory.CreateCharacter(CharacterFactory.CharacterType.POLICE_OFFICER, Content.Load<Texture2D>("Sprites\\Police"), new Vector2(labyrinth.GetCase(7, 9).GetPosition().X, labyrinth.GetCase(7, 9).GetPosition().Y), labyrinth.GetCase(7, 9)));

            //Teleporter entrance
            warpEntrance = Content.Load<Texture2D>("Sprites\\Warp1");
            warpEntreePos = new Vector2(labyrinth.GetCase(7, 4).GetPosition().X - Tile.LIGN_SIZE, labyrinth.GetCase(7, 4).GetPosition().Y + Tile.LIGN_SIZE);

            //Teleporter exits
            for (int i = 0; i < warpExits.Length; i++)
            {
                warpExits[i] = Content.Load<Texture2D>("Sprites\\Warp2");
            }

            warpExitsPos[0] = new Vector2(labyrinth.GetCase(0, 0).GetPosition().X, labyrinth.GetCase(0, 0).GetPosition().Y);
            warpExitsPos[1] = new Vector2(labyrinth.GetCase(Labyrinth.WIDTH - 1, 0).GetPosition().X, labyrinth.GetCase(Labyrinth.WIDTH - 1, 0).GetPosition().Y);
            warpExitsPos[2] = new Vector2(labyrinth.GetCase(0, Labyrinth.HEIGHT - 1).GetPosition().X, labyrinth.GetCase(0, Labyrinth.HEIGHT - 1).GetPosition().Y);
            warpExitsPos[3] = new Vector2(labyrinth.GetCase(Labyrinth.WIDTH - 1, Labyrinth.HEIGHT - 1).GetPosition().X, labyrinth.GetCase(Labyrinth.WIDTH - 1, Labyrinth.HEIGHT - 1).GetPosition().Y);
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
            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || padOneState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

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

            Gru.Move();
            foreach (NonPlayerCharacter c in characters)
            {
                c.Move();
            }
            base.Update(gameTime);
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
                    labyrinth.GetCase(i, j).DrawWalls(spriteBatch, murHorizontal, murVertical);
                }
            }

            //Draw of the outside border
            labyrinth.DrawHorizontal(spriteBatch, murHorizontal);
            labyrinth.DrawVertical(spriteBatch, murVertical);

            //Draw of the teleporter entrance
            spriteBatch.Draw(warpEntrance, warpEntreePos, Color.White);

            //Draw the teleporter exits
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(warpExits[i], warpExitsPos[i], Color.White);
            }

            //Draw the police officers and the minions
            foreach(NonPlayerCharacter c in characters)
            {
                c.Draw(spriteBatch);
            }

            //Draw Gru
            Gru.Draw(spriteBatch);

            spriteBatch.Draw(bananas, Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
