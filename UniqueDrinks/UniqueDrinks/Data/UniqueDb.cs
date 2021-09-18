using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniqueDrinks.Models;

namespace UniqueDrinks.Data
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// recolhe a data de registo de um utilizador
        /// </summary>
        public DateTime DataRegisto { get; set; }
    }

    public class UniqueDb : IdentityDbContext<ApplicationUser>
    {

        public UniqueDb (DbContextOptions<UniqueDb> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" },
            new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" }
            );

            modelBuilder.Entity<Bebidas>().HasData(
                    new Bebidas
                    {
                        Id = 9,
                        Nome = "Vinho Rose Mateus",
                        Descricao = "MATEUS ROSÉ é um vinho leve, fresco, jovem e ligeiramente «pétillant»",
                        Preco = 12.50F,
                        Imagem = "Vinho-Mateus-Rose.jpg",
                        Stock = "Sim",
                        Categoria = "Vinho"
                    },

                     new Bebidas
                     {
                         Id = 10,
                         Nome = "Vinho do Porto Ferreira",
                         Descricao = "É vinificado pelo método tradicional do vinho do Porto.",
                         Preco = 17.25F,
                         Imagem = "ferreira_Porto.jpg",
                         Stock = "Sim",
                         Categoria = "Vinho do Porto"
                     },

                     new Bebidas
                     {
                         Id = 11,
                         Nome = "Grants Whisky",
                         Descricao = "Grant’s é um whisky extraordinário e um dos mais complexos produzidos na Escócia.",
                         Preco = 27.99F,
                         Imagem = "grants_whisky.jpg",
                         Stock = "Sim",
                         Categoria = "Whiskey"
                     },

                    new Bebidas
                    {
                        Id = 12,
                        Nome = "Super Bock Pack15",
                        Descricao = "O sabor autêntico.Super Bock Original é a única cerveja portuguesa com 37 medalhas de ouro consecutivas",
                        Preco = 12.50F,
                        Imagem = "superBock.jpg",
                        Stock = "Sim",
                        Categoria = "Cerveja"
                    }



                    );



        }

        public DbSet<Bebidas> Bebidas { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<ListaReservas> ListaReservas { get; set; }
        public DbSet<Reservas> Reservas { get; set; }


    }
}