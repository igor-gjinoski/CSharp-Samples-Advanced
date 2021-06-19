
namespace Generic_Object_Mapping
{
    class Program
    {
        static void Main()
        {
            var entity = new EntityObject()
            {
                str = "string test"
            };

            var foo = entity.Unit().ProjectTo<EntityDTO>();

            var bar = Map<EntityDTO, EntityObject>(entity);
        }

        static TResponse Map<TResponse, TObject>(TObject entity)
            where TObject : class
            where TResponse : class
        {
            var obj = entity.Unit().ProjectTo<TResponse>();

            return (TResponse)System.Convert.ChangeType(obj, typeof(TResponse));
        }
    }


    public interface IEntity
    {
    }

    public class EntityObject : IEntity
    {
        public string str { get; set; }
    }


    public interface IEntityDto
    {
    }

    public class EntityDTO : IEntityDto
    {
        public string str { get; set; }
    }
}
