using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class UpdateStudentCommandHandler : AsyncRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository studentRepository;
        private readonly ISpecializationRepository specializationRepository;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, ISpecializationRepository specializationRepository)
        {
            this.studentRepository = studentRepository;
            this.specializationRepository = specializationRepository;
        }

        protected override async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.Id);
            bool specializationExists = await specializationRepository.Exists(x => x.Id == request.SpecializationId);
            if (studentExists && specializationExists)
            {
                var specialization = await specializationRepository.GetSpecialization(request.SpecializationId);
                Student studentToBeUpdated = await studentRepository.GetStudent(request.Id);
                studentToBeUpdated.PIN = request.PIN;
                studentToBeUpdated.Specialization = specialization;
                studentToBeUpdated.Group = request.Group;
                studentToBeUpdated.StudyYear = request.StudyYear;
                studentToBeUpdated.User.FirstName = request.FirstName;
                studentToBeUpdated.User.LastName = request.LastName;
                studentToBeUpdated.User.Email = request.Email;
                studentToBeUpdated.User.FathersInitial = request.FathersInitial;
                await studentRepository.Update(studentToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The student or the specialization was not found...");
            }
        }
    }
}
