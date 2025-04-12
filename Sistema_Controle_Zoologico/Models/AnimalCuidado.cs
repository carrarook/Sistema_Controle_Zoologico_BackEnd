namespace Sistema_Controle_Zoologico.Models
{
    public class AnimalCuidado
    {
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        public int CuidadoId { get; set; }
        public Cuidado Cuidado { get; set; }
    }
}
