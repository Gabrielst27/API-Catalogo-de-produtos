using APICatalogo.Validations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace APICatalogo.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome deve conter entre 2 e 100 caracteres",
            MinimumLength = 2)]
        [PrimeiraMaiuscula]
        public string? Nome { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "O caminho da imagem deve ter entre 10 e 500 caracteres",
            MinimumLength = 5)]
        public string? ImgUrl { get; set; }

        public ICollection<Produto>? Produtos { get; set; }

    }
}
