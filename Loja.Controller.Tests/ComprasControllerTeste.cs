using Loja.Controller.Tests.Comparacoes;
using LojaServices3.Controllers;
using LojaServices3.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Loja.Controller.Tests
{
    public class ComprasControllerTeste
    {

        private readonly ServicesFake _servicesFake;

        public ComprasControllerTeste()
        {
            // nova instancia do serviço mocado
            _servicesFake = new ServicesFake();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Devera_Retornar_Ok_Quando_Get_Por_Id(int id)
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeCompraService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<CompraDTO>(fakeService.ProcurarPorId(id));

            // nova instancia da controller
            var controller = new ComprasController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Get(id);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as CompraDTO;

            //comparar tipo do retorno
            Assert.IsType<CompraDTO>(actual);

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new CompraDTOIdComparer());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Devera_Retornar_Ok_Quando_Procurar_Por_Cliente(int clienteid)
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeCompraService().Object;
            // recuperar dado mocado
            var expected =  _servicesFake.Mapper.Map<List<CompraDTO>>(fakeService.ProcurarPorClienteId(clienteid));

            // nova instancia da controller
            var controller = new ComprasController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.GetAll(clienteId: clienteid);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as List<CompraDTO>;

            //comparar tipo do retorno
            Assert.IsType<List<CompraDTO>>(actual);

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new CompraDTOIdComparer());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Devera_Retornar_Ok_Quando_Procurar_Por_Produto(int produtoid)
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeCompraService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<List<CompraDTO>>(fakeService.ProcurarPorProduto(produtoid));

            // nova instancia da controller
            var controller = new ComprasController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.GetAll(produtoId: produtoid);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as List<CompraDTO>;

            //comparar tipo do retorno
            Assert.IsType<List<CompraDTO>>(actual);

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new CompraDTOIdComparer());
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Add_Post()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeCompraService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<CompraDTO>().First();
            expected.Id = 0;

            // nova instancia da controller
            var controller = new ComprasController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Post(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as CompraDTO;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(999, actual.Id);
            Assert.Equal(expected.ClienteId, actual.ClienteId);
            Assert.Equal(expected.ProdutoId, actual.ProdutoId);
            Assert.Equal(expected.Quantidade, actual.Quantidade);
            Assert.Equal(expected.Preco, actual.Preco);
        }
    }
}
