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





        }

        public DbSet<Bebidas> Bebidas { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<ListaReservas> ListaReservas { get; set; }
        public DbSet<Reservas> Reservas { get; set; }


    }
}