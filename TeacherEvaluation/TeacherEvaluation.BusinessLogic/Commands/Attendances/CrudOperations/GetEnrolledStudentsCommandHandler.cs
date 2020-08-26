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
    public class GetEnrolledStudentsCommandHandler : IRequestHandler<GetEnrolledStudentsCommand, IEnumerable<Student>>
    {
        private readonly ITaughtSubjectRepository taughtSubjectRepository;
        private readonly IRepository<Subject> subjectRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrolledStudentsCommandHandler(ITaughtSubjectRepository taughtSubjectRepository, 
            IRepository<Subject> subjectRepository, ITeacherRepository teacherRepository, 
            IRepository<ApplicationUser> userRepository, IEnrollmentRepository enrollmentRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
            this.teacherRepository = teacherRepository;
            this.subjectRepository = subjectRepository;
            this.userRepository = userRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetEnrolledStudentsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.SubjectId);
            if(userExists && subjectExists)
            {
                var teacher = (await teacherRepository.GetAllWithRelatedEntities())
                              .Where(x => x.User.Id == request.UserId)
                              .First();

                bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Subject.Id == request.SubjectId && 
                                                                                     x.Teacher.Id == teacher.Id && 
                                                                                     x.Type == request.Type);

                var taughtSubject = (await taughtSubjectRepository.GetAllWithRelatedEntities())
                                    .Where(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id && x.Type == request.Type)
                                    .First();
                
                if(taughtSubjectExists)
                {
                    var enrollments = await enrollmentRepository.GetEnrollmentsForTaughtSubject(taughtSubject.Id);
                    return enrollments.Select(x => x.Student).ToList();
                }
                else
                {
                    throw new ItemNotFoundException("The taught subject was not found...");
                }
            }
            throw new ItemNotFoundException("The user or the subject was not found...");
        }
    }
}
