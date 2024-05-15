using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        //Procurar Categoria por id
        [HttpGet("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Categoria> GetById(int id)
        {
            _logger.LogInformation("======================== Get/Categorias/Id ===========================");

            var categoria = _repository.GetCategoria(id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }
            return Ok(categoria);
        }

        //Procurar todos os objetos da classe Categoria
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> GetAll([FromQuery] int top)
        {
            _logger.LogInformation("========================= Get/Categorias =============================");

            var categorias = _repository.GetCategorias();
            return Ok(categorias);
        }

        //Procurar todas os objetos da classe Categoria e Produto
        [HttpGet("produtos")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> GetCatProd()
        {
            _logger.LogInformation("===================== Get/Categorias/Produtos ========================");

            var categoriasProdutos = _repository.GetCategoriasProdutos();
            return Ok(categoriasProdutos);
        }

        //Inserir nova Categoria
        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            _logger.LogInformation("========================== Post/Categorias ===========================");
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Dados inválidos");
                return BadRequest(ModelState);
            }

            _repository.Insert(categoria);

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
                _logger.LogWarning($"Dados inválidos");
                return BadRequest("Dados inválidos");
            }

            var categoriaAntiga = _repository.GetCategoria(id);
            _repository.Update(categoria);

            return Ok(categoriaAntiga);
        }

        //Deletar Categoria por id
        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("========================= Delete/Categorias ==========================");

            var categoria = _repository.GetCategoria(id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada");
                return NotFound($"Categoria com id={id} não encontrada");
            }

            var categoriaDeletada = _repository.Delete(id);

            return Ok(categoriaDeletada);
        }
    }
}
