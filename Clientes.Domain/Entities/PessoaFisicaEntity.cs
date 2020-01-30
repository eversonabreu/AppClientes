using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Clientes.Domain.Entities
{
    public class PessoaFisicaEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal ValorLimiteCredito { get; set; }
        public List<PessoaContatosEntity> Contatos { get; set; }
        public List<EnderecoEntity> Enderecos { get; set; }
    }
}
