using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) {}

        public IEnumerable<Produto> GetProdutosCategoria(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            return _context.Produtos.AsNoTracking().Where(p => p.CategoriaId == id);
        }
    }
}
