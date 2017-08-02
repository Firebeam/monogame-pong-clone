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
        private const float PADDLE_SPEED = 3.5f;

        public Paddle(Texture2D texture, Vector2 location, Rectangle bounds, PlayerType playerType) : base(texture, location, bounds)
        {
            _playerType = playerType;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (_playerType == PlayerType.Computer)
            {
                if (gameObjects.Ball.Location.Y + gameObjects.Ball.Height < Location.Y)
                {
                    Velocity = new Vector2(0, -PADDLE_SPEED);
                }

                if (gameObjects.Ball.Location.Y > Location.Y + Height)
                {
                    Velocity = new Vector2(0, PADDLE_SPEED);
                }
            }

            if (_playerType == PlayerType.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    Velocity = new Vector2(0, -PADDLE_SPEED);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
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