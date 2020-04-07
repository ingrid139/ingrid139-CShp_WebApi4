using LojaServices3.DTO;
using System.Collections.Generic;

namespace Loja.Controller.Tests.Comparacoes
{
    public class ClienteDTOIdComparer : IEqualityComparer<ClienteDTO>
    {
        public bool Equals(ClienteDTO x, ClienteDTO y)
        {
            return x.Id == y.Id && x.EnderecoId == y.EnderecoId;
        }

        public int GetHashCode(ClienteDTO obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
