using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class UpdateTeacherCommandHandler : AsyncRequestHandler<UpdateTeacherCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateTeacherCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(x => x.Id == request.Id);
            if (teacherExists)
            {
                Teacher teacherToBeUpdated = await unitOfWork.TeacherRepository.GetTeacher(request.Id);
                teacherToBeUpdated.User.PIN = request.PIN;
                teacherToBeUpdated.Degree = request.Degree;
                teacherToBeUpdated.Department = request.Department;
                teacherToBeUpdated.User.FirstName = request.FirstName;
                teacherToBeUpdated.User.LastName = request.LastName;
                teacherToBeUpdated.User.Email = request.Email;
                teacherToBeUpdated.User.FathersInitial = request.FathersInitial;
                unitOfWork.TeacherRepository.Update(teacherToBeUpdated);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The teacher was not found...");
            }
        }
    }
}
