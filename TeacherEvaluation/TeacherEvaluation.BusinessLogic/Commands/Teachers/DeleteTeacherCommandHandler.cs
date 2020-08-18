using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class DeleteTeacherCommandHandler : AsyncRequestHandler<DeleteTeacherCommand>
    {
        private readonly ITeacherRepository teacherRepository;

        public DeleteTeacherCommandHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        protected override async Task Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await teacherRepository.Exists(x => x.Id == request.Id);
            if (teacherExists)
            {
                await teacherRepository.Delete(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The teacher was not found...");
            }
        }
    }
}
