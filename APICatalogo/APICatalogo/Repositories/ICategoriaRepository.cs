using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
        Categoria GetCategoria(int id);
        IEnumerable<Categoria> GetCategoriasProdutos();
        Categoria Insert(Categoria categoria);
        Categoria Update(Categoria categoria);
        Categoria Delete(int id);
    }
}
