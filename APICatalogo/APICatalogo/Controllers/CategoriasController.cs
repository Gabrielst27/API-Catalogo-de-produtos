using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
        {
            _logger = logger;
            _context = context;
        }

        //Procurar Categoria por id
        [HttpGet("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Categoria> GetById(int id)
        {
            throw new ArgumentException("Ocorreu um erro no tratamento do request");
            _logger.LogInformation("======================== Get/Categorias/Id ===========================");
            
            var categoria = _context.Categorias.AsNoTracking()
                .FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }
            return categoria;
        }

        //Procurar a primeira Categoria
        [HttpGet("primeiro")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Categoria> GetFirst()
        {
            _logger.LogInformation("===================== Get/Categorias/Primeiro ========================");
            
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault();

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return categoria;
        }

        //Procurar todos os objetos da classe Categoria
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll([FromQuery] int top)
        {
            _logger.LogInformation("========================= Get/Categorias =============================");
            
            return await _context.Categorias.AsNoTracking().Take(top).ToListAsync();
        }

        //Procurar todas os objetos da classe Categoria e Produto
        [HttpGet("produtos")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCatProd([FromQuery]int topCategoria, [FromQuery]int topProduto)
        {
            _logger.LogInformation("===================== Get/Categorias/Produtos ========================");
            
            return await _context.Categorias.AsNoTracking().Include(p => p.Produtos
                .Take(topProduto)).Take(topCategoria).ToListAsync();
        }

        //Inserir nova Categoria
        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            _logger.LogInformation("========================== Post/Categorias ===========================");
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

        //Atualizar Categoria por id
        [HttpPut("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, [FromBody]Categoria categoria)
        {
            _logger.LogInformation("========================== Put/Categorias ============================");
            
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

            _context.Update(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }

        //Deletar Categoria por id
        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("========================= Delete/Categorias ==========================");
            
            var categoria = _context.Categorias.Find(id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada");
                return NotFound($"Categoria com id={id} não encontrada");
            }

            _context.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
