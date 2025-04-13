using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sistema_Controle_Zoologico.Models;
using System;
using System.Linq;

namespace Sistema_Controle_Zoologico.Data
{
    public static class DbSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Animais.Any() || context.Cuidados.Any())
                {
                    return;
                }

                var alimentacao = new Cuidado
                {
                    Nome = "Alimentação",
                    Descricao = "Fornecimento de alimentos conforme dieta específica da espécie",
                    Frequencia = "Diária"
                };

               

                context.Cuidados.AddRange(alimentacao);
                context.SaveChanges();

                var leao = new Animal
                {
                    Nome = "Simba",
                    Descricao = "Leão macho adulto com juba característica",
                    DataNascimento = new DateTime(2018, 5, 10),
                    Especie = "Panthera leo",
                    Habitat = "Savana africana",
                    PaisOrigem = "Quênia"
                };

              

                context.Animais.AddRange(leao);
                context.SaveChanges();

                context.AnimalCuidados.AddRange(
                    new AnimalCuidado { AnimalId = leao.Id, CuidadoId = alimentacao.Id }
                );

                context.SaveChanges();
            }
        }
    }
}
