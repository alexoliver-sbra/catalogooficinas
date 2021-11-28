using CatalogoOficinas.Entities;
using CatalogoOficinas.Exceptions;
using CatalogoOficinas.InputModel;
using CatalogoOficinas.Repositories;
using CatalogoOficinas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.Services
{
    public class EstrelaService : IEstrelaService
    {
        private readonly IEstrelaRepository _estrelaRepository;

        public EstrelaService(IEstrelaRepository estrelaRepository)
        {
            _estrelaRepository = estrelaRepository;
        }

        public async Task<List<EstrelaViewModel>> Obter(int pagina, int quantidade)
        {
            var estrelas = await _estrelaRepository.Obter(pagina, quantidade);

            return estrelas.Select(estrela => new EstrelaViewModel
            {
                Id = estrela.Id,
                IdOficina = estrela.IdOficina,
                IdUsuario = estrela.IdUsuario,
                Estrelas = estrela.Estrelas
            }).ToList();
        }

        public async Task<EstrelaViewModel> Obter(Guid id)
        {
            var estrela = await _estrelaRepository.Obter(id);

            if (estrela == null)
                return null;

            return new EstrelaViewModel
            {
                Id = estrela.Id,
                IdOficina = estrela.IdOficina,
                IdUsuario = estrela.IdUsuario,
                Estrelas = estrela.Estrelas
            };
        }
        public async Task<EstrelaViewModel> ObterEstrelas(Guid id)
        {
            var estrela = await _estrelaRepository.Obter(id);

            if (estrela == null)
                return null;

            return new EstrelaViewModel
            {
                Id = estrela.Id,
                IdOficina = estrela.IdOficina,
                IdUsuario = estrela.IdUsuario,
                Estrelas = estrela.Estrelas
            };
        }

        public async Task<EstrelaViewModel> Inserir(EstrelaInputModel estrela)
        {
            var entidadeEstrela = await _estrelaRepository.Obter(estrela.IdOficina, estrela.IdUsuario);


            var estrelaInsert = new Estrela
            {
                Id = Guid.NewGuid(),
                IdOficina = estrela.IdOficina,
                IdUsuario = estrela.IdUsuario,
                Estrelas = estrela.Estrelas
            };

            await _estrelaRepository.Inserir(estrelaInsert);

            return new EstrelaViewModel
            {
                Id = estrelaInsert.Id,
                IdOficina = estrela.IdOficina,
                IdUsuario = estrela.IdUsuario,
                Estrelas = estrela.Estrelas
            };
        }

        public async Task Atualizar(Guid id, EstrelaInputModel estrela)
        {
            var entidadeEstrela = await _estrelaRepository.Obter(id);

            if (entidadeEstrela == null)
                throw new OficinaNaoCadastradaException();

            entidadeEstrela.IdOficina = estrela.IdOficina;
            entidadeEstrela.IdUsuario = estrela.IdUsuario;
            entidadeEstrela.Estrelas = estrela.Estrelas;
 

            await _estrelaRepository.Atualizar(entidadeEstrela);
        }

        public async Task Atualizar(Guid id, double estrelas)
        {
            var entidadeEstrela = await _estrelaRepository.Obter(id);

            if (entidadeEstrela == null)
                throw new OficinaNaoCadastradaException();

            entidadeEstrela.Estrelas = estrelas;

            await _estrelaRepository.Atualizar(entidadeEstrela);
                
        }
        public async Task Remover(Guid id)
        {
            var estrela = await _estrelaRepository.Obter(id);

            if(estrela == null)
                throw new OficinaNaoCadastradaException();

            await _estrelaRepository.Remover(id);
        }

        public void Dispose()
        {
            _estrelaRepository?.Dispose();
        }

    }
}
