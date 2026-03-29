using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Domain.ValueObjects
{
    public sealed class Idade
    {
        public int Value { get; }

        private Idade() { }

        public Idade(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("A idade deve ser maior que zero.");

            if (value > 140)
                throw new ArgumentOutOfRangeException("A idade máxima permitida é 140 anos.");

            Value = value;
        }

        public override bool Equals(object? obj)
            => obj is Idade other && Value == other.Value;

        public override int GetHashCode()
            => Value.GetHashCode();
        
        public static bool operator ==(Idade left, Idade right)
            => Equals(left, right);
        
        public static bool operator !=(Idade left, Idade right)
            => !Equals(left, right);
    }
}