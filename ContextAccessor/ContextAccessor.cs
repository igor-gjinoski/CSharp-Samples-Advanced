using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ContextAccessor
{
    public class ContextAccessor<TContext> : IContextAccessor<TContext>
        where TContext : DbContext
    {
        private static readonly AsyncLocal<ContextHolder> _contextCurrent = new();

        public TContext? Context
        {
            get
            {
                return _contextCurrent.Value?.Context;
            }
            set
            {
                var holder = _contextCurrent.Value;
                if (holder != null)
                {
                    // Clear current Context trapped in the AsyncLocals, as its done.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the HttpContext in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    _contextCurrent.Value = 
                        new() 
                        { 
                            Context = value 
                        };
                }
            }
        }

        private class ContextHolder
        {
            public TContext? Context;
        }
    }
}
