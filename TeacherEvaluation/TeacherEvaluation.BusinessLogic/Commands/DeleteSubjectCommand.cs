﻿using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class DeleteSubjectCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
