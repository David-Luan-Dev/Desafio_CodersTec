using Microsoft.AspNetCore.Http;
using MovimentacoesFinanceira.Application.InputModels;
using MovimentacoesFinanceira.Core.Notifications;

namespace MovimentacoesFinanceira.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task<ResultNotifications> ReadAsStringAsync(IFormFile file);

    }
}
