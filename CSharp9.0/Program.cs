
namespace CSharp9
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                new WeatherObservation
                {
                    RecordedAt = DateTime.Now,
                    TemperatureInCelsius = 20,
                    PressureInMillibars = 998.0m
                }
                .ToString());


            /*
             * If you need to mutate immutable properties of a record instance, 
             * you can use a with expression to achieve nondestructive mutation. 
             * A with expression makes a new record instance that is a copy of an existing record instance, 
             * with specified properties and fields modified.
             */
            var person = new Person 
            { 
                FirstName = "A", 
                LastName = "K" 
            };
            var otherPerson = person 
                with 
                { 
                    LastName = "Other" 
                };
        }
    }

    /// <summary>
    /// Init only setters provide consistent syntax to initialize members of an object.
    /// Starting with C# 9.0, you can create init accessors instead of set accessors for properties and indexers.
    /// Changing after initialization is an error by assigning to an init-only property outside of initialization.
    /// </summary>
    public struct WeatherObservation
    {
        public DateTime RecordedAt { get; init; }
        public decimal TemperatureInCelsius { get; init; }
        public decimal PressureInMillibars { get; init; }

        public override string ToString() =>
            $"At {RecordedAt:h:mm tt} on {RecordedAt:M/d/yyyy}: " +
            $"Temp = {TemperatureInCelsius}, with {PressureInMillibars} pressure";
    }


    /// <summary>
    /// record 
    /// Define a reference type that provides built-in functionality for encapsulating data.
    /// Primarily intended for supporting immutable data models.
    /// </summary>
    public record Person
    {
        #nullable enable
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }
}
