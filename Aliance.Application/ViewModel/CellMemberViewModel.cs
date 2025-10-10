using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class CellMemberViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid CellGuId { get; set; }
    public Guid UserId { get; set; }
}
