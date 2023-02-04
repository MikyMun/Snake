using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Snake
{
    public static class Images
    {
        public readonly static ImageSource Empty = LoadImage("texture.jpg");
        public readonly static ImageSource Body = LoadImage("snakeBody.png");
        public readonly static ImageSource Head = LoadImage("snakeHead.png");
        public readonly static ImageSource Tail = LoadImage("snakeTail.png");
        public readonly static ImageSource DeadBody = LoadImage("snakeDeadBody.png");
        public readonly static ImageSource DeadHead = LoadImage("snakeDeadHead.png");
        public readonly static ImageSource DeadTail = LoadImage("snakeDeadTail.png");
        public readonly static ImageSource Food = LoadImage("Apple.png");
        public readonly static ImageSource SuperFood = LoadImage("strawberry.png");

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"SnakeAssets/{fileName}", UriKind.Relative));
        }
    }
}
