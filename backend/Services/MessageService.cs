using backend.Models;

namespace backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly List<Message> _messages = new();
        private int _counter = 1;

        public MessageResponse ProcessMessage(Message message)
        {
            // Генерируем ID для сообщения
            message.Id = $"MSG_{_counter++}_{DateTime.Now:yyyyMMddHHmmss}";
            message.Timestamp = DateTime.Now;

            // Сохраняем сообщение
            _messages.Add(message);

            // Создаем ответ
            var response = new MessageResponse
            {
                Message = $"Сообщение получено: '{message.Text}'",
                Id = message.Id,
                ReceivedAt = DateTime.Now,
                Length = message.Text?.Length ?? 0
            };

            // Имитация обработки
            Thread.Sleep(500); // Задержка 0.5 секунды для имитации обработки

            return response;
        }

        public List<Message> GetMessages()
        {
            return _messages.OrderByDescending(m => m.Timestamp).ToList();
        }

        public Message? GetMessageById(string id)
        {
            return _messages.FirstOrDefault(m => m.Id == id);
        }
    }
}