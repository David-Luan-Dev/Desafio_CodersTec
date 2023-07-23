using Microsoft.AspNetCore.Http;
using MovimentacoesFinanceira.Application.InputModels;
using MovimentacoesFinanceira.Application.Services.Interfaces;
using MovimentacoesFinanceira.Core.Enums;
using MovimentacoesFinanceira.Core.Notifications;

namespace MovimentacoesFinanceira.Application.Services.Implementations
{
    public class FileService : IFileService
    {
        public FileService() { }

        public async Task<ResultNotifications> ReadAsStringAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (extension != ".txt" || file.Length == 0)
            {
                return await Task.FromResult(new ResultNotifications("Arquivo Invalido"));
            }

            var listInputModel = new List<TransacoesFinanceiraInputModel>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        var line = await reader.ReadLineAsync();
                        var tipo = getTipoTransacao(line);
                        var data_ocorrencia = FormatDate(line[1..9]);
                        var valor = ValorDivididoPorCem(int.Parse(line[9..19])); // regra escrita no documento.
                        var cpf = line[19..30];
                        var cartao = line[29..42];
                        var hora = FormatHours(line[42..48]);
                        var dono = line[48..62];
                        var nomeLoja = line[62..];

                        listInputModel.Add(new TransacoesFinanceiraInputModel(tipo, data_ocorrencia, valor, cpf, cartao, hora, dono, nomeLoja));
                    }
                    catch (Exception)
                    {
                        return await Task.FromResult(new ResultNotifications("Erro ao ler esse arquivo"));
                    }
                }
            }

            return await Task.FromResult(new ResultNotifications("Arquivo upload com sucesso", listInputModel));
        }

        public static int ValorDivididoPorCem(int valor)
        {
            return valor / 100;
        }

        public static TipoTransacoesEnum getTipoTransacao(string? line)
        {
            var tipoTransacao = line[..1];
            Enum.TryParse(tipoTransacao, out TipoTransacoesEnum myStatus);
            return myStatus;
        }

        public static string FormatHours(string time)
        {
            var hours = int.Parse(time[..2]); // hours 
            var minute = int.Parse(time[2..4]); // minute
            var second = int.Parse(time[4..6]); // second

            var newTimeString = new TimeSpan(hours, minute, second).ToString();
            return newTimeString;

        }

        public static string FormatDate(string dateFormat)
        {
            var year = int.Parse(dateFormat[..4]); // Year 
            var month = int.Parse(dateFormat[4..6]); // Month
            var day = int.Parse(dateFormat[6..8]); // Day
            var newDateTime = string.Format("{0:d}", new DateTime(year, month, day)); // formated pt-br

            return newDateTime;
        }
    }
}
