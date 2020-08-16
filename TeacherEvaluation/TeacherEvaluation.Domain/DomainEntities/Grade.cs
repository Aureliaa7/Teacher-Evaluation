﻿using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Grade
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
        public GradeType Type { get; set; }
    }
}
