using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalogo.Models;
using ApiCatalogo.Services.Interfaces;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IRepositoryServices _context;

        public ProdutosController(IRepositoryServices context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
          if (_context.ProdutoRepository == null)
          {
              return NotFound("Produtos não encontrados!");
          }
            return await _context.ProdutoRepository.Get().AsNoTracking().ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public ActionResult<Produto> GetProduto(int id)
        {
          if (_context.ProdutoRepository == null)
          {
              return NotFound("Não há produdos cadastrados!");
          }
            var produto = _context.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado!");
            }

            return produto;
        }

        // GET: api/Produtos/PrecoOrdemCrescente
        [HttpGet("PrecoOrdemCrescente")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorPreco(int id)
        {
            if (_context.ProdutoRepository == null)
            {
                return NotFound("Não há produdos cadastrados!");
            }
            var produtos = _context.ProdutoRepository.GetProdutosPorPreco().ToList();

            if (produtos == null)
            {
                return NotFound("Produtos não encontrados!");
            }

            return produtos;
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Id do produto não existe!");
            }

            _context.ProdutoRepository.Update(produto);

            try
            {
                _context.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound("Id do produto não existe!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produtos
        [HttpPost]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
          if (_context.ProdutoRepository == null)
          {
              return Problem("Entity set 'AppDbContext.Produtos'  is null.");
          }
            _context.ProdutoRepository.Add(produto);
            _context.Commit();

            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            if (_context.ProdutoRepository == null)
            {
                return NotFound("Não existem produtos!");
            }
            var produto = _context.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound("Id do produto não existe!");
            }

            _context.ProdutoRepository.Delete(produto);
            _context.Commit();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            var produto = _context.ProdutoRepository.Get().AsNoTracking();
            return produto.Any(e => e.ProdutoId == id);
        }
    }
}
