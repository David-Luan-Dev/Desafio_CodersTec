using MovimentacoesFinanceira.Core.Enums;


namespace MovimentacoesFinanceira.Application.InputModels
{
    public class TransacoesFinanceiraInputModel
    {
        public TransacoesFinanceiraInputModel(TipoTransacoesEnum tipo, string data_ocorrencia, int valor, string cpf, string cartao, string hora_ocorrencia, string donoDaLoja, string nomeDaLoja)
        {
            Tipo = tipo;
            Data_Ocorrencia = data_ocorrencia;
            Valor = valor;
            Cpf = cpf;
            Cartao = cartao;
            Hora_Ocorrencia = hora_ocorrencia;
            DonoDaLoja = donoDaLoja;
            NomeDaLoja = nomeDaLoja;
        }
        public TipoTransacoesEnum Tipo { get; set; }
        public string Data_Ocorrencia { get; set; }
        public int Valor { get; set; }
        public string Cpf { get; set; }
        public string Cartao { get; set; }
        public string Hora_Ocorrencia { get; set; }
        public string DonoDaLoja { get; set; }
        public string NomeDaLoja { get; set; }


        //public List<TransacoesBancariasViewModel> ParserToViewModel(List<TransacoesBancariasInputModel> transacoesInputModel)
        //{
        //    var transacaoBancariaViewModel = new List<TransacoesBancariasViewModel>();

        //    foreach (var inputModel in transacoesInputModel)
        //    {
        //        transacaoBancariaViewModel.Add(new TransacoesBancariasViewModel(inputModel.Tipo, inputModel.Data_Ocorrencia, inputModel.Valor,
        //            inputModel.Cpf, inputModel.Cartao, inputModel.Hora_Ocorrencia,
        //            inputModel.DonoDaLoja, inputModel.NomeDaLoja));
        //    }

        //    return transacaoBancariaViewModel;
        //}
    }
}
