using MovimentacoesFinanceira.Core.Entities;

namespace MovimentacoesFinanceira.Core.Repositories
{
    public interface ITransacaoFinanceiraRepository
    {
        Task<int> SalvarTransacoes(List<TransacaoFinanceira> transacoesBancarias);
        Task<List<Loja>> GetAll();
    }
}
