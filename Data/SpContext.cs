using Microsoft.EntityFrameworkCore;
using ProWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProWeb.Data
{
    public class SpContext : IdentityDbContext<IdentityUser>
    {
        public SpContext(DbContextOptions<SpContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Detalle> Detalle { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Producto>().HasOne(x=> x.Categoria)
            .WithMany(b=>b.Producto) .HasForeignKey(x => x.CategoriaForeignKey)
            .HasConstraintName("ForeignKey_Producto_Categoria").IsRequired();

            builder.Entity<Pedido>().HasMany(p=>p.producto).WithMany(p=>p.Pedido)
            .UsingEntity<Detalle>(
                j=>j.HasOne(p=>p.Producto).WithMany(r=>r.Detalle).HasForeignKey(p=>p.ProductoId),
                j=>j.HasOne(p=>p.Pedido).WithMany(e=>e.Detalle).HasForeignKey(p=>p.PedidoId),
                j =>{   
                    j.Property(p => p.Id).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.ProductoId, t.PedidoId });
                }
            );

            base.OnModelCreating(builder);
        }
    }
}