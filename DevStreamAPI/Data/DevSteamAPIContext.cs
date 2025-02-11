using DevStreamAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevStreamAPI.Data
{
    public class DevSteamAPIContext : IdentityDbContext
    {
        //metodo construtor
        public DevSteamAPIContext(DbContextOptions<DevSteamAPIContext> options) :
            base(options)
        { }

        //definir as tabelas do banco de dados
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        //sobscrever o metodo OnModeCreating
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<Jogo>().ToTable("Jogos");
            modelbuilder.Entity<Categoria>().ToTable("Categorias");
        }
        public DbSet<DevStreamAPI.Models.Carrinho> Carrinho { get; set; } = default!;
        public DbSet<DevStreamAPI.Models.ItemCarrinho> ItemCarrinho { get; set; } = default!;
    }
};
