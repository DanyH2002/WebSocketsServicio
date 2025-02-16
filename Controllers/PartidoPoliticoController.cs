using System.Collections.Generic;
using WebSocketsServicio.API.Models;

namespace  WebSocketsServicio.API.Controllers
{
    /*public class PartidoPoliticoController
    {
        // Lista de los partidos politicos
        private readonly List<PartidoPoliticoModel> partidos_Politicos = new List<PartidoPoliticoModel>
        {
            new PartidoPoliticoModel { Id = 1, Nombre = "Galletas", Votos = 10 },
            new PartidoPoliticoModel { Id = 2, Nombre = "Frutas", Votos = 9 },
            new PartidoPoliticoModel { Id = 3, Nombre = "Jugos", Votos = 11 },
            new PartidoPoliticoModel { Id = 4, Nombre = "Doritos", Votos = 8 },
        };

        // Funcion para enviar la lista de los partidos politicos a los clientes conectados
        public List<PartidoPoliticoModel> EnviarListaDePartidosPoliticos()
        {
            return partidos_Politicos;
        }

        // Funcion para actualizar los votos de un partido politico por ID y enviar la lista de los partidos politicos a los clientes conectados
        public PartidoPoliticoModel ActualizarVotosDePartidoPolitico(int id)
        {
            // Buscar el partido politico por ID
            var partido = partidos_Politicos.Find(p => p.Id == id);
            // Si el partido existe, se incrementa el numero de votos
            if (partido != null)
            {
                partido.Votos++;
            }
            return partido;
        }
    }*/
}