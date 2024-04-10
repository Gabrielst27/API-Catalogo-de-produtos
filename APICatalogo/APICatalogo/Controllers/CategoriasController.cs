using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        //Procurar Categoria por id
        [HttpGet("{id:int:min(1)}")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking()
                    .FirstOrDefault(p => p.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Procurar a primeira Categoria
        [HttpGet("primeiro")]
        public ActionResult<Categoria> GetFirst()
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault();

                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada");
                }

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento de sua solicitação. Contate o suporte");
            }
        }

        //Procurar todos os objetos da classe Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll([FromQuery] int top)
        {
            try
            {
                return await _context.Categorias.AsNoTracking().Take(top).ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Procurar todas os objetos da classe Categoria e Produto
        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCatProd([FromQuery]int topCategoria, [FromQuery]int topProduto)
        {
            try
            {
                return await _context.Categorias.AsNoTracking().Include(p => p.Produtos
                    .Take(topProduto)).Take(topCategoria).ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Inserir nova Categoria
        [HttpPost]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Atualizar Categoria por id
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody]Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Dados inválidos");
                }

                _context.Update(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Deletar Categoria por id
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }
    }
}
