using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class SundaySchoolClassroom : BaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; private set; }

    public string Name { get; set; }

    public bool Status { get; set; }

    public int ChurchId { get; set; }

    public Church Church { get; set; }

    // Collections
    public ICollection<SundaySchoolClass> Classes { get; set; } = new List<SundaySchoolClass>();

    private SundaySchoolClassroom() { }

    public SundaySchoolClassroom(int id, Guid guid, string name, int churchId, bool status = true)
    {
        Id = id;
        Guid = guid;
        Name = name;
        Status = status;
        ChurchId = churchId;
    }
}
