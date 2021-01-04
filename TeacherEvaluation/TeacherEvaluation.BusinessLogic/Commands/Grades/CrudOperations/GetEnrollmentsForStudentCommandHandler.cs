using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{ 
    public class GetEnrollmentsForStudentCommandHandler : IRequestHandler<GetEnrollmentsForStudentCommand, IEnumerable<Enrollment>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetEnrollmentsForStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Enrollment>> Handle(GetEnrollmentsForStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.Exists(x => x.Id == request.Id);
            if (studentExists)
            {
                return await unitOfWork.EnrollmentRepository.GetForStudent(request.Id);
            }
            throw new ItemNotFoundException("The student was not found...");
        }
    }
}
