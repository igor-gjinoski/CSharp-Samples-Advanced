using System;
using System.Reflection;
using System.Collections.Concurrent;

namespace ExtremelyFastReflection
{
    public class PropertyHelper
    {
        private static readonly ConcurrentDictionary<string, Delegate> _cache =
            new ConcurrentDictionary<string, Delegate>();

        private static readonly MethodInfo CallInnerDelegateMethod =
            typeof(PropertyHelper)
            .GetMethod(nameof(CallInnerDelegate),
                BindingFlags.NonPublic |
                BindingFlags.Static);


        public static Func<object, TResult> MakeGetter<TResult>(PropertyInfo property)
        {
            return (Func<object, TResult>)
                _cache.GetOrAdd(property.Name, key =>
                {
                    var declaringClass = property.DeclaringType;
                    var typeOfResult = typeof(TResult);

                    var getMethodDelegateType =
                        typeof(Func<,>).MakeGenericType(declaringClass, typeOfResult);

                    var getMethodDelegate = property
                        .GetMethod
                        .CreateDelegate(getMethodDelegateType);

                    var callInnerGenericMethod =
                        CallInnerDelegateMethod
                        .MakeGenericMethod(declaringClass, typeOfResult)
                        .Invoke(null, new[] { getMethodDelegate });

                    return (Func<object, TResult>)callInnerGenericMethod;
                });
        }

        private static Func<object, TResult> CallInnerDelegate<TClass, TResult>(
            Func<TClass, TResult> @delegate)
        {
            return instance => @delegate((TClass)instance);
        }
    }
}
