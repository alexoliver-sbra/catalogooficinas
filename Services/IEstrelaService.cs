using CatalogoOficinas.InputModel;
using CatalogoOficinas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.Services
{
    public interface IEstrelaService : IDisposable
    {
        Task<List<EstrelaViewModel>> Obter(int pagina, int quantidade);
        Task<EstrelaViewModel> Obter(Guid id);
        Task<EstrelaViewModel> Inserir(EstrelaInputModel estrela);
        Task Atualizar(Guid id, EstrelaInputModel estrela);
        Task Atualizar(Guid id, double estrelas);
        Task Remover(Guid id);
    }
}
