using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models;

namespace QuanLyKho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatTuController : ControllerBase
    {
        private readonly QuanLyKhoContext _context;

        public VatTuController(QuanLyKhoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetVatTu()
        {
          if (_context.VatTu == null)
          {
              return NotFound();
          }
          return Ok(await _context.VatTu.Select(e => new {e.maVatTu, e.tenVatTu,e.anhVatTu,e.donViTinh, e.xuatXu}).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VatTu>> GetVatTu(string id)
        {
          if (_context.VatTu == null)
          {
              return NotFound();
          }
            var vatTu = await _context.VatTu.FindAsync(id);

            if (vatTu == null)
            {
                return NotFound();
            }

            return vatTu;
        }

        [HttpGet("spinner")]
        public async Task<ActionResult> GetVatTuSpinner()
        {
            var returnList = new List<string>();
            var list = await _context.VatTu.Select(e => new { e.maVatTu, e.tenVatTu }).ToListAsync();
            foreach (var item in list)
            {
                returnList.Add(item.maVatTu + " - " + item.tenVatTu);
            }
            return Ok(returnList);
        }

        [HttpPut]
        public async Task<IActionResult> PutVatTu(VatTu_Model model)
        {
            var vatTu = _context.VatTu.SingleOrDefault(e => e.maVatTu == model.maVatTu);
            if (vatTu == null)
            {
                return NotFound();
            }
            vatTu.tenVatTu = model.tenVatTu;
            vatTu.anhVatTu = model.anhVatTu;
            vatTu.donViTinh = model.donViTinh;
            vatTu.xuatXu = model.xuatXu;
            _context.VatTu.Update(vatTu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VatTu>> PostVatTu(VatTu_Model model)
        {
            if (isExistVatTu(model.maVatTu))
            {
                return BadRequest();
            }
            var newVatTu = new VatTu
            {
                maVatTu = model.maVatTu,
                tenVatTu = model.tenVatTu,
                anhVatTu = model.anhVatTu,
                donViTinh = model.donViTinh,
                xuatXu = model.xuatXu
            };

            await _context.VatTu.AddAsync(newVatTu);
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVatTu(string id)
        {
            if (_context.VatTu == null)
            {
                return NotFound();
            }
            var vatTu = await _context.VatTu.FindAsync(id);
            if (vatTu == null)
            {
                return NotFound();
            }

            _context.VatTu.Remove(vatTu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool isExistVatTu(string id)
        {
            return _context.VatTu.Any(e => e.maVatTu == id);
        }
    }
}
