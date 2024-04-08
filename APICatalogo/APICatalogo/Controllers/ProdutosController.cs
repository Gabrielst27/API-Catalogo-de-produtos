using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //Buscar objeto da classe Produto por id
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> GetById(int id)
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound("Produto não encontrado");
                }

                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Procurar o primweiro produto
        [HttpGet("primeiro")]
        public ActionResult<Produto> GetFirst()
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault();

                if (produto is null)
                {
                    return NotFound("Produto não encontrado");
                }

                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte");
            }
        }

        //Buscar todos os objetos da classe Produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll([FromQuery] int top)
        {
            try
            {
                return await _context.Produtos.AsNoTracking().Take(top).ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Inserir novo Produto
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest();
                }

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Atualizar Produto por id
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                _context.Update(produto);
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Deletar Produto por id
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.Find(id);

                if (produto is null)
                {
                    return NotFound("Produto não encontrado");
                }

                _context.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }
    }
}
