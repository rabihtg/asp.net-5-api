using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole.PipeLine
{
    public class ExceptionMiddleWareTwo<T> : Pipe<T>
    {
        public ExceptionMiddleWareTwo(Func<T, Task> next) : base(next) { }

        public override async Task Handle(T data)
        {
            try
            {
                Console.WriteLine("In exception before action");
                await _next(data);
                Console.WriteLine("In exception after action");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Found an exception: {ex.Message}");
            }
        }
    }
}
