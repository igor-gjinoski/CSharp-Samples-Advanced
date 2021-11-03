
namespace ExplicitAndImplicitOperators
{
    /*
        In general:
            Implicit: something is being done for you automatically.
            Explicit: you've written something in the source code to indicate what you want to happen.
     */

    class Program
    {
        static void Main()
        {
            RoleWithImplicitOperator implicitRole = "dev";
            RoleWithExplicitOperator explicitRole = (RoleWithExplicitOperator)"dev";

            Person person = (Name: "Kosstow", Age: 29);
        }
    }

    class RoleWithImplicitOperator
    {
        public string RoleName { get; set; }

        public static implicit operator RoleWithImplicitOperator(string roleName)
            => new() 
            {
                RoleName = roleName
            };
    }

    class RoleWithExplicitOperator
    {
        public string RoleName { get; set; }

        public static explicit operator RoleWithExplicitOperator(string roleName)
            => new()
            {
                RoleName = roleName
            };
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public static implicit operator Person((string Name, int Age) personalData)
            => new()
            {
                Name = personalData.Name,
                Age = personalData.Age
            };
    }
}
