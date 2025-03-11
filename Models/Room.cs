namespace WebSocketsServicio.API.Models 
{
    public class Room
    {
        public required string Name { get; set; } //  Nombre de la sala
        public required string Host { get; set; } // Creador de la sala
        public required string Password { get; set;} // Constraseña para crear y unise 
        public List<Player>? Players { get; set; } // Lista de jugadores 
        public required string Description { get; set; } // Descripcion  
        public List<Question>? Questions { get; set; } // Preguntas 
        public bool? IsActive { get; set; }  // Partida activa
        public Room(string name, string host, string password,string description, List<Player>? players, List<Question>? questions, bool? isActive)
        {
            Name = name;
            Host = host;
            Password = password;
            Description = description;
            Players = players;
            Questions = questions;
            IsActive = isActive;
        }
    }
    public class Player
    {
        public required string Name { get; set; }
        public bool? IsHost { get; set; }
        public Player(string name, bool? isHost)
        {
            Name = name;
            IsHost = isHost;
        }
    }

    public class Question
    {
        public required string PlayerName { get; set; } // Id del jugador que hace la pregunta
        public required string Text { get; set; }
        public bool? IsCorrect { get; set; }
        public Question(string playerName, string text, bool? isCorrect)
        {
            PlayerName = playerName;
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}