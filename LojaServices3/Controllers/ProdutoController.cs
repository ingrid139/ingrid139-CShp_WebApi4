using System;
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
    public class ProdutoController : ControllerBase
    {

        private IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        // GET: api/Produto
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDto>> Get()
        {
            var produtos = _produtoService.ListarProdutos();

            var retorno = new List<ProdutoDto>();
            if (produtos != null)
            {
                //foreach (var item in produtos)
                //{
                //    var retornoAux = new ProdutoDto()
                //    {
                //        Id = item.Id,
                //        Categoria = item.Categoria,
                //        Nome = item.Nome,
                //        PrecoUnitario = item.PrecoUnitario
                //    };

                //    retorno.Add(retornoAux);
                //}

                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper
                retorno = _mapper.Map<List<ProdutoDto>>(produtos);

                return Ok(retorno);
            }
            else
                return NotFound();
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public ActionResult<ProdutoDto> Get(int id)
        {
            var produto = _produtoService.ProcurarPorId(id);

            if (produto != null)
            {
                var retorno = _mapper.Map<ProdutoDto>(produto);

                return Ok(retorno);
            }
            else
                return NotFound();
        }


        // GET: api/Produto/aleatorio
        [HttpGet("aleatorio")]
        public ActionResult<ProdutoJSONDTO> GetAleatorio()
        {
            var produto = _produtoService.ProcurarAleatorio();

            if (produto != null)
            {
                var retorno = _mapper.Map<ProdutoJSONDTO>(produto);

                return Ok(retorno);
            }
            else
                return NotFound();
        }

        // POST: api/Produto
        [HttpPost]
        public ActionResult<ProdutoDto> Post([FromBody]ProdutoDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var produto = _mapper.Map<Produto>(value);
            //Salvar
            var retorno = _produtoService.Salvar(produto);
            //mapear Model para Dto
            return Ok(_mapper.Map<ProdutoDto>(retorno));
        }

        // PUT: api/Produto/5
        [HttpPut]
        public ActionResult<ProdutoDto> Put([FromBody] ProdutoDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var produto = _mapper.Map<Produto>(value);
            //Salvar
            var retorno = _produtoService.Salvar(produto);
            //mapear Model para Dto

            return Ok(_mapper.Map<ProdutoDto>(retorno));
        }

    }
}
