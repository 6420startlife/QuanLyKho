using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(string subject, string content)
        {
            var message = new Message(new string[] { "6420startlife@gmail.com" }, subject, content, null);
            if(message == null)
            {
                return BadRequest();
            }
            await _emailSender.SendEmailAsync(message);
            return NoContent();
        }

        [HttpPost("SendMailWithAttachment")]
        public async Task<IActionResult> SendMailWithAttachment()
        {
            var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
            var message = new Message(new string[] { "n18dccn222@student.ptithcm.edu.vn"}, "Gửi tài liệu", "Đây là tài liệu được gửi từ người dùng",files);
            if (message == null)
            {
                return BadRequest();
            }
            await _emailSender.SendEmailAsync(message);
            return NoContent();
        }

        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendMailWithAttachment(string email)
        {
            var OTP_value = new Random().Next(100000, 999999);
            var message = new Message(new string[] { email }, "Xác thực OTP", "Mã xác thực của bạn là : " + OTP_value, null);
            if (message == null)
            {
                return BadRequest();
            }
            await _emailSender.SendEmailAsync(message);
            return Ok(OTP_value);
        }
    }
}
