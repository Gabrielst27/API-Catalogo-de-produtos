using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //Buscar objeto da classe Produto pelo id
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        //Buscar todos os objetos da classe Produto
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            return _context.Produtos.ToList();
        }
    }
}
