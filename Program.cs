// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

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
        private static readonly WebSocketServer server = new WebSocketServer("ws://192.168.40.113:9001"); // es el nombre del servicio
=======
        private static readonly WebSocketServer server = new WebSocketServer("192.168.100.30:9001"); // es el nombre del servicio
>>>>>>> d6b8693 (antes de examen)
        private static readonly List<IWebSocketConnection> users = new List<IWebSocketConnection>(); //lista de clientes conectdos

        // Lista de los partidos politicos -- mio
        //private static readonly PartidoPoliticoController partidosPoliticos = new PartidoPoliticoController();
        // Lista estatica de los partidos politicos -- profe
        /*private static List<PartidoPoliticoModel> partidos = new List<PartidoPoliticoModel>
        {
            new PartidoPoliticoModel(Guid.NewGuid(), "Morena", 0, true, new PictureSettings("https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Morena_logo_%28alt%29.svg/1200px-Morena_logo_%28alt%29.svg.png")),
            new PartidoPoliticoModel(Guid.NewGuid(), "PRD", 0, true, new PictureSettings("https://upload.wikimedia.org/wikipedia/commons/thumb/8/8f/PRD_logo_%28Mexico%29.svg/2048px-PRD_logo_%28Mexico%29.svg.png")),
            new PartidoPoliticoModel(Guid.NewGuid(), "PAN", 0, true, new PictureSettings("https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/PAN_logo_%28Mexico%29.svg/320px-PAN_logo_%28Mexico%29.svg.png")),
            new PartidoPoliticoModel(Guid.NewGuid(), "PT", 0, true, new PictureSettings("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCVDlIGKIBQ0PMQldq8QuDY0TrAd1RIaCuCQ&s"))
        };*/

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
                    //clients.Add(cliente_que_se_conecta);
                    //? Enviar la lista de los partidos politicos a los clientes conectados
                    //obtener_partidos(cliente_que_se_conecta);
                    /* Enviar la lista de los partidos politicos a los clientes conectados
                    var partidosPoliticosJson = JsonSerializer.Serialize(partidosPoliticos.EnviarListaDePartidosPoliticos());
                    cliente_que_se_conecta.Send(partidosPoliticosJson);
                    cliente_que_se_conecta.Send("Para votar envia el ID del partido politico");*/
                };

                connection.OnClose = () =>
                {
                    users.Remove(connection); //? Eliminar el cliente de la lista de clientes conectados
                    Console.WriteLine($"Leave: {connection.ConnectionInfo.Id}");
                    //clients.Remove(cliente_que_se_conecta);
                };

                connection.OnMessage = (string entry) =>
                {
                    /*Console.WriteLine($"Mensaje recibido: {entry}");
                    foreach (var cliente in Lista_de_clientes_conectados)
                    {
                        cliente.Send(mensaje_del_cliente);
                    }
                    // Actualizar los votos de un parti2do politico por ID y enviar la lista de los partidos politicos a los clientes conectados
                    var mensajeDelClienteJson = JsonSerializer.Deserialize<PartidoPoliticoModel>(mensaje_del_cliente);
                    var partido = partidosPoliticos.ActualizarVotosDePartidoPolitico(mensajeDelClienteJson.Id);
                    var partidoJson = JsonSerializer.Serialize(partido);
                    foreach (var cliente in Lista_de_clientes_conectados)
                    {
                        cliente.Send(partidoJson);
                    }*/

                    /*try
                    {
                        //? Deserializar el mensaje del cliente
                        EntryModel entry = JsonConvert.DeserializeObject<EntryModel>(mensaje_del_cliente);
                        //? Procesar el mensaje del cliente
                        Lo_Que_Vamos_Hacer_Con_El_Mensaje_Del_Usuario(entry, cliente_que_se_conecta);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    foreach (IWebSocketConnection cliente in clients)
                    {
                        if (cliente.ConnectionInfo.Id != conection.ConnectionInfo.Id)
                        {
                            cliente.Send(entry);
                        }
                    }*/
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

        /*private static void crear_partido(string nombre)
        {
            PartidoPoliticoModel partido_nuevo = new PartidoPoliticoModel(Guid.NewGuid(), nombre, 0, true, new PictureSettings("https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Morena_logo_%28alt%29.svg/1200px-Morena_logo_%28alt%29.svg.png"));
            partidos.Add(partido_nuevo);
        }
        private static void cambiar_nombre_partido(Value value)
        {
            //? Buscar el partido por ID
            var partido = partidos.Find(p => p.Id == value.Id);
            //? Si el partido no existe no hace nada
            if (partido == null) return;
            //? Actualizar el nombre del partido
            partido.Nombre = value.Nombre;
        }
        private static void borrar_partido(Guid id)
        {
            //? Buscar el partido por ID
            var partido = partidos.Find(p => p.Id == id);
            //? Si el partido no existe no hace nada
            if (partido == null) return;
            //? Eliminar el partido de la lista
            partidos.Remove(partido);
        }
        private static void voto_partido(Guid id)
        {
            //? Buscar el partido por ID
            var partido = partidos.Find(p => p.Id == id);
            //?Si el partido no existe no hace nada
            if (partido == null) return;
            //? Actualizar los votos del partido
            partido.Votos++;
        }
        private static void obtener_partidos(IWebSocketConnection el_cliente_que_se_conecto)
        {
            //? Envia la lista de partidos al cliente que se acaba de conectar
            el_cliente_que_se_conecto.Send(JsonConvert.SerializeObject(partidos));
        }
        private static void Lo_Que_Vamos_Hacer_Con_El_Mensaje_Del_Usuario(EntryModel entry, IWebSocketConnection cliente_que_se_conecto)
        {
            switch (entry.Action_Type)
            {
                case ActionType.crear_partido:
                    crear_partido(entry.Value.Nombre);
                    break;
                case ActionType.cambiar_nombre_partido:
                    cambiar_nombre_partido(entry.Value);
                    break;
                case ActionType.borrar_partido:
                    borrar_partido((Guid)entry.Value.Id);
                    break;
                case ActionType.voto_partido:
                    voto_partido((Guid)entry.Value.Id);
                    break;
                case ActionType.obtener_partidos:
                    obtener_partidos(cliente_que_se_conecto);
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }*/

        private static void Handle(EntryModel entry, IWebSocketConnection connection)
        {
            switch (entry.Action_Type)
            {
                case ActionType.join:
                    Join(entry.Value.Name, connection);
                    break;
                case ActionType.leave:
                    Leave(entry.Value.Name, connection);
                    break;
                case ActionType.message:
                    Message(entry.Value, connection);
                    break;
                default:
                    break;
            }
        }
        private static void Join(string nameRoom, IWebSocketConnection connection)
        {
            // Saber si existe la sala
            if (!rooms.ContainsKey(nameRoom))
            {
                rooms[nameRoom] = new List<IWebSocketConnection>(); // Crear la sala
            }
            // verificar si el cliente ya esta en la sala
            if (!rooms[nameRoom].Contains(connection))
            {
                rooms[nameRoom].Add(connection); // Agregar el cliente a la sala
                SendRooms();
                Console.WriteLine($"User: {connection.ConnectionInfo.Id} Join to room: {nameRoom}");
            }
        }
        private static void Leave(string nameRoom, IWebSocketConnection connection)
        {
            // Verificar si esta la sala
            if (!rooms.ContainsKey(nameRoom)) return;
            // Saber si esta dentro de la sala 
            if (rooms[nameRoom].Contains(connection))
            {
                rooms[nameRoom].Remove(connection); // Eliminar el cliente de la sala
                Console.WriteLine($"User: {connection.ConnectionInfo.Id} Leave to room: {nameRoom}");
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
                            Name = value.Name,
                            Msg = value.Msg
                        }
                    };

                    string jsonMessage = JsonConvert.SerializeObject(message);
                    client.Send(jsonMessage); // Enviar la cadena JSON
                    //client.Send(value.Msg);
                }
            }
        }
        private static void SendRoomsOnOpen(IWebSocketConnection connection) //* Enviar las salas al cliente que se conecta
        {
            // para las rooms dinamicas
            var roomsList = rooms.Select(room => new
            {
                RoomName = room.Key,  //? Nombre de la sala
                User = room.Value.Count //? Contar los usuarios en la sala
            }).ToArray();
            var message = new
            {
                Action_Type = "rooms",
                Value = new
                {
                    Name = "",
                    Msg = "",
                    Rooms = roomsList
                    /*new[]
                    {
                        new
                        {
                            RoomName = "Room 1",
                            User = 3
                        },
                        new
                        {
                            RoomName = "Room 2",
                            User = 2
                        }
                    }*/
                }
            };
            string jsonMessage = JsonConvert.SerializeObject(message);
            connection.Send(jsonMessage);
        }
        private static void SendRooms() 
        {
            // para las rooms dinamicas
            var roomsList = rooms.Select(room => new
            {
                RoomName = room.Key,  //? Nombre de la sala
                User = room.Value.Count //? Contar los usuarios en la sala
            }).ToArray();
            //string jsonMessage = JsonConvert.SerializeObject(message);
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
            foreach(IWebSocketConnection connection in users){
                connection.Send(jsonMessage);
            }
            //connection.Send(jsonMessage);
        }
    }
}