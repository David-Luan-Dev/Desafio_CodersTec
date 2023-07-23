
namespace MovimentacoesFinanceira.Core.Notifications
{
    public class ResultNotifications
    {
        public ResultNotifications(string mensagens,object? data = null)
        {
            Data = data;
            Mensagem = mensagens;
        }

        public object? Data { get; set; }
        public string Mensagem { get; set; }
    }
}
