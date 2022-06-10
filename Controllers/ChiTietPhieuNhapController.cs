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
    public class ChiTietPhieuNhapController : ControllerBase
    {
        private readonly QuanLyKhoContext _context;

        public ChiTietPhieuNhapController(QuanLyKhoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetChiTietPhieuNhap()
        {
          if (_context.ChiTietPhieuNhap == null)
          {
              return NotFound();
          }
          return Ok(await _context.ChiTietPhieuNhap.Select(e => new {e.soPhieu, e.maVatTu, e.soLuong}).ToListAsync());
        }

        [HttpGet("{soPhieu}")]
        public async Task<ActionResult> GetChiTietPhieuNhap(int soPhieu)
        {
            return Ok(await _context.ChiTietPhieuNhap.Where(e => e.soPhieu == soPhieu).Select(e => new { e.soPhieu, e.maVatTu, e.soLuong }).ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> PutChiTietPhieuNhap(ChiTietPhieuNhap_Model model)
        {
            var chiTietPhieuNhap = await _context.ChiTietPhieuNhap.SingleOrDefaultAsync(e => e.soPhieu == model.soPhieu && e.maVatTu == model.maVatTu);
            if (chiTietPhieuNhap == null)
            {
                return NotFound();
            }
            chiTietPhieuNhap.soLuong = model.soLuong;
            _context.ChiTietPhieuNhap.Update(chiTietPhieuNhap);

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

        private bool isExistPhieuNhap(int soPhieu)
        {
            return _context.PhieuNhap.Any(e => e.soPhieu == soPhieu);
        }

        [HttpPost]
        public async Task<ActionResult<ChiTietPhieuNhap>> PostChiTietPhieuNhap(ChiTietPhieuNhap_Model model)
        {
            if (!isExistPhieuNhap(model.soPhieu))
            {
                return NotFound();
            }
            var newChiTietPhieuNhap = new ChiTietPhieuNhap
            {
                soPhieu = model.soPhieu,
                maVatTu = model.maVatTu,
                soLuong = model.soLuong
            };
            _context.ChiTietPhieuNhap.Add(newChiTietPhieuNhap);
            try
            {
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{soPhieu}/{maVatTu}")]
        public async Task<IActionResult> DeleteChiTietPhieuNhap(int soPhieu, string maVatTu)
        {
            if (_context.ChiTietPhieuNhap == null)
            {
                return NotFound();
            }
            var chiTietPhieuNhap = await _context.ChiTietPhieuNhap.SingleOrDefaultAsync(e => e.soPhieu == soPhieu && e.maVatTu.Equals(maVatTu));
            if (chiTietPhieuNhap == null)
            {
                return NotFound();
            }

            _context.ChiTietPhieuNhap.Remove(chiTietPhieuNhap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool isExistChiTietPhieuNhap(int soPhieu, string maVatTu)
        {
            return (_context.ChiTietPhieuNhap?.Any(e => e.soPhieu == soPhieu && e.maVatTu == maVatTu)).GetValueOrDefault();
        }
    }
}
