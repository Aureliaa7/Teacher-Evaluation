using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherByCriteriaCommandHandler : IRequestHandler<GetTeacherByCriteriaCommand, Teacher>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeacherByCriteriaCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Teacher> Handle(GetTeacherByCriteriaCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserIdForStudent);
            if(userExists)
            {
                bool subjectExists = await unitOfWork.SubjectRepository.Exists(x => x.Id == request.SubjectId);
                if(subjectExists)
                {
                    var student = await unitOfWork.StudentRepository.GetByUserId(request.UserIdForStudent);
                    var enrollment = await unitOfWork.EnrollmentRepository.GetEnrollmentBySubjectStateTypeAndStudent(
                        request.SubjectId, request.EnrollmentState, request.SubjectType, student.Id);
                    var teacherId = enrollment.TaughtSubject.Teacher.Id;
                    return await unitOfWork.TeacherRepository.GetTeacher(teacherId);
                }
            }
            throw new ItemNotFoundException("The item was not found...");
        }
    }
}
