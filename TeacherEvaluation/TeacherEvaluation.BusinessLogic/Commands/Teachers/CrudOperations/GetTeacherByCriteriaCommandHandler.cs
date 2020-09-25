using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherByCriteriaCommandHandler : IRequestHandler<GetTeacherByCriteriaCommand, Teacher>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<Subject> subjectRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly ITeacherRepository teacherRepository;

        public GetTeacherByCriteriaCommandHandler(IEnrollmentRepository enrollmentRepository, IRepository<Subject> subjectRepository, 
            IStudentRepository studentRepository, IRepository<ApplicationUser> userRepository, ITeacherRepository teacherRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.subjectRepository = subjectRepository;
            this.studentRepository = studentRepository;
            this.userRepository = userRepository;
            this.teacherRepository = teacherRepository;
        }

        public async Task<Teacher> Handle(GetTeacherByCriteriaCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserIdForStudent);
            if(userExists)
            {
                bool subjectExists = await subjectRepository.Exists(x => x.Id == request.SubjectId);
                if(subjectExists)
                {
                    var student = await studentRepository.GetByUserId(request.UserIdForStudent);
                    var enrollment = await enrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudent(
                        request.SubjectId, request.EnrollmentState, request.SubjectType, student.Id);
                    var teacherId = enrollment.TaughtSubject.Teacher.Id;
                    return await teacherRepository.GetTeacher(teacherId);
                }
            }
            throw new ItemNotFoundException("The item was not found...");
        }
    }
}
