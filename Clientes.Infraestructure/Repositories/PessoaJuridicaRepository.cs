﻿using Clientes.Domain.Entities;
using Clientes.Domain.Repositories;
using Clientes.Infraestructure.MongoDb;
using Clientes.Infraestructure.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clientes.Infraestructure.Repositories
{
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        private readonly IMongoCollection<PessoaJuridicaEntity> pessoa;

        public PessoaJuridicaRepository(IMongoDbDatabaseSettings mongoDbDatabaseSettings)
        {
            var client = new MongoClient(mongoDbDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbDatabaseSettings.DatabaseName);
            pessoa = database.GetCollection<PessoaJuridicaEntity>("PessoaJuridica");
        }

        public List<PessoaJuridicaEntity> GetAll() => pessoa.Find(entity => true).ToList();

        public PessoaJuridicaEntity GetById(string id) => pessoa.Find(entity => entity.Id.ToString() == id).FirstOrDefault();

        public PessoaJuridicaEntity Create(PessoaJuridicaEntity entity)
        {
            ValidarCnpj(entity);

            int qtdEnderecos = entity.Enderecos.Count;
            if (qtdEnderecos < 1 || qtdEnderecos > 3)
            {
                throw new Exception("A quantidade de endereços deve ser no mínimo 1 (um) e no máximo 3 (três).");
            }

            const decimal valorLimiteCreditoInicial = 100m;
            entity.ValorLimiteCredito = valorLimiteCreditoInicial;

            entity.Id = ObjectId.GenerateNewId();
            pessoa.InsertOne(entity);
            return entity;
        }

        public void Update(string id, PessoaJuridicaEntity entity)
        {
            ValidarCnpj(entity, id);

            var idsEnderecos = new List<string>();
            entity.Enderecos.ForEach(item => idsEnderecos.Add(item.Id.ToString()));
            GetById(id).Enderecos.ForEach(item => idsEnderecos.Add(item.Id.ToString()));
            idsEnderecos = idsEnderecos.Distinct().ToList();
            if (idsEnderecos.Count > 3)
            {
                throw new Exception("A quantidade de endereços deve ser no mínimo 1 (um) e no máximo 3 (três).");
            }

            if (entity.ValorLimiteCredito < 0m)
            {
                throw new Exception("O valor de limite de crédito não pode ser negativo.");
            }

            pessoa.ReplaceOne(ps => ps.Id.ToString() == id, entity);
        }

        public void Remove(string id) => pessoa.DeleteOne(entity => entity.Id.ToString() == id);

        private void ValidarCnpj(PessoaJuridicaEntity entity, string id = null)
        {
            if (!ValidadorCNPJ.CNPJValido(entity.Cnpj))
            {
                throw new Exception("O CNPJ informado não é válido.");
            }

            if (id is null)
            {
                if (GetAll().Count(item => item.Cnpj == entity.Cnpj) > 0)
                {
                    throw new Exception("O CNPJ informado já esta sendo utilizado em outro registro.");
                }
            }
            else
            {
                if (GetAll().Count(item => item.Cnpj == entity.Cnpj && item.Id.ToString() != id) > 0)
                {
                    throw new Exception("O CNPJ informado já esta sendo utilizado em outro registro.");
                }
            }
        }
    }
}
