using Microsoft.EntityFrameworkCore;
using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Notifications;
using MovimentacoesFinanceira.Core.Repositories;
namespace MovimentacoesFinanceira.Infrastructure.Persistence.Repositories
{
    public class LojaRepository : ILojaRepository
    {
        private readonly TransacoesBancariasDbContext _transacoesBancariasDbContext;

        public LojaRepository(TransacoesBancariasDbContext transacoesBancariasDbContext)
        {
            _transacoesBancariasDbContext = transacoesBancariasDbContext;
        }

        public async Task<ResultNotifications> AddLoja(Loja loja)
        {
            var lojaExists = await GetByNome(loja.NomeDaLoja);

            if (lojaExists is not null)
            {
               return new ResultNotifications("Loja já cadastrada.", lojaExists);
            }

            var lojaCadastrada = await _transacoesBancariasDbContext.Lojas.AddAsync(loja);

            if (lojaCadastrada.State == EntityState.Added)
            {
                _transacoesBancariasDbContext.SaveChanges();

                return new ResultNotifications("cadastrada com sucesso!");
            }

            return new ResultNotifications("erro ao cadastrar essa loja");
        }

        public async Task<Loja> GetByNome(string nomeLoja)
        {
            try
            {
                var empresa = await _transacoesBancariasDbContext.Lojas.SingleOrDefaultAsync(x => x.NomeDaLoja.ToLower() == nomeLoja.ToLower());
                if (empresa == null)
                {
                    return null;
                }

                return empresa;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
