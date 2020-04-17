using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Organizadores;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos
{
    public class Evento : Entity<Evento>
    {
        public Evento(string nome,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool onLine,
            string nomeEmpresa)
        {

            Id = Guid.NewGuid();
            Nome = nome;
            DataIncio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = onLine;
            NomeEmpresa = nomeEmpresa;

        }
        public string Nome { get; private set; }
        public string DescricaoCurta { get; private set; }
        public string DescricaoLonga { get; private set; }
        public DateTime DataIncio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public decimal Valor { get; private set; }
        public bool Online { get; private set; }
        public string NomeEmpresa { get; private set; }
        public Categoria Categoria { get; private set; }
        public ICollection<Tags> Tags { get; private set; }
        public Edereco Endereco { get; private set; }
        public Organizador Organizador { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return validationResult.IsValid;

        }

        #region Validações

        private void Validar()
        {
            VaidarNome();
            VaidarValor();
            ValidarData();
            ValidarLocal();
            ValidarEmpresa();
            validationResult = Validate(this);
        }

        public void VaidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome do Evento precisa ser fornecido")
                .Length(3, 100).WithMessage("O Nome do evento precisa ter entre 3 e 100 caracteres");

        }

        public void VaidarValor()
        {
            if (!Gratuito)
                RuleFor(c => c.Valor)
                        .ExclusiveBetween(1, 50000)
                        .WithMessage("O valor deve estar entre 1.00 e 50.000");

            if (Gratuito)
                RuleFor(c => c.Valor)
                        .ExclusiveBetween(0, 0).When(e => e.Gratuito)
                        .WithMessage("O valor não deve ser diferente de 0");

        }

        public void ValidarData()
        {
            RuleFor(d => d.DataIncio)
                .GreaterThan(d => d.DataFim)
                .WithMessage("A data de inicio não pode ser superior a data final do evento");

            RuleFor(d => d.DataIncio)
                .LessThan(DateTime.Now)
                .WithMessage("A data de inicio não pode ser anterior a data atual");

        }

        public void ValidarLocal()
        {
            RuleFor(e => e.Endereco)
                .NotNull().When(o => o.Online)
                .WithMessage("O evento não deve possuir endereço quando for OnLine");

            RuleFor(e => e.Endereco)
                .NotNull().When(o => o.Online == false)
                .WithMessage("O evento deve possuir um endereço");

        }

        public void ValidarEmpresa()
        {
            RuleFor(e => e.NomeEmpresa)
                .NotEmpty().WithMessage("O nome da empresa precisa ser Fornecido")
                .Length(3, 150).WithMessage("O nome da empresa precisa ter entre 3 e 150 caracteres");
        }
        #endregion
    }
}
