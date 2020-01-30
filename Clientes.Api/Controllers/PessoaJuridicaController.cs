using Clientes.Domain.Entities;
using Clientes.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Clientes.Api.Controllers
{
    //[EnableCors("AllowOrigin")]
    [Route("pessoa-juridica")]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly IPessoaJuridicaRepository pessoaJuridicaRepository;

        public PessoaJuridicaController(IPessoaJuridicaRepository pessoaJuridicaRepository) => this.pessoaJuridicaRepository = pessoaJuridicaRepository;

        [Route("inserir"), HttpPost]
        public PessoaJuridicaEntity Post([FromBody] PessoaJuridicaEntity entity) => pessoaJuridicaRepository.Create(entity);

        [Route("atualizar/{id}"), HttpPut]
        public void Put(string id, [FromBody] PessoaJuridicaEntity entity) => pessoaJuridicaRepository.Update(id, entity);

        [Route("excluir/{id}"), HttpDelete]
        public void Delete(string id) => pessoaJuridicaRepository.Remove(id);

        [Route("obter"), HttpGet]
        public List<PessoaJuridicaEntity> GetAll() => pessoaJuridicaRepository.GetAll();

        [Route("obter/{id}"), HttpGet]
        public PessoaJuridicaEntity GetById(string id) => pessoaJuridicaRepository.GetById(id);

    }
}
