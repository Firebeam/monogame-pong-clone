using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PongCloneGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameObjects _gameObjects;
        private Ball ball;
        private Paddle _playerPaddle;
        private Paddle _enemyPaddle;

        public PongCloneGame()
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
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Rectangle gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            _playerPaddle = new Paddle(Content.Load<Texture2D>("Paddle"), Vector2.Zero, gameBoundaries, PlayerType.Human);
            Texture2D enemyTexture = Content.Load<Texture2D>("Paddle2");
            _enemyPaddle = new Paddle(enemyTexture, new Vector2(gameBoundaries.Width - enemyTexture.Width, 0), gameBoundaries, PlayerType.Computer);
            ball = new Ball(Content.Load<Texture2D>("Ball"), Vector2.Zero, gameBoundaries);
            ball.AttachTo(_playerPaddle);

            _gameObjects = new GameObjects
            {
                PlayerPaddle = _playerPaddle,
                Ball = ball,
                EnemyPaddle = _enemyPaddle
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _playerPaddle.Update(gameTime, _gameObjects);
            _enemyPaddle.Update(gameTime, _gameObjects);
            ball.Update(gameTime, _gameObjects);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            _playerPaddle.Draw(spriteBatch);
            _enemyPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
