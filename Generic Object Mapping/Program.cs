
namespace Generic_Object_Mapping
{
    class Program
    {
        static void Main()
        {
            var entity = new EntityObject()
            {
            };

            var foo = entity.Unit().ProjectTo<EntityDTO>();
        }
    }


    public interface IEntity
    {
    }

    public class EntityObject : IEntity
    {
        public object obj { get; set; }
    }


    public interface IEntityDto
    {
    }

    public class EntityDTO : IEntityDto
    {
        public object obj { get; set; }
    }
}
