using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FitnessAPI.Domain.ValueObjects
{
    public sealed class Altura
    {
        public decimal Value { get; }

        private Altura() { }

        public Altura(decimal value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Altura deve ser maior que zero.");

            if (value >= 3)
                throw new ArgumentOutOfRangeException("Altura máxima permitida é de 3 metros");

            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Altura other && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Altura left, Altura right)
            => Equals(left, right);

        public static bool operator !=(Altura left, Altura right)
            => !Equals(left, right);
    }
}