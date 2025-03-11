using WebSocketsServicio.API.Models;
using System.Text.Json;
using Fleck;
using Newtonsoft.Json;

namespace WebSocketsServicio.API
{
    internal class Program
    {
        private static readonly WebSocketServer server = new WebSocketServer("ws://172.16.10.212:9001");
        //private static readonly List<IWebSocketConnection> users = new List<IWebSocketConnection>();
        private static readonly List<ConnectionModel> users = new List<ConnectionModel>();
        private static readonly Dictionary<string, RoomModel> rooms = new Dictionary<string, RoomModel>();


        static void Main(string[] args)
        {
            // Inicia el servidor WebSocket
            server.Start(socket =>
            {
                // Evento cuando un cliente se conecta
                socket.OnOpen = () =>
                {
                    var ConnectionModel = new ConnectionModel(socket.ConnectionInfo.Path, socket);
                    users.Add(ConnectionModel);
                    Console.WriteLine($"join: {socket.ConnectionInfo.Path}");
                    //users.Add(connection);
                    // SendRoomsOnOpen(connection);
                };

                // Evento cuando un cliente se desconecta
                socket.OnClose = () =>
                {
                    Console.WriteLine($"Leave: {socket.ConnectionInfo.Id}");
                    //users.Remove(connection);
                };

                // Evento cuando el servidor recibe un mensaje de un cliente
                socket.OnMessage = (string mensaje_del_cliente) =>
                {
                    /*EntryModel entryModel = JsonConvert.DeserializeObject<EntryModel>(mensaje_del_cliente);
                    foreach (IWebSocketConnection user in users)
                    {
                        if (connection != user)
                        {
                            var message = new
                            {
                                Id = entryModel.Id,
                                Color = entryModel.Color
                            };
                            string jsonMessage = JsonConvert.SerializeObject(message);
                            user.Send(jsonMessage);
                        }
                    }*/

                    SendRoomsInfo(socket, mensaje_del_cliente);
                };
            });

            // mantiene la aplicacion en ejecucion hasta que se presione enter 
            Console.ReadLine();
        }
        private static void SendRoomsInfo(IWebSocketConnection socket, string roomName)
        {
            // Ejemplo de rooms 
            rooms.Add("room1", new RoomModel("room1", "1234"));
            rooms.Add("room2", new RoomModel("room2", "5678"));           
            RoomModel room = rooms[roomName];
            // Si la room no existe, enviar un mensaje de error
            if (room == null)
            {
                var message = new
                {
                    Type = "error",
                    Message = "Room not found"
                };
                string jsonMessage = JsonConvert.SerializeObject(message);
                socket.Send(jsonMessage);
            }
            else
            {
                // Si la room existe, enviar la info de la room
                var message = new
                {
                    RoomName = room.RoomName,
                    Password = room.Password,
                    users = room.users
                };
                string jsonMessage = JsonConvert.SerializeObject(message);
                socket.Send(jsonMessage);
            }
        }

    }
}