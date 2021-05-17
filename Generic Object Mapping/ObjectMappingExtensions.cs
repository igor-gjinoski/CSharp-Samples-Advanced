
namespace Generic_Object_Mapping
{
    public static class ObjectMappingExtensions
    {
        public static Unit<T> Unit<T>(this T source)
        {
            return new Unit<T>(source);
        }
    }

    public struct Unit<T>
    {
        private readonly T Value;

        public Unit(T value)
        {
            Value = value;
        }

        public U ProjectTo<U>()
        {
            return Utils.MapFunc<T, U>()
                .Invoke(Value);
        }
    }
}
