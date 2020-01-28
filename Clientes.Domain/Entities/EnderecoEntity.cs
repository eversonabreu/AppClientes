using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clientes.Domain.Entities
{
    public class EnderecoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
