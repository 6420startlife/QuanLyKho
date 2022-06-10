using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FoodStore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models;

namespace QuanLyKho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapController : ControllerBase
    {
        private readonly QuanLyKhoContext _context;

        public PhieuNhapController(QuanLyKhoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetPhieuNhap()
        {
          if (_context.PhieuNhap == null)
          {
              return NotFound();
          }
          return Ok(await _context.PhieuNhap.Select(e => new {e.soPhieu, ngayLap = e.ngayLap.ToString("dd/MM/yyyy"), e.maKho}).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuNhap>> GetPhieuNhap(int id)
        {
          if (_context.PhieuNhap == null)
          {
              return NotFound();
          }
            var phieuNhap = await _context.PhieuNhap.FindAsync(id);

            if (phieuNhap == null)
            {
                return NotFound();
            }

            return phieuNhap;
        }

        [HttpPut]
        public async Task<IActionResult> PutPhieuNhap(int soPhieu, PhieuNhap_Model model)
        {
            if (!isExistKho(model.maKho))
            {
                return BadRequest();
            }
            var phieuNhap = _context.PhieuNhap.SingleOrDefault(e => e.soPhieu == soPhieu);
            if (phieuNhap == null)
            {
                return NotFound();
            }
            phieuNhap.ngayLap = DateTime.ParseExact(model.ngayLap, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            phieuNhap.maKho = model.maKho;
            _context.PhieuNhap.Update(phieuNhap);

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
        public async Task<ActionResult<PhieuNhap>> PostPhieuNhap(PhieuNhap_Model model)
        {
            if (!isExistKho(model.maKho))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Mã kho không tồn tại",
                    Data = null
                });
            }
            var newPhieuNhap = new PhieuNhap
            {
                ngayLap = DateTime.ParseExact(model.ngayLap, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                maKho = model.maKho
            };
            _context.PhieuNhap.Add(newPhieuNhap);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Thêm phiếu nhập thành công",
                    Data = newPhieuNhap.soPhieu
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi lưu phiếu nhập",
                    Data = ex.Message
                });
            }
        }

        private bool isExistKho(string maKho)
        {
            return _context.Kho.Any(e => e.maKho == maKho);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhieuNhap(int id)
        {
            if (_context.PhieuNhap == null)
            {
                return NotFound();
            }
            var phieuNhap = await _context.PhieuNhap.FindAsync(id);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            _context.PhieuNhap.Remove(phieuNhap);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
