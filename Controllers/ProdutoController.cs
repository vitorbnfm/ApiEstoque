using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly DataContext _context;

        public ProdutoController(DataContext context)
        {
            _context = context;
        }

        //POST: api/produto/create
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {

            _context.Produtos.Add(produto);
            _context.Entry(produto.Categoria).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
        }

        //GET: api/produto/list
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            var produtos = await _context.Produtos.Include(a => a.Categoria)
                .Where(b => b.Ativo == true).OrderByDescending(c => c.ProdutoId).ToListAsync();

            return produtos;
        }

        //GET: api/produto/quantidade/1
        [HttpGet]
        [Route("quantidade/{id}")]

        public ActionResult<int> GetEstoque()
        {

            var estoque = _context.Produtos.Where(c => c.Ativo == true).Sum(a => a.Quantidade);

            return estoque;
        }

        //GET: api/produto/getbyid/1
        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<ActionResult<Produto>> GetProdutoPorCodigo(int id)
        {
            var produto = await _context.Produtos.Where(c => c.ProdutoId == id).FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        //PUT: api/produto/update/1
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> PutProduto(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            _context.Entry(produto.Categoria).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: /api/produto/delete/1
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().Include(e => e.Categoria).Where(c => c.ProdutoId == id).FirstOrDefaultAsync();
            produto.Ativo = false;
            _context.Entry(produto).State = EntityState.Modified;
            _context.Entry(produto.Categoria).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
