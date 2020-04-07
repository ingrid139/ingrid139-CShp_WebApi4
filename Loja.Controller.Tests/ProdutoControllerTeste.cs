using Loja.Controller.Tests.Comparacoes;
using LojaServices3.Controllers;
using LojaServices3.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Loja.Controller.Tests
{
    public class ProdutoControllerTeste
    {
        private readonly ServicesFake _servicesFake;

        public ProdutoControllerTeste()
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
            var fakeService = _servicesFake.FakeProdutoService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<ProdutoDto>(fakeService.ProcurarPorId(id));

            // nova instancia da controller
            var controller = new ProdutoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Get(id);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ProdutoDto;

            //comparar tipo do retorno
            Assert.IsType<ProdutoDto>(actual);

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new ProdutoDtoComparer());
        }

        [Fact]
        public void Devera_Retornar_Ok_Quando_Get_Lista()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeProdutoService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<List<ProdutoDto>>(fakeService.ListarProdutos());

            // nova instancia da controller
            var controller = new ProdutoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Get();

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as List<ProdutoDto>;

            //comparar valor retorno controller é nulo
            Assert.IsType<List<ProdutoDto>>(actual);
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Add_Post()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeProdutoService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<ProdutoDto>().First();
            expected.Id = 0;

            // nova instancia da controller
            var controller = new ProdutoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Post(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ProdutoDto;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(999, actual.Id);
            Assert.Equal(expected.Nome, actual.Nome);
            Assert.Equal(expected.PrecoUnitario, actual.PrecoUnitario);
            Assert.Equal(expected.Categoria, actual.Categoria);
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Alterar_Put()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeProdutoService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<ProdutoDto>().First();

            // nova instancia da controller
            var controller = new ProdutoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Put(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ProdutoDto;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Nome, actual.Nome);
            Assert.Equal(expected.PrecoUnitario, actual.PrecoUnitario);
            Assert.Equal(expected.Categoria, actual.Categoria);
        }
    }
}
