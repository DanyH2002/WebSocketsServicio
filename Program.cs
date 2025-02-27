using WebSocketsServicio.API.Controllers;
using WebSocketsServicio.API.Models;
using System.Text.Json;
using Fleck;
using Newtonsoft.Json;

namespace WebSocketsServicio.API
{
    internal class Program
    {
<<<<<<< HEAD
<<<<<<< HEAD
        private static readonly WebSocketServer server = new WebSocketServer("ws://192.168.40.113:9001"); // es el nombre del servicio
=======
        private static readonly WebSocketServer server = new WebSocketServer("192.168.100.30:9001"); // es el nombre del servicio
>>>>>>> d6b8693 (antes de examen)
=======
        private static readonly WebSocketServer server = new WebSocketServer("ws://192.168.40.114:9001"); // es el nombre del servicio
>>>>>>> 6d4f831 (1er paricial)
        private static readonly List<IWebSocketConnection> users = new List<IWebSocketConnection>(); //lista de clientes conectdos
        // Diccionario para el nombres de usuarios
        private static readonly Dictionary<IWebSocketConnection, string> usersName = new Dictionary<IWebSocketConnection, string>();
        // Dicionario 
        private static readonly Dictionary<string, List<IWebSocketConnection>> rooms = new Dictionary<string, List<IWebSocketConnection>>();
        static void Main(string[] args)
        {
            server.Start(connection => //* cliente que se conecta
            {
                connection.OnOpen = () =>
                {
                    users.Add(connection); //? Agregar el cliente a la lista de clientes conectados
                    Console.WriteLine($"Join: {connection.ConnectionInfo.Id}");
                    SendRoomsOnOpen(connection);
                };

                connection.OnClose = () =>
                {
                    users.Remove(connection); //? Eliminar el cliente de la lista de clientes conectados
                    usersName.Remove(connection); //? Eliminar el nombre del usuario
                    foreach (var room in rooms.Values)
                    {
                        // Eliminar el cliente de todas las salas
                        room.Remove(connection);
                    }
                    SendRooms(); //? Enviar la lista de salas a todos los clientes
                    Console.WriteLine($"Leave: {connection.ConnectionInfo.Id}");
                };

                connection.OnMessage = (string entry) =>
                {
                    try
                    {
                        EntryModel entryModel = JsonConvert.DeserializeObject<EntryModel>(entry);
                        Handle(entryModel, connection);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                };
            });

            Console.ReadLine(); // para que este corriendo el servicio
        }

        private static void Handle(EntryModel entry, IWebSocketConnection connection)
        {
            switch (entry.Action_Type)
            {
                case ActionType.join:
                    Join(entry.Value.Name, connection, entry.Value.UserName);
                    break;
                case ActionType.leave:
                    Leave(entry.Value.Name, connection, entry.Value.UserName);
                    break;
                case ActionType.message:
                    Message(entry.Value, connection);
                    break;
                case ActionType.create:
                    Create(entry.Value.Name, connection, entry.Value.UserName);
                    break;
                default:
                    break;
            }
        }
        private static void Create(string nameRoom, IWebSocketConnection connection, string userName)
        {
            // Verificar si la sala ya existe
            if (rooms.ContainsKey(nameRoom))
            {
                connection.Send("La sala ya existe");
                return;
            }
            // Crear la sala
            rooms.Add(nameRoom, new List<IWebSocketConnection>());
            // Enviar la lista de salas a todos los clientes
            SendRooms();
            // Enviar mensaje de creación de sala
            Console.WriteLine($"User: {userName} con ID {connection.ConnectionInfo.Id} Create room: {nameRoom}");
            connection.Send("Sala creada por " + userName);
            Notification("El usuario " + userName + " ha creado la sala " + nameRoom);
        }
        private static void Join(string nameRoom, IWebSocketConnection connection, string userName)
        {
            // Verificar si esta la sala
            if (!rooms.ContainsKey(nameRoom))
            // mandar mensaje de que la sala no existe y salir
            {
                connection.Send("La sala no existe");
                return;
            }
            // Verificar si el cliente ya esta en la sala
            if (rooms[nameRoom].Contains(connection))
            {
                connection.Send("Ya estas en la sala");
                return;
            }
            // Agregar el cliente a la sala
            rooms[nameRoom].Add(connection);
            usersName[connection] = userName;
            // Enviar la lista de salas a todos los clientes
            SendRooms();
            Console.WriteLine($"User:  {userName} con ID {connection.ConnectionInfo.Id} Join to room: {nameRoom}");
            Notification("El usuario " + userName + " se ha unido a la sala " + nameRoom);
        }
        private static void Leave(string nameRoom, IWebSocketConnection connection, string userName)
        {
            // Verificar si esta la sala
            if (!rooms.ContainsKey(nameRoom)) return;
            // Saber si esta dentro de la sala 
            if (rooms[nameRoom].Contains(connection))
            {
                rooms[nameRoom].Remove(connection); // Eliminar el cliente de la sala
                SendRooms(); // Enviar la lista de salas a todos los clientes
                Console.WriteLine($"User: {userName} con ID {connection.ConnectionInfo.Id} Leave to room: {nameRoom}");
                Notification("El usuario " + userName + " ha dejado la sala " + nameRoom);
            }
        }
        private static void Message(Value value, IWebSocketConnection connection)
        {
            // Verificar si esta la sala
            if (!rooms.ContainsKey(value.Name)) return;
            // Verificar si el cliente esta en la sala
            if (!rooms[value.Name].Contains(connection)) return;
            // Enviar el mensaje a todos los clientes de la sala
            foreach (IWebSocketConnection client in rooms[value.Name])
            {
                // Enviar el mensaje a todos los clientes de la sala, pero no al cliente que envio el mensaje
                if (client != connection)
                {
                    var message = new
                    {
                        Action_Type = "message",
                        Value = new
                        {
                            Name = value.Name, //? Nombre de la sala
                            Msg = $"{usersName[connection]}: {value.Msg}" //? Nombre del usuario que envia el mensaje y el mensaje
                        }
                    };

                    string jsonMessage = JsonConvert.SerializeObject(message);
                    client.Send(jsonMessage); // Enviar la cadena JSON
                    //client.Send(value.Msg);
                }
            }
        }
        private static void SendRoomsOnOpen(IWebSocketConnection connection) //* Enviar las rooms al cliente que se conecta
        {
            // para las rooms dinamicas
            var roomsList = rooms.Select(room => new
            {
                RoomName = room.Key,  //? Nombre de la sala
                User = room.Value.Count, //? Contar los usuarios en la sala
                Members = room.Value.Select(
                    list => usersName.ContainsKey(list) ? usersName[list] : "Anonimo"
                    ).ToArray() //? lista de miembros de rooms
            }).ToArray();
            var message = new
            {
                Action_Type = "rooms",
                Value = new
                {
                    Name = "",
                    Msg = "",
                    Rooms = roomsList
                }
            };
            string jsonMessage = JsonConvert.SerializeObject(message);
            connection.Send(jsonMessage);
        }
        private static void SendRooms() //* Enviar las rooms actualizadas a todos los clientes
        {
            // para las rooms dinamicas
            var roomsList = rooms.Select(room => new
            {
                RoomName = room.Key,  //? Nombre de la sala
                User = room.Value.Count, //? Contar los usuarios en la sala
                //? lista de miembros de rooms
                Members = room.Value.Select(
                    con => usersName.ContainsKey(con) ? usersName[con] : "Anonimo"
                    ).ToArray()//? lista de miembros de rooms
            }).ToArray();
            var message = new
            {
                Action_Type = "rooms",
                Value = new
                {
                    Name = "",
                    Msg = "",
                    Rooms = roomsList
                }
            };
            string jsonMessage = JsonConvert.SerializeObject(message);
            foreach (IWebSocketConnection connection in users)
            {
                connection.Send(jsonMessage);
            }
        }
        private static void Notification(string change) //* Notificación de que un cliente creo, de unio o dejo una sala
        {
            foreach (IWebSocketConnection client in users)
            {
                client.Send(change);
            }

        }

    }
}