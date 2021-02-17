using System;
using System.Linq;
using System.Collections.Generic;

namespace ExtremelyFastReflection
{
    class Program
    {
        static void Main()
        {
            var homeController = new HomeController();

            var property = homeController
                .GetType()
                .GetProperties()
                .FirstOrDefault(x => x.IsDefined(typeof(DataAttribute), true));

            var @delegate = PropertyHelper
                    .MakeGetter<IDictionary<string, object>>(property);

            var dictionary = @delegate(homeController);

            Console.WriteLine(
                $"Key: {dictionary.Keys.First()}, " +
                $"Value: {dictionary.Values.First()}");
        }
    }


    public class HomeController
    {
        [Data]
        public Dictionary<string, object> Data { get; set; }

        public HomeController()
            => Data = new Dictionary<string, object>
            {
                ["left"] = "right"
            };
    }


    public class DataAttribute : Attribute
    {
    }
}
