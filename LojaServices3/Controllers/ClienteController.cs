using LojaServices3.Api.Models;
using LojaServices3.DTO;
using LojaServices3.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaServices3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IClienteService _clienteService;
        //private readonly IMapper mapper;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
            //this.mapper = mapper;
        }

        // Ok = 200
        // No content = 204 - nao tem conteudo no identificador
        // Created = 201
        // Not Found = 404
        // Bad Request = 400

        // Recursos - É qualquer coisa que possua um URI sendo um mapeamento conceitual para uma ou mais unidades.

        // Representação - Uma representação é um instantâneo do estado de um recurso em um ponto no tempo. Sempre que um cliente HTTP solicita um recurso, é a representação que é retornada, e não o próprio recurso (DTO)
        //Interações sem estado (Stateless) - Não armazenam nenhum contexto de cliente no servidor entre as solicitações


        // Mensagens - Usam métodos HTTP explicitamente. Por exemplo, GET, POST, PUT e DELETE

        // retornar uma Ação (ActionResult) tipada DTO (JSON)
        // GET api/cliente/{id}
        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> Get(int id)
        {
            var cliente = _clienteService.ProcurarPorId(id);

            if (cliente != null)
            {
                var retorno = new ClienteDTO()
                {
                    Id = cliente.Id,
                    EnderecoId = cliente.EnderecoId,
                    Nome = cliente.Nome
                };

                return Ok(retorno);
            }
            else
                return NotFound();
        }

        // POST api/cliente
        // binding argumento
        [HttpPost]
        public ActionResult<ClienteDTO> Post([FromBody]ClienteDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var endereco = new Endereco()
            {
                Logradouro = value.Endereco.Logradouro,
                Numero = value.Endereco.Numero,
                Complemento = value.Endereco.Complemento,
                Bairro = value.Endereco.Bairro,
                Cidade = value.Endereco.Cidade
            };

            var cliente = new Cliente()
            {
                Nome = value.Nome,
                EnderecoDeEntrega = endereco
            };

            var retornoCliente = _clienteService.Salvar(cliente);

            var retorno = new ClienteDTO()
            {
                Id = retornoCliente.Id,
                Nome = retornoCliente.Nome,
                EnderecoId = retornoCliente.EnderecoId
            };

            return Ok(retorno);
        }

        // POST api/cliente
        [HttpPut]
        public ActionResult<ClienteDTO> Put([FromBody] ClienteDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var endereco = new Endereco()
            {
                Id = value.EnderecoId,
                Logradouro = value.Endereco.Logradouro,
                Numero = value.Endereco.Numero,
                Complemento = value.Endereco.Complemento,
                Bairro = value.Endereco.Bairro,
                Cidade = value.Endereco.Cidade
            };

            var cliente = new Cliente()
            {
                Id = value.Id,
                Nome = value.Nome,
                EnderecoId = value.EnderecoId,
                EnderecoDeEntrega = endereco
            };

            var retornoCliente = _clienteService.Salvar(cliente);

            var retorno = new ClienteDTO()
            {
                Id = retornoCliente.Id,
                Nome = retornoCliente.Nome,
                EnderecoId = retornoCliente.EnderecoId
            };

            return Ok(retorno);
        }

    }
}