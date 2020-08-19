using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects
{
    public class AssignSubjectCommandHandler : AsyncRequestHandler<AssignSubjectCommand>
    {
        private readonly IRepository<Subject> subjectRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IRepository<TaughtSubject> taughtSubjectRepository;

        public AssignSubjectCommandHandler(IRepository<Subject> subjectRepository, ITeacherRepository teacherRepository, 
            IRepository<TaughtSubject> taughtSubjectRepository)
        {
            this.subjectRepository = subjectRepository;
            this.teacherRepository = teacherRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        protected override async Task Handle(AssignSubjectCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await teacherRepository.Exists(x => x.Id == request.TeacherId);
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.SubjectId);

            if (teacherExists && subjectExists)
            {
                Subject subject = await subjectRepository.Get(request.SubjectId);
                Teacher teacher = await teacherRepository.GetTeacher(request.TeacherId);
                TaughtSubject taughtSubject = new TaughtSubject { Teacher = teacher, Subject = subject, Type = request.Type };
                await taughtSubjectRepository.Add(taughtSubject);
            }
            else
            {
                throw new ItemNotFoundException("The teacher or the subject was not found...");
            }
        }
    }
}
