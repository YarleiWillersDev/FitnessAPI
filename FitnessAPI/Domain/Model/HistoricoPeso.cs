using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessAPI.Domain.ValueObjects;

namespace FitnessAPI.Domain.Model
{
    public class HistoricoPeso
    {
        public Guid Id { get; private set; }
        public Peso Peso { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public Guid UsuarioId { get; private set; }

        private HistoricoPeso()
        {
            Peso = default!;
        }

        public HistoricoPeso(Peso peso)
        {
            Peso = peso;
            DataRegistro = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
    }
}