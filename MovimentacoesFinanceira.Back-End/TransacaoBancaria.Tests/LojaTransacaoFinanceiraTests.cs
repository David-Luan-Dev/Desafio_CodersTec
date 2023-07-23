using Microsoft.Extensions.DependencyInjection;
using MovimentacoesFinanceira.Application.Interfaces;
using MovimentacoesFinanceira.Application.ViewModels;
using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Repositories;
using MovimentacoesFinanceira.Tests.Extensions;

namespace MovimentacoesFinanceira.Tests
{
    public class LojaTransacaoFinanceiraTests
    {
        private readonly ITransacaoFinanceiraService _transacoesBancariasService;
        private readonly ILojaRepository _lojaRepository;
        public LojaTransacaoFinanceiraTests()
        {
            var serviceProvider = ServiceCollectionExtensionsTest.DependencyInjectionTest();
            _transacoesBancariasService = serviceProvider?.GetService<ITransacaoFinanceiraService>();
            _lojaRepository = serviceProvider?.GetService<ILojaRepository>();
        }

        [Fact]
        public async void GetAllAsync_DeveRetornarTodasTransacoesDaLoja()
        {
            // Usando a lib Mock. // Para esse teste passar precisa ter rodado a migration, que vai ter dados para testar.
            // Arrange
            var resultExpected = new Loja { NomeDaLoja = "MERCEARIA 3 IRMÃOS", DonoDaLoja = "JOSÉ COSTA    " };

            // Act
            var resultNotification = await _transacoesBancariasService.getAll();
            var lojaEntity = resultNotification.Data as List<TransacoesFinanceiraViewModel>;
            var lojaFirstOrDefault = lojaEntity.FirstOrDefault(x => x.DonoDaLoja == resultExpected.DonoDaLoja);

            //Assert
            Assert.NotEmpty(lojaEntity);
            Assert.NotNull(lojaFirstOrDefault);
        }

        [Fact]
        public async void GetByNomeDaLoja_DeveRetornarDadosDaLojaPeloNome()
        {
            //Arrange
            var nomeDaLoja = "LOJA DO Ó - MATRIZ";

            //Act
            var resultLoja = await _lojaRepository.GetByNome(nomeDaLoja);

            //Assert
            Assert.Equal(nomeDaLoja, resultLoja.NomeDaLoja);

        }

        [Fact] 
        public async void AddLoja_DeveCadastrarUmaLoja()
        {
            //Arrange // precisa mudar o nome da empresa e o dono para esse teste passar.
            var newLoja = new Loja {LojaId = Guid.NewGuid(), NomeDaLoja = "TesteLoja3", DonoDaLoja = "David Luan Teste3" };

            //Act
            var result = await _lojaRepository.AddLoja(newLoja);
            var loja = result.Data as Loja;

            //Assert
            Assert.Equal("cadastrada com sucesso!", result.Mensagem);
            Assert.Null(loja);
        }

        [Fact]
        public async void AddLoja_ReturnErrorlojaExistente()
        {
            //Arrange // Para esse teste passar precisa ter rodado a migration, que vai ter dados para testar.
            var newLoja = new Loja { LojaId = Guid.NewGuid(), NomeDaLoja = "TesteLoja", DonoDaLoja = "JOSÉ COSTA    " };

            //Act
            var result = await _lojaRepository.AddLoja(newLoja);
            var loja = result.Data as Loja;

            //Assert
            Assert.Equal("Loja já cadastrada.", result.Mensagem);
            Assert.NotNull(loja);
        }

    }
}
