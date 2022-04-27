using BookLibraryClassLibrary.Models;
using BookLibraryTestingConsole.Game;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole
{
    public class Program
    {
        public static List<Thread> Count = new();

        public static Task MainAction(BookModel book)
        {

            foreach (var item in book.GetType().GetProperties())
            {
                Console.WriteLine($"{item.Name}: {item.GetValue(book)}");
            }

            Console.WriteLine($"This is Thread: {Thread.CurrentThread.ManagedThreadId}, waiting");

            var x = 5;

            for (int i = 0; i < 100_000_000; i++)
            {
                x *= i;
            }

            Count.Add(Thread.CurrentThread);
            return Task.CompletedTask;
        }

        public static void Main(string[] args)
        {
            /*var book = new BookModel()
            {
                DateAdded = DateTime.Now.AddDays(-100),
                Title = "A New Book",
                Description = "Something",
                PublisherId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };

            var x = new PipeBuilder<BookModel>(MainAction).AddPipe(typeof(TimeMiddleWare<BookModel>)).AddPipe(typeof(ExceptionMiddleWareTwo<BookModel>)).Build();

            for (int i = 0; i < 10; i++)
            {
                var t = new Thread(() => x(book));
                t.Start();
                t.Join();
            }

            var run = true;
            while (run)
            {
                var c = Console.ReadLine();
                if (c == "w")
                {
                    run = false;
                }
            }
            Console.WriteLine($"Total: {Count.Count}");*/

            var game = new TicTacToe();

            var playerOne = new HumanPlayer();
            var playerThree = new HumanPlayer();

            var playerTwo = new RandomCompPlayer();

            PlayGame(game, playerOne, playerThree);
        }

        public static void PlayGame(TicTacToe game, TicToePlayers playerOne, TicToePlayers playerTwo)
        {
            playerOne.Letter = "X";
            playerTwo.Letter = "O";

            var currentPlayer = playerOne;

            TicTacToe.BoardSqaures();

            while (game.HasMoves())
            {
                var sqaure = currentPlayer.MakeMove(game.GetAvailbleMoves());
                game.MakeMove(sqaure, currentPlayer);
                game.PrintBoard();

                if (game.Winner == currentPlayer)
                {
                    Console.WriteLine();
                    Console.WriteLine("#############################################");
                    Console.WriteLine();
                    Console.WriteLine($"\t\t{currentPlayer.Letter} Wins!");
                    Console.WriteLine();
                    Console.WriteLine("#############################################");
                    Console.WriteLine();
                    return;
                }

                currentPlayer = currentPlayer == playerOne ? playerTwo : playerOne;
            }
            Console.WriteLine("It's A Tie.");
        }
    }
}
