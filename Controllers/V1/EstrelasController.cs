using CatalogoOficinas.Exceptions;
using CatalogoOficinas.InputModel;
using CatalogoOficinas.Services;
using CatalogoOficinas.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoOficinas.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class EstrelasController : ControllerBase
    {
        private readonly IEstrelaService _estrelaService;
        public EstrelasController(IEstrelaService estrelaService)
        {
            _estrelaService = estrelaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstrelaViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var estrelas = await _estrelaService.Obter(pagina, quantidade);
            if (estrelas.Count() == 0)
                return NoContent();

            return Ok(estrelas);
        }

        [HttpGet("{idOficina:guid}")]
        public async Task<ActionResult<EstrelaViewModel>> Obter([FromRoute]Guid idEstrela)
        {
            var estrela = await _estrelaService.Obter(idEstrela);
            if (estrela == null)
                return NoContent();

            return Ok(estrela);
        }

        [HttpPost]
        public async Task<ActionResult<EstrelaViewModel>> InserirEstrela([FromBody]EstrelaInputModel estrelaInputModel)
        {
            try
            {
                var estrela = await _estrelaService.Inserir(estrelaInputModel);

                return Ok(estrela);
            }
            catch (OficinaJaCadastradaException ex)
            
            {
                return UnprocessableEntity("Esta Oficina já existe");
            }
        }
        [HttpPut("{idOficina:guid}")]
        public async Task<ActionResult> AtualizarEstrela([FromRoute]Guid idEstrela, [FromBody]EstrelaInputModel estrelaInputModel)
        {
            try
            {
                await _estrelaService.Atualizar(idEstrela, estrelaInputModel);
                return Ok();
            }
            catch (OficinaNaoCadastradaException ex)
            
            {
                return NotFound("Não existe essa Oficina");
            }
        }
        [HttpPatch("{idOficina:guid}/estrela/{estrela}")]
        public async Task<ActionResult> AtualizarEstrela([FromRoute]Guid idEstrela,[FromRoute] double estrela)
        {
            try
            {
                await _estrelaService.Atualizar(idEstrela, estrela);

                return Ok();            
            }
            catch (OficinaNaoCadastradaException ex)
            
            {
                return NotFound("Não existe essa Oficina");
            }
        }
    
        [HttpDelete("{idOficina:guid}")]
        public async Task<ActionResult> ApagarEstrela([FromRoute]Guid idEstrela)
        {
            try
            {
                await _estrelaService.Remover(idEstrela);
                return Ok();
            }
            catch (OficinaNaoCadastradaException ex)
            {
                return NotFound("Não existe essa Oficina");
            }
        }
    }
}
