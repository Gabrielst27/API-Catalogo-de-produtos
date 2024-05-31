using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IProdutoRepository repository, ILogger<ProdutosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> GetById(int id)
        {
            _logger.LogInformation("========================= Get/Produtos/Id ============================");

            var produto = _repository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                _logger.LogWarning($"Produto com id={id} não encontrado");
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            _logger.LogInformation("========================== Get/Produtos ==============================");

            var produtos = _repository.GetAll();

            return Ok(produtos);
        }

        [HttpGet("categorias")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> GetProdCat(int id)
        {
            _logger.LogInformation("===================== Get/Produtos/Categoria =========================");

            var produtos = _repository.GetProdutosCategoria(id);

            return Ok(produtos);
        }


        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> Post([FromBody] Produto produto)
        {
            _logger.LogInformation("========================== Post/Produtos =============================");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dados inválidos");
                return BadRequest(ModelState);
            }

            _repository.Create(produto);

            return Ok(produto);

        }

        [HttpPut]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> Put([FromBody] Produto produto)
        {
            _logger.LogInformation("=========================== Put/Produtos =============================");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Dados inválidos");
                return BadRequest(ModelState);
            }

            var produtoAntigo = _repository.Get(p => p.ProdutoId == produto.ProdutoId);

            _repository.Update(produto);

            return Ok($"Dados antigos: {produtoAntigo}; Dados atualizados: {produto}");
        }

        [HttpDelete]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> Delete(int id)
        {
            _logger.LogInformation("========================== Delete/Produtos ===========================");

            var produto = _repository.Get(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            _repository.Delete(produto);

            return Ok($"Dados excluídos: {produto.ToString}");
        }
    }
}
