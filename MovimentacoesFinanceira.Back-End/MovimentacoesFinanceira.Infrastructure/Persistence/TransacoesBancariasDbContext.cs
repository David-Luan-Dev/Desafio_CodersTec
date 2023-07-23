using Microsoft.EntityFrameworkCore;
using MovimentacoesFinanceira.Core.Entities;
using System.Reflection;

namespace MovimentacoesFinanceira.Infrastructure.Persistence
{
    public class TransacoesBancariasDbContext : DbContext
    {
        public TransacoesBancariasDbContext(DbContextOptions<TransacoesBancariasDbContext> options) : base(options)
        { }

        public DbSet<TransacaoFinanceira> TransacoesFinanceira { get; set; }
        public DbSet<Loja> Lojas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Loja>().HasData(
             new Loja(Guid.Parse("EF988CFF-0A16-4A8C-BC3B-02A05C3A82EE"), "MERCADO DA AVENIDA", "MARCOS PEREIRA"),
             new Loja(Guid.Parse("6AAD4635-C182-42A6-96D6-0C3B65FE0AA6"), "MERCEARIA 3 IRMÃOS", "JOSÉ COSTA    "),
             new Loja(Guid.Parse("69697AEF-BF41-4D85-B2F6-15E34A93BFB1"), "BAR DO JOÃO       ", "JOÃO MACEDO   "),
             new Loja(Guid.Parse("51301CC7-C2FD-408F-A960-AB9B60946A88"), "LOJA DO Ó - FILIAL", "MARIA JOSEFINA"),
             new Loja(Guid.Parse("A8CCDCED-894E-4C31-A8CF-E7079D56A062"), "LOJA DO Ó - MATRIZ", "MARIA JOSEFINA")
           );

            // adiciona todas as referências que utilizam como extensão a IEntityTypeConfiguration 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

           // modelBuilder.Entity<Loja>()
           //.HasMany(loja => loja.TransacaoBancarias)
           //.WithOne(transacao => transacao.Loja)
           //.HasForeignKey(t => t.LojaId);
        }
    }
}
