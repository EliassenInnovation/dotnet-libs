﻿using Eliassen.MongoDB.Extensions;
using MongoDB.Driver;
using Nucleus.Lesson.Persistence.Collections;

namespace Nucleus.Lesson.Persistence.Services
{
    public interface ILessonMongoDatabase
    {
        [CollectionName("lessons")]
        IMongoCollection<LessonCollection> Lessons { get; }
    }
}
