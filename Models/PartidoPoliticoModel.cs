namespace WebSocketsServicio.API.Models
{
    public class PartidoPoliticoModel
    {
        public Guid? Id { get; set; }
        public string Nombre { get; set; }
        public int Votos { get; set; }
        public bool Activo { get; set; }
        public PictureSettings PictureSettings { get; set; }
        // Constructor del modelo
        public PartidoPoliticoModel(Guid? id, string nombre, int votos, bool activo, PictureSettings pictureSettings)
        {
            Id = id;
            Nombre = nombre;
            Votos = votos;
            Activo = activo;
            PictureSettings = pictureSettings;
        }
    }
    public class PictureSettings
    {
        public string Src { get; set; }
        // Constructor del modelo
        public PictureSettings(string src)
        {
            Src = src;
        }
    }
}
