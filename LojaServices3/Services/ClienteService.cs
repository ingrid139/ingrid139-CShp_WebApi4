using LojaServices3.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaServices3.Services
{
    public class ClienteService : IClienteService
    {
        private LojaContexto _context;

        public ClienteService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Cliente ProcurarPorId(int clienteId)
        {
            //utilzar metodo Find
            return _context.Clientes.Find(clienteId);
        }

        public IList<Cliente> ProcurarPorNome(string nome)
        {
            //utilizar método Where
            return _context.Clientes.Where(x => x.Nome == nome).ToList();
        }

        public Cliente Salvar(Cliente cliente)
        {
            //var estado = cliente.Id == 0 ? EntityState.Added : EntityState.Modified;

            //_context.Entry(cliente).State = estado;

            //var existe = _context.Clientes.Find(cliente.Id);

            var existe = _context.Clientes
                                .Include(e => e.EnderecoDeEntrega)
                                .Where(x => x.Id == cliente.Id)
                                .FirstOrDefault();

            if (existe == null)
                _context.Clientes.Add(cliente);
            else
            {
                existe.Nome = cliente.Nome;
                existe.EnderecoDeEntrega.Logradouro = cliente.EnderecoDeEntrega.Logradouro;
            }

            _context.SaveChanges();

            return cliente;
        }
    }
}
