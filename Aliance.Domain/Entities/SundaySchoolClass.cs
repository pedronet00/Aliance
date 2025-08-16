using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class SundaySchoolClass
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }

    public string Name { get; set; }

    public bool Status { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    private SundaySchoolClass() { }

    public SundaySchoolClass(int id, Guid guid, string name, bool status, int churchId)
    {
        Id = id;
        Guid = guid;
        Name = name;
        Status = status;
        ChurchId = churchId;
    }
}
