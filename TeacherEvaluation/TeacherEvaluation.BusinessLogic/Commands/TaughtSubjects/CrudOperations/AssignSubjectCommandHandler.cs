using MediatR;
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

        public AssignSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(AssignSubjectCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(x => x.Id == request.TeacherId);
            bool subjectExists = await unitOfWork.SubjectRepository.Exists(x => x.Id == request.SubjectId);

            if (teacherExists && subjectExists)
            {
                Subject subject = await unitOfWork.SubjectRepository.Get(request.SubjectId);
                Teacher teacher = await unitOfWork.TeacherRepository.GetTeacher(request.TeacherId);
                TaughtSubject taughtSubject = new TaughtSubject { Teacher = teacher, Subject = subject, Type = request.Type };
                await unitOfWork.TaughtSubjectRepository.Add(taughtSubject);
            }
            else
            {
                throw new ItemNotFoundException("The teacher or the subject was not found...");
            }
        }
    }
}
