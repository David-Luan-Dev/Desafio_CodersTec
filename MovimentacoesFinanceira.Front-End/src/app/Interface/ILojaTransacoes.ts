export interface ILojaTransacoes {
  lojaId: string;
  nomeDaLoja: string;
  expanded: boolean;
  donoDaLoja: string;
  transacoesFinanceira: transacoesFinanceira[];
}

export interface transacoesFinanceira {
  idTransacao: string;
  lojaId: string;
  donoDaLoja: string;
  nomeDaLoja: string;
  tipo: TipoTransacoesEnum;
  data_Ocorrencia: string;
  valor: number;
  cpf: string;
  cartao: string;
  hora_Ocorrencia: string;
}

export enum TipoTransacoesEnum {
  Debito = 1,
  Boleto = 2,
  Financiamento = 3,
  Credito = 4,
  Recebimento_Emprestimo = 5,
  Vendas = 6,
  Recebimento_TED = 7,
  Recebimento_DOC = 8,
  Aluguel = 9
}
