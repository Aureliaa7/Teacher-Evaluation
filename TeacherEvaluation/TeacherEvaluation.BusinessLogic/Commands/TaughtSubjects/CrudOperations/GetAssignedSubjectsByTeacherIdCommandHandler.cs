using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    class GetAssignedSubjectsByTeacherIdCommandHandler : IRequestHandler<GetAssignedSubjectsByTeacherIdCommand, IDictionary<string, string>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAssignedSubjectsByTeacherIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, string>> Handle(GetAssignedSubjectsByTeacherIdCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.TeacherId);
            if(!teacherExists)
            {
                throw new Exception("The teacher was not found!");
            }

            var taughtSubjects = await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteriaAsync(
                ts => ts.Teacher.Id == request.TeacherId);

            var subjectsAndIds = new Dictionary<string, string>();
            foreach (var taughtSubject in taughtSubjects)
            {
                if (!subjectsAndIds.ContainsKey(taughtSubject.Subject.Id.ToString()))
                {
                    subjectsAndIds.Add(taughtSubject.Subject.Id.ToString(), taughtSubject.Subject.Name);
                }
            }

            return subjectsAndIds;
        }
    }
}
