
namespace FunWithGenerics
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Collections.Generic;

    class Program
    {
        private const int _MAX = 65535; // Max number of generic parameters

        /// <summary>
        /// What’s the maximum number of generic parameters for a class in .NET/C#?
        /// 65535!
        /// </summary>
        static void Main()
        {
            var assemblyBuilder = DefineDynamicAssembly();
            var moduleBuilder = DefineDynamicModule(assemblyBuilder);
            var typeBuilder = DefineTypeBuilder(moduleBuilder);

            typeBuilder.DefineGenericParameters(Ts().ToArray());

            var generic = typeBuilder.CreateType();

            var t = generic.MakeGenericType(Repeat(typeof(int), _MAX).ToArray());

            PrintInstance(instance: Activator.CreateInstance(t));

            static IEnumerable<string> Ts()
            {
                return Range(0, _MAX).Select(x => $"T{x}");
            }

            static IEnumerable<long> Range(long start, long end)
            {
                for (var index = start; index < end; index++)
                    yield return index;
            }

            static IEnumerable<T> Repeat<T>(T value, long count)
            {
                for (var index = 0; index < count; index++)
                    yield return value;
            }
        }

        /// <summary>
        /// Print given object to the console.
        /// </summary>
        private static void PrintInstance(object instance) => Console.WriteLine(instance);


        /// <summary>
        /// Defines a dynamic assembly that has the specified name and access rights.
        /// </summary>
        private static AssemblyBuilder DefineDynamicAssembly()
            => AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Dummy"), AssemblyBuilderAccess.Run);


        /// <summary>
        /// Defines a named transient dynamic module in this assembly.
        /// </summary>
        private static ModuleBuilder DefineDynamicModule(AssemblyBuilder assemblyBuilder)
            => assemblyBuilder.DefineDynamicModule("Dummy");


        /// <summary>
        /// Constructs a TypeBuilder given the type name and the type attributes.
        /// </summary>
        private static TypeBuilder DefineTypeBuilder(ModuleBuilder moduleBuilder)
            => moduleBuilder.DefineType("RealGenericClass", TypeAttributes.Class);
    }
}
