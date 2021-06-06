using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    class ResetGradeCommandHandler : AsyncRequestHandler<ResetGradeCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResetGradeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected async override Task Handle(ResetGradeCommand request, CancellationToken cancellationToken)
        {
            bool gradeExists = await unitOfWork.GradeRepository.ExistsAsync(g => g.Id == request.GradeId);
            if(!gradeExists)
            {
                throw new ItemNotFoundException("The grade was not found!");
            }

            var grade = await unitOfWork.GradeRepository.GetAsync(request.GradeId);
            grade.Date = null;
            grade.Value = null;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
