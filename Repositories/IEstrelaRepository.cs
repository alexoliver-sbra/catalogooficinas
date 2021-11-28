using CatalogoOficinas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.Repositories
{
    public interface IEstrelaRepository : IDisposable
    {
        Task<List<Estrela>> Obter(int pagina, int quantidade);
        Task<Estrela> Obter(Guid id);
        Task<List<Estrela>> Obter(string IdOficina, string IdUsuario);
        Task Inserir(Estrela estrela);
        Task Atualizar(Estrela estrela);
        Task Remover(Guid id);
    }
}
