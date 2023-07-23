using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MovimentacoesFinanceira.Application.InputModels;
using MovimentacoesFinanceira.Application.Interfaces;
using MovimentacoesFinanceira.Application.Services.Interfaces;
using MovimentacoesFinanceira.Tests.Extensions;
using System.IO.Abstractions;

namespace MovimentacoesFinanceira.Tests
{
    public class FileServiceTests
    {
        private readonly IFileService _fileService;
        private readonly ITransacaoFinanceiraService _transacoesBancariasService;

        public FileServiceTests()
        {
            var serviceProvider = ServiceCollectionExtensionsTest.DependencyInjectionTest();

            _fileService = serviceProvider?.GetService<IFileService>();
            _transacoesBancariasService = serviceProvider?.GetService<ITransacaoFinanceiraService>();
        }

        [Fact]
        public async Task UploadArquivoAsync_DeveSalvarDadosNoBancoDeDados()
        {
            // Arrange

            // Simule o caminho do arquivo .txt
            var fileName = "CNAB.txt";
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // colar o arquivo CNAB.txt na area de trabalho
            var path = Path.Combine(pathDesktop, fileName);

            // Leia o conteúdo do arquivo .txt
            var fileSystem = new FileSystem();
            var fileContent = fileSystem.File.ReadAllText(path);

            // Crie um mock do IFormFile usando o conteúdo real do arquivo
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(fileContent);
            writer.Flush();
            memoryStream.Position = 0;

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(memoryStream.Length);
            fileMock.Setup(f => f.OpenReadStream()).Returns(memoryStream);

            //Parse do arquivo
            var result = await _fileService.ReadAsStringAsync(fileMock.Object);
            var inputModel = result.Data as List<TransacoesFinanceiraInputModel>;

            // Act
            var transacaoSalva = await _transacoesBancariasService.SalvarTransacoes(inputModel);

            // Assert
            Assert.NotNull(transacaoSalva.Data);
            Assert.Equal("Registrado com sucesso", transacaoSalva.Mensagem);
        }

        [Fact]
        public async Task UploadFile_WithInvalidFile_ShouldReturnNull()
        {
            // Arrange
            var fileName = "CNAB.pdf";
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(0);

            //Act
            var result = await _fileService.ReadAsStringAsync(fileMock.Object);

            // Assert
            Assert.Equal("Arquivo Invalido", result.Mensagem);
            Assert.Null(result.Data);

        }
    }
}

