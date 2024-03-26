using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        //Procurar Categoria por id
        [HttpGet("{id:int}")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria is null)
            {
                return NotFound();
            }

            return categoria;
        }

        //Procurar todos os objetos da classe Categoria
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAll()
        {
            return _context.Categorias.ToList();
        }

        //Procurar todas os objetos da classe Categoria e Produto
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCatProd()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        //Inserir nova Categoria
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

        //Atualizar Categoria por id
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Update(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

        //Deletar Categoria por id
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            _context.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
