using AutoMapper;
using LojaServices3.Api.Models;
using LojaServices3.DTO;
using LojaServices3.Services;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Loja.Controller.Tests
{
    public class ServicesFake
    {
        private Dictionary<Type, string> NomesArquivosDados { get; } =
           new Dictionary<Type, string>();
        

        public IMapper Mapper { get; }

        public ServicesFake()
        {
            // preenchimento dos paths para acesso aos arquivos de testes 
            // chave = tipo do Modelo, valor = path string da localização do arquivo no projeto
            //dadsos mocados para retorno do serviço
            NomesArquivosDados.Add(typeof(Endereco), $"DadosFake{Path.DirectorySeparatorChar}enderecos.json");

            NomesArquivosDados.Add(typeof(Cliente), $"DadosFake{Path.DirectorySeparatorChar}clientes.json");

            NomesArquivosDados.Add(typeof(Produto), $"DadosFake{Path.DirectorySeparatorChar}produtos.json");

            NomesArquivosDados.Add(typeof(Compra), $"DadosFake{Path.DirectorySeparatorChar}compras.json");

            NomesArquivosDados.Add(typeof(Promocao), $"DadosFake{Path.DirectorySeparatorChar}promocoes.json");

            NomesArquivosDados.Add(typeof(PromocaoProduto), $"DadosFake{Path.DirectorySeparatorChar}promocaoproduto.json");

            // preenchimento dos paths para acesso aos arquivos de testes 
            // chave = tipo do Modelo, valor = path string da localização do arquivo no projeto
            //dadsos mocados para testes do POST
            NomesArquivosDados.Add(typeof(EnderecoDTO), $"DadosFake{Path.DirectorySeparatorChar}enderecos.json");

            NomesArquivosDados.Add(typeof(ClienteDTO), $"DadosFake{Path.DirectorySeparatorChar}clientes.json");

            NomesArquivosDados.Add(typeof(ProdutoDto), $"DadosFake{Path.DirectorySeparatorChar}produtos.json");

            NomesArquivosDados.Add(typeof(CompraDTO), $"DadosFake{Path.DirectorySeparatorChar}compras.json");

            NomesArquivosDados.Add(typeof(PromocaoDTO), $"DadosFake{Path.DirectorySeparatorChar}promocoes.json");

            NomesArquivosDados.Add(typeof(PromocaoProdutoDTO), $"DadosFake{Path.DirectorySeparatorChar}promocaoproduto.json");


            //add config de mapeamento
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Endereco, EnderecoDTO>().ReverseMap();
                cfg.CreateMap<Cliente, ClienteDTO>().ReverseMap();
                cfg.CreateMap<Produto, ProdutoDto>().ReverseMap();
                cfg.CreateMap<Compra, CompraDTO>().ReverseMap();
                cfg.CreateMap<PromocaoProduto, PromocaoProdutoDTO>().ReverseMap();
                cfg.CreateMap<Promocao, PromocaoDTO>()
                    .ForMember(dest => dest.PromocaoProdutos, opt => opt.MapFrom(src => src.Produtos)).ReverseMap();
            });

            this.Mapper = configuration.CreateMapper();
        }

        //método que retorna o arquivo de dados fake através do método tipado

        private string NomeArquivo<T>() 
        {
            // retorno do valor de acordo com a chave - dictionary
            // variável T (tê maiúsculo) representa um tipo genérico, podemos recuperar através de todos modelos 
            return NomesArquivosDados[typeof(T)]; 
        }

        //método que retorna os dados fake deserializado de acordo com o método tipado
        public List<T> GetDadosFake<T>()
        {
            // retorna lista tipada JSON (serealizada)
            string conteudo = File.ReadAllText(NomeArquivo<T>());

            // variável T (tê maiúsculo) representa um tipo genérico, podemos deserializar através de todos modelos 
            return JsonConvert.DeserializeObject<List<T>>(conteudo);
        }

        #region Cliente Services Mock
        public Mock<IClienteService> FakeClienteService()
        {
            // nova instancia de Mock do serviço
            var service = new Mock<IClienteService>();

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProcurarPorId(It.IsAny<int>())).
                Returns((int id) => GetDadosFake<Cliente>().FirstOrDefault(x => x.Id == id));


            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.Salvar(It.IsAny<Cliente>())).
                Returns((Cliente cliente) => {
                    // ao add novo registro o server devolve novo Id e EnderecoId
                    if (cliente.Id == 0)
                    {
                        cliente.Id = 999;
                        cliente.EnderecoId = 998;
                    }
                    return cliente;
                });

            // retorno da nova instancia de Mock do serviço - com os métodos mocados
            return service;
        }

        #endregion

        #region Produto Services Mock

        public Mock<IProdutoService> FakeProdutoService()
        {
            // nova instancia de Mock do serviço
            var service = new Mock<IProdutoService>();

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProcurarPorId(It.IsAny<int>())).
                Returns((int id) => GetDadosFake<Produto>().FirstOrDefault(x => x.Id == id));

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ListarProdutos()).
                Returns(() => GetDadosFake<Produto>().ToList());


            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.Salvar(It.IsAny<Produto>())).
                Returns((Produto produto) => {
                    // ao add novo registro o server devolve novo Id
                    if (produto.Id == 0)
                    {
                        produto.Id = 999;
                    }
                    return produto;
                });

            // retorno da nova instancia de Mock do serviço - com os métodos mocados
            return service;
        }

        #endregion

        #region Compra Services Mock

        public Mock<ICompraService> FakeCompraService()
        {
            // nova instancia de Mock do serviço
            var service = new Mock<ICompraService>();

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProcurarPorId(It.IsAny<int>())).
                Returns((int id) => GetDadosFake<Compra>().FirstOrDefault(x => x.Id == id));

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProcurarPorClienteId(It.IsAny<int>())).
                Returns((int id) => {
                    var retorno = GetDadosFake<Compra>().Where(x => x.ClienteId == id).ToList();
                    retorno.ForEach(c => {
                        c.Cliente = GetDadosFake<Cliente>().Where(x => x.Id == c.ClienteId).FirstOrDefault();
                        c.Produtos = GetDadosFake<Produto>().Where(x => x.Id == c.ProdutoId).FirstOrDefault();
                    });

                    return retorno;
                });

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProcurarPorProduto(It.IsAny<int>())).
                Returns((int id) => {
                    var retorno = GetDadosFake<Compra>().Where(x => x.ProdutoId == id).ToList();
                    retorno.ForEach(c => {
                        c.Cliente = GetDadosFake<Cliente>().Where(x => x.Id == c.ClienteId).FirstOrDefault();
                        c.Produtos = GetDadosFake<Produto>().Where(x => x.Id == c.ProdutoId).FirstOrDefault();
                    });
                    return retorno;
                });


            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.Salvar(It.IsAny<Compra>())).
                Returns((Compra compra) => {
                    // ao add novo registro o server devolve novo Id
                    if (compra.Id == 0)
                    {
                        compra.Id = 999;
                    }
                    return compra;
                });

            // retorno da nova instancia de Mock do serviço - com os métodos mocados
            return service;
        }

        #endregion

        #region Promocao Services Mock

        public Mock<IPromocoesService> FakePromocaoService()
        {
            // nova instancia de Mock do serviço
            var service = new Mock<IPromocoesService>();

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProcurarPorId(It.IsAny<int>())).
                Returns((int id) => GetDadosFake<Promocao>().FirstOrDefault(x => x.Id == id));

            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.ProdutosPromocoesLista()).
                Returns(() => GetDadosFake<Promocao>().ToList());


            // Mock do método do serviço
            //Setup - Metodo quer sera mocado
            //Returns - indicar retorno do metodo com dados mocados
            service.Setup(x => x.Salvar(It.IsAny<Promocao>())).
                Returns((Promocao promocao) => {
                    // ao add novo registro o server devolve novo Id
                    if (promocao.Id == 0)
                    {
                        promocao.Id = 999;
                    }
                    return promocao;
                });

            // retorno da nova instancia de Mock do serviço - com os métodos mocados
            return service;
        }


        #endregion

    }
}
