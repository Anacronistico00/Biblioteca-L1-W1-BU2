using Biblioteca_L1_W1_BU2.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_L1_W1_BU2.Data
{
    public class BibliotecaEfCoreDbContext :DbContext
    {
        public BibliotecaEfCoreDbContext(DbContextOptions<BibliotecaEfCoreDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
