using System;
using System.Linq.Expressions;

namespace DesignPatterns_Specification.Implementation
{
    public class DatabaseLogSpecification : Specification<Log>
    {
        private readonly string _dbLog;

        public DatabaseLogSpecification(string dbLog)
        {
            _dbLog = dbLog;
        }

        public override Expression<Func<Log, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
