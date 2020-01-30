using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Clientes.Domain.Entities
{
    public class PessoaJuridicaEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public decimal ValorLimiteCredito { get; set; }
        public List<PessoaContatosEntity> Contatos { get; set; }
        public List<EnderecoEntity> Enderecos { get; set; }
    }
}
