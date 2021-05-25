using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetEnrolledStudentsCommandHandler : IRequestHandler<GetEnrolledStudentsCommand, IEnumerable<Student>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetEnrolledStudentsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> Handle(GetEnrolledStudentsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(x => x.Id == request.SubjectId);
            if(userExists && subjectExists)
            {
                var teacher = (await unitOfWork.TeacherRepository.GetAllWithRelatedEntitiesAsync())
                              .Where(x => x.User.Id == request.UserId)
                              .First();

                bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Subject.Id == request.SubjectId && 
                                                                                     x.Teacher.Id == teacher.Id && 
                                                                                     x.Type == request.Type);

                if(taughtSubjectExists)
                {
                    var taughtSubject = (await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntitiesAsync())
                                    .Where(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id && x.Type == request.Type)
                                    .First();

                    var enrollments = (await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubject(taughtSubject.Id))
                                      .Where(x => x.State == Domain.DomainEntities.Enums.EnrollmentState.InProgress &&
                                       x.NumberOfAttendances == 0);
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
