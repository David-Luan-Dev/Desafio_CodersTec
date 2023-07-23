using Microsoft.EntityFrameworkCore;
using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Repositories;

namespace MovimentacoesFinanceira.Infrastructure.Persistence.Repositories
{
    public class TransacoesBancariasRepository : ITransacaoFinanceiraRepository
    {
        private readonly TransacoesBancariasDbContext _dbContext;

        public TransacoesBancariasRepository(TransacoesBancariasDbContext transacoesBancariasDbContext)
        {
            _dbContext = transacoesBancariasDbContext;
        }

        public async Task<List<Loja>> GetAll()
        {
            try
            {
                var lojasTransacoes = await _dbContext.Lojas
                            .Include(x => x.TransacoesFinanceira)
                            .ToListAsync();

                return lojasTransacoes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> SalvarTransacoes(List<TransacaoFinanceira> transacoesBancarias)
        {
            try
            {
                await _dbContext.TransacoesFinanceira.AddRangeAsync(transacoesBancarias);
                var linhas = await _dbContext.SaveChangesAsync();

                return linhas;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
