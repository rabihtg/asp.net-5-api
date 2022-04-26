using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole.Game
{
    public class TicTacToe
    {
        public List<string> Board { get; set; }
        public TicToePlayers Winner { get; set; }

        public TicTacToe()
        {
            Board = new() { " ", " ", " ", " ", " ", " ", " ", " ", " " };
            FillBoard();
        }

        public static void BoardSqaures()
        {
            for (int i = 1; i < 9; i += 3)
            {
                Console.WriteLine($"{i - 1} | {i} | {i + 1} ");
            }
        }

        public void FillBoard()
        {
            for (int i = 0; i < Board.Count; i++)
            {
                Board[i] = " ";
                Console.WriteLine(Board[i]);
            }
        }

        public void PrintBoard()
        {
            for (int i = 1; i < 9; i += 3)
            {
                Console.WriteLine($"{Board[i - 1]}|{Board[i]}|{Board[i + 1]}");
            }
        }

        public List<int> GetAvailbleMoves()
        {
            var availableMoves = new List<int>();
            for (int i = 0; i < Board.Count; i++)
            {
                if (Board[i] == " ")
                {
                    availableMoves.Add(i);
                }
            }
            return availableMoves;
        }

        public bool HasMoves()
        {
            return Board.Contains(" ");
        }

        public void MakeMove(int square, TicToePlayers player)
        {
            Board[square] = player.Letter;

            if (IsWinner(square, player))
            {
                Winner = player;
            }
        }

        public bool IsWinner(int square, TicToePlayers player)
        {
            int row = square / 3;
            int col = square % 3;

            bool winRow = true;
            bool winCol = true;
            bool winDiag = false;

            for (int i = (row * 3); i < (row * 3) + 3; i++)
            {

                if (Board[i] != player.Letter)
                {
                    winRow = false;
                }
            }

            for (int i = col; i < col + 7; i += 3)
            {
                if (Board[i] != player.Letter)
                {
                    winCol = false;
                }
            }

            if (square % 2 == 0)
            {
                winDiag = true;

                foreach (int i in new int[] { 0, 4, 8 })
                {
                    if (Board[i] != player.Letter)
                    {
                        winDiag = false;
                    }
                }

                foreach (int i in new int[] { 2, 4, 6 })
                {
                    if (Board[i] != player.Letter)
                    {
                        winDiag = false;
                    }
                }
            }

            return winRow || winCol || winDiag;
        }
    }
}
