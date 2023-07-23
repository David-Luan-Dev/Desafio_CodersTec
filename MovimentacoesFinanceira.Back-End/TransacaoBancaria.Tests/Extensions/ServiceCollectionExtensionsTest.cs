using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movimentacoes.Financeira.API.Extensions;

namespace MovimentacoesFinanceira.Tests.Extensions
{
    public static class ServiceCollectionExtensionsTest
    {
        public static ServiceProvider DependencyInjectionTest()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            var serviceProvider = new ServiceCollection()
                .AddRepositories()
                .AddServices()
                .AddContext(configuration)
                .AddAutoMapper()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
