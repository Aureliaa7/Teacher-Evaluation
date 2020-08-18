using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class UpdateTeacherCommandHandler : AsyncRequestHandler<UpdateTeacherCommand>
    {
        private readonly ITeacherRepository teacherRepository;

        public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        protected override async Task Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await teacherRepository.Exists(x => x.Id == request.Id);
            if (teacherExists)
            {
                Teacher teacherToBeUpdated = await teacherRepository.GetTeacher(request.Id);
                teacherToBeUpdated.PIN = request.PIN;
                teacherToBeUpdated.Degree = request.Degree;
                teacherToBeUpdated.Department = request.Department;
                teacherToBeUpdated.User.FirstName = request.FirstName;
                teacherToBeUpdated.User.LastName = request.LastName;
                teacherToBeUpdated.User.Email = request.Email;
                teacherToBeUpdated.User.FathersInitial = request.FathersInitial;
                await teacherRepository.Update(teacherToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The teacher was not found...");
            }
        }
    }
}
