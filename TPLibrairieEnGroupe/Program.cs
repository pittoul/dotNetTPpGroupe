using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrairieDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TPLibrairiev03;

namespace TPLibrairieEnGroupe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            var dbContext = (LibrairieDbContext)builder.Services.CreateScope().ServiceProvider.GetRequiredService(typeof(LibrairieDbContext));
            dbContext.Database.EnsureCreated();
            if (!dbContext.Articles.Any())
            {
                dbContext.Secteurs.AddRange(
                    new[] {
                        new Secteur
                        {
                            Id = 1,
                            Nom = "Secteur A"
                        },
                        new Secteur
                        {
                            Id = 2,
                            Nom = "Secteur B"
                        }
                    });
                dbContext.Etageres.AddRange(
                    new[] {
                        new Etagere
                        {
                            Id = 1,
                            PoidsMaximum = 15000,
                            IdSecteur = 1

                        },
                        new Etagere
                        {
                            Id = 2,
                            PoidsMaximum = 17000,
                            IdSecteur =1
                        },
                        new Etagere
                        {
                            Id = 3,
                            PoidsMaximum = 15500,
                            IdSecteur =1
                        },
                        new Etagere
                        {
                            Id = 4,
                            PoidsMaximum = 12000,
                            IdSecteur =2
                        }
                    });
                dbContext.Articles.AddRange(
                    new[] {
                        new Article
                        {
                            Id = 1,
                            Libelle = "Tablette",
                            SKU = "123456",
                            DateSortie = new DateTime(2019, 02, 10),
                            PrixInitial = 499.99m,
                            Poids = 499m,
                        },
                        new Article
                        {
                            Id = 2,
                            Libelle = "Telephone",
                            SKU = "789101",
                            DateSortie = new DateTime(2019, 03, 02),
                            PrixInitial = 299.59m,
                            Poids = 258m,
                        },
                        new Article
                        {
                            Id = 3,
                            Libelle = "PC",
                            SKU = "147852",
                            DateSortie = new DateTime(2018, 05, 05),
                            PrixInitial = 1566.23m,
                            Poids = 1890m,
                        },
                        new Article
                        {
                            Id = 4,
                            Libelle = "Bureau",
                            SKU = "258963",
                            DateSortie = new DateTime(2010, 06, 02),
                            PrixInitial = 350m,
                            Poids = 9500m,
                        }
                    });
                dbContext.PositionMagasins.AddRange(
                    new[]{
                        new PositionMagasin
                        {
                            IdArticle = 1,
                            IdEtagere = 1,
                            Quantite = 10
                        },
                        new PositionMagasin
                        {
                            IdArticle = 2,
                            IdEtagere = 1,
                            Quantite = 2
                        },
                        new PositionMagasin
                        {
                            IdArticle = 1,
                            IdEtagere = 3,
                            Quantite = 15
                        }
                });

                dbContext.SaveChanges();

            }


            //Je lance mon site
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
