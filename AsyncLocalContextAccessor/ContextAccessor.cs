using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AsyncLocalContextAccessor
{
    public class ContextAccessor<TContext> : IContextAccessor<TContext>
        where TContext : DbContext
    {
    }
}
