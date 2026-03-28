using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessAPI.Domain.ValueObjects;

namespace FitnessAPI.Domain.Model
{
    public class PlanoDeTreino
    {
        
        public Nome Nome { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        private PlanoDeTreino()
        {
            Nome = default!;
        }

        public PlanoDeTreino(Nome nome)
        {
            ValidarParametros(nome);

            Nome = nome;
        }

        private void ValidarParametros(Nome nome)
        {
            ArgumentNullException.ThrowIfNull(nome);
        }

        public void DefinirUsuario(Usuario usuario)
        {
            ArgumentNullException.ThrowIfNull(usuario);

            GarantirQueNaoFoiReatribuido(usuario);
            
            Usuario = usuario;
            UsuarioId = usuario.Id;
        }

        private void GarantirQueNaoFoiReatribuido(Usuario usuario)
        {
            if(UsuarioId != Guid.Empty && UsuarioId != usuario.Id)
                throw new InvalidOperationException("Este plano já está vinculado a um usuário");
        }

    }
}