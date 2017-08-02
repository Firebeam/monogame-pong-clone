using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    public abstract class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Location;
        protected readonly Rectangle _gameBoundaries;

        public int Width => _texture.Width;
        public int Height => _texture.Height;

        public Rectangle BoundingBox => new Rectangle((int) Location.X, (int) Location.Y, Width, Height);

        public Vector2 Velocity { get; protected set; }

        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries)
        {
            this._texture = texture;
            this.Location = location;
            _gameBoundaries = gameBoundaries;
            Velocity = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;

            CheckBounds();
        }

        protected abstract void CheckBounds();

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Location, Color.White);
        }
    }
}
