using Loja.Controller.Tests.Comparacoes;
using LojaServices3.Controllers;
using LojaServices3.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Loja.Controller.Tests
{
    public class ClienteControllerTeste
    {
        private readonly ServicesFake _servicesFake;

        public ClienteControllerTeste()
        {
            // nova instancia do serviço mocado
            _servicesFake = new ServicesFake();
        }

        [Fact]
        public void Devera_Retornar_Ok_Quando_Get_Por_Id()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeClienteService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<ClienteDTO>(fakeService.ProcurarPorId(1));

            // nova instancia da controller
            var controller = new ClienteController(fakeService);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Get(1);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);
            
            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ClienteDTO;
            
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new ClienteDTOIdComparer());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Devera_Retornar_Ok_Quando_Get_Por_Ids(int id)
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeClienteService().Object;
            // recuperar dado mocado
            var expected = _servicesFake.Mapper.Map<ClienteDTO>(fakeService.ProcurarPorId(id));

            // nova instancia da controller
            var controller = new ClienteController(fakeService);

            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Get(id);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ClienteDTO;

            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);
            // comparar retorno com esperado
            Assert.Equal(expected, actual, new ClienteDTOIdComparer());
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Add_Post()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeClienteService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<ClienteDTO>().First();
            expected.Endereco = _servicesFake.GetDadosFake<EnderecoDTO>().Where(x => x.Id == expected.EnderecoId).FirstOrDefault();
            expected.Id = 0;

            // nova instancia da controller
            var controller = new ClienteController(fakeService);
            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Post(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);
            
            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ClienteDTO;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(999, actual.Id);
            Assert.Equal(expected.Nome, actual.Nome);
            Assert.Equal(998, actual.EnderecoId);
        }

        [Fact]
        public void Devera_Retornar_OK_Quando_Aletrar_Put()
        {
            //arranje
            //nova instancia de service
            var fakeService = _servicesFake.FakeClienteService().Object;
            // recuperar dado DTO mocado
            var expected = _servicesFake.GetDadosFake<ClienteDTO>().First();
            expected.Endereco = _servicesFake.GetDadosFake<EnderecoDTO>().Where(x => x.Id == expected.EnderecoId).FirstOrDefault();

            // nova instancia da controller
            var controller = new ClienteController(fakeService);
            //act
            //metodo em testes - metodo da requisição da controller
            var result = controller.Put(expected);

            //assert
            // comparar o tipo do status de retorno
            Assert.IsType<OkObjectResult>(result.Result);

            // casting valor do retorno para DTO
            var actual = (result.Result as OkObjectResult).Value as ClienteDTO;
            //comparar valor retorno controller é nulo
            Assert.NotNull(actual);

            // comparar retorno com esperado inserido no retorno dos metodo fake
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Nome, actual.Nome);
            Assert.Equal(expected.EnderecoId, actual.EnderecoId);
        }
    }
}
