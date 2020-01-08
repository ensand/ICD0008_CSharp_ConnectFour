using System;

namespace GameEngine
{
    public class Game
    {
        /// <summary>
        /// Game logic
        /// </summary>
        
        private int[][] Board { get; set; }

        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }

        public bool PlayerOneMove { get; set; }

        private int[][] InitializeBoard(int height, int width)
        {
            int[][] board = new int[height][];
            for (var i = 0; i < height; i++)
            {
                board[i] = new int[width];
            }
            return board;
        }

        public Game(GameSettings settings)
        {
            if (settings.BoardHeight < 4 || settings.BoardWidth < 4)
                throw new ArgumentException("Board size has to be at least 4x4.");
            
            BoardHeight = settings.BoardHeight;
            BoardWidth = settings.BoardWidth;
            Board = InitializeBoard(BoardHeight, BoardWidth);
        }
        
        public int[][] GetBoard()
        {
            var result = InitializeBoard(BoardHeight, BoardWidth);
            Array.Copy(Board, result, Board.Length);
            return result;
        }

        public void LoadGame(int[][] board)
        {
            Board = board;
            BoardHeight = board.Length;
            BoardWidth = board[0].Length;
        }
        
        public bool IsGameDone()
        {
            for (var yIndex = 0; yIndex < BoardHeight; yIndex++)
            {
                for (var xIndex = 0; xIndex < BoardWidth; xIndex++)
                {
                    if (Board[yIndex][xIndex] == 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public void Move(int x)
        {
            var posX = x - 1;
            var posY = GetLowestEmptyPos(posX);
            Board[posY][posX] = PlayerOneMove ? 1 : 2;
            PlayerOneMove = !PlayerOneMove;
        }

        private int GetLowestEmptyPos(int posX)
        {
            for (var i = BoardHeight-1; i > -1; i--)
            {
                if (Board[i][posX] == 0)
                    return i;
            }

            return -1;
        }

        public bool IsColumnFull(int x)
        {
            var posX = x - 1;
            for (var i = 0; i < BoardHeight; i++)
            {
                if (Board[i][posX] == 0)
                    return false;
            }

            return true;
        }
    }
}