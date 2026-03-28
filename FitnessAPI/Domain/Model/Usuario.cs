using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessAPI.Domain.ValueObjects;


namespace FitnessAPI.Domain.Model
{
    public class Usuario
    {

        public Guid Id { get; private set; }
        public Nome Nome { get; private set; }
        public Sobrenome Sobrenome { get; private set; }
        public Idade Idade { get; private set; }
        public Altura Altura { get; private set; }
        public Peso Peso { get; private set; }
        public Email Email { get; private set; }
        public SenhaHash SenhaHash { get; private set; }

        public Guid PerfilId { get; private set; }
        public Perfil? Perfil { get; private set; }

        public ICollection<PlanoDeTreino> PlanosDeTreino { get; private set; }
        public ICollection<PlanoAlimentar> PlanosAlimentares { get; private set; }
        public ICollection<Objetivo> Objetivos { get; private set; }


        private Usuario()
        {
            Nome = default!;
            Sobrenome = default!;
            Idade = default!;
            Altura = default!;
            Peso = default!;
            Email = default!;
            SenhaHash = default!;

            PlanosDeTreino = new List<PlanoDeTreino>();
            PlanosAlimentares = new List<PlanoAlimentar>();
            Objetivos = new List<Objetivo>();
        }

        public Usuario(
            Nome nome,
            Sobrenome sobrenome,
            Idade idade,
            Altura altura,
            Peso peso,
            Email email,
            SenhaHash senhaHash
        )
        {
            ValidarParametros(nome, sobrenome, idade, altura, peso, email, senhaHash);

            Id = Guid.NewGuid();
            Nome = nome;
            Sobrenome = sobrenome;
            Idade = idade;
            Altura = altura;
            Peso = peso;
            Email = email;
            SenhaHash = senhaHash;

            PlanosDeTreino = new List<PlanoDeTreino>();
            PlanosAlimentares = new List<PlanoAlimentar>();
            Objetivos = new List<Objetivo>();
        }

        private void ValidarParametros(
            Nome nome,
            Sobrenome sobrenome,
            Idade idade,
            Altura altura,
            Peso peso,
            Email email,
            SenhaHash senhaHash)
        {
            ArgumentNullException.ThrowIfNull(nome);
            ArgumentNullException.ThrowIfNull(sobrenome);
            ArgumentNullException.ThrowIfNull(idade);
            ArgumentNullException.ThrowIfNull(altura);
            ArgumentNullException.ThrowIfNull(peso);
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(senhaHash);
        }

        public void AdicionarPlanoDeTreino(PlanoDeTreino planoTreino)
        {
            ArgumentNullException.ThrowIfNull(planoTreino);

            GarantirQuePlanoDeTreinoNaoExiste(planoTreino);

            planoTreino.DefinirUsuario(this);
            PlanosDeTreino.Add(planoTreino);
        }

        private void GarantirQuePlanoDeTreinoNaoExiste(PlanoDeTreino planoTreino)
        {
            if (PlanosDeTreino.Any(p => p.Nome.Equals(planoTreino.Nome)))
                throw new InvalidOperationException("Já existe um plano de treino com esse nome.");
        }

        public void AdicionarPlanoAlimentar(PlanoAlimentar planoAlimentar)
        {
            ArgumentNullException.ThrowIfNull(planoAlimentar);

            GarantirQuePlanoAlimentarNaoExiste(planoAlimentar);

            planoAlimentar.DefinirUsuario(this);
            PlanosAlimentares.Add(planoAlimentar);
        }

        private void GarantirQuePlanoAlimentarNaoExiste(PlanoAlimentar planoAlimentar)
        {
            if (PlanosAlimentares.Any(p => p.Nome.Equals(planoAlimentar.Nome)))
                throw new InvalidOperationException("Já existe um plano alimentar com esse nome.");
        }
    }
}