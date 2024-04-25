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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public ActionResult<Categoria> GetById(int id)
        {
            _logger.LogInformation("======================== Get/Categorias/Id ===========================");
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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public ActionResult<Categoria> GetFirst()
        {
            _logger.LogInformation("===================== Get/Categorias/Primeiro ========================");
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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll([FromQuery] int top)
        {
            _logger.LogInformation("========================= Get/Categorias =============================");
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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCatProd([FromQuery]int topCategoria, [FromQuery]int topProduto)
        {
            _logger.LogInformation("===================== Get/Categorias/Produtos ========================");
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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            _logger.LogInformation("========================== Post/Categorias ===========================");
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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public ActionResult Put(int id, [FromBody]Categoria categoria)
        {
            _logger.LogInformation("========================== Put/Categorias ============================");
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
        [ServiceFilter(typeof(ApiLoggingFilters))]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("========================= Delete/Categorias ==========================");
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
