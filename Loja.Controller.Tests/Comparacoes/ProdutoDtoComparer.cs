using LojaServices3.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Controller.Tests.Comparacoes
{
    public class ProdutoDtoComparer : IEqualityComparer<ProdutoDto>
    {
        public bool Equals(ProdutoDto x, ProdutoDto y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ProdutoDto obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
