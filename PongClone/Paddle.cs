using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    public enum PlayerType
    {
        Human,
        Computer
    }

    public class Paddle : Sprite
    {
        private readonly PlayerType _playerType;

#if ANDROID
        private const float PADDLE_SPEED = 5f;
#else
        private const float PADDLE_SPEED = 3.5f;
#endif


        public Paddle(Texture2D texture, Vector2 location, Rectangle bounds, PlayerType playerType) : base(texture, location, bounds)
        {
            _playerType = playerType;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (_playerType == PlayerType.Computer)
            {
                Random random = new Random();
                int reactionThreshold = random.Next(30, 130);

                if (gameObjects.Ball.Location.Y + gameObjects.Ball.Height < Location.Y + reactionThreshold)
                {
                    Velocity = new Vector2(0, -PADDLE_SPEED);
                }

                if (gameObjects.Ball.Location.Y > Location.Y + Height + reactionThreshold)
                {
                    Velocity = new Vector2(0, PADDLE_SPEED);
                }
            }

            if (_playerType == PlayerType.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || gameObjects.TouchInput.Up)
                {
                    Velocity = new Vector2(0, -PADDLE_SPEED);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down) || gameObjects.TouchInput.Down)
                {
                    Velocity = new Vector2(0, PADDLE_SPEED);
                }
            }

            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, _gameBoundaries.Height - _texture.Height);
        }
    }
}