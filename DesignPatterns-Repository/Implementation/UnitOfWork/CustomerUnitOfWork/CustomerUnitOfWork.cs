using DesignPatterns_Repository.Data;

namespace DesignPatterns_Repository.Implementation.UnitOfWork.CustomerUnitOfWork
{
    public class CustomerUnitOfWork : UnitOfWork<ApplicationDbContext>
    {
        public CustomerUnitOfWork(ApplicationDbContext contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
