using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    public class Ball : Sprite
    {
        private Paddle _attachedToPaddle;

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
        }

        protected override void CheckBounds()
        {
            if (Location.Y >= (_gameBoundaries.Height - _texture.Height) || Location.Y <= 0)
            {
                Vector2 newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                Velocity = newVelocity;
            }
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _attachedToPaddle != null)
            {
                Vector2 newVelocity = new Vector2(5.0f, _attachedToPaddle.Velocity.Y * .6f);
                Velocity = newVelocity;
                _attachedToPaddle = null;
            }

            if (_attachedToPaddle != null)
            {
                Location.X = _attachedToPaddle.Location.X + _attachedToPaddle.Width;
                Location.Y = _attachedToPaddle.Location.Y;
            }
            else
            {
                if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) ||
                    BoundingBox.Intersects(gameObjects.EnemyPaddle.BoundingBox))
                {
                    Vector2 newVelocity = new Vector2(-Velocity.X, Velocity.Y);
                    Velocity = newVelocity;
                }
            }

            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {
            _attachedToPaddle = paddle;
        }
    }
}