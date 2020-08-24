using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects
{
    public class GetSubjectsForTeacherCommandHandler : IRequestHandler<GetSubjectsForTeacherCommand, IEnumerable<TaughtSubject>>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly ITaughtSubjectRepository taughtSubjectRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public GetSubjectsForTeacherCommandHandler(ITeacherRepository teacherRepository, ITaughtSubjectRepository taughtSubjectRepository,
            IRepository<ApplicationUser> userRepository)
        {
            this.teacherRepository = teacherRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetSubjectsForTeacherCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);
            if (userExists)
            {
                var allTeachers = await teacherRepository.GetAllWithRelatedEntities();
                var teacher = allTeachers.Where(x => x.User.Id == request.UserId).First();
                var allTaughtSubjects = await taughtSubjectRepository.GetAllWithRelatedEntities();
                return allTaughtSubjects.Where(x => x.Teacher.Id == teacher.Id && x.Type == request.Type);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
