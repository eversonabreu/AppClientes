using Clientes.Domain.Entities;
using Clientes.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Clientes.Api.Controllers
{
    [Route("pessoa-fisica")]
    public class PessoaFisicaController : ControllerBase
    {
        private readonly IPessoaFisicaRepository pessoaFisicaRepository;

        public PessoaFisicaController(IPessoaFisicaRepository pessoaFisicaRepository) => this.pessoaFisicaRepository = pessoaFisicaRepository;

        [Route("inserir"), HttpPost]
        public PessoaFisicaEntity Post([FromBody] PessoaFisicaEntity entity) => pessoaFisicaRepository.Create(entity);

        [Route("atualizar/{id}"), HttpPut]
        public void Put(string id, [FromBody] PessoaFisicaEntity entity) => pessoaFisicaRepository.Update(id, entity);

        [Route("excluir/{id}"), HttpDelete]
        public void Delete(string id) => pessoaFisicaRepository.Remove(id);

        [Route("obter"), HttpGet]
        public List<PessoaFisicaEntity> GetAll() => pessoaFisicaRepository.GetAll();

        [Route("obter/{id}"), HttpGet]
        public PessoaFisicaEntity GetById(string id) => pessoaFisicaRepository.GetById(id);

    }
}
