using Sigil;
using System;
using System.Reflection;

namespace CompileTimePerformanceWithReflection
{
    public static class ReflectionUsage
    {
        public static string NoReflectionGet()
        {
            var obj = new ReflectionTestClass();

            return obj.PublicProperty;
        }

        /*
         * Traditional Reflection
         */
        public static string TraditionalReflection()
        {
            var reflectionTestClass = new ReflectionTestClass();

            var propertyInfo = reflectionTestClass
                .GetType()
                .GetProperty("PublicProperty", BindingFlags.Instance | BindingFlags.Public);

            return propertyInfo!
                    .GetValue(reflectionTestClass)
                    .ToString();
        }


        /*
         * Optimization!
         * PropertyInfo is quite heavy operation to get.
         * We can cache it.
         */
        private static PropertyInfo _cachePropertyInfo =
            typeof(ReflectionTestClass)
                .GetProperty("PublicProperty", BindingFlags.Instance | BindingFlags.Public);

        public static string OptimizedTraditionalReflection()
        {
            var reflectionTestClass = new ReflectionTestClass();

            return _cachePropertyInfo!
                    .GetValue(reflectionTestClass)
                    .ToString();
        }


        /*
         * The best way is to use Delegate
         * Create and invoke Delegate with cached property.
         */
        private static readonly Func<ReflectionTestClass, string> GetPropertyDelegate =
            (Func<ReflectionTestClass, string>)
                Delegate.CreateDelegate(
                    typeof(Func<ReflectionTestClass, string>),
                    _cachePropertyInfo.GetGetMethod(true)!);

        public static string CompiledDelegate()
        {
            var instance = new ReflectionTestClass();

            return GetPropertyDelegate(instance);
        }


        /*
         * However, the above code has some limitations.
         * We can make it more generic and Emit the class
         * to create PropertyEmmiter and get access to types at runtime like internal.
         */
        private static readonly Type classType = typeof(ReflectionTestClass);

        private static readonly Emit<Func<object, string>> GetPropertyEmmiter =
            Emit<Func<object, string>>
                .NewDynamicMethod("GetPropertyValue")
                .LoadArgument(0)
                .CastClass(classType) // Here we can make it Generic and use typeof(T)
                .Call(_cachePropertyInfo.GetGetMethod(true)!)
                .Return();

        private static readonly Func<object, string> GetPropertyEmmitedDelegate =
            GetPropertyEmmiter.CreateDelegate();

        public static string EmmitedVersion()
        {
            var internalClass = Activator.CreateInstance(classType);

            return GetPropertyEmmitedDelegate(internalClass);
        }
    }




    #region TEST CLASSES

    public class ReflectionTestClass
    {
        public string PublicProperty { get; set; } = "Default";
    }

    #endregion
}
