namespace PongClone
{
    public class GameObjects
    {
        public Paddle PlayerPaddle { get; set; }
        public Paddle EnemyPaddle { get; set; }
        public Ball Ball { get; set; }
        public Score Score { get; set; }
        public TouchInput TouchInput { get; set; }
    }
}