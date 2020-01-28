using Clientes.Domain.Entities;
using System.Collections.Generic;

namespace Clientes.Domain.Repositories
{
    public interface IPessoaFisicaRepository
    {
        List<PessoaFisicaEntity> GetAll();

        PessoaFisicaEntity GetById(string id);

        PessoaFisicaEntity Create(PessoaFisicaEntity entity);

        void Update(string id, PessoaFisicaEntity entity);

        void Remove(string id);
    }
}
