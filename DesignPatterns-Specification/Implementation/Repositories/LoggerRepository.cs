
namespace DesignPatterns_Specification.Implementation
{
    public class LoggerRepository : Repository<Log>
    {
    }

    public abstract class Repository<T>
        where T : Entity
    { 
    }
}
