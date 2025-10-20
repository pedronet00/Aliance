using Aliance.Domain.Enums;

namespace Aliance.Application.ViewModel;

public class ServiceViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public DateTime Date { get; set; }

    public ServiceStatus Status { get; set; }

    public int LocationId { get; set; }

    public string LocationName { get; set; }

    public int ChurchId { get; set; }
}
