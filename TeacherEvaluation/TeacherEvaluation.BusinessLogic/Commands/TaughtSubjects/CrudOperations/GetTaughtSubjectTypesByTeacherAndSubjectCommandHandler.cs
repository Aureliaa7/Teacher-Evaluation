using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    class GetTaughtSubjectTypesByTeacherAndSubjectCommandHandler : IRequestHandler<GetTaughtSubjectTypesByTeacherAndSubjectCommand, List<TaughtSubjectType>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTaughtSubjectTypesByTeacherAndSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }

        public async Task<List<TaughtSubjectType>> Handle(GetTaughtSubjectTypesByTeacherAndSubjectCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.TeacherId);
            if(!teacherExists)
            {
                throw new ItemNotFoundException("The teacher was not found!");
            }

            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(s => s.Id == request.SubjectId);
            if (!subjectExists)
            {
                throw new ItemNotFoundException("The subject was not found!");
            }

            var taughtSubjects = await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteria(
                ts => ts.Subject.Id == request.SubjectId && ts.Teacher.Id == request.TeacherId);
            var types = taughtSubjects.Select(t => t.Type).ToList();

            return types;
        }
    }
}
