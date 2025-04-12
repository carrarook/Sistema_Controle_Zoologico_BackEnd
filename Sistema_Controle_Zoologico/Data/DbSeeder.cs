//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Sistema_Controle_Zoologico.Models;
//using System;
//using System.Linq;

//namespace Sistema_Controle_Zoologico.Data
//{
//    public static class DbSeeder
//    {
//        public static void Initialize(IServiceProvider serviceProvider)
//        {
//            using (var context = new ApplicationDbContext(
//                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
//            {
//                // Verifica se já existem dados no banco
//                if (context.Animais.Any() || context.Cuidados.Any())
//                {
//                    return; // Banco já foi populado
//                }

//                // Adiciona cuidados de exemplo
//                var alimentacao = new Cuidado
//                {
//                    Nome = "Alimentação",
//                    Descricao = "Fornecimento de alimentos conforme dieta específica da espécie",
//                    Frequencia = "Diária"
//                };

//                var exameVeterinario = new Cuidado
//                {
//                    Nome = "Exame Veterinário",
//                    Descricao = "Avaliação de saúde completa",
//                    Frequencia = "Mensal"
//                };

//                var vacinacao = new Cuidado
//                {
//                    Nome = "Vacinação",
//                    Descricao = "Aplicação de vacinas preventivas",
//                    Frequencia = "Anual"
//                };

//                var treinamento = new Cuidado
//                {
//                    Nome = "Treinamento",
//                    Descricao = "Técnicas de condicionamento e enriquecimento ambiental",
//                    Frequencia = "Semanal"
//                };

//                context.Cuidados.AddRange(alimentacao, exameVeterinario, vacinacao, treinamento);
//                context.SaveChanges();

//                // Adiciona animais de exemplo
//                var leao = new Animal
//                {
//                    Nome = "Simba",
//                    Descricao = "Leão macho adulto com juba característica",
//                    DataNascimento = new DateTime(2018, 5, 10),
//                    Especie = "Panthera leo",
//                    Habitat = "Savana africana",
//                    PaisOrigem = "Quênia"
//                };

//                var tigre = new Animal
//                {
//                    Nome = "Rajah",
//                    Descricao = "Tigre de bengala jovem com pelagem vibrante",
//                    DataNascimento = new DateTime(2020, 3, 15),
//                    Especie = "Panthera tigris",
//                    Habitat = "Floresta tropical",
//                    PaisOrigem = "Índia"
//                };

//                var elefante = new Animal
//                {
//                    Nome = "Dumbo",
//                    Descricao = "Elefante asiático macho com orelhas grandes",
//                    DataNascimento = new DateTime(2015, 7, 20),
//                    Especie = "Elephas maximus",
//                    Habitat = "Floresta tropical",
//                    PaisOrigem = "Tailândia"
//                };

//                context.Animais.AddRange(leao, tigre, elefante);
//                context.SaveChanges();

//                // Estabelece relações entre animais e cuidados
//                context.AnimalCuidados.AddRange(
//                    new AnimalCuidado { AnimalId = leao.Id, CuidadoId = alimentacao.Id },
//                    new AnimalCuidado { AnimalId = leao.Id, CuidadoId = exameVeterinario.Id },
//                    new AnimalCuidado { AnimalId = tigre.Id, CuidadoId = alimentacao.Id },
//                    new AnimalCuidado { AnimalId = tigre.Id, CuidadoId = vacinacao.Id },
//                    new AnimalCuidado { AnimalId = elefante.Id, CuidadoId = alimentacao.Id },
//                    new AnimalCuidado { AnimalId = elefante.Id, CuidadoId = treinamento.Id }
//                );

//                context.SaveChanges();
//            }
//        }
//    }
//}