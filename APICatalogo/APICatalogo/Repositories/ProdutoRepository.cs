using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Produto GetProduto(int id)
        {
            return _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
        }

        public IEnumerable<Produto> GetProdutos()
        {
            return _context.Produtos.AsNoTracking().ToList();
        }

        public IEnumerable<Produto> GetProdutosCategoria(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            return _context.Produtos.AsNoTracking().Where(p => p.CategoriaId == id);


        }

        public Produto Insert(Produto produto)
        {
            if(produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return produto;
        }

        public Produto Update(Produto produto)
        {
            if(produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return produto;
        }

        public Produto Delete(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            
            if(produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return produto;
        }
    }
}
