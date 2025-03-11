using WebSocketsServicio.API.Models;
namespace WebSocketsServicio.API.Models
{
    public class RoomModel
    {
        public string RoomName { get; set; }
        public string Password { get; set; }
        public List<ConnectionModel> users { get; set; } = new List<ConnectionModel>();
        public RoomModel(string roomName, string password)
        {
            RoomName = roomName;
            Password = password;
        }
    }
}