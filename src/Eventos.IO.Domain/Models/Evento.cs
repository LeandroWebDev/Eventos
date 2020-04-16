﻿using Eventos.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Models
{
    public class Evento : Entity
    {
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
    }
}
