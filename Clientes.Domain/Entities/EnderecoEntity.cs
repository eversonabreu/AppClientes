using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clientes.Domain.Entities
{
    public class EnderecoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        public EnderecoEntity()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
