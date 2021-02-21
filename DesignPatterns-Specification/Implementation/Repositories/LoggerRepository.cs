
using System.Linq;
using System.Collections.Generic;
using NHibernate;

namespace DesignPatterns_Specification.Implementation
{
    public class LoggerRepository : Repository<Log>
    {
    }

    public abstract class Repository<T>
        where T : Entity
    {
        public IReadOnlyList<T> Find(Specification<T> specification, int page = 0, int pageSize = 100)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Query<T>()
                    .Where(specification.ToExpression())
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }
    }
}
