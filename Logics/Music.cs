using System.Media;

namespace Snake
{
    public struct Music
    {
        public readonly static SoundPlayer snakeTheme = new SoundPlayer(Properties.Resources.SnakeGameThemeSong);
        public readonly static SoundPlayer snake = new SoundPlayer(Properties.Resources.Snake);
        public readonly static SoundPlayer gameOver = new SoundPlayer(Properties.Resources.gameOver);
        public readonly static SoundPlayer eatFood = new SoundPlayer(Properties.Resources.eatFood);
        public readonly static SoundPlayer eatSuperFood = new SoundPlayer(Properties.Resources.eatSuperFood);
    }
}
