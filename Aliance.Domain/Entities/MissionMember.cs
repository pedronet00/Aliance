﻿namespace Aliance.Domain.Entities;

public class MissionMember
{
    public int Id { get; set; }

    public Guid Guid { get; private set; }

    public int MemberId { get; set; }
    public ApplicationUser Member { get; set; }

    public int MissionId { get; set; }

    public Mission Mission { get; set; }

    public bool Status { get; set; } = true;

    private MissionMember() { }

    public MissionMember(int memberId, int missionId, bool status)
    {
        MemberId = memberId;
        MissionId = missionId;
        Guid = Guid.NewGuid();
        Status = status;
    }
}
