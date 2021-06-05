using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class AssignSubjectCommandHandler : AsyncRequestHandler<AssignSubjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;

        public AssignSubjectCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        protected override async Task Handle(AssignSubjectCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(x => x.Id == request.TeacherId);
            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(x => x.Id == request.SubjectId);

            if (teacherExists && subjectExists)
            {
                var command = new AssignedSubjectVerificationCommand
                {
                    SubjectId = request.SubjectId,
                    TeacherId = request.TeacherId,
                    Type = request.Type
                };

                bool assignmentExists = await mediator.Send(command);
                if(assignmentExists)
                {
                    throw new Exception($"The subject with the id {request.SubjectId} " +
                        $"is already assigned to the teacher with the id {request.TeacherId}");
                }

                Subject subject = await unitOfWork.SubjectRepository.GetAsync(request.SubjectId);
                Teacher teacher = await unitOfWork.TeacherRepository.GetTeacherAsync(request.TeacherId);
                TaughtSubject taughtSubject = new TaughtSubject
                {
                    Teacher = teacher,
                    Subject = subject,
                    Type = request.Type,
                    MaxNumberOfAttendances = request.MaxNumberOfAttendances
                };

                await unitOfWork.TaughtSubjectRepository.AddAsync(taughtSubject);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The teacher or the subject was not found...");
            }
        }
    }
}
