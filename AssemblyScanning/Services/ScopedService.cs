
namespace AssemblyScanning.Services
{
    public class ScopedService : IScopedService
    {
        public void Print()
            => System.Console.WriteLine(nameof(ScopedService));
    }
}
