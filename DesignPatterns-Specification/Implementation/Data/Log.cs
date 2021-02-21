using System;

namespace DesignPatterns_Specification.Implementation
{
    public class Log : Entity
    {
        public virtual string _logData { get; }
        public virtual DateTime _createdOn { get; }

        protected Log()
        {
        }

        public Log(string logData, DateTime createdOn)
            : this()
        {
            _logData = logData;
            _createdOn = createdOn;
        }
    }
}
