using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessageService messageService, ILogger<MessagesController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] Message message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message.Text))
                {
                    return BadRequest(new { error = "Текст сообщения не может быть пустым" });
                }
                var response = _messageService.ProcessMessage(message);
                SaveMessageToFile(message, response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке сообщения");
                return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
            }
        }
        private void SaveMessageToFile(Message message, MessageResponse response)
        {
            try
            {
                var logEntry =$"Текст: {message.Text}, " +
                      $"Получено: {response.ReceivedAt:yyyy-MM-dd HH:mm:ss}" +
                      Environment.NewLine;

                System.IO.File.AppendAllText("data.txt", logEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении в файл");
            }
        }
        [HttpGet]
        public IActionResult GetAllMessages()
        {
            try
            {
                if (!System.IO.File.Exists("data.txt")) 
                {
                    return Ok(new { content = "Файл data.txt пуст или не существует", isEmpty = true });
                }
                var fileContent = System.IO.File.ReadAllText("data.txt"); 
                return Ok(new { 
                    message = fileContent
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
            }
        }
    } 
}
