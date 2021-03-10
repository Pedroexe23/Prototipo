using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Prototipo.Models
{
    public partial class Documentacion : DbContext
    {
        public Documentacion()
            : base("name=Documentacion")
        {
        }

        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Registro> Registro { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documento>()
                .Property(e => e.Archivo)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .HasMany(e => e.Registro)
                .WithOptional(e => e.Documento)
                .HasForeignKey(e => e.Fk_Id_Documento);

            modelBuilder.Entity<Personas>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.Rut)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .HasMany(e => e.Registro)
                .WithOptional(e => e.Personas)
                .HasForeignKey(e => e.Fk_RUT);

            modelBuilder.Entity<Registro>()
                .Property(e => e.Fk_RUT)
                .IsUnicode(false);
        }
    }
}
