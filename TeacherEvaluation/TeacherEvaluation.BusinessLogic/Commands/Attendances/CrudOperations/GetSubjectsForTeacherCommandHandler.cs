using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetSubjectsForTeacherCommandHandler : IRequestHandler<GetSubjectsForTeacherCommand, IEnumerable<TaughtSubject>>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly ITaughtSubjectRepository taughtSubjectRepository;

        public GetSubjectsForTeacherCommandHandler(ITeacherRepository teacherRepository, 
            IRepository<ApplicationUser> userRepository, ITaughtSubjectRepository taughtSubjectRepository)
        {
            this.teacherRepository = teacherRepository;
            this.userRepository = userRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetSubjectsForTeacherCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);
            bool teacherExists = await teacherRepository.Exists(x => x.User.Id == request.UserId);
            if(userExists && teacherExists)
            {
                var teacher = (await teacherRepository.GetAllWithRelatedEntities()).Where(x => x.User.Id == request.UserId).First();
                var taughtSubjects = await taughtSubjectRepository.GetAllWithRelatedEntities();
                return (IEnumerable<TaughtSubject>)taughtSubjects.Where(x => x.Teacher.Id == teacher.Id);
            }
            throw new ItemNotFoundException("The teacher was not found...");
        }
    }
}
