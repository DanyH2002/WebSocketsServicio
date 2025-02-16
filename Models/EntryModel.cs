namespace WebSocketsServicio.API.Models
{

    public class EntryModel
    {
        public required ActionType Action_Type { get; set; }

        public required Value Value { get; set; }

        public EntryModel(Value value, ActionType action_type)
        {
            Value = value;
            Action_Type = action_type;
        }
    }

    public enum ActionType
    {
        /*crear_partido,
        cambiar_nombre_partido,
        borrar_partido,
        voto_partido,
        obtener_partidos*/
        join,
        leave,
        message,
        rooms
    }

    public class Value
    {
        //public Guid? Id { get; set; }

        public string Name { get; set; }
        public string? Msg { get; set; }
        // lista de rooms
        public List<Room>? Rooms { get; set; }


        public Value(string name, string? msg, List<Room>? rooms)
        {
            Name = name;
            Msg = msg;
            Rooms = rooms;
        }

    }
    public class Room
    {
        public string RoomName { get; set; }
        public int User { get; set; }
        public Room(string? roomName, int user)
        {
            RoomName = roomName;
            User = user;
        }
    }

}