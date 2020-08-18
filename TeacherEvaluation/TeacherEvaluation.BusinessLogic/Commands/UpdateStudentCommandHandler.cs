using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class UpdateStudentCommandHandler : AsyncRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository studentRepository;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        protected override async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await studentRepository.Exists(x => x.Id == request.Id);
            if (subjectExists)
            { 
                Student studentToBeUpdated = await studentRepository.GetStudent(request.Id);
                studentToBeUpdated.PIN = request.PIN;
                studentToBeUpdated.Section = request.Section;
                studentToBeUpdated.Group = request.Group;
                studentToBeUpdated.StudyProgramme = request.StudyProgramme;
                studentToBeUpdated.StudyYear = request.StudyYear;
                studentToBeUpdated.User.FirstName = request.FirstName;
                studentToBeUpdated.User.LastName = request.LastName;
                studentToBeUpdated.User.Email = request.Email;
                studentToBeUpdated.User.FathersInitial = request.FathersInitial;
                await studentRepository.Update(studentToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The student was not found...");
            }
        }
    }
}
