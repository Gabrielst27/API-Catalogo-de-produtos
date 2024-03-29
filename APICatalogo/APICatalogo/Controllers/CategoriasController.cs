﻿using APICatalogo.Context;
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

        //Procurar todos os objetos da classe Categoria
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAll(int top)
        {
            try
            {
                return _context.Categorias.AsNoTracking().Take(top).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Procurar todas os objetos da classe Categoria e Produto
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCatProd(int topCategoria, int topProduto)
        {
            try
            {
                return _context.Categorias.AsNoTracking().Include(p => p.Produtos
                    .Take(topProduto)).Take(topCategoria).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro no tratamento da sua solicitação. Contate o suporte.");
            }
        }

        //Inserir nova Categoria
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest("Dados inválidos");
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
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
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
        [HttpDelete("{id:int}")]
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
