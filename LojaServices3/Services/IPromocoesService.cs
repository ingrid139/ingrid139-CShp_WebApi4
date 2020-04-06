using LojaServices3.Api.Models;
using System.Collections.Generic;

namespace LojaServices3.Services
{
    public interface IPromocoesService
    {
        Promocao ProcurarPorId(int promocaoId);
        IList<Promocao> ProdutosPorPromocaoId(int promocaoId);
        Promocao ProdutosPromocoes();
        IList<Promocao> ProdutosPromocoesLista();
        Promocao Salvar(Promocao produto);
    }
}