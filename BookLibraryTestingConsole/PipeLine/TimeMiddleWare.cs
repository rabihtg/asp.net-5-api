using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole.PipeLine
{
    public class TimeMiddleWare<T> : Pipe<T>
    {
        public TimeMiddleWare(Func<T, Task> next) : base(next) { }

        public override async Task Handle(T data)
        {
            Stopwatch timer = Stopwatch.StartNew();

            Console.WriteLine("This is time before _next");
            await _next(data);
            Console.WriteLine("This is time before _next");

            timer.Stop();

            Console.WriteLine($"{timer.ElapsedMilliseconds}");
            Console.WriteLine("######################### Done ########################");
        }
    }
}
