using Newtonsoft.Json;

namespace LojaServices3.DTO
{
    public class ProdutoJSONDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("precoUnitario")]
        public decimal PrecoUnitario { get; set; }
    }
}
