using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Notifications;

namespace MovimentacoesFinanceira.Core.Repositories
{
    public interface ILojaRepository
    {
        Task<Loja> GetByNome(string nome);
        Task<ResultNotifications> AddLoja(Loja loja);
    }
}
