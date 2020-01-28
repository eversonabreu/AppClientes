using Clientes.Domain.Entities;
using Clientes.Domain.Repositories;
using Clientes.Infraestructure.MongoDb;
using Clientes.Infraestructure.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clientes.Infraestructure.Repositories
{
    public class PessoaFisicaRepository : IPessoaFisicaRepository
    {
        private readonly IMongoCollection<PessoaFisicaEntity> pessoa;

        public PessoaFisicaRepository(IMongoDbDatabaseSettings mongoDbDatabaseSettings)
        {
            var client = new MongoClient(mongoDbDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbDatabaseSettings.DatabaseName);
            pessoa = database.GetCollection<PessoaFisicaEntity>("PessoaFisica");
        }

        public List<PessoaFisicaEntity> GetAll() => pessoa.Find(entity => true).ToList();

        public PessoaFisicaEntity GetById(string id) => pessoa.Find(entity => entity.Id == id).FirstOrDefault();

        public PessoaFisicaEntity Create(PessoaFisicaEntity entity)
        {
            ValidarCpf(entity);

            int qtdEnderecos = entity.Enderecos.Count;
            if (qtdEnderecos < 1 || qtdEnderecos > 3)
            {
                throw new Exception("A quantidade de endereços deve ser no mínimo 1 (um) e no máximo 3 (três).");
            }

            const decimal valorLimiteCreditoInicial = 100m;
            entity.ValorLimiteCredito = valorLimiteCreditoInicial;

            pessoa.InsertOne(entity);
            return entity;
        }

        public void Update(string id, PessoaFisicaEntity entity)
        {
            ValidarCpf(entity, id);

            var idsEnderecos = new List<string>();
            entity.Enderecos.ForEach(item => idsEnderecos.Add(item.Id));
            GetById(id).Enderecos.ForEach(item => idsEnderecos.Add(item.Id));
            idsEnderecos = idsEnderecos.Distinct().ToList();
            if (idsEnderecos.Count > 3)
            {
                throw new Exception("A quantidade de endereços deve ser no mínimo 1 (um) e no máximo 3 (três).");
            }

            if (entity.ValorLimiteCredito < 0m)
            {
                throw new Exception("O valor de limite de crédito não pode ser negativo.");
            }

            pessoa.ReplaceOne(ps => ps.Id == id, entity);
        }

        private void ValidarCpf(PessoaFisicaEntity entity, string id = null)
        {
            if (!ValidadorCpf.CPFValido(entity.Cpf))
            {
                throw new Exception("O CPF informado não é válido.");
            }

            if (id is null)
            {
                if (GetAll().Count(item => item.Cpf == entity.Cpf) > 0)
                {
                    throw new Exception("O CPF informado já esta sendo utilizado em outro registro.");
                }
            }
            else
            {
                if (GetAll().Count(item => item.Cpf == entity.Cpf && item.Id != id) > 0)
                {
                    throw new Exception("O CPF informado já esta sendo utilizado em outro registro.");
                }
            }
        }

        public void Remove(string id) => pessoa.DeleteOne(entity => entity.Id == id);
    }
}
