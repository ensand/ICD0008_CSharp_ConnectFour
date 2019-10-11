using System;

namespace GameEngine
{
    public class Game
    {
        /// <summary>
        /// Game logic
        /// </summary>
        
        private CellState[,] Board { get; set; }

        public int BoardHeight { get; }
        public int BoardWidth { get; }

        private bool _playerOneMove;

        public Game(int height = 4, int width = 4)
        {
            if (height < 4 || width < 4)
                throw new ArgumentException("Board size has to be at least 4x4.");
            
            BoardHeight = height;
            BoardWidth = width;
            Board = new CellState[BoardHeight, BoardWidth];
        }
        
        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }


        public bool IsGameDone()
        {
            for (var yIndex = 0; yIndex < BoardHeight; yIndex++)
            {
                for (var xIndex = 0; xIndex < BoardWidth; xIndex++)
                {
                    if (Board[yIndex, xIndex] == CellState.Empty)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public void Move(int posX)
        {
            var posY = GetLowestEmptyPos(posX);
            Board[posY, posX] = _playerOneMove ? CellState.PlayerOne : CellState.PlayerTwo;
            _playerOneMove = !_playerOneMove;
        }

        private int GetLowestEmptyPos(int posX)
        {
            for (var i = BoardHeight-1; i > -1; i--)
            {
                if (Board[i, posX] == CellState.Empty)
                    return i;
            }

            return -1;
        }

        public bool IsColumnFull(int posX)
        {
            for (var i = 0; i < BoardHeight; i++)
            {
                if (Board[i, posX] == CellState.Empty)
                    return false;
            }

            return true;
        }
    }
}