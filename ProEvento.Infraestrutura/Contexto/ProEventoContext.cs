using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Identity;
using ProEvento.Dominio.Models;
using ProEventos.Domain.Models;

namespace ProEventos.Api.Data
{
    public class ProEventoContext : IdentityDbContext<User, Role, int, 
                                                        IdentityUserClaim<int>, IdentityUserRole<int>,
                                                        IdentityUserLogin<int>, IdentityRoleClaim<int>, 
                                                        IdentityUserToken<int>>
    {
        public ProEventoContext(DbContextOptions<ProEventoContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSocials)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lote>()
                .Property(i => i.Id).UseIdentityColumn();

            modelBuilder.Entity<Evento>()
                .Property(i => i.Id).UseIdentityColumn();
        }
    }
}