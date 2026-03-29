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

        private readonly List<PlanoDeTreino> _planosDeTreino;
        public IReadOnlyCollection<PlanoDeTreino> PlanosDeTreino => _planosDeTreino;

        private readonly List<PlanoAlimentar> _planosAlimentares;
        public IReadOnlyCollection<PlanoAlimentar> PlanosAlimentares => _planosAlimentares;

        private readonly List<Objetivo> _objetivos;
        public IReadOnlyCollection<Objetivo> Objetivos => _objetivos;

        private readonly List<HistoricoPeso> _historicoPesos;
        public IReadOnlyCollection<HistoricoPeso> HistoricoPesos => _historicoPesos;

        private Usuario()
        {
            Nome = default!;
            Sobrenome = default!;
            Idade = default!;
            Altura = default!;
            Peso = default!;
            Email = default!;
            SenhaHash = default!;

            _planosDeTreino = new List<PlanoDeTreino>();
            _planosAlimentares = new List<PlanoAlimentar>();
            _objetivos = new List<Objetivo>();
            _historicoPesos = new List<HistoricoPeso>();
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

            _planosDeTreino = new List<PlanoDeTreino>();
            _planosAlimentares = new List<PlanoAlimentar>();
            _objetivos = new List<Objetivo>();
            _historicoPesos = new List<HistoricoPeso>();
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

        // Gestão de Dados Pessoais
        public void AtualizarPeso(Peso novoPeso)
        {
            ArgumentNullException.ThrowIfNull(novoPeso);

            if (Peso.Value == novoPeso.Value)
                return;
            
            _historicoPesos.Add(new HistoricoPeso(Peso));

            Peso = novoPeso;
        }

        // Métodos de PlanoDeTreino
        public void AdicionarPlanoDeTreino(PlanoDeTreino planoTreino)
        {
            ArgumentNullException.ThrowIfNull(planoTreino);

            GarantirQuePlanoDeTreinoNaoExiste(planoTreino);

            planoTreino.DefinirUsuario(this);
            _planosDeTreino.Add(planoTreino);
        }

        private void GarantirQuePlanoDeTreinoNaoExiste(PlanoDeTreino planoTreino)
        {
            if (PlanosDeTreino.Any(p => p.Nome.Equals(planoTreino.Nome)))
                throw new InvalidOperationException("Já existe um plano de treino com esse nome.");
        } 

        // Método de PlanoAlimentar
        public void AdicionarPlanoAlimentar(PlanoAlimentar planoAlimentar)
        {
            ArgumentNullException.ThrowIfNull(planoAlimentar);

            GarantirQuePlanoAlimentarNaoExiste(planoAlimentar);

            planoAlimentar.DefinirUsuario(this);
            _planosAlimentares.Add(planoAlimentar);
        }

        private void GarantirQuePlanoAlimentarNaoExiste(PlanoAlimentar planoAlimentar)
        {
            if (PlanosAlimentares.Any(p => p.Nome.Equals(planoAlimentar.Nome)))
                throw new InvalidOperationException("Já existe um plano alimentar com esse nome.");
        }

        //Métodos de Objetivo
        public void AdicionarObjetivo(Objetivo objetivo)
        {
            ArgumentNullException.ThrowIfNull(objetivo);

            GarantirQueObjetivoNaoExiste(objetivo);

            objetivo.DefinirUsuario(this);
            _objetivos.Add(objetivo);
        }

        private void GarantirQueObjetivoNaoExiste(Objetivo objetivo)
        {
            if(Objetivos.Any(o => o.Nome.Equals(objetivo.Nome)))
                throw new InvalidOperationException("Já existe um objetivo com esse nome.");
        }
    }
}