using AutoMapper;
using LojaServices3.Api.Models;
using LojaServices3.DTO;

namespace LojaServices3
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Compra, CompraDTO>().ReverseMap();
            CreateMap<PromocaoProduto, PromocaoProdutoDTO>().ReverseMap();
            CreateMap<Promocao, PromocaoDTO>()
                .ForMember(dest => dest.PromocaoProdutos, opt => opt.MapFrom(src => src.Produtos))
                .ReverseMap();
        }
    }
}
