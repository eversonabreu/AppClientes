using Clientes.Domain.Entities;
using System.Collections.Generic;

namespace Clientes.Domain.Repositories
{
    public interface IPessoaJuridicaRepository
    {
        List<PessoaJuridicaEntity> GetAll();

        PessoaJuridicaEntity GetById(string id);

        PessoaJuridicaEntity Create(PessoaJuridicaEntity entity);

        void Update(string id, PessoaJuridicaEntity entity);

        void Remove(string id);
    }
}
