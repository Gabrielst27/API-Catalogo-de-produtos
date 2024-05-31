using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres",
            MinimumLength = 2)]
        [PrimeiraMaiuscula]
        public string? Nome { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "A descrição deve ter entre 10 e 200 caracteres",
            MinimumLength = 10)]
        [PrimeiraMaiuscula]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        [Range(0.1, 100000, ErrorMessage = "o preço deve estar entre {1} e {2}")]
        public decimal? Preco { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "O caminho deve ter entre 10 e 500 caracteres",
            MinimumLength = 5)]
        public string? ImgUrl { get; set; }

        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

    }
}
