using ComerceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ComerceAPI.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Cart>().Property(c => c.ClientId);
            modelBuilder.Entity<Client>().Property(c => c.Name).HasMaxLength(128);
            modelBuilder.Entity<Client>().Property(c => c.CPF).HasMaxLength(1);
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(128);
            modelBuilder.Entity<Product>().Property(p => p.ImageUrl).HasMaxLength(256);
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(256);
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(128);

           
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.Id);


            modelBuilder.Entity<Cart>()
            .HasOne(c => c.Client)         
            .WithOne(client => client.Cart) 
            .HasForeignKey<Cart>(c => c.ClientId) 
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Products) 
                .WithMany()  
                .UsingEntity(j => j.ToTable("CartProducts"));

           
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); 

           
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
