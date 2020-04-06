using System.ComponentModel.DataAnnotations;

namespace LojaServices3.DTO
{
    public class PromocaoProdutoDTO
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int PromocaoId { get; set; }
    }
}
