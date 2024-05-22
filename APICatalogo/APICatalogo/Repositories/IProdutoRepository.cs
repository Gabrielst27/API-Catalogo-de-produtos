using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface IProdutoRepository
    {
        public IEnumerable<Produto> GetProdutos();
        public Produto GetProduto(int id);
        public Produto Insert(Produto produto);
        public Produto Update(Produto produto);
        public Produto Delete(int id);
    }
}
