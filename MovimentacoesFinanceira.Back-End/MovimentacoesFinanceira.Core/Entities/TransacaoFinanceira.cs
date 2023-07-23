using MovimentacoesFinanceira.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimentacoesFinanceira.Core.Entities
{
    public class TransacaoFinanceira
    {
        // here
        protected TransacaoFinanceira() { }
        public TransacaoFinanceira(Guid idTransacao, Guid lojaId, TipoTransacoesEnum tipo, string data_Ocorrencia, int valor, string cpf, string cartao, string hora_Ocorrencia, string donoDaLoja, string nomeDaLoja)
        {
            IdTransacao = idTransacao;
            LojaId = lojaId;
            Tipo = tipo;
            Data_Ocorrencia = data_Ocorrencia;
            Valor = valor;
            Cpf = cpf;
            Cartao = cartao;
            Hora_Ocorrencia = hora_Ocorrencia;
            DonoDaLoja = donoDaLoja;
            NomeDaLoja = nomeDaLoja;
        }

        public Guid IdTransacao { get; set; }
        [ForeignKey("LojaId")]
        public Guid LojaId { get; set; }
        public TipoTransacoesEnum Tipo { get; set; }
        public string Data_Ocorrencia { get; set; }
        public int Valor { get; set; }
        public string Cpf { get; set; }
        public string Cartao { get; set; }
        public string Hora_Ocorrencia { get; set; }
        public string DonoDaLoja { get; set; }
        public string NomeDaLoja { get; set; }

    }
}
