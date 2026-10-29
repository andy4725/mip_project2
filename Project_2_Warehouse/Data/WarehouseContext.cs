using System.Data.Entity;
using System.Data.SQLite;
using Project_2_Warehouse.Models;

namespace Project_2_Warehouse.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext() : base("name=WarehouseContext")
        {
            Database.SetInitializer<WarehouseContext>(null);
        }

        public DbSet<Produs> produse { get; set; }
        public DbSet<CategorieProdus> categorii { get; set; }
        public DbSet<IstoricVanzari> istoricVanzari { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // A product must belong to one category.
            modelBuilder.Entity<Produs>()
                .HasRequired(p => p.categorie)
                .WithMany(c => c.produse)
                .HasForeignKey(p => p.idCategorie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produs>().ToTable("Produs");
            modelBuilder.Entity<CategorieProdus>().ToTable("CategorieProdus");
            modelBuilder.Entity<IstoricVanzari>().ToTable("IstoricVanzari");

            base.OnModelCreating(modelBuilder);
        }
    }
}
