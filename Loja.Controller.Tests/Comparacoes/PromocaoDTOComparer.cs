using LojaServices3.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Controller.Tests.Comparacoes
{
    public class PromocaoDTOComparer : IEqualityComparer<PromocaoDTO>
    {
        public bool Equals(PromocaoDTO x, PromocaoDTO y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(PromocaoDTO obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
