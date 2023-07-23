using Microsoft.AspNetCore.Mvc;
using MovimentacoesFinanceira.Application.InputModels;
using MovimentacoesFinanceira.Application.Interfaces;
using MovimentacoesFinanceira.Application.Services.Interfaces;

namespace Movimentacoes.Financeira.ByCodersTec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacoesFinanceiraController : ControllerBase
    {
        private readonly ITransacaoFinanceiraService _transacoesBancariasService;
        private readonly IFileService _fileService;
        private readonly ILogger<MovimentacoesFinanceiraController> _logger;

        public MovimentacoesFinanceiraController(
                ILogger<MovimentacoesFinanceiraController> logger,
                ITransacaoFinanceiraService transacoesBancariasService,
                IFileService fileService
            )
        {
            _logger = logger;
            _transacoesBancariasService = transacoesBancariasService;
            _fileService = fileService;
        }

        /// <summary>
        /// arquivo CNAB para salvar as movimentações financeira, aceita somente arquivo .txt
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("ArquivoCNAB")]
        public async Task<ActionResult> PostSingleFile(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("Arquivo vazio");
            }

            try
            {
                var resultFile = await _fileService.ReadAsStringAsync(file);
                var inputModel = resultFile.Data as List<TransacoesFinanceiraInputModel>;
                if (inputModel == null)
                {
                    return BadRequest(resultFile);
                }

                var resultTransacoes = await _transacoesBancariasService.SalvarTransacoes(inputModel);
                if (resultFile.Data == null)
                {
                    return BadRequest(resultTransacoes);
                }

                return Ok(resultTransacoes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// Retorna as movimentações daa lojaj e suas transações.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTransacoes")]
        public async Task<ActionResult> GetAll()
        {
            var Lojas = await _transacoesBancariasService.getAll();
            return Ok(Lojas);
        }
    }
}