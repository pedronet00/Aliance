using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Entities;

public class SundaySchool
{
    
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public string Lesson { get; set; }

    public int TeacherId { get; set; }

    public ApplicationUser Teacher { get; set; }

    public int SundaySchoolClassId { get; set; }

    public SundaySchoolClass SundaySchoolClass { get; set; }

    private SundaySchool() { }

    public SundaySchool(string lesson, int teacherId, int sundaySchoolClassId)
    {
        Guid = Guid.NewGuid();
        Lesson = lesson;
        TeacherId = teacherId;
        SundaySchoolClassId = sundaySchoolClassId;
    }

}
