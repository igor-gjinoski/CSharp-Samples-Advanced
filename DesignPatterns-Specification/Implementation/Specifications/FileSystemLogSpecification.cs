
using System;
using System.Linq.Expressions;

namespace DesignPatterns_Specification.Implementation
{
    public class FileSystemLogSpecification : Specification<Log>
    {
        private readonly string _logInfo;

        public FileSystemLogSpecification(string logInfo)
        {
            _logInfo = logInfo;
        }


        public override Expression<Func<Log, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
