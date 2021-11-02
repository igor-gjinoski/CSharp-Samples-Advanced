using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocalModel
{
    class Program
    {
        static void Main()
        {
            TaskMaker taskMaker = new();

            var one = taskMaker.RunTask("Test1", 22);
            var two = taskMaker.RunTask("Test2", 26);
            Task.WaitAll(one, two);
        }
    }

    /// <summary>
    /// AsyncLocal<T> 
    /// provide a mechanism to preserve values within an asynchronous execution context.
    /// </summary>
    class TaskMaker
    {
        // An await allows a method to return to the caller, which could change the context.
        // When execution returns control to the method,
        // it could be in a different thread,
        // even though from the async point of view the context is the same.
        // Using AsyncLocal<T> ensures that the state of the context is restored when the await returns control to the method after the awaitable object has completed.
        static AsyncLocal<string> _task = new();
        static AsyncLocal<int> _execTime = new();

        public async Task RunTask(string value, int time)
        {
            await Task.Delay(1000);
            _task.Value = value;
            _execTime.Value = time;
            await Run();
        }

        private async Task Run()
        {
            await Task.Delay(_execTime.Value);
            System.Console.WriteLine($"Task: {_task.Value}\nExecuted for: {_execTime.Value}\n");
        }
    }
}
