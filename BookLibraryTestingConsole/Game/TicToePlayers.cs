using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole.Game
{
    public abstract class TicToePlayers
    {
        public string Letter { get; set; }

        public abstract int MakeMove(List<int> availableMoves);
    }

    public class HumanPlayer : TicToePlayers
    {
        public override int MakeMove(List<int> availableMoves)
        {
            if (availableMoves.Count == 0) return -1;
            Console.WriteLine($"Player {Letter} Enter you move: ");

            var validMove = false;
            while (!validMove)
            {
                var isNumber = int.TryParse(Console.ReadLine(), out var val);
                if (isNumber)
                {
                    if (availableMoves.Contains(val))
                    {
                        return val;
                    }
                    else
                    {
                        Console.Write($"Invalid move: Available Moves: ");
                        foreach (var item in availableMoves)
                        {
                            Console.Write($"{item} - ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Move Has to be a ## Number ##!");
                }
            }

            return -1;
        }
    }

    public class RandomCompPlayer : TicToePlayers
    {
        public Random Random = new();
        public override int MakeMove(List<int> availableMoves)
        {
            int index = Random.Next(0, availableMoves.Count);
            Console.WriteLine($"Player {Letter} made a move to {availableMoves[index]}");
            return availableMoves[index];
        }
    }

}
