using System;
using System.Collections.Generic;
using System.Linq;
using ClassArchitecture.Abstractions;

namespace ClassArchitecture.Implementations
{
    /*
     * 
     * Pythagorean equation: a*a + b*b = c*c
     * Fundamental relation in Euclidean geometry
     * 
    */
    public class PythagoreanTheorem : IManipulator<int>
    {
        public int Manipulate(IEnumerable<int> data)
        {
            int[] dataAsArray = data.ToArray();
            int a = dataAsArray[0];
            int b = dataAsArray[1];

            return (int)(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
    }
}
