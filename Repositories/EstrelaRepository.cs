using CatalogoOficinas.Entities;
using CatalogoOficinas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CatalogoOficinas.Repositories
{

    public class EstrelaRepository : IEstrelaRepository
    {
        private static Dictionary<Guid, Estrela> estrelas = new Dictionary<Guid, Estrela>()
        {
            {Guid.Parse("8185d868-dd94-4ebb-ab0c-5f3581af329e"), new Estrela{ Id = Guid.Parse("8185d868-dd94-4ebb-ab0c-5f3581af329e"), IdOficina = "A84AF7A4-9D31-44DB-9C34-390984E9FB41", IdUsuario = "2614EC1F-24AD-48BF-B4E3-FDCC19CE400D", Estrelas = 4.8 } },

        };

        public Task<List<Estrela>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(estrelas.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Estrela> Obter(Guid id)
        {
            if (!estrelas.ContainsKey(id))
                return null;

            return Task.FromResult(estrelas[id]);
        }
        public Task<Estrela> ObterEstrelas(Guid id)
        {
            if (!estrelas.ContainsKey(id))
                return null;

            return Task.FromResult(estrelas[id]);
        }

        public Task<List<Estrela>> Obter(string IdOficina, string IdUsuario)
        {
            return Task.FromResult(estrelas.Values.Where(estrela => estrela.IdOficina.Equals(IdOficina) && estrela.IdUsuario.Equals(IdUsuario)).ToList());
        }
        public Task<List<Estrela>> ObterSemLambda(string IdOficina, string IdUsuario)
        {
            var retorno = new List<Estrela>();

            foreach (var estrela in estrelas.Values)
            {
                if (estrela.IdOficina.Equals(IdOficina) && estrela.IdUsuario.Equals(IdUsuario))
                    retorno.Add(estrela);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Estrela estrela)
        {
            estrelas.Add(estrela.Id, estrela);
            return Task.CompletedTask;
        }

        public Task Atualizar(Estrela estrela)
        {
            estrelas[estrela.Id] = estrela;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            estrelas.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
