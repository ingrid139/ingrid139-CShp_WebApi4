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
    public class ComprasController : ControllerBase
    {
        private ICompraService _comprasService;
        private readonly IMapper _mapper;

        public ComprasController(ICompraService comprasService, IMapper mapper)
        {
            _comprasService = comprasService;
            _mapper = mapper;
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public ActionResult<CompraDTO> Get(int id)
        {
            var compra = _comprasService.ProcurarPorId(id);

            if (compra != null)
            {
                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper

                return Ok(_mapper.Map<CompraDTO>(compra));
            }
            else
                return NotFound();
        }


        [HttpGet]
        public ActionResult<IEnumerable<CompraDTO>> GetAll(int? clienteId = null, int? produtoId = null)
        {
            if (clienteId.HasValue)
            {
                var compras = _comprasService.ProcurarPorClienteId(clienteId.Value).ToList();
                var retorno = _mapper.Map<List<CompraDTO>>(compras);

                return Ok(retorno);
            }
            else if (produtoId.HasValue)
            {
                return Ok(_comprasService.ProcurarPorProduto(produtoId.Value).
                    Select(x => _mapper.Map<CompraDTO>(x)).
                    ToList());
            }
            else
                return NoContent();
        }


        // POST: api/Compras
        [HttpPost]
        public ActionResult<CompraDTO> Post([FromBody]CompraDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var compra = _mapper.Map<Compra>(value);
            //Salvar
            var retorno = _comprasService.Salvar(compra);
            //mapear Model para Dto
            return Ok(_mapper.Map<CompraDTO>(retorno));
        }

    }
}
