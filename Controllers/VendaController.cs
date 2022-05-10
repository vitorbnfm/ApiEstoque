using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Controllers
{   
    [ApiController]
    [Route("api/venda")]
        public class VendaController : ControllerBase
    {
        private readonly DataContext _context;

        public VendaController(DataContext context)
        {
            _context = context;
        }

        //POST: api/venda/create
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Venda>> PostVenda(Venda venda)
        {
            var produtoId = venda.ProdutoId;
            
            venda.Produto = _context.Produtos.Find(produtoId);
   
            venda.Produto.Quantidade = venda.Produto.Quantidade - venda.QuantidadeVenda;
            var random = new Random();
            venda.Documento = random.Next(000000000,999999999);

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVenda", new { id = venda.VendaId }, venda);
        }


        //GET: api/venda/getbyid/1
        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<ActionResult<Venda>> GetVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }

        //PUT: api/venda/update
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> PutVenda(Venda venda)
        {
            _context.Entry(venda).State = EntityState.Modified;
            _context.Entry(venda.Produto).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        //DELETE: /api/venda/delete/1
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.VendaId == id);
        }
    }
}
