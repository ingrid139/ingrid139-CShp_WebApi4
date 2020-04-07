using LojaServices3.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Controller.Tests.Comparacoes
{
    public class CompraDTOIdComparer : IEqualityComparer<CompraDTO>
    {
        public bool Equals(CompraDTO x, CompraDTO y)
        {
            return x.Id == y.Id &&
                    x.ClienteId == y.ClienteId &&
                    x.ProdutoId == y.ProdutoId;
        }

        public int GetHashCode(CompraDTO obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
