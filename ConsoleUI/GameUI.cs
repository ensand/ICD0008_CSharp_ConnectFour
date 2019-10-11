﻿using System;
using System.ComponentModel;
using GameEngine;

namespace ConsoleUI
{
    public class GameUI
    {
        /// <summary>
        /// Sets the board styles and looks
        /// </summary>
        
        private static readonly string _verticalSeparator = "|";
        private static readonly string _horizontalSeparator = "---";
        private static readonly string _centerSeparator = "+";
        
        public static void PrintBoard(Game game)
        {
            var board = game.GetBoard();
            Console.WriteLine(GetBoardIndexes(game));
            Console.WriteLine(GetHorizontalSeparator(game));
            for (var yIndex = 0; yIndex < game.BoardHeight; yIndex++)
            {
                Console.Write(_verticalSeparator);
                for (var xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                {
                    Console.Write(" " + GetSingleState(board[yIndex, xIndex]) + " ");
                    Console.ResetColor();
                    if (xIndex < game.BoardWidth)
                        Console.Write(_verticalSeparator);
                }
                Console.WriteLine();
                Console.WriteLine(GetHorizontalSeparator(game));
            }
        }

        private static String GetHorizontalSeparator(Game game)
        {
            var line = _centerSeparator;
            for (var xIndex = 0; xIndex < game.BoardWidth; xIndex++)
            {
                line += _horizontalSeparator;
                if (xIndex < game.BoardWidth)
                    line += _centerSeparator;
            }

            return line;
        }

        private static String GetBoardIndexes(Game game)
        {
            var line = " ";
            for (var xIndex = 0; xIndex < game.BoardWidth; xIndex++)
            {
                line += " " + xIndex + " ";
                if (xIndex < 10)
                    line += " ";
            }

            return line;
        }

        public static string GetSingleState(CellState state)
        {
            switch (state)
            {
                case CellState.Empty:
                    return " ";
                case CellState.PlayerOne:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    return "O";
                case CellState.PlayerTwo:
                    Console.ForegroundColor = ConsoleColor.Red;
                    return "0";
                default:
                    throw new InvalidEnumArgumentException("Unknown enum option!");
            }
        }
    }
}