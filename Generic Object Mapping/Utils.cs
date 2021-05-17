using System;
using System.Linq;
using System.Linq.Expressions;

namespace Generic_Object_Mapping
{
    public static class Utils
    {
        public static Func<TInput, TOutput> MapFunc<TInput, TOutput>()
        {
            var source = Expression.Parameter(typeof(TInput), "source");

            var body = Expression.MemberInit(
                Expression.New(
                        typeof(TOutput)),
                        source.Type.GetProperties()
                                   .Select(property =>
                                       Expression.Bind(typeof(TOutput).GetProperty(property.Name),
                                       Expression.Property(source, property))));

            var expr = Expression.Lambda<Func<TInput, TOutput>>(
                body,
                source);

            return expr.Compile();
        }
    }
}
