using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Controle_Zoologico.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Especie { get; set; }

        public int Idade { get; set; }

        public string Sexo { get; set; }

        // Relação muitos-para-muitos com Cuidado via tabela AnimalCuidado
        public ICollection<AnimalCuidado> AnimalCuidados { get; set; }
    }
}
