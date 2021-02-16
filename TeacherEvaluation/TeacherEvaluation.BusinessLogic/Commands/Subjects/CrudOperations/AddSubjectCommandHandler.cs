﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class AddSubjectCommandHandler : AsyncRequestHandler<AddSubjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject newSubject = new Subject
            {
                Name = request.Name,
                NumberOfCredits = request.NumberOfCredits
            };
            await unitOfWork.SubjectRepository.Add(newSubject);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
