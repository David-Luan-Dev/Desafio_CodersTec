using AutoMapper;
using MovimentacoesFinanceira.Application.InputModels;
using MovimentacoesFinanceira.Application.Interfaces;
using MovimentacoesFinanceira.Application.ViewModels;
using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Notifications;
using MovimentacoesFinanceira.Core.Repositories;

namespace MovimentacoesFinanceira.Application.Services.Implementations
{
    public class TransacaoFinanceiraService : ITransacaoFinanceiraService
    {
        private readonly ITransacaoFinanceiraRepository _transacaoBancariaRepository;
        private readonly IMapper _mapper;
        private readonly ILojaRepository _lojaRepository;

        public TransacaoFinanceiraService(ITransacaoFinanceiraRepository transacaoBancariaRepository, ILojaRepository lojaRepository, IMapper mapper)
        {
            _transacaoBancariaRepository = transacaoBancariaRepository;
            _lojaRepository = lojaRepository;
            _mapper = mapper;
        }

        public async Task<ResultNotifications> AddLoja(Loja loja)
        {
            var resultCadastroLoja = await _lojaRepository.AddLoja(loja);
            return resultCadastroLoja;
        }
         
        public async Task<ResultNotifications> getAll()
        {
            var lojas = await _transacaoBancariaRepository.GetAll();
            var ViewModel = _mapper.Map<List<TransacoesFinanceiraViewModel>>(lojas);

            return new ResultNotifications("Retornado com sucesso", ViewModel);
        }

        public async Task<ResultNotifications> SalvarTransacoes(List<TransacoesFinanceiraInputModel> inputModel)
        {
            var transacaoBancariaEntity = new List<TransacaoFinanceira>();
            foreach (var transacao in inputModel)
            {
                var newLoja = new Loja(Guid.NewGuid(), transacao.NomeDaLoja, transacao.DonoDaLoja);
                var lojaAdd = await AddLoja(newLoja);
                var lojaExists = lojaAdd.Data as Loja;
                // se vier null cadastro essa loja na tabela de lojas, junto com a transacao.
                if (lojaExists is not null)
                {
                    transacaoBancariaEntity.Add(new TransacaoFinanceira
                    (
                        Guid.NewGuid(), lojaExists.LojaId, transacao.Tipo, transacao.Data_Ocorrencia,
                        transacao.Valor, transacao.Cpf, transacao.Cartao,
                        transacao.Hora_Ocorrencia, transacao.DonoDaLoja, transacao.NomeDaLoja
                    ));
                }
                else 
                {
                    transacaoBancariaEntity.Add(new TransacaoFinanceira
                    (
                        Guid.NewGuid(), newLoja.LojaId, transacao.Tipo, transacao.Data_Ocorrencia,
                        transacao.Valor, transacao.Cpf, transacao.Cartao,
                        transacao.Hora_Ocorrencia, transacao.DonoDaLoja, transacao.NomeDaLoja
                    ));

                }

            }

           var linhasAfetadas =  await _transacaoBancariaRepository.SalvarTransacoes(transacaoBancariaEntity);

            return new ResultNotifications("Registrado com sucesso", linhasAfetadas);
        }
    }
}
