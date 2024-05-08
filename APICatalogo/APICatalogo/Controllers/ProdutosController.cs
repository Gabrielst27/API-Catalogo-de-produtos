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
            
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            return produto;
        }

        //Procurar o primweiro produto
        [HttpGet("primeiro")]
        public ActionResult<Produto> GetFirst()
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault();

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            return produto;
        }

        //Buscar todos os objetos da classe Produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll([FromQuery] int top)
        {
            return await _context.Produtos.AsNoTracking().Take(top).ToListAsync();
        }

        //Inserir novo Produto
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        //Atualizar Produto por id
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Update(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

        //Deletar Produto por id
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
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
    }
}
