﻿using Entities.Models;

namespace Contracts
{
    public interface IClassroomRepository
    {
        IEnumerable<Classroom> GetClassrooms(Guid schoolId, bool trackChanges);
        Classroom GetClassroom(Guid schoolId, Guid id, bool trackChanges);

    }
}
