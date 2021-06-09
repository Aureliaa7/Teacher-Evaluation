using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetSubjectsForStudentCommandHandler : IRequestHandler<GetSubjectsForStudentCommand, IEnumerable<TakenSubjectVm>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TakenSubjectVm>> Handle(GetSubjectsForStudentCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserId);
                var enrollments = await unitOfWork.EnrollmentRepository.GetForStudentAsync(student.Id);
                var takenSubjectsVm = enrollments.Where(x => x.TaughtSubject.Type == request.SubjectType && x.State == request.EnrollmentState)
                                                .Select(x => new TakenSubjectVm
                                                {
                                                    SubjectTitle = x.TaughtSubject.Subject.Name,
                                                    NumberOfAttendances = x.NumberOfAttendances,
                                                    NumberOfCredits = x.TaughtSubject.Subject.NumberOfCredits,
                                                    TeacherName = x.TaughtSubject.Teacher.User.LastName + " " +
                                                    x.TaughtSubject.Teacher.User.FathersInitial + " " +
                                                    x.TaughtSubject.Teacher.User.FirstName
                                                });
                
                return takenSubjectsVm;
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
