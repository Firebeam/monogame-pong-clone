using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    public class Score
    {
        private readonly SpriteFont _font;
        private readonly Rectangle _gameBoundaries;

        public int PlayerScore { get; set; }
        public int EnemyScore { get; set; }

        public Score(SpriteFont font, Rectangle gameBoundaries)
        {
            _font = font;
            _gameBoundaries = gameBoundaries;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string scoreText = $"{PlayerScore}:{EnemyScore}";
            float xPosition = (_gameBoundaries.Width/2) - (_font.MeasureString(scoreText).X/2);
            Vector2 position = new Vector2(xPosition, _gameBoundaries.Height - 100);

            spriteBatch.DrawString(_font, scoreText, position, Color.Black);
        }

        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.Ball.Location.X + gameObjects.Ball.Width < 0)
            {
                EnemyScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }

            if (gameObjects.Ball.Location.X > _gameBoundaries.Width)
            {
                PlayerScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }
        }
    }
}