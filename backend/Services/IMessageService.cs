using backend.Models;

namespace backend.Services
{
    public interface IMessageService
    {
        MessageResponse ProcessMessage(Message message);
        List<Message> GetMessages();
        Message? GetMessageById(string id);
    }
}