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
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserId(request.UserId);
                var enrollments = await unitOfWork.EnrollmentRepository.GetForStudent(student.Id);
                var takenSubjectsVm = enrollments.Where(x => x.TaughtSubject.Type == request.SubjectType && x.State == request.EnrollmentState)
                                                .Select(x => new TakenSubjectVm
                                                {
                                                    TaughtSubject = x.TaughtSubject,
                                                    NumberOfAttendances = x.NumberOfAttendances
                                                });
                
                return takenSubjectsVm;
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
