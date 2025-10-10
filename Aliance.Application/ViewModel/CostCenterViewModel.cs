namespace Aliance.Application.ViewModel;

public class CostCenterViewModel
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string? Name { get; set; }

    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; }

    public int ChurchId { get; set; }

    public bool Status { get; set; }
}
