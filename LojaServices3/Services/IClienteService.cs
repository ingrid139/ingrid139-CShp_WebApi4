using LojaServices3.Api.Models;
using System.Collections.Generic;

namespace LojaServices3.Services
{
    public interface IClienteService
    {
        Cliente ProcurarPorId(int clienteId);
        IList<Cliente> ProcurarPorNome(string nome);
        Cliente Salvar(Cliente cliente);
    }
}