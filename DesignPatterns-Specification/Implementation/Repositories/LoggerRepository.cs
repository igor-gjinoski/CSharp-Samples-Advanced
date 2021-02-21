using System.Linq;
using NHibernate;

namespace DesignPatterns_Specification.Implementation
{
    public class LoggerRepository : Repository<Log>
    {
    }

    public abstract class Repository<T>
        where T : Entity
    {
        public bool LogInfo(Specification<T> specification)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                bool isOk;
                try
                {
                    session.Query<T>().Where(specification.ToExpression());
                    isOk = true;
                }
                finally
                {
                    isOk = false;
                }
                return isOk;
            }
        }
    }
}
