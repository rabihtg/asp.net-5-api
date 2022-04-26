using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole.PipeLine
{
    public abstract class Pipe<T>
    {
        protected Func<T, Task> _next;

        public Pipe(Func<T, Task> next)
        {
            _next = next;
        }

        public abstract Task Handle(T data);
    }
}
