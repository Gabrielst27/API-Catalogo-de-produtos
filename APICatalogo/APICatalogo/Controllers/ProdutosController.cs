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
        private readonly ILogger _logger;

        public ProdutosController(IProdutoRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> GetById(int id)
        {
            _logger.LogInformation("========================= Get/Produtos/Id ============================");

            var produto = _repository.GetProduto(id);

            if (produto is null)
            {
                return NotFound("Produto não encontrada");
            }

            return Ok(produto);
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            _logger.LogInformation("========================== Get/Produtos ==============================");

            var produtos = _repository.GetProdutos();

            return Ok(produtos);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> Post([FromBody] Produto produto)
        {
            _logger.LogInformation("========================== Post/Produtos =============================");

            var prod = _repository.Insert(produto);

            return Ok(produto);

        }

        [HttpPut]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> Put([FromBody] Produto produto)
        {
            _logger.LogInformation("=========================== Put/Produtos =============================");

            var prod = _repository.Update(produto);

            return Ok(prod);
        }
    }
}
