using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace PongClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameObjects _gameObjects;
        private Ball _ball;
        private Paddle _playerPaddle;
        private Paddle _enemyPaddle;
        private Score _score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
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

            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.Flick | GestureType.Tap;

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

            Rectangle gameBoundaries = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            _playerPaddle = new Paddle(Content.Load<Texture2D>("Paddle"), Vector2.Zero, gameBoundaries, PlayerType.Human);
            Texture2D enemyTexture = Content.Load<Texture2D>("Paddle2");
            _enemyPaddle = new Paddle(enemyTexture, new Vector2(gameBoundaries.Width - enemyTexture.Width, 0), gameBoundaries, PlayerType.Computer);
            _ball = new Ball(Content.Load<Texture2D>("Ball"), Vector2.Zero, gameBoundaries);
            _ball.AttachTo(_playerPaddle);

            _score = new Score(Content.Load<SpriteFont>("Score"), gameBoundaries);

            _gameObjects = new GameObjects
            {
                PlayerPaddle = _playerPaddle,
                Ball = _ball,
                EnemyPaddle = _enemyPaddle,
                Score = _score
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

            _gameObjects.TouchInput = new TouchInput();
            GetTouchInput();

            _playerPaddle.Update(gameTime, _gameObjects);
            _enemyPaddle.Update(gameTime, _gameObjects);
            _ball.Update(gameTime, _gameObjects);
            _score.Update(gameTime, _gameObjects);

            base.Update(gameTime);
        }

        private void GetTouchInput()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.Delta.Y > 0)
                {
                    _gameObjects.TouchInput.Down = true;
                }

                if (gesture.Delta.Y < 0)
                {
                    _gameObjects.TouchInput.Up = true;
                }

                if (gesture.GestureType == GestureType.Tap)
                {
                    _gameObjects.TouchInput.Tapped = true;
                }
            }
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
            _ball.Draw(spriteBatch);
            _score.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
