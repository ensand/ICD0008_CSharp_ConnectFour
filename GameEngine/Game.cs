﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace GameEngine
{
    public class Game
    {
        /// <summary>
        /// Game logic and database object
        /// </summary>
        
        public Guid GameId { get; set; }
        public string BoardString { get; set; } = "";
        public string? SaveName { get; set; }
        public string SaveCreationDateTime { get; set; } = "";
        [NotMapped] private int[][] Board { get; set; } = new int[0][];
        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }
        public bool PlayerOneMove { get; set; }

        public Game() { }
        
        public Game(GameSettings settings)
        {
            if (settings.BoardHeight < 4 || settings.BoardWidth < 4)
                throw new ArgumentException("Board size has to be at least " + settings.MinSize + "x" + settings.MinSize + ".");
            
            if (settings.BoardHeight > settings.MaxSize || settings.BoardWidth > settings.MaxSize)
                throw new ArgumentException("Board size has to be less than " + settings.MaxSize + "x" + settings.MaxSize + ".");

            BoardHeight = settings.BoardHeight;
            BoardWidth = settings.BoardWidth;
            Board = InitializeBoard(settings.BoardHeight, settings.BoardWidth);
            SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            SerializeBoard(Board);
        }

        public int[][] GetBoard()
        {
            var result = InitializeBoard(BoardHeight, BoardWidth);
            Array.Copy(Board, result, Board.Length);
            return result;
        }

        public void Move(int x)
        {
            var posX = x - 1;
            var posY = GetLowestEmptyPos(posX);
            Board[posY][posX] = PlayerOneMove ? 1 : 2;
            SerializeBoard(Board);
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
        
        private int[][] InitializeBoard(int height, int width)
        {
            int[][] board = new int[height][];
            for (var i = 0; i < height; i++)
            {
                board[i] = new int[width];
            }
            return board;
        }

        public void DeserializeBoard(string boardString)
        {
            Board = JsonSerializer.Deserialize<int[][]>(boardString);
        }

        public void SerializeBoard(int[][] board)
        {
            BoardString = JsonSerializer.Serialize(board);
        }
    }
}