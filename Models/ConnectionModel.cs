using Fleck; 
namespace WebSocketsServicio.API.Models
{
    public class ConnectionModel // Modelo para la conexion
    {
        public string Id { get; set; }
        public IWebSocketConnection Connection { get; private set;}

        // Constructor
        public ConnectionModel(string id, IWebSocketConnection connection)
        {
            Id = id;
            Connection = connection;
        }
    }
}