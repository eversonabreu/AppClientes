using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clientes.Domain.Entities
{
    public enum TipoContatoEnum
    {
        Principal = 1,
        Comercial = 2
    }

    public class PessoaContatosEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public TipoContatoEnum TipoContato { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public PessoaContatosEntity()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
