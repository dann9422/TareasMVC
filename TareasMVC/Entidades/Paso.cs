namespace TareasMVC.Entidades
{
    public class Paso
    {
        public Guid Id { get; set; }

        public int TareaId { get; set; }
        public Tarea Tarea { get; set; }// se usa para generar la FK de la otra tabla
        public string Descripcion { get; set; }
        public bool Realizado { get; set; }

        public int Orden { get; set; }

    }
}
