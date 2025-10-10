using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.DTOs;

public class CellMemberDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public Guid CellId { get; set; }
    public Guid UserId { get; set; }
}
