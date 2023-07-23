using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovimentacoesFinanceira.Application.Interfaces;
using MovimentacoesFinanceira.Application.Services.Implementations;
using MovimentacoesFinanceira.Application.Services.Interfaces;
using MovimentacoesFinanceira.Application.ViewModels;
using MovimentacoesFinanceira.Core.Entities;
using MovimentacoesFinanceira.Core.Repositories;
using MovimentacoesFinanceira.Infrastructure.Persistence;
using MovimentacoesFinanceira.Infrastructure.Persistence.Repositories;

namespace Movimentacoes.Financeira.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransacaoFinanceiraRepository, TransacoesBancariasRepository>();
            services.AddScoped<ILojaRepository, LojaRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITransacaoFinanceiraService, TransacaoFinanceiraService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("DevCodersTec"); // trocar a sua connectionString no appsettings.json
            services.AddDbContext<TransacoesBancariasDbContext>(
                options => options.UseSqlServer(connectionsString));

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransacoesFinanceiraViewModel, Loja>()
                .ForMember(l => l.TransacoesFinanceira, mo => mo.MapFrom(t => t.TransacoesFinanceira));

                cfg.CreateMap<Loja, TransacoesFinanceiraViewModel>()
                .ForMember(t => t.TransacoesFinanceira, mo => mo.MapFrom(l => l.TransacoesFinanceira));
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        } 

    }
}
