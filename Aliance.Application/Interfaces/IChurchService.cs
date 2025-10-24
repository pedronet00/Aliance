using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;

namespace Aliance.Application.Interfaces;

public interface IChurchService
{
    Task<IEnumerable<ChurchViewModel>> GetAllChurches();

    Task<string> GetChurchesFirstUser(string churchAsaasId);

    Task<ChurchViewModel> GetChurchById(int id);

    Task<ChurchViewModel> InsertChurch(ChurchDTO church);

    Task<bool> UpdateChurch(ChurchDTO church);

    Task<bool> DeleteChurch(int id);

    Task<ChurchViewModel> GetChurchByAsaasCustomerId(string asaasCustomerId);
    Task AtualizarPagamentoRecebidoAsync(string customerId, DateTime dataRecebimento, DateTime? proximaCobranca, decimal valorPago);
    Task AtualizarPagamentoAtrasadoAsync(string customerId, DateTime dataVencimento);


}
