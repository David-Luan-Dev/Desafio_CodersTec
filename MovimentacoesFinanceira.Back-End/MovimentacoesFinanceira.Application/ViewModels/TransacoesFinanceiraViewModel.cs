using MovimentacoesFinanceira.Core.Entities;

namespace MovimentacoesFinanceira.Application.ViewModels
{
    public class TransacoesFinanceiraViewModel
    {
        public TransacoesFinanceiraViewModel(Guid lojaId, string nomeDaLoja, string donoDaLoja)
        {
            LojaId = lojaId;
            NomeDaLoja = nomeDaLoja;
            DonoDaLoja = donoDaLoja;
        }

        public Guid LojaId { get; set; }
        public string NomeDaLoja { get; set; }
        public string DonoDaLoja { get; set; }
        public virtual ICollection<TransacaoFinanceira> TransacoesFinanceira { get; set; }
    }
}
