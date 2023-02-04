using System;
using System.Collections.Generic;
namespace Snake
{
    public class GameState
    {
        public int rows { get; }
        public int columns { get; }
        public GridValue[,] grid { get; }
        public Direction dir { get; private set; }
        public int score { get; private set; }
        public bool gameOver { get; private set; }
        public bool onPause { get; set; }
        private readonly LinkedList<Direction> dirChanges = new();
        private readonly LinkedList<Position> snakePositions = new();
        private readonly Random random = new();

        public GameState(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.grid = new GridValue[rows, columns];
            dir = Direction.right;

            AddSnake();
            AddFood();
        }

        private void AddSnake()
        {
            int r = rows / 2;
            for (int c = 0; c < 3; c++)
            {
                grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositins()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddSuperFood(Position pos)
        {
            grid[pos.row, pos.column] = GridValue.SuperFood;
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositins());

            if (empty.Count == 0)
            {
                return;
            }

            Position pos = empty[random.Next(empty.Count)];
            if (score != 0 && score % 10 == 0)
            {
                AddSuperFood(pos);
            }
            else
                grid[pos.row, pos.column] = GridValue.Food;
        }

        public Position HeadPosition()
        {
            return snakePositions.First.Value;
        }

        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            grid[pos.row, pos.column] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            grid[tail.row, tail.column] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        private Direction GetLastDirection()
        {
            if (dirChanges.Count == 0)
            {
                return dir;
            }

            return dirChanges.Last.Value;
        }

        private bool CanChangeDirection(Direction newDir)
        {
            if (dirChanges.Count == 2)
            {
                return false;
            }

            Direction lastDir = GetLastDirection();
            return newDir != lastDir && newDir != lastDir.Opposite();
        }

        public void ChangeDirection(Direction dir)
        {
            if (CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            }
        }

        private bool OutsideGrid(Position pos)
        {
            return pos.row < 0 || pos.row >= rows
                || pos.column < 0 || pos.column >= columns;
        }

        private GridValue WillHit(Position nextPosition)
        {
            if (OutsideGrid(nextPosition))
            {
                return GridValue.Outside;
            }
            else if (nextPosition == TailPosition())
            {
                return GridValue.Empty;
            }

            return grid[nextPosition.row, nextPosition.column];
        }

        private void Move()
        {
            if (dirChanges.Count > 0)
            {
                dir = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }
            Position newHeadPos = HeadPosition().Translate(dir);
            GridValue nextPos = WillHit(newHeadPos);

            if (nextPos == GridValue.Food)
            {
                AddHead(newHeadPos);
                Music.eatFood.Play();
                score++;
                AddFood();
            }
            else if (nextPos == GridValue.SuperFood)
            {
                AddHead(newHeadPos);
                Music.eatSuperFood.Play();
                score += 3;
                AddFood();
            }
            else if (nextPos == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else
                gameOver = true;
        }
        public void Run()
        {
            if (!onPause)
            {
                Move();
            }
        }

    }
}
