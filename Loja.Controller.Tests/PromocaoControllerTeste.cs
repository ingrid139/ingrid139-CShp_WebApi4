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
    public class PromocaoControllerTeste
    {
        private readonly ServicesFake _servicesFake;

        public PromocaoControllerTeste()
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
            var fakeService = _servicesFake.FakePromocaoService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<PromocaoDTO>(fakeService.ProcurarPorId(id));

            // nova instancia da controller
            var controller = new PromocaoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Get(id);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as PromocaoDTO;

            //comparar tipo do retorno
            Assert.IsType<PromocaoDTO>(actual);

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new PromocaoDTOComparer());
        }


        [Fact]
        public void Devera_Retornar_Ok_Quando_GetAll()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakePromocaoService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<List<PromocaoDTO>>(fakeService.ProdutosPromocoesLista());

            // nova instancia da controller
            var controller = new PromocaoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.GetAll();

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as List<PromocaoDTO>;

            //comparar tipo do retorno
            Assert.IsType<List<PromocaoDTO>>(actual);

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Add_Post()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakePromocaoService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<PromocaoDTO>().First();
            expected.Id = 0;

            // nova instancia da controller
            var controller = new PromocaoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Post(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as PromocaoDTO;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(999, actual.Id);
            Assert.Equal(expected.Descricao, actual.Descricao);
            Assert.Equal(expected.DataInicio, actual.DataInicio);
            Assert.Equal(expected.DataTermino, actual.DataTermino);
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Put()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakePromocaoService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<PromocaoDTO>().First();

            // nova instancia da controller
            var controller = new PromocaoController(fakeService, _servicesFake.Mapper);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Put(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as PromocaoDTO;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Descricao, actual.Descricao);
            Assert.Equal(expected.DataInicio, actual.DataInicio);
            Assert.Equal(expected.DataTermino, actual.DataTermino);
        }
    }
}
