using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Controle_Zoologico.Models
{
    public class Cuidado
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        // Navegação reversa: animais que recebem esse cuidado
        public ICollection<AnimalCuidado> AnimalCuidados { get; set; }
    }
}
