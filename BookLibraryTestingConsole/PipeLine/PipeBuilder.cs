using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTestingConsole.PipeLine
{
    public class PipeBuilder<T>
    {
        private readonly Func<T, Task> _mainAction;
        private readonly List<Type> _pipes;

        public PipeBuilder(Func<T, Task> mainAction)
        {
            _mainAction = mainAction;
            _pipes = new();
        }

        public PipeBuilder<T> AddPipe(Type pipe)
        {
            _pipes.Add(pipe);
            return this;
        }

        public Func<T, Task> Build()
        {
            return CreatePipe(0);
        }

        private Func<T, Task> CreatePipe(int index)
        {
            if (index < _pipes.Count - 1)
            {
                var action = CreatePipe(index + 1);
                var childPipe = (Pipe<T>)Activator.CreateInstance(_pipes[index], action);
                return childPipe.Handle;
            }
            else
            {
                var mainPipe = (Pipe<T>)Activator.CreateInstance(_pipes[index], _mainAction);
                return mainPipe.Handle;
            }
        }

    }
}
