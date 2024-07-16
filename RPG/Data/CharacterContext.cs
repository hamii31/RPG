using Microsoft.EntityFrameworkCore;
using RPG.Data.Models;

namespace RPG.Data
{
    internal class CharacterContext : DbContext
    {
        public CharacterContext()
        {

        }
        public CharacterContext(DbContextOptions<CharacterContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=RPG2024;User ID=sa;Password=H@meleon3109;TrustServerCertificate=True;");
            // Data Source=your_server_name;Initial Catalog=your_database_name;User ID=your_username;Password=your_password;TrustServerCertificate=True;
        }
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<Character>()
                .Property(h => h.CreatedOn)
                .HasPrecision(4, 3); // Set the precision and scale for the datetime column
        }
    }
}
