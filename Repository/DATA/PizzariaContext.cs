using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DATA
{
    public class PizzariaContext : IdentityDbContext<UsuarioLogado>
    {

        public PizzariaContext(DbContextOptions<PizzariaContext> options) : base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Sabor> Sabores { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<Bebida> Bebidas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ItemSabor> ItemSabores { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<ItemPizza> ItemPizzas { get; set; }
        public DbSet<ItemBebida> ItemBebidas { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios");
            modelBuilder.Entity<Cargo>().ToTable("Cargos");
            modelBuilder.Entity<Sabor>().ToTable("Sabores");
            modelBuilder.Entity<Tamanho>().ToTable("Tamanhos");
            modelBuilder.Entity<Bebida>().ToTable("Bebidas");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<ItemSabor>().ToTable("ItemSabores");
            modelBuilder.Entity<Pizza>().ToTable("Pizzas");
            modelBuilder.Entity<ItemPizza>().ToTable("ItemPizzas");
            modelBuilder.Entity<ItemBebida>().ToTable("ItemBebidas");
            modelBuilder.Entity<Venda>().ToTable("Vendas");

            base.OnModelCreating(modelBuilder);
        }

    }
}
