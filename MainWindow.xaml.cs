using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            {GridValue.Empty, Images.Empty },
            {GridValue.Food, Images.Food },
            {GridValue.SuperFood, Images.SuperFood },
            {GridValue.Snake, Images.Body }
        };

        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            {Direction.up, 270 },
            {Direction.down, 90 },
            {Direction.right, 0 },
            {Direction.left, 180 }
        };

        private const int gridSize = 30;
        private readonly int rows = gridSize, cols = gridSize;
        private readonly Image[,] gridImages;
        private GameState gameState;
        private bool gameRunning;
        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
            Music.snakeTheme.PlayLooping();
        }

        private async Task RunGame()
        {
            Draw();
            Music.snakeTheme.Stop();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();

            gameState = new GameState(rows, cols);
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }
            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.gameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    gameState.ChangeDirection(Direction.up);
                    break;
                case Key.Down:
                    gameState.ChangeDirection(Direction.down);
                    break;
                case Key.Right:
                    gameState.ChangeDirection(Direction.right);
                    break;
                case Key.Left:
                    gameState.ChangeDirection(Direction.left);
                    break;
                case Key.Space:
                    gameState.onPause = (gameState.onPause == true) ? false : true;
                    break;
            }
        }


        private async Task GameLoop()
        {
            while (!gameState.gameOver)
            {
                await Task.Delay(100);
                gameState.Run();
                Draw();
            }
        }
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }
        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            DrawSnakeTail();
            ScoreText.Visibility = Visibility.Visible;
            ScoreText.Text = $"Score : {gameState.score}";
        }

        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridValue = gameState.grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridValue];
                }
            }
        }

        private void DrawSnakeHead()
        {
            Position headPos = gameState.HeadPosition();
            Image image = gridImages[headPos.row, headPos.column];

            image.Source = Images.Head;
            int rotation = dirToRotation[gameState.dir];
            image.RenderTransform = new RotateTransform(rotation);
        }
        private void DrawSnakeTail()
        {
            Position tailPos = gameState.TailPosition();
            Image image = gridImages[tailPos.row, tailPos.column];

            image.Source = Images.Tail;

        }
        private async Task DrawDeadSnake()
        {
            List<Position> snakePos = new(gameState.SnakePositions());
            for (int i = 0; i < snakePos.Count; i++)
            {
                Position pos = snakePos[i];
                ImageSource source;

                if (i == 0) source = Images.DeadHead;
                else if (i == snakePos.Count - 1) source = Images.DeadTail;
                else source = Images.DeadBody;

                gridImages[pos.row, pos.column].Source = source;
                await Task.Delay(50);
            }
        }

        private async Task ShowCountDown()
        {
            for (int i = 3; i > 0; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
            OverlayText.Text = "GO";
            await Task.Delay(500);
        }
        private async Task ShowGameOver()
        {
            Music.gameOver.Play();
            await DrawDeadSnake();
            Overlay.Visibility = Visibility.Visible;
            ScoreText.Visibility = Visibility.Hidden;
            OverlayText.Text = $"Your Score : {gameState.score}\nPress Any Key To Restart";
        }

    }
}
