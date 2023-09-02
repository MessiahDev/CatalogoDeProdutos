using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalogo.Models;
using ApiCatalogo.Services.Interfaces;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IRepositoryServices _context;

        public CategoriasController(IRepositoryServices context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
          if (_context.CategoriaRepository == null)
          {
              return NotFound("Não existem categorias!");
          }
            return await _context.CategoriaRepository.Get().AsNoTracking().ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            if (_context.CategoriaRepository == null)
            {
                return NotFound("Não há categorias cadastradas!");
            }
            var categoria = _context.CategoriaRepository.GetById(p => p.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada!");
            }

            return categoria;
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public IActionResult PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Id da categoria não existe!");
            }

            _context.CategoriaRepository.Update(categoria);

            try
            {
                _context.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound("Id da categoria não existe!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        [HttpPost]
        public ActionResult<Categoria> PostCategoria(Categoria categoria)
        {
            if (_context.CategoriaRepository == null)
            {
                return Problem("Entity set 'AppDbContext.Categorias'  is null.");
            }
            _context.CategoriaRepository.Add(categoria);
            _context.Commit();

            return CreatedAtAction("GetCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id)
        {
            if (_context.CategoriaRepository == null)
            {
                return NotFound("Id da categoria não existe!");
            }
            var categoria = _context.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound("Id da categoria não existe!");
            }

            _context.CategoriaRepository.Delete(categoria);
            _context.Commit();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            var categoria = _context.CategoriaRepository.Get().AsNoTracking();
            return (categoria.Any(e => e.CategoriaId == id));
        }
    }
}
