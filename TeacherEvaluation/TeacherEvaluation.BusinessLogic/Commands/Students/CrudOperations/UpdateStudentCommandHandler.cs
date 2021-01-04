﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class UpdateStudentCommandHandler : AsyncRequestHandler<UpdateStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.Exists(x => x.Id == request.Id);
            bool specializationExists = await unitOfWork.SpecializationRepository.Exists(x => x.Id == request.SpecializationId);
            if (studentExists && specializationExists)
            {
                var specialization = await unitOfWork.SpecializationRepository.GetSpecialization(request.SpecializationId);
                Student studentToBeUpdated = await unitOfWork.StudentRepository.GetStudent(request.Id);
                studentToBeUpdated.PIN = request.PIN;
                studentToBeUpdated.Specialization = specialization;
                studentToBeUpdated.Group = request.Group;
                studentToBeUpdated.StudyYear = request.StudyYear;
                studentToBeUpdated.User.FirstName = request.FirstName;
                studentToBeUpdated.User.LastName = request.LastName;
                studentToBeUpdated.User.Email = request.Email;
                studentToBeUpdated.User.FathersInitial = request.FathersInitial;
                await unitOfWork.StudentRepository.Update(studentToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The student or the specialization was not found...");
            }
        }
    }
}
