using Microsoft.EntityFrameworkCore;

namespace TareasMVC.Entidades
{
    public class ArchivoAdjunto
    {
        public Guid Id { get; set; }

        public int TareaId { get; set; }
        public Tarea Tarea { get; set; }// se usa para generar la FK de la otra tabla
        [Unicode]
        public string Url { get; set; }
        public string Titulo { get; set; }
        public int Orden { get; set; }
        public DateTime FechaCreacion { get; set; }

        public List<ArchivoAdjunto> ArchivoAdjuntos { get; set; }   

    }
}
