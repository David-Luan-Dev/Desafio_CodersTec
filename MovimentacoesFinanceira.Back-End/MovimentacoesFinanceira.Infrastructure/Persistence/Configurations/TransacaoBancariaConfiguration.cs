using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentacoesFinanceira.Core.Entities;

namespace MovimentacoesFinanceira.Infrastructure.Persistence.Configurations
{
    public class TransacaoBancariaConfiguration : IEntityTypeConfiguration<TransacaoFinanceira>
    {
        public void Configure(EntityTypeBuilder<TransacaoFinanceira> builder)
        {
            builder
            .HasKey(transacao => transacao.IdTransacao);

        }
    }
}
