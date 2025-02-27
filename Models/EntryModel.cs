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
        create,
        join,
        leave,
        message,
        // lista de rooms
        rooms
    }

    public class Value
    {
        public string Name { get; set; }
        public string? UserName { get; set; }
        public string? Msg { get; set; }
        // lista de rooms
        public List<Room>? Rooms { get; set; }

        public Value(string name, string? userName, string? msg, List<Room>? rooms)
        {
            Name = name;
            UserName = userName;
            Msg = msg;
            Rooms = rooms;
        }

    }
    public class Room
    {
        public string RoomName { get; set; }
        public int User { get; set; }
        // lista de miembros de rooms
        public List<string>? Members { get; set; } 
        public Room(string? roomName, int user, List<string>? members)
        {
            RoomName = roomName;
            User = user;
            Members = members;
        }
    }

}