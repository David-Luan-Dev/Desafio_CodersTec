using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentacoesFinanceira.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentacoesFinanceira.Infrastructure.Persistence.Configurations
{
    internal class LojaConfiguration : IEntityTypeConfiguration<Loja>
    {
        public void Configure(EntityTypeBuilder<Loja> builder)
        {
            builder
            .HasKey(loja => loja.LojaId);

        }
    }
}
