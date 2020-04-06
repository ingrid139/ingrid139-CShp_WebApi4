﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LojaServices3.Api.Models;
using LojaServices3.DTO;
using LojaServices3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaServices3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocaoController : ControllerBase
    {
        private IPromocoesService _promocaoService;
        private readonly IMapper _mapper;

        public PromocaoController(IPromocoesService promocaoService, IMapper mapper)
        {
            _promocaoService = promocaoService;
            _mapper = mapper;
        }

        // GET: api/Promocao
        [HttpGet]
        public ActionResult<IEnumerable<PromocaoDTO>> GetAll(int? promocaoId = null)
        {
            if (promocaoId.HasValue)
            {
                var promocoes = _promocaoService.ProdutosPorPromocaoId(promocaoId.Value);
                var retorno = _mapper.Map<List<PromocaoDTO>>(promocoes);

                return Ok(retorno);
            }
            else if (promocaoId == null)
            {
                return Ok(_promocaoService.ProdutosPromocoesLista().
                    Select(x => _mapper.Map<PromocaoDTO>(x)).
                    ToList());
            }
            else
                return NoContent();
        }

        // GET: api/Promocao/5
        [HttpGet("{id}")]
        public ActionResult<PromocaoDTO> Get(int id)
        {
            var promocao = _promocaoService.ProcurarPorId(id);

            if (promocao != null)
            {
                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper

                return Ok(_mapper.Map<PromocaoDTO>(promocao));
            }
            else
                return NotFound();
        }

        // POST: api/Promocao
        [HttpPost]
        public ActionResult<PromocaoDTO> Post([FromBody] PromocaoDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var promocao = _mapper.Map<Promocao>(value);
            //Salvar
            var retorno = _promocaoService.Salvar(promocao);
            //mapear Model para Dto
            return Ok(_mapper.Map<PromocaoDTO>(retorno));
        }

        // PUT: api/Promocao/5
        [HttpPut]
        public ActionResult<PromocaoDTO> Put([FromBody] PromocaoDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var promocao = _mapper.Map<Promocao>(value);
            //Salvar
            var retorno = _promocaoService.Salvar(promocao);
            //mapear Model para Dto
            return Ok(_mapper.Map<PromocaoDTO>(retorno));
        }

    }
}
