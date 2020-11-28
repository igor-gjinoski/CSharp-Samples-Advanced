
namespace CSharp9._0___Init_only_setters
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
}
