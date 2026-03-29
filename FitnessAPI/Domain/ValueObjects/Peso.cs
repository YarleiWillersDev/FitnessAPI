using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Domain.ValueObjects
{
    public sealed class Peso
    {
        public decimal Value { get; }

        private Peso() { }

        public Peso(decimal value)
        {
            if (value <= 20)
                throw new ArgumentException(nameof(value), "Peso abaixo do limite humano esperado");

            if (value > 700)
                throw new ArgumentOutOfRangeException(nameof(value), "O peso limite permitido para registro é 700KG");

            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Peso other && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Peso left, Peso right)
            => Equals(left, right);

        public static bool operator !=(Peso left, Peso right)
            => !Equals(left, right);
    }
}