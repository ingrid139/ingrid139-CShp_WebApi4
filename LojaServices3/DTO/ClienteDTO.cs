using System.ComponentModel.DataAnnotations;


namespace LojaServices3.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int EnderecoId { get; set; }

        [Required]
        public EnderecoDTO Endereco { get; set; }
    }
}
