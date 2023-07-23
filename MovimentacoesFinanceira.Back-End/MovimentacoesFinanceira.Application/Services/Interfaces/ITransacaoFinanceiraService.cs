using MovimentacoesFinanceira.Application.InputModels;
using MovimentacoesFinanceira.Application.ViewModels;
using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Notifications;

namespace MovimentacoesFinanceira.Application.Interfaces
{
    public interface ITransacaoFinanceiraService
    {
        Task<ResultNotifications> SalvarTransacoes(List<TransacoesFinanceiraInputModel> inputModel);
        Task<ResultNotifications> getAll();

        Task<ResultNotifications> AddLoja(Loja loja);

    }
}
