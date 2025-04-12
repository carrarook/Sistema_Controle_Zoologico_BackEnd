using System;
using System.Collections.Generic;

namespace Sistema_Controle_Zoologico.Models
{
    public class Cuidado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }  
        public string Frequencia { get; set; } 

  
        public ICollection<AnimalCuidado> AnimalCuidados { get; set; }
    }
}
