using System;
using System.Xml.Serialization;

namespace GameEngine
{
    public class Game
    {
        /// <summary>
        /// Game logic
        /// </summary>
        
        private CellState[][] Board { get; set; }

        public int BoardHeight { get; }
        public int BoardWidth { get; }

        public bool PlayerOneMove { get; set; }

        private CellState[][] InitializeBoard(int height, int width)
        {
            CellState[][] board = new CellState[height][];
            for (var i = 0; i < height; i++)
            {
                board[i] = new CellState[width];
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
        
        public CellState[][] GetBoard()
        {
            var result = InitializeBoard(BoardHeight, BoardWidth);
            Array.Copy(Board, result, Board.Length);
            return result;
        }


        public bool IsGameDone()
        {
            for (var yIndex = 0; yIndex < BoardHeight; yIndex++)
            {
                for (var xIndex = 0; xIndex < BoardWidth; xIndex++)
                {
                    if (Board[yIndex][xIndex] == CellState.Empty)
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
            Board[posY][posX] = PlayerOneMove ? CellState.PlayerOne : CellState.PlayerTwo;
            PlayerOneMove = !PlayerOneMove;
        }

        private int GetLowestEmptyPos(int posX)
        {
            for (var i = BoardHeight-1; i > -1; i--)
            {
                if (Board[i][posX] == CellState.Empty)
                    return i;
            }

            return -1;
        }

        public bool IsColumnFull(int x)
        {
            var posX = x - 1;
            for (var i = 0; i < BoardHeight; i++)
            {
                if (Board[i][posX] == CellState.Empty)
                    return false;
            }

            return true;
        }
    }
}