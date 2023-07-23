
namespace MovimentacoesFinanceira.Core.Entities
{
    public class Loja
    {
        public Loja()
        {
            
        }
        public Loja(Guid lojaId, string nomeDaLoja, string donoDaLoja)
        {
            LojaId = lojaId;
            NomeDaLoja = nomeDaLoja;
            DonoDaLoja = donoDaLoja;
        }

        public Guid LojaId { get; set; }
        public virtual ICollection<TransacaoFinanceira> TransacoesFinanceira { get; set; } 
        public string NomeDaLoja { get; set; }
        public string DonoDaLoja { get; set; }
    }
}

