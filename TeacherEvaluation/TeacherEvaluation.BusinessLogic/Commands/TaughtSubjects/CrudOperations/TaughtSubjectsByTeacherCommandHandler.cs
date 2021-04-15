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
    public class TaughtSubjectsByTeacherCommandHandler : IRequestHandler<TaughtSubjectsByTeacherCommand, IDictionary<string, string>>
    {
        private readonly IUnitOfWork unitOfWork;

        public TaughtSubjectsByTeacherCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, string>> Handle(TaughtSubjectsByTeacherCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.Id == request.TeacherId);
            if(teacherExists)
            {
                IDictionary<string, string> taughtSubjectsIdsAndTitles = new Dictionary<string, string>();
                var taughtSubjects = await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteria(ts => ts.Teacher.Id == request.TeacherId);
                taughtSubjects = taughtSubjects.OrderBy(ts => ts.Subject.Name);
                foreach(var ts in taughtSubjects)
                {
                    string title = ts.Subject.Name;
                    title = ts.Type == TaughtSubjectType.Course ? string.Concat(title, "(course)") : string.Concat(title, "(laboratory)");
                    taughtSubjectsIdsAndTitles.Add(ts.Id.ToString(), title);
                }
                return taughtSubjectsIdsAndTitles;
            }
            throw new ItemNotFoundException("The teacher was not found");
        }
    }
}
