
namespace AsyncLocalContextAccessor
{
    public interface IContextAccessor<TContext>
    {
        TContext Context { get; set; }
    }
}
