using System;
using System.Threading.Tasks;

namespace DesignPatterns_Repository.UnitType
{
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {
        private static readonly Unit _value = new();

        /// <summary>
        /// Default and only value of the <see cref="Unit"/> type.
        /// </summary>
        public static ref readonly Unit Value 
            => ref _value;

        /// <summary>
        /// Task from a <see cref="Unit"/> type.
        /// </summary>
        public static Task<Unit> Task { get; } 
            = System.Threading.Tasks.Task.FromResult(_value);

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        public int CompareTo(Unit other) 
            => 0;

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance 
        /// precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        int IComparable.CompareTo(object? obj) 
            => 0;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode() 
            => 0;

        /// <summary>
        /// Determines whether the current object is equal to another object of the same type.
        /// </summary>
        public bool Equals(Unit other) 
            => true;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        public override bool Equals(object? obj) 
            => obj is Unit;

        /// <summary>
        /// Determines whether the <paramref name="first"/> object is equal to the <paramref name="second"/> object.
        /// </summary>
        public static bool operator ==(Unit first, Unit second) 
            => true;

        /// <summary>
        /// Determines whether the <paramref name="first"/> object is not equal to the <paramref name="second"/> object.
        /// </summary>
        public static bool operator !=(Unit first, Unit second) 
            => false;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString() 
            => "()";
    }
}
