using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class PatrimonyMaintenance
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }
    public DateTime MaintanceDate { get; set; }
    public string Description { get; set; }
    public int PatrimonyId { get; set; }
    public Patrimony Patrimony { get; set; }
    private PatrimonyMaintenance() { }
    public PatrimonyMaintenance(DateTime maintanceDate, string description, int patrimonyId)
    {
        Guid = Guid.NewGuid();
        MaintanceDate = maintanceDate;
        Description = description;
        PatrimonyId = patrimonyId;
    }
}
