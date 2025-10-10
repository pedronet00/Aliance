using Aliance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class PastoralVisitViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime VisitDate { get; set; }
    public string Description { get; set; }
    public string VisitedMemberId { get; set; }
    public string VisitedMemberName { get; set; }
    public string PastorId { get; set; }
    public string PastorName { get; set; }
    public MeetingStatus Status { get; set; }
    public int ChurchId { get; set; }
}
