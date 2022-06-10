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
    public class KhoController : ControllerBase
    {
        private readonly QuanLyKhoContext _context;

        public KhoController(QuanLyKhoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetKho(string? maKho, string? sort)
        {
            if (_context.Kho == null)
            {
                return NotFound();
            }
            if (sort == "asc")
            {
                return Ok(await _context.Kho.Select(e => new { e.maKho, e.tenKho }).OrderBy(e => e.maKho).ToListAsync());
            }
            if (sort == "desc")
            {
                return Ok(await _context.Kho.Select(e => new { e.maKho, e.tenKho }).OrderByDescending(e => e.maKho).ToListAsync());
            }
            if ( maKho == null)
            {
                return Ok(await _context.Kho.Select(e => new { e.maKho, e.tenKho }).ToListAsync());
            }
            if (!isExistKho(maKho))
            {
                return NotFound();
            }
            return Ok(await _context.Kho.Where(e => e.maKho == maKho).Select(e => new { e.maKho, e.tenKho }).ToListAsync());
        }

        [HttpGet("spinner")]
        public async Task<ActionResult> GetKhoSpinner()
        {
            var returnList = new List<string>();
            var list = await _context.Kho.Select(e => new { e.maKho, e.tenKho }).ToListAsync();
            foreach (var item in list)
            {
                returnList.Add(item.maKho + " - " + item.tenKho);
            }
            return Ok(returnList);
        }

        [HttpPut]
        public async Task<IActionResult> PutKho(Kho_Model model)
        {
            var kho = _context.Kho.SingleOrDefault(e => e.maKho == model.maKho);
            if(kho == null)
            {
                return NotFound();
            }
            kho.tenKho = model.tenKho;
            _context.Kho.Update(kho);

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
        public async Task<ActionResult<Kho>> PostKho(Kho_Model kho)
        {
            if (isExistKho(kho.maKho))
            {
                return BadRequest();
            }
            var newKho = new Kho
            {
                maKho = kho.maKho,
                tenKho = kho.tenKho
            };
            _context.Kho.Add(newKho);
            try
            {
                _context.SaveChanges();
                return NoContent();
            }catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool isExistKho(string maKho)
        {
            return _context.Kho.Any(e => e.maKho == maKho);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKho(string id)
        {
            if (_context.Kho == null)
            {
                return NotFound();
            }
            var kho = await _context.Kho.FindAsync(id);
            if (kho == null)
            {
                return NotFound();
            }

            _context.Kho.Remove(kho);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
