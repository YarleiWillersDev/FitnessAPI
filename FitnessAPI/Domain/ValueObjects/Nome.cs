using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Domain.ValueObjects
{
    public class Nome
    {
        public string Value { get; private set; }

        public Nome(string value)
        {
            ValidarParametros(value);

            Value = value;
        }

        private void ValidarParametros(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
        }
    }
}