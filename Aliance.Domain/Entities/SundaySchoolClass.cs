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

    public string Lesson { get; set; }

    public string TeacherId { get; set; }

    public ApplicationUser Teacher { get; set; }

    public int SundaySchoolClassroomId { get; set; }

    public SundaySchoolClassroom SundaySchoolClassroom { get; set; }

    private SundaySchoolClass() { }

    public SundaySchoolClass(string lesson, string teacherId, int sundaySchoolClassroomId)
    {
        Guid = Guid.NewGuid();
        Lesson = lesson;
        TeacherId = teacherId;
        SundaySchoolClassroomId = sundaySchoolClassroomId;
    }

}
